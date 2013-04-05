using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class BackButton : Base
	{
		LinkMakeScreen parent;
		public BackButton(LinkMakeScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.ScreenMode = muphic.ScreenMode.TopScreen;
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
