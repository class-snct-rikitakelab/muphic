using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// ScoreButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class ScoreButton : Base
	{
		public LinkMakeScreen parent;
		public ScoreButton(LinkMakeScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.LinkMakeScreenMode = muphic.LinkMakeScreenMode.ScoreScreen;
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
