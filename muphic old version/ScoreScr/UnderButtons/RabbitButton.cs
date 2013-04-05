using System;

namespace muphic.ScoreScr.UnderButtons
{
	/// <summary>
	/// RabbitButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class RabbitButton : Base
	{
		public ScoreButtons parent;

		public RabbitButton(ScoreButtons sb)
		{
			this.parent = sb;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);

			parent.NowClick = muphic.ScoreScr.AnimalScoreMode.Rabbit;
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
			if(parent.NowClick != muphic.ScoreScr.AnimalScoreMode.Rabbit)
			{
				this.State = 0;
			}
		}
	}
}
