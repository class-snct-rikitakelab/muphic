using System;

namespace muphic.Link
{
	/// <summary>
	/// RightScrollButton の概要の説明です。
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
