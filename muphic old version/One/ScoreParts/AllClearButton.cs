using System;

namespace muphic.One.ScoreParts
{
	/// <summary>
	/// AllClearButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class AllClearButton : Base
	{
		Score parent;
		public AllClearButton(Score one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.isAllClearDialog = true;
			parent.parent.OneScreenMode = muphic.OneScreenMode.AllClearDialog;
			//parent.parent.score.Animals.AllDelete();
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
