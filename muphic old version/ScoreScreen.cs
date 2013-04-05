using System;
using System.Collections;
using muphic.ScoreScr;
using muphic.Titlemode;

namespace muphic
{	
	#region SVGA (�`ver.0.8.8)
	/*
	/// <summary>
	/// �y�����Screen�N���X
	/// </summary>
	public class ScoreScreen : Screen
	{
		
		public Screen parent;				// �eScreen�N���X
		public ScreenMode ParentScreenMode;	// �eScreen��ScreenMode��ێ�

		public ArrayList AnimalList = new ArrayList();		// �������X�g�̃R�s�[

		public BackButton back;				// ���ǂ�{�^��
		public ReadButton read;				// ��т����{�^��
		public WriteButton write;			// �̂����{�^��6
		public UpScrollButton up;			// ��X�N���[���{�^��
		public DownScrollButton down;		// ���X�N���[���{�^��
		public ScoreWindow scorewindow;		// �y���\���E�B���h�E
		public ScoreButtons scores;			// �e�����{�^�����Ǘ�
		public ScoreMain scoremain;			// �y���{��
		public ScoreSelectDialog sedialog;	// �y���ǂݍ��݃_�C�A���O
		public ScoreSaveDialog sadialog;	// �y���ۑ��_�C�A���O
		public DeleteTexture delete;		// �摜�J���p

		public bool isSelectDialog = false;	// �ǂݍ��݃_�C�A���O���J����Ă��邩�ǂ����̃t���O
		public bool isSaveDialog = false;	// �ۑ��_�C�A���O���J����Ă��邩�ǂ����̃t���O

		/// <summary>
		/// �y�����Screen�N���X �R���X�g���N�^
		/// �ЂƂ�ŉ��y���[�h����̌Ăяo�����Ɏg�p
		/// </summary>
		/// <param name="screen">�ЂƂ�ŉ��y(OneScreen)�N���X</param>
		public ScoreScreen(OneScreen onescreen)
		{
			this.parent = (Screen)onescreen;				// parent��Screen�^�Ȃ̂ŃL���X�g���Ďg�p
			this.ParentScreenMode = ScreenMode.OneScreen;	// �eScreen��ScreenMode���ЂƂ�ŉ��y���[�h�ɐݒ�
			this.ScoreScreenPreparation();					// �e���i�̃C���X�^���X����
		}

		/// <summary>
		/// �y�����Screen�N���X �R���X�g���N�^
		/// �Ȃ��ĉ��y���[�h����̌Ăяo�����Ɏg�p
		/// </summary>
		/// <param name="screen">�Ȃ��ĉ��y(LinkScreen)�N���X</param>
		public ScoreScreen(LinkScreen linkscreen)
		{
			this.parent = (Screen)linkscreen;				// parent��Screen�^�Ȃ̂ŃL���X�g���Ďg�p
			this.ParentScreenMode = ScreenMode.LinkScreen;	// �eScreen��ScreenMode���ЂƂ�ŉ��y���[�h�ɐݒ�
			this.ScoreScreenPreparation();						// �e���i�̃C���X�^���X����
		}

		/// <summary>
		/// �y�����Screen�N���X �R���X�g���N�^
		/// ���̂����艹�y���[�h����̌Ăяo�����Ɏg�p
		/// </summary>
		/// <param name="screen">���̂����艹�y(StoryScreen)�N���X</param>
		public ScoreScreen(StoryScreen storyscreen)
		{
			this.parent = (Screen)storyscreen;				// parent��Screen�^�Ȃ̂ŃL���X�g���Ďg�p
			this.ParentScreenMode = ScreenMode.StoryScreen;	// �eScreen��ScreenMode���ЂƂ�ŉ��y���[�h�ɐݒ�
			this.ScoreScreenPreparation();					// �e���i�̃C���X�^���X����
		}


		/// <summary>
		/// �y����ʃR���X�g���N�^�Q
		/// �e���i�̃C���X�^���X���Ɠo�^���s��
		/// </summary>
		private void ScoreScreenPreparation()
		{
			System.Console.WriteLine("�y����ʌĂяo����:{0}", this.ParentScreenMode);
			this.AnimalList.Clear();

			// �Ăяo����Screen�ɖ� ���ꂼ���AnimalList���R�s�[����
			switch(this.ParentScreenMode)
			{
				case ScreenMode.OneScreen:
					this.AnimalList = ((OneScreen)this.parent).score.Animals.AnimalList;
					break;
				case ScreenMode.LinkScreen:
					for(int i=0; i<((LinkScreen)this.parent).score.AnimalList.Count; i++)
					{
						// LinkScreen��AnimalList����v�f���ЂƂЂƂ��o���A��������"Sheep"�ɂ��Ď蓮�R�s�[
						Animal temp = (Animal)(((LinkScreen)this.parent).score.AnimalList[i]);
						Animal animal = new Animal(temp.place, temp.code, "Sheep");
						this.AnimalList.Add(animal);
					}
					// this.AnimalList = ((LinkScreen)this.parent).score.AnimalList;
					break;
				case ScreenMode.StoryScreen:
					this.AnimalList = ((StoryScreen)this.parent).score.Animals.AnimalList;
					break;
				default:
					break;
			}

			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			back = new BackButton(this);
			read = new ReadButton(this);
			write = new WriteButton(this);
			up = new UpScrollButton(this);
			down = new DownScrollButton(this);
			scorewindow = new ScoreWindow(this);
			scores = new ScoreButtons(this);
			scoremain = new ScoreMain(this);
			sedialog = new ScoreSelectDialog(this);
			sadialog = new ScoreSaveDialog(this);
			delete = new DeleteTexture(this);


			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\Score\\score.png");	// �{���̔w�i
			//muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\Score\\gakuhu.png");	// �{�^���z�u���{�w�i
			muphic.DrawManager.Regist(back.ToString(), 25, 7, "image\\Score\\button\\modoru.png", "image\\score\\button\\modoru_on.png");
			muphic.DrawManager.Regist(read.ToString(), 403, 15, "image\\Score\\button\\yobidasu.png", "image\\score\\button\\yobidasu_on.png");
			muphic.DrawManager.Regist(write.ToString(), 576, 16, "image\\Score\\button\\nokosu.png", "image\\score\\button\\nokosu_on.png");
			muphic.DrawManager.Regist(up.ToString(), 704, 126, "image\\Score\\button\\up.png", "image\\score\\button\\up.png");
			muphic.DrawManager.Regist(down.ToString(), 704, 488, "image\\Score\\button\\down.png", "image\\score\\button\\down.png");
			muphic.DrawManager.Regist(scorewindow.ToString(), 33, 124, "image\\Score\\score_haikei.png");
			muphic.DrawManager.Regist(scores.ToString(), 35, 533, "image\\Score\\scores.png");
			
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(back);
			BaseList.Add(read);
			BaseList.Add(write);
			BaseList.Add(scorewindow);
			BaseList.Add(up);
			BaseList.Add(down);
			BaseList.Add(scores);
			BaseList.Add(scoremain);
		}

		public override void Draw()
		{
			base.Draw ();
			if(isSelectDialog)
			{
				sedialog.Draw();
			}
			else if(isSaveDialog)
			{
				sadialog.Draw();
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
			if(isSelectDialog)
			{
				sedialog.MouseMove(p);
			}
			else if(isSaveDialog)
			{
				sadialog.MouseMove(p);
			}
		}

		public override void Click(System.Drawing.Point p)
		{
			if(isSelectDialog)
			{
				sedialog.Click(p);
			}
			else if(isSaveDialog)
			{
				sadialog.Click(p);
			}
			else
			{
				base.Click (p);
			}
		}
	}
		
	*/
	#endregion
		
