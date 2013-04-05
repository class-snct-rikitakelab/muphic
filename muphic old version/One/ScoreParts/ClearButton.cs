using System;

namespace muphic.One.ScoreParts
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
			if(parent.parent.ones.NowClick == muphic.One.OneButtonsClickMode.Cancel)		//���Ƀ{�^����I��������ԂȂ�
			{
				parent.parent.ones.NowClick = muphic.One.OneButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.parent.ones.NowClick = muphic.One.OneButtonsClickMode.Cancel;		//����I�����Ă����Ԃɂ���
				this.State = 1;												//������I����Ԃɂ���
			}
			//parent.Animals.Delete();										//���ݑI�𒆂̓������폜����
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.parent.ones.NowClick != muphic.One.OneButtonsClickMode.Cancel)
			{
				this.State = 0;
			}
		}


	}
}
