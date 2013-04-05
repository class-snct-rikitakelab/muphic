using System;
using muphic.Titlemode;

namespace muphic.MakeStory
{
	/// <summary>
	/// TitleButton �̊T�v�̐����ł��B
	/// </summary>
	public class TitleButton : Base
	{
		MakeStoryScreen parent;

		public TitleButton(MakeStoryScreen mss)
		{
			parent = mss;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.TitOrSent = muphic.Titlemode.TargetMode.Title;
			parent.MakeStoryScreenMode = muphic.MakeStoryScreenMode.TitleScreen;
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
