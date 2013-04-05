using System;

namespace muphic.Link
{
	/// <summary>
	/// RightScrollButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class RightScrollButton : Base
	{
		public LinkScreen parent;
		public RightScrollButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.score.RightScroll();
		}

	}
}
