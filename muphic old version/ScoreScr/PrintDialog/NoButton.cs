using System;

namespace muphic.ScoreScr.PrintDialog
{
	/// <summary>
	/// NoButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class NoButton : Base
	{
		public ScorePrintDialog parent;
		
		public NoButton(ScorePrintDialog dialog)
		{
			this.parent = dialog;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
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
