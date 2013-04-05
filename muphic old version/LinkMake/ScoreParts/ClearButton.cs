using System;

namespace muphic.LinkMake.ScoreParts
{
	/// <summary>
	/// ClearButton �̊T�v�̐����ł��B
	/// </summary>
	public class ClearButton : Base
	{
		Score parent;
		public ClearButton(Score one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.parent.linkmakes.NowClick == muphic.LinkMake.LinkMakeButtonsClickMode.Cancel)		//���Ƀ{�^����I��������ԂȂ�
			{
				parent.parent.linkmakes.NowClick = muphic.LinkMake.LinkMakeButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.parent.linkmakes.NowClick = muphic.LinkMake.LinkMakeButtonsClickMode.Cancel;		//����I�����Ă����Ԃɂ���
				this.State = 1;												//������I����Ԃɂ���
			}
			//parent.Animals.Delete();
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.parent.linkmakes.NowClick != muphic.LinkMake.LinkMakeButtonsClickMode.Cancel)
			{
				this.State = 0;
			}
		}
	}
}
