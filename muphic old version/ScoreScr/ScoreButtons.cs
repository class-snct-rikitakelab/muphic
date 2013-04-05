using System;
using muphic.ScoreScr.UnderButtons;

namespace muphic.ScoreScr
{
	#region SVGA (�`ver.0.8.8)
	/*
		
	public enum AnimalScoreMode{None, All, Sheep, Rabbit, Bird, Dog, Pig, Voice}
	
	/// <summary>
	/// ScoreButtons �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreButtons : Screen
	{
		public ScoreScreen parent;			// �eScreen
		public AnimalScoreMode nowClick;	// ���ݑI�����Ă��铮��
											// �e�����{�^���ɂ��ω�����
		public AllButton all;
		public SheepButton sheep;
		public RabbitButton rabbit;
		public BirdButton bird;
		public DogButton dog;
		public PigButton pig;
		public VoiceButton voice;

		public AnimalScoreMode NowClick
		{
			get
			{
				return nowClick;
			}
			set
			{
				nowClick = value;
				switch(value)
				{
					case AnimalScoreMode.All:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.All;
						break;
					case AnimalScoreMode.Sheep:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Sheep;
						break;
					case AnimalScoreMode.Rabbit:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Rabbit;
						break;
					case AnimalScoreMode.Bird:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Bird;
						break;
					case AnimalScoreMode.Dog:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Dog;
						break;
					case AnimalScoreMode.Pig:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Pig;
						break;
					case AnimalScoreMode.Voice:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Voice;
						break;
					default:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.None;
						break;
				}

				// ScoreMain�̃I�t�Z�b�g���N���A���`�惊�X�g���Đ�������
				parent.scoremain.ClearOffset();
				parent.scoremain.ReDraw();
			}
		}
		
		public ScoreButtons(ScoreScreen screen)
		{
			parent = screen;	// �eScreen�̐ݒ�

			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			all = new AllButton(this);
			sheep = new SheepButton(this);
			rabbit = new RabbitButton(this);
			bird = new BirdButton(this);
			dog = new DogButton(this);
			pig = new PigButton(this);
			voice = new VoiceButton(this);


			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			int x = 40;			// x���W�l�i��ԍ��̉摜�E���ꂪ��j
			int y = 540;		// y���W�l
			int xplus = 104;	// �{�^��1����x���W����
			muphic.DrawManager.Regist(all.ToString(), x+(0*xplus), y, "image\\score\\score_button\\all_off.png", "image\\score\\score_button\\all_on.png");
			muphic.DrawManager.Regist(sheep.ToString(), x+(1*xplus), y, "image\\score\\score_button\\sheep_off.png", "image\\score\\score_button\\sheep_on.png");
			muphic.DrawManager.Regist(rabbit.ToString(), x+(2*xplus), y, "image\\score\\score_button\\rabbit_off.png", "image\\score\\score_button\\rabbit_on.png");
			muphic.DrawManager.Regist(bird.ToString(), x+(3*xplus), y, "image\\score\\score_button\\bird_off.png", "image\\score\\score_button\\bird_on.png");
			muphic.DrawManager.Regist(dog.ToString(), x+(4*xplus), y, "image\\score\\score_button\\dog_off.png", "image\\score\\score_button\\dog_on.png");
			muphic.DrawManager.Regist(pig.ToString(), x+(5*xplus), y, "image\\score\\score_button\\pig_off.png", "image\\score\\score_button\\pig_on.png");
			muphic.DrawManager.Regist(voice.ToString(), x+(6*xplus), y, "image\\score\\score_button\\voice_off.png", "image\\score\\score_button\\voice_on.png");

			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(all);
			BaseList.Add(sheep);
			BaseList.Add(rabbit);
			BaseList.Add(bird);
			BaseList.Add(dog);
			BaseList.Add(pig);
			BaseList.Add(voice);
		}

		public override void Click(System.Drawing.Point p)
		{
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).State = 0;							//�{���̃N���b�N�������s���O��
			}															//���ׂĂ̗v�f���N���b�N���Ă��Ȃ���Ԃɖ߂�
			base.Click (p);
		}
	}
	
	*/
	#endregion
		
	#region XGA (ver.0.9.0�`)
		
	public enum AnimalScoreMode{None, All, Sheep, Rabbit, Bird, Dog, Pig, Cat, Voice}
	
