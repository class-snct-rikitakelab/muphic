using System;

namespace muphic.Link.RightButtons
{
	/// <summary>
	/// CancelButton �̊T�v�̐����ł��B
	/// </summary>
	public class CancelButton : Base
	{
		public LinkButtons parent;
		public CancelButton(LinkButtons links)
		{
			parent = links;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.Link.LinkButtonsClickMode.Cancel)		//���ɃL�����Z���{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.Link.LinkButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�L�����Z���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.Link.LinkButtonsClickMode.Cancel;		//�L�����Z����I�����Ă����Ԃɂ���
				this.State = 1;												//������I����Ԃɂ���
			}
		}
	}
}
