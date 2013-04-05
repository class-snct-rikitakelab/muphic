using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;

namespace Muphic.Manager
{
	/// <summary>
	/// 音声管理クラス (シングルトン・継承不可) 
	/// <para>DirectSound を利用した音声ファイルの管理を行い、再生のための静的メソッドを提供する。</para>
	/// </summary>
	public sealed class SoundManager : Manager
	{
		/// <summary>
		/// 音声管理クラスの静的インスタンス (シングルトンパターン)。
		/// </summary>
		private static SoundManager __instance = new SoundManager();

		/// <summary>
		/// 音声管理クラスの静的インスタンス (シングルトンパターン)。
		/// </summary>
		private static SoundManager Instance
		{
			get { return SoundManager.__instance; }
		}


		#region プロパティ

		/// <summary>
		/// DirectSound デバイス オブジェクト。
		/// </summary>
		private Device _Device { get; set; }

		/// <summary>
		/// バッファについての情報設定クラス。
		/// </summary>
		private BufferDescription _BufDesc { get; set; }

		/// <summary>
		/// DirectSound セカンダリバッファテーブル。
		/// 音源ファイルパスをキーとして、再生用セカンダリバッファを関連付けている。
		/// </summary>
		private Dictionary<string, SecondaryBuffer> _BufferTable { get; set; }

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// 音声管理クラスの新しインスタンスを初期化する。
		/// </summary>
		private SoundManager()
		{
			this._BufferTable = new Dictionary<string, SecondaryBuffer>();	// セカンダリバッファテーブルを初期化
		}


		/// <summary>
		/// 音声管理クラスの静的インスタンス生成及び使用するサウンドデバイス等の初期化を行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <param name="mainWindow">muphic メインウィンドウ。</param>
		/// <returns>正常に初期化が行われた場合は true、それ以外は false。</returns>
		private bool _Initialize(MainWindow mainWindow)
		{
			if (this._IsInitialized) return false;					// 初期化済みの場合は初期化を行わない

			if(!this._InitializeDevice(mainWindow)) return false;	// サウンドデバイスの初期化 失敗したらプログラム終了

			return this._IsInitialized = true;						// 初期化後に初期化済みフラグを立てる
		}


		/// <summary>
		/// デバイスの初期化を行う。
		/// </summary>
		/// <param name="mainWindow">muphic メインウィンドウ。</param>
		/// <returns>初期化に成功した場合はtrue、それ以外はfalse。</returns>
		private bool _InitializeDevice(MainWindow mainWindow)
		{
			try
			{
				this._Device = new Device();													// デバイス生成
				this._Device.SetCooperativeLevel(mainWindow, CooperativeLevel.Priority);		// デバイスを優先協調レベルに設定
				LogFileManager.WriteLine(
					Properties.Resources.Msg_SoundMgr_CreateDevice,
					Properties.Resources.Msg_SoundMgr_CreateDevice_Priority
				);
			}
			catch (DirectXException ex)
			{
				// デバイス生成に失敗した場合、ログ表示
				LogFileManager.WriteLineError(
					Properties.Resources.Msg_SoundMgr_CreateDevice,
					Properties.Resources.Msg_SoundMgr_CreateDevice_Failure
				);
				// エラーログ表示
				Tools.CommonTools.CreateErrorLogFile(ex);

				// メッセージウィンドウを表示し終了
				MessageBox.Show(null,
					Properties.Resources.ErrorMsg_SoundMgr_Show_FailureCreateDevice_Text,
					Properties.Resources.ErrorMsg_SoundMgr_Show_FailureCreateDevice_Caption,
					MessageBoxButtons.OK, MessageBoxIcon.Error);

				return false;
			}

			this._BufDesc = new BufferDescription();
			this._BufDesc.ControlVolume = true;

			Tools.DebugTools.ConsolOutputMessage("SoundManager -Initialize", "音声管理クラス生成完了", true);
			return true;
		}

		#endregion


		#region 音声ファイルの登録と削除

		/// <summary>
		/// 指定した音声ファイルをセカンダリバッファテーブルに登録する。
		/// </summary>
		/// <param name="fileName">登録する音声ファイルのパス。</param>
		/// <returns>登録に成功したらtrue、それ以外はfalse。</returns>
		private bool _Regist(string fileName)
		{
			SecondaryBuffer secondaryBuffer = null;

			if (!ArchiveFileManager.Exists(fileName))
			{
				// ファイルが存在しなかった場合
				Tools.DebugTools.ConsolOutputError("SoundManager - RegistSound", "音声ファイル\"" + fileName + "\"が見つからない");
				return false;
			}

			try
			{
				// 指定されたファイル名でセカンダリバッファの作成を試みる
				secondaryBuffer = new SecondaryBuffer(new System.IO.MemoryStream(ArchiveFileManager.GetData(fileName)), this._BufDesc, this._Device);
				secondaryBuffer.Volume = ConfigurationManager.Current.DirectSoundVolume;
			}
			catch (SoundException)
			{
				// サウンドに関する何らかのエラーが発生した場合
				Tools.DebugTools.ConsolOutputError("SoundManager - RegistSound", "DirectSound.SoundException - セカンダリバッファ生成に失敗");
				return false;
			}

			// 作成したセカンダリバッファをテーブルに登録する
			this._BufferTable.Add(fileName, secondaryBuffer);

			return true;
		}


