using System;

namespace muphic.Common
{
	/// <summary>
	/// AllClearYesButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class AllClearYesButton : Base
	{
		AllClearDialog parent;
		public AllClearYesButton(AllClearDialog dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);

			switch (parent.Mode)
			{
				case ScreenMode.LinkScreen:
					parent.parent_link.score.AnimalList.Clear();
					for (int i = 0; i < parent.parent_link.score.putFlag.Length; i++)
					{
						parent.parent_link.score.putFlag[i] = false;
					}
					for (int i = 0; i < 10; i++)
					{
						for (int j = 0; j < 4; j++)
						{
							parent.parent_link.score.ribbon[i, j] = false;
						}
					}
					parent.parent_link.LinkScreenMode = muphic.LinkScreenMode.LinkScreen;
					break;

				case ScreenMode.LinkMakeScreen:
					parent.parent_linkmake.score.Animals.AllDelete();
					parent.parent_linkmake.LinkMakeScreenMode = muphic.LinkMakeScreenMode.LinkMakeScreen;
					break;

				case ScreenMode.OneScreen:
					parent.parent_one.score.Animals.AllDelete();
					parent.parent_one.isAllClearDialog = false;
					parent.parent_one.ScoreTitle = "";
					parent.parent_one.OneScreenMode = muphic.OneScreenMode.OneScreen;
					break;

				case ScreenMode.StoryScreen:
					parent.parent_story.score.Animals.AllDelete();
					parent.parent_story.StoryScreenMode = muphic.StoryScreenMode.StoryScreen;
					break;

				case ScreenMode.MakeStoryScreen:
					//parent.parent_makestory.PictureStory.Slide[parent.parent_makestory.NowPage] = new muphic.MakeStory.Slide();
					parent.parent_makestory.PictureStory.Slide[parent.parent_makestory.NowPage].ObjList = new System.Collections.ArrayList();
					parent.parent_makestory.PictureStory.Slide[parent.parent_makestory.NowPage].haikei = new muphic.MakeStory.Obj(0);
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