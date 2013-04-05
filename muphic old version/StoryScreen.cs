using System;
using System.Collections;
using System.Drawing;
using muphic.Story;

namespace muphic
{
	public enum StoryScreenMode {StoryScreen, ScoreScreen, AllClearDialog, VoiceRegistDialog, VoiceRegistOneMoreDialog};
	/// <summary>
	/// Story �̊T�v�̐����ł��B
	/// </summary>
	public class StoryScreen : Screen
	{
		public MakeStoryScreen parent;
		public Score score;
		public House house;
		public Tempo tempo;
		public BackButton back;
		public RestartButton restart;
		public StartStopButton startstop;
		public ScoreButton scorebutton;
		public Window window;
		public StoryButtons stories;
		public Tsuibi tsuibi;

		public Screen Screen;

		private StoryScreenMode storyscreenmode;
		//public String StoryTitle;
		//public ArrayList SlideList;
		public const int PageNum = 10;								//�܂�A�͈͂Ƃ��Ă�0�`9
		public int NowPage;											//���݉����ڂ̃X���C�h�̉��y����Ȃ��Ă��邩
/*
		public bool IsMakeStory
		{
			get
			{
				return isMakeStory;
			}
			set
			{
				isMakeStory = value;
				if(isMakeStory)
				{
					makestory = new MakeStoryScreen(this, this.PictureStory);
				}
				else
				{
					makestory = null;
				}
			}
		}*/

