using System;

namespace muphic.Link
{
	/// <summary>
	/// ClearButton �̊T�v�̐����ł��B
	/// </summary>
	public class ClearButton : Base
	{
		LinkScreen parent;
		public ClearButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);

			if (!parent.score.isPlay && !parent.score.answerCheckFlag)
			{
				parent.links.BaseState0();
				if(parent.links.NowClick == muphic.Link.LinkButtonsClickMode.Cancel)		//���ɃL�����Z���{�^����I��������ԂȂ�
				{
					parent.links.NowClick = muphic.Link.LinkButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
					this.State = 0;												//�����̑I������
				}
				else															//�L�����Z���{�^����I�����Ă��Ȃ��Ȃ�
				{
					parent.links.NowClick = muphic.Link.LinkButtonsClickMode.Cancel;		//�L�����Z����I�����Ă����Ԃɂ���
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
			if(parent.links.NowClick != muphic.Link.LinkButtonsClickMode.Cancel)
			{
				this.State = 0;
			}
		}
	}
}


