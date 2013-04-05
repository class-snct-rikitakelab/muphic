using System;
using System.IO;
using System.Windows.Forms;

namespace LogCallBackTool
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// 各パスの生成
			
			// アプリケーションデータパス
			// Vista なら Users\ユーザー名\AppData
			// XP(笑)なら Documents and Settings\ユーザー名\Application Data
			string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SNCT\\muphic");

			// コピー元の muphic ログファイルのパス
			string logFilePath = Path.Combine(appDataPath, "muphic.log");

			// コピー元の muphic エラーログファイルのパス
			string errorLogFilePath = Path.Combine(appDataPath, "muphic_error.log");

			// コピー先のフォルダ名
			string distDirectoryName = "muphic log";

			// コピー先のファイル名に使用する日付
			string dateTime = System.DateTime.Now.ToString().Replace("/", "").Replace(":", "-");

			// コピー先のファイル名に使用される、コンピュータ名と日付
			string distFileName = System.Net.Dns.GetHostName() + " " + dateTime;

			// コピー先の muphic ログファイルのパス
			string distLogFileName = distFileName + " muphic.log";

			// コピー先の muphic エラーログファイルのパス
			string distErrorLogFileName = distFileName + " muphicError.log";


			// 回収対象のログが見つからなかった場合はメッセージを表示し終了
			if (!File.Exists(logFilePath) && !File.Exists(errorLogFilePath))
			{
				MessageBox.Show(
					Properties.Resources.Msg_Show_LogFileNotFound.Replace("%str1%", logFilePath).Replace("%str2%", errorLogFilePath),
					Properties.Resources.Msg_Show_LogFileNotFound_Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation
				);
				return;
			}

			try
			{
				Directory.CreateDirectory(distDirectoryName);
			}
			catch (Exception e)
			{
				MessageBox.Show(
					Properties.Resources.Msg_Show_LogFileNotFound.Replace("%str1%", e.ToString()),
					Properties.Resources.Msg_Show_LogFileNotFound_Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			if (File.Exists(logFilePath))
			{
				try
				{
					File.Copy(logFilePath, Path.Combine(distDirectoryName, distLogFileName), true);
				}
				catch (Exception e)
				{
					MessageBox.Show(
						Properties.Resources.Msg_Show_FileCopyFailure.Replace("%str1%", e.ToString()),
						Properties.Resources.Msg_Show_FileCopyFailure_Caption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
					return;
				}
			}
			if (File.Exists(errorLogFilePath))
			{
				try
				{
					File.Copy(errorLogFilePath, Path.Combine(distDirectoryName, distErrorLogFileName), true);
				}
				catch (Exception e)
				{
					MessageBox.Show(
						Properties.Resources.Msg_Show_FileCopyFailure.Replace("%str1%", e.ToString()),
						Properties.Resources.Msg_Show_FileCopyFailure_Caption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
					return;
				}
			}

		}
	}
}