		/// <summary>
		/// ���̂����艹�y��ʂ�ScreenMode
		/// �y����ʂ╨��쐬��ʁA�_�C�A���O�Ȃǂւ̐؂�ւ��Ɏg�p
		/// </summary>
		public StoryScreenMode StoryScreenMode
		{
			get
			{
				return storyscreenmode;
			}
			set
			{
				storyscreenmode = value;
				switch(value)
				{
					case StoryScreenMode.StoryScreen:
						//�q�̃E�B���h�E�͕\�������A�������g��`�悷��
						Screen = null;
						break;
					case StoryScreenMode.ScoreScreen:
						//ScoreScreen���C���X�^���X�����A�������`�悷��
						Screen = new ScoreScreen(this);
						break;
					case StoryScreenMode.AllClearDialog:
						Screen = new muphic.Common.AllClearDialog(this);
						break;
					case StoryScreenMode.VoiceRegistDialog:
						Screen = new VoiceRegistDialog(this);
						break;
					case StoryScreenMode.VoiceRegistOneMoreDialog:
						Screen = new VoiceRegistOneMoreDialog(this);
						break;
					default:
						//�Ƃ肠����StoryScreen�ɕ`���߂�
						Screen = null;
						break;
				}
			}
		}
		//public StoryScreen(TopScreen ts)
		public StoryScreen(MakeStoryScreen ts, MakeStory.Slide slide, int num)
		{
			this.parent = ts;
			this.NowPage = num;
			this.StoryScreenMode = StoryScreenMode.StoryScreen;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			DrawManager.BeginRegist(77);
			score = new Score(this);
			house = new House(this);
			tempo = new Tempo(this);
			back = new BackButton(this);
			restart = new RestartButton(this);
			startstop = new StartStopButton(this);
			scorebutton = new ScoreButton(this);
			window = new Window(this, num);
			tsuibi = new Tsuibi(this);
			stories = new StoryButtons(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\one\\background.png");
			muphic.DrawManager.Regist(score.ToString(), 102, 180, "image\\one\\parts\\road.png");
			muphic.DrawManager.Regist(house.ToString(), 15, 137, "image\\one\\parts\\house.png");
			muphic.DrawManager.Regist(tempo.ToString(), 120,120,/*147, 118,*/ "image\\one\\button\\tempo\\tempo.png");
			muphic.DrawManager.Regist(back.ToString(), 8, 5, "image\\Story\\button\\back_off.png", "image\\Story\\button\\back_on.png");
			muphic.DrawManager.Regist(restart.ToString(), 270, 12, "image\\one\\button\\restart\\restart_off.png", "image\\one\\button\\restart\\restart_on.png");
			muphic.DrawManager.Regist(startstop.ToString(), 167, 12, new String[] {"image\\one\\button\\start_stop\\start_off.png", "image\\one\\button\\start_stop\\start_on.png", "image\\one\\button\\start_stop\\stop_off.png", "image\\one\\button\\start_stop\\stop_on.png"});
			muphic.DrawManager.Regist(scorebutton.ToString(), 814, 197, "image\\one\\button\\gakuhu\\gakuhu_off.png", "image\\one\\button\\gakuhu\\gakuhu_on.png");
			muphic.DrawManager.Regist(window.ToString(), 760, 10, "image\\one\\parts\\story_window.png");
			muphic.DrawManager.Regist(stories.ToString(), 896, 190, "image\\one\\button\\animal\\buttons.png");//896,199 before 679,176
			muphic.DrawManager.Regist(tsuibi.ToString(), 0, 0, new String[9] 
					{
						"image\\MakeStory\\none.png",
						"image\\one\\button\\animal\\bird\\bird.png", "image\\one\\button\\animal\\dog\\dog.png",
						"image\\one\\button\\animal\\pig\\pig.png", "image\\one\\button\\animal\\rabbit\\rabbit.png",
						"image\\one\\button\\animal\\sheep\\sheep.png", "image\\one\\button\\animal\\cat\\cat.png",
						"image\\one\\button\\animal\\voice\\voice.png", "image\\common\\clear.png"});

			DrawManager.EndRegist();
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(house);
			BaseList.Add(score);
			BaseList.Add(tempo);
			BaseList.Add(back);
			BaseList.Add(restart);
			BaseList.Add(startstop);
			BaseList.Add(scorebutton);
			BaseList.Add(window);
			BaseList.Add(stories);						//tsuibi�͓o�^���Ȃ����Ƃɒ��ӁB

			
			///////////////////////////////////////////////////////////////////
			//�X���C�h�ɂ���y���f�[�^�̎擾
			///////////////////////////////////////////////////////////////////
			score.Animals.AnimalList = slide.AnimalList;
			tempo.TempoMode = slide.tempo;
		}

		public override void Click(System.Drawing.Point p)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:
					ClickStoryScreen(p);
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.Click(p);						//ScoreScreen�̂Ƃ����A�S�Ă����瑤�̏���
					break;
				default:
					break;

			}
		}

		private void ClickStoryScreen(Point p)
		{
			if(score.isPlay)						//----------���ˏI���̂��m�点----------
			{
				String[] StoryPermissionList = new String[] {startstop.ToString(), restart.ToString(),
															  tempo.ToString()};
				base.Click(p, StoryPermissionList);
			}
			else
			{
				base.Click(p);
			}
		}

