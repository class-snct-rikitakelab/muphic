using System;

namespace muphic.Story.RightButtons
{
	/// <summary>
	/// VoiceButton �̊T�v�̐����ł��B
	/// </summary>
	public class VoiceButton : Base
	{
		StoryButtons parent;
		public VoiceButton(StoryButtons stories)
		{
			parent = stories;

			// �^���E���������̐ݒ�̏ꍇ�͉������Ȃ�
			if (!muphic.Common.CommonSettings.getEnableVoice()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			if(parent.NowClick == muphic.Story.StoryButtonsClickMode.Voice)//���ɐ��{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.None;	//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.Voice;	//����I�����Ă����Ԃɂ���
				this.State = 1;												//������I����Ԃɂ���
			}
		}

		public override void MouseEnter()
		{
			if (this.State == 2) return;
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			if (this.State == 2) return;
			base.MouseLeave ();
			if(parent.NowClick != muphic.Story.StoryButtonsClickMode.Voice)
			{
				this.State = 0;
			}
		}
	}
}
