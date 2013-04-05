using System;
using System.Windows.Forms;

using Muphic.Manager;
using Muphic.Tools;

namespace Muphic
{
	/// <summary>
	/// muphic ���C���E�B���h�E
	/// </summary>
	public partial class MainWindow : Form
	{
		/// <summary>
		/// muphic ���C���E�B���h�E�̐ÓI�C���X�^���X�B
		/// </summary>
		public static MainWindow Instance { get; private set; }


		#region �t�B�[���h / �v���p�e�B

		/// <summary>
		/// �g�b�v��ʁB
		/// </summary>
		private TopScreen TopScreen { get; set; }


		#region �V�X�e�����s�t���O

		/// <summary>
		/// �v���O���������쒆�ł��邱�Ƃ�\�킷 System.Boolean �l�B
		/// <para>Running �v���p�e�B���g�p���邱�ƁB</para>
		/// </summary>
		private bool __running;

		/// <summary>
		/// �v���O���������쒆�ł��邱�Ƃ������l���擾�܂��͐ݒ肷��B
		/// <para>���̃v���p�e�B�l�� true �̊ԁA�v���O�����̃��C�����[�v�����s�����B</para>
		/// </summary>
		public static bool Running
		{
			get
			{
				return MainWindow.Instance.__running;
			}
			set
			{
				MainWindow.Instance.__running = value;				// �v���O��������̊J�n�ƏI���ɍ��킹��
				MainWindow.Instance.nitifyIcon.Visible = value;		// �^�X�N�g���C�A�C�R���̕\��/��\�����؂�ւ���
			}
		}

		#endregion


		#region ���샂�[�h

		/// <summary>
		/// �v���O�������f�o�b�O���[�h�ł��邱�Ƃ�\�킷 System.Boolean �l�B
		/// <para>IsDebugMode �v���p�e�B���g�p���邱�ƁB</para>
		/// </summary>
		private readonly bool __isDebugMode;

		/// <summary>
		/// �f�o�b�O���[�h���ǂ�����\�� System.Boolean �l���擾����B
		/// </summary>
		public static bool IsDebugMode
		{
			get { return MainWindow.Instance.__isDebugMode; }
		}


		/// <summary>
		/// muphic �̓��샂�[�h���擾����B
		/// </summary>
		public static MuphicOperationMode MuphicOperationMode
		{
			get { return (MuphicOperationMode)(ConfigurationManager.Locked.MuphicOperationMode); }
		}

		#endregion


		/// <summary>
		/// 1 �t���[���`�斈�ɍX�V�����`���ԃf�[�^���擾����B
		/// </summary>
		public static DrawStatusArgs DrawStatus { get; private set; }

		#endregion


		#region �R���X�g���N�^ (�N������)

		/// <summary>
		/// muphic ���C���E�B���h�E�𐶐����A�N���������s���B
		/// </summary>
		private MainWindow()
		{
			try
			{
				#region �f�o�b�O���[�h�̔���
#if DEBUG
				this.__isDebugMode = true;
#else
				this.__isDebugMode = false;
#endif
				#endregion

				// ���C���E�B���h�E�̃t�H�[���̐ݒ���s��
				InitializeComponent();

				// �e�Ǘ��N���X�̐ÓI�C���X�^���X�����Ə�����
				if (this.InitializeManager())
				{									// �����Ə������ɐ��������ꍇ
					this.__running = true;			// ���C�����[�v���s�t���O�𗧂Ă�
				}
				else
				{									// �����Ə������Ɏ��s�����ꍇ
					this.__running = false;			// ���C�����[�v���s�t���O���~�낵
					return;							// �v���O�����I��
				}

				// �V�X�e���y�у{�^�������摜�̓ǂݍ���
				this.LoadTextureFile(Settings.ResourceNames.SystemImages);

				// �e��ʂ̃t�@�C����\�ߓǂݍ���
				if (ConfigurationManager.Current.IsLoadTextureFilePreliminarily)
				{
					// this.LoadTextureFile(Settings.ResourceNames.TopScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.CompositionScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.MakeStoryScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.EntitleScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.PlayStoryScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.ScoreScreenImages);
				}

				// �}�E�X�ݒ�
				this.IsClicked = false;					// �N���b�N���OFF
				this.CursorState = 0;					// �J�[�\���̏�Ԃ�������
				System.Windows.Forms.Cursor.Hide();		// Windows�ʏ�̃}�E�X�J�[�\�����B��
				this.NowMouseLocation = new System.Drawing.Point(this.Width + 20, this.Height + 20);

				// �J�[�\���e�N�X�`���̓o�^
				DrawManager.Regist("muphicCursor", 0, 0, "IMAGE_SYSTEM_CURSOR_OFF", "IMAGE_SYSTEM_CURSOR_ON");

				// �V�X�e���g���C�A�C�R���̕\��
				this.nitifyIcon.Visible = true;

				MainWindow.DrawStatus = new DrawStatusArgs();

				// NowLoading�_�C�A���O�̃C���X�^���X��
				DrawManager.CreateNowLoaing();

				// �g�b�v��ʂ̃C���X�^���X��
				TopScreen = new TopScreen(this);

				//KeyPress += new KeyPressEventHandler(key_press);
				//font = new System.Drawing.Font("MS Gothic", 12);
				//brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
			}
			catch (Exception e)
			{
				// �J�[�\����\��
				System.Windows.Forms.Cursor.Show();

				// ���������ɔ���������O�� Application.ThreadException �C�x���g���������Ȃ��̂ŁA������L���b�`�����O�ɏ�������
				LogFileManager.WriteLineError(e.ToString());

				// ���b�Z�[�W�E�B���h�E�̕\��
				MessageBox.Show(
					CommonTools.GetResourceMessage(Properties.Resources.ErrorMsg_MainWindow_Show_UnhandledException_Text, e.Message),
					Properties.Resources.ErrorMsg_MainWindow_Show_UnhandledException,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				// ���C�����[�v�͎��s�����v���O�����I��
				this.__running = false;
			}
		}

		#endregion

	}


	/// <summary>
	/// muphic �̓����\�����ʎq���w�肷��B
	/// </summary>
	public enum MuphicOperationMode : int
	{
		/// <summary>
		/// �ʏ퓮��
		/// </summary>
		NormalMode = 0,

		/// <summary>
		/// ���ƒ��̓��� (�����p)
		/// </summary>
		StudentMode = 1,

		/// <summary>
		/// ���ƒ��̓��� (�u�t�p)
		/// </summary>
		TeacherMode = 2,
	}
}
