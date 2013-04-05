using System;

namespace muphic.Link
{
	/// <summary>
	/// ListenButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class ListenButton : Base
	{
		public LinkScreen parent;
		public ListenButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.score.isPlay && !parent.score.answerCheckFlag)
			{
				if (parent.QuestionNum != 0) parent.LinkScreenMode = LinkScreenMode.ListenDialog;
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
