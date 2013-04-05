using System;

namespace muphic.One.RightButtons
{
	/// <summary>
	/// CatButton �̊T�v�̐����ł��B
	/// </summary>
	public class CatButton : Base
	{
		public OneButtons parent;
		public CatButton(OneButtons ones)
		{
			parent = ones;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.One.OneButtonsClickMode.Cat)		//���ɔL�{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�L�{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.Cat;		//�L��I�����Ă����Ԃɂ���
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
			if(parent.NowClick != muphic.One.OneButtonsClickMode.Cat)
			{
				this.State = 0;
			}
		}
	}
}
