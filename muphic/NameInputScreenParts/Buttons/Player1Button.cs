using Muphic.Common;

namespace Muphic.NameInputScreenParts.Buttons
{
	/// <summary>
	/// プレイヤー1 の名前入力ボタン。
	/// </summary>
	public class Player1Button : Button
	{
		/// <summary>
		/// 親にあたるプレイヤー名入力画面。
		/// </summary>
		public NameInputScreen Parent { get; private set; }

		/// <summary>
		/// プレイヤー1 名前入力ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるプレイヤー名入力画面。</param>
		public Player1Button(NameInputScreen parent)
		{
			this.Parent = parent;

			this.SetLabelTexture(this.ToString(), Locations.Player1Button, "IMAGE_ENTITLESCR_PLAYER1BTN");
			this.SetBgTexture(this.ToString(), Locations.Player1Button, "IMAGE_BUTTON_BOX2_GREEN", "IMAGE_BUTTON_BOX2_ON", "IMAGE_BUTTON_BOX2_ORANGE");
		}


		/// <summary>
		/// プレイヤー1 名前入力ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.InputMode = NameInputScreen.NameInputMode.Player1;
		}
	}
}
