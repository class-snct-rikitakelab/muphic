using System;

namespace muphic.Tutorial.TutorialStartParts
{
	/// <summary>
	/// StartButton �̊T�v�̐����ł��B
	/// </summary>
	public class StartButton : Base
	{
		public TutorialStart parent;
		
		public StartButton(TutorialStart ts)
		{
			this.parent = ts;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �`���[�g���A����� �`���[�g���A���̊J�n
			// ��������X�^�[�g�ɂ͂��Ȃ�
			parent.parnet.StartTutorial(false);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
