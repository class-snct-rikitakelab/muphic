using System;
using muphic.Story;

namespace muphic.MakeStory
{
	/// <summary>
	/// AllClearButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class AllClearButton : Base
	{
		public MakeStoryScreen parent;

		public AllClearButton(MakeStoryScreen mss)
		{
			parent = mss;
		}

		public override void Click(System.Drawing.Point p)
		{
			if(parent.isClear)
				parent.cb.Reset();
			base.Click (p);
			parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.AllClearDialog;
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
