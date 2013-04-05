#if DEBUG
#warning ConfigurationTool デバッグビルドです。
#endif

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using ConfigurationTool.Manager;

namespace ConfigurationTool
{
	using Settings = ConfigurationTool.Properties.Settings;

	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			// ハンドルされない例外が発生した場合の処理の登録
			System.Threading.Thread.GetDomain().UnhandledException
				+= new UnhandledExceptionEventHandler(Program_UnhandledException);

			bool isFirstLaunch = false;

			if (args.Length == 1 && args[0] == "-firstLaunch")
			{
				// muphic 初回起動時の設定で呼び出された場合
				isFirstLaunch = true;
			}
			else
			{
				// 既に起動している場合はエラーメッセージ表示後終了
				if (!Program.CheckMultipleStartUp())
				{
					MessageBox.Show(
						Properties.Resources.ErrorMsg_Show_MultipleStartup_Text,
						Properties.Resources.ErrorMsg_Show_CommonCaption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					  );

					return 0;
				}
			}

			// === 以下起動フェーズ ===

			if (!ConfigurationManager.Initialize()) return 1;			// 構成設定管理クラス初期化  失敗したらプログラム終了

			Application.EnableVisualStyles();							// XP 視覚スタイルの適用
			Application.SetCompatibleTextRenderingDefault(false);		// よくわからな何かを設定

			using (var confWindow = new ConfigurationWindow(!isFirstLaunch))	// 動作設定ウィンドウ生成
			{
				Application.Run(confWindow);									// 動作設定ウィンドウ表示  メインループ開始

				if (confWindow.DialogResult == DialogResult.Retry)				// 動作設定ウィンドウが閉じられた際、
				{																// DialogResult が Retry に設定されていた場合
					if (File.Exists(Settings.Default.MuphicFileName))
					{															// 同じフォルダ内に muphic.exe が存在するかを確認し
						Process.Start(Settings.Default.MuphicFileName);			// 存在する場合は muphic.exe を実行
					}
				}
			}

			return 0;
		}


		#region 多重起動防止

		/// <summary>
		/// 多重起動の阻止に使用する同期オブジェクト
		/// </summary>
		private static System.Threading.Mutex __mutex;

		/// <summary>
		/// muphic が既に起動されているかをチェックする。
		/// </summary>
		/// <returns>既に起動されていると判断した場合は false、それ以外の場合は true。</returns>
		private static bool CheckMultipleStartUp()
		{
			// 同時に1つのスレッドでしか所有できない同期オブジェクトであるミューテックスを使用する

			// ミューテックスの初期所有権
			bool createdNew;

			// ミューテックスクラスの生成
			// 同じ名前のミューテックスが既に存在している場合、createNewにfalseが格納される
			Program.__mutex = new System.Threading.Mutex(true, "muphic_v7", out createdNew);

			// 初期所有権が得られた場合、初めて起動されると判断する
			if (createdNew) return true;

			// 初期所有権が付与されなかった場合は既に起動していると判断する
			else return false;
		}

		#endregion


		#region 例外処理

		/// <summary>
		/// 予期しない例外が発生した際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Program_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Exception exception = e.ExceptionObject as Exception;

				// エラーの詳細をファイルに書き込み
				string errorlogPath = 
					Program.WriteErrorLog(exception.ToString());

				// エラーについてメッセージ表示
				MessageBox.Show(
					Properties.Resources.ErrorMsg_Show_ThreadException.Replace("%str1%", exception.Message).Replace("%str2%", errorlogPath),
					Properties.Resources.ErrorMsg_Show_CommonCaption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				
			}
			catch (Exception)
			{
			}
			finally
			{
				Environment.Exit(1);
			}
		}


		/// <summary>
		/// 与えられたメッセージを、規定のログファイルに書き込む。
		/// </summary>
		/// <param name="message">書き込むメッセージ。</param>
		/// <returns>書き込まれたログファイルのパス。</returns>
		public static string WriteErrorLog(string message)
		{
			// エラーログを吐く位置は、規定のユーザーフォルダ直下に muphic_conf.log
			string errorlogPath = Path.Combine(ConfigurationData.UserDataFolder, Properties.Settings.Default.ErrorLogFileName);
			
			// エラーの詳細をファイルに書き込み
			Directory.CreateDirectory(errorlogPath);
			File.AppendAllText(errorlogPath, Properties.Resources.ErrorMsg_Log_ThreadException.Replace("%str1%", DateTime.Now.ToString()).Replace("%str2%", message));

			return errorlogPath;
		}

		#endregion
	}
}
