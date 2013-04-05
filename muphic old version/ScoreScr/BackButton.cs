using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// �y����ʂ̖߂�{�^��
	/// �y����ʂ���Ăяo�����̉�ʁi�ЂƂ�ŉ��y�E�Ȃ��ĉ��y�E���̂����艹�y�j�ɖ߂�ۂɎg�p
	/// </summary>
	public class BackButton : Base
	{
        public ScoreScreen parent;

		public BackButton(ScoreScreen score)
		{
			this.parent = score;
		}

		/// <summary>
		/// �N���b�N���̏���
		/// �y����ʂ��Ăяo������ʂɖ߂�
		/// </summary>
		/// <param name="p"></param>
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);

			// �e�N�X�`���̊J��
			//this.parent.delete.DeleteAllScoreTexture();

			// �ǂ̉�ʂ���Ăяo���ꂽ���ɂ���ď������ς��
			switch(parent.ParentScreenMode)
			{
				case ScreenMode.OneScreen:
					// �ЂƂ�ŉ��y���[�h����y����ʂ��Ăяo����Ă����ꍇ�A�߂�{�^���������ƂЂƂ�ŉ��y���[�h�ɖ߂�
					((OneScreen)this.parent.parent).OneScreenMode = OneScreenMode.OneScreen;
					break;
				case ScreenMode.LinkScreen:
					// �Ȃ��ĉ��y���[�h����y����ʂ��Ăяo����Ă����ꍇ�A�߂�{�^���������ƂȂ��ĉ��y���[�h�ɖ߂�
					((LinkScreen)this.parent.parent).LinkScreenMode = LinkScreenMode.LinkScreen;
					break;
				case ScreenMode.LinkMakeScreen:
					// �Ȃ��ĉ��y���[�h����y����ʂ��Ăяo����Ă����ꍇ�A�߂�{�^���������ƂȂ��ĉ��y���[�h�ɖ߂�
					((LinkMakeScreen)this.parent.parent).LinkMakeScreenMode = LinkMakeScreenMode.LinkMakeScreen;
					break;
				case ScreenMode.StoryScreen:
					// ���̂����艹�y���[�h����y����ʂ��Ăяo����Ă����ꍇ�A�߂�{�^���������Ƃ��̂����艹�y���[�h�ɖ߂�
					((StoryScreen)parent.parent).StoryScreenMode = StoryScreenMode.StoryScreen;
					break;
				default:
					// ���̃R�[�h�����s����邱�Ƃ͂��肦�܂���B���s�����悤�Ȃ��Ƃ�����ΐ��E���I���܂��B�������炸�B
					System.Console.WriteLine("Sekai no owari. Fatal error.");
					Environment.Exit(52);
					break;
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
			this.State = 0;
		}
	}
}