		/// <summary>
		/// セカンダリバッファテーブルから指定した音声ファイルを削除する。
		/// </summary>
		/// <param name="fileName">削除する音声ファイルのパス。</param>
		/// <returns>削除に成功したらtrue、それ以外はfalse。</returns>
		private bool _Delete(string fileName)
		{
			if (!_BufferTable.ContainsKey(fileName)) return false;	// キー(ファイル名)の存在チェック 存在しなければ処理せず戻る

			this._BufferTable[fileName].Dispose();					// キーに該当するセカンダリバッファを解放
			this._BufferTable.Remove(fileName);						// テーブルから削除

			return true;
		}

		#endregion


		#region 音声ファイルの再生/停止

		/// <summary>
		/// 指定した音声ファイルを再生する。
		/// </summary>
		/// <param name="fileName">再生する音声ファイル。</param>
		private bool _Play(string fileName)
		{
			SecondaryBuffer secondaryBuffer = null;

			if (_BufferTable.ContainsKey(fileName))
			{														// 指定された音声ファイルのセカンダリバッファが既に登録済みの場合
				secondaryBuffer = this._BufferTable[fileName];		// 当該セカンダリバッファのデータを参照
				secondaryBuffer.Stop();								// 当該セカンダリバッファの再生を停止する
				secondaryBuffer.SetCurrentPosition(0);				// 当該セカンダリバッファの再生位置を一番最初に戻す
				secondaryBuffer.Play(0, BufferPlayFlags.Default);	// 当該セカンダリバッファの通常再生
			}
			else
			{														// 指定された音声ファイルのセカンダリバッファがテーブル内に存在しない場合
				if (!this._Regist(fileName)) return false;			// 新しくセカンダリバッファを作成しテーブルに登録(失敗したら無視し戻る)
				secondaryBuffer = this._BufferTable[fileName];		// 登録したセカンダリバッファのデータを参照
				secondaryBuffer.Play(0, BufferPlayFlags.Default);	// 当該セカンダリバッファの通常再生
			}

			return true;
		}


		/// <summary>
		/// 指定した音声ファイルの再生を停止する。
		/// </summary>
		/// <param name="fileName">停止する音声ファイル。</param>
		private void _Stop(string fileName)
		{
			if (!this._BufferTable.ContainsKey(fileName)) return;	// 指定された音声ファイルのセカンダリバッファがテーブル内に存在しない場合、無視し戻る

			this._BufferTable[fileName].Stop();						// 当該セカンダリバッファの再生停止
		}


		/// <summary>
		/// 全てのセカンダリバッファを解放し、テーブルをクリアする。
		/// </summary>
		private void _Clear()
		{
			foreach (KeyValuePair<string, SecondaryBuffer> key in this._BufferTable)
			{
				key.Value.Dispose();
			}

			this._BufferTable.Clear();
		}

		#endregion


		#region デバイス解放

		/// <summary>
		/// 音声管理クラスで使用したリソースを破棄する。
		/// </summary>
		private void _Dispose()
		{
			this._SafeDispose(this._BufDesc);
			this._SafeDispose(this._Device);
		}

		#endregion


		#region 外部から呼ばれるメソッド群

		/// <summary>
		/// 音声管理クラスの静的インスタンス生成及び使用するサウンドデバイス等の初期化を行う。
		/// インスタンス生成後に 1 度しか実行できない点に注意。
		/// </summary>
		/// <returns>正常に初期化が行われた場合は true、それ以外は false。</returns>
		public static bool Initialize(MainWindow mainScreen)
		{
			return Muphic.Manager.SoundManager.Instance._Initialize(mainScreen);
		}


		/// <summary>
		/// 指定した音声ファイルを再生する。
		/// </summary>
		/// <param name="fileName">再生する音声ファイル。</param>
		public static void Play(string fileName)
		{
			Muphic.Manager.SoundManager.Instance._Play(fileName);
		}

		/// <summary>
		/// 指定した動物を再生する (動物の種類と音階から再生するファイルを自動的に選択する)。
		/// </summary>
		/// <param name="animal">再生する動物。</param>
		public static void Play(CompositionScreenParts.Animal animal)
		{
			Muphic.Manager.SoundManager.Instance._Play(Tools.CompositionTools.GetSoundFileName(animal));
		}


		/// <summary>
		/// 指定した音声ファイルの再生を停止する。
		/// </summary>
		/// <param name="fileName">停止する音声ファイル。</param>
		public static void Stop(string fileName)
		{
			Muphic.Manager.SoundManager.Instance._Stop(fileName);
		}


		/// <summary>
		/// セカンダリバッファテーブルから指定した音声ファイルを削除する。
		/// </summary>
		/// <param name="fileName">削除する音声ファイルのパス。</param>
		/// <returns>削除に成功したら true、それ以外は false。</returns>
		public static void Delete(string fileName)
		{
			Muphic.Manager.SoundManager.Instance._Delete(fileName);
		}


		/// <summary>
		/// セカンダリバッファテーブルをクリアする。
		/// </summary>
		public static void Clear()
		{
			Muphic.Manager.SoundManager.Instance._Clear();
		}


		/// <summary>
		/// 音声管理クラスで使用したリソースを破棄する。
		/// </summary>
		public static void Dispose()
		{
			Muphic.Manager.SoundManager.Instance._Dispose();
		}

		#endregion

	}
}
