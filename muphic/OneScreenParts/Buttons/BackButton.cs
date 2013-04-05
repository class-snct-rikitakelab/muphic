
namespace Muphic.OneScreenParts.Buttons
{
	/// <summary>
	/// ひとりでおんがく画面の"もどる"ボタンクラス
	/// </summary>
	public class BackButton : Common.Button
	{
		/// <summary>
		/// 親にあたるひとりでおんがく画面
		/// </summary>
		public OneScreen Parent { get; private set; }


		/// <summary>
		/// ひとりでおんがく画面の"もどる"ボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="oneScreen">親にあたるひとりでおんがく画面。</param>
		public BackButton(OneScreen oneScreen)
		{
			this.Parent = oneScreen;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.OneScr_BackBtn, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.OneScr_BackBtn, "IMAGE_COMPOSITIONSCR_BACKBTN");
		}


		/// <summary>
		/// ひとりでおんがく画面の"もどる"ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			// 児童モードで、シフトキーフラグも立ってなければクリック無効
			if (Manager.KeyboardInputManager.IsProtection) return;

			// トップ画面の表示画面をトップ画面自身に戻す
			this.Parent.Parent.ScreenMode = TopScreenMode.TopScreen;
		}
	}
}
