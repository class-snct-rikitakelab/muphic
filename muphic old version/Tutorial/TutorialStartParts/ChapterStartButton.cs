using System;

namespace muphic.Tutorial.TutorialStartParts
{
	/// <summary>
	/// StartButton �̊T�v�̐����ł��B
	/// </summary>
	public class ChapterStartButton : Base
	{
		public TutorialStart parent;
		
		public ChapterStartButton(TutorialStart ts)
		{
			this.parent = ts;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �X�^�[�g�{�^����������X�e�[�g�i�s�ł�����
			this.parent.parnet.tutorialmain.NextState();
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
