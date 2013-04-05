using System;

namespace muphic.ScoreScr.PrintDialog
{
	/// <summary>
	/// YesButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class YesButton : Base
	{
		public ScorePrintDialog parent;
		
		public YesButton(ScorePrintDialog dialog)
		{
			this.parent = dialog;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			this.parent.parent.scoremain.Print();
			this.parent.parent.isPrintDialog = false;
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
