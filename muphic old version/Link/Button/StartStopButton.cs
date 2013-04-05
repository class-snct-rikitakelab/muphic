using System;

namespace muphic.Link
{
	/// <summary>
	/// StartStopButton の概要の説明です。
	/// </summary>
	public class StartStopButton : Base
	{
		public LinkScreen parent;
		public StartStopButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (!parent.score.answerCheckFlag)
			{
				base.Click (p);
				if(this.State == 0 || this.State == 1)
				{
					this.State = 2;
					//parent.tsuibi.Visible = false;
					parent.score.PlayStart();
				}
				else if(this.State == 2 || this.State == 3)
				{
					this.State = 0;
					//parent.tsuibi.Visible = true;
					parent.score.PlayStop();
				}
			}
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			if(this.State == 0 || this.State == 1)				//すすむが表示されているなら
			{													//すすむのonになる
				this.State = 1;
			}
			else if(this.State == 2 || this.State == 3)			//とまるが表示されているなら
			{													//とまるのonになる
				this.State = 3;
			}
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(this.State == 0 || this.State == 1)				//すすむが表示されているなら
			{													//すすむのoffになる
				this.State = 0;
			}
			else if(this.State == 2 || this.State == 3)			//とまるが表示されているなら
			{													//とまるのoffになる
				this.State = 2;
			}
		}
	}
}
