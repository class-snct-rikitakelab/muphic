using System;
using System.Collections.Generic;
using System.Text;

namespace Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts
{
	/// <summary>
	/// 物語再生画面の"えをつくる"ボタンクラス。
	/// <para>クリックされると、物語再生画面から物語作成画面へ遷移する。</para>
	/// </summary>
	public class BackFromPlayStoryButton : Common.Button
	{
		/// <summary>
		/// "えをつくる"ボタンの親となる物語再生画面。
		/// </summary>
		private readonly PlayStoryScreen __parent;

		/// <summary>
		/// "えをつくる"ボタンの親となる物語再生画面を取得する。
		/// </summary>
		public PlayStoryScreen Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// 物語再生画面の"えをつくる"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">このボタンの親となる物語再生画面。</param>
		public BackFromPlayStoryButton(PlayStoryScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.PlayStoryScr_BackBtn, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.PlayStoryScr_BackBtn, "IMAGE_PLAYSTORYSCR_BACKBTN");
		}


		/// <summary>
		/// 物語再生画面の"えをつくる"ボタンがクリックされると実行される。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
		}

	}
}
