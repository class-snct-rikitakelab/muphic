using System;

namespace muphic.Link.Dialog.Select
{
	/// <summary>
	/// UpperScrollButton の概要の説明です。
	/// </summary>
	public class UpperScrollButton : Base
	{
		public SelectDialog parent;
		public UpperScrollButton(SelectDialog link)
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