	#region XGA (ver.0.9.0�`)

	/// <summary>
	/// �y�����Screen�N���X
	/// </summary>
	public class ScoreScreen : Screen
	{
		public Screen parent;				// �eScreen�N���X
		public ScreenMode ParentScreenMode;	// �eScreen��ScreenMode��ێ�

		public ArrayList AnimalList = new ArrayList();		// �������X�g�̃R�s�[

		public BackButton back;				// ���ǂ�{�^��
		public ReadButton read;				// ��т����{�^��
		public WriteButton write;			// �̂����{�^��6
		public UpScrollButton up;			// ��X�N���[���{�^��
		public DownScrollButton down;		// ���X�N���[���{�^��
		public ScoreWindow scorewindow;		// �y���\���E�B���h�E
		public ScoreButtons scores;			// �e�����{�^�����Ǘ�
		public ScoreMain scoremain;			// �y���{��
		public TitleScreen title;			// �薼���̓_�C�A���O
		public ScoreSelectDialog sedialog;	// �y���ǂݍ��݃_�C�A���O
		public ScoreSaveDialog sadialog;	// �y���ۑ��_�C�A���O
		public ScorePrintDialog pdialog;	// ����m�F�_�C�A���O
		public PrintButton print;			// �y�����
		public ScoreTitleArea tarea;		// �Ȗ��\��
		public DeleteTexture delete;		// �摜�J���p
		
