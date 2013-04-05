using System;

namespace muphic.Link.RightButtons
{
	/// <summary>
	/// SheepButton �̊T�v�̐����ł��B
	/// </summary>
	public class Sheep09Button : Base
	{
		public LinkButtons parent;
		public Sheep09Button(LinkButtons links)
		{
			parent = links;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.parent.score.isPlay && !parent.parent.score.answerCheckFlag)
			{
				if(parent.NowClick == muphic.Link.LinkButtonsClickMode.Sheep09)		//���ɗr�{�^����I��������ԂȂ�
				{
					parent.NowClick = muphic.Link.LinkButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
					this.State = 0;												//�����̑I������
				}
				else															//�r�{�^����I�����Ă��Ȃ��Ȃ�
				{
					parent.NowClick = muphic.Link.LinkButtonsClickMode.Sheep09;		//�r��I�����Ă����Ԃɂ���
					this.State = 1;												//������I����Ԃɂ���
				}
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
			if(parent.NowClick != muphic.Link.LinkButtonsClickMode.Sheep09)
			{
				this.State = 0;
			}
		}
	}
}
