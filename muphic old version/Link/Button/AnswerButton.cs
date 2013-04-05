using System;

namespace muphic.Link
{
	/// <summary>
	/// AnswerButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class AnswerButton : Base
	{
		public LinkScreen parent;

		public AnswerButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.score.isPlay && !parent.score.answerCheckFlag)
			{
				if (parent.QuestionNum <= 0 || parent.score.AnimalList.Count == 0) return;
				//parent.check = new muphic.Link.Dialog.Answer.AnswerCheck(parent);
				parent.LinkScreenMode = LinkScreenMode.AnswerDialog;
				parent.check.Check();
				parent.score.PlayAnswerCheck();
				//parent.tsuibi.Visible = false;
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
