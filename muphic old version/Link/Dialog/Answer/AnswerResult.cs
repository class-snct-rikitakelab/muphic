using System;

namespace muphic.Link.Dialog.Answer
{
	/// <summary>
	///  ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class AnswerResult : Base
	{
		AnswerDialog parent;
		public AnswerResult(AnswerDialog dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			//parent.parent.parent.Screen = parent.parent;
			//parent.parent.LinkScreenMode = muphic.LinkScreenMode.LinkScreen;
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
		}
	}
}