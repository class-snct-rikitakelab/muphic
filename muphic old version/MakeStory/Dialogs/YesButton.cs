using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// YesButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class YesButton : Base
	{
		int parent;
		public StoryMakeDialog make;
		public YesButton(StoryMakeDialog dialog)
		{
			make = dialog;
			parent = 1;
		}
		public StorySaveDialog save;
		public YesButton(StorySaveDialog dialog)
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
					make.parent.NowPage = 0;
					make.parent.PictureStory = new PictStory();
					make.parent.thumb.init();
					make.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
					break;
				case 2:
					StoryFileWriter sfw = new StoryFileWriter(save.parent.PictureStory);
					sfw.Write(save.parent.PictureStory.Title);
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
