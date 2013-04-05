using System;

namespace muphic.Tutorial.TutorialSPParts
{
	/// <summary>
	/// CheckButton �̊T�v�̐����ł��B
	/// </summary>
	public class CheckButton : Base
	{
		public TutorialMain parent;
		
		public CheckButton(TutorialMain tm)
		{
			this.parent = tm;
		}
		
		
		public bool Check()
		{
			StoryScreen story = (StoryScreen)((MakeStoryScreen)this.parent.topscreen.Screen).Screen;
			
			// �����̏���1 ������4�g���Ă��邱��
			if( story.score.Animals.AnimalList.Count != 4)
			{
				this.parent.msgwindow.getText(new string[] {"�܂��܂��A���Ȃ������@����Ȃ������B", "�ǂ��Ԃ́@�����Ɓ@��������ĂˁB", "���������ǁ@����΂��Ă݂悤�I" });
				this.parent.msgwindow.ChangeWindowCoordinate(1);
				this.parent.SetVoice("PT03_Story50_1.mp3");
				
				return false;
			}
			
			// �����̏���2 �S�Ẳ��̉��K���������Ă���
			int code0 = ((Animal)story.score.Animals.AnimalList[0]).code;
			int code1 = ((Animal)story.score.Animals.AnimalList[1]).code;
			int code2 = ((Animal)story.score.Animals.AnimalList[2]).code;
			int code3 = ((Animal)story.score.Animals.AnimalList[3]).code;
			if( !((code0 < code1) && (code1 < code2) && (code2 < code3)) )
			{
				this.parent.msgwindow.getText(new string[] {"�܂��܂��A���Ȃ������@����Ȃ������B", "���Ƃ̂����������@�����Ă����񂾂�B", "���������ǁ@����΂��Ă݂悤�I" });
				this.parent.msgwindow.ChangeWindowCoordinate(1);
				this.parent.SetVoice("PT03_Story50_2.mp3");
				
				return false;
			}
			
			// ��L2���N���A�����琳��
			muphic.Common.TutorialStatus.setDisableIsSPMode();		// ���փ{�^���N���b�N�ŃX�e�[�g�i�s�ł���悤�ɂ���
			this.parent.msgwindow.getText(new string[] {"���������f�B���ł����ˁI", "����Ł@�Ă����́@���������͂����I" });
			this.parent.msgwindow.ChangeWindowCoordinate(1);
			this.parent.SetVoice("PT03_Story50_3.mp3");
			
			return true;
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �`�F�b�N�{�^�����N���b�N���ꂽ��y���̍̓_(�H)���s���A���̌��ʂɊ����R�}���h���[�h���ēx�Ăяo��
			this.parent.SPCommand("PT03_Story50_" + this.Check().ToString());
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
