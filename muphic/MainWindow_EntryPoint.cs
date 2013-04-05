#if DEBUG
#warning muphic デバッグビルドです。
#endif

using System;
using System.Windows.Forms;

namespace Muphic
{
	/// <summary>
	/// muphic メインウィンドウ
	/// </summary>
	public sealed partial class MainWindow : Form
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		private static int Main()
		{
			Muphic.Tools.DebugTools.Test();									// 【デバッグ用】テストコードの実行
			Muphic.Tools.DebugTools.InitializeDebugWindow();				// 【デバッグ用】デバッグ用ウィンドウ生成

			Application.EnableVisualStyles();								// visual スタイル有効化
			Muphic.MainWindow.RegistExceptionEventHandler();				// 例外発生時のイベント ハンドラの登録

			if (!Muphic.MainWindow.CheckMultipleStartUp()) return 1;			// 多重起動チェック            既に起動していると判断した場合はプログラム終了
			if (!Muphic.MainWindow.CheckEssentialFiles()) return 1;				// 必須ファイルチェック        起動に必要なファイルが存在しない場合はプログラム終了
			if (!Muphic.Manager.ArchiveFileManager.Initialize()) return 1;		// アーカイブ管理クラス生成    アーカイブの展開に失敗した場合はプログラム終了
			if (!Muphic.Manager.ConfigurationManager.Initialize()) return 1;	// 構成設定管理クラス生成      構成設定の読込に失敗した場合はプログラム終了
			if (!Muphic.Manager.LogFileManager.Initialize()) return 1;			// ログファイル管理クラス生成  ロギングが開始できず、ユーザが終了を選択した場合はプログラム終了
			if (!Muphic.Manager.SystemInfoManager.Initialize()) return 1;		// システム情報管理クラス生成  同時に DirectX チェックを行い、使用できなければプログラム終了

			Muphic.Tools.DebugTools.ShowDebugWindow();						// 【デバッグ用】デバッグウィンドウ表示

			var splashWindow = new Muphic.SubForms.SplashWindow();			// スプラッシュウィンドウ生成
			splashWindow.Show();											// スプラッシュウィンドウ表示
			Application.DoEvents();											// ここまでの Windows 処理を全て実施 (フォーム上のラベルの表示等)

			using (Muphic.MainWindow.Instance = new MainWindow())			// メイン画面生成 (プログラムのメインループから脱出した際に自動的に破棄される)
			{
				if (!Muphic.MainWindow.Running) return 1;			// メイン画面生成時にメインループが無効になっていた場合はプログラム終了

				Muphic.Manager.FrameManager.CountStart();			// フレームカウント (FPS制御) 開始

				Muphic.MainWindow.Instance.Show();					// メインフォーム表示
				Muphic.MainWindow.Instance.Activate();				// メインフォームをアクティブ化
				splashWindow.Dispose();								// スプラッシュウィンドウ破棄

				Muphic.MainWindow.Instance.MainLoop();				// プログラムメインループ
			}

			return 0;
		}

	}
}
