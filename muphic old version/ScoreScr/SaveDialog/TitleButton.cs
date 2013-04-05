using System;

namespace muphic.ScoreScr.SaveDialog
{
	/// <summary>
	/// TitleButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class TitleButton : Base
	{
		public ScoreSaveDialog parent;

		public TitleButton(ScoreSaveDialog dialog)
		{
			this.parent = dialog;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.isTitleScreen = true;
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
