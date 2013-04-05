using System;

namespace muphic.Link
{
	/// <summary>
	/// LeftScrollButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LeftScrollButton : Base
	{
		public LinkScreen parent;
		public LeftScrollButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.score.LeftScroll();
		}

	}
}
