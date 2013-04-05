﻿
namespace Muphic.NameInputScreenParts.Buttons
{
	/// <summary>
	/// プレイヤー1 の性別選択男児ボタンクラス。
	/// </summary>
	public class Player1BoyButton : Common.Button
	{
		/// <summary>
		/// 親にあたるプレイヤー名入力画面。
		/// </summary>
		public NameInputScreen Parent { get; set; }

		/// <summary>
		/// プレイヤー1 の性別選択男児ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるプレイヤー名入力画面。</param>
		public Player1BoyButton(NameInputScreen parent)
		{
			this.Parent = parent;

			this.SetLabelTexture(this.ToString(), Locations.Player1BoyButton, "IMAGE_ENTITLESCR_BOYBTN");
			this.SetBgTexture(this.ToString(), Locations.Player1BoyButton, "IMAGE_BUTTON_BOX3_ON", "IMAGE_BUTTON_BOX3_AQUA", "IMAGE_BUTTON_BOX3_AQUA");
		}


		/// <summary>
		/// プレイヤー1 の性別選択男児ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Player1Gender = Muphic.PlayerWorks.AuthorGender.Boy;
			this.Parent.SetStateOfPlayerGenderSelectButtons();
		}

		/// <summary>
		/// プレイヤー1 の性別選択男児ボタンを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);
		}
	}
}
