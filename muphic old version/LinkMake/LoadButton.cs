using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// LoadButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LoadButton : Base
	{
		LinkMakeScreen parent;
		public LoadButton(LinkMakeScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.LinkMakeScreenMode = muphic.LinkMakeScreenMode.LoadDialog;
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
