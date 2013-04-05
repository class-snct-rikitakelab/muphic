
namespace Muphic.NameInputScreenParts.Buttons
{
	/// <summary>
	/// プレイヤー2 の性別選択女児ボタンクラス。
	/// </summary>
	public class Player2GirlButton : Common.Button
	{
		/// <summary>
		/// 親にあたるプレイヤー名入力画面。
		/// </summary>
		public NameInputScreen Parent { get; set; }

		/// <summary>
		/// プレイヤー2 の性別選択女児ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるプレイヤー名入力画面。</param>
		public Player2GirlButton(NameInputScreen parent)
		{
			this.Parent = parent;

			this.SetLabelTexture(this.ToString(), Locations.Player2GirlButton, "IMAGE_ENTITLESCR_GIRLBTN");
			this.SetBgTexture(this.ToString(), Locations.Player2GirlButton, "IMAGE_BUTTON_BOX3_ON", "IMAGE_BUTTON_BOX3_PINK", "IMAGE_BUTTON_BOX3_PINK");
		}


		/// <summary>
		/// プレイヤー2 の性別選択女児ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Player2Gender = Muphic.PlayerWorks.AuthorGender.Girl;
			this.Parent.SetStateOfPlayerGenderSelectButtons();
		}

		/// <summary>
		/// プレイヤー2 の性別選択女児ボタンを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);
		}
	}
}
