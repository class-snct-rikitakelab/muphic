using System;

namespace muphic.One
{
	/// <summary>
	/// TitleForm ‚ÌŠT—v‚Ìà–¾‚Å‚·B
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
