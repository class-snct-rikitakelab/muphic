using System;
using System.IO;
using System.Text;
using System.Drawing;
using muphic.Common;
using muphic.ADV;
using muphic.Tutorial.TutorialSPParts;

namespace muphic.Tutorial
{
	/// <summary>
	/// �`���[�g���A���̃��C���N���X ADV�p�̃N���X���p��
	/// </summary>
	public class TutorialMain : AdventureMain
	{
		public TopScreen topscreen;
		public new TutorialScreen parent;
		
		public HintButton hintbutton;
		public CheckButton checkbutton;
		public CompleteButton completebutton;
		
		public TutorialEndDialog edialog;	// �I���m�F�_�C�A���O
		public bool isEndDialog;			// �I���m�F�_�C�A���O�\�����邩�ۂ�
		
		public TutorialStart tutorialstart;	// �`���v�^�[���̃g�b�v���
		public bool isTutorialStart;		// �`���v�^�[���̃g�b�v��ʂ�\�����邩�ۂ�
		
		public bool isHintButton;			// �q���g�{�^�����o�Ă��邩�ǂ���
		
		public CursorControl cursorcontrol;
		
		public const string SaveFileName = "TutorialSaveData.sav";
		
		private int Emergency;		// �ً}���p
		
		public TutorialMain(TutorialScreen tscreen, string DirectoryName, bool continueflag) : base((Screen)tscreen, DirectoryName, ADVParentScreen.TutorialScreen)
		{
			this.parent = tscreen;
			this.isEndDialog = false;
			this.isHintButton = false;
			this.Emergency = 0;
			
			// ��������J�n����ꍇ�̓Z�[�u�f�[�^�̓ǂݍ���
			if(continueflag) this.ADVChapter = TutorialTools.ReadSaveFile(this.DirectoryName + SaveFileDirectory + "\\" + SaveFileName, false) - 1;
			
			// �`���v�^�[�ꗗ�𓾂�
			this.Chapters = TutorialTools.getDirectoryNames(DirectoryName + PatternFileDirectory, "TutorialChapter");
			
			// �r���I���p�̃_�C�A���O
			edialog = new TutorialEndDialog(this);
			
			// �`���[�g���A���p�̃g�b�v�X�N���[��
			topscreen = new TopScreen(this);
			
			// �J�[�\������
			cursorcontrol = new CursorControl(this);
			
			// �{�^���w���p���̓o�^
			DrawManager.Regist("TutorialSupport_arrow_left" , 0, 0, "image\\TutorialXGA\\Arrow\\arrow_left1.png" , "image\\TutorialXGA\\Arrow\\arrow_left2.png" );
			DrawManager.Regist("TutorialSupport_arrow_right", 0, 0, "image\\TutorialXGA\\Arrow\\arrow_right1.png", "image\\TutorialXGA\\Arrow\\arrow_right2.png");
			
			// �N���b�N�w���p�g�̓o�^
			DrawManager.Regist("TutorialSupport_frame_animal"    , 0, 0, "image\\TutorialXGA\\Frame\\frame_animal1.png"    , "image\\TutorialXGA\\Frame\\frame_animal2.png"    );
			DrawManager.Regist("TutorialSupport_frame_link"      , 0, 0, "image\\TutorialXGA\\Frame\\frame_link1.png"      , "image\\TutorialXGA\\Frame\\frame_link2.png"      );
			DrawManager.Regist("TutorialSupport_frame_one"       , 0, 0, "image\\TutorialXGA\\Frame\\frame_one1.png"       , "image\\TutorialXGA\\Frame\\frame_one2.png"       );
			DrawManager.Regist("TutorialSupport_frame_story_back", 0, 0, "image\\TutorialXGA\\Frame\\frame_story_back1.png", "image\\TutorialXGA\\Frame\\frame_story_back2.png");
			DrawManager.Regist("TutorialSupport_frame_story_body", 0, 0, "image\\TutorialXGA\\Frame\\frame_story_body1.png", "image\\TutorialXGA\\Frame\\frame_story_body2.png");
		}
		
		
		/// <summary>
		/// �`���v�^�[�����ɐi�߂郁�\�b�h
		/// </summary>
		public new void NextChapter()
		{
			// �`���v�^�[����i�߂�
			this.ADVChapter++;
			
			// �p�^�[���t�@�C���ꗗ�𓾂�
			this.PatternFiles = TutorialTools.getFileNames(this.Chapters[this.ADVChapter], ".pat");
			
			// �X�e�[�g�����Z�b�g���A�ŏ��̃X�e�[�g�֐i�s
			this.ADVState = -1;
			this.NextState();
		}
		
		
		/// <summary>
		/// ���ɐi�߂郁�\�b�h
		/// </summary>
		public new void NextState()
		{
			// �X�e�[�g���`���v�^�[�̃p�^�[�����𒴂����ꍇ
			if(this.ADVState+1 >= this.PatternFiles.Length)
			{
				if(this.ADVChapter+1 >= this.Chapters.Length)
				{
					// �����S�Ẵ`���v�^�[�𗬂��I������I��
					this.TutorialEnd(false);
					return;
				}
				else
				{
					// ���̃`���v�^�[�֐i��
					this.NextChapter();
					return;
				}
			}
			
			// �t���O�Ƒҋ@��Ԃ��������A�g���K�[���X�g�Ƌ����X�g���N���A
			this.initTutorialStatus();
			
			// �p����ADV�N���X�̃X�e�[�g�i�s���\�b�h�����s
			base.NextState();
			
			// �`���[�g���A���i�s�̂��߃N���b�N��������p�[�c�����X�g�ɒǉ�
			this.addPermissionList();
			
			// �}�E�X�N���b�N�w�肪����Ă����ꍇ�͎��s
			if( this.pattern.MouseClick.X != -1 ) this.MouseClick(this.pattern.MouseClick);
			
			// �`���v�^�[���̃g�b�v��ʂ�`�悷��ݒ�ɂȂ��Ă����ꍇ�̓g�b�v��ʂ��C���X�^���X��
			if( this.pattern.ChapterTop != "" )
			{
				tutorialstart = new TutorialStart(this.parent, this.pattern.ChapterTop);
				this.isTutorialStart = true;
			}
			else
			{
				this.isTutorialStart = false;
			}
			
			// �t�F�[�h���ʂ��o���ꍇ�͂�������
			if(this.pattern.Fade.X != -1)
			{
				// �񐔂�0�Ɏw�肳��Ă����炱����Ō��߂��񐔂ōs��
				if(this.pattern.Fade.X == 0) this.Fade();
				else this.Fade(this.pattern.Fade.X, this.pattern.Fade.Y);
			}
			
			// ����R�}���h���ݒ肳��Ă���Δ���
			if(this.pattern.SPMode != "") this.SPCommand(this.pattern.SPMode);
			
			// �����I��muphic�g�b�v��ʂɖ߂�悤�ݒ肳��Ă����ꍇ�͂�������
			if( pattern.TopScreen ) this.BackTopScreen();
			
			// �p�^�[���t�@�C���ǂݍ��݌�A���X�e�[�g�i�s����悤�ݒ肳��Ă����ꍇ
			// (�V�X�e���֘A�݂̂̃p�^�[���������ۂɎg�p)
			// ���̃R�[�h�͔O�̂���NextState���\�b�h�̍Ō�ɋL�q����
			if( this.pattern.NextState )
			{
				this.NextState();
				return;
			}
		}
		
		
		public new void StopVoice()
		{
			base.StopVoice();
			
			if(this.isHintButton) this.hintbutton.StopVoice();
		}
		
		
		/// <summary>
		/// �`���[�g���A���̏�Ԃ̊ȈՏ�����
		/// </summary>
		public void initTutorialStatus()
		{
			TutorialStatus.setDisableNextState();
			TutorialStatus.setDisableNextStateStandBy();
			TutorialStatus.initTriggerPartsList();
			TutorialStatus.initPermissionPartsList();
			TutorialStatus.setDisableClickControl();
		}
		
