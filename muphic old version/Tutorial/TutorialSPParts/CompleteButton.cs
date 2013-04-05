using System;

namespace muphic.Tutorial.TutorialSPParts
{
	/// <summary>
	/// CompleteButton �̊T�v�̐����ł��B
	/// </summary>
	public class CompleteButton : Base
	{
		public TutorialMain parent;
		
		public CompleteButton(TutorialMain tm)
		{
			this.parent = tm;
		}
		
		public bool Check()
		{
			OneScreen one = ((OneScreen)this.parent.topscreen.Screen);
			
			// �����̏��� ������10�C�ȏ�A���ߐ����S�ȏ�ł��邱��
			if( !(one.score.Animals.AnimalList.Count >= 10 && ((Animal)one.score.Animals.AnimalList[one.score.Animals.AnimalList.Count-1]).place >= 24) )
			{
				this.parent.msgwindow.getText(new string[] {"����ł́@�݂��������ā@�̂낢���@�Ƃ��Ȃ��B", "���������ǁ@����΂��Ă݂悤�I"});
				this.parent.msgwindow.ChangeWindowCoordinate(1);
				this.parent.SetVoice("PT04_One02_1.mp3");
				
				return false;
			}
			
			// ��L���N���A�����琳��
			muphic.Common.TutorialStatus.setDisableIsSPMode();		// ���փ{�^���N���b�N�ŃX�e�[�g�i�s�ł���悤�ɂ���
			this.parent.msgwindow.getText(new string[] {"���傭���ł����ˁI�@����Ł@�̂낢�́@�Ƃ���@�͂����I" });
			this.parent.msgwindow.ChangeWindowCoordinate(1);
			this.parent.SetVoice("PT04_One02_2.mp3");
			
			return true;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �`�F�b�N�{�^�����N���b�N���ꂽ��y���̍̓_(�H)���s���A���̌��ʂɊ����R�}���h���[�h���ēx�Ăяo��
			this.parent.SPCommand("PT04_One02_" + this.Check().ToString());
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
