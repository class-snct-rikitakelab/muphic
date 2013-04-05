using System;
using muphic.MakeStory.Dialog;

namespace muphic.MakeStory
{
	/// <summary>
	/// StoryMakeDialog の概要の説明です。
	/// </summary>
	public class StoryMakeDialog : Screen
	{
		public MakeStoryScreen parent;
		public NoButton no;
		public YesButton yes;

		public StoryMakeDialog(MakeStoryScreen story)
		{
			parent = story;

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			no = new NoButton(this);
			yes = new YesButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 239, 287, @"image\MakeStory\dialog\newmake\haikei.png");
			muphic.DrawManager.Regist(no.ToString(), 575, 460, @"image\MakeStory\dialog\no_off.png", @"image\MakeStory\dialog\no_on.png");
			muphic.DrawManager.Regist(yes.ToString(), 375, 460, @"image\MakeStory\dialog\yes_off.png", @"image\MakeStory\\dialog\yes_on.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(no);
			BaseList.Add(yes);
		}

		public override void Draw()
		{
			base.Draw ();
		}

	}
}
