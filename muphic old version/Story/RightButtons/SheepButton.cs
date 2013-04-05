using System;

namespace muphic.Story.RightButtons
{
	/// <summary>
	/// SheepButton �̊T�v�̐����ł��B
	/// </summary>
	public class SheepButton : Base
	{
		public StoryButtons parent;
		public SheepButton(StoryButtons stories)
		{
			parent = stories;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.Story.StoryButtonsClickMode.Sheep)		//���ɗr�{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�r�{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.Sheep;		//�r��I�����Ă����Ԃɂ���
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
			if(parent.NowClick != muphic.Story.StoryButtonsClickMode.Sheep)
			{
				this.State = 0;
			}
		}
	}
}