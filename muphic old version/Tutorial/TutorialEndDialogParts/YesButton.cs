using System;

namespace muphic.Tutorial.TutorialEndDialogParts
{
	/// <summary>
	/// YesButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class YesButton : Base
	{
		public TutorialEndDialog parent;
		
		public YesButton(TutorialEndDialog dialog)
		{
			this.parent = dialog;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.TutorialEnd();
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
