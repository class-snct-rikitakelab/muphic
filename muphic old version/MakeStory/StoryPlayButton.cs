using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// StoryPlayButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class StoryPlayButton : Base
	{
		MakeStoryScreen parent;
		public StoryPlayButton(MakeStoryScreen story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.PlayStoryScreen;
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
