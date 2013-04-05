using System;

namespace muphic.MakeStory.Dialog.select
{
	/// <summary>
	/// BackButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class BackButton : Base
	{
		StorySelectDialog parent;
		public BackButton(StorySelectDialog story)
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
