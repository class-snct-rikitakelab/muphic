using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// StorySelectButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class StorySelectButton : Base
	{
		MakeStoryScreen parent;
		public StorySelectButton(MakeStoryScreen story)
		{
			parent = story;
		}

		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.SelectDialog;
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