		/// <summary>
		/// �`���[�g���A���i�s�̂��߃N���b�N��������p�[�c��ǉ�����
		/// </summary>
		private void addPermissionList()
		{			
			// ���փ{�^���͖������Ńg���K�[�p�[�c�Ƃ���
			TutorialStatus.addTriggerPartsList("muphic.ADV.MsgWindowParts.NextButton");
			
			// ��߂�{�^���Ɖ����؂�ւ��{�^���͋�����
			TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.EndButton");
			TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.VoiceButton");
			
			// �I���m�F�_�C�A���O�̃{�^����������
			TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialEndDialogParts.YesButton");
			TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialEndDialogParts.NoButton");
		}
		
		
		/// <summary>
		/// �ȂȂȂȂȂȂȂȂ�Ƃ�������R�}���h
		/// </summary>
		public void SPCommand(string SPMode)
		{
			StoryScreen story;
			
			switch(SPMode)
			{
				case "PT02_Link27_1":
					// �Ȃ��ĉ��y�p�^�[���t�@�C��27
					
					// �Ȃ��ĉ��y�̖��������I������
					new muphic.Link.Dialog.Select.SelectDialog(1, (LinkScreen)topscreen.Screen);
					break;
					
				case "PT02_Link30":
					// �Ȃ��ĉ��y�p�^�[���t�@�C��30
					
					// �܂�����R�}���h���[�h��ON�ɂ���
					TutorialStatus.setEnableIsSPMode("PT02_Link30");
					
					// �q���g�{�^�����C���X�^���X�����o�^��
					hintbutton = new HintButton(this, DirectoryName + ControlFileDirectory + "\\" + TutorialStatus.getisSPMode() );
					DrawManager.Regist(hintbutton.ToString(), 540, 30, "image\\TutorialXGA\\FreedomButtons\\hint_off.png", "image\\TutorialXGA\\FreedomButtons\\hint_on.png");
					BaseList.Add(hintbutton);
					TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialSPParts.HintButton");
					this.isHintButton = true;
					
					// �g���K�[�p�[�c���X�g���N���A����i�P���ȃ{�^������ɂ��X�e�[�g�i�s�͖������߁j
					TutorialStatus.initTriggerPartsList();
					
					// ���փ{�^���������X�g�֒ǉ�
					TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.NextButton");
					
					// �Ȗ��o�[������
					((LinkScreen)this.topscreen.Screen).titlebar.Visible = false;
					break;
					
				case "PT02_Link30_True":
					// �Ȃ��ĉ��y�p�^�[���t�@�C��30 �������킹�Ő����������ꍇ
					
					// ���b�Z�[�W�E�B���h�E�ɐ����̂��o��
					this.msgwindow.getText(new string[] {"������I����Ł@���傭�́@���񂹂����I�I"});
					this.msgwindow.ChangeWindowCoordinate(1);
					
					// ���R������I��
					TutorialStatus.setDisableIsSPMode();	// �܂�����R�}���h���[�h��OFF�ɂ���
					hintbutton.Visible = false;				// �q���g�{�^��������
					this.isHintButton = false;
					
					// �N���b�N�����X�g�n���N���A���ĉ]�X
					this.initTutorialStatus();
					this.addPermissionList();
					
					break;
					
				case "PT02_Link30_False":
					// �Ȃ��ĉ��y�p�^�[���t�@�C��30 �������킹�ŕs�����������ꍇ
					
					// ���b�Z�[�W�E�B���h�E�ɕs�����̂��o��
					this.msgwindow.getText(new string[] {"����˂�I�@���������ǂ���΂낤�I"});
					this.msgwindow.ChangeWindowCoordinate(1);
					
					// �������ꂾ���ł����񂩁[���ȁ[�H ����񂩁[���ȁ[�H				
					break;
					
				case "PT03_Story28":
					// ���̂����艹�y�p�^�[���t�@�C��28
					
					// ��ȉ�� �y���̃N���A
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					this.ClearStoryScore(story);
					break;
					
				case "PT03_Story30":
					// ���̂����艹�y�p�^�[���t�@�C��30

					// ����R�}���h���[�h��ON�ɂ���
					TutorialStatus.setEnableIsSPMode("PT03_Story30");
					break;
					
				case "PT03_Story36":
					// ���̂����艹�y�p�^�[���t�@�C��32
					
					// �܂�����R�}���h���[�h��ON�ɂ���
					TutorialStatus.setEnableIsSPMode("PT03_Story32");
					
					// ����΂�
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					String oldAnimalName = story.score.Animals[0].AnimalName;			//4�C���ׂē������O�Ȃ̂ŁC�ǂꂩ��Ƃ��Ă��֌W�˂�
					this.ClearStoryScore(story);
					
					// ������ŐV���ɓ�����o�^
					story.score.Animals.Insert(0,8,oldAnimalName, false);
					story.score.Animals.Insert(4,8,oldAnimalName, false);
					story.score.Animals.Insert(5,8,oldAnimalName, false);
					break;
					
				case "PT03_Story40":
					// ���̂����艹�y�p�^�[���t�@�C��38
					
					// �܂�����R�}���h���[�h��ON�ɂ���
					TutorialStatus.setEnableIsSPMode("PT03_Story38");
					
					// ����΂�
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					String oldAnimalName2 = story.score.Animals[0].AnimalName;			//3�C���ׂē������O�Ȃ̂ŁC�ǂꂩ��Ƃ��Ă��֌W�˂�
					this.ClearStoryScore(story);
					
					// ������ŐV���ɓ�����o�^
					story.score.Animals.Insert(0,8,oldAnimalName2, false);
					story.score.Animals.Insert(4,7,oldAnimalName2, false);
					story.score.Animals.Insert(5,6,oldAnimalName2, false);
					break;
					
				case "PT03_Story46":
					// ���̂����艹�y�p�^�[���t�@�C��46
					
					// �܂�����R�}���h���[�h��ON�ɂ���
					TutorialStatus.setEnableIsSPMode("PT03_Story46");
					
					// ����΂�
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					this.ClearStoryScore(story);
					
					// �\�ߗp�ӂ������̂�����X���C�h��ǂݍ��݁A�T���l�C���ɉ摜��\�������Ă��܂��̂ł���  ����[
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					muphic.MakeStory.StoryFileReader reader = new muphic.MakeStory.StoryFileReader(story.parent.PictureStory);
					reader.Read("..\\TutorialData\\TutorialMain\\ControlFiles\\PT03_Story46\\������������������");
					
					break;
					
				case "PT03_Story50":
					// ���̂����艹�y�p�^�[���t�@�C��50
					
					// �e���}���_��
					//story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					//this.ClearStoryScore(story);
					
					
					// �܂�����R�}���h���[�h��ON�ɂ���
					TutorialStatus.setEnableIsSPMode("PT03_Story50");
					
					// �`�F�b�N�{�^�����C���X�^���X�����o�^��
					checkbutton = new CheckButton(this);
					DrawManager.Regist(checkbutton.ToString(), 540, 30, "image\\TutorialXGA\\FreedomButtons\\check_off.png", "image\\TutorialXGA\\FreedomButtons\\check_on.png");
					BaseList.Add(checkbutton);
					TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialSPParts.CheckButton");
					
					// ���b�Z�[�W�E�B���h�E���\��
					this.msgwindow.ChangeWindowCoordinate(0);
					
					// �g���K�[�p�[�c���X�g���N���A����i�P���ȃ{�^������ɂ��X�e�[�g�i�s�͖������߁j
					TutorialStatus.initTriggerPartsList();
					
					// ���փ{�^���������X�g�֒ǉ�
					TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.NextButton");
					
					break;
					
				case "PT03_Story50_True":
					// ���̂����艹�y�p�^�[���t�@�C��50 �����������ꍇ
					
					// �����̃��b�Z�[�W
					//this.msgwindow.getText(new string[] {"�܂��܂��A���Ȃ������@����Ȃ������B", "�ǂ��Ԃ́@�����Ƃ���@�����ĂˁB", "���������ǁ@����΂��Ă݂悤�I"});
					
					// ���R������I��
					TutorialStatus.setDisableIsSPMode();	// �܂�����R�}���h���[�h��OFF�ɂ���
					checkbutton.Visible = false;			// �`�F�b�N�{�^��������
					
					// �N���b�N�����X�g�n���N���A���ĉ]�X
					this.initTutorialStatus();
					this.addPermissionList();
					
					break;
					
				case "PT03_Story50_False":
					// ���̂����艹�y�p�^�[���t�@�C��50 �������Ȃ������ꍇ
					
					break;
					
				case "PT04_One02":
					// �ЂƂ�ŉ��y�p�^�[���t�@�C��02
					
					// �܂�����R�}���h���[�h��ON�ɂ���
					TutorialStatus.setEnableIsSPMode("PT04_One02");
					
					// �����{�^���̃C���X�^���X�����o�^��
					completebutton = new CompleteButton(this);
					DrawManager.Regist(completebutton.ToString(), 540, 30, "image\\TutorialXGA\\FreedomButtons\\comp_off.png", "image\\TutorialXGA\\FreedomButtons\\comp_on.png");
					BaseList.Add(completebutton);
					TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialSPParts.CompleteButton");
					
					// �ЂƂ�ŉ��y��� �^�C�g���\���֘A�E�t�@�C�����o�͊֘A�͕s�v�Ȃ̂ŏ���
					((OneScreen)this.topscreen.Screen).titlebutton.Visible = false;
					((OneScreen)this.topscreen.Screen).titleform.Visible = false;
					((OneScreen)this.topscreen.Screen).savebutton.Visible = false;
					((OneScreen)this.topscreen.Screen).selectbutton.Visible = false;
					
					// ���b�Z�[�W�E�B���h�E���\��
					this.msgwindow.ChangeWindowCoordinate(0);
					
					// �g���K�[�p�[�c���X�g���N���A����i�P���ȃ{�^������ɂ��X�e�[�g�i�s�͖������߁j
					TutorialStatus.initTriggerPartsList();
					
					// ���փ{�^���������X�g�֒ǉ�
					TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.NextButton");
					
					break;
					
				case "PT04_One02_True":
					// �ЂƂ�ŉ��y�p�^�[���t�@�C��02 �����̏ꍇ
					
					// ���R������I��
					TutorialStatus.setDisableIsSPMode();	// �܂�����R�}���h���[�h��OFF�ɂ���
					completebutton.Visible = false;			// �`�F�b�N�{�^��������
					
					// �N���b�N�����X�g�n���N���A���ĉ]�X
					this.initTutorialStatus();
					this.addPermissionList();
					
					break;
					
				case "PT04_One02_False":
					// �ЂƂ�ŉ��y�p�^�[���t�@�C��02 �s�����̏ꍇ
					
					break;
					
				default:
					System.Console.WriteLine("������őz�肵�Ă��Ȃ�����R�}���h���g�����Ƃ����悤��");
					System.Console.WriteLine("�R(`�D�L)ɼ�Ȱ�!");
					break;
			}
		}
		
		
		public void ClearStoryScore(StoryScreen story)
		{
			// ���̂����艹�y�̓����{�^���Q�������I������Ă��Ȃ���Ԃɂ���
			//story.stories.NowClick = muphic.Story.StoryButtonsClickMode.None;
			story.stories.AllClear();
					
			// �X�N���[���o�[�̈ʒu�������ʒu�ɖ߂�
			story.score.ChangeScroll(0);			
			
			// ������S�č폜
			story.score.Animals.AllDelete();
		}
		
		
		/// <summary>
		/// MainScreen�̃N���b�N���\�b�h���Ă�ł����
		/// </summary>
		/// <param name="point"></param>
		public void MouseClick(Point point)
		{
			System.Windows.Forms.MouseEventArgs e = new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.None, 1, point.X, point.Y, 0);
			this.parent.parent.parent.OnMouseDownPublic(e);
			this.parent.parent.parent.OnMouseUpPublic(e);
		}
		
		
		/// <summary>
		/// �t�F�[�h���ʂ̃��\�b�h
		/// ������̓`���[�g���A���p�ɐ��l��\�ߐݒ肵�Ă݂�
		/// </summary>
		public void Fade()
		{
			this.Fade(4, 6);
		}
		
