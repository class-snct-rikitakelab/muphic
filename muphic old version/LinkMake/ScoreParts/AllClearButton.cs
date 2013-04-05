using System;

namespace muphic.LinkMake.ScoreParts
{
	/// <summary>
	/// AllClearButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
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
			if (!parent.parent.score.isPlay)
			{
				parent.parent.LinkMakeScreenMode = muphic.LinkMakeScreenMode.AllClearDialog;
				//parent.parent.score.Animals.AllDelete();
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
