using System;

namespace muphic.ADV.MsgWindowParts
{
	/// <summary>
	/// VoiceButton �̊T�v�̐����ł��B
	/// </summary>
	public class VoiceButton : Base
	{
		public MsgWindow parent;
		
		public VoiceButton(MsgWindow msgwindow)
		{
			this.parent = msgwindow;
			this.State = 1;				// �f�t�H���g�ŉ�����ON�ɂ��Ă���
		}

		/// <summary>
		/// �������Đ����邩�ۂ���₢���킹��
		/// </summary>
		/// <returns></returns>
		public bool getIsVoice()
		{
			if(this.State == 0) return false;
			else return true;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			if(this.State == 0)
			{
				// �{�^���������璼�����̓����特���𗬂��ׂ����A���̕����痬���ׂ���
				this.State = 1;
				((TutorialScreen)this.parent.parent.parent).tutorialmain.isPlayVoice = true;
			}
			else 
			{
				// �{�^����OFF�ɂ��A�����̒�~
				this.State = 0;
				((TutorialScreen)this.parent.parent.parent).tutorialmain.StopVoice();
				((TutorialScreen)this.parent.parent.parent).tutorialmain.isPlayVoice = false;
			}
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
		}
	}
}
