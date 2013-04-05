using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Muphic.Common;
using Muphic.Tools;

namespace Muphic.Manager
{
	using DxManager = Microsoft.DirectX.Direct3D.Manager;

	/// <summary>
	/// 描画管理クラス ver.12 (シングルトン・継承不可) 
	/// <para>Direct3D を利用したテクスチャの描画及び管理、各部構成パーツで使用するテクスチャの登録・削除を行う。</para>
	/// </summary>
	public sealed class DrawManager : Manager
	{
		// muphic DrawManager Ver.12
		//   ・DirectX 使用
		//   ・統合画像採用
		//   ・動作エンジン再構築

		/// <summary>
		/// 描画管理クラスの静的インスタンス (シングルトンパターン)。
		/// </summary>
		private static readonly DrawManager __instance = new DrawManager();

		/// <summary>
		/// 描画管理クラスの静的インスタンス (シングルトンパターン) を取得する。
		/// </summary>
		private static DrawManager Instance
		{
			get { return Muphic.Manager.DrawManager.__instance; }
		}


		#region プロパティ

		/// <summary>
		/// テクスチャテーブル
		/// <para>テクスチャファイルパスをキーとし、統合画像本体を格納する。</para>
		/// </summary>
		private Dictionary<string, Texture> TextureTable { get; set; }

		/// <summary>
		/// 登録テーブル
		/// <para>RegistTexture で登録したキーを格納する。</para>
		/// </summary>
		private List<List<string>> RegistTable { get; set; }

		/// <summary>
		/// テクスチャ登録中であることを示す整数を取得または設定する。
		/// <para>登録中であれば 0 以上の RegistTable の index 番号、登録中でなければ -1 となる。</para>
		/// </summary>
		private int RegistNum { get; set; }

		/// <summary>
		/// DirectX での描画先の画面 (トップレベルウィンドウ)
		/// </summary>
		private MainWindow Owner { get; set; }


		/// <summary>
		/// ウィンドウモードであることを示す値を取得または設定する。
		/// </summary>
		private bool IsWindow
		{
			get { return ConfigurationManager.Current.IsWindow; }
			set { ConfigurationManager.Current.IsWindow = value; }
		}

		/// <summary>
		/// 主にフルスクリーンモードで使用するグラフィックアダプタの番号を取得または設定する。
		/// </summary>
		private int AdapterNum
		{
			get { return ConfigurationManager.Current.AdapterNum; }
			set { ConfigurationManager.Current.AdapterNum = value; }
		}

		/// <summary>
		/// NowLoading ダイアログ。
		/// </summary>
		private Dialog NowLoadingDialog { get; set; }


		/// <summary>
		/// 非同期でのテクスチャ読み込み用ワーカースレッド。
		/// </summary>
		private BackgroundWorker TextureLoader { get; set; }


		/// <summary>
		/// DirectX デバイス。
		/// </summary>
		private Device Device { get; set; }

		/// <summary>
		/// Direct3D スプライト。
		/// </summary>
		private Sprite Sprite { get; set; }

		/// <summary>
		/// Direct3D ライン。
		/// </summary>
		private Line Line { get; set; }

		/// <summary>
		/// 【デバッグ用】フォント。
		/// </summary>
		private Microsoft.DirectX.Direct3D.Font Font { get; set; }

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// 描画管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private DrawManager()
		{
			this.TextureTable = new Dictionary<string, Texture>();	// テクスチャテーブルの初期化
			this.RegistTable = new List<List<string>>();			// 登録テーブルの初期化
			this.RegistNum = -1;									// 登録中フラグも降ろしておく
			this.NowLoadingDialog = null;

			this.TextureLoader = new BackgroundWorker();
			this.TextureLoader.DoWork += new DoWorkEventHandler(this._TextureLoad);
			this.TextureLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this._TextureLoadCompleted);
		}


		/// <summary>
		/// 描画管理クラスの静的インスタンス生成及び使用する描画デバイス等の初期化を行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>初期化が正常に終了した場合は true、それ以外は false。</returns>
		private bool _Initialize(MainWindow mainScreen)
		{
			if (this._IsInitialized) return false;						// 初期化済みでない場合のみ以下を実行

			this.Owner = mainScreen;									// 描画先の設定

			if (!this._InitializeDevice()) return false;				// デバイスの初期化 失敗したらプログラム終了

			this._InitializeLine(this.Device);							// ラインの初期化

			this._InitializeFont();										// デバッグ時のみフォント生成

			DebugTools.ConsolOutputMessage("DrawManager -Initialize", "描画管理クラス生成完了", true);

			return this._IsInitialized = true;							// 初期化後に初期化済みフラグを立てる
		}

		/// <summary>
		/// デバイスの初期化を行う。
		/// </summary>
		/// <returns>初期化が正常に終了すれば true、異常が発生した場合は false。</returns>
		private bool _InitializeDevice()
		{
			try
			{
				if (!(this.AdapterNum < DxManager.Adapters.Count))
				{															// アダプタ数を調べ、指定されたアダプタ番号が妥当かチェックする
					this.AdapterNum = 0;									// アダプタの数を超える番号だった場合、強制的に 0 にする
				}

				PresentParameters pp = this._SetParameters(true);			// デバイス設定 (最初は Window モードで生成する)
				if (pp == null) return false;								// 設定に失敗したら戻る

				if (!this._CreateDevice(pp)) return false;					// デバイス生成
			}
			catch (TypeInitializationException exception)
			{
				// デバイス生成時にこの例外が発生したら、DirectXがインストールされていないと判断

				#region	ログの書き込みとエラーメッセージの表示

				LogFileManager.WriteLineError("TypeInitializationException", "プログラムの動作に必要なランタイムが不足しています。");
				LogFileManager.WriteLineError("                           ", "詳細は以下のメッセージを参照してください。");
				LogFileManager.WriteLineError(exception.ToString(), "");

				// メッセージウィンドウ表示
				MessageBox.Show(this.Owner,
					Properties.Resources.ErrorMsg_DrawMgr_Show_FailureDirectXLoad_Text,
					Properties.Resources.ErrorMsg_DrawMgr_Show_FailureDirectXLoad_Caption,
					MessageBoxButtons.OK, MessageBoxIcon.Error);

				#endregion

				return false;
			}

			this.Sprite = new Sprite(this.Device);							// スプライトオブジェクトのインスタンス化

			this.Owner.ControlBox = this.IsWindow;							// フルスクリーンモードの場合はキャプションバーを非表示

			LogFileManager.WriteLine(										// ウィンドウ/フルスクリーンの状態をログに書き込み
				Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
				this._GetDisplayModeName(this.IsWindow)
			);

			return true;
		}

		#endregion


		#region デバイス関連

