using System;
using System.IO;
using System.Text;
using System.Collections;
using muphic.Common;

namespace muphic.ADV
{
	public enum ADVParentScreen { TutorialScreen }
	
	/// <summary>
	/// �ėp�A�h�x���`���[�p�[�g�i�ǂނ����j
	/// �`���[�g���A���ł��g���̂ŁA�Ƃ����������������C���Ȃ̂ŁA�`���[�g���A�����s�ɓs���̂����悤�ɍ���(ry
	/// �ł��Ɨ����Ďg�����Ƃ����Ăł���B���ׂ̈ɔėp�������񂾂�B�g�����킩��񂯂ǁB
	/// �`���[�g���A���ȊO�Ŏg���Ƃ��́A�񋓌^ADVParentScreen��AdventureEnd���\�b�h��DrawImage���\�b�h������ɉ���������������΂�����������Ȃ��B
	/// </summary>
	public class AdventureMain : Screen
	{
		public Screen parent;
		public ADVParentScreen parentscreen;
		public MsgWindow msgwindow;

		public string DirectoryName;	// �X�g�[���[�Ɏg�p����f�[�^���i�[���ꂽ�f�B���N�g��
		public string[] PatternFiles;	// �p�^�[���t�@�C���ꗗ
		public string[] Chapters;		// �`���v�^�[�ꗗ�i�t�H���_���j
		public string[] ImageFiles;		// �摜�t�@�C���ꗗ
		public PatternData pattern;		// �p�^�[���t�@�C������̏��
		public string BGMFile;			// �Đ�����BGM�̃t�@�C���� �Đ����Ŗ����ꍇ�͋�ɂ��Ƃ�
		public string VoiceFile;		// �Đ����̉����̃t�@�C���� �Đ����Ŗ����ꍇ�͋�ɂ��Ƃ�
		public bool isPlayVoice;		// �������Đ����邩�ǂ����̃t���O
		
		public int ADVChapter;			// �`���v�^�[�̐i�s��\��
		public int ADVState;			// �X�g�[���[�̐i�s��\���B
		
		public const string PatternFileDirectory = "\\PatternFiles";		// �p�^�[���t�@�C���̃f�B���N�g��
		public const string ImageFileDirectory   = "\\ImageFiles";		// �摜�̃f�B���N�g��
		public const string BGMFileDirectory     = "\\BGMFiles";			// BGM�t�@�C���̃f�B���N�g��
		public const string VoiceFileDirectory   = "\\VoiceFiles";		// �����t�@�C���̃f�B���N�g��
		public const string SaveFileDirectory    = "\\SaveFiles";		// ���炩�̃Z�[�u�f�[�^�̃f�B���N�g��
		public const string ControlFileDirectory = "\\ControlFiles";		// ����t�@�C���̃f�B���N�g��
		
