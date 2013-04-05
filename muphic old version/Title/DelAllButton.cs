using System;

namespace muphic.Titlemode
{
	/// <summary>
	/// ClearButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class DelAllButton : Base
	{
		public TitleScreen parent;

		public DelAllButton(TitleScreen ts)
		{
			parent = ts;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.Text = null;
			System.Diagnostics.Debug.WriteLine(true,"DelAll");
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
