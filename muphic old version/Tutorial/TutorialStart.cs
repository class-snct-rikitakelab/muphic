using System;
using muphic.Tutorial.TutorialStartParts;

namespace muphic.Tutorial
{
	/// <summary>
	/// TutorialStart �̊T�v�̐����ł��B
	/// </summary>
	public class TutorialStart : Screen
	{
		public TutorialScreen parnet;
		
		public StartButton startbutton;
		public ContinueButton continuebutton;
		public ChapterStartButton chapterstartbutton;
		public BackButton backbutton;
		
		public bool Chapter;
		
		/// <summary>
		/// �R���X�g���N�^
		/// ������́A��Ƀ`���[�g���A���J�n���ɌĂ΂��
		/// </summary>
		/// <param name="tscreen">�e�̃X�N���[��</param>
		public TutorialStart(TutorialScreen tscreen)
		{
			this.parnet = tscreen;
			this.Chapter = false;
			
			// �X�^�[�g�{�^�����C���X�^���X�����A�摜��o�^
			startbutton = new StartButton(this);
			DrawManager.Regist(startbutton.ToString(), 223, 514, "image\\TutorialXGA\\TutorialStart\\start_off.png",  "image\\TutorialXGA\\TutorialStart\\start_on.png");
			BaseList.Add(startbutton);
			
			// ��������{�^�����C���X�^���X�����A�摜��o�^
			continuebutton = new ContinueButton(this);
			DrawManager.Regist(continuebutton.ToString(), 548, 514, new string[] {"image\\TutorialXGA\\TutorialStart\\continue_off.png",  "image\\TutorialXGA\\TutorialStart\\continue_on.png", "image\\TutorialXGA\\TutorialStart\\continue_off_off.png"});
			BaseList.Add(continuebutton);

			// �w�i�摜�͂��炩���ߌ��߂�ꂽ�^�C�g���摜�ɂ���
			if(FileNameManager.GetFileExist(this.ToString())) DrawManager.Delete(this.ToString());
			DrawManager.Regist(this.ToString(), 0, 0, "image\\TutorialXGA\\TutorialStart\\TutorialStart_bak.png");
			
			TutorialStartConstructor2();
		}
		
		/// <summary>
		/// �R���X�g���N�^
		/// ������́A��Ƀ`���v�^�[�J�n���ɌĂ΂��
		/// </summary>
		/// <param name="tscreen">�e�̃X�N���[��</param>
		/// <param name="filename"></param>
		public TutorialStart(TutorialScreen tscreen, string filename)
		{
			this.parnet = tscreen;
			this.Chapter = true;
			
			// �e�`���v�^�[�̃X�^�[�g�{�^�����C���X�^���X�����A�摜��o�^
			chapterstartbutton = new ChapterStartButton(this);
			DrawManager.Regist(chapterstartbutton.ToString(), 362, 500, "image\\TutorialXGA\\TutorialStart\\chapterstart_off.png",  "image\\TutorialXGA\\TutorialStart\\chapterstart_on.png");
			BaseList.Add(chapterstartbutton);
			
			// �w�i�摜�͈����œn���ꂽ�摜�ɂ���
			if(FileNameManager.GetFileExist(this.ToString())) DrawManager.Delete(this.ToString());
			DrawManager.Regist(this.ToString(), 0, 0, filename);
			
			TutorialStartConstructor2();
		} 

		public void TutorialStartConstructor2()
		{
			// �����[�h�ɋ��ʂ���̂́A�߂�{�^��
			// �߂�{�^�����C���X�^���X�����A�摜��o�^
			backbutton = new BackButton(this);
			DrawManager.Regist(backbutton.ToString(), 434, 659, "image\\TutorialXGA\\TutorialStart\\back_off.png",  "image\\TutorialXGA\\TutorialStart\\back_on.png");
			BaseList.Add(backbutton);
		}

		public override void Draw()
		{
			base.Draw ();
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
