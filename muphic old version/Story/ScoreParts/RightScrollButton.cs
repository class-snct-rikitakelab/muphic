using System;

namespace muphic.Story.ScoreParts
{
	/// <summary>
	/// RightScrollButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class RightScrollButton : Base
	{
		public Score parent;
		public RightScrollButton(Score score)
		{
			parent = score;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.RightScroll();
		}

	}
}
