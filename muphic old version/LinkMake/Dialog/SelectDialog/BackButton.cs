using System;

namespace muphic.LinkMake.Dialog.Select
{
	/// <summary>
	/// BackButton の概要の説明です。
	/// </summary>
	public class BackButton : Base
	{
		LinkSelectDialog parent;
		public BackButton(LinkSelectDialog story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.LinkMakeScreenMode = muphic.LinkMakeScreenMode.LinkMakeScreen;
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
