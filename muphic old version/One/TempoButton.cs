using System;

namespace muphic.One
{
	/// <summary>
	/// TempoButton �̊T�v�̐����ł��B
	/// </summary>
	public class TempoButton : Base
	{
		public Tempo parent;
		public int num;											//���̃e���|�{�^���̔ԍ�
		public TempoButton(Tempo tempo, int i)
		{
			parent = tempo;
			num = i;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.TempoMode = num;
			this.State = 1;
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.TempoMode != num)
			{
				this.State = 0;
			}
		}



		public override string ToString()
		{
			return "TempoButton" + this.num;					//�͋Z
		}


	}
}
