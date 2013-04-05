using System;

namespace muphic.Story
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class BackButton : Base
	{
		public StoryScreen parent;
		public BackButton(StoryScreen story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
