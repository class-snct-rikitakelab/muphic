using System;
using System.IO;

namespace ReleaseTool
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			string releaseDir = "";

			// コマンドライン引数を取得
			string[] args = Environment.GetCommandLineArgs();

			if (args.Length <= 1)
			{
				// 引数が無しの場合、実行ファイルの位置に発行
				releaseDir = Directory.GetCurrentDirectory();
			}
			else
			{
				// 引数がある場合、そのパスに発行
				releaseDir = args[1];
			}

			Releaser.Release(
				releaseDir,
				Path.Combine(releaseDir, Properties.Settings.Default.MuphicReleasePath),
				Path.Combine(releaseDir, Properties.Settings.Default.MuphicDebugPath),
				Path.Combine(releaseDir, Properties.Settings.Default.MuphicArchivePath),
				Path.Combine(releaseDir, Properties.Settings.Default.MuphicConfPath),
				Path.Combine(releaseDir, Properties.Settings.Default.JpnKanaConversionDllReleasePath),
				Path.Combine(releaseDir, Properties.Settings.Default.JpnKanaConvHelperDllReleasePath)
			);
		}
	}
}
