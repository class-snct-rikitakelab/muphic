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
		// =======================================
		// muphic のメイン処理に関するコードを記述
		// =======================================


		/// <summary>
		/// プログラムのメインループを実行する。
		/// </summary>
		private void MainLoop()
		{
			// フルスクリーンモード設定の場合、ここで切り換え
			DrawManager.SetFullScreenMode();

			// 起動フェーズ完了とメインループ実行のメッセージ
			LogFileManager.WriteLine(Properties.Resources.Msg_MainWindow_StartupPhase_Complete);

			// muphic メインループ
			while (MainWindow.Running)
			{
				if (DrawManager.CheckDevice()) 		// DirectXの描画デバイスのチェック
				{									// デバイスが使用可能状態の時のみ
					this.Render();					//  ・1フレームの描画
					FrameManager.Update();			//  ・FPS制御
				}

				KeyboardInputManager.KeyInput();	// キーボード入力
				Application.DoEvents();				// デバイスの状態に関わらずイベント処理
			}
		}


		/// <summary>
		/// １フレーム単位の描画を行う。
		/// </summary>
		private void Render()
		{
			DrawManager.BeginScene();					// シーンレンダリング開始を描画管理クラスへ通知

			var drawStatus = new DrawStatusArgs();		// 描画用の状態データ生成
			this.TopScreen.Draw(drawStatus);			// トップ画面から描画開始
			this.DrawCursor(drawStatus.CursorAlpha);	// マウスカーソルの描画
			MainWindow.DrawStatus = drawStatus;			// 描画状態を次のフレームまで一時コピー

			DebugTools.DrawNowStatus();					// マウス情報・FPS・バージョン情報等の描画 (デバッグ用)

			DrawManager.EndScene();						// シーンレンダリング終了を描画管理クラスへ通知
		}

	}
}
