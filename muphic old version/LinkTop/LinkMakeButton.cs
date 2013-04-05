using System;

namespace muphic
{
	/// <summary>
	/// LinkMakeButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LinkMakeButton : Base
	{
		LinkTop parent;
		public LinkMakeButton(LinkTop dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//parent.parent.parent.Screen = parent.parent;
			parent.parent.Screen = new LinkMakeScreen(parent.parent);
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.State = 0;
		}
	}
}