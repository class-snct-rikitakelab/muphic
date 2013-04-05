using System;
using System.IO;

namespace muphic.Tutorial.TutorialStartParts
{
	/// <summary>
	/// StartButton �̊T�v�̐����ł��B
	/// </summary>
	public class ContinueButton : Base
	{
		public TutorialStart parent;
		
		public ContinueButton(TutorialStart ts)
		{
			this.parent = ts;
			
			// �Z�[�u�f�[�^�����݂��Ȃ������甼�����\�� �����Ȃ�����
			string savefile = TutorialScreen.TutorialPass + TutorialMain.SaveFileDirectory + "\\" + TutorialMain.SaveFileName;
			//if( !File.Exists(savefile) )
			if( muphic.Common.TutorialTools.ReadSaveFile(savefile, false) < 1 )
			{
				this.State = 2;
			}
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(this.State == 2) return;
			
			// �`���[�g���A����� �`���[�g���A���̊J�n
			// ��������X�^�[�g
			parent.parnet.StartTutorial(true);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			if(this.State == 0) this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(this.State == 1) this.State = 0;
		}
	}
}