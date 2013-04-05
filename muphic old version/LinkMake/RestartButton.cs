using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// RestartButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class RestartButton : Base
	{
		public LinkMakeScreen parent;
		public RestartButton(LinkMakeScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.startstop.State = 2;
			parent.score.PlayFirst();
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
