using System;

namespace muphic.Link
{
	/// <summary>
	/// RestartButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class RestartButton : Base
	{
		public LinkScreen parent;
		public RestartButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.score.answerCheckFlag)
			{
				parent.startstop.State = 2;
				parent.links.BaseState0();
				parent.score.PlayFirst();
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
