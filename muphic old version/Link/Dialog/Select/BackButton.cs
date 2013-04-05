using System;

namespace muphic.Link.Dialog.Select
{
	/// <summary>
	/// BackButton �̊T�v�̐����ł��B
	/// </summary>
	public class BackButton : Base
	{
		SelectDialog parent;
		public BackButton(SelectDialog story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.LinkScreenMode = muphic.LinkScreenMode.LinkScreen;
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
