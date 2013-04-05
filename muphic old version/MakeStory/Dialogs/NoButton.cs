using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// NoButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class NoButton : Base
	{
		int parent;
		public StoryMakeDialog make;
		public NoButton(StoryMakeDialog dialog)
		{
			make = dialog;
			parent = 1;
		}
		public StorySaveDialog save;
		public NoButton(StorySaveDialog dialog)
		{
			save = dialog;
			parent = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			switch(parent)
			{
				case 1:
					make.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
					break;
				case 2:
					save.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
					break;
			}
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
