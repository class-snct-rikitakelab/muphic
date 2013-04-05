using System;

namespace muphic.One.RightButtons
{
	/// <summary>
	/// DogButton �̊T�v�̐����ł��B
	/// </summary>
	public class DogButton : Base
	{
		public OneButtons parent;
		public DogButton(OneButtons ones)
		{
			parent = ones;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.One.OneButtonsClickMode.Dog)		//���Ɍ��{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.Dog;		//����I�����Ă����Ԃɂ���
				this.State = 1;												//������I����Ԃɂ���
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
			if(parent.NowClick != muphic.One.OneButtonsClickMode.Dog)
			{
				this.State = 0;
			}
		}
	}
}
