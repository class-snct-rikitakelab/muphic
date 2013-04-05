using System;
using System.Drawing;
using muphic.MakeStory.PrintDialog;

namespace muphic.MakeStory
{
	/// <summary>
	/// ScorePrintDialog の概要の説明です。
	/// </summary>
	public class StoryPrintDialog : Screen
	{
		public MakeStoryScreen parent;
		
		public muphic.MakeStory.PrintDialog.BackButton back;
		public PrintMemorialButton memorial;
		public PrintStoryButton story;
		
		public StoryPrintDialog(MakeStoryScreen msscreen)
		{
			this.parent = msscreen;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			back = new muphic.MakeStory.PrintDialog.BackButton(this);
			memorial = new PrintMemorialButton(this);
			story = new PrintStoryButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 160, "image\\MakeStory\\print\\printdialog.png");
			muphic.DrawManager.Regist(back.ToString(), 680, 560, "image\\MakeStory\\print\\back_off.png", "image\\MakeStory\\print\\back_on.png");
			muphic.DrawManager.Regist(memorial.ToString(), 322, 300, "image\\MakeStory\\print\\memorial_off.png", "image\\MakeStory\\print\\memorial_on.png");
			muphic.DrawManager.Regist(story.ToString(), 582, 300, "image\\MakeStory\\print\\kamishibai_off.png", "image\\MakeStory\\print\\kamishibai_on.png");
			DrawManager.Regist("Preview_Memorial", 282, 370, "image\\MakeStory\\print\\preview_memorial.png");
			DrawManager.Regist("Preview_Story", 542, 370, "image\\MakeStory\\print\\preview_story.png");
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(back);
			BaseList.Add(memorial);
			BaseList.Add(story);
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}
		
		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}
		
		public override void Draw()
		{
			base.Draw ();
			Rectangle r = PointManager.Get(parent.wind.ToString());
			DrawManager.Draw("Preview_Memorial");
			DrawManager.Draw("Preview_Story");
			DrawManager.Change(new Rectangle(r.X-100, r.Y-100, r.Width+200, r.Height+200), new Rectangle(282, 370, 200, 150));
			parent.wind.PreviewMemorial();
			DrawManager.Change(r, new Rectangle(542, 370, 200, 150));
			parent.wind.PreviewStory();
		}
	}
}
