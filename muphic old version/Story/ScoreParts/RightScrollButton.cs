using System;

namespace muphic.Story.ScoreParts
{
	/// <summary>
	/// RightScrollButton の概要の説明です。
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
