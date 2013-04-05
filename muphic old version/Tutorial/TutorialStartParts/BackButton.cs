using System;

namespace muphic.Tutorial.TutorialStartParts
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class BackButton : Base
	{
		public TutorialStart parent;
		
		public BackButton(TutorialStart ts)
		{
			this.parent = ts;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(this.parent.Chapter)
				this.parent.parnet.tutorialmain.TutorialEnd();
			else
				parent.parnet.parent.ScreenMode = muphic.ScreenMode.TopScreen;
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
