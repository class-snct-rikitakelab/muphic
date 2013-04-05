using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// StoryPrintButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class StoryPrintButton : Base
	{
		MakeStoryScreen parent;
		public StoryPrintButton(MakeStoryScreen story)
		{
			parent = story;

			if (!muphic.Common.CommonSettings.getEnablePrint()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.PrintDialog;
		}

		public override void MouseEnter()
		{
			if (this.State == 2) return;
			base.MouseEnter();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			if (this.State == 2) return;
			base.MouseLeave();
			this.State = 0;
		}
	}
}
