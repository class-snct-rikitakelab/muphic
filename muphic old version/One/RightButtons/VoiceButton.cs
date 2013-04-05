using System;

using muphic.tag;

namespace muphic.One.RightButtons
{
	/// <summary>
	/// VoiceButton �̊T�v�̐����ł��B
	/// </summary>
	public class VoiceButton : Base
	{
		public OneButtons parent;
		public VoiceButton(OneButtons ones)
		{
			parent = ones;

			// �^���E���������̐ݒ�̏ꍇ�͉������Ȃ�
			if (!muphic.Common.CommonSettings.getEnableVoice()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			if(parent.NowClick == muphic.One.OneButtonsClickMode.Voice)		//���ɐ��{�^����I��������ԂȂ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.None;		//�����I�����Ă��Ȃ���Ԃɂ���
				this.State = 0;												//�����̑I������
			}
			else															//���{�^����I�����Ă��Ȃ��Ȃ�
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.Voice;		//����I�����Ă����Ԃɂ���
				this.State = 1;												//������I����Ԃɂ���
//				
//				MakeSound ms = new MakeSound();
//				ms.VoiceToWave();
			}
		}

		public override void MouseEnter()
		{
			if (this.State == 2) return;
			base.MouseEnter();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			if (this.State == 2) return;
			base.MouseLeave();
			if(parent.NowClick != muphic.One.OneButtonsClickMode.Voice)
			{
				this.State = 0;
			}
		}
	}
}