	/// <summary>
	/// ScoreButtons �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreButtons : Screen
	{
		public ScoreScreen parent;			// �eScreen
		public AnimalScoreMode nowClick;	// ���ݑI�����Ă��铮��
											// �e�����{�^���ɂ��ω�����
		public AllButton all;
		public SheepButton sheep;
		public RabbitButton rabbit;
		public BirdButton bird;
		public DogButton dog;
		public PigButton pig;
		public CatButton cat;
		public VoiceButton voice;

		public AnimalScoreMode NowClick
		{
			get
			{
				return nowClick;
			}
			set
			{
				AllClear();
				nowClick = value;
				switch(value)
				{
					case AnimalScoreMode.All:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.All;
						break;
					case AnimalScoreMode.Sheep:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Sheep;
						break;
					case AnimalScoreMode.Rabbit:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Rabbit;
						break;
					case AnimalScoreMode.Bird:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Bird;
						break;
					case AnimalScoreMode.Dog:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Dog;
						break;
					case AnimalScoreMode.Pig:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Pig;
						break;
					case AnimalScoreMode.Cat:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Cat;
						break;
					case AnimalScoreMode.Voice:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.Voice;
						break;
					default:
						parent.scoremain.nowScore = muphic.ScoreScr.AnimalScoreMode.None;
						break;
				}

				// ScoreMain�̃I�t�Z�b�g���N���A���`�惊�X�g���Đ�������
				parent.scoremain.ClearOffset();
				parent.scoremain.ReDraw();
			}
		}
		
		public ScoreButtons(ScoreScreen screen)
		{
			parent = screen;	// �eScreen�̐ݒ�

			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			all = new AllButton(this);
			sheep = new SheepButton(this);
			rabbit = new RabbitButton(this);
			bird = new BirdButton(this);
			dog = new DogButton(this);
			pig = new PigButton(this);
			cat = new CatButton(this);
			voice = new VoiceButton(this);


			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			int x = 99;			// x���W�l�i��ԍ��̉摜�E���ꂪ��j
			int y = 694;		// y���W�l
			int xplus = 105;	// �{�^��1����x���W����
			muphic.DrawManager.Regist(all.ToString(), x+(0*xplus), y, "image\\ScoreXGA\\score_select\\all_off.png", "image\\ScoreXGA\\score_select\\all_on.png");
			muphic.DrawManager.Regist(sheep.ToString(), x+(1*xplus), y, "image\\ScoreXGA\\score_select\\sheep_off.png", "image\\ScoreXGA\\score_select\\sheep_on.png");
			muphic.DrawManager.Regist(rabbit.ToString(), x+(2*xplus), y, "image\\ScoreXGA\\score_select\\rabbit_off.png", "image\\ScoreXGA\\score_select\\rabbit_on.png");
			muphic.DrawManager.Regist(bird.ToString(), x+(3*xplus), y, "image\\ScoreXGA\\score_select\\bird_off.png", "image\\ScoreXGA\\score_select\\bird_on.png");
			muphic.DrawManager.Regist(dog.ToString(), x+(4*xplus), y, "image\\ScoreXGA\\score_select\\dog_off.png", "image\\ScoreXGA\\score_select\\dog_on.png");
			muphic.DrawManager.Regist(pig.ToString(), x+(5*xplus), y, "image\\ScoreXGA\\score_select\\pig_off.png", "image\\ScoreXGA\\score_select\\pig_on.png");
			muphic.DrawManager.Regist(cat.ToString(), x+(6*xplus), y, "image\\ScoreXGA\\score_select\\cat_off.png", "image\\ScoreXGA\\score_select\\cat_on.png");
			muphic.DrawManager.Regist(voice.ToString(), x+(7*xplus), y, "image\\ScoreXGA\\score_select\\voice_off.png", "image\\ScoreXGA\\score_select\\voice_on.png");

			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(all);
			BaseList.Add(sheep);
			BaseList.Add(rabbit);
			BaseList.Add(bird);
			BaseList.Add(dog);
			BaseList.Add(pig);
			BaseList.Add(cat);
			BaseList.Add(voice);
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).MouseLeave();
			}
		}

		public void AllClear()
		{
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).State = 0;							//�{���̃N���b�N�������s���O��
			}															//���ׂĂ̗v�f���N���b�N���Ă��Ȃ���Ԃɖ߂�
		}

	}
	
	#endregion
}
