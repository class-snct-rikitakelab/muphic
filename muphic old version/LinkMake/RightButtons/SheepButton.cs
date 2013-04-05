using System;

namespace muphic.LinkMake.RightButtons
{
	/// <summary>
	/// SheepButton �̊T�v�̐����ł��B
	/// </summary>
	public class SheepButton : Base
	{
		public LinkMakeButtons parent;
		public SheepButton(LinkMakeButtons ones)
		{
			parent = ones;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.LinkMake.LinkMakeButtonsClickMode.Sheep)		//���ɗr�{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.LinkMake.LinkMakeButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�r�{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.LinkMake.LinkMakeButtonsClickMode.Sheep;		//�r��I�����Ă����Ԃɂ���
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
			if(parent.NowClick != muphic.LinkMake.LinkMakeButtonsClickMode.Sheep)
			{
				this.State = 0;
			}
		}
	}
}
