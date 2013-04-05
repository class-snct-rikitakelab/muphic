using System;
using System.Windows.Forms;

using Muphic.Manager;
using Muphic.Tools;

namespace Muphic
{
	/// <summary>
	/// muphic メインウィンドウ
	/// </summary>
	public partial class MainWindow : Form
	{
		// ============================================
		// 実行中の例外発生時の処理に関するコードを記述
		// ============================================


		/// <summary>
		/// 実行中にハンドルされない例外が発生した際にイベントを発生させるようイベント ハンドラへの登録を行う。
		/// </summary>
		private static void RegistExceptionEventHandler()
		{
			// フォーム上でハンドルされない例外が発生した場合の処理の登録
			Application.ThreadException
				+= new System.Threading.ThreadExceptionEventHandler(MainWindow.Application_ThreadException);

			// ハンドルされない例外が発生した場合の処理の登録
			System.Threading.Thread.GetDomain().UnhandledException +=
				new UnhandledExceptionEventHandler(MainWindow.MainWindow_UnhandledException);
		}


		/// <summary>
		/// ハンドルされなかった例外が発生した際のイベント。
		/// <para>このメソッドが呼ばれると、例外に関するエラーメッセージが表示され、muphic が終了する。</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			try
			{
				// カーソルを表示
				System.Windows.Forms.Cursor.Show();

				// エラーログ生成
				CommonTools.CreateErrorLogFile(e.Exception);

				// ログファイルに書き込み
				LogFileManager.WriteLineError(Properties.Resources.Msg_MainWindow_ThreadExeption);
				LogFileManager.WriteLineError(e.Exception.ToString());

				// エラーメッセージを表示
				MessageBox.Show(
					MainWindow.Instance,
					CommonTools.GetResourceMessage(Properties.Resources.ErrorMsg_MainWindow_Show_ThreadExeption_Text, e.Exception.Message),
					Properties.Resources.ErrorMsg_MainWindow_Show_ThreadExeption_Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
			finally
			{
				// メインウィンドウのインスタンスが生成されていなければ、強制終了
				if (MainWindow.Instance == null) Environment.Exit(1);

				// ループ脱出フラグが既に立っていた場合は、強制終了させる
				if (!MainWindow.Running) Environment.Exit(1);

				// それ以外の場合は、ループ脱出フラグを立てる
				else MainWindow.Running = false;
			}
		}


		/// <summary>
		/// メイン・スレッド以外でハンドルされなかった例外が発生した際のイベント。
		/// <para>このメソッドが呼ばれると、例外に関するエラーメッセージが表示され、muphic が終了する。</para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void MainWindow_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				// カーソルを表示
				System.Windows.Forms.Cursor.Show();

				Exception exception = e.ExceptionObject as Exception;

				// エラーログ生成
				CommonTools.CreateErrorLogFile(exception);

				// ログファイルに書き込み
				LogFileManager.WriteLineError(Properties.Resources.Msg_MainWindow_UnhandledException);
				LogFileManager.WriteLineError(exception.ToString());

				// メッセージウィンドウの表示
				MessageBox.Show(
					CommonTools.GetResourceMessage(Properties.Resources.ErrorMsg_MainWindow_Show_UnhandledException_Text, exception.Message),
					Properties.Resources.ErrorMsg_MainWindow_Show_UnhandledException,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
			finally
			{
				// メインウィンドウのインスタンスが生成されていなければ、強制終了
				if (MainWindow.Instance == null) Environment.Exit(1);

				// メインウィンドウのインスタンスが生成されているが、ループ脱出フラグが既に立っていた場合も強制終了
				else if (!MainWindow.Running) Environment.Exit(1);

				// それ以外の場合は、ループ脱出フラグを立てる
				else MainWindow.Running = false;
			}
		}

	}
}
