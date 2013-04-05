using System;

namespace muphic.One
{
	/// <summary>
	/// SelectButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class SelectButton : Base
	{
		public OneScreen parent;
		
		public SelectButton(OneScreen one)
		{
			this.parent = one;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//this.parent.isSelectDialog = true;
			this.parent.OneScreenMode = muphic.OneScreenMode.FileSelectDialog;
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
