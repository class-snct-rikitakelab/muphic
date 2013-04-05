using System;
using muphic.Top;
using muphic.Tutorial;

namespace muphic
{
	public enum ScreenMode{TopScreen, OneScreen, LinkScreen, LinkMakeScreen, StoryScreen, ScoreScreen, TutorialScreen};

	/// <summary>
	/// TopScreen �̊T�v�̐����ł��B
	/// </summary>
	public class TopScreen : Screen
	{
		public MainScreen parent;
		public TutorialMain tutorialparent;
		public OneButton onebutton;
		public LinkButton linkbutton;
		public StoryButton storybutton;
		public TutorialButton tutorialbutton;
		public EndButton endbutton;
		public Screen Screen;
		private ScreenMode screenmode;


		///////////////////////////////////////////////////////////////////////
		//�v���p�e�B�̐錾
		///////////////////////////////////////////////////////////////////////
		public ScreenMode ScreenMode
		{
			get
			{
				return screenmode;
			}
			set
			{
				screenmode = value;
				switch(value)
				{
					case muphic.ScreenMode.TopScreen:
						Screen = null;									//�q�̃E�B���h�E�͕\�������A�������g��`�悷��
						break;
					case muphic.ScreenMode.OneScreen:
						Screen = new OneScreen(this);					//OneScreen���C���X�^���X�����A�������`�悷��
						break;
					case muphic.ScreenMode.LinkScreen:
						Screen = new LinkTop(this);
						break;
					case muphic.ScreenMode.LinkMakeScreen:
						Screen = new LinkTop(this);
						break;
					case muphic.ScreenMode.StoryScreen:
						Screen = new MakeStoryScreen(this);
						break;
					case muphic.ScreenMode.TutorialScreen:
						Screen = new TutorialScreen(this);
						break;
					default:
						Screen = null;									//�Ƃ肠����TopScreen�ɕ`���߂�
						break;
				}
			}
		}


		public TopScreen(MainScreen main)
		{
			muphic.Common.TutorialStatus.initTutorialStatus();
			//muphic.Common.TutorialStatus.test();
			this.parent = main;
			this.TopScreenPreparation();
		}
		public TopScreen(TutorialMain tscreen)
		{
			this.tutorialparent = tscreen;
			this.TopScreenPreparation();
		}

		public void TopScreenPreparation()
		{
			this.ScreenMode = muphic.ScreenMode.TopScreen;

			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			onebutton = new OneButton(this);
			linkbutton = new LinkButton(this);
			storybutton = new StoryButton(this);
			tutorialbutton = new TutorialButton(this);
			endbutton = new EndButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\top\\background.png");
			//muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\top\\topXGA.png");
			muphic.DrawManager.Regist(onebutton.ToString(), 458, 350, new string[] {"image\\top\\button\\one\\one_off.png", "image\\top\\button\\one\\one_on.png", "image\\top\\button\\one\\one_non.png"});
			muphic.DrawManager.Regist(linkbutton.ToString(), 750, 350, new string[] {"image\\top\\button\\link\\link_off.png", "image\\top\\button\\link\\link_on.png", "image\\top\\button\\link\\link_non.png"});
			muphic.DrawManager.Regist(storybutton.ToString(), 605, 453, new string[] {"image\\top\\button\\story\\story_off.png", "image\\top\\button\\story\\story_on.png", "image\\top\\button\\story\\story_non.png"});
			muphic.DrawManager.Regist(tutorialbutton.ToString(), 549, 272, new string[] {"image\\top\\button\\tutorial\\startmuphic_off.png", "image\\top\\button\\tutorial\\startmuphic_on.png", "image\\top\\button\\tutorial\\startmuphic_non.png"});
			muphic.DrawManager.Regist(endbutton.ToString(), 600, 630, new string[] {"image\\top\\button\\end\\end_off.png", "image\\top\\button\\end\\end_on.png", "image\\top\\button\\end\\end_non.png"});
			DrawManager.Regist("Nowloading", 222, 173, "image\\Nowloading\\Nowloading.png");
			DrawManager.Regist("Nowloading_bak", 0, 0, "image\\Nowloading\\Nowloading_bak.png");
			DrawManager.Regist("Nowloading_all", 0, 0, "image\\Nowloading\\Nowloading_all.png");
			DrawManager.Regist("1px", 0, 0, "image\\Nowloading\\1px.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(onebutton);
			BaseList.Add(linkbutton);
			BaseList.Add(storybutton);
			BaseList.Add(tutorialbutton);
			BaseList.Add(endbutton);
			
			muphic.Common.AutoSave.initAutoSave();
			muphic.Common.AutoSave.onAutoSave();
		}

		public override void Click(System.Drawing.Point p)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)	//TopScreen��Click���Ă�
			{
				base.Click (p);
			}
			else
			{
				Screen.Click(p);								//TopScreen����Ȃ��Ȃ�A������̂ق���Click���Ă�
			}
		}

		public override void Draw()
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)	//TopScreen��`�悷��
			{
				base.Draw ();
			}
			else
			{
				Screen.Draw();									//TopScreen����Ȃ��Ȃ�A������̂ق��̕`�������
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)
			{
				base.MouseMove (p);								//TopScreen��MouseMove���Ă�
			}
			else
			{
				Screen.MouseMove(p);							//TopScreen����Ȃ��Ȃ�A������̂ق���MouseMove���Ă�
			}
		}

		public override void DragBegin(System.Drawing.Point begin)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)
			{
				base.DragBegin(begin);							//TopScreen��DragBegin���Ă�
			}
			else
			{
				Screen.DragBegin(begin);						//TopScreen����Ȃ��Ȃ�A������̂ق���DragBegin���Ă�
			}
		}

		public override void DragEnd(System.Drawing.Point begin, System.Drawing.Point p)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)
			{
				base.DragEnd(begin, p);							//TopScreen��DragEnd���Ă�
			}
			else
			{
				Screen.DragEnd(begin, p);						//TopScreen����Ȃ��Ȃ�A������̂ق���Drag���Ă�
			}
		}

		public override void Drag(System.Drawing.Point begin, System.Drawing.Point p)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)
			{
				base.Drag(begin, p);							//TopScreen��Drag���Ă�
			}
			else
			{
				Screen.Drag(begin, p);							//TopScreen����Ȃ��Ȃ�A������̂ق���Drag���Ă�
			}
		}
	}
}