using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ReleaseTool
{
	/// <summary>
	/// muphic 発行版を生成する為のメソッドを実装するクラス。
	/// </summary>
	public class Releaser
	{
		/// <summary>
		/// muphic の現在の実行ファイルから、muphic を簡易的に発行する。
		/// </summary>
		/// <param name="createDirectory">発行先のフォルダ。</param>
		/// <param name="releasePath">muphic の Release 実行ファイルパス。</param>
		/// <param name="debugPath">muphic の Debug 実行ファイルパス。</param>
		/// <param name="archivePath">muphic のアーカイブファイルパス。</param>
		/// <param name="confPath">muphic の構成設定ツール実行ファイルパス。</param>
		/// <param name="jpnKanaConvDllPath">日本語変換用必須 DLL ファイルパス。</param>
		/// <param name="jpnKanaConvHelpDllPath">日本語変換用必須 DLL ファイルパス。</param>
		/// <returns>発行に成功した場合は true、それ以外は false。</returns>
		public static bool Release(string createDirectory, string releasePath, string debugPath, string archivePath, string confPath, string jpnKanaConvDllPath, string jpnKanaConvHelpDllPath)
		{
			#region ファイル存在チェック
			if (!File.Exists(releasePath))
			{
				MessageBox.Show(
					Properties.Settings.Default.FileNotfoundMessage.Replace("%str1%", releasePath),
					Properties.Settings.Default.FileNotfoundTitle.Replace("%str1%", "Release 実行"),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
				return false;
			}
			if (!File.Exists(debugPath))
			{
				MessageBox.Show(
					Properties.Settings.Default.FileNotfoundMessage.Replace("%str1%", debugPath),
					Properties.Settings.Default.FileNotfoundTitle.Replace("%str1%", "Debug 実行"),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
				return false;
			}
			if (!File.Exists(archivePath))
			{
				MessageBox.Show(
					Properties.Settings.Default.FileNotfoundMessage.Replace("%str1%", archivePath),
					Properties.Settings.Default.FileNotfoundTitle.Replace("%str1%", "アーカイブ "),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
				return false;
			}
			if (!File.Exists(confPath))
			{
				MessageBox.Show(
					Properties.Settings.Default.FileNotfoundMessage.Replace("%str1%", confPath),
					Properties.Settings.Default.FileNotfoundTitle.Replace("%str1%", "設定ツール 実行"),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
				return false;
			}
			if (!File.Exists(jpnKanaConvDllPath))
			{
				MessageBox.Show(
					Properties.Settings.Default.FileNotfoundMessage.Replace("%str1%", jpnKanaConvDllPath),
					Properties.Settings.Default.FileNotfoundTitle.Replace("%str1%", "かな変換用必須 DLL "),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
				return false;
			}
			if (!File.Exists(jpnKanaConvHelpDllPath))
			{
				MessageBox.Show(
					Properties.Settings.Default.FileNotfoundMessage.Replace("%str1%", jpnKanaConvHelpDllPath),
					Properties.Settings.Default.FileNotfoundTitle.Replace("%str1%", "かな変換用必須 DLL "),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				 );
				return false;
			}
			#endregion

			// muphic のバージョン情報取得
			var versionInfo = FileVersionInfo.GetVersionInfo(releasePath);

			// 発行するディレクトリ名を生成
			string distDirectory = Properties.Settings.Default.DistDirectoryName.Replace("%str1%", versionInfo.FileVersion);

			#region 発行先ディレクトリの存在チェック
			if (Directory.Exists(distDirectory))
			{
				var result = MessageBox.Show(
					Properties.Settings.Default.OverwriteMessage.Replace("%str1%", distDirectory),
					Properties.Settings.Default.OverwriteTitle,
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question
				);

				if (result == DialogResult.OK)
				{
					try
					{
						Directory.Delete(distDirectory, true);
					}
					catch (Exception e)
					{
						MessageBox.Show(e.ToString(), e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			#endregion

			#region 発行先ディレクトリ生成
			// 発行先サブディレクトリ生成
			string distReleaseDirectory = Path.Combine(distDirectory, "Release");
			string distDebugDirectory = Path.Combine(distDirectory, "Debug");
			string distArchiveDirectory = Path.Combine(distDirectory, "Archive");

			try
			{
				// 発行先ディレクトリ生成
				Directory.CreateDirectory(distDirectory);

				// 発行先サブディレクトリ生成
				Directory.CreateDirectory(distReleaseDirectory);
				Directory.CreateDirectory(distDebugDirectory);
				Directory.CreateDirectory(distArchiveDirectory);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			#endregion

			#region 発行 (ファイルコピー)
			// 発行先ファイルパス生成
			string distReleasePath = Path.Combine(distReleaseDirectory, Path.GetFileName(releasePath));
			string distConfPath = Path.Combine(distReleaseDirectory, Path.GetFileName(confPath));
			string distDebugPath = Path.Combine(distDebugDirectory, Path.GetFileName(debugPath));
			string distArchivePath = Path.Combine(distArchiveDirectory, Path.GetFileName(archivePath));
			string distJpnConvDllReleasePath = Path.Combine(distReleaseDirectory, Path.GetFileName(jpnKanaConvDllPath));
			string distJpnConvDllDebugPath = Path.Combine(distDebugDirectory, Path.GetFileName(jpnKanaConvDllPath));
			string distJpnConvHelpDllReleasePath = Path.Combine(distReleaseDirectory, Path.GetFileName(jpnKanaConvHelpDllPath));
			string distJpnConvHelpDllDebugPath = Path.Combine(distDebugDirectory, Path.GetFileName(jpnKanaConvHelpDllPath));

			try
			{
				File.Copy(releasePath, distReleasePath);
				File.Copy(confPath, distConfPath);
				File.Copy(debugPath, distDebugPath);
				File.Copy(archivePath, distArchivePath);
				File.Copy(jpnKanaConvDllPath, distJpnConvDllReleasePath);
				File.Copy(jpnKanaConvDllPath, distJpnConvDllDebugPath);
				File.Copy(jpnKanaConvHelpDllPath, distJpnConvHelpDllReleasePath);
				File.Copy(jpnKanaConvHelpDllPath, distJpnConvHelpDllDebugPath);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			#endregion

			return true;
		}
	}
}
