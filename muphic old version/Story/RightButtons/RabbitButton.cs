using System;

namespace muphic.Story.RightButtons
{
	/// <summary>
	/// RabbitButton �̊T�v�̐����ł��B
	/// </summary>
	public class RabbitButton : Base
	{
		public StoryButtons parent;
		public RabbitButton(StoryButtons stories)
		{
			parent = stories;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.Story.StoryButtonsClickMode.Rabbit)		//���ɃE�T�M�{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�E�T�M�{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.Rabbit;		//�E�T�M��I�����Ă����Ԃɂ���
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
			if(parent.NowClick != muphic.Story.StoryButtonsClickMode.Rabbit)
			{
				this.State = 0;
			}
		}
	}
}
