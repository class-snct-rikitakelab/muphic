using System;

namespace muphic.Tutorial.TutorialEndDialogParts
{
	/// <summary>
	/// YesButton の概要の説明です。
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
