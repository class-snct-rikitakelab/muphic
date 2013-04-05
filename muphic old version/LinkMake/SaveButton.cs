using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// SaveButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class SaveButton : Base
	{
		LinkMakeScreen parent;
		public SaveButton(LinkMakeScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.LinkMakeScreenMode = muphic.LinkMakeScreenMode.SaveDialog;
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
