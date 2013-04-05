using System;

namespace muphic.MakeStory.Play
{
	/// <summary>
	/// BackButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class PlayBackButton : Base
	{
		PlayScreen parent;
		public PlayBackButton(PlayScreen s)
		{
			parent = s;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//parent.parent.ScreenMode = muphic.ScreenMode.TopScreen;
			parent.parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.MakeStoryScreen;
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
