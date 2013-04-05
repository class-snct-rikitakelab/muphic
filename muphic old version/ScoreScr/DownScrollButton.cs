using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// DownScreenButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class DownScrollButton : Base
	{
		public ScoreScreen parent;

		public DownScrollButton(ScoreScreen score)
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
			parent.scoremain.DownScroll();
		}

	}
}
