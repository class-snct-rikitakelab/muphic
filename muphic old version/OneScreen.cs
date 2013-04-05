using System;
using System.Drawing;
using muphic.One;
using muphic.Titlemode;

namespace muphic
{
	public enum OneScreenMode {OneScreen, ScoreScreen, TitleScreen, VoiceRegistDialog, FileSelectDialog, FileSaveDialog, AllClearDialog, VoiceRegistDialogOneMore};
	/// <summary>
	/// OneScreen �̊T�v�̐����ł��B
	/// </summary>
	public class OneScreen : Screen
	{
		public TopScreen parent;
		public Screen Screen;
		private OneScreenMode onescreenmode;
		
		public Score score;
		public House house;
		public Tempo tempo;
		public BackButton back;
		public RestartButton restart;
		public StartStopButton startstop;
		public ScoreButton scorebutton;
		public Window window;
		public OneButtons ones;
		public Tsuibi tsuibi;
		
		// �ȉ��ǉ������t�B�[���h
		public string ScoreTitle;			// �Ȗ�
		public TitleButton titlebutton;		// �薼�{�^��
		public TitleForm titleform;			// �薼�\���̈�
		public SaveButton savebutton;		// �ۑ��{�^��
		public SelectButton selectbutton;	// �ǂݍ��݃{�^��
		//public TitleScreen title;			// �薼���̓_�C�A���O
		public ScoreSelectDialog sedialog;	// �y���ǂݍ��݃_�C�A���O
		public ScoreSaveDialog sadialog;	// �y���ۑ��_�C�A���O
		
		public bool isTitleScreen = false;		// �薼���͉�ʂ��ǂ����̃t���O
		public bool isSelectDialog = false;		// �ǂݍ��݃_�C�A���O���J����Ă��邩�ǂ����̃t���O
		public bool isSaveDialog = false;		// �ۑ��_�C�A���O���J����Ă��邩�ǂ����̃t���O
		public bool isAllClearDialog = false;
		
		///////////////////////////////////////////////////////////////////////
		//�v���p�e�B�̐錾
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �ЂƂ�ŉ��y��ʂ�ScreenMode
		/// ��Ɋy����ʂւ̐؂�ւ��Ɏg�p
		/// </summary>
		public OneScreenMode OneScreenMode
		{
			get
			{
				return onescreenmode;
			}
			set
			{
				onescreenmode = value;
				this.isSaveDialog = false;		// �Ƃ肠�����_�C�A���O�̕\���t���O���������Ƃ�
				this.isSelectDialog = false;	// �Ƃ肠�����_�C�A���O�̕\���t���O���������Ƃ�
				switch(value)
				{
					case OneScreenMode.OneScreen:
						//�q�̃E�B���h�E�͕\�������A�������g��`�悷��
						Screen = null;
						break;
					case OneScreenMode.ScoreScreen:
						// ScoreScreen���C���X�^���X�����A�������`�悷��
						Screen = new ScoreScreen(this);
						break;
					case OneScreenMode.TitleScreen:
						// TitleScreen���C���X�^���X�����A�������`�悷��
						DrawManager.BeginRegist(138);
						Screen = new TitleScreen(this);
						DrawManager.EndRegist();
						break;
					case OneScreenMode.VoiceRegistDialog:
						Screen = new VoiceRegistDialog(this);
						break;
					case OneScreenMode.FileSelectDialog:
						// �t�@�C���ǂݍ��݃_�C�A���O���C���X�^���X�����A�������`�悷��
						Screen = new ScoreSelectDialog(this);
						this.isSelectDialog = true;
						break;
					case OneScreenMode.FileSaveDialog:
						// �t�@�C���������݃_�C�A���O���C���X�^���X�����A�������`�悷��
						Screen = new ScoreSaveDialog(this);
						this.isSaveDialog = true;
						break;
					case OneScreenMode.AllClearDialog:
						Screen = new muphic.Common.AllClearDialog(this);
						break;
					case OneScreenMode.VoiceRegistDialogOneMore:
						Screen = new muphic.One.VoiceRegistOneMoreDialog(this);
						break;
					default:
						//�Ƃ肠����OneScreen�ɕ`���߂�
						Screen = null;
						break;
				}
			}
		}

