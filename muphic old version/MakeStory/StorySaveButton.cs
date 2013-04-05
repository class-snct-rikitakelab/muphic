using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// StorySaveButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class StorySaveButton : Base
	{
		MakeStoryScreen parent;
		public StorySaveButton(MakeStoryScreen story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.SaveDialog;
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
