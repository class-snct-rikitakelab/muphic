using System;

namespace muphic.Story.RightButtons
{
	/// <summary>
	/// CatButton �̊T�v�̐����ł��B
	/// </summary>
	public class CatButton : Base
	{
		public StoryButtons parent;
		public CatButton(StoryButtons stories)
		{
			parent = stories;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.Story.StoryButtonsClickMode.Cat)//���ɔL�{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.None;	//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�L�{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.Cat;	//�L��I�����Ă����Ԃɂ���
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
			if(parent.NowClick != muphic.Story.StoryButtonsClickMode.Cat)
			{
				this.State = 0;
			}
		}
	}
}
