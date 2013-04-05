using System;

namespace muphic.MakeStory.PrintDialog
{
	/// <summary>
	/// BackButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class BackButton : Base
	{
		public StoryPrintDialog parent;
		
		public BackButton(StoryPrintDialog dialog)
		{
			this.parent = dialog;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			this.parent.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
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
