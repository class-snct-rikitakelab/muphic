using System;

namespace muphic.One.SaveDialog
{
	/// <summary>
	/// YesButtton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class YesButton : Base
	{
		public ScoreSaveDialog parent;

		public YesButton(ScoreSaveDialog dialog)
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
