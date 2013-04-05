using System;

namespace muphic.One
{
	/// <summary>
	/// ScoreButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class ScoreButton : Base
	{
		public OneScreen parent;
		public ScoreButton(OneScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.OneScreenMode = muphic.OneScreenMode.ScoreScreen;
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