		public bool isSelectDialog = false;	// �ǂݍ��݃_�C�A���O���J����Ă��邩�ǂ����̃t���O
		public bool isSaveDialog = false;	// �ۑ��_�C�A���O���J����Ă��邩�ǂ����̃t���O
		public bool isPrintDialog = false;	// ����m�F�_�C�A���O���J����Ă��邩�ǂ����̃t���O
		public bool isTitleScreen = false;	// �薼���͉�ʂ��ǂ����̃t���O

		/// <summary>
		/// �y�����Screen�N���X �R���X�g���N�^
		/// �ЂƂ�ŉ��y���[�h����̌Ăяo�����Ɏg�p
		/// </summary>
		/// <param name="screen">�ЂƂ�ŉ��y(OneScreen)�N���X</param>
		public ScoreScreen(OneScreen onescreen)
		{
			this.parent = (Screen)onescreen;				// parent��Screen�^�Ȃ̂ŃL���X�g���Ďg�p
			this.ParentScreenMode = ScreenMode.OneScreen;	// �eScreen��ScreenMode���ЂƂ�ŉ��y���[�h�ɐݒ�
			this.ScoreScreenPreparation();					// �e���i�̃C���X�^���X����
		}
		
		/// <summary>
		/// �y�����Screen�N���X �R���X�g���N�^
		/// �Ȃ��ĉ��y���[�h����̌Ăяo�����Ɏg�p
		/// </summary>
		/// <param name="screen">�Ȃ��ĉ��y(LinkScreen)�N���X</param>
		public ScoreScreen(LinkScreen linkscreen)
		{
			this.parent = (Screen)linkscreen;				// parent��Screen�^�Ȃ̂ŃL���X�g���Ďg�p
			this.ParentScreenMode = ScreenMode.LinkScreen;	// �eScreen��ScreenMode���ЂƂ�ŉ��y���[�h�ɐݒ�
			this.ScoreScreenPreparation();					// �e���i�̃C���X�^���X����
		}

		/// <summary>
		/// �y�����Screen�N���X �R���X�g���N�^
		/// �Ȃ��ĉ��y���[�h����̌Ăяo�����Ɏg�p
		/// </summary>
		/// <param name="linkmakescreen">�Ȃ��ĉ��y(LinkMakeScreen)�N���X</param>
		public ScoreScreen(LinkMakeScreen linkscreen)
		{
			this.parent = (Screen)linkscreen;					// parent��Screen�^�Ȃ̂ŃL���X�g���Ďg�p
			this.ParentScreenMode = ScreenMode.LinkMakeScreen;	// �eScreen��ScreenMode���ЂƂ�ŉ��y���[�h�ɐݒ�
			this.ScoreScreenPreparation();						// �e���i�̃C���X�^���X����
		}
		
