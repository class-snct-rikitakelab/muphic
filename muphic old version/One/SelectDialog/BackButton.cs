using System;

namespace muphic.One.SelectDialog
{
	/// <summary>
	/// BackButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class BackButton : Base
	{
		ScoreSelectDialog parent;
		
		public BackButton(ScoreSelectDialog story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//parent.parent.isSelectDialog = false;
			this.parent.parent.OneScreenMode = muphic.OneScreenMode.OneScreen;
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
