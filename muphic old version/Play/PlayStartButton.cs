using System;

namespace muphic.MakeStory.Play
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class PlayStartButton : Base
	{
		PlayScreen parent;
		public PlayStartButton(PlayScreen s)
		{
			parent = s;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//parent.parent.ScreenMode = muphic.ScreenMode.TopScreen;
			parent.PlayFlag = true;
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
