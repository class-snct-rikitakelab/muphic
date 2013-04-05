using System;

namespace muphic.Story.ScoreParts
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
			if(parent.parent.stories.NowClick == muphic.Story.StoryButtonsClickMode.Cancel)		//���Ƀ{�^����I��������ԂȂ�
			{
				parent.parent.stories.NowClick = muphic.Story.StoryButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//�L�����Z���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.parent.stories.NowClick = muphic.Story.StoryButtonsClickMode.Cancel;		//�L�����Z����I�����Ă����Ԃɂ���
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
			if(parent.parent.stories.NowClick != muphic.Story.StoryButtonsClickMode.Cancel)
			{
				this.State = 0;
			}
		}


	}
}
