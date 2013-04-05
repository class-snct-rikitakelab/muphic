using System;

namespace muphic.Link.Dialog.Answer
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class AnswerBackButton : Base
	{
		AnswerDialog parent;
		public AnswerBackButton(AnswerDialog dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			//parent.parent.parent.Screen = parent.parent;
			//parent.parent.Screen = null;
			//parent.parent.tsuibi.Visible = true;
			parent.parent.LinkScreenMode = muphic.LinkScreenMode.LinkScreen;
			parent.parent.signboard.drawFlag = true;
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.State = 0;
		}
	}
}