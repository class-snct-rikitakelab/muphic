using System;

namespace muphic.LinkMake.ScoreParts
{
	/// <summary>
	/// RightScrollButton の概要の説明です。
	/// </summary>
	public class RightScrollButton : Base
	{
		public Score parent;
		public RightScrollButton(Score one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.RightScroll();
		}

	}
}
