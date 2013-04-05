using System;

namespace muphic.LinkMake.Dialog.Save
{
	/// <summary>
	/// BackButtton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class BackButton : Base
	{
		public LinkSaveDialog parent;

		public BackButton(LinkSaveDialog dialog)
		{
			this.parent = dialog;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.LinkMakeScreenMode = muphic.LinkMakeScreenMode.LinkMakeScreen;
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
