using System;

namespace muphic.One
{
	/// <summary>
	/// TitleForm �̊T�v�̐����ł��B
	/// </summary>
	public class TitleForm : Base
	{
		public OneScreen parent;
		
		public TitleForm(OneScreen one)
		{
			this.parent = one;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
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
