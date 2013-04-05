using System;

namespace muphic.LinkMake.ScoreParts
{
	/// <summary>
	/// LeftScrollButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LeftScrollButton : Base
	{
		public Score parent;
		public LeftScrollButton(Score one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.LeftScroll();
		}

	}
}
