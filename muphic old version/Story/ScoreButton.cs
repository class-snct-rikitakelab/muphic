using System;

namespace muphic.Story
{
	/// <summary>
	/// ScoreButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class ScoreButton : Base
	{
		public StoryScreen parent;
		public ScoreButton(StoryScreen story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.StoryScreenMode = muphic.StoryScreenMode.ScoreScreen;
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
