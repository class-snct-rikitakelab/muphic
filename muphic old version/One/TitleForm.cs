using System;

namespace muphic.One
{
	/// <summary>
	/// TitleForm の概要の説明です。
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
