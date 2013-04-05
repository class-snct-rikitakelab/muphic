using System;
using muphic.Titlemode;

namespace muphic.LinkMake.Dialog.Save
{
	/// <summary>
	/// TitleButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class TitleButton : Base
	{
		public LinkSaveDialog parent;

		public TitleButton(LinkSaveDialog dialog)
		{
			this.parent = dialog;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.Screen = new TitleScreen(parent);
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
