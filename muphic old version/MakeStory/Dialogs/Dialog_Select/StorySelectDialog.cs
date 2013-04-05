using System;
using muphic.MakeStory.Dialog;

namespace muphic.MakeStory
{
	/// <summary>
	/// StorySelectDialog の概要の説明です。
	/// </summary>
	public class StorySelectDialog : Screen
	{
		public MakeStoryScreen parent;
		public SelectButtons sbs;
		public UpperScrollButton upper;
		public LowerScrollButton lower;
		public muphic.MakeStory.Dialog.BackButton back;

		public StorySelectDialog(MakeStoryScreen story)
		{
			parent = story;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			sbs = new SelectButtons(this);
			upper = new UpperScrollButton(this);
			lower = new LowerScrollButton(this);
			back = new muphic.MakeStory.Dialog.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, @"image\MakeStory\dialog\select\haikei.png");
			muphic.DrawManager.Regist(sbs.ToString(), 285, 358, @"image\MakeStory\dialog\select\dialog_select.png");
			muphic.DrawManager.Regist(upper.ToString(), 640, 388, @"image\MakeStory\dialog\select\scroll_up.png");
			muphic.DrawManager.Regist(lower.ToString(), 642, 470, @"image\MakeStory\dialog\select\scroll_down.png");
			muphic.DrawManager.Regist(back.ToString(), 680, 435, @"image\MakeStory\dialog\select\back_off.png", @"image\MakeStory\dialog\select\back_on.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(sbs);
			BaseList.Add(upper);
			BaseList.Add(lower);
			BaseList.Add(back);
		}
		
		public override void Draw()
		{
			base.Draw ();
		}

	}
}
