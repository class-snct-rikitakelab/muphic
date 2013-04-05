using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScorePrint ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class PrintButton : Base
	{
		public ScoreScreen parent;

		public PrintButton(ScoreScreen screen)
		{
			this.parent = screen;
			if(this.parent.ParentScreenMode == muphic.ScreenMode.LinkScreen) this.Visible = false;
			if (!muphic.Common.CommonSettings.getEnablePrint()) this.Visible = false;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.isPrintDialog = true;
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
