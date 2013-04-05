using System;

namespace muphic.Link.Dialog.Listen
{
	/// <summary>
	/// StartStopButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class ListenStartStopButton : Base
	{
		public ListenDialog parent;
		public ListenStartStopButton(ListenDialog dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(this.State == 0)
			{
				parent.bar.lionPoint = 0;
				this.State = 1;
				parent.bar.isPlay = true;
			}
			else if(this.State == 1)
			{
				//this.State = 0;
				//parent.bar.isPlay = false;
			}
		}

	}
}
