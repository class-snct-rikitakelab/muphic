using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// WriteButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class WriteButton : Base
	{
		public ScoreScreen parent;

		public WriteButton(ScoreScreen score)
		{
			this.parent = score;
			if(this.parent.ParentScreenMode == muphic.ScreenMode.LinkScreen) this.Visible = false;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.isSaveDialog = true;
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