		public OneScreen(TopScreen ts)
		{
			this.parent = ts;
			this.OneScreenMode = muphic.OneScreenMode.OneScreen;
			this.ScoreTitle = null;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			DrawManager.BeginRegist(86);
			
			score = new Score(this);
			house = new House(this);
			tempo = new Tempo(this);
			back = new BackButton(this);
			restart = new RestartButton(this);
			startstop = new StartStopButton(this);
			scorebutton = new ScoreButton(this);
			window = new Window(this);
			ones = new OneButtons(this);
			tsuibi = new Tsuibi(this);
			
			titlebutton = new TitleButton(this);
			titleform = new TitleForm(this);
			savebutton = new SaveButton(this);
			selectbutton = new SelectButton(this);
			//title = new TitleScreen(this);
//			sedialog = new ScoreSelectDialog(this);
//			sadialog = new ScoreSaveDialog(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist("test", 0, 0, "image\\one\\oneXGA.png");
			//muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\one\\oneXGA_new.png");
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\one\\background.png");
			muphic.DrawManager.Regist(score.ToString(), 102, 180, "image\\one\\parts\\road.png");
			muphic.DrawManager.Regist(house.ToString(), 15, 137, "image\\one\\parts\\house.png");
			muphic.DrawManager.Regist(tempo.ToString(), 120, 120, "image\\one\\button\\tempo\\tempo.png");
			muphic.DrawManager.Regist(back.ToString(), 18, 18, "image\\one\\button\\back\\back_off.png", "image\\one\\button\\back\\back_on.png");
			muphic.DrawManager.Regist(restart.ToString(), 270, 12, "image\\one\\button\\restart\\restart_off.png", "image\\one\\button\\restart\\restart_on.png");
			muphic.DrawManager.Regist(startstop.ToString(), 167, 12, new String[] {"image\\one\\button\\start_stop\\start_off.png", "image\\one\\button\\start_stop\\start_on.png", "image\\one\\button\\start_stop\\stop_off.png", "image\\one\\button\\start_stop\\stop_on.png"});
			muphic.DrawManager.Regist(scorebutton.ToString(), 814, 197, "image\\one\\button\\gakuhu\\gakuhu_off.png", "image\\one\\button\\gakuhu\\gakuhu_on.png");
			muphic.DrawManager.Regist(window.ToString(), 760, 10, "image\\one\\parts\\one_window.png");
			muphic.DrawManager.Regist(ones.ToString(), 896, 190, "image\\one\\button\\animal\\buttons.png");//896,199 before 679,176
			muphic.DrawManager.Regist(tsuibi.ToString(), 0, 0, new String[9] 
					{
						"image\\MakeStory\\none.png",
						"image\\one\\button\\animal\\bird\\bird.png", "image\\one\\button\\animal\\dog\\dog.png",
						"image\\one\\button\\animal\\pig\\pig.png", "image\\one\\button\\animal\\rabbit\\rabbit.png",
						"image\\one\\button\\animal\\sheep\\sheep.png", "image\\one\\button\\animal\\cat\\cat.png",
						"image\\one\\button\\animal\\voice\\voice.png", "image\\common\\clear.png"});
			
			muphic.DrawManager.Regist(titlebutton.ToString(), 380, 17, "image\\one\\button\\title\\title_off.png", "image\\one\\button\\title\\title_on.png");
			muphic.DrawManager.Regist(titleform.ToString(), 379, 61, "image\\one\\button\\title\\titlearea.png");
			muphic.DrawManager.Regist(savebutton.ToString(), 640, 122, "image\\one\\button\\save\\save_off.png", "image\\one\\button\\save\\save_on.png");
			muphic.DrawManager.Regist(selectbutton.ToString(), 529, 123, "image\\one\\button\\select\\select_off.png", "image\\one\\button\\select\\select_on.png");
			
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
			BaseList.Add(window);
			BaseList.Add(ones);							//tsuibi�͓o�^���Ȃ����Ƃɒ��ӁB
			BaseList.Add(scorebutton);
			
			BaseList.Add(titlebutton);
			BaseList.Add(titleform);
			BaseList.Add(savebutton);
			BaseList.Add(selectbutton);
		}