		public AdventureMain(Screen screen, string DirectoryName, muphic.ADV.ADVParentScreen parentscreen)
		{
			this.parent = screen;
			this.DirectoryName = DirectoryName;
			this.parentscreen = parentscreen;
			this.ADVChapter = -1;
			this.BGMFile = "";
			this.VoiceFile = "";
			this.isPlayVoice = true;
			
			// ���b�Z�[�W�E�B���h�E�̃C���X�^���X��
			this.msgwindow = new MsgWindow(this);
			
			// �`���v�^�[�ꗗ�𓾂�
			this.Chapters = TutorialTools.getDirectoryNames(DirectoryName + PatternFileDirectory, "ADVChapter");
			
			// �摜�t�@�C���ꗗ�𓾂�
			// this.ImageFiles = TutorialTools.getFileNames(DirectoryName + ImageFileDirectory);
		}
		
		
		/// <summary>
		/// �`���v�^�[�����ɐi�߂郁�\�b�h
		/// </summary>
		public void NextChapter()
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
		/// �X���C�h�����ɐi�߂郁�\�b�h
		/// </summary>
		public void NextState()
		{
			// ��i�߂�
			this.ADVState++;
			
			// �X�e�[�g���`���v�^�[�̃p�^�[�����𒴂����ꍇ
			if( !TutorialStatus.getIsTutorial() && this.ADVState >= this.PatternFiles.Length )
			{
				if(this.ADVChapter >= this.Chapters.Length)
				{
					// �����S�Ẵ`���v�^�[�𗬂��I������I��
					this.AdventureEnd();
					return;
				}
				else
				{
					// ���̃`���v�^�[�֐i��
					this.NextChapter();
				}
			}
			
			// ���̃p�^�[���t�@�C����ǂݍ���
			this.pattern = TutorialTools.ReadPatternFile( this.PatternFiles[this.ADVState] );
			
			// ������̕`��������邩�ǂ����̐ݒ� 
			if( this.pattern.DrawString ) TutorialStatus.setEnableDrawString();
			else TutorialStatus.setDisableDrawString();
			
			// ���b�Z�[�W�E�B���h�E�Ɋւ���
			if( this.pattern.Window != 0 )
			{
				// ���b�Z�[�W�E�B���h�E���\�������ݒ�ł����
				this.msgwindow.Visible = true;
				this.msgwindow.ChangeWindowCoordinate(this.pattern.Window);	// �ʒu�̐ݒ�
				
				// ���b�Z�[�W�E�B���h�E�Ɏ��̃X���C�h�̃e�L�X�g��n��
				this.msgwindow.getText(this.pattern.text);
				
				// ���b�Z�[�W�E�B���h�E�̎��փ{�^���̕`��̕ύX �x���`��̊֌W��visible�ς��邾���ł̓_���Ȃ̂�
				this.msgwindow.getNextButtonVisible(this.pattern.NextButton);
				
				// ���b�Z�[�W�E�B���h�E�̎��փ{�^����\�����钼�O�ɉ摜��regist����ݒ�ɂȂ��Ă���ꍇ
				this.msgwindow.NBRegist = this.pattern.NBRegist;
				
				// ���b�Z�[�W�E�B���h�E�̃A�V�X�^���g�����̕ύX
				// �p�^�[���t�@�C���ɉ����L�q����Ă��Ȃ������ꍇ�͂��̂܂܂ɂ��Ă���
				if(this.pattern.assistant != "") this.msgwindow.setAssistant(this.pattern.assistant);
			}
			else
			{
				// ���b�Z�[�W�E�B���h�E���\������Ȃ��ݒ肾������
				this.msgwindow.Visible = false;
			}
			
			// BGM�̏���
			if( (this.pattern.BGM != "") && (this.pattern.BGM != this.BGMFile) )
			{
				// �p�^�[���t�@�C����BGM���Ɍ��ݍĐ����̃t�@�C�����Ƃ͕ʂ̉��炩�̃f�[�^����������
				
				// �܂�BGM���~���t�@�C��������ɂ���
				this.StopBGM();
				this.BGMFile = "";
				
				// "STOP"�łȂ���΁A�V�����t�@�C�����Đ�����
				if(this.pattern.BGM != "STOP")
				{
					this.BGMFile = this.pattern.BGM;
					this.PlayBGM(); //���邳���̂Ŏ~�߂Ƃ�
				}
			}
			
			// Voice�̏���
			// �܂��Đ����̂��~���A�V�����t�@�C�������Z�b�g�����肷��
			//this.StopVoice();
			//this.VoiceFile = this.pattern.Voice;
			//this.PlayVoice();
			this.SetVoice(this.pattern.Voice);
			
			
			// �p�^�[���t�@�C���ǂݍ��݌�A���X�e�[�g�i�s����悤�ݒ肳��Ă����ꍇ
			// (�V�X�e���֘A�݂̂̃p�^�[���������ۂɎg�p)
			// ���̃R�[�h�͔O�̂���NextState���\�b�h�̍Ō�ɋL�q����
			if( this.pattern.NextState )
			{
				// �`���[�g���A�����s���̏ꍇ�͂�����̕��ŌĂяo�����߁A�`���[�g���A�����s���łȂ��ꍇ�݂̂����ŃX�e�[�g�i�s
				if( !TutorialStatus.getIsTutorial() ) this.NextState();
				return;
			}
		}
		
		
		/// <summary>
		/// BGM�̍Đ����J�n���郁�\�b�h
		/// BGM�̃t�@�C�����̓N���X�t�B�[���h����擾
		/// </summary>
		/// <returns>�Đ��J�n�ł������ǂ��� true�Ȃ琬��</returns>
		public bool PlayBGM()
		{
			// �p�X���܂߂��t�@�C�����̐���
			string filename = DirectoryName + BGMFileDirectory + "\\" + this.BGMFile;
			
			if( File.Exists(filename) )
			{
				// �t�@�C���̑��݃`�F�b�N���s���A���݂��Ă�����Đ��J�n
				SoundManager.Play(filename);
				System.Console.WriteLine("BGM " + filename + " �Đ�");
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// BGM�̍Đ����~���郁�\�b�h
		/// BGM�̃t�@�C�����̓N���X�t�B�[���h����擾
		/// </summary>
		/// <returns>�Đ���~�ł������ǂ��� true�Ȃ琬��</returns>
		public bool StopBGM()
		{
			// �p�X���܂߂��t�@�C�����̐���
			string filename = DirectoryName + BGMFileDirectory + "\\" + this.BGMFile;
			
			if( File.Exists(filename) )
			{
				// �t�@�C���̑��݃`�F�b�N���s���A���݂��Ă�����Đ���~
				SoundManager.Stop(filename);
				System.Console.WriteLine("BGM " + filename + " ��~");
				return true;
			}
			return false;
		}
		
		
		/// <summary>
		/// Voice�̍Đ����J�n���郁�\�b�h
		/// Voice�̃t�@�C�����̓N���X�t�B�[���h����擾
		/// </summary>
		/// <returns>�Đ��J�n�ł������ǂ��� true�Ȃ琬��</returns>
		public bool PlayVoice()
		{
			if(!this.isPlayVoice) return false;
			
			// �p�X���܂߂��t�@�C�����̐���
			string filename = DirectoryName + VoiceFileDirectory + "\\" + this.VoiceFile;
			
			if( File.Exists(filename) )
			{
				// �t�@�C���̑��݃`�F�b�N���s���A���݂��Ă�����Đ��J�n
				VoiceManager.Play(filename);
				System.Console.WriteLine("Voice " + filename + " �Đ�");
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Voice�̍Đ����~���郁�\�b�h
		/// Voice�̃t�@�C�����̓N���X�t�B�[���h����擾
		/// </summary>
		/// <returns>�Đ���~�ł������ǂ��� true�Ȃ琬��</returns>
		public bool StopVoice()
		{
			if(!this.isPlayVoice) return false;
			
			// �p�X���܂߂��t�@�C�����̐���
			string filename = DirectoryName + VoiceFileDirectory + "\\" + this.VoiceFile;
			
			if( File.Exists(filename) )
			{
				// �t�@�C���̑��݃`�F�b�N���s���A���݂��Ă�����Đ���~
				VoiceManager.Stop();
				System.Console.WriteLine("Voice " + filename + " ��~");
				return true;
			}
			return false;
		}
		
		
		public void SetVoice(string filename)
		{
			this.StopVoice();
			this.VoiceFile = filename;
			if(this.isPlayVoice) this.PlayVoice();
		}
		
		
		/// <summary>
		/// �S�X���C�h�𗬂��I������Ăяo��
		/// �A�h�x���`���[�p�[�g�̏I��
		/// </summary>
		public void AdventureEnd()
		{
			// �Ƃ肠����BGM�~�߂�
			this.StopBGM();
			this.StopVoice();
			
			// �e�̃X�N���[���������ɂ���ď�����ς���
			switch(this.parentscreen)
			{
				case muphic.ADV.ADVParentScreen.TutorialScreen:
					// �`���[�g���A���������ꍇ�A�`���[�g���A���I�����\�b�h���Ă�
					((TutorialScreen)this.parent).EndTutorial();
					break;
				default:
					break;
			}
		}
		
		/// <summary>
		/// Screen�N���X��Draw���\�b�h���Ă�
		/// </summary>
		public void ScreenDraw()
		{
			base.Draw();
		}
		
		/// <summary>
		/// �o�^���ꂽ�摜��`��
		/// </summary>
		protected void ImageDraw()
		{
			for(int i=0; i<this.pattern.UseImage.Count/3; i++)
			{
				// ArrayList����g�p����o�^�摜���Ƃ��̍��W�����o��
				// �������A�o�^�������݂��Ȃ���Δ�΂�
				string image = (string)this.pattern.UseImage[i*3];
				if( !FileNameManager.GetFileExist(image)) continue;
				
				int    x     = (int)this.pattern.UseImage[i*3+1];
				int    y     = (int)this.pattern.UseImage[i*3+2];
				int    state = FileNameManager.GetFileNames(image).Length;
				
				
				if( state == 1 || image.IndexOf("Support") == -1)
				{
					// state��1�����Ȃ��ꍇ�́A���ʂɕ`��
					DrawManager.Draw(image, x, y);
				}
				else
				{
					// state����������ꍇ�́A1�b�Ԃɂ��������ւ��Ȃ���`�悷��
					
					// �������A�e�L�X�g�̒x���`����s���̓`���[�g���A���⏕�摜�͕\�������Ȃ�
					if( ((TutorialScreen)this.parent).tutorialmain.msgwindow.DelayDraw && ((int)image.IndexOf("TutorialSupport_") != -1) ) continue;
					
					// MainScreen��FrameCount�𗘗p���邽�߁A�e�̃N���X�ɂ���ĎQ�Ƃ̎d�����ς��
					int framecount = 0;
					switch(this.parentscreen)
					{
						case muphic.ADV.ADVParentScreen.TutorialScreen:
							framecount = ((TutorialScreen)this.parent).parent.parent.FrameCount;
							break;
						default:
							break;
					}
					
					// framecount���g�p��state�����I�ɕύX���Ȃ���`��
					DrawManager.Draw(image, x, y, (int)System.Math.Ceiling((double)(framecount+1)/(60/state)) - 1 );
				}
			}
		}
		
		
		/// <summary>
		/// �`�惁�\�b�h
		/// </summary>
		public override void Draw()
		{
			base.Draw ();
			
			// �o�^���ꂽ�摜��`��
			ImageDraw();
			
			// ���b�Z�[�W�E�B���h�E�̕`��
			this.msgwindow.Draw();
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			this.msgwindow.Click(p);
		}
		
		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
			this.msgwindow.MouseMove(p);
		}
	}
}