		/// <summary>
		/// Direct3D.PresentParameters の設定を行う。
		/// </summary>
		/// <param name="isWindow">ウィンドウモードで設定を行う場合は true、フルスクリーンモードで設定を行う場合は false。</param>
		/// <returns>設定された Direct3D.PresentParameters。設定に失敗した場合は null。</returns>
		private PresentParameters _SetParameters(bool isWindow)
		{
			PresentParameters pp = new PresentParameters();		// パラメータ設定クラス デバイスを使用する環境の設定を行う

			pp.DeviceWindow = this.Owner;						// 描画対象のウィンドウを指定
			pp.DeviceWindowHandle = this.Owner.Handle;			// 描画するコントロールを指定
			pp.SwapEffect = SwapEffect.Discard;					// サーフェイスフリップはディスプレイドライバ側で設定させる(最良)
			pp.EnableAutoDepthStencil = false;					// 深度ステンシルバッファ 3Dではないのでfalseでいい
			pp.AutoDepthStencilFormat = DepthFormat.D16;		// 自動深度ステンシルサーフェイスのフォーマット 深度ステンシルバッファをfalseにしたため無視される
			pp.BackBufferCount = 0;								// バックバッファの枚数 0指定で1枚になると思われる
			pp.MultiSample = MultiSampleType.None;				// アンチエイリアスの設定 使用しないのでNone
			pp.MultiSampleQuality = 0;							// アンチエイリアスの品質 使用しないので0
			pp.PresentFlag = PresentFlag.None;					// 用途不明
			pp.PresentationInterval = PresentInterval.Default;	// 描画の書き換えのタイミングの指定 Defaultでいいらしい

			if (isWindow)										// ウィンドウモード / フルスクリーンモードに応じた設定を行う
			{
				pp.Windowed = true;										// ウィンドウモード時

				pp.BackBufferWidth = 0;									// Direct3Dデバイスが使用するバックバッファのサイズ
				pp.BackBufferHeight = 0;								// 0を指定するとDeviceWindowのクライアントサイズと同じになる
				pp.BackBufferFormat = Format.Unknown;					// バックバッファのフォーマット ウィンドウモードなのでUnknownでいい
				pp.FullScreenRefreshRateInHz = 0;						// リフレッシュレートの設定 ウィンドウモードなので0でいい
			}
			else
			{
				pp.Windowed = false;										// フルスクリーンモード時

				bool findDisplayMode = false;								// 使用するディスプレイモードが見つかった場合のフラグ
				int width = Settings.System.Default.WindowSize_Width;		// ウィンドウサイズ
				int height = Settings.System.Default.WindowSize_Height;		// ウィンドウサイズ
				int refreshRate = ConfigurationManager.Current.RefreshRate;	// 使用するリフレッシュレート				

				// ディスプレイモードを列挙し、クライアントと同じサイズかつ同じリフレッシュレートのモードを探す
				foreach (DisplayMode dm in DxManager.Adapters[this.AdapterNum].SupportedDisplayModes)
				{
					if (dm.Width == width && dm.Height == height && dm.RefreshRate == refreshRate)
					{
						pp.BackBufferWidth = dm.Width;					// 条件にマッチするディスプレイモードが存在すれば使用する
						pp.BackBufferHeight = dm.Height;				// バックバッファのサイズはクライアントと同じ
						pp.BackBufferFormat = dm.Format;				// フォーマットはディスプレイに従う
						pp.FullScreenRefreshRateInHz = refreshRate;		// リフレッシュレートは指定された値
						findDisplayMode = true;							// ディスプレイモードが見つかったことを示すフラグを立てる
						break;
					}
				}

				if (!findDisplayMode)
				{
					// 指定されたモードがなければ、エラーメッセージを表示して終了する
					MessageBox.Show(this.Owner,
						CommonTools.GetResourceMessage(
							Properties.Resources.ErrorMsg_DrawMgr_Show_FailureDisplayMode_Text,
							width.ToString(),
							height.ToString(),
							refreshRate.ToString()
						),
						Properties.Resources.ErrorMsg_DrawMgr_Show_FailureDisplayMode_Caption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);

					return null;
				}
			}

			return pp;
		}


		/// <summary>
		/// デバイスの生成を行う。
		/// </summary>
		/// <param name="pp">デバイス設定。</param>
		/// <returns>正常に生成された場合は true、それ以外は false。</returns>
		private bool _CreateDevice(PresentParameters pp)
		{
			try
			{
				// デバイス生成   ハードウェアアクセラレータ、ハードウェアによる頂点処理
				//                最上位のパフォーマンスとなるが、ビデオカードによっては実装できない処理が存在するため、
				//                失敗したら下位のパフォーマンスでデバイスを生成する
				this.Device = new Device(this.AdapterNum, DeviceType.Hardware, this.Owner.Handle, CreateFlags.HardwareVertexProcessing, pp);
				LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_CreateDevice, Properties.Resources.Msg_DrawMgr_CreateDevice_HH);
			}
			catch (DirectXException)
			{
				try
				{
					// デバイス生成  ハードウェアアクセラレータ、ソフトウェアによる頂点処理
					//               グラフィックがチップセット内臓のラップトップは大抵これになる
					this.Device = new Device(this.AdapterNum, DeviceType.Hardware, this.Owner.Handle, CreateFlags.SoftwareVertexProcessing, pp);
					LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_CreateDevice, Properties.Resources.Msg_DrawMgr_CreateDevice_HS);
				}
				catch (DirectXException)
				{
					try
					{
						// デバイス生成   リファレンスラスタライザ、ソフトウェアによる頂点処理
						//                パフォーマンスは最も低いが、殆どの処理を制限なく行うことができる
						//                これで失敗したら事実上デバイスは生成できない
						this.Device = new Device(this.AdapterNum, DeviceType.Reference, this.Owner.Handle, CreateFlags.SoftwareVertexProcessing, pp);
						LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_CreateDevice, Properties.Resources.Msg_DrawMgr_CreateDevice_SS);
					}
					catch (DirectXException e)
					{
						// デバイス生成失敗
						LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_CreateDevice, Properties.Resources.Msg_DrawMgr_CreateDevice_Failure);
						LogFileManager.WriteLine(e.ToString());

						// メッセージウィンドウを表示し終了
						MessageBox.Show(this.Owner,
							Properties.Resources.ErrorMsg_DrawMgr_Show_FailureCreateDevice_Text,
							Properties.Resources.ErrorMsg_DrawMgr_Show_FailureCreateDevice_Caption,
							MessageBoxButtons.OK, MessageBoxIcon.Error);

