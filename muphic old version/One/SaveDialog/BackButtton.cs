using System;

namespace muphic.One.SaveDialog
{
	/// <summary>
	/// BackButtton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class BackButton : Base
	{
		public ScoreSaveDialog parent;

		public BackButton(ScoreSaveDialog dialog)
		{
			this.parent = dialog;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//parent.parent.isSaveDialog = false;
			this.parent.parent.OneScreenMode = muphic.OneScreenMode.OneScreen;
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
