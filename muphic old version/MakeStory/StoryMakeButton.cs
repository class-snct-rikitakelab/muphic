using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// StoryMakeButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class StoryMakeButton : Base
	{
		MakeStoryScreen parent;
		public StoryMakeButton(MakeStoryScreen story)
		{
			parent = story;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeDialog;
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
