using System;

namespace muphic.Link
{
	/// <summary>
	/// ScoreButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class ScoreButton : Base
	{
		public LinkScreen parent;
		public ScoreButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.score.isPlay && !parent.score.answerCheckFlag)
			{
				parent.LinkScreenMode = LinkScreenMode.ScoreScreen;
			}
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
