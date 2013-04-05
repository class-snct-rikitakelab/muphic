using System;

namespace muphic.One.RightButtons
{
	/// <summary>
	/// PigButton �̊T�v�̐����ł��B
	/// </summary>
	public class PigButton : Base
	{
		public OneButtons parent;
		public PigButton(OneButtons ones)
		{
			parent = ones;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.One.OneButtonsClickMode.Pig)		//���ɓ؃{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�؃{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.Pig;		//�؂�I�����Ă����Ԃɂ���
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
			if(parent.NowClick != muphic.One.OneButtonsClickMode.Pig)
			{
				this.State = 0;
			}
		}
	}
}
