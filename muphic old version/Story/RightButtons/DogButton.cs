using System;

namespace muphic.Story.RightButtons
{
	/// <summary>
	/// DogButton �̊T�v�̐����ł��B
	/// </summary>
	public class DogButton : Base
	{
		public StoryButtons parent;
		public DogButton(StoryButtons stories)
		{
			parent = stories;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.Story.StoryButtonsClickMode.Dog)		//���Ɍ��{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.Dog;		//����I�����Ă����Ԃɂ���
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
			if(parent.NowClick != muphic.Story.StoryButtonsClickMode.Dog)
			{
				this.State = 0;
			}
		}
	}
}
