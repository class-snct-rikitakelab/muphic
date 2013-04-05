using System;

namespace muphic.LinkMake.ScoreParts
{
	/// <summary>
	/// LeftScrollButton �̊T�v�̐����ł��B
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
