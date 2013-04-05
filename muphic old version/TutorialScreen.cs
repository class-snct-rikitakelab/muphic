using System;
using System.IO;
using muphic.Common;
using muphic.Tutorial;

namespace muphic
{
	/// <summary>
	/// TutorialScreen �̊T�v�̐����ł��B
	/// </summary>
	public class TutorialScreen : Screen
	{
		public TopScreen parent;

		public TutorialStart tutorialstart;
		public TutorialMain tutorialmain;

		public bool isTutorialStart;	// �X�^�[�g��ʂ��ǂ����̃t���O
		
		public const string TutorialPass = "TutorialData\\TutorialMain";
		
		/// <summary>
		/// Tutorial�̃R���X�g���N�^
		/// </summary>
		/// <param name="screen">�eScreen</param>
		public TutorialScreen(TopScreen topscreen)
		{
			this.parent = topscreen;
			
			// �t�F�[�h���ʂ̐ݒ�
			DrawManager.FadeOutEvent += new FadeEventHandler(this.Draw);
			DrawManager.FadeInEvent += new FadeEventHandler(this.Draw);
			
			// �X�^�[�g��ʂ̃C���X�^���X��
			tutorialstart = new TutorialStart(this);
			
			// �X�^�[�g��ʂ�on�ɂ���
			this.isTutorialStart = true;
		}
		
		
		/// <summary>
		/// �`���[�g���A�����J�n����
		/// </summary>
		public void StartTutorial(bool continueflag)
		{
			// �X�^�[�g���off
			this.isTutorialStart = false;
			
			// �`���[�g���A���̎��s��TutorialStatus�ɒʒm
			TutorialStatus.setEnableTutorial();
			
			// NowLoading��ʌĂяo��
			DrawManager.BeginRegist(45);
			
			// �`���[�g���A����ʂ̃C���X�^���X��
			this.tutorialmain = new TutorialMain(this, TutorialPass, continueflag);
			
			// NowLoading��ʏI��
			//DrawManager.EndRegist();

			// �`���[�g���A���J�n �ŏ��̃`���v�^�[��
			this.tutorialmain.NextChapter();
		}
		
		
		/// <summary>
		/// �`���[�g���A�����I������
		/// </summary>
		public void EndTutorial()
		{
			// �`���[�g���A���I��
			TutorialStatus.initTutorialStatus();
			
			// Top��ʂɖ߂�
			this.parent.ScreenMode = muphic.ScreenMode.TopScreen;
			
			Console.WriteLine("�`���[�g���A���I��");
		}
		
		public string getTutorialPass()
		{
			return TutorialScreen.TutorialPass;
		}
		
		
		/// <summary>
		/// �`�惁�\�b�h
		/// </summary>
		public override void Draw()
		{
			base.Draw();

			// �X�^�[�g��ʂ�on�Ȃ�
			if(isTutorialStart)
			{
				// �X�^�[�g��ʂ̂ݕ`�悵�ďI���
				this.tutorialstart.Draw();
				return;
			}
			
			// �`���[�g���A���̕`��
			this.tutorialmain.Draw();
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			// �X�^�[�g��ʂ�on�Ȃ�
			if(isTutorialStart)
			{
				// �X�^�[�g��ʂ̓���̂�
				this.tutorialstart.Click(p);
				return;
			}
			
			// �`���[�g���A���̃N���b�N����
			this.tutorialmain.Click(p);
		}
		
		
		public override void MouseMove(System.Drawing.Point p)
		{
			// �X�^�[�g��ʂ�on�Ȃ�
			if(isTutorialStart)
			{
				// �X�^�[�g��ʂ̓���̂�
				this.tutorialstart.MouseMove(p);
				return;
			}
			
			// �`���[�g���A���̓���
			this.tutorialmain.MouseMove(p);
		}
	}
}