		public override void Click(System.Drawing.Point p)
		{
			switch(this.OneScreenMode)
			{
				case muphic.OneScreenMode.OneScreen:
					ClickOneScreen(p);					//�Đ����̋��s������������̂ŁA���\�b�h���Ă΂���
					break;
				case muphic.OneScreenMode.ScoreScreen:
					Screen.Click(p);
					break;
				case muphic.OneScreenMode.TitleScreen:
					Screen.Click(p);
					break;
				case muphic.OneScreenMode.VoiceRegistDialog:
					Screen.Click(p);
					break;
				case muphic.OneScreenMode.FileSelectDialog:
				case muphic.OneScreenMode.FileSaveDialog:
				case muphic.OneScreenMode.AllClearDialog:
				case muphic.OneScreenMode.VoiceRegistDialogOneMore:
					Screen.Click(p);
					break;
				default:
					break;
			}
		}

		private void ClickOneScreen(Point p)
		{
			if(score.isPlay)						//----------���ˏI���̂��m�点----------
			{
				String[] OnePermissionList = new String[] {startstop.ToString(), restart.ToString(),
					tempo.ToString()};
				base.Click(p, OnePermissionList);
			}
			else
			{
				base.Click(p);
			}
		}

		public override void DragBegin(Point begin)
		{
			switch(onescreenmode)
			{
				case muphic.OneScreenMode.OneScreen:
					if(score.isPlay == false)
					{
						base.DragBegin(begin);
					}
					//tsuibi.DragBegin(begin);						//tsuibi��Base�^�����AMouseMove���K�v�Ȃ̂ŁA�Ă�
					break;
				case muphic.OneScreenMode.ScoreScreen:
					Screen.DragBegin(begin);
					break;
				case muphic.OneScreenMode.TitleScreen:
					Screen.DragBegin(begin);
					break;
				case muphic.OneScreenMode.VoiceRegistDialog:
					Screen.DragBegin(begin);
					break;
				case muphic.OneScreenMode.FileSelectDialog:
				case muphic.OneScreenMode.FileSaveDialog:
				case muphic.OneScreenMode.AllClearDialog:
				case muphic.OneScreenMode.VoiceRegistDialogOneMore:
					Screen.DragBegin(begin);
					break;
				default:
					break;
			}
		}
		
		
		public override void Drag(Point begin, Point p)
		{
			switch(onescreenmode)
			{
				case muphic.OneScreenMode.OneScreen:
					if(score.isPlay == false)
					{
						base.Drag(begin, p);
					}
					//tsuibi.Drag(begin, p);						//tsuibi��Base�^�����AMouseMove���K�v�Ȃ̂ŁA�Ă�
					break;
				case muphic.OneScreenMode.ScoreScreen:
					Screen.Drag(begin, p);
					break;
				case muphic.OneScreenMode.TitleScreen:
					Screen.Drag(begin, p);
					break;
				case muphic.OneScreenMode.VoiceRegistDialog:
					Screen.Drag(begin, p);
					break;
				case muphic.OneScreenMode.FileSelectDialog:
				case muphic.OneScreenMode.FileSaveDialog:
				case muphic.OneScreenMode.AllClearDialog:
				case muphic.OneScreenMode.VoiceRegistDialogOneMore:
					Screen.Drag(begin, p);
					break;
				default:
					break;
			}
		}
		
