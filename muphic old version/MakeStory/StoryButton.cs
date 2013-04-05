using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// ScoreButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class StoryButton : Base
	{
		public MakeStoryScreen parent;
		public StoryButton(MakeStoryScreen story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.StoryScreen;
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
