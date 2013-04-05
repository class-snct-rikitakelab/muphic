using System;

namespace muphic.One.ScoreParts
{
	/// <summary>
	/// RightScrollButton �̊T�v�̐����ł��B
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