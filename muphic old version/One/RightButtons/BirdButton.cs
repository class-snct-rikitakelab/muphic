using System;

namespace muphic.One.RightButtons
{
	/// <summary>
	/// BirdButton �̊T�v�̐����ł��B
	/// </summary>
	public class BirdButton : Base
	{
		public OneButtons parent;
		public BirdButton(OneButtons ones)
		{
			parent = ones;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.One.OneButtonsClickMode.Bird)	//���ɃL�����Z���{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�L�����Z���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.Bird;	//�L�����Z����I�����Ă����Ԃɂ���
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
			if(parent.NowClick != muphic.One.OneButtonsClickMode.Bird)
			{
				this.State = 0;
			}
		}
	}
}
