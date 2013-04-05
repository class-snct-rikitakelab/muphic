using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// NewMakeButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class NewMakeButton : Base
	{
		LinkMakeScreen parent;
		public NewMakeButton(LinkMakeScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.score.Animals.AllDelete();
			//parent.score.Animals.AnimalList.Clear();
			//parent.parent.ScreenMode = muphic.ScreenMode.TopScreen;
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
