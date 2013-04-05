using System;

namespace muphic.MakeStory.Play
{
	/// <summary>
	/// Sentence ‚ÌŠT—v‚Ìà–¾‚Å‚·B
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
