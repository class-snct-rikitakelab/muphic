using System;
using muphic.Titlemode;

namespace muphic.MakeStory
{
	/// <summary>
	/// TitleButton の概要の説明です。
	/// </summary>
	public class SentenceButton : Base
	{
		MakeStoryScreen parent;

		public SentenceButton(MakeStoryScreen mss)
		{
			parent = mss;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.TitOrSent = muphic.Titlemode.TargetMode.Sentence;
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
