using System;

namespace muphic
{
	/// <summary>
	/// LinkMakeButton の概要の説明です。
	/// </summary>
	public class LinkMakeButton : Base
	{
		LinkTop parent;
		public LinkMakeButton(LinkTop dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//parent.parent.parent.Screen = parent.parent;
			parent.parent.Screen = new LinkMakeScreen(parent.parent);
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.State = 0;
		}
	}
}