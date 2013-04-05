using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScoreTitleArea �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreTitleArea : Screen
	{
		public ScoreScreen parent;
		public string ScoreTitle;		
		
		public ScoreTitleArea(ScoreScreen score)
		{
			this.parent = score;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			// Ȱ� �R(`�D�L)ɳܰ�
			
			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			DrawManager.Regist(this.ToString(), 170, 42, "image\\ScoreXGA\\titlearea.png");
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			// Ȱ� �R(`�D�L)ɳܰ�
			
			switch(this.parent.ParentScreenMode)
			{
				case muphic.ScreenMode.OneScreen:
					// �ЂƂ�ŉ��y����Ăяo���ꂽ�ꍇ 
					this.ScoreTitle = ((OneScreen)this.parent.parent).ScoreTitle;
					break;
				case muphic.ScreenMode.LinkScreen:
					// �Ȃ��ĉ��y�i�񓚁j����Ăяo���ꂽ�ꍇ
					this.ScoreTitle = ((LinkScreen)this.parent.parent).titlebar.Title;
					break;
				case muphic.ScreenMode.LinkMakeScreen:
					// �Ȃ��ĉ��y�i���쐬�j����Ăяo���ꂽ�ꍇ
					this.Visible = false;
					break;
				case muphic.ScreenMode.StoryScreen:
					// ���̂����艹�y����Ăяo���ꂽ�ꍇ
					// ���肾�����ꍇ�͂�����ő�����A�S�p�����ŃX���C�h�̃y�[�W����t������
					string title = ((StoryScreen)this.parent.parent).parent.PictureStory.Title;
					if( title == "" || title == null ) title = "�����炵�����̂�����"; 
					string page = "�@" + ((char)((int)'�P' + ((StoryScreen)this.parent.parent).NowPage)).ToString() + "�y�[�W";
					this.ScoreTitle = title + page;
					break;
				default:
					break;
			}
			
		}
		
		public override void Draw()
		{
			base.Draw ();
			DrawManager.DrawString(this.ScoreTitle, 191, 57);
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}
		
		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}



	}
}
