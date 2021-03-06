using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// SaveButton の概要の説明です。
	/// </summary>
	public class SaveButton : Base
	{
		LinkMakeScreen parent;
		public SaveButton(LinkMakeScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.LinkMakeScreenMode = muphic.LinkMakeScreenMode.SaveDialog;
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
