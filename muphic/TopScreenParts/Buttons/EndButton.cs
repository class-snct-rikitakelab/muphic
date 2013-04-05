
namespace Muphic.TopScreenParts.Buttons
{
	/// <summary>
	/// トップ画面の muphic 終了ボタンクラス
	/// </summary>
	public class EndButton : Common.Button
	{
		/// <summary>
		/// 親にあたるトップ画面クラス
		/// </summary>
		public TopScreen Parent { get; set; }


		/// <summary>
		/// muphic 終了ボタンクラスの初期化を行う。
		/// </summary>
		/// <param name="topScreen">親にあたるトップ画面クラス。</param>
		public EndButton(TopScreen topScreen)
		{
			this.Parent = topScreen;		// 親スクリーンの設定
			this.CompositionSetting();		// ボタン構成設定
		}

		/// <summary>
		/// muphic 終了ボタンの新しいインスタンスを初期化する。
		/// </summary>
		protected void CompositionSetting()
		{
			// ==============================
			// ボタンのテクスチャと座標の登録
			// ==============================
			System.Drawing.Point location = Settings.PartsLocation.Default.TopScr_EndBtn;
			this.SetLabelTexture(this.ToString(), location, "IMAGE_TOPSCR_BUTTON_END");
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_TOP03_BLUE", "IMAGE_BUTTON_TOP03_ON");
		}

		/// <summary>
		/// muphic 終了ボタンが押された場合の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (Manager.KeyboardInputManager.IsProtection) return;

			MainWindow.Running = false;			// メインループ終了のお知らせ
			
			Manager.LogFileManager.WriteLine(	// ログにメッセージ出力
				Properties.Resources.Msg_MainWindow_ButtonClick,
				Properties.Resources.Msg_TopScr_Click_EndButton
			);

			// プレイヤー名のバックアップを削除する
			this.Parent.DeletePlayerNameBackupFile();
		}

	}
}
