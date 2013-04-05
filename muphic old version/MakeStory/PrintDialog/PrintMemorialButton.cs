using System;

namespace muphic.MakeStory.PrintDialog
{
	/// <summary>
	/// PrintMemorialButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class PrintMemorialButton : Base
	{
		public StoryPrintDialog parent;
		
		public PrintMemorialButton(StoryPrintDialog dialog)
		{
			this.parent = dialog;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// bbbbbbbbbbbbbbbbbbbbbbbbbbb
			// b‚³‚ A‚±‚±‚É‘‚«‚½‚Ü‚¦Iˆóüƒƒ\ƒbƒh‚ÌŒÄ‚Ño‚µ‚ğIb
			// «««««««««««««««««««««««««««
			parent.parent.wind.PrintMemorial();
			// ªªªªªªªªªªªªªªªªªªªªªªªªªªª
			// b‚³‚ A‚±‚±‚É‘‚«‚½‚Ü‚¦Iˆóüƒƒ\ƒbƒh‚ÌŒÄ‚Ño‚µ‚ğIb
			// bbbbbbbbbbbbbbbbbbbbbbbbbbb
			
			this.parent.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
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
