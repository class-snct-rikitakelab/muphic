﻿
namespace Muphic.ScoreScreenParts.Buttons
{
	/// <summary>
	/// 楽譜画面でネコの楽譜表示を指定するボタン。
	/// </summary>
	public class CatButton : Muphic.Common.Button
	{
		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		private readonly ScoreScreen __parent;

		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		public ScoreScreen Parent
		{
			get { return this.__parent; }
		}


		/// <summary>
		/// ネコの楽譜表示ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public CatButton(ScoreScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Locations.CatButton, "IMAGE_BUTTON_ELLIPSE_BLUE", "IMAGE_BUTTON_ELLIPSE_ON");
			this.SetLabelTexture(this.ToString(), Locations.CatButton, "IMAGE_SCORESCR_CATBTN");
		}


		/// <summary>
		/// ネコの楽譜表示ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を示す Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScoreMode = ScoreScreenScoreMode.Cat;
		}
	}
}
