using System;

namespace muphic.Common
{
	/// <summary>
	/// AllClearNoButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class AllClearNoButton : Base
	{
		AllClearDialog parent;
		public AllClearNoButton(AllClearDialog dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);

			switch (parent.Mode)
			{
				case ScreenMode.LinkScreen:
					parent.parent_link.LinkScreenMode = muphic.LinkScreenMode.LinkScreen;
					break;
				case ScreenMode.LinkMakeScreen:
					parent.parent_linkmake.LinkMakeScreenMode = muphic.LinkMakeScreenMode.LinkMakeScreen;
					break;
				case ScreenMode.OneScreen:
					parent.parent_one.isAllClearDialog = false;
					parent.parent_one.OneScreenMode = muphic.OneScreenMode.OneScreen;
					break;
				case ScreenMode.StoryScreen:
					parent.parent_story.StoryScreenMode = muphic.StoryScreenMode.StoryScreen;
					break;
				case ScreenMode.MakeStoryScreen:
					parent.parent_makestory.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
					break;
				default:
					break;
			}
			//parent.parent.LinkScreenMode = muphic.LinkScreenMode.LinkScreen;
			
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.State = 0;
		}
	}
}