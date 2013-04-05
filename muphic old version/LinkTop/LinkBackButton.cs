using System;

namespace muphic
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LinkBackButton : Base
	{
		LinkTop parent;
		public LinkBackButton(LinkTop dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//parent.parent.parent.Screen = parent.parent;
			parent.parent.ScreenMode = muphic.ScreenMode.TopScreen;
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