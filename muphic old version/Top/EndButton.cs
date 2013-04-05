using System;
using System.Windows.Forms;

namespace muphic.Top
{
	/// <summary>
	/// EndButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class EndButton : Base
	{
		public TopScreen parent;

		public EndButton(TopScreen ts)
		{
			parent = ts;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			Application.Exit();
		}

		public override void MouseEnter()
		{
			if (this.State == 2) return;
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			if (this.State == 2) return;
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