		/// <summary>
		/// �t�F�[�h���ʂ̃��\�b�h
		/// ������͈����ŉ񐔂Ǝ��Ԃ����߂���
		/// </summary>
		/// <param name="num">�t�F�[�h���ʂ̉񐔁i���Á��� �̉񐔁j</param>
		/// <param name="time">�����Â̐؂�ւ��ɂ�����t���[����</param>
		public void Fade(int num, int time)
		{
			//return;
			
			for(int i=0; i<num; i++)
			{
				DrawManager.StartFadeOut(time);
				DrawManager.StartFadeIn(time);
			}
		}
		
		
		/// <summary>
		/// �����I�Ƀg�b�v��ʂɖ߂����\�b�h
		/// </summary>
		public void BackTopScreen()
		{
			switch(this.topscreen.ScreenMode)
			{
				case muphic.ScreenMode.OneScreen:
					// �߂�{�^�������������Ƃɂ���
					((OneScreen)this.topscreen.Screen).back.Click(System.Drawing.Point.Empty);
					break;
				case muphic.ScreenMode.LinkScreen:
					// �������킹�̌��ʃ_�C�A���O�̖߂�{�^���������A�Ȃ��ĉ��y�񓚉�ʂ̖߂�{�^�������������Ƃɂ���
					((Link.Dialog.Answer.AnswerDialog)((LinkScreen)this.topscreen.Screen).Screen).back.Click(System.Drawing.Point.Empty);
					((LinkScreen)this.topscreen.Screen).back.Click(System.Drawing.Point.Empty);
					break;
				case muphic.ScreenMode.StoryScreen:
					((StoryScreen)((MakeStoryScreen)this.topscreen.Screen).Screen).back.Click(Point.Empty);
					((MakeStoryScreen)this.topscreen.Screen).bb.Click(Point.Empty);
					break;
				default:
					break;
			}
		}
		
		
		/// <summary>
		/// �ً}���Ɉ�O�̃X�e�[�g�ɖ߂�
		/// �Ȃ�Ƃ������Ƃł��傤
		/// </summary>
		public void EmergencyCode()
		{
			System.Console.WriteLine("EmergencyCode�����B1�O�̃X�e�[�g�ɖ߂�");
			this.ADVState -= 2;
			this.Emergency = 0;
			this.NextState();
		}
		
		
		/// <summary>
		/// �S�p�^�[�������s���I�������Ăяo��
		/// �`���[�g���A�����I��
		/// ���������ŌĂяo�����ꍇ�͎����I�ɃZ�[�u�����
		/// </summary>
		public void TutorialEnd()
		{
			this.TutorialEnd(true);
		}
		/// <summary>
		/// �S�p�^�[�������s���I�������Ăяo��
		/// �`���[�g���A�����I��
		/// </summary>
		/// <param name="save">�Z�[�u���s�����ǂ���</param>
		public void TutorialEnd(bool save)
		{
			if(save) this.TutorialSave();
			base.AdventureEnd();
		}
		
		
		/// <summary>
		/// �`���[�g���A���̃Z�[�u
		/// </summary>
		public void TutorialSave()
		{
			TutorialTools.WriteSaveFile(this.DirectoryName + SaveFileDirectory + "\\" + SaveFileName, this.ADVChapter, this.Chapters[this.ADVChapter], this.Chapters.Length);
		}
		
		
		
