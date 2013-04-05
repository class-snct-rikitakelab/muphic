using System;

namespace muphic.One
{
	/// <summary>
	/// SaveButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class SaveButton : Base
	{
		public OneScreen parent;
		
		public SaveButton(OneScreen one)
		{
			this.parent = one;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//this.parent.isSaveDialog = true;
			this.parent.OneScreenMode = muphic.OneScreenMode.FileSaveDialog;
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
