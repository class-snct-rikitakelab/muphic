using System;

namespace muphic.Story.ScoreParts
{
	/// <summary>
	/// LeftScrollButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LeftScrollButton : Base
	{
		public Score parent;
		public LeftScrollButton(Score score)
		{
			parent = score;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.LeftScroll();
		}

	}
}