		public new void Draw()
		{
			topscreen.Draw();		// �`���[�g���A���p��ʂ̕`��
			base.Draw();
			
			if(this.isEndDialog)
			{
				this.edialog.Draw();
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.Draw();
			}
		}

		public new void Click(System.Drawing.Point p)
		{
			if(this.isEndDialog)
			{
				// �I���m�F�_�C�A���O���o�Ă���ꍇ�͂�����̃N���b�N����
				this.edialog.Click(p);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.Click(p);
			}
			else
			{
				// ���b�Z�[�W�E�B���h�E�̉��̉�ʂɃN���b�N���`����Ă��܂��A���̋���̉�����
				// ���b�Z�[�W�E�B���h�E�̉�����\�ߊm�F���Ă���
				bool MsgWindowVisible = this.msgwindow.Visible;
				
				base.Click (p);
				
				// �N���b�N����������Ă����ꍇ�A�ȍ~�̏����͍s�킸
				if( TutorialStatus.getClickControl()) return;
				
				// �ً}���ɍ����3��N���b�N����ƈ�O�ɖ߂�
				if( this.inRect(p, new Rectangle(0, 0, 50, 50))) this.Emergency++;
				if( this.Emergency >= 3 )
				{
					this.EmergencyCode();
					return;
				}
				
				// �N���b�N�ł���͈͂����W�Ő������Ă���ꍇ�̓N���b�N���ꂽ���W��������Ă���͈͂����`�F�b�N
				if( this.pattern.rect.X != -1 && !this.inRect(p, this.pattern.rect) )
				{
					// ���͈͊O�������ꍇ��topscreen�ȉ��̃N���b�N�͂����Ȃ�	
				}
				else
				{
					if( !(MsgWindowVisible && this.msgwindow.nextbutton.Visible)) topscreen.Click(p);
				}
				
				// ����ҋ@���łȂ��A�X�e�[�g�i�s�t���O�������Ă�����
				if( TutorialStatus.getNextStateFlag() && !TutorialStatus.getNextStateStandBy())
				{
					// �t���O���������Ď��̃X�e�[�g�ɐi��
					TutorialStatus.setDisableNextState();
					this.NextState();
				}
			}
		}
		
		public new void MouseMove(System.Drawing.Point p)
		{
			if(this.isEndDialog)
			{
				this.edialog.MouseMove(p);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.MouseMove(p);
			}
			else
			{
				base.MouseMove (p);
				topscreen.MouseMove(p);
			}
		}
		
		public new void DragBegin(Point begin)
		{
			if(this.isEndDialog)
			{
				this.edialog.DragBegin (begin);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.DragBegin (begin);
			}
			else
			{
				base.DragBegin (begin);
				topscreen.DragBegin (begin);
			}
		}
		
		public new void Drag(Point begin, Point p)
		{
			if(this.isEndDialog)
			{
				this.edialog.Drag (begin, p);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.Drag (begin, p);
			}
			else
			{
				base.Drag (begin, p);
				topscreen.Drag (begin, p);
			}
		}
		
		public new void DragEnd(Point begin, Point p)
		{
			if(this.isEndDialog)
			{
				this.edialog.DragEnd (begin, p);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.DragEnd (begin, p);
			}
			else
			{
				base.DragEnd (begin, p);
				topscreen.DragEnd (begin, p);
			}
		}
	}
}
