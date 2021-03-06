using System;

namespace muphic.One.SelectDialog
{
	/// <summary>
	/// LowerScrollButton の概要の説明です。
	/// </summary>
	public class LowerScrollButton : Base
	{
		public ScoreSelectDialog parent;
		public LowerScrollButton(ScoreSelectDialog story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.sbs.Lower();
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
		}
	}
}
