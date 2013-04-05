using System;

namespace muphic.One.ScoreParts
{
	/// <summary>
	/// SharpButton �̊T�v�̐����ł��B
	/// </summary>
	public class SharpButton : Base
	{
		Score parent;
		public SharpButton(Score one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