		public override void DragBegin(System.Drawing.Point begin)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:
					if(score.isPlay == false)
					{
						base.DragBegin(begin);
					}
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.DragBegin(begin);					//ScoreScreen�̂Ƃ����A�S�Ă����瑤�̏���
					break;
				default:
					break;

			}
		}

		public override void Drag(System.Drawing.Point begin, System.Drawing.Point p)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:	
					if(score.isPlay == false)
					{
						base.Drag(begin, p);
					}
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.Drag(begin, p);					//ScoreScreen�̂Ƃ����A�S�Ă����瑤�̏���
					break;
				default:
					break;

			}
		}

		public override void DragEnd(System.Drawing.Point begin, System.Drawing.Point p)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:	
					if(score.isPlay == false)
					{
						base.DragEnd(begin, p);
					}
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.DragEnd(begin, p);					//ScoreScreen�̂Ƃ����A�S�Ă����瑤�̏���
					break;
				default:
					break;

			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:	
					base.MouseMove (p);
					tsuibi.MouseMove(p);					//tsuibi��Base�^�����AMouseMove���K�v�Ȃ̂ŁA�Ă�
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.MouseMove(p);					//ScoreScreen�̂Ƃ����A�S�Ă����瑤�̏���
					break;
				default:
					break;

			}
		}

		public override void Draw()
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:
					base.Draw();							//���i�̃f�t�H���g����
					if(tsuibi.Visible)
					{
						muphic.DrawManager.DrawCenter(tsuibi.ToString(), tsuibi.point.X, tsuibi.point.Y, tsuibi.State);
						//tsuibi��BaseList�ɓo�^���ĂȂ��̂ŁA�ʂɕ`�悷��K�v������
					}
					break;
				case StoryScreenMode.ScoreScreen:
					Screen.Draw();							//ScoreScreen�̂Ƃ����A�S�Ă����瑤�̏���
					break;
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					base.Draw();
					Screen.Draw();
					break;
				default:
					break;

			}
		}
/*
		/// <summary>
		/// Window�̂ق��ŁA�E�X�N���[���{�^���������ꂽ�Ƃ��̏����B
		/// �X���C�h�̕ύX��T���l�C���̕ύX�Ȃǂ̕ύX�����̈�ԏ�ɑ��݂���
		/// </summary>
		public void NextSlide()
		{
			if(muphic.StoryScreen.PageNum <= this.NowPage+1)			//+1�����Ƃ��Ƀy�[�W�͈̔͂𒴂���
			{
				this.ChangeSlide(muphic.StoryScreen.PageNum-1);
				return;
			}
			this.ChangeSlide(this.NowPage + 1);
		}

		/// <summary>
		/// Window�̂ق��ŁA���X�N���[���{�^���������ꂽ�Ƃ��̏����B
		/// �X���C�h�̕ύX��T���l�C���̕ύX�Ȃǂ̕ύX�����̈�ԏ�ɑ��݂���
		/// </summary>
		public void PrevSlide()
		{
			if(this.NowPage-1 < 0)									//-1�����Ƃ��Ƀy�[�W�͈̔͂𒴂���
			{
				this.ChangeSlide(0);
				return;
			}
			this.ChangeSlide(this.NowPage - 1);
		}*/
/*
		/// <summary>
		/// �X���C�h������������Ƃ��Ɏg�p���郁�\�b�h�B�K��0���ڂ���n�܂�B
		/// </summary>
		public void ChangeSlide0()
		{
			this.NowPage = 0;
			Slide s = PictureStory.Slide[this.NowPage];
			tempo.TempoMode = s.tempo;
			score.Animals.AnimalList = s.AnimalList;
			window.thumbnail.ChangeThumbnail(0);
		}

		/// <summary>
		/// �X���C�h��ύX����Ƃ��Ɏg�p���郁�\�b�h�B���������T���l�C�����ύX���Ă����
		/// </summary>
		/// <param name="next">�ύX��̃X���C�h�̃y�[�W</param>
		private void ChangeSlide(int next)
		{
			Slide s = PictureStory.Slide[this.NowPage];
			s.tempo = tempo.TempoMode;											//���݂̃e���|�����݂̃X���C�h�̂��̂Ƃ��ĕۑ�
			s.AnimalList = score.Animals.AnimalList;							//���݂̊y�������݂̃X���C�h�̂��̂Ƃ��ĕۑ�
			PictureStory.Slide[this.NowPage] = s;

			Slide n = PictureStory.Slide[next];
			score.Animals.AnimalList = n.AnimalList;							//�V�����y�[�W�̊y�������݂̊y���Ƃ���
			tempo.TempoMode = n.tempo;									//�e���|���ύX����
			window.thumbnail.ChangeThumbnail(next);						//�E�B���h�E�̃T���l�����ύX����
			this.NowPage = next;
		}*/
	}
}
