using System;

namespace muphic.Titlemode
{

	/// <summary>
	/// ClearButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class DecisionButton : Base
	{
		public TitleScreen parent;

		public DecisionButton(TitleScreen ts)
		{
			parent = ts;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
            switch (parent.ParentNum)
            {
                case 1:
					if (parent.TargetMode == TargetMode.Title)
					{
						parent.Makescr.PictureStory.Title = parent.Text;
					}
					else if (parent.TargetMode == TargetMode.Sentence)
					{
						parent.Makescr.PictureStory.Slide[parent.Makescr.NowPage].Sentence = parent.Text;
					}
					parent.Makescr.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
					break;
				case 3:
					this.parent.Scorescr.sadialog.titlename = parent.Text;
					this.parent.Scorescr.isTitleScreen = false;
					break;
				case 4:
					this.parent.Linksave.titlename = parent.Text;
					this.parent.Linksave.Screen = null;
					break;
				case 5:
					this.parent.Onescr.ScoreTitle = parent.Text;
					//this.parent.Onescr.sadialog.checkScoreName();
					this.parent.Onescr.OneScreenMode = muphic.OneScreenMode.OneScreen;
					break;
                default:
                    break;
            }
            //System.Diagnostics.Debug.WriteLine(true,"Decision:"+parent.parent.title);
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
