using System;

namespace muphic.ScoreScr.UnderButtons
{
	/// <summary>
	/// ManButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class VoiceButton : Base
	{
		public ScoreButtons parent;

		public VoiceButton(ScoreButtons sb)
		{
			this.parent = sb;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);

			parent.NowClick = muphic.ScoreScr.AnimalScoreMode.Voice;
			this.State = 1;
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.NowClick != muphic.ScoreScr.AnimalScoreMode.Voice)
			{
				this.State = 0;
			}
		}
	}
}
