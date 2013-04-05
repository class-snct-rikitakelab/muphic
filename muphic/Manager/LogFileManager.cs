using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Muphic.Tools;

namespace Muphic.Manager
{
	/// <summary>
	/// ログファイル管理クラス (シングルトン・継承不可) 
	/// <para>ログファイルの生成・管理及び、実行中における任意の文字列のログ出力を行う。</para>
	/// </summary>
	public sealed class LogFileManager : Manager
	{
		/// <summary>
		/// ログファイル管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static LogFileManager __instance = new LogFileManager();

		/// <summary>
		/// ログファイル管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static LogFileManager Instance
		{
			get { return LogFileManager.__instance; }
		}


		#region プロパティ

		/// <summary>
		/// 実行中にロギングを行われることを示す値を取得または設定する。
		/// </summary>
		public static bool IsLogging { get; private set; }

		/// <summary>
		/// ログファイル書き込みストリーム
		/// </summary>
		private StreamWriter LogfileWriter { get; set; }

		#endregion


		#region コンストラクタ/初期化

		/// <summary>
		/// ログファイル管理クラスの新しインスタンスを初期化する。
		/// </summary>
		private LogFileManager()
		{
			LogFileManager.IsLogging = false;		// 初期状態ではロギング無効設定
		}


		/// <summary>
		/// ログファイル管理クラスの静的インスタンス生成及び使用するログファイルの書き込みストリームの設定を行う。
		/// </summary>
		/// <returns>初期化が正常に行われた場合は true、それ以外は false。</returns>
		private bool _initialize()
		{
			if (!ConfigurationManager.Current.IsLogging)
			{															// ロギングを行う設定かどうかを確認
				LogFileManager.IsLogging = false;
				return true;											// ロギングしない設定の場合は初期化は行わないが成功扱いとする
			}

			LogFileManager.IsLogging = true;

			if (this._IsInitialized) return false;						// 初期化済みの場合は初期化を行わない

			if (!this._initializeStreamWriter()) return false;			// 書き込みストリームの初期化 失敗したらプログラム終了

			DebugTools.ConsolOutputMessage("LogfileManager -Initialize", "ログファイル管理クラス生成完了", true);

			return this._IsInitialized = true;							// 初期化後に初期化済みフラグを立てる
		}


