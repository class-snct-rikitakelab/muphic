using System;

namespace muphic.Link
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class BackButton : Base
	{
		LinkScreen parent;
		public BackButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.score.isPlay && !parent.score.answerCheckFlag)
			{
				parent.parent.ScreenMode = muphic.ScreenMode.TopScreen;
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
