using System;

namespace muphic.One
{
	/// <summary>
	/// StartStopButton �̊T�v�̐����ł��B
	/// </summary>
	public class StartStopButton : Base
	{
		public OneScreen parent;
		public StartStopButton(OneScreen one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(this.State == 0 || this.State == 1)
			{
				this.State = 2;
				parent.score.PlayStart();
			}
			else if(this.State == 2 || this.State == 3)
			{
				this.State = 0;
				parent.score.PlayStop();
			}
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			if(this.State == 0 || this.State == 1)				//�����ނ��\������Ă���Ȃ�
			{													//�����ނ�on�ɂȂ�
				this.State = 1;
			}
			else if(this.State == 2 || this.State == 3)			//�Ƃ܂邪�\������Ă���Ȃ�
			{													//�Ƃ܂��on�ɂȂ�
				this.State = 3;
			}
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(this.State == 0 || this.State == 1)				//�����ނ��\������Ă���Ȃ�
			{													//�����ނ�off�ɂȂ�
				this.State = 0;
			}
			else if(this.State == 2 || this.State == 3)			//�Ƃ܂邪�\������Ă���Ȃ�
			{													//�Ƃ܂��off�ɂȂ�
				this.State = 2;
			}
		}
	}
}
