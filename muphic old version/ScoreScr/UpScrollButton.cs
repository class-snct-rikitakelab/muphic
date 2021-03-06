using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// UpScreenButton の概要の説明です。
	/// </summary>
	public class UpScrollButton : Base
	{
		public ScoreScreen parent;

		public UpScrollButton(ScoreScreen score)
		{
			parent = score;
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			//this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.scoremain.UpScroll();
		}

	}
}
