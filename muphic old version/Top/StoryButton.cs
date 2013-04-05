using System;

namespace muphic.Top
{
	/// <summary>
	/// StoryButton の概要の説明です。
	/// </summary>
	public class StoryButton : Base
	{
		public TopScreen parent;

		public StoryButton(TopScreen ts)
		{
			parent = ts;

			// ものがたり音楽を使用しない場合押せなくする
			if (!muphic.Common.CommonSettings.getEnableStoryScreen()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			this.parent.ScreenMode = muphic.ScreenMode.StoryScreen;
		}

		public override void MouseEnter()
		{
			if (this.State == 2) return;
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			if (this.State == 2) return;
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
