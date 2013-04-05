using System;

namespace muphic.One
{
	/// <summary>
	/// titlebutton �̊T�v�̐����ł��B
	/// </summary>
	public class TitleButton : Base
	{
		public OneScreen parent;
		
		public TitleButton(OneScreen one)
		{
			this.parent = one;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			this.parent.OneScreenMode = muphic.OneScreenMode.TitleScreen;
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
