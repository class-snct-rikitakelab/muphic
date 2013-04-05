using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// BackButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class BackButton : Base
	{
		int parent;
		public StoryMakeDialog make;
		public BackButton(StoryMakeDialog dialog)
		{
			make = dialog;
			parent = 1;
		}
		public StorySaveDialog save;
		public BackButton(StorySaveDialog dialog)
		{
			save = dialog;
			parent = 2;
		}
		StorySelectDialog select;
		public BackButton(StorySelectDialog story)
		{
			select = story;
			parent = 3;
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
				case 3:
					select.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
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
