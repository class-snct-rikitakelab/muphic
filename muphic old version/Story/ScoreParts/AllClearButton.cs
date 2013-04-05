using System;

namespace muphic.Story.ScoreParts
{
	/// <summary>
	/// AllClearButton の概要の説明です。
	/// </summary>
	public class AllClearButton : Base
	{
		Score parent;
		public AllClearButton(Score one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.StoryScreenMode = muphic.StoryScreenMode.AllClearDialog;
			//parent.parent.score.Animals.AllDelete();
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