		public override void DragEnd(Point begin, Point p)
		{
			switch(onescreenmode)
			{
				case muphic.OneScreenMode.OneScreen:
					if(score.isPlay == false)
					{
						base.DragEnd(begin, p);
					}
					//tsuibi.DragEnd(begin, p);						//tsuibi��Base�^�����AMouseMove���K�v�Ȃ̂ŁA�Ă�
					break;
				case muphic.OneScreenMode.ScoreScreen:
					Screen.DragEnd(begin, p);
					break;
				case muphic.OneScreenMode.TitleScreen:
					Screen.DragEnd(begin, p);
					break;
				case muphic.OneScreenMode.VoiceRegistDialog:
					Screen.DragEnd(begin, p);
					break;
				case muphic.OneScreenMode.FileSelectDialog:
				case muphic.OneScreenMode.FileSaveDialog:
				case muphic.OneScreenMode.AllClearDialog:
				case muphic.OneScreenMode.VoiceRegistDialogOneMore:
					Screen.DragEnd(begin, p);
					break;
				default:
					break;
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			switch(onescreenmode)
			{
				case muphic.OneScreenMode.OneScreen:
					base.MouseMove (p);
					tsuibi.MouseMove(p);						//tsuibi��Base�^�����AMouseMove���K�v�Ȃ̂ŁA�Ă�
					break;
				case muphic.OneScreenMode.ScoreScreen:
					Screen.MouseMove(p);
					break;
				case muphic.OneScreenMode.TitleScreen:
					Screen.MouseMove(p);
					break;
				case muphic.OneScreenMode.VoiceRegistDialog:
					Screen.MouseMove(p);
					break;
				case muphic.OneScreenMode.FileSelectDialog:
				case muphic.OneScreenMode.FileSaveDialog:
				case muphic.OneScreenMode.AllClearDialog:
				case muphic.OneScreenMode.VoiceRegistDialogOneMore:
					Screen.MouseMove(p);
					break;
				default:
					break;
			}
		}

		public override void Draw()
		{
			/*if(isClicked)
			{
				muphic.DrawManager.Draw("test");
				return;
			}*/
			switch(onescreenmode)
			{
				case muphic.OneScreenMode.OneScreen:
					base.Draw ();
					if(tsuibi.Visible)
					{
						muphic.DrawManager.DrawCenter(tsuibi.ToString(), tsuibi.point.X, tsuibi.point.Y, tsuibi.State);
						//tsuibi��BaseList�ɓo�^���ĂȂ��̂ŁA�ʂɕ`�悷��K�v������
					}
					// �Ȗ��̕`��
					DrawManager.DrawString(this.ScoreTitle, 400, 75);
					
					// �e�_�C�A���O�̕`��
//					if(this.isSaveDialog) this.sadialog.Draw();
//					else if(this.isSelectDialog) this.sedialog.Draw();
					
					break;
				case muphic.OneScreenMode.ScoreScreen:
					Screen.Draw();
					break;
				case muphic.OneScreenMode.TitleScreen:
					Screen.Draw();
					break;
				case muphic.OneScreenMode.VoiceRegistDialog:
					base.Draw();
					Screen.Draw();
					break;
				case muphic.OneScreenMode.FileSaveDialog:
				case muphic.OneScreenMode.FileSelectDialog:
				case muphic.OneScreenMode.AllClearDialog:
				case muphic.OneScreenMode.VoiceRegistDialogOneMore:
					base.Draw();
					
					// �Ȗ��̕`��
					DrawManager.DrawString(this.ScoreTitle, 400, 75);
					
					Screen.Draw();
					break;
				default:
					break;
			}
		}
		
		
		
		
		/// <summary>
		/// �ꕔ�̕�����̓_�C�A���O�\�����ɔ���Ă��܂�
		/// �������\�����Ă������ǂ�����₢���킹�郁�\�b�h
		/// </summary>
		/// <returns>true�Ȃ�\�����Ă悵</returns>
		public bool StringDraw()
		{
			// �`���[�g���A���Ǘ����̃_�C�A���O���܂߂��S�Ẵ_�C�A���O���\������Ă��Ȃ����̂�true��Ԃ�
			return !(this.isSaveDialog || this.isAllClearDialog || this.isSelectDialog || this.isTitleScreen || muphic.Common.TutorialStatus.getisTutorialDialog() );
		}
	}
}