		/// <summary>
		/// �y�����Screen�N���X �R���X�g���N�^
		/// ���̂����艹�y���[�h����̌Ăяo�����Ɏg�p
		/// </summary>
		/// <param name="screen">���̂����艹�y(StoryScreen)�N���X</param>
		public ScoreScreen(StoryScreen storyscreen)
		{
			this.parent = (Screen)storyscreen;					// parent��Screen�^�Ȃ̂ŃL���X�g���Ďg�p
			this.ParentScreenMode = ScreenMode.StoryScreen;		// �eScreen��ScreenMode���ЂƂ�ŉ��y���[�h�ɐݒ�
			this.ScoreScreenPreparation();						// �e���i�̃C���X�^���X����
		}
		
		
		/// <summary>
		/// �y����ʃR���X�g���N�^�Q
		/// �e���i�̃C���X�^���X���Ɠo�^���s��
		/// </summary>
		private void ScoreScreenPreparation()
		{			
			System.Console.WriteLine("�y����ʌĂяo����:{0}", this.ParentScreenMode);
			this.AnimalList.Clear();

			// �Ăяo����Screen�ɖ� ���ꂼ���AnimalList���R�s�[����
			switch(this.ParentScreenMode)
			{
				case ScreenMode.OneScreen:
					this.AnimalList = ((OneScreen)this.parent).score.Animals.AnimalList;
					break;
				case ScreenMode.LinkScreen:
					for(int i=0; i<((LinkScreen)this.parent).score.AnimalList.Count; i++)
					{
						// LinkScreen��AnimalList����v�f���ЂƂЂƂ��o���A��������"Sheep"�ɂ��Ď蓮�R�s�[
						Animal temp = (Animal)(((LinkScreen)this.parent).score.AnimalList[i]);
						Animal animal = new Animal(temp.place, temp.code, "Sheep");
						this.AnimalList.Add(animal);
					}
					// this.AnimalList = ((LinkScreen)this.parent).score.AnimalList;
					break;
				case ScreenMode.LinkMakeScreen:
					for(int i=0;i<((LinkMakeScreen)this.parent).score.Animals.AnimalList.Count; i++)
					{
						// LinkMakeScreen��Animals��AnimalList����v�f���ЂƂЂƂ��o���A��������"Sheep"�ɂ��Ď蓮�R�s�[
						Animal temp = (Animal)(((LinkMakeScreen)this.parent).score.Animals[i]);
						Animal animal = new Animal(temp.place, temp.code, "Sheep");
						this.AnimalList.Add(animal);
					}
					break;
				case ScreenMode.StoryScreen:
					this.AnimalList = ((StoryScreen)this.parent).score.Animals.AnimalList;
					break;
				default:
					break;
			}

			// NowLoading �Ăяo��
			DrawManager.BeginRegist(67);

			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			back = new BackButton(this);
			//read = new ReadButton(this);
			//write = new WriteButton(this);
			up = new UpScrollButton(this);
			down = new DownScrollButton(this);
			scorewindow = new ScoreWindow(this);
			scores = new ScoreButtons(this);
			scoremain = new ScoreMain(this);
			//title = new TitleScreen(this);
			//sedialog = new ScoreSelectDialog(this);
			//sadialog = new ScoreSaveDialog(this);
			print = new PrintButton(this);
			pdialog = new ScorePrintDialog(this);
			tarea = new ScoreTitleArea(this);
			//delete = new DeleteTexture(this);


			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\ScoreXGA\\background.png");	// �{���̔w�i
			//muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\ScoreXGA\\scoreXGA.png");	// �{�^���z�u���{�w�i
			muphic.DrawManager.Regist(back.ToString(), 15, 10, "image\\ScoreXGA\\button\\back_off.png", "image\\ScoreXGA\\button\\back_on.png");
			//muphic.DrawManager.Regist(read.ToString(), 489, 24, "image\\ScoreXGA\\button\\load_off.png", "image\\ScoreXGA\\button\\load_on.png");
			//muphic.DrawManager.Regist(write.ToString(), 655, 24, "image\\ScoreXGA\\button\\save_off.png", "image\\ScoreXGA\\button\\save_on.png");
			muphic.DrawManager.Regist(print.ToString(), 821, 24, "image\\ScoreXGA\\button\\print_off.png", "image\\ScoreXGA\\button\\print_on.png");
			muphic.DrawManager.Regist(up.ToString(), 936, 142, "image\\ScoreXGA\\scrool\\up.png");
			muphic.DrawManager.Regist(down.ToString(), 936, 628, "image\\ScoreXGA\\scrool\\down.png");
			muphic.DrawManager.Regist(scorewindow.ToString(), 37, 125, "image\\ScoreXGA\\score_back.png", "image\\ScoreXGA\\score_back_.png");
			muphic.DrawManager.Regist(scores.ToString(), 75, 680, "image\\ScoreXGA\\scores.png");
			
			// NowLoading �I���
			DrawManager.EndRegist();
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(back);
			//BaseList.Add(read);
			//BaseList.Add(write);
			BaseList.Add(print);
			BaseList.Add(scorewindow);
			BaseList.Add(up);
			BaseList.Add(down);
			BaseList.Add(scores);
			BaseList.Add(scoremain);
		}

		public override void Draw()
		{
			base.Draw ();
			tarea.Draw();
			if(isTitleScreen)
			{
				title.Draw();
			}
			else if(isSelectDialog)
			{
				sedialog.Draw();
			}
			else if(isSaveDialog)
			{
				sadialog.Draw();
			}
			else if(isPrintDialog)
			{
				pdialog.Draw();
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			if(isTitleScreen)
			{
				title.MouseMove(p);
			}
			else if(isSelectDialog)
			{
				sedialog.MouseMove(p);
			}
			else if(isSaveDialog)
			{
				sadialog.MouseMove(p);
			}
			else if(isPrintDialog)
			{
				pdialog.MouseMove(p);
			}
			else
			{
				base.MouseMove (p);
			}
		}

		public override void Click(System.Drawing.Point p)
		{
			if(isTitleScreen)
			{
				title.Click(p);
			}
			else if(isSelectDialog)
			{
				sedialog.Click(p);
			}
			else if(isSaveDialog)
			{
				sadialog.Click(p);
			}
			else if(isPrintDialog)
			{
				pdialog.Click(p);
			}
			else
			{
				base.Click (p);
			}
		}
	}
	
	#endregion
}