		/// <summary>
		/// ログファイル書き込みストリームの初期化を行う。
		/// </summary>
		/// <returns>初期化が正常に行われたか、初期化失敗時でも起動続行が選択された場合は true、それ以外は false。</returns>
		private bool _initializeStreamWriter()
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(ConfigurationManager.Current.LogFileSavePath));
				this.LogfileWriter = new StreamWriter(ConfigurationManager.Current.LogFileSavePath, false, Encoding.GetEncoding(932));
				this.LogfileWriter.AutoFlush = true;
			}
			catch (Exception)
			{
				// 何らかのエラーにより書き込みストリーム初期化に失敗した場合はメッセージウィンドウを表示  起動を続行するか尋ねる
				if (MessageBox.Show(null,
						CommonTools.GetResourceMessage(Properties.Resources.ErrorMsg_LogFileMgr_Show_FailureCreate_Text, Path.GetFullPath(ConfigurationManager.Current.LogFileSavePath)),
						Properties.Resources.ErrorMsg_LogFileMgr_Show_FailureCreate_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
				{
					LogFileManager.IsLogging = false;		// 起動続行が選択された場合
				}											// ロギング機能をOFFにして起動を続行
				else
				{											// 起動中止が選択された場合
					return false;							// falseを返す
				}
			}

			if (LogFileManager.IsLogging)					// ロギングが行われる設定となった場合
			{
				this._WriteLine(							// ログファイルのヘッダ部を書き込み
					CommonTools.GetResourceMessage(Properties.Resources.Msg_LogfileMgr_LogfileHeader, SystemInfoManager.MuphicVersion),
					"", false, false, false
				);
				this._WriteLine(							// ロギング開始メッセージを書き込み
					Properties.Resources.Msg_LogFileMgr_StartLogging,
					"", true, false, false
				);
			}
			else
			{												// ロギングが中止された場合、コンソールにメッセージ出力
				DebugTools.ConsolOutputMessage("LogfileManager -Initialize", "ロギング中止", true);
			}

			
			return true;
		}

		#endregion


		#region 書き込み

		/// <summary>
		/// ログファイルへの書き込みを行う。
		/// </summary>
		/// <param name="message1">書き込むメッセージ1 (主にメッセージタイトル)。</param>
		/// <param name="message2">書き込むメッセージ2 (主にメッセージ本文)。</param>
		/// <param name="isDatetime">同時に日付も書き込む場合は true、それ以外は false。</param>
		/// <param name="isError">エラーメッセージであると明示する場合は true、それ以外は false。</param>
		/// <param name="isDebug">デバッグメッセージであると明示する場合は true、それ以外は false。</param>
		private void _WriteLine(string message1, string message2, bool isDatetime, bool isError, bool isDebug)
		{
			// ロギングを行わない設定になっているか、書き込みストリームが無効の場合は終了
			if (!LogFileManager.IsLogging || this.LogfileWriter == null) return;

			// メッセージ生成処理は Muphic.Tools.CommonTools.CreateLogMessage に移植

			try
			{						// 汎用ツールクラスで生成したログメッセージの書き込みを試行
				this.LogfileWriter.WriteLine(Tools.CommonTools.CreateLogMessage(message1, message2, isDatetime, isError, isDebug));
			}
			catch (Exception e)
			{						// 何らかの例外が発生した場合、その旨コンソールに出力
				DebugTools.ConsolOutputError("LogfileManager -WriteLine", e.ToString());
			}
		}

		#endregion


		#region 解放

		/// <summary>
		/// バッファのデータを全て書き込み、書き込みストリームを解放する。
		/// </summary>
		private void _Dispose()
		{
			this._DisposeStreamWriter(true);
		}

		/// <summary>
		/// バッファのデータを全て書き込み、書き込みストリームを解放する。
		/// </summary>
		/// <param name="writePlayTime">最後にプレイ時間を書き込む場合は true、それ以外は false。</param>
		private void _DisposeStreamWriter(bool writePlayTime)
		{
			if (this.LogfileWriter == null) return;

			if (writePlayTime)
			{
				StringBuilder timeMsg = new StringBuilder();
				TimeSpan playTime = FrameManager.PlayTime;
				
				if (playTime.Days > 0)
				{
					timeMsg.Append(playTime.Days);
					timeMsg.Append("日");
				}
				if (playTime.Hours > 0 || playTime.Days > 0)
				{
					timeMsg.Append(playTime.Hours);
					timeMsg.Append("時間");
				}
				if (playTime.Minutes > 0 || playTime.Hours > 0 || playTime.Days > 0)
				{
					timeMsg.Append(playTime.Minutes);
					timeMsg.Append("分");
				}

				timeMsg.Append(playTime.Seconds);
				timeMsg.Append("秒");

				this._WriteLine(Properties.Resources.Msg_LogfileMgr_Uptime, timeMsg.ToString(), true, false, false);
			}

			this._WriteLine(Properties.Resources.Msg_LogfileMgr_EndLogging, "", true, false, false);
			this.LogfileWriter.Flush();
			this.LogfileWriter.Dispose();
		}

		#endregion


		#region 外部から呼ばれるメソッド群

		/// <summary>
		/// ログファイル管理クラスの静的インスタンス生成及び使用するログファイルの書き込みストリームの設定を行う。
		/// インスタンス生成後に１度しか実行できない点に注意。
		/// </summary>
		/// <returns>初期化が正常に行われた場合は true、それ以外は false。</returns>
		public static bool Initialize()
		{
			return Muphic.Manager.LogFileManager.Instance._initialize();
		}

		/// <summary>
		/// ログファイル管理クラスで使用されている書き込みストリームの解放を行う。
		/// </summary>
		public static void Dispose()
		{
			Muphic.Manager.LogFileManager.Instance._Dispose();
		}


		/// <summary>
		/// ログファイルにメッセージの書き込みを行う。
		/// </summary>
		public static void WriteLine()
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(" ", "", true, false, false);
			DebugTools.ConsolOutputMessage(" ", false);
		}


		/// <summary>
		/// ログファイルにメッセージの書き込みを行う。
		/// </summary>
		/// <param name="message">書き込むメッセージ。</param>
		public static void WriteLine(string message)
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(message, "", true, false, false);
			DebugTools.ConsolOutputMessage(message, false);
		}
		/// <summary>
		/// ログファイルにメッセージの書き込みを行う。
		/// </summary>
		/// <param name="title">書き込むメッセージのタイトル。</param>
		/// <param name="message">書き込むメッセージ本文。</param>
		public static void WriteLine(string title, string message)
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(title, message, true, false, false);
			DebugTools.ConsolOutputMessage(title, message, false);
		}

		/// <summary>
		/// ログファイルにエラーメッセージの書き込みを行う。
		/// </summary>
		/// <param name="errorMessage">書き込むエラーメッセージ。</param>
		public static void WriteLineError(string errorMessage)
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(errorMessage, "", true, true, false);
			DebugTools.ConsolOutputError(errorMessage, "", false);
		}
		/// <summary>
		/// ログファイルにエラーメッセージの書き込みを行う。
		/// </summary>
		/// <param name="errorTitle">書き込むエラーメッセージのタイトル。</param>
		/// <param name="errorMessage">書き込むエラーメッセージ本文。</param>
		public static void WriteLineError(string errorTitle, string errorMessage)
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(errorTitle, errorMessage, true, true, false);
			DebugTools.ConsolOutputError(errorTitle, errorMessage, false);
		}

		/// <summary>
		/// ログファイルにデバッグ出力としてメッセージの書き込みを行う。
		/// </summary>
		/// <param name="message">書き込むメッセージ。</param>
		public static void WriteLineDebug(string message)
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(message, "", true, false, true);
		}
		/// <summary>
		/// ログファイルにデバッグ出力としてメッセージの書き込みを行う。
		/// </summary>
		/// <param name="title">書き込むメッセージのタイトル。</param>
		/// <param name="message">書き込むメッセージ本文。</param>
		public static void WriteLineDebug(string title, string message)
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(title, message, true, false, true);
		}

		/// <summary>
		/// ログファイルにデバッグ出力としてエラーメッセージの書き込みを行う。
		/// </summary>
		/// <param name="errorMessage">エラーメッセージ。</param>
		public static void WriteLineErrorDebug(string errorMessage)
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(errorMessage, "", true, true, true);
		}
		/// <summary>
		/// ログファイルにデバッグ出力としてエラーメッセージの書き込みを行う。
		/// </summary>
		/// <param name="errorPlace">エラーの発生箇所。</param>
		/// <param name="errorMessage">エラーメッセージ本文。</param>
		public static void WriteLineErrorDebug(string errorPlace, string errorMessage)
		{
			Muphic.Manager.LogFileManager.Instance._WriteLine(errorPlace, errorMessage, true, true, true);
		}
		

		///// <summary>
		///// ログファイルへの書き込みを行う (オプションを手動設定) 。
		///// </summary>
		///// <param name="message1">書き込むメッセージ1 (主にメッセージタイトル)。</param>
		///// <param name="message2">書き込むメッセージ2 (主にメッセージ本文)。</param>
		///// <param name="isDatetime">同時に日付も書き込む場合は true、それ以外は false。</param>
		///// <param name="isError">エラーメッセージであると明示する場合は true、それ以外は false。</param>
		///// <param name="isDebug">デバッグメッセージであると明示する場合は true、それ以外は false。</param>
		//public static void WriteLineManual(string message1, string message2, bool isDatetime, bool isError, bool isDebug)
		//{
		//    Muphic.Manager.LogFileManager.Instance._WriteLine(message1, message2, isDatetime, isError, isDebug);
		//}

		#endregion
	}
}
