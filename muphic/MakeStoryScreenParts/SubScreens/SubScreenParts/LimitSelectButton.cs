using System.Drawing;
using Muphic.Manager;

namespace Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts
{
	/// <summary>
	/// 物語作曲画面で、使用する音階制限の種類を選択するボタン。
	/// </summary>
	public class LimitSelectButton : Common.Button
	{
		/// <summary>
		/// 親にあたる音階制限選択領域。
		/// </summary>
		private readonly LimitSelectArea __parent;

		/// <summary>
		/// 親にあたる音階制限選択領域を取得する。
		/// </summary>
		public LimitSelectArea Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// このボタンを押すと選択される制限の種類を示す整数。
		/// </summary>
		private readonly int __number;

		/// <summary>
		/// このボタンを押すと選択される制限の種類を示す整数を取得する。
		/// </summary>
		public int Number
		{
			get { return this.__number; }
		}

		/// <summary>
		/// 音階制限選択ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる音階制限選択領域。</param>
		/// <param name="number">制限の種類を示す整数。</param>
		public LimitSelectButton(LimitSelectArea parent, int number)
		{
			this.__parent = parent;
			this.__number = number;

			// ボタン表示位置の算出
			Point location = Locations.LimitSelectButtonLocations[number];

			// ボタンの背景背景テクスチャを登録
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_MINIBOX_GREEN", "IMAGE_BUTTON_MINIBOX_ON", "IMAGE_BUTTON_MINIBOX_ORANGE");

			// ボタンのラベルテクスチャを登録 位置はボタンの中央になるように調整 ラベルはそのボタンの文字
			location.X += (TextureFileManager.GetRectangle("IMAGE_BUTTON_MINIBOX_GREEN").Width - StringManager.StringSize.Width) / 2 + 1;
			location.Y += (TextureFileManager.GetRectangle("IMAGE_BUTTON_MINIBOX_GREEN").Height - StringManager.StringSize.Height) / 2;
			this.SetLabelTexture(this.ToString(), location, StringManager.ConvertHalfsizeToFullsize((number + 1).ToString()));
		}


		/// <summary>
		/// 音階制限選択ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus"></param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);
			this.Parent.LimitMode = this.Number;
		}


		/// <summary>
		/// 現在の System.Object に、このボタンが担当する制限の番号を付加した文字列を返す。
		/// </summary>
		/// <returns>現在の System.Object に、このボタンが担当する制限の番号を付加した文字列。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Number.ToString();
		}
	}
}
