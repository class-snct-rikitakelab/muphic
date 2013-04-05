using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace Muphic.Manager
{
	/// <summary>
	/// キーボード入力管理クラス (シングルトン・継承付加)
	/// <para>DirectInput を利用したキーボード入力の管理を行い、入力のための静的メソッドを提供する。</para>
	/// </summary>
	public sealed class KeyboardInputManager : Manager
	{

		/// <summary>
		/// キーボード入力管理クラスの静的インスタンス (シングルトンパターン)。
		/// </summary>
		private static KeyboardInputManager __instance = new KeyboardInputManager();

		/// <summary>
		/// キーボード入力管理クラスの静的インスタンス (シングルトンパターン)。
		/// </summary>
		private static KeyboardInputManager Instance
		{
			get { return KeyboardInputManager.__instance; }
		}


		#region プロパティ


		#region DirectInput デバイス

		/// <summary>
		/// DirectInput キーボード デバイス オブジェクト。
		/// </summary>
		private Device _Device { get; set; }

		#endregion


		#region 特殊キーフラグ

		#region エンターキー

		/// <summary>
		/// 通常のエンターキーが押されているかどうかを示す値を取得または設定する。
		/// </summary>
		private bool _IsNormalEnter { get; set; }

		/// <summary>
		/// テンキーのエンターキーが押されているかどうかを示す値を取得または設定する。
		/// </summary>
		private bool _IsNumPadEnter { get; set; }

		/// <summary>
		/// エンターキーが押されているかどうかを示す値を取得する。
		/// </summary>
		private bool _IsEnter
		{
			get { return this._IsNormalEnter || this._IsNumPadEnter; }
		}

		/// <summary>
		/// エンターキーが押されているかどうかを示す値を取得する。
		/// </summary>
		public static bool IsEnter
		{
			get { return Muphic.Manager.KeyboardInputManager.Instance._IsEnter; }
		}

		#endregion

		#region コントロールキー

		/// <summary>
		/// 左コントロールキーが押されているかどうかを示す値を取得または設定する。
		/// </summary>
		private bool _IsLeftControl { get; set; }

		/// <summary>
		/// 右コントロールキーが押されているかどうかを示す値を取得または設定する。
		/// </summary>
		private bool _IsRightControl { get; set; }

		/// <summary>
		/// 左右いずれかのコントロールキーが押されているかどうかを示す値を取得する。
		/// </summary>
		private bool _IsControl
		{
			get { return this._IsLeftControl || this._IsRightControl; }
		}

		/// <summary>
		/// 左右いずれかのコントロールキーが押されているかどうかを示す値を取得する。
		/// </summary>
		public static bool IsControl
		{
			get { return Muphic.Manager.KeyboardInputManager.Instance._IsControl; }
		}

		#endregion

		#region オルタネートキー

		/// <summary>
		/// 左 Alt キーが押されているかどうかを示す値を取得または設定する。
		/// </summary>
		private bool _IsLeftAlternate { get; set; }

		/// <summary>
		/// 右 Alt キーが押されているかどうかを示す値を取得または設定する。
		/// </summary>
		private bool _IsRightAlternate { get; set; }

		/// <summary>
		/// 左右いずれかの Alt キーが押されているかどうかを示す値を取得する。
		/// </summary>
		private bool _IsAlternate
		{
			get { return this._IsLeftAlternate || this._IsRightAlternate; }
		}

		/// <summary>
		/// 左右いずれかの Alt キーが押されているかどうかを示す値を取得する。
		/// </summary>
		public static bool IsAlternate
		{
			get { return Muphic.Manager.KeyboardInputManager.Instance._IsAlternate; }
		}

		#endregion

		#region シフトキー

		/// <summary>
		/// 左シフトキーが押されているかどうかを示す値を取得または設定する。
		/// </summary>
		private bool _IsLeftShift { get; set; }

		/// <summary>
		/// 右シフトキーが押されているかどうかを示す値を取得または設定する。
		/// </summary>
		private bool _IsRightShift { get; set; }

		/// <summary>
		/// 左右いずれかのシフトキーが押されているかどうかを示す値を取得する。
		/// </summary>
		private bool _IsShift
		{
			get { return this._IsLeftShift || this._IsRightShift; }
		}

		/// <summary>
		/// 左右いずれかのシフトキーが押されているかどうかを示す値を取得する。
		/// </summary>
		public static bool IsShift
		{
			get { return Muphic.Manager.KeyboardInputManager.Instance._IsShift; }
		}

		#endregion

		#endregion


		#region Shift キー制限

		/// <summary>
		/// 児童モードによるプロテクトが有効かどうかを示す値を取得する。
		/// 通常、このプロパティ値が true ならば、Esc キーによるプログラム終了等の一部のキー操作が無効化され、新規作成等の各画面の一部の機能は Shift キーを押さないと反応しなくなる。
		/// </summary>
		private bool _IsProtection
		{
			get
			{
				return																	// プロテクトが有効になる条件
					MainWindow.MuphicOperationMode == MuphicOperationMode.StudentMode	// 条件1: 児童用モードである
					&& ConfigurationManager.Locked.IsProtection							// 条件2: 動作設定でプロテクトが有効化されている
					&& !this._IsShift;													// 条件3: Shift キーが押されていない
			}
		}

		/// <summary>
		/// 児童モードによるプロテクトが有効かどうかを示す値を取得する。
		/// 通常、このプロパティ値が true ならば、Esc キーによるプログラム終了等の一部のキー操作が無効化され、新規作成等の各画面の一部の機能は Shift キーを押さないと反応しなくなる。
		/// </summary>
		public static bool IsProtection
		{
			get { return Muphic.Manager.KeyboardInputManager.Instance._IsProtection; }
		}

		#endregion


		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// キーボード入力管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private KeyboardInputManager()
		{
			// 全ての特殊キーフラグを下ろしておく
			this._IsNormalEnter = this._IsNumPadEnter = false;
			this._IsLeftAlternate = this._IsRightAlternate = false;
			this._IsLeftControl = this._IsRightControl = false;
			this._IsLeftShift = this._IsRightShift = false;
		}


		/// <summary>
		/// キーボード入力管理クラスの静的インスタンス生成及び使用する入力デバイス等の初期化を行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <param name="mainWindow">muphic メインウィンドウ。</param>
		/// <returns>正常に初期化が行われた場合は true、それ以外は false。</returns>
		private bool _Initialize(MainWindow mainWindow)
		{
			if (this._IsInitialized) return false;

			if (!this._InitializeKeyboardDevice(mainWindow)) return false;	// デバイスの初期化 失敗したらプログラム終了

			Tools.DebugTools.ConsolOutputMessage("KeyboardInputManager -Initialize", "キーボード入力管理クラス生成完了", true);

			return this._IsInitialized = true;
		}


		/// <summary>
		/// キーボード デバイスの初期化を行う。
		/// </summary>
		/// <param name="mainWindow">muphic メインウィンドウ。</param>
		/// <returns>初期化に成功した場合は true、それ以外は false。</returns>
		private bool _InitializeKeyboardDevice(MainWindow mainWindow)
		{
			try
			{
				this._Device = new Device(SystemGuid.Keyboard);	// デバイス生成
				this._Device.SetCooperativeLevel(				// 協調レベル設定
					mainWindow, 
					CooperativeLevelFlags.NoWindowsKey |	// Windows キー無効
					CooperativeLevelFlags.Foreground |		// アクティブ時のみデバイスの入力を受け取る
					CooperativeLevelFlags.NonExclusive		// デバイスを他のアプリケーションと共有する
				);											// (フルスクリーンの時は他のアプリケーションと共有しない方が良い？)
				LogFileManager.WriteLine(
					Properties.Resources.Msg_InputMgr_CreateKeyboardDevice,
					Properties.Resources.Msg_InputMgr_CreateKeyboardDevice_Success
				);

				this._Device.Properties.BufferSize = 8;		// バッファサイズを指定
			}
			catch (DirectXException ex)
			{
				// デバイス生成に失敗した場合、ログ表示
				LogFileManager.WriteLineError(
					Properties.Resources.Msg_InputMgr_CreateKeyboardDevice,
					Properties.Resources.Msg_InputMgr_CreateKeyboardDevice_Failure
				);
				// エラーログ生成
				Tools.CommonTools.CreateErrorLogFile(ex);

				// メッセージウィンドウを表示し終了
				MessageBox.Show(null,
					Properties.Resources.ErrorMsg_InputMgr_Show_FailureCreateKeyboardDevice_Text,
					Properties.Resources.ErrorMsg_InputMgr_Show_FailureCreateKeyboardDevice_Caption,
					MessageBoxButtons.OK, MessageBoxIcon.Error);

				return false;
			}

			return true;
		}

		#endregion


		#region キー入力 / キー判定

		/// <summary>
		/// メインループからの呼び出しを想定し、デバイスの利用権取得とキーボード状態のキャプチャを行う。キーの入力状態に応じた処理の実行及びトップ画面へのキー入力伝達を行う。
		/// </summary>
		private void _KeyInput()
		{
			// バッファに格納されたデータを受け取るコレクション
			BufferedDataCollection buffer = null;

			try
			{
				this._Device.Acquire();						// デバイスの利用権を取得
				buffer = this._Device.GetBufferedData();	// キーボードの状態をキャプチャ
			}
			catch (DirectXException) { }

			// 利用権の取得もしくはキャプチャに失敗したか、データが無い場合は中止
			if (buffer == null) return;

			// 個々のバッファデータで入力に対する処理を行う
			foreach (BufferedData bufferedData in buffer)
			{
				Key inputKey = ((Key)bufferedData.Offset);

				if (bufferedData.Data == 0x80)		// ==============================
				{									// キーを押した時のバッファデータ
					Tools.DebugTools.ConsolOutputMessage("DirectInput KeyDown", inputKey.ToString());

					this._SetSpecialKeyFlags(inputKey, true);		// 特殊キーのフラグの設定

					switch (inputKey)				// 個々のキーに対する特殊な処理を記述
					{
						case Key.Escape:							// エスケープキー
							if (this._IsProtection) break;			// (児童モードの場合を除き)
							MainWindow.Running = false;				// プログラム終了
							LogFileManager.WriteLine(
								Properties.Resources.Msg_MainWindow_KeyPress,
								Properties.Resources.Msg_MainWindow_KeyPress_Esc
							);
							break;

						case Key.D:									// Ctrl+D
							if (this._IsControl)					// デバッグウィンドウ表示
								Tools.DebugTools.ShowDebugWindow();
							break;
					}

					MainWindow.Instance.TopScreenKeyDown(inputKey);
				}
				else if(bufferedData.Data == 0x00)	// ================================
				{									// キーを放したときのバッファデータ
					Tools.DebugTools.ConsolOutputMessage("DirectInput KeyUp", inputKey.ToString());

					this._SetSpecialKeyFlags(inputKey, false);		// 特殊キーのフラグの設定
				}


				if (this._IsEnter && this._IsAlternate)
				{											// Alt + Enter キーの判定
					DrawManager.ChangeWindowMode();			// ウィンドウ/フルスクリーンモードの切り替え
					LogFileManager.WriteLine(
						Properties.Resources.Msg_MainWindow_KeyPress,
						Properties.Resources.Msg_MainWindow_KeyPress_AltEnter
					);
				}
			}
		}


		#region 即時方式 (廃止済み)
		/// <summary>
		/// 即時方式でキーボード入力を受ける (主に muphic を操作する特殊なコマンド)。_KeyInput メソッドからの呼び出しでのみ使用する。
		/// </summary>
		private void _KeyInputFromImmediate()
		{
			KeyboardState state = null;

			try
			{	// キーボードの状態をキャプチャ
				state = this._Device.GetCurrentKeyboardState();
			}
			catch (DirectXException) { }

			// キャプチャに失敗したか、データが無い場合は中止
			if (state == null) return;


			if (state[Key.Return] && (state[Key.RightAlt] || state[Key.LeftAlt]))
			{											// Alt + Enter キーの判定
				DrawManager.ChangeWindowMode();			// ウィンドウ/フルスクリーンモードの切り替え
				LogFileManager.WriteLine(
					Properties.Resources.Msg_MainWindow_KeyPress,
					Properties.Resources.Msg_MainWindow_KeyPress_AltEnter
				);
			}
		}
		#endregion



		#endregion


		#region 特殊キー

		/// <summary>
		/// key が特殊キー (エンターキー、コントロールキー、Alt キー、シフトキー) だった場合、該当する特殊キーのフラグを flag の値にセットする。
		/// </summary>
		/// <param name="key">特殊キー (特殊キーでない場合、このメソッドは何も行わない)。</param>
		/// <param name="flag">特殊キーに設定するフラグ。</param>
		/// <returns>key が特殊キーだった場合は true、それ以外は false。</returns>
		private bool _SetSpecialKeyFlags(Key key, bool flag)
		{
			switch (key)
			{
				case Key.Return:
					this._IsNormalEnter = flag;
					return true;

				case Key.NumPadEnter:
					this._IsNumPadEnter = flag;
					return true;

				case Key.LeftControl:
					this._IsLeftControl = flag;
					return true;

				case Key.RightControl:
					this._IsRightControl = flag;
					return true;

				case Key.LeftAlt:
					this._IsLeftAlternate = flag;
					return true;

				case Key.RightAlt:
					this._IsRightAlternate = flag;
					return true;

				case Key.LeftShift:
					this._IsLeftShift = flag;
					return true;

				case Key.RightShift:
					this._IsRightShift = flag;
					return true;

				default:
					return false;
			}
		}

		#endregion


		#region デバイス解放

		/// <summary>
		/// キーボード入力管理クラスで使用したリソースを破棄する。
		/// </summary>
		private void _Dispose()
		{
			this._SafeDispose(this._Device);
		}

		#endregion


		#region 外部から呼ばれるメソッド群

		/// <summary>
		/// 入力管理クラスの静的インスタンス生成及び使用するデバイスの初期化を行う。
		/// インスタンス生成後に 1 度しか実行できない点に注意。
		/// </summary>
		/// <returns>正常に初期化が行われた場合は true、それ以外は false。</returns>
		public static bool Initialize(MainWindow mainWindow)
		{
			return Muphic.Manager.KeyboardInputManager.Instance._Initialize(mainWindow);
		}

		/// <summary>
		/// メインループからの呼び出しを想定し、デバイスの利用権取得とキーボード状態のキャプチャを行う。キーの入力状態に応じた処理の実行及びトップ画面へのキー入力伝達を行う。
		/// </summary>
		public static void KeyInput()
		{
			Muphic.Manager.KeyboardInputManager.Instance._KeyInput();
		}

		/// <summary>
		/// キーボード入力管理クラスで使用したリソースを破棄する。
		/// </summary>
		public static void Dispose()
		{
			Muphic.Manager.KeyboardInputManager.Instance._Dispose();
		}

		#endregion

	}
}
