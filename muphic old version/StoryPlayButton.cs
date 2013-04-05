using System;

namespace muphic.Story
{
	/// <summary>
	/// StoryPlayButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class StoryPlayButton : Base
	{
		StoryScreen parent;
		public StoryPlayButton(StoryScreen story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.StoryScreenMode = muphic.StoryScreenMode.PlayStoryScreen;
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