						return false;
					}
				}
			}

			return true;
		}


		/// <summary>
		/// デバイスの状態をチェックする。ロストしていた場合はデバイスの再生成を試みる。
		/// </summary>
		/// <returns>正常状態またはデバイスを再生成した場合は true、それ以外 (主にデバイスロスト時) は false。</returns>
		private bool _CheckDevice()
		{
			int deviceResult;	// デバイスの状態を表す(結果コード格納用)

			if (!this.Device.CheckCooperativeLevel(out deviceResult))
			{
				// CheckCooperativeLevelメソッドでデバイスの状態をチェック
				// 正常状態でなければfalseが返り、deviceResultに結果コードが格納される

				switch ((ResultCode)deviceResult)				// デバイス異常時は結果コードに応じた以下の処理を行う
				{
					case ResultCode.DeviceLost:					// デバイスをロストしており、リセットも出来ない状態であれば
						Thread.Sleep(1000);						// 1秒待機させ
						return false;							// falseを返す

					case ResultCode.DeviceNotReset:				// リセット可能状態であれば、デバイスを再生成してtrueを返す
						this._ResetDevice(this.Device.PresentationParameters);
						return true;

					default:									// それ以外の場合は予期していないため
						Muphic.MainWindow.Running = false;		// プログラム終了
						return false;							// falseを返す
				}
			}

			return true;		// 正常状態であればtrueを返す
		}


		/// <summary>
		/// デバイスの再生成を行う。
		/// </summary>
		/// <param name="pp">デバイス設定。</param>
		private void _ResetDevice(PresentParameters pp)
		{
			this.Device.Reset(pp);
			LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_ResetDevice);
		}


		/// <summary>
		/// 構成設定でフルスクリーンモードが設定されていた場合、フルスクリーンモードでデバイスを再生成する。起動フェーズ終了時に使用する。
		/// </summary>
		/// <returns>正常に再生成された場合、またはフルスクリーンモードに設定されていなかった場合は true、それ以外は false。</returns>
		private bool _SetFullScreenMode()
		{
			if (!this.IsWindow)
			{
				return this._ChangeWindowMode(this.IsWindow);
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// ウィンドウモード / フルスクリーンモードの切り替えを行う。
		/// </summary>
		/// <returns>正常に切り替えられた場合は true、それ以外は false。</returns>
		private bool _ChangeWindowMode()
		{
			return this._ChangeWindowMode(!this.IsWindow);
		}

		/// <summary>
		/// ウィンドウモード / フルスクリーンモードの切り替えを行う。
		/// </summary>
		/// <param name="isWindow">ウィンドウモードにする場合は true、それ以外は false。</param>
		/// <returns>正常に切り替えられた場合は true、それ以外は false。</returns>
		private bool _ChangeWindowMode(bool isWindow)
		{
			PresentParameters pp = this._SetParameters(isWindow);

			if (pp != null)										// デバイス設定を行う
			{													// 設定に成功したら
				this.IsWindow = isWindow;						// ウィンドウ/フルスクリーンモード切替
				this._ResetDevice(pp);							// デバイス再生成
				this.Owner.ControlBox = isWindow;				// フルスクリーンモードの場合はキャプションバーを非表示
				this.Owner.TopMost = !isWindow;					// ウィンドウモード時は最前面表示off
				this.Owner.ShowIcon = isWindow;					// ウィンドウモード時はアイコンを表示

				LogFileManager.WriteLine(
					Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
					CommonTools.GetResourceMessage(
						Properties.Resources.Msg_DrawMgr_ChangeIsWindow_Success,
						this._GetDisplayModeName(!isWindow),
						this._GetDisplayModeName(isWindow)
					)
				);

				return true;
			}
			else
			{
				LogFileManager.WriteLine(
					Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
					CommonTools.GetResourceMessage(
						Properties.Resources.Msg_DrawMgr_ChangeIsWindow_Failure,
						this._GetDisplayModeName(!isWindow),
						this._GetDisplayModeName(isWindow)
					)
				);

				return false;
			}
		}


		#region backup
		///// <summary>
		///// ウィンドウモード / フルスクリーンモードの切り替えを行う。
		///// </summary>
		///// <returns>正常に切り替えられた場合は true、それ以外は false。</returns>
		//private bool _ChangeWindowMode()
		//{
		//    PresentParameters pp = this._SetParameters(!this.IsWindow);

		//    if (pp != null)											// デバイス設定を行う
		//    {														// 設定に成功したら
		//        this.IsWindow = !this.IsWindow;						// ウィンドウ/フルスクリーンモード切替
		//        this._ResetDevice(pp);								// デバイス再生成
		//        this.Owner.ControlBox = this.IsWindow;				// フルスクリーンモードの場合はキャプションバーを非表示
		//        this.Owner.TopMost = !this.IsWindow;				// ウィンドウモード時は最前面表示off

		//        LogFileManager.WriteLine(
		//            Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
		//            CommonTools.GetResourceMessage(
		//                Properties.Resources.Msg_DrawMgr_ChangeIsWindow_Success,
		//                this._GetDisplayModeName(!this.IsWindow),
		//                this._GetDisplayModeName(this.IsWindow)
		//            )
		//        );

		//        return true;
		//    }
		//    else
		//    {
		//        LogFileManager.WriteLine(
		//            Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
		//            CommonTools.GetResourceMessage(
		//                Properties.Resources.Msg_DrawMgr_ChangeIsWindow_Failure,
		//                this._GetDisplayModeName(!this.IsWindow),
		//                this._GetDisplayModeName(this.IsWindow)
		//            )
		//        );

		//        return false;
		//    }
		//}
		#endregion

		/// <summary>
		/// ウィンドウ/フルスクリーンモード名を返す。
		/// </summary>
		/// <param name="isWindow">ウィンドウモードの場合は true、フルスクリーンモードの場合は false。</param>
		/// <returns>メッセージリソースで定められたウィンドウ/フルスクリーンモードの文字列。</returns>
		private string _GetDisplayModeName(bool isWindow)
		{
			return (isWindow) ? Properties.Resources.Msg_DrawMgr_WindowMode : Properties.Resources.Msg_DrawMgr_FullScreenMode;
		}

		#endregion


		#region 解放

		/// <summary>
		/// デバイスやテクスチャの解放を行う。
		/// </summary>
		private void _Dispose()
		{
			// 読み込んだ統合画像等のテクスチャファイルを全て破棄
			foreach (KeyValuePair<string, Texture> kvp in this.TextureTable)
			{
				this._SafeDispose(kvp.Value);
			}
			this.TextureTable.Clear();

			// スプライトやデバイス等の DirectX オブジェクトを破棄
			this._SafeDispose(this.Line);
			this._SafeDispose(this.Sprite);
			this._SafeDispose(this.Device);
		}

		#endregion


		#region シーン/スプライト

		/// <summary>
		/// デバイスのシーン描画を開始する。
		/// </summary>
		/// <param name="clearColor">描画前に画面をクリアする場合その色を指定した Color 型、クリアしない場合は Color.Empty。</param>
		private void _BeginScene(Color clearColor)
		{
			// クリア指定されている場合、描画開始前に白でクリアする
			if (clearColor != Color.Empty) this.Device.Clear(ClearFlags.Target, clearColor, 0, 0);

			this.Device.BeginScene();	// シーン描画開始
		}

		/// <summary>
		/// デバイスのシーン描画を終了する。
		/// </summary>
		/// <returns>正常に終了した場合は true、それ以外の場合は false。</returns>
		private bool _EndScene()
		{
			try
			{
				this.Device.EndScene();		// シーン描画終了
				this.Device.Present();		// プレゼンテーション(レンダーターゲットのバックバッファをフロントバッファへ)
			}
			catch (DeviceLostException)		// デバイスロストの例外が発生した場合
			{
				return false;				// falseを返す
			}

			return true;
		}


		/// <summary>
		/// スプライト描画を開始する。
		/// </summary>
		private void _BeginSprite()
		{
			this.Sprite.Begin(SpriteFlags.AlphaBlend);		// スプライト描画開始(レンダリングオプションに半透明処理を指定)
		}

		/// <summary>
		/// スプライト描画を終了する。
		/// </summary>
		private void _EndSprite()
		{
			this.Sprite.End();		// スプライト描画終了
		}

		#endregion


		#region NowLoading

		/// <summary>
		/// NowLoading ダイアログを描画する。
		/// </summary>
		private void _DrawNowLoadingDialog()
		{
			if (!Settings.System.Default.EnabledNowLoading) return;			// システム設定確認 表示しない設定の場合終了

			DrawManager.BeginScene(Color.Empty);							// NowLoading 中に (無理矢理) スプライト描画開始
			var drawStatus = new DrawStatusArgs();
			drawStatus.ShowDialog = MainWindow.DrawStatus.ShowDialog;		// 状態データを取得しダイアログの状況を引き継ぎ
			this.NowLoadingDialog.Draw(drawStatus);							// NowLoading ダイアログ描画
			DrawManager.EndScene();											// (無理矢理な) スプライト描画終了

			DebugTools.ConsolOutputMessage("DrawManager -DrawNowLoading", "Now Loading... ダイアログ表示");		// メッセージ表示のようなもの
		}

		/// <summary>
		/// NowLoading ダイアログを構築する。
		/// </summary>
		private void _CreateNowLoadingDialog()
		{
			this.NowLoadingDialog = new Dialog("NowLoading", DialogButtons.None, DialogIcons.Caution, "IMAGE_DIALOGTITLE_NOWLOADING", "IMAGE_DIALOGMSG_NOWLOADING");
		}


		#endregion


		#region フォントの生成/文字列の描画

		/// <summary>
		/// 【デバッグ用】フォントの生成を行う。
		/// </summary>
		[Conditional("DEBUG")]
		private void _InitializeFont()
		{
			FontDescription fd = new FontDescription();		// フォントデータの構造体を生成

			fd.Height = 16;									// 高さ20pt
			fd.FaceName = "ＭＳ ゴシック";					// MSゴシックを指定
			fd.Quality = FontQuality.Default;				// デバッグ用なので品質は最低限でいい
			//fd.Quality = FontQuality.AntiAliased;
			//fd.Height = 24;
			//fd.OutputPrecision = Precision.PsOnly;

			this.Font = new Microsoft.DirectX.Direct3D.Font(this.Device, fd);	// フォントの生成
		}


		/// <summary>
		/// 【デバッグ用】文字列を描画する。
		/// </summary>
		/// <param name="str">描画する文字列。</param>
		/// <param name="x">開始x座標。</param>
		/// <param name="y">開始y座標。</param>
		/// <param name="color">文字列の色。</param>
		[Conditional("DEBUG")]
		private void _DrawString(string str, int x, int y, Color color)
		{
			// ダミーの極小テクスチャを指定された座標でスプライト描画し，文字列は0,0に描画する
			// DrawTextにスプライトを指定すると, 最後にスプライト描画されたテクスチャの座標が基準になる仕様(?)のため
			Muphic.Manager.DrawManager.Instance._DrawTexture("IMAGE_DUMMY", new Point(x, y), 0, false, 1.0F, 0.0F, Color.FromArgb(0, 255, 255, 255));
			this.Font.DrawText(this.Sprite, str, 0, 0, color);
		}

		#endregion


		#region 統合画像ファイルの読込/削除
		
		/// <summary>
		/// 複数の統合画像を読み込む。
		/// </summary>
		/// <param name="fileNames">読み込む統合画像のパス群。</param>
		/// <returns>全ての読み込みに成功した場合は true、それ以外は false。</returns>
		private bool _LoadTextureFiles(string[] fileNames)
		{
			bool result = true;

			foreach (string fileName in fileNames)
			{
				if (!this._LoadTextureFile(fileName)) result = false;
			}

			return result;
		}

		/// <summary>
		/// 統合画像を読み込む。
		/// </summary>
		/// <param name="fileName">読み込む統合画像のパス。</param>
		/// <returns>読み込みに成功した場合は true、それ以外は false。</returns>
		private bool _LoadTextureFile(string fileName)
		{
			// テクスチャテーブル内の存在チェック
			if (this.TextureTable.ContainsKey(fileName))
			{
				// 既に同じファイル名の統合画像ファイルが読み込まれていた場合は終了
				//LogFileManager.WriteLine(
				//    Properties.Resources.Msg_DrawMgr_LoadTextureFile,
				//    CommonTools.GetResourceMessage(Properties.Resources.Msg_DrawMgr_LoadTextureFile_Failure_Reason_Loaded, fileName)
				//);

				return false;
			}

			// ファイルの存在チェック
			if (!ArchiveFileManager.Exists(fileName))
			{
				// 与えられたファイル名のテクスチャが存在しなければ終了
				//LogFileManager.WriteLineError(
				//    Properties.Resources.Msg_DrawMgr_LoadTextureFile,
				//    CommonTools.GetResourceMessage(
				//        Properties.Resources.Msg_DrawMgr_LoadTextureFile_Failure_Reason_NotExist, fileName)
				//);
				throw new Exception(
					CommonTools.GetResourceMessage(Properties.Resources.Msg_DrawMgr_LoadTextureFile_Failure_Reason_NotExist_, fileName)
				);
			}

			#region 読み込み時間計測開始
			Stopwatch readTimeWatch = new Stopwatch();
			readTimeWatch.Start();
			#endregion

			// テクスチャの登録
			this.TextureTable.Add(fileName, new Texture(this.Device, new MemoryStream(ArchiveFileManager.GetData(fileName)), Usage.None, Pool.Managed));

			LogFileManager.WriteLine(
				Properties.Resources.Msg_DrawMgr_LoadTextureFile,
				ConfigurationManager.Current.IsLoggingWithFullPath ? System.IO.Path.GetFullPath(fileName) : fileName
			);

			#region 読み込み時間計測終了・ログ出力
			readTimeWatch.Stop();
			LogFileManager.WriteLine(
				Muphic.Properties.Resources.Msg_DrawMgr_TextureLoadTime_Title,
				Tools.CommonTools.GetResourceMessage(Muphic.Properties.Resources.Msg_DrawMgr_TextureLoadTime, readTimeWatch.ElapsedMilliseconds.ToString())
			);
			#endregion

			return true;
		}


		/// <summary>
		/// 統合画像を削除する。
		/// </summary>
		/// <param name="fileName">削除する統合画像のパス。</param>
		/// <returns>削除に成功したら true、それ以外は false。</returns>
		private bool _UnloadTextureFile(string fileName)
		{
			// テクスチャテーブル内の存在チェック
			if (!this.TextureTable.ContainsKey(fileName))
			{
				// 既に同じファイル名の統合画像ファイルが読み込まれていた場合は終了
				LogFileManager.WriteLineError(Properties.Resources.Msg_DrawMgr_UnloadTextureFile, fileName);
				return false;
			}

			// ファイルの存在チェック
			if (!ArchiveFileManager.Exists(fileName))
			{
				// 与えられたファイル名のテクスチャが存在しなければ終了
				LogFileManager.WriteLineError(Properties.Resources.Msg_DrawMgr_UnloadTextureFile, fileName);
				return false;
			}

			// テクスチャの削除
			this._SafeDispose(this.TextureTable[fileName]);
			this.TextureTable.Remove(fileName);

			LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_UnloadTextureFile, fileName);
			return true;
		}


		/// <summary>
		/// 指定した統合画像が読み込まれているかどうかを確認する。
		/// </summary>
		/// <param name="fileName">確認する統合画像のパス。</param>
		/// <returns>読み込まれていた場合は true、それ以外は false。</returns>
		private bool _ExistsTextureFile(string fileName)
		{
			return this.TextureTable.ContainsKey(fileName);
		}



		/// <summary>
		/// 統合画像からのテクスチャ生成を非同期で実行する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _TextureLoad(object sender, DoWorkEventArgs e)
		{
			List<Texture> textures = new List<Texture>();		// 生成されたテクスチャのリスト
			List<string> messages = new List<string>();			// メッセージリスト

			foreach (string fileName in (List<string>)e.Argument)
			{
				if (this.TextureTable.ContainsKey(fileName))	// テクスチャテーブル内の存在チェック
				{
					messages.Add("スキップ - " + fileName + " (読込済み)");
					continue;
				}

				if (!ArchiveFileManager.Exists(fileName))		// アーカイブ内の存在チェック
				{
					messages.Add("スキップ - " + fileName + " (アーカイブ内に存在しない)");
					continue;
				}

				textures.Add(new Texture(this.Device, new MemoryStream(ArchiveFileManager.GetData(fileName)), Usage.None, Pool.Managed));
				messages.Add("成功" + fileName);
			}

			e.Result = new object[] { textures, messages };
		}

		/// <summary>
		/// 統合画像からのテクスチャ生成が完了すると実行され、後処理を行う。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _TextureLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		#endregion


		#region テクスチャの登録/削除

		/// <summary>
		/// テクスチャの登録を開始する。
		/// </summary>
		/// <param name="isNowLoading">NowLoading 画面を表示する場合は true、それ以外は false。</param>
		/// <returns>登録テーブルの index。</returns>
		private int _BeginRegistTexture(bool isNowLoading)
		{
			// 既存の登録テーブル内で空になっている番号が無いかを探る
			for (this.RegistNum = 0; this.RegistNum < RegistTable.Count; this.RegistNum++)
			{
				// 空になっている番号を発見したらそれが使用する登録番号
				if (this.RegistTable[this.RegistNum] == null) break;
			}

			// 空になっている番号がなければ(登録番号が要素数と同じになっていたら)、新しいリストを作成
			if (this.RegistNum == this.RegistTable.Count) this.RegistTable.Add(new List<string>());

			// 既存のリストで開いている番号を発見したら、その番号を使う
			else this.RegistTable[this.RegistNum] = new List<string>();

			// NowLoading指定されていれば描画
			if (isNowLoading) this._DrawNowLoadingDialog();

			// デバッグ用メッセージ
			DebugTools.ConsolOutputMessage("DrawManager -BeginRegistTexture", "クラステクスチャ登録 番号:" + this.RegistNum, true);

			// 登録番号(使用する登録テーブルのインデックス)
			return this.RegistNum;
		}


		/// <summary>
		/// テクスチャの登録を終了する。
		/// </summary>
		private void _EndRegistTexture()
		{
			this.RegistNum = -1;		// 登録中フラグを降ろす
		}


		/// <summary>
		/// クラスで使用するテクスチャとその座標の登録を行う。
		/// </summary>
		/// <param name="keyName">ハッシュに登録するキー(クラス名)。</param>
		/// <param name="p">テクスチャを表示する座標 (位置が変わるテクスチャの場合は適当でいい)。</param>
		/// <param name="textureNames">使用するテクスチャの名前。</param>
		private void _RegistTexture(String keyName, Point p, String[] textureNames)
		{
			Rectangle[] rs = new Rectangle[textureNames.Length];

			for (int i = 0; i < textureNames.Length; i++)
			{
				rs[i] = new Rectangle(p, TextureFileManager.GetRectangle(textureNames[i]).Size);
			}

			// TextureNameManagerにテクスチャ名を登録
			Muphic.Manager.TextureNameManager.Regist(keyName, textureNames);

			// PointManagerにテクスチャの座標を登録
			Muphic.Manager.RectangleManager.Regist(keyName, rs);

			// 登録中フラグが立っていれば登録テーブルに登録名を追加
			if (this.RegistNum >= 0) this.RegistTable[this.RegistNum].Add(keyName);
		}


		/// <summary>
		/// クラスで使用するテクスチャとその座標の削除を行う。
		/// </summary>
		/// <param name="index">登録テーブルのインデックス番号。</param>
		private void _DeleteTexture(int index)
		{
			foreach (string keyName in this.RegistTable[index])
			{
				this._DeleteTexture(keyName);
			}

			this.RegistTable[index] = null;
			DebugTools.ConsolOutputMessage("DrawManager -DeleteTexture", "登録クラステクスチャ一括削除 番号:" + index, true);
		}

		/// <summary>
		/// クラスで使用するテクスチャとその座標の削除を行う。
		/// </summary>
		/// <param name="keyName">削除するキー(クラス名)。</param>
		private void _DeleteTexture(string keyName)
		{
			if (keyName == null) return;

			TextureNameManager.Delete(keyName);
			RectangleManager.Delete(keyName);
		}

		#endregion


		#region テクスチャ描画

		/// <summary>
		/// スプライトの描画を行う (描画座標自動取得)。
		/// </summary>
		/// <param name="className">描画するテクスチャのキー (クラス名等)。</param>
		/// <param name="state">そのクラスの状態 (状態によって描画するテクスチャが変わる)。</param>
		/// <param name="isCenter">location が左上でなくテクスチャ中央の座標である場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率 (拡大・縮小を行わない場合は 1.0)。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ (テクスチャの色をそのまま使用する場合は Color.White)。</param>
		private void _DrawTexture(string className, int state, bool isCenter, float scaling, Color filter)
		{
			this._DrawTexture(className, state, isCenter, scaling, false, filter);
		}

		/// <summary>
		/// スプライトの描画を行う (描画座標自動取得)。
		/// </summary>
		/// <param name="className">描画するテクスチャのキー (クラス名等)。</param>
		/// <param name="state">そのクラスの状態 (状態によって描画するテクスチャが変わる)。</param>
		/// <param name="isCenter">location が左上でなくテクスチャ中央の座標である場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率 (拡大・縮小を行わない場合は 1.0)。</param>
		/// <param name="isLotation90">テクスチャを 90 度回転させて描画する場合は true、それ以外は false。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ (テクスチャの色をそのまま使用する場合は Color.White)。</param>
		private void _DrawTexture(string className, int state, bool isCenter, float scaling, bool isLotation90, Color filter)
		{
			// クラス名から描画する際の左上座標を得る
			Rectangle r = Muphic.Manager.RectangleManager.Get(className, state);

			// 存在チェック
			if (r.X == -1)
			{
				// キーが存在しない場合、その旨をコンソールに出力して終了
				Muphic.Tools.DebugTools.ConsolOutputError("DrawManager -DrawTexture", "テクスチャ描画失敗 (キー: " + className + " の座標は登録されていない) ");
				return;
			}

			// キーが存在し描画する際の左上座標が判明したら、同メソッド(オーバーロード)を呼ぶ
			this._DrawTexture(className, r.Location, state, isCenter, scaling, isLotation90 ? (float)(Math.PI / 2) : 0.0F, filter);
		}

		/// <summary>
		/// スプライトの描画を行う (描画座標指定)。
		/// </summary>
		/// <param name="className">描画するテクスチャのキー (クラス名等), またはテクスチャ名。</param>
		/// <param name="location">描画する座標。</param>
		/// <param name="state">そのクラスの状態 (状態によって描画するテクスチャが変わる)。</param>
		/// <param name="isCenter">location が左上でなくテクスチャ中央の座標である場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率 (拡大・縮小を行わない場合は 1.0)。</param>
		/// <param name="isLotation90">テクスチャを 90 度回転させて描画する場合は true、それ以外は false。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ (テクスチャの色をそのまま使用する場合は Color.White)。</param>
		private void _DrawTexture(string className, Point location, int state, bool isCenter, float scaling, bool isLotation90, Color filter)
		{
			this._DrawTexture(className, location, state, isCenter, scaling, isLotation90 ? (float)(Math.PI / 2) : 0.0F, filter);
		}

		/// <summary>
		/// スプライトの描画を行う (描画座標指定)。
		/// </summary>
		/// <param name="className">描画するテクスチャのキー (クラス名等), またはテクスチャ名。</param>
		/// <param name="location">描画する座標。</param>
		/// <param name="state">そのクラスの状態 (状態によって描画するテクスチャが変わる)。</param>
		/// <param name="isCenter">location が左上でなくテクスチャ中央の座標である場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率 (拡大・縮小を行わない場合は 1.0)。</param>
		/// <param name="lotationAngle">テクスチャを回転させて描画する場合はその回転角 (ラジアン右回転)、回転させない場合は 0。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ (テクスチャの色をそのまま使用する場合は Color.White)。</param>
		private void _DrawTexture(string className, Point location, int state, bool isCenter, float scaling, float lotationAngle, Color filter)
		{
			string TextureName = Muphic.Manager.TextureNameManager.Get(className, state);		// クラス名からテクスチャ名を得る

			if (string.IsNullOrEmpty(TextureName))							// クラス名からテクスチャ名が得られたかをチェックする
			{
				if (Muphic.Manager.TextureFileManager.Exist(className))		// 得られなかった場合,className自体がテクスチャ名になっている可能性がある
				{
					TextureName = className;								// TextureFileManagerに登録されていれば,そのままテクスチャ名とする
				}
				else
				{
					Muphic.Tools.DebugTools.ConsolOutputError("DrawManager -DrawTexture", "テクスチャ描画失敗 (キー: " + className + " は未登録) ");
					return;		// キーが存在しない場合、その旨をコンソールに出力して終了
				}
			}

			string fileName = Muphic.Manager.TextureFileManager.GetFilePath(TextureName);			// テクスチャ名から描画に使用するテクスチャファイル名を得る
			Rectangle srcRectangle = Muphic.Manager.TextureFileManager.GetRectangle(TextureName);	// テクスチャ名から描画に使用するテクスチャ内矩形領域を得る

			if (String.IsNullOrEmpty(fileName))			// テクスチャファイルの存在チェック
			{
				Muphic.Tools.DebugTools.ConsolOutputError("DrawManager -DrawTexture", "テクスチャ描画失敗 (テクスチャ名: " + TextureName + " は未登録");
				return;		// テクスチャファイルが存在しない場合、その旨をコンソールに出力して終了
			}

			if (isCenter)	// 中央指定かチェック
			{
				// 与えられた描画座標が中央のものであった場合、テクスチャサイズを用いて左上座標を算出
				location = Muphic.Tools.CommonTools.CenterToOnreft(location, srcRectangle.Size);
			}

			// == スプライト描画 ==
			// 第１引数 : 描画するテクスチャの指定    ファイルパスを元にテクスチャテーブル内の何番目の統合画像を使用するのかを決定
			// 第２引数 : テクスチャの転送矩形の指定  統合画像内の描画する矩形領域を指定
			// 第３引数 : 描画先の範囲の指定          拡大・縮小率が指定されている場合はそれに応じ矩形サイズを変更 / 指定されていない場合は転送矩形サイズを指定
			// 第４引数 : テクスチャの回転中心の指定  テクスチャを回転して描画する場合、テクスチャの左下を回転の中心座標として指定 / 指定されていない場合は左上を指定
			// 第５引数 : テクスチャの回転角の指定    テクスチャを回転して描画する場合、その回転角を指定
			// 第６引数 : 描画先の座標の指定          転送矩形の描画位置の左上座標を指定
			// 第７引数 : 色フィルタの指定      
			this.Sprite.Draw2D(
				this.TextureTable[fileName],
				srcRectangle,
				scaling == 1.0 ? srcRectangle.Size : new SizeF(srcRectangle.Width * scaling, srcRectangle.Height * scaling),
				lotationAngle == 0.0F ? new PointF(0, 0) : new PointF(0, srcRectangle.Height),
				lotationAngle,
				location,
				filter
			);
		}

		#endregion


		#region ライン描画

		/// <summary>
		/// ラインの初期化を行う。
		/// </summary>
		/// <param name="devide">デバイス。</param>
		private void _InitializeLine(Device devide)
		{
			this.Line = new Line(devide);

			this.Line.Width = 1.0f;				// 線の太さを指定
			this.Line.Antialias = false;		// アンチエイリアスの指定 (水平直角の線のみなので必要ない) 
			this.Line.Pattern = -1;				// 点描の指定 (１にすると点描のような描画になる) 
			this.Line.PatternScale = 1.0f;		// 点描の間隔を指定 (ｲﾗﾈ) 
		}

		/// <summary>
		/// 指定された矩形のラインを描画する。
		/// </summary>
		/// <param name="lineArea">ラインを描画する矩形。</param>
		/// <param name="lineColor">ラインの色。</param>
		/// <param name="width">ラインの太さ。</param>
		private void _DrawLine(Rectangle lineArea, Color lineColor, float width)
		{
			this.Line.Width = width;	// 指定されたラインの幅に設定

			this.Sprite.End();			// スプライト描画を一旦終了
			this.Line.Begin();			// ライン描画開始

			// 矩形の頂点 (四隅の座標) を設定
			Vector2[] positions = new Vector2[5];
			positions[0] = new Vector2(lineArea.Left, lineArea.Top);
			positions[1] = new Vector2(lineArea.Left, lineArea.Bottom);
			positions[2] = new Vector2(lineArea.Right, lineArea.Bottom);
			positions[3] = new Vector2(lineArea.Right, lineArea.Top);
			positions[4] = new Vector2(lineArea.Left, lineArea.Top);

			this.Line.Draw(positions, lineColor);			// ライン描画

			this.Line.End();								// ライン描画終了
			this.Sprite.Begin(SpriteFlags.AlphaBlend);		// 再びスプライト描画開始
		}

		#endregion


		#region その他

		/// <summary>
		/// 設定ファイルから値を得る。
		/// </summary>
		/// <typeparam name="Type">取得したい設定値の型</typeparam>
		/// <param name="key">設定名。</param>
		/// <returns>設定値。</returns>
		private Type GetSettings<Type>(string key)
		{
			return CommonTools.GetSettings<Type>(key);
		}

		#endregion


		#region 外部から呼び出されるメソッド群

		#region Init/Dispose

		/// <summary>
		/// 描画管理クラスの静的インスタンス生成及び使用する描画デバイス等の初期化を行う。
		/// インスタンス生成後に１度しか実行できない点に注意。
		/// </summary>
		public static bool Initialize(MainWindow mainScreen)
		{
			return Muphic.Manager.DrawManager.Instance._Initialize(mainScreen);
		}

		/// <summary>
		/// 描画管理クラスで使用されているデバイスやテクスチャ等のアンマネージリソースを解放する。
		/// </summary>
		public static void Dispose()
		{
			Muphic.Manager.DrawManager.Instance._Dispose();
		}

		#endregion

		#region Device/Scene

		/// <summary>
		/// デバイスの状態のチェックを行う。ロストしていた場合はデバイスの再生成を試みる。
		/// </summary>
		/// <returns>正常状態またはデバイスを再生成した場合は true、それ以外 (デバイスロスト時) は false。</returns>
		public static bool CheckDevice()
		{
			return Muphic.Manager.DrawManager.Instance._CheckDevice();
		}

		/// <summary>
		/// 構成設定でフルスクリーンモードが設定されていた場合、フルスクリーンモードでデバイスを再生成する。起動フェーズ終了時に使用する。
		/// </summary>
		/// <returns>正常に再生成された場合、またはフルスクリーンモードに設定されていなかった場合は true、それ以外は false。</returns>
		public static bool SetFullScreenMode()
		{
			return Muphic.Manager.DrawManager.Instance._SetFullScreenMode();
		}

		/// <summary>
		/// ウィンドウ/フルスクリーンモードの切り替えを行う。
		/// </summary>
		/// <returns>正常に切り替えられた場合は true、それ以外は false。</returns>
		public static bool ChangeWindowMode()
		{
			return Muphic.Manager.DrawManager.Instance._ChangeWindowMode();
		}


		/// <summary>
		/// 画面を白でクリアし、シーン描画を開始する (デバイスのシーン描画開始とスプライトの描画開始) 。
		/// </summary>
		public static void BeginScene()
		{
			Muphic.Manager.DrawManager.BeginScene(Color.White);
		}

		/// <summary>
		/// シーン描画を開始する (デバイスのシーン描画開始とスプライトの描画開始) 。
		/// </summary>
		/// <param name="clearColor">描画前に画面をクリアする場合その色を指定した Color 型、クリアしない場合は Color.Empty。</param>
		public static void BeginScene(Color clearColor)
		{
			Muphic.Manager.DrawManager.Instance._BeginScene(clearColor);
			Muphic.Manager.DrawManager.Instance._BeginSprite();
		}

		/// <summary>
		/// シーン描画を終了する (スプライトの描画終了とデバイスのシーン描画終了) 。
		/// </summary>
		public static void EndScene()
		{
			Muphic.Manager.DrawManager.Instance._EndSprite();
			Muphic.Manager.DrawManager.Instance._EndScene();
		}

		/// <summary>
		/// NowLoading ダイアログを描画する。
		/// </summary>
		public static void DrawNowLoading()
		{
			Muphic.Manager.DrawManager.Instance._DrawNowLoadingDialog();
		}

		/// <summary>
		/// NowLoading ダイアログを生成する。
		/// </summary>
		public static void CreateNowLoaing()
		{
			Muphic.Manager.DrawManager.Instance._CreateNowLoadingDialog();
		}

		#endregion

		#region Font

		/// <summary>
		/// 【デバッグ用】文字列の描画を行う。
		/// </summary>
		/// <param name="str">描画する文字列。</param>
		/// <param name="xLocation">開始x座標。</param>
		/// <param name="yLocation">開始y座標。</param>
		/// <param name="color">文字列の色。</param>
		public static void DrawString(string str, int xLocation, int yLocation, Color color)
		{
			Muphic.Manager.DrawManager.Instance._DrawString(str, xLocation, yLocation, color);
		}

		/// <summary>
		/// 【デバッグ用】文字列の描画を行う(デバッグ時のみ)。
		/// </summary>
		/// <param name="str">描画する文字列。</param>
		/// <param name="xLocation">開始x座標。</param>
		/// <param name="yLocation">開始y座標。</param>
		public static void DrawString(string str, int xLocation, int yLocation)
		{
			Muphic.Manager.DrawManager.Instance._DrawString(str, xLocation, yLocation, Color.Black);
		}

		#endregion

		#region Load

		/// <summary>
		/// 複数の統合画像を読み込む。
		/// </summary>
		/// <param name="fileNames">統合画像のファイルパス。</param>
		/// <returns>全ての読み込みに成功した場合は true、それ以外は false。</returns>
		public static bool LoadTextureFiles(string[] fileNames)
		{
			return Muphic.Manager.DrawManager.Instance._LoadTextureFiles(fileNames);
		}

		/// <summary>
		/// 統合画像を読み込む。
		/// </summary>
		/// <param name="fileName">統合画像のファイルパス。</param>
		/// <returns>読み込みに成功成功した場合は true、それ以外は false。</returns>
		public static bool LoadTextureFile(string fileName)
		{
			return Muphic.Manager.DrawManager.Instance._LoadTextureFile(fileName);
		}

		/// <summary>
		/// 統合画像を削除する。
		/// </summary>
		/// <param name="fileName">統合画像のファイルパス。</param>
		/// <returns>削除に成功成功した場合は true、それ以外は false。</returns>
		public static bool UnLoadTextureFile(string fileName)
		{
			return Muphic.Manager.DrawManager.Instance._UnloadTextureFile(fileName);
		}

		/// <summary>
		/// 指定した統合画像が読み込まれているかどうかを確認する。
		/// </summary>
		/// <param name="fileName">確認する統合画像のパス。</param>
		/// <returns>読み込まれていた場合は treu、それ以外は false。</returns>
		public static bool ExistsTextureFile(string fileName)
		{
			return Muphic.Manager.DrawManager.Instance._ExistsTextureFile(fileName);
		}

		#endregion

		#region Regist

		/// <summary>
		/// 使用するテクスチャの登録を開始する。必ず EndRegist メソッドで終了させること。
		/// </summary>
		/// <param name="isNowLoading">登録中に NowLoading 画面を表示する場合は true、それ以外は false。</param>
		/// <returns>登録番号。解放する際はこの番号を指定して Delete メソッドを呼び出すと、登録したクラス名等を一括削除できる。</returns>
		public static int BeginRegist(bool isNowLoading)
		{
			return Muphic.Manager.DrawManager.Instance._BeginRegistTexture(isNowLoading);
		}

		/// <summary>
		/// 使用するテクスチャの登録を終了する。
		/// </summary>
		public static void EndRegist()
		{
			Muphic.Manager.DrawManager.Instance._EndRegistTexture();
		}


		/// <summary>
		/// 使用するテクスチャとその座標を登録する。
		/// </summary>
		/// <param name="keyName">登録するキー。</param>
		/// <param name="xLocation">テクスチャを表示する際の左上x座標。</param>
		/// <param name="yLocation">テクスチャを表示する際の左上y座標。</param>
		/// <param name="textureNames">登録するテクスチャ名。</param>
		public static void Regist(string keyName, int xLocation, int yLocation, params string[] textureNames)
		{
			Muphic.Manager.DrawManager.Instance._RegistTexture(keyName, new Point(xLocation, yLocation), textureNames);
		}

		/// <summary>
		/// 使用するテクスチャとその座標を登録する。
		/// </summary>
		/// <param name="className">登録するキー(クラス名)。</param>
		/// <param name="textureLocation">テクスチャを表示する際の座標。</param>
		/// <param name="textureNames">登録するテクスチャ名。</param>
		public static void Regist(string className, Point textureLocation, params string[] textureNames)
		{
			Muphic.Manager.DrawManager.Instance._RegistTexture(className, textureLocation, textureNames);
		}


		/// <summary>
		/// クラスで使用するテクスチャとその座標の削除を行う。
		/// </summary>
		/// <param name="index">BeginRegist メソッド戻り値の登録番号。</param>
		public static void Delete(int index)
		{
			Muphic.Manager.DrawManager.Instance._DeleteTexture(index);
		}

		/// <summary>
		/// クラスで使用するテクスチャとその座標の削除を行う。
		/// </summary>
		/// <param name="keyName">削除するキー(クラス名)。</param>
		public static void Delete(string keyName)
		{
			Muphic.Manager.DrawManager.Instance._DeleteTexture(keyName);
		}

		#endregion

		#region Draw

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標は Manager 側で検索 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		public static void Draw(String className)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, 0, false, 1.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標は Manager 側で検索 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Draw(String className, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, 0, false, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標は Manager 側で検索 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		public static void Draw(String className, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, false, 1.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標は Manager 側で検索 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Draw(String className, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, false, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標は Manager 側で検索 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="isCenter">登録された配置座標が中央座標である場合は true、左上座標である場合は false。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Draw(String className, bool isCenter, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, isCenter, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標は Manager 側で検索 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		/// <param name="isLotation90">テクスチャを 90 度右回転させて描画する場合は true、それ以外は false。</param>
		public static void Draw(String className, int state, byte alpha, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, false, 1.0F, isLotation90, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		public static void Draw(String keyName, int xLocation, int yLocation)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Draw(String keyName, int xLocation, int yLocation, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標指定 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		public static void Draw(String className, int xLocation, int yLocation, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過無し / 拡大・縮小無し / 90 度回転指定 / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="isLotation90">テクスチャを 90 度右回転させて描画する場合は true、それ以外は false。</param>
		public static void Draw(String className, int xLocation, int yLocation, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, false, 1.0F, true, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過無し / 拡大・縮小率指定 / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void Draw(String className, int xLocation, int yLocation, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, false, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 拡大・縮小無し / 回転無し / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Draw(String className, int xLocation, int yLocation, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 拡大・縮小率指定 / 回転無し / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Draw(String className, int xLocation, int yLocation, float scaling, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, false, scaling, 0.0F, filter);
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標指定 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Draw(String className, int xLocation, int yLocation, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 拡大・縮小率指定 / 回転角指定 / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="lotationAngle">テクスチャの回転角 (ラジアン右回転)。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Draw(String className, int xLocation, int yLocation, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, false, scaling, lotationAngle, filter);
		}


		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		public static void Draw(String keyName, Point location)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過無し / 拡大・縮小率指定 / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void Draw(String keyName, Point location, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, false, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Draw(String keyName, Point location, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標指定 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		public static void Draw(String className, Point location, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過無し / 拡大・縮小無し / 90 度回転指定 / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="isLotation90">テクスチャを 90 度右回転させて描画する場合は true、それ以外は false。</param>
		public static void Draw(String className, Point location, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, false, 1.0F, true, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 拡大・縮小無し / 回転無し / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Draw(String className, Point location, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, false, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 拡大・縮小率指定 / 回転無し / フィルタ指定)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Draw(String keyName, Point location, float scaling, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, false, scaling, 0.0F, filter);
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標指定 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Draw(String className, Point location, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 透過度指定 / 拡大・縮小率指定 / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void Draw(String className, Point location, byte alpha, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, false, scaling, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 / 拡大・縮小率指定 / 回転角指定 / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="lotationAngle">テクスチャの回転角 (ラジアン右回転)。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Draw(String className, Point location, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, false, scaling, lotationAngle, filter);
		}

		#endregion

		#region DrawCenter

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標は Manager 側で検索 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		public static void DrawCenter(String className)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, 0, true, 1.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標は Manager 側で検索 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void DrawCenter(String className, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, 0, true, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標は Manager 側で検索 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		public static void DrawCenter(String className, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, true, 1.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標は Manager 側で検索 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void DrawCenter(String className, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, true, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標は Manager 側で検索 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		/// <param name="isLotation90">テクスチャを 90 度右回転させて描画する場合は true、それ以外は false。</param>
		public static void DrawCenter(String className, int state, byte alpha, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, true, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		public static void DrawCenter(String keyName, int xLocation, int yLocation)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void DrawCenter(String keyName, int xLocation, int yLocation, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, true, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 90 度回転指定 / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="isLotation90">テクスチャを 90 度右回転させて描画する場合は true、それ以外は false。</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, true, 1.0F, true, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小率指定 / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, true, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 拡大・縮小無し / 回転無し / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 拡大・縮小率指定 / 回転無し / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, float scaling, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, true, scaling, 0.0F, filter);
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 拡大・縮小率指定 / 回転角指定 / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="xLocation">描画するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">描画するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="lotationAngle">テクスチャの回転角 (ラジアン右回転)。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, true, scaling, lotationAngle, filter);
		}


		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		public static void DrawCenter(String keyName, Point location)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, true, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小率指定 / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void DrawCenter(String keyName, Point location, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, true, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void DrawCenter(String keyName, Point location, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		public static void DrawCenter(String className, Point location, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, true, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 90 度回転指定 / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="isLotation90">テクスチャを 90 度右回転させて描画する場合は true、それ以外は false。</param>
		public static void DrawCenter(String className, Point location, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, true, 1.0F, true, Color.White);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 拡大・縮小無し / 回転無し / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void DrawCenter(String className, Point location, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, true, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 拡大・縮小率指定 / 回転無し / フィルタ指定)
		/// </summary>
		/// <param name="keyName">描画するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void DrawCenter(String keyName, Point location, float scaling, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, true, scaling, 0.0F, filter);
		}

		/// <summary>
		/// テクスチャを描画する (state 指定 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void DrawCenter(String className, Point location, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小率指定 / 回転無し / フィルタ無し)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void DrawCenter(String className, Point location, byte alpha, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, true, scaling, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// テクスチャを描画する (state = 0 / 座標指定 (中心座標) / 拡大・縮小率指定 / 回転角指定 / フィルタ指定)
		/// </summary>
		/// <param name="className">描画するキー (クラス名)。</param>
		/// <param name="location">描画するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより描画するテクスチャが変わる)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="lotationAngle">テクスチャの回転角 (ラジアン右回転)。</param>
		/// <param name="filter">スプライト描画の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void DrawCenter(String className, Point location, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, true, scaling, lotationAngle, filter);
		}

		#endregion

		#region Line

		/// <summary>
		/// 指定された矩形を線で描画する。
		/// </summary>
		/// <param name="line">描画する線の矩形。</param>
		public static void DrawLine(Rectangle line)
		{
			Muphic.Manager.DrawManager.Instance._DrawLine(line, Color.Red, 2.0F);
		}

		/// <summary>
		/// 指定された矩形で線を描画する。
		/// </summary>
		/// <param name="line">描画する線の矩形。</param>
		/// <param name="lineColor">描画する線の色。</param>
		/// <param name="lineWidth">描画する線の太さ。</param>
		public static void DrawLine(Rectangle line, Color lineColor, float lineWidth)
		{
			Muphic.Manager.DrawManager.Instance._DrawLine(line, lineColor, lineWidth);
		}

		#endregion

		#endregion

	}
}
