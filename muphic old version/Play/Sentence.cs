using System;

namespace muphic.MakeStory.Play
{
	/// <summary>
	/// Sentence の概要の説明です。
	/// </summary>
	public class Sentence : Base
	{
		PlayScreen parent;
		public Sentence(PlayScreen s)
		{
			parent = s;
		}
//
//		public override void Click(System.Drawing.Point p)
//		{
//			base.Click (p);
//			//parent.parent.ScreenMode = muphic.ScreenMode.TopScreen;
//			parent.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
//		}
//
//		public override void MouseEnter()
//		{
//			base.MouseEnter ();
//			this.State = 1;
//		}
//
//		public override void MouseLeave()
//		{
//			base.MouseLeave ();
//			this.State = 0;
//		}
	}
}
