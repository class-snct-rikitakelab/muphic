using System;

namespace muphic.LinkMake.Dialog.Select
{
	/// <summary>
	/// UpperScrollButton �̊T�v�̐����ł��B
	/// </summary>
	public class UpperScrollButton : Base
	{
		public LinkSelectDialog parent;
		public UpperScrollButton(LinkSelectDialog link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.sbs.Upper();
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
