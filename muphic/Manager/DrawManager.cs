using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Muphic.Common;
using Muphic.Tools;

namespace Muphic.Manager
{
	using DxManager = Microsoft.DirectX.Direct3D.Manager;

	/// <summary>
	/// �`��Ǘ��N���X ver.12 (�V���O���g���E�p���s��) 
	/// <para>Direct3D �𗘗p�����e�N�X�`���̕`��y�ъǗ��A�e���\���p�[�c�Ŏg�p����e�N�X�`���̓o�^�E�폜���s���B</para>
	/// </summary>
	public sealed class DrawManager : Manager
	{
		// muphic DrawManager Ver.12
		//   �EDirectX �g�p
		//   �E�����摜�̗p
		//   �E����G���W���č\�z

		/// <summary>
		/// �`��Ǘ��N���X�̐ÓI�C���X�^���X (�V���O���g���p�^�[��)�B
		/// </summary>
		private static readonly DrawManager __instance = new DrawManager();

		/// <summary>
		/// �`��Ǘ��N���X�̐ÓI�C���X�^���X (�V���O���g���p�^�[��) ���擾����B
		/// </summary>
		private static DrawManager Instance
		{
			get { return Muphic.Manager.DrawManager.__instance; }
		}


		#region �v���p�e�B

		/// <summary>
		/// �e�N�X�`���e�[�u��
		/// <para>�e�N�X�`���t�@�C���p�X���L�[�Ƃ��A�����摜�{�̂��i�[����B</para>
		/// </summary>
		private Dictionary<string, Texture> TextureTable { get; set; }

		/// <summary>
		/// �o�^�e�[�u��
		/// <para>RegistTexture �œo�^�����L�[���i�[����B</para>
		/// </summary>
		private List<List<string>> RegistTable { get; set; }

		/// <summary>
		/// �e�N�X�`���o�^���ł��邱�Ƃ������������擾�܂��͐ݒ肷��B
		/// <para>�o�^���ł���� 0 �ȏ�� RegistTable �� index �ԍ��A�o�^���łȂ���� -1 �ƂȂ�B</para>
		/// </summary>
		private int RegistNum { get; set; }

		/// <summary>
		/// DirectX �ł̕`���̉�� (�g�b�v���x���E�B���h�E)
		/// </summary>
		private MainWindow Owner { get; set; }


		/// <summary>
		/// �E�B���h�E���[�h�ł��邱�Ƃ������l���擾�܂��͐ݒ肷��B
		/// </summary>
		private bool IsWindow
		{
			get { return ConfigurationManager.Current.IsWindow; }
			set { ConfigurationManager.Current.IsWindow = value; }
		}

		/// <summary>
		/// ��Ƀt���X�N���[�����[�h�Ŏg�p����O���t�B�b�N�A�_�v�^�̔ԍ����擾�܂��͐ݒ肷��B
		/// </summary>
		private int AdapterNum
		{
			get { return ConfigurationManager.Current.AdapterNum; }
			set { ConfigurationManager.Current.AdapterNum = value; }
		}

		/// <summary>
		/// NowLoading �_�C�A���O�B
		/// </summary>
		private Dialog NowLoadingDialog { get; set; }


		/// <summary>
		/// �񓯊��ł̃e�N�X�`���ǂݍ��ݗp���[�J�[�X���b�h�B
		/// </summary>
		private BackgroundWorker TextureLoader { get; set; }


		/// <summary>
		/// DirectX �f�o�C�X�B
		/// </summary>
		private Device Device { get; set; }

		/// <summary>
		/// Direct3D �X�v���C�g�B
		/// </summary>
		private Sprite Sprite { get; set; }

		/// <summary>
		/// Direct3D ���C���B
		/// </summary>
		private Line Line { get; set; }

		/// <summary>
		/// �y�f�o�b�O�p�z�t�H���g�B
		/// </summary>
		private Microsoft.DirectX.Direct3D.Font Font { get; set; }

		#endregion


		#region �R���X�g���N�^ / ������

		/// <summary>
		/// �`��Ǘ��N���X�̐V�����C���X�^���X������������B
		/// </summary>
		private DrawManager()
		{
			this.TextureTable = new Dictionary<string, Texture>();	// �e�N�X�`���e�[�u���̏�����
			this.RegistTable = new List<List<string>>();			// �o�^�e�[�u���̏�����
			this.RegistNum = -1;									// �o�^���t���O���~�낵�Ă���
			this.NowLoadingDialog = null;

			this.TextureLoader = new BackgroundWorker();
			this.TextureLoader.DoWork += new DoWorkEventHandler(this._TextureLoad);
			this.TextureLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this._TextureLoadCompleted);
		}


		/// <summary>
		/// �`��Ǘ��N���X�̐ÓI�C���X�^���X�����y�юg�p����`��f�o�C�X���̏��������s���B
		/// �C���X�^���X������ɂP�x�������s�ł��Ȃ��B
		/// </summary>
		/// <returns>������������ɏI�������ꍇ�� true�A����ȊO�� false�B</returns>
		private bool _Initialize(MainWindow mainScreen)
		{
			if (this._IsInitialized) return false;						// �������ς݂łȂ��ꍇ�݈̂ȉ������s

			this.Owner = mainScreen;									// �`���̐ݒ�

			if (!this._InitializeDevice()) return false;				// �f�o�C�X�̏����� ���s������v���O�����I��

			this._InitializeLine(this.Device);							// ���C���̏�����

			this._InitializeFont();										// �f�o�b�O���̂݃t�H���g����

			DebugTools.ConsolOutputMessage("DrawManager -Initialize", "�`��Ǘ��N���X��������", true);

			return this._IsInitialized = true;							// ��������ɏ������ς݃t���O�𗧂Ă�
		}

		/// <summary>
		/// �f�o�C�X�̏��������s���B
		/// </summary>
		/// <returns>������������ɏI������� true�A�ُ킪���������ꍇ�� false�B</returns>
		private bool _InitializeDevice()
		{
			try
			{
				if (!(this.AdapterNum < DxManager.Adapters.Count))
				{															// �A�_�v�^���𒲂ׁA�w�肳�ꂽ�A�_�v�^�ԍ����Ó����`�F�b�N����
					this.AdapterNum = 0;									// �A�_�v�^�̐��𒴂���ԍ��������ꍇ�A�����I�� 0 �ɂ���
				}

				PresentParameters pp = this._SetParameters(true);			// �f�o�C�X�ݒ� (�ŏ��� Window ���[�h�Ő�������)
				if (pp == null) return false;								// �ݒ�Ɏ��s������߂�

				if (!this._CreateDevice(pp)) return false;					// �f�o�C�X����
			}
			catch (TypeInitializationException exception)
			{
				// �f�o�C�X�������ɂ��̗�O������������ADirectX���C���X�g�[������Ă��Ȃ��Ɣ��f

				#region	���O�̏������݂ƃG���[���b�Z�[�W�̕\��

				LogFileManager.WriteLineError("TypeInitializationException", "�v���O�����̓���ɕK�v�ȃ����^�C�����s�����Ă��܂��B");
				LogFileManager.WriteLineError("                           ", "�ڍׂ͈ȉ��̃��b�Z�[�W���Q�Ƃ��Ă��������B");
				LogFileManager.WriteLineError(exception.ToString(), "");

				// ���b�Z�[�W�E�B���h�E�\��
				MessageBox.Show(this.Owner,
					Properties.Resources.ErrorMsg_DrawMgr_Show_FailureDirectXLoad_Text,
					Properties.Resources.ErrorMsg_DrawMgr_Show_FailureDirectXLoad_Caption,
					MessageBoxButtons.OK, MessageBoxIcon.Error);

				#endregion

				return false;
			}

			this.Sprite = new Sprite(this.Device);							// �X�v���C�g�I�u�W�F�N�g�̃C���X�^���X��

			this.Owner.ControlBox = this.IsWindow;							// �t���X�N���[�����[�h�̏ꍇ�̓L���v�V�����o�[���\��

			LogFileManager.WriteLine(										// �E�B���h�E/�t���X�N���[���̏�Ԃ����O�ɏ�������
				Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
				this._GetDisplayModeName(this.IsWindow)
			);

			return true;
		}

		#endregion


		#region �f�o�C�X�֘A

		/// <summary>
		/// Direct3D.PresentParameters �̐ݒ���s���B
		/// </summary>
		/// <param name="isWindow">�E�B���h�E���[�h�Őݒ���s���ꍇ�� true�A�t���X�N���[�����[�h�Őݒ���s���ꍇ�� false�B</param>
		/// <returns>�ݒ肳�ꂽ Direct3D.PresentParameters�B�ݒ�Ɏ��s�����ꍇ�� null�B</returns>
		private PresentParameters _SetParameters(bool isWindow)
		{
			PresentParameters pp = new PresentParameters();		// �p�����[�^�ݒ�N���X �f�o�C�X���g�p������̐ݒ���s��

			pp.DeviceWindow = this.Owner;						// �`��Ώۂ̃E�B���h�E���w��
			pp.DeviceWindowHandle = this.Owner.Handle;			// �`�悷��R���g���[�����w��
			pp.SwapEffect = SwapEffect.Discard;					// �T�[�t�F�C�X�t���b�v�̓f�B�X�v���C�h���C�o���Őݒ肳����(�ŗ�)
			pp.EnableAutoDepthStencil = false;					// �[�x�X�e���V���o�b�t�@ 3D�ł͂Ȃ��̂�false�ł���
			pp.AutoDepthStencilFormat = DepthFormat.D16;		// �����[�x�X�e���V���T�[�t�F�C�X�̃t�H�[�}�b�g �[�x�X�e���V���o�b�t�@��false�ɂ������ߖ��������
			pp.BackBufferCount = 0;								// �o�b�N�o�b�t�@�̖��� 0�w���1���ɂȂ�Ǝv����
			pp.MultiSample = MultiSampleType.None;				// �A���`�G�C���A�X�̐ݒ� �g�p���Ȃ��̂�None
			pp.MultiSampleQuality = 0;							// �A���`�G�C���A�X�̕i�� �g�p���Ȃ��̂�0
			pp.PresentFlag = PresentFlag.None;					// �p�r�s��
			pp.PresentationInterval = PresentInterval.Default;	// �`��̏��������̃^�C�~���O�̎w�� Default�ł����炵��

			if (isWindow)										// �E�B���h�E���[�h / �t���X�N���[�����[�h�ɉ������ݒ���s��
			{
				pp.Windowed = true;										// �E�B���h�E���[�h��

				pp.BackBufferWidth = 0;									// Direct3D�f�o�C�X���g�p����o�b�N�o�b�t�@�̃T�C�Y
				pp.BackBufferHeight = 0;								// 0���w�肷���DeviceWindow�̃N���C�A���g�T�C�Y�Ɠ����ɂȂ�
				pp.BackBufferFormat = Format.Unknown;					// �o�b�N�o�b�t�@�̃t�H�[�}�b�g �E�B���h�E���[�h�Ȃ̂�Unknown�ł���
				pp.FullScreenRefreshRateInHz = 0;						// ���t���b�V�����[�g�̐ݒ� �E�B���h�E���[�h�Ȃ̂�0�ł���
			}
			else
			{
				pp.Windowed = false;										// �t���X�N���[�����[�h��

				bool findDisplayMode = false;								// �g�p����f�B�X�v���C���[�h�����������ꍇ�̃t���O
				int width = Settings.System.Default.WindowSize_Width;		// �E�B���h�E�T�C�Y
				int height = Settings.System.Default.WindowSize_Height;		// �E�B���h�E�T�C�Y
				int refreshRate = ConfigurationManager.Current.RefreshRate;	// �g�p���郊�t���b�V�����[�g				

				// �f�B�X�v���C���[�h��񋓂��A�N���C�A���g�Ɠ����T�C�Y���������t���b�V�����[�g�̃��[�h��T��
				foreach (DisplayMode dm in DxManager.Adapters[this.AdapterNum].SupportedDisplayModes)
				{
					if (dm.Width == width && dm.Height == height && dm.RefreshRate == refreshRate)
					{
						pp.BackBufferWidth = dm.Width;					// �����Ƀ}�b�`����f�B�X�v���C���[�h�����݂���Ύg�p����
						pp.BackBufferHeight = dm.Height;				// �o�b�N�o�b�t�@�̃T�C�Y�̓N���C�A���g�Ɠ���
						pp.BackBufferFormat = dm.Format;				// �t�H�[�}�b�g�̓f�B�X�v���C�ɏ]��
						pp.FullScreenRefreshRateInHz = refreshRate;		// ���t���b�V�����[�g�͎w�肳�ꂽ�l
						findDisplayMode = true;							// �f�B�X�v���C���[�h�������������Ƃ������t���O�𗧂Ă�
						break;
					}
				}

				if (!findDisplayMode)
				{
					// �w�肳�ꂽ���[�h���Ȃ���΁A�G���[���b�Z�[�W��\�����ďI������
					MessageBox.Show(this.Owner,
						CommonTools.GetResourceMessage(
							Properties.Resources.ErrorMsg_DrawMgr_Show_FailureDisplayMode_Text,
							width.ToString(),
							height.ToString(),
							refreshRate.ToString()
						),
						Properties.Resources.ErrorMsg_DrawMgr_Show_FailureDisplayMode_Caption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);

					return null;
				}
			}

			return pp;
		}


		/// <summary>
		/// �f�o�C�X�̐������s���B
		/// </summary>
		/// <param name="pp">�f�o�C�X�ݒ�B</param>
		/// <returns>����ɐ������ꂽ�ꍇ�� true�A����ȊO�� false�B</returns>
		private bool _CreateDevice(PresentParameters pp)
		{
			try
			{
				// �f�o�C�X����   �n�[�h�E�F�A�A�N�Z�����[�^�A�n�[�h�E�F�A�ɂ�钸�_����
				//                �ŏ�ʂ̃p�t�H�[�}���X�ƂȂ邪�A�r�f�I�J�[�h�ɂ���Ă͎����ł��Ȃ����������݂��邽�߁A
				//                ���s�����牺�ʂ̃p�t�H�[�}���X�Ńf�o�C�X�𐶐�����
				this.Device = new Device(this.AdapterNum, DeviceType.Hardware, this.Owner.Handle, CreateFlags.HardwareVertexProcessing, pp);
				LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_CreateDevice, Properties.Resources.Msg_DrawMgr_CreateDevice_HH);
			}
			catch (DirectXException)
			{
				try
				{
					// �f�o�C�X����  �n�[�h�E�F�A�A�N�Z�����[�^�A�\�t�g�E�F�A�ɂ�钸�_����
					//               �O���t�B�b�N���`�b�v�Z�b�g�����̃��b�v�g�b�v�͑���ɂȂ�
					this.Device = new Device(this.AdapterNum, DeviceType.Hardware, this.Owner.Handle, CreateFlags.SoftwareVertexProcessing, pp);
					LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_CreateDevice, Properties.Resources.Msg_DrawMgr_CreateDevice_HS);
				}
				catch (DirectXException)
				{
					try
					{
						// �f�o�C�X����   ���t�@�����X���X�^���C�U�A�\�t�g�E�F�A�ɂ�钸�_����
						//                �p�t�H�[�}���X�͍ł��Ⴂ���A�w�ǂ̏����𐧌��Ȃ��s�����Ƃ��ł���
						//                ����Ŏ��s�����玖����f�o�C�X�͐����ł��Ȃ�
						this.Device = new Device(this.AdapterNum, DeviceType.Reference, this.Owner.Handle, CreateFlags.SoftwareVertexProcessing, pp);
						LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_CreateDevice, Properties.Resources.Msg_DrawMgr_CreateDevice_SS);
					}
					catch (DirectXException e)
					{
						// �f�o�C�X�������s
						LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_CreateDevice, Properties.Resources.Msg_DrawMgr_CreateDevice_Failure);
						LogFileManager.WriteLine(e.ToString());

						// ���b�Z�[�W�E�B���h�E��\�����I��
						MessageBox.Show(this.Owner,
							Properties.Resources.ErrorMsg_DrawMgr_Show_FailureCreateDevice_Text,
							Properties.Resources.ErrorMsg_DrawMgr_Show_FailureCreateDevice_Caption,
							MessageBoxButtons.OK, MessageBoxIcon.Error);

						return false;
					}
				}
			}

			return true;
		}


		/// <summary>
		/// �f�o�C�X�̏�Ԃ��`�F�b�N����B���X�g���Ă����ꍇ�̓f�o�C�X�̍Đ��������݂�B
		/// </summary>
		/// <returns>�����Ԃ܂��̓f�o�C�X���Đ��������ꍇ�� true�A����ȊO (��Ƀf�o�C�X���X�g��) �� false�B</returns>
		private bool _CheckDevice()
		{
			int deviceResult;	// �f�o�C�X�̏�Ԃ�\��(���ʃR�[�h�i�[�p)

			if (!this.Device.CheckCooperativeLevel(out deviceResult))
			{
				// CheckCooperativeLevel���\�b�h�Ńf�o�C�X�̏�Ԃ��`�F�b�N
				// �����ԂłȂ����false���Ԃ�AdeviceResult�Ɍ��ʃR�[�h���i�[�����

				switch ((ResultCode)deviceResult)				// �f�o�C�X�ُ펞�͌��ʃR�[�h�ɉ������ȉ��̏������s��
				{
					case ResultCode.DeviceLost:					// �f�o�C�X�����X�g���Ă���A���Z�b�g���o���Ȃ���Ԃł����
						Thread.Sleep(1000);						// 1�b�ҋ@����
						return false;							// false��Ԃ�

					case ResultCode.DeviceNotReset:				// ���Z�b�g�\��Ԃł���΁A�f�o�C�X���Đ�������true��Ԃ�
						this._ResetDevice(this.Device.PresentationParameters);
						return true;

					default:									// ����ȊO�̏ꍇ�͗\�����Ă��Ȃ�����
						Muphic.MainWindow.Running = false;		// �v���O�����I��
						return false;							// false��Ԃ�
				}
			}

			return true;		// �����Ԃł����true��Ԃ�
		}


		/// <summary>
		/// �f�o�C�X�̍Đ������s���B
		/// </summary>
		/// <param name="pp">�f�o�C�X�ݒ�B</param>
		private void _ResetDevice(PresentParameters pp)
		{
			this.Device.Reset(pp);
			LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_ResetDevice);
		}


		/// <summary>
		/// �\���ݒ�Ńt���X�N���[�����[�h���ݒ肳��Ă����ꍇ�A�t���X�N���[�����[�h�Ńf�o�C�X���Đ�������B�N���t�F�[�Y�I�����Ɏg�p����B
		/// </summary>
		/// <returns>����ɍĐ������ꂽ�ꍇ�A�܂��̓t���X�N���[�����[�h�ɐݒ肳��Ă��Ȃ������ꍇ�� true�A����ȊO�� false�B</returns>
		private bool _SetFullScreenMode()
		{
			if (!this.IsWindow)
			{
				return this._ChangeWindowMode(this.IsWindow);
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// �E�B���h�E���[�h / �t���X�N���[�����[�h�̐؂�ւ����s���B
		/// </summary>
		/// <returns>����ɐ؂�ւ���ꂽ�ꍇ�� true�A����ȊO�� false�B</returns>
		private bool _ChangeWindowMode()
		{
			return this._ChangeWindowMode(!this.IsWindow);
		}

		/// <summary>
		/// �E�B���h�E���[�h / �t���X�N���[�����[�h�̐؂�ւ����s���B
		/// </summary>
		/// <param name="isWindow">�E�B���h�E���[�h�ɂ���ꍇ�� true�A����ȊO�� false�B</param>
		/// <returns>����ɐ؂�ւ���ꂽ�ꍇ�� true�A����ȊO�� false�B</returns>
		private bool _ChangeWindowMode(bool isWindow)
		{
			PresentParameters pp = this._SetParameters(isWindow);

			if (pp != null)										// �f�o�C�X�ݒ���s��
			{													// �ݒ�ɐ���������
				this.IsWindow = isWindow;						// �E�B���h�E/�t���X�N���[�����[�h�ؑ�
				this._ResetDevice(pp);							// �f�o�C�X�Đ���
				this.Owner.ControlBox = isWindow;				// �t���X�N���[�����[�h�̏ꍇ�̓L���v�V�����o�[���\��
				this.Owner.TopMost = !isWindow;					// �E�B���h�E���[�h���͍őO�ʕ\��off
				this.Owner.ShowIcon = isWindow;					// �E�B���h�E���[�h���̓A�C�R����\��

				LogFileManager.WriteLine(
					Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
					CommonTools.GetResourceMessage(
						Properties.Resources.Msg_DrawMgr_ChangeIsWindow_Success,
						this._GetDisplayModeName(!isWindow),
						this._GetDisplayModeName(isWindow)
					)
				);

				return true;
			}
			else
			{
				LogFileManager.WriteLine(
					Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
					CommonTools.GetResourceMessage(
						Properties.Resources.Msg_DrawMgr_ChangeIsWindow_Failure,
						this._GetDisplayModeName(!isWindow),
						this._GetDisplayModeName(isWindow)
					)
				);

				return false;
			}
		}


		#region backup
		///// <summary>
		///// �E�B���h�E���[�h / �t���X�N���[�����[�h�̐؂�ւ����s���B
		///// </summary>
		///// <returns>����ɐ؂�ւ���ꂽ�ꍇ�� true�A����ȊO�� false�B</returns>
		//private bool _ChangeWindowMode()
		//{
		//    PresentParameters pp = this._SetParameters(!this.IsWindow);

		//    if (pp != null)											// �f�o�C�X�ݒ���s��
		//    {														// �ݒ�ɐ���������
		//        this.IsWindow = !this.IsWindow;						// �E�B���h�E/�t���X�N���[�����[�h�ؑ�
		//        this._ResetDevice(pp);								// �f�o�C�X�Đ���
		//        this.Owner.ControlBox = this.IsWindow;				// �t���X�N���[�����[�h�̏ꍇ�̓L���v�V�����o�[���\��
		//        this.Owner.TopMost = !this.IsWindow;				// �E�B���h�E���[�h���͍őO�ʕ\��off

		//        LogFileManager.WriteLine(
		//            Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
		//            CommonTools.GetResourceMessage(
		//                Properties.Resources.Msg_DrawMgr_ChangeIsWindow_Success,
		//                this._GetDisplayModeName(!this.IsWindow),
		//                this._GetDisplayModeName(this.IsWindow)
		//            )
		//        );

		//        return true;
		//    }
		//    else
		//    {
		//        LogFileManager.WriteLine(
		//            Properties.Resources.Msg_DrawMgr_CreateDevice_DisplayMode,
		//            CommonTools.GetResourceMessage(
		//                Properties.Resources.Msg_DrawMgr_ChangeIsWindow_Failure,
		//                this._GetDisplayModeName(!this.IsWindow),
		//                this._GetDisplayModeName(this.IsWindow)
		//            )
		//        );

		//        return false;
		//    }
		//}
		#endregion

		/// <summary>
		/// �E�B���h�E/�t���X�N���[�����[�h����Ԃ��B
		/// </summary>
		/// <param name="isWindow">�E�B���h�E���[�h�̏ꍇ�� true�A�t���X�N���[�����[�h�̏ꍇ�� false�B</param>
		/// <returns>���b�Z�[�W���\�[�X�Œ�߂�ꂽ�E�B���h�E/�t���X�N���[�����[�h�̕�����B</returns>
		private string _GetDisplayModeName(bool isWindow)
		{
			return (isWindow) ? Properties.Resources.Msg_DrawMgr_WindowMode : Properties.Resources.Msg_DrawMgr_FullScreenMode;
		}

		#endregion


		#region ���

		/// <summary>
		/// �f�o�C�X��e�N�X�`���̉�����s���B
		/// </summary>
		private void _Dispose()
		{
			// �ǂݍ��񂾓����摜���̃e�N�X�`���t�@�C����S�Ĕj��
			foreach (KeyValuePair<string, Texture> kvp in this.TextureTable)
			{
				this._SafeDispose(kvp.Value);
			}
			this.TextureTable.Clear();

			// �X�v���C�g��f�o�C�X���� DirectX �I�u�W�F�N�g��j��
			this._SafeDispose(this.Line);
			this._SafeDispose(this.Sprite);
			this._SafeDispose(this.Device);
		}

		#endregion


		#region �V�[��/�X�v���C�g

		/// <summary>
		/// �f�o�C�X�̃V�[���`����J�n����B
		/// </summary>
		/// <param name="clearColor">�`��O�ɉ�ʂ��N���A����ꍇ���̐F���w�肵�� Color �^�A�N���A���Ȃ��ꍇ�� Color.Empty�B</param>
		private void _BeginScene(Color clearColor)
		{
			// �N���A�w�肳��Ă���ꍇ�A�`��J�n�O�ɔ��ŃN���A����
			if (clearColor != Color.Empty) this.Device.Clear(ClearFlags.Target, clearColor, 0, 0);

			this.Device.BeginScene();	// �V�[���`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̃V�[���`����I������B
		/// </summary>
		/// <returns>����ɏI�������ꍇ�� true�A����ȊO�̏ꍇ�� false�B</returns>
		private bool _EndScene()
		{
			try
			{
				this.Device.EndScene();		// �V�[���`��I��
				this.Device.Present();		// �v���[���e�[�V����(�����_�[�^�[�Q�b�g�̃o�b�N�o�b�t�@���t�����g�o�b�t�@��)
			}
			catch (DeviceLostException)		// �f�o�C�X���X�g�̗�O�����������ꍇ
			{
				return false;				// false��Ԃ�
			}

			return true;
		}


		/// <summary>
		/// �X�v���C�g�`����J�n����B
		/// </summary>
		private void _BeginSprite()
		{
			this.Sprite.Begin(SpriteFlags.AlphaBlend);		// �X�v���C�g�`��J�n(�����_�����O�I�v�V�����ɔ������������w��)
		}

		/// <summary>
		/// �X�v���C�g�`����I������B
		/// </summary>
		private void _EndSprite()
		{
			this.Sprite.End();		// �X�v���C�g�`��I��
		}

		#endregion


		#region NowLoading

		/// <summary>
		/// NowLoading �_�C�A���O��`�悷��B
		/// </summary>
		private void _DrawNowLoadingDialog()
		{
			if (!Settings.System.Default.EnabledNowLoading) return;			// �V�X�e���ݒ�m�F �\�����Ȃ��ݒ�̏ꍇ�I��

			DrawManager.BeginScene(Color.Empty);							// NowLoading ���� (�����) �X�v���C�g�`��J�n
			var drawStatus = new DrawStatusArgs();
			drawStatus.ShowDialog = MainWindow.DrawStatus.ShowDialog;		// ��ԃf�[�^���擾���_�C�A���O�̏󋵂������p��
			this.NowLoadingDialog.Draw(drawStatus);							// NowLoading �_�C�A���O�`��
			DrawManager.EndScene();											// (�������) �X�v���C�g�`��I��

			DebugTools.ConsolOutputMessage("DrawManager -DrawNowLoading", "Now Loading... �_�C�A���O�\��");		// ���b�Z�[�W�\���̂悤�Ȃ���
		}

		/// <summary>
		/// NowLoading �_�C�A���O���\�z����B
		/// </summary>
		private void _CreateNowLoadingDialog()
		{
			this.NowLoadingDialog = new Dialog("NowLoading", DialogButtons.None, DialogIcons.Caution, "IMAGE_DIALOGTITLE_NOWLOADING", "IMAGE_DIALOGMSG_NOWLOADING");
		}


		#endregion


		#region �t�H���g�̐���/������̕`��

		/// <summary>
		/// �y�f�o�b�O�p�z�t�H���g�̐������s���B
		/// </summary>
		[Conditional("DEBUG")]
		private void _InitializeFont()
		{
			FontDescription fd = new FontDescription();		// �t�H���g�f�[�^�̍\���̂𐶐�

			fd.Height = 16;									// ����20pt
			fd.FaceName = "�l�r �S�V�b�N";					// MS�S�V�b�N���w��
			fd.Quality = FontQuality.Default;				// �f�o�b�O�p�Ȃ̂ŕi���͍Œ���ł���
			//fd.Quality = FontQuality.AntiAliased;
			//fd.Height = 24;
			//fd.OutputPrecision = Precision.PsOnly;

			this.Font = new Microsoft.DirectX.Direct3D.Font(this.Device, fd);	// �t�H���g�̐���
		}


		/// <summary>
		/// �y�f�o�b�O�p�z�������`�悷��B
		/// </summary>
		/// <param name="str">�`�悷�镶����B</param>
		/// <param name="x">�J�nx���W�B</param>
		/// <param name="y">�J�ny���W�B</param>
		/// <param name="color">������̐F�B</param>
		[Conditional("DEBUG")]
		private void _DrawString(string str, int x, int y, Color color)
		{
			// �_�~�[�̋ɏ��e�N�X�`�����w�肳�ꂽ���W�ŃX�v���C�g�`�悵�C�������0,0�ɕ`�悷��
			// DrawText�ɃX�v���C�g���w�肷���, �Ō�ɃX�v���C�g�`�悳�ꂽ�e�N�X�`���̍��W����ɂȂ�d�l(?)�̂���
			Muphic.Manager.DrawManager.Instance._DrawTexture("IMAGE_DUMMY", new Point(x, y), 0, false, 1.0F, 0.0F, Color.FromArgb(0, 255, 255, 255));
			this.Font.DrawText(this.Sprite, str, 0, 0, color);
		}

		#endregion


		#region �����摜�t�@�C���̓Ǎ�/�폜
		
		/// <summary>
		/// �����̓����摜��ǂݍ��ށB
		/// </summary>
		/// <param name="fileNames">�ǂݍ��ޓ����摜�̃p�X�Q�B</param>
		/// <returns>�S�Ă̓ǂݍ��݂ɐ��������ꍇ�� true�A����ȊO�� false�B</returns>
		private bool _LoadTextureFiles(string[] fileNames)
		{
			bool result = true;

			foreach (string fileName in fileNames)
			{
				if (!this._LoadTextureFile(fileName)) result = false;
			}

			return result;
		}

		/// <summary>
		/// �����摜��ǂݍ��ށB
		/// </summary>
		/// <param name="fileName">�ǂݍ��ޓ����摜�̃p�X�B</param>
		/// <returns>�ǂݍ��݂ɐ��������ꍇ�� true�A����ȊO�� false�B</returns>
		private bool _LoadTextureFile(string fileName)
		{
			// �e�N�X�`���e�[�u�����̑��݃`�F�b�N
			if (this.TextureTable.ContainsKey(fileName))
			{
				// ���ɓ����t�@�C�����̓����摜�t�@�C�����ǂݍ��܂�Ă����ꍇ�͏I��
				//LogFileManager.WriteLine(
				//    Properties.Resources.Msg_DrawMgr_LoadTextureFile,
				//    CommonTools.GetResourceMessage(Properties.Resources.Msg_DrawMgr_LoadTextureFile_Failure_Reason_Loaded, fileName)
				//);

				return false;
			}

			// �t�@�C���̑��݃`�F�b�N
			if (!ArchiveFileManager.Exists(fileName))
			{
				// �^����ꂽ�t�@�C�����̃e�N�X�`�������݂��Ȃ���ΏI��
				//LogFileManager.WriteLineError(
				//    Properties.Resources.Msg_DrawMgr_LoadTextureFile,
				//    CommonTools.GetResourceMessage(
				//        Properties.Resources.Msg_DrawMgr_LoadTextureFile_Failure_Reason_NotExist, fileName)
				//);
				throw new Exception(
					CommonTools.GetResourceMessage(Properties.Resources.Msg_DrawMgr_LoadTextureFile_Failure_Reason_NotExist_, fileName)
				);
			}

			#region �ǂݍ��ݎ��Ԍv���J�n
			Stopwatch readTimeWatch = new Stopwatch();
			readTimeWatch.Start();
			#endregion

			// �e�N�X�`���̓o�^
			this.TextureTable.Add(fileName, new Texture(this.Device, new MemoryStream(ArchiveFileManager.GetData(fileName)), Usage.None, Pool.Managed));

			LogFileManager.WriteLine(
				Properties.Resources.Msg_DrawMgr_LoadTextureFile,
				ConfigurationManager.Current.IsLoggingWithFullPath ? System.IO.Path.GetFullPath(fileName) : fileName
			);

			#region �ǂݍ��ݎ��Ԍv���I���E���O�o��
			readTimeWatch.Stop();
			LogFileManager.WriteLine(
				Muphic.Properties.Resources.Msg_DrawMgr_TextureLoadTime_Title,
				Tools.CommonTools.GetResourceMessage(Muphic.Properties.Resources.Msg_DrawMgr_TextureLoadTime, readTimeWatch.ElapsedMilliseconds.ToString())
			);
			#endregion

			return true;
		}


		/// <summary>
		/// �����摜���폜����B
		/// </summary>
		/// <param name="fileName">�폜���铝���摜�̃p�X�B</param>
		/// <returns>�폜�ɐ��������� true�A����ȊO�� false�B</returns>
		private bool _UnloadTextureFile(string fileName)
		{
			// �e�N�X�`���e�[�u�����̑��݃`�F�b�N
			if (!this.TextureTable.ContainsKey(fileName))
			{
				// ���ɓ����t�@�C�����̓����摜�t�@�C�����ǂݍ��܂�Ă����ꍇ�͏I��
				LogFileManager.WriteLineError(Properties.Resources.Msg_DrawMgr_UnloadTextureFile, fileName);
				return false;
			}

			// �t�@�C���̑��݃`�F�b�N
			if (!ArchiveFileManager.Exists(fileName))
			{
				// �^����ꂽ�t�@�C�����̃e�N�X�`�������݂��Ȃ���ΏI��
				LogFileManager.WriteLineError(Properties.Resources.Msg_DrawMgr_UnloadTextureFile, fileName);
				return false;
			}

			// �e�N�X�`���̍폜
			this._SafeDispose(this.TextureTable[fileName]);
			this.TextureTable.Remove(fileName);

			LogFileManager.WriteLine(Properties.Resources.Msg_DrawMgr_UnloadTextureFile, fileName);
			return true;
		}


		/// <summary>
		/// �w�肵�������摜���ǂݍ��܂�Ă��邩�ǂ������m�F����B
		/// </summary>
		/// <param name="fileName">�m�F���铝���摜�̃p�X�B</param>
		/// <returns>�ǂݍ��܂�Ă����ꍇ�� true�A����ȊO�� false�B</returns>
		private bool _ExistsTextureFile(string fileName)
		{
			return this.TextureTable.ContainsKey(fileName);
		}



		/// <summary>
		/// �����摜����̃e�N�X�`��������񓯊��Ŏ��s����B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _TextureLoad(object sender, DoWorkEventArgs e)
		{
			List<Texture> textures = new List<Texture>();		// �������ꂽ�e�N�X�`���̃��X�g
			List<string> messages = new List<string>();			// ���b�Z�[�W���X�g

			foreach (string fileName in (List<string>)e.Argument)
			{
				if (this.TextureTable.ContainsKey(fileName))	// �e�N�X�`���e�[�u�����̑��݃`�F�b�N
				{
					messages.Add("�X�L�b�v - " + fileName + " (�Ǎ��ς�)");
					continue;
				}

				if (!ArchiveFileManager.Exists(fileName))		// �A�[�J�C�u���̑��݃`�F�b�N
				{
					messages.Add("�X�L�b�v - " + fileName + " (�A�[�J�C�u���ɑ��݂��Ȃ�)");
					continue;
				}

				textures.Add(new Texture(this.Device, new MemoryStream(ArchiveFileManager.GetData(fileName)), Usage.None, Pool.Managed));
				messages.Add("����" + fileName);
			}

			e.Result = new object[] { textures, messages };
		}

		/// <summary>
		/// �����摜����̃e�N�X�`����������������Ǝ��s����A�㏈�����s���B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _TextureLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		#endregion


		#region �e�N�X�`���̓o�^/�폜

		/// <summary>
		/// �e�N�X�`���̓o�^���J�n����B
		/// </summary>
		/// <param name="isNowLoading">NowLoading ��ʂ�\������ꍇ�� true�A����ȊO�� false�B</param>
		/// <returns>�o�^�e�[�u���� index�B</returns>
		private int _BeginRegistTexture(bool isNowLoading)
		{
			// �����̓o�^�e�[�u�����ŋ�ɂȂ��Ă���ԍ�����������T��
			for (this.RegistNum = 0; this.RegistNum < RegistTable.Count; this.RegistNum++)
			{
				// ��ɂȂ��Ă���ԍ��𔭌������炻�ꂪ�g�p����o�^�ԍ�
				if (this.RegistTable[this.RegistNum] == null) break;
			}

			// ��ɂȂ��Ă���ԍ����Ȃ����(�o�^�ԍ����v�f���Ɠ����ɂȂ��Ă�����)�A�V�������X�g���쐬
			if (this.RegistNum == this.RegistTable.Count) this.RegistTable.Add(new List<string>());

			// �����̃��X�g�ŊJ���Ă���ԍ��𔭌�������A���̔ԍ����g��
			else this.RegistTable[this.RegistNum] = new List<string>();

			// NowLoading�w�肳��Ă���Ε`��
			if (isNowLoading) this._DrawNowLoadingDialog();

			// �f�o�b�O�p���b�Z�[�W
			DebugTools.ConsolOutputMessage("DrawManager -BeginRegistTexture", "�N���X�e�N�X�`���o�^ �ԍ�:" + this.RegistNum, true);

			// �o�^�ԍ�(�g�p����o�^�e�[�u���̃C���f�b�N�X)
			return this.RegistNum;
		}


		/// <summary>
		/// �e�N�X�`���̓o�^���I������B
		/// </summary>
		private void _EndRegistTexture()
		{
			this.RegistNum = -1;		// �o�^���t���O���~�낷
		}


		/// <summary>
		/// �N���X�Ŏg�p����e�N�X�`���Ƃ��̍��W�̓o�^���s���B
		/// </summary>
		/// <param name="keyName">�n�b�V���ɓo�^����L�[(�N���X��)�B</param>
		/// <param name="p">�e�N�X�`����\��������W (�ʒu���ς��e�N�X�`���̏ꍇ�͓K���ł���)�B</param>
		/// <param name="textureNames">�g�p����e�N�X�`���̖��O�B</param>
		private void _RegistTexture(String keyName, Point p, String[] textureNames)
		{
			Rectangle[] rs = new Rectangle[textureNames.Length];

			for (int i = 0; i < textureNames.Length; i++)
			{
				rs[i] = new Rectangle(p, TextureFileManager.GetRectangle(textureNames[i]).Size);
			}

			// TextureNameManager�Ƀe�N�X�`������o�^
			Muphic.Manager.TextureNameManager.Regist(keyName, textureNames);

			// PointManager�Ƀe�N�X�`���̍��W��o�^
			Muphic.Manager.RectangleManager.Regist(keyName, rs);

			// �o�^���t���O�������Ă���Γo�^�e�[�u���ɓo�^����ǉ�
			if (this.RegistNum >= 0) this.RegistTable[this.RegistNum].Add(keyName);
		}


		/// <summary>
		/// �N���X�Ŏg�p����e�N�X�`���Ƃ��̍��W�̍폜���s���B
		/// </summary>
		/// <param name="index">�o�^�e�[�u���̃C���f�b�N�X�ԍ��B</param>
		private void _DeleteTexture(int index)
		{
			foreach (string keyName in this.RegistTable[index])
			{
				this._DeleteTexture(keyName);
			}

			this.RegistTable[index] = null;
			DebugTools.ConsolOutputMessage("DrawManager -DeleteTexture", "�o�^�N���X�e�N�X�`���ꊇ�폜 �ԍ�:" + index, true);
		}

		/// <summary>
		/// �N���X�Ŏg�p����e�N�X�`���Ƃ��̍��W�̍폜���s���B
		/// </summary>
		/// <param name="keyName">�폜����L�[(�N���X��)�B</param>
		private void _DeleteTexture(string keyName)
		{
			if (keyName == null) return;

			TextureNameManager.Delete(keyName);
			RectangleManager.Delete(keyName);
		}

		#endregion


		#region �e�N�X�`���`��

		/// <summary>
		/// �X�v���C�g�̕`����s�� (�`����W�����擾)�B
		/// </summary>
		/// <param name="className">�`�悷��e�N�X�`���̃L�[ (�N���X����)�B</param>
		/// <param name="state">���̃N���X�̏�� (��Ԃɂ���ĕ`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="isCenter">location ������łȂ��e�N�X�`�������̍��W�ł���ꍇ�� true�A����ȊO�� false�B</param>
		/// <param name="scaling">�g��E�k���� (�g��E�k�����s��Ȃ��ꍇ�� 1.0)�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^ (�e�N�X�`���̐F�����̂܂܎g�p����ꍇ�� Color.White)�B</param>
		private void _DrawTexture(string className, int state, bool isCenter, float scaling, Color filter)
		{
			this._DrawTexture(className, state, isCenter, scaling, false, filter);
		}

		/// <summary>
		/// �X�v���C�g�̕`����s�� (�`����W�����擾)�B
		/// </summary>
		/// <param name="className">�`�悷��e�N�X�`���̃L�[ (�N���X����)�B</param>
		/// <param name="state">���̃N���X�̏�� (��Ԃɂ���ĕ`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="isCenter">location ������łȂ��e�N�X�`�������̍��W�ł���ꍇ�� true�A����ȊO�� false�B</param>
		/// <param name="scaling">�g��E�k���� (�g��E�k�����s��Ȃ��ꍇ�� 1.0)�B</param>
		/// <param name="isLotation90">�e�N�X�`���� 90 �x��]�����ĕ`�悷��ꍇ�� true�A����ȊO�� false�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^ (�e�N�X�`���̐F�����̂܂܎g�p����ꍇ�� Color.White)�B</param>
		private void _DrawTexture(string className, int state, bool isCenter, float scaling, bool isLotation90, Color filter)
		{
			// �N���X������`�悷��ۂ̍�����W�𓾂�
			Rectangle r = Muphic.Manager.RectangleManager.Get(className, state);

			// ���݃`�F�b�N
			if (r.X == -1)
			{
				// �L�[�����݂��Ȃ��ꍇ�A���̎|���R���\�[���ɏo�͂��ďI��
				Muphic.Tools.DebugTools.ConsolOutputError("DrawManager -DrawTexture", "�e�N�X�`���`�掸�s (�L�[: " + className + " �̍��W�͓o�^����Ă��Ȃ�) ");
				return;
			}

			// �L�[�����݂��`�悷��ۂ̍�����W������������A�����\�b�h(�I�[�o�[���[�h)���Ă�
			this._DrawTexture(className, r.Location, state, isCenter, scaling, isLotation90 ? (float)(Math.PI / 2) : 0.0F, filter);
		}

		/// <summary>
		/// �X�v���C�g�̕`����s�� (�`����W�w��)�B
		/// </summary>
		/// <param name="className">�`�悷��e�N�X�`���̃L�[ (�N���X����), �܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷����W�B</param>
		/// <param name="state">���̃N���X�̏�� (��Ԃɂ���ĕ`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="isCenter">location ������łȂ��e�N�X�`�������̍��W�ł���ꍇ�� true�A����ȊO�� false�B</param>
		/// <param name="scaling">�g��E�k���� (�g��E�k�����s��Ȃ��ꍇ�� 1.0)�B</param>
		/// <param name="isLotation90">�e�N�X�`���� 90 �x��]�����ĕ`�悷��ꍇ�� true�A����ȊO�� false�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^ (�e�N�X�`���̐F�����̂܂܎g�p����ꍇ�� Color.White)�B</param>
		private void _DrawTexture(string className, Point location, int state, bool isCenter, float scaling, bool isLotation90, Color filter)
		{
			this._DrawTexture(className, location, state, isCenter, scaling, isLotation90 ? (float)(Math.PI / 2) : 0.0F, filter);
		}

		/// <summary>
		/// �X�v���C�g�̕`����s�� (�`����W�w��)�B
		/// </summary>
		/// <param name="className">�`�悷��e�N�X�`���̃L�[ (�N���X����), �܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷����W�B</param>
		/// <param name="state">���̃N���X�̏�� (��Ԃɂ���ĕ`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="isCenter">location ������łȂ��e�N�X�`�������̍��W�ł���ꍇ�� true�A����ȊO�� false�B</param>
		/// <param name="scaling">�g��E�k���� (�g��E�k�����s��Ȃ��ꍇ�� 1.0)�B</param>
		/// <param name="lotationAngle">�e�N�X�`������]�����ĕ`�悷��ꍇ�͂��̉�]�p (���W�A���E��])�A��]�����Ȃ��ꍇ�� 0�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^ (�e�N�X�`���̐F�����̂܂܎g�p����ꍇ�� Color.White)�B</param>
		private void _DrawTexture(string className, Point location, int state, bool isCenter, float scaling, float lotationAngle, Color filter)
		{
			string TextureName = Muphic.Manager.TextureNameManager.Get(className, state);		// �N���X������e�N�X�`�����𓾂�

			if (string.IsNullOrEmpty(TextureName))							// �N���X������e�N�X�`����������ꂽ�����`�F�b�N����
			{
				if (Muphic.Manager.TextureFileManager.Exist(className))		// �����Ȃ������ꍇ,className���̂��e�N�X�`�����ɂȂ��Ă���\��������
				{
					TextureName = className;								// TextureFileManager�ɓo�^����Ă����,���̂܂܃e�N�X�`�����Ƃ���
				}
				else
				{
					Muphic.Tools.DebugTools.ConsolOutputError("DrawManager -DrawTexture", "�e�N�X�`���`�掸�s (�L�[: " + className + " �͖��o�^) ");
					return;		// �L�[�����݂��Ȃ��ꍇ�A���̎|���R���\�[���ɏo�͂��ďI��
				}
			}

			string fileName = Muphic.Manager.TextureFileManager.GetFilePath(TextureName);			// �e�N�X�`��������`��Ɏg�p����e�N�X�`���t�@�C�����𓾂�
			Rectangle srcRectangle = Muphic.Manager.TextureFileManager.GetRectangle(TextureName);	// �e�N�X�`��������`��Ɏg�p����e�N�X�`������`�̈�𓾂�

			if (String.IsNullOrEmpty(fileName))			// �e�N�X�`���t�@�C���̑��݃`�F�b�N
			{
				Muphic.Tools.DebugTools.ConsolOutputError("DrawManager -DrawTexture", "�e�N�X�`���`�掸�s (�e�N�X�`����: " + TextureName + " �͖��o�^");
				return;		// �e�N�X�`���t�@�C�������݂��Ȃ��ꍇ�A���̎|���R���\�[���ɏo�͂��ďI��
			}

			if (isCenter)	// �����w�肩�`�F�b�N
			{
				// �^����ꂽ�`����W�������̂��̂ł������ꍇ�A�e�N�X�`���T�C�Y��p���č�����W���Z�o
				location = Muphic.Tools.CommonTools.CenterToOnreft(location, srcRectangle.Size);
			}

			// == �X�v���C�g�`�� ==
			// ��P���� : �`�悷��e�N�X�`���̎w��    �t�@�C���p�X�����Ƀe�N�X�`���e�[�u�����̉��Ԗڂ̓����摜���g�p����̂�������
			// ��Q���� : �e�N�X�`���̓]����`�̎w��  �����摜���̕`�悷���`�̈���w��
			// ��R���� : �`���͈̔͂̎w��          �g��E�k�������w�肳��Ă���ꍇ�͂���ɉ�����`�T�C�Y��ύX / �w�肳��Ă��Ȃ��ꍇ�͓]����`�T�C�Y���w��
			// ��S���� : �e�N�X�`���̉�]���S�̎w��  �e�N�X�`������]���ĕ`�悷��ꍇ�A�e�N�X�`���̍�������]�̒��S���W�Ƃ��Ďw�� / �w�肳��Ă��Ȃ��ꍇ�͍�����w��
			// ��T���� : �e�N�X�`���̉�]�p�̎w��    �e�N�X�`������]���ĕ`�悷��ꍇ�A���̉�]�p���w��
			// ��U���� : �`���̍��W�̎w��          �]����`�̕`��ʒu�̍�����W���w��
			// ��V���� : �F�t�B���^�̎w��      
			this.Sprite.Draw2D(
				this.TextureTable[fileName],
				srcRectangle,
				scaling == 1.0 ? srcRectangle.Size : new SizeF(srcRectangle.Width * scaling, srcRectangle.Height * scaling),
				lotationAngle == 0.0F ? new PointF(0, 0) : new PointF(0, srcRectangle.Height),
				lotationAngle,
				location,
				filter
			);
		}

		#endregion


		#region ���C���`��

		/// <summary>
		/// ���C���̏��������s���B
		/// </summary>
		/// <param name="devide">�f�o�C�X�B</param>
		private void _InitializeLine(Device devide)
		{
			this.Line = new Line(devide);

			this.Line.Width = 1.0f;				// ���̑������w��
			this.Line.Antialias = false;		// �A���`�G�C���A�X�̎w�� (�������p�̐��݂̂Ȃ̂ŕK�v�Ȃ�) 
			this.Line.Pattern = -1;				// �_�`�̎w�� (�P�ɂ���Ɠ_�`�̂悤�ȕ`��ɂȂ�) 
			this.Line.PatternScale = 1.0f;		// �_�`�̊Ԋu���w�� (���) 
		}

		/// <summary>
		/// �w�肳�ꂽ��`�̃��C����`�悷��B
		/// </summary>
		/// <param name="lineArea">���C����`�悷���`�B</param>
		/// <param name="lineColor">���C���̐F�B</param>
		/// <param name="width">���C���̑����B</param>
		private void _DrawLine(Rectangle lineArea, Color lineColor, float width)
		{
			this.Line.Width = width;	// �w�肳�ꂽ���C���̕��ɐݒ�

			this.Sprite.End();			// �X�v���C�g�`�����U�I��
			this.Line.Begin();			// ���C���`��J�n

			// ��`�̒��_ (�l���̍��W) ��ݒ�
			Vector2[] positions = new Vector2[5];
			positions[0] = new Vector2(lineArea.Left, lineArea.Top);
			positions[1] = new Vector2(lineArea.Left, lineArea.Bottom);
			positions[2] = new Vector2(lineArea.Right, lineArea.Bottom);
			positions[3] = new Vector2(lineArea.Right, lineArea.Top);
			positions[4] = new Vector2(lineArea.Left, lineArea.Top);

			this.Line.Draw(positions, lineColor);			// ���C���`��

			this.Line.End();								// ���C���`��I��
			this.Sprite.Begin(SpriteFlags.AlphaBlend);		// �ĂуX�v���C�g�`��J�n
		}

		#endregion


		#region ���̑�

		/// <summary>
		/// �ݒ�t�@�C������l�𓾂�B
		/// </summary>
		/// <typeparam name="Type">�擾�������ݒ�l�̌^</typeparam>
		/// <param name="key">�ݒ薼�B</param>
		/// <returns>�ݒ�l�B</returns>
		private Type GetSettings<Type>(string key)
		{
			return CommonTools.GetSettings<Type>(key);
		}

		#endregion


		#region �O������Ăяo����郁�\�b�h�Q

		#region Init/Dispose

		/// <summary>
		/// �`��Ǘ��N���X�̐ÓI�C���X�^���X�����y�юg�p����`��f�o�C�X���̏��������s���B
		/// �C���X�^���X������ɂP�x�������s�ł��Ȃ��_�ɒ��ӁB
		/// </summary>
		public static bool Initialize(MainWindow mainScreen)
		{
			return Muphic.Manager.DrawManager.Instance._Initialize(mainScreen);
		}

		/// <summary>
		/// �`��Ǘ��N���X�Ŏg�p����Ă���f�o�C�X��e�N�X�`�����̃A���}�l�[�W���\�[�X���������B
		/// </summary>
		public static void Dispose()
		{
			Muphic.Manager.DrawManager.Instance._Dispose();
		}

		#endregion

		#region Device/Scene

		/// <summary>
		/// �f�o�C�X�̏�Ԃ̃`�F�b�N���s���B���X�g���Ă����ꍇ�̓f�o�C�X�̍Đ��������݂�B
		/// </summary>
		/// <returns>�����Ԃ܂��̓f�o�C�X���Đ��������ꍇ�� true�A����ȊO (�f�o�C�X���X�g��) �� false�B</returns>
		public static bool CheckDevice()
		{
			return Muphic.Manager.DrawManager.Instance._CheckDevice();
		}

		/// <summary>
		/// �\���ݒ�Ńt���X�N���[�����[�h���ݒ肳��Ă����ꍇ�A�t���X�N���[�����[�h�Ńf�o�C�X���Đ�������B�N���t�F�[�Y�I�����Ɏg�p����B
		/// </summary>
		/// <returns>����ɍĐ������ꂽ�ꍇ�A�܂��̓t���X�N���[�����[�h�ɐݒ肳��Ă��Ȃ������ꍇ�� true�A����ȊO�� false�B</returns>
		public static bool SetFullScreenMode()
		{
			return Muphic.Manager.DrawManager.Instance._SetFullScreenMode();
		}

		/// <summary>
		/// �E�B���h�E/�t���X�N���[�����[�h�̐؂�ւ����s���B
		/// </summary>
		/// <returns>����ɐ؂�ւ���ꂽ�ꍇ�� true�A����ȊO�� false�B</returns>
		public static bool ChangeWindowMode()
		{
			return Muphic.Manager.DrawManager.Instance._ChangeWindowMode();
		}


		/// <summary>
		/// ��ʂ𔒂ŃN���A���A�V�[���`����J�n���� (�f�o�C�X�̃V�[���`��J�n�ƃX�v���C�g�̕`��J�n) �B
		/// </summary>
		public static void BeginScene()
		{
			Muphic.Manager.DrawManager.BeginScene(Color.White);
		}

		/// <summary>
		/// �V�[���`����J�n���� (�f�o�C�X�̃V�[���`��J�n�ƃX�v���C�g�̕`��J�n) �B
		/// </summary>
		/// <param name="clearColor">�`��O�ɉ�ʂ��N���A����ꍇ���̐F���w�肵�� Color �^�A�N���A���Ȃ��ꍇ�� Color.Empty�B</param>
		public static void BeginScene(Color clearColor)
		{
			Muphic.Manager.DrawManager.Instance._BeginScene(clearColor);
			Muphic.Manager.DrawManager.Instance._BeginSprite();
		}

		/// <summary>
		/// �V�[���`����I������ (�X�v���C�g�̕`��I���ƃf�o�C�X�̃V�[���`��I��) �B
		/// </summary>
		public static void EndScene()
		{
			Muphic.Manager.DrawManager.Instance._EndSprite();
			Muphic.Manager.DrawManager.Instance._EndScene();
		}

		/// <summary>
		/// NowLoading �_�C�A���O��`�悷��B
		/// </summary>
		public static void DrawNowLoading()
		{
			Muphic.Manager.DrawManager.Instance._DrawNowLoadingDialog();
		}

		/// <summary>
		/// NowLoading �_�C�A���O�𐶐�����B
		/// </summary>
		public static void CreateNowLoaing()
		{
			Muphic.Manager.DrawManager.Instance._CreateNowLoadingDialog();
		}

		#endregion

		#region Font

		/// <summary>
		/// �y�f�o�b�O�p�z������̕`����s���B
		/// </summary>
		/// <param name="str">�`�悷�镶����B</param>
		/// <param name="xLocation">�J�nx���W�B</param>
		/// <param name="yLocation">�J�ny���W�B</param>
		/// <param name="color">������̐F�B</param>
		public static void DrawString(string str, int xLocation, int yLocation, Color color)
		{
			Muphic.Manager.DrawManager.Instance._DrawString(str, xLocation, yLocation, color);
		}

		/// <summary>
		/// �y�f�o�b�O�p�z������̕`����s��(�f�o�b�O���̂�)�B
		/// </summary>
		/// <param name="str">�`�悷�镶����B</param>
		/// <param name="xLocation">�J�nx���W�B</param>
		/// <param name="yLocation">�J�ny���W�B</param>
		public static void DrawString(string str, int xLocation, int yLocation)
		{
			Muphic.Manager.DrawManager.Instance._DrawString(str, xLocation, yLocation, Color.Black);
		}

		#endregion

		#region Load

		/// <summary>
		/// �����̓����摜��ǂݍ��ށB
		/// </summary>
		/// <param name="fileNames">�����摜�̃t�@�C���p�X�B</param>
		/// <returns>�S�Ă̓ǂݍ��݂ɐ��������ꍇ�� true�A����ȊO�� false�B</returns>
		public static bool LoadTextureFiles(string[] fileNames)
		{
			return Muphic.Manager.DrawManager.Instance._LoadTextureFiles(fileNames);
		}

		/// <summary>
		/// �����摜��ǂݍ��ށB
		/// </summary>
		/// <param name="fileName">�����摜�̃t�@�C���p�X�B</param>
		/// <returns>�ǂݍ��݂ɐ������������ꍇ�� true�A����ȊO�� false�B</returns>
		public static bool LoadTextureFile(string fileName)
		{
			return Muphic.Manager.DrawManager.Instance._LoadTextureFile(fileName);
		}

		/// <summary>
		/// �����摜���폜����B
		/// </summary>
		/// <param name="fileName">�����摜�̃t�@�C���p�X�B</param>
		/// <returns>�폜�ɐ������������ꍇ�� true�A����ȊO�� false�B</returns>
		public static bool UnLoadTextureFile(string fileName)
		{
			return Muphic.Manager.DrawManager.Instance._UnloadTextureFile(fileName);
		}

		/// <summary>
		/// �w�肵�������摜���ǂݍ��܂�Ă��邩�ǂ������m�F����B
		/// </summary>
		/// <param name="fileName">�m�F���铝���摜�̃p�X�B</param>
		/// <returns>�ǂݍ��܂�Ă����ꍇ�� treu�A����ȊO�� false�B</returns>
		public static bool ExistsTextureFile(string fileName)
		{
			return Muphic.Manager.DrawManager.Instance._ExistsTextureFile(fileName);
		}

		#endregion

		#region Regist

		/// <summary>
		/// �g�p����e�N�X�`���̓o�^���J�n����B�K�� EndRegist ���\�b�h�ŏI�������邱�ƁB
		/// </summary>
		/// <param name="isNowLoading">�o�^���� NowLoading ��ʂ�\������ꍇ�� true�A����ȊO�� false�B</param>
		/// <returns>�o�^�ԍ��B�������ۂ͂��̔ԍ����w�肵�� Delete ���\�b�h���Ăяo���ƁA�o�^�����N���X�������ꊇ�폜�ł���B</returns>
		public static int BeginRegist(bool isNowLoading)
		{
			return Muphic.Manager.DrawManager.Instance._BeginRegistTexture(isNowLoading);
		}

		/// <summary>
		/// �g�p����e�N�X�`���̓o�^���I������B
		/// </summary>
		public static void EndRegist()
		{
			Muphic.Manager.DrawManager.Instance._EndRegistTexture();
		}


		/// <summary>
		/// �g�p����e�N�X�`���Ƃ��̍��W��o�^����B
		/// </summary>
		/// <param name="keyName">�o�^����L�[�B</param>
		/// <param name="xLocation">�e�N�X�`����\������ۂ̍���x���W�B</param>
		/// <param name="yLocation">�e�N�X�`����\������ۂ̍���y���W�B</param>
		/// <param name="textureNames">�o�^����e�N�X�`�����B</param>
		public static void Regist(string keyName, int xLocation, int yLocation, params string[] textureNames)
		{
			Muphic.Manager.DrawManager.Instance._RegistTexture(keyName, new Point(xLocation, yLocation), textureNames);
		}

		/// <summary>
		/// �g�p����e�N�X�`���Ƃ��̍��W��o�^����B
		/// </summary>
		/// <param name="className">�o�^����L�[(�N���X��)�B</param>
		/// <param name="textureLocation">�e�N�X�`����\������ۂ̍��W�B</param>
		/// <param name="textureNames">�o�^����e�N�X�`�����B</param>
		public static void Regist(string className, Point textureLocation, params string[] textureNames)
		{
			Muphic.Manager.DrawManager.Instance._RegistTexture(className, textureLocation, textureNames);
		}


		/// <summary>
		/// �N���X�Ŏg�p����e�N�X�`���Ƃ��̍��W�̍폜���s���B
		/// </summary>
		/// <param name="index">BeginRegist ���\�b�h�߂�l�̓o�^�ԍ��B</param>
		public static void Delete(int index)
		{
			Muphic.Manager.DrawManager.Instance._DeleteTexture(index);
		}

		/// <summary>
		/// �N���X�Ŏg�p����e�N�X�`���Ƃ��̍��W�̍폜���s���B
		/// </summary>
		/// <param name="keyName">�폜����L�[(�N���X��)�B</param>
		public static void Delete(string keyName)
		{
			Muphic.Manager.DrawManager.Instance._DeleteTexture(keyName);
		}

		#endregion

		#region Draw

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�� Manager ���Ō��� / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		public static void Draw(String className)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, 0, false, 1.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�� Manager ���Ō��� / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void Draw(String className, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, 0, false, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�� Manager ���Ō��� / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		public static void Draw(String className, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, false, 1.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�� Manager ���Ō��� / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void Draw(String className, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, false, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�� Manager ���Ō��� / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="isCenter">�o�^���ꂽ�z�u���W���������W�ł���ꍇ�� true�A������W�ł���ꍇ�� false�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void Draw(String className, bool isCenter, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, isCenter, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�� Manager ���Ō��� / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		/// <param name="isLotation90">�e�N�X�`���� 90 �x�E��]�����ĕ`�悷��ꍇ�� true�A����ȊO�� false�B</param>
		public static void Draw(String className, int state, byte alpha, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, false, 1.0F, isLotation90, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��), �܂��̓e�N�X�`�����B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		public static void Draw(String keyName, int xLocation, int yLocation)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��), �܂��̓e�N�X�`�����B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void Draw(String keyName, int xLocation, int yLocation, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�w�� / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		public static void Draw(String className, int xLocation, int yLocation, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߖ��� / �g��E�k������ / 90 �x��]�w�� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="isLotation90">�e�N�X�`���� 90 �x�E��]�����ĕ`�悷��ꍇ�� true�A����ȊO�� false�B</param>
		public static void Draw(String className, int xLocation, int yLocation, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, false, 1.0F, true, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߖ��� / �g��E�k�����w�� / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		public static void Draw(String className, int xLocation, int yLocation, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, false, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / �g��E�k������ / ��]���� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void Draw(String className, int xLocation, int yLocation, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / �g��E�k�����w�� / ��]���� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void Draw(String className, int xLocation, int yLocation, float scaling, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, false, scaling, 0.0F, filter);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�w�� / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void Draw(String className, int xLocation, int yLocation, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / �g��E�k�����w�� / ��]�p�w�� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		/// <param name="lotationAngle">�e�N�X�`���̉�]�p (���W�A���E��])�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void Draw(String className, int xLocation, int yLocation, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, false, scaling, lotationAngle, filter);
		}


		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��),�܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		public static void Draw(String keyName, Point location)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߖ��� / �g��E�k�����w�� / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��),�܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		public static void Draw(String keyName, Point location, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, false, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��),�܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void Draw(String keyName, Point location, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�w�� / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		public static void Draw(String className, Point location, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߖ��� / �g��E�k������ / 90 �x��]�w�� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="isLotation90">�e�N�X�`���� 90 �x�E��]�����ĕ`�悷��ꍇ�� true�A����ȊO�� false�B</param>
		public static void Draw(String className, Point location, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, false, 1.0F, true, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / �g��E�k������ / ��]���� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void Draw(String className, Point location, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, false, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / �g��E�k�����w�� / ��]���� / �t�B���^�w��)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��), �܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void Draw(String keyName, Point location, float scaling, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, false, scaling, 0.0F, filter);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�w�� / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void Draw(String className, Point location, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / ���ߓx�w�� / �g��E�k�����w�� / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		public static void Draw(String className, Point location, byte alpha, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, false, scaling, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� / �g��E�k�����w�� / ��]�p�w�� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		/// <param name="lotationAngle">�e�N�X�`���̉�]�p (���W�A���E��])�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void Draw(String className, Point location, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, false, scaling, lotationAngle, filter);
		}

		#endregion

		#region DrawCenter

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�� Manager ���Ō��� (���S���W) / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		public static void DrawCenter(String className)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, 0, true, 1.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�� Manager ���Ō��� (���S���W) / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void DrawCenter(String className, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, 0, true, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�� Manager ���Ō��� (���S���W) / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		public static void DrawCenter(String className, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, true, 1.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�� Manager ���Ō��� (���S���W) / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void DrawCenter(String className, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, true, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�� Manager ���Ō��� (���S���W) / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		/// <param name="isLotation90">�e�N�X�`���� 90 �x�E��]�����ĕ`�悷��ꍇ�� true�A����ȊO�� false�B</param>
		public static void DrawCenter(String className, int state, byte alpha, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, state, true, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��), �܂��̓e�N�X�`�����B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		public static void DrawCenter(String keyName, int xLocation, int yLocation)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��), �܂��̓e�N�X�`�����B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void DrawCenter(String keyName, int xLocation, int yLocation, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�w�� (���S���W) / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, true, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߖ��� / �g��E�k������ / 90 �x��]�w�� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="isLotation90">�e�N�X�`���� 90 �x�E��]�����ĕ`�悷��ꍇ�� true�A����ȊO�� false�B</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, true, 1.0F, true, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߖ��� / �g��E�k�����w�� / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, true, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / �g��E�k������ / ��]���� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / �g��E�k�����w�� / ��]���� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, float scaling, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), 0, true, scaling, 0.0F, filter);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�w�� (���S���W) / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / �g��E�k�����w�� / ��]�p�w�� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="xLocation">�`�悷��e�N�X�`���̍��゘���W�B</param>
		/// <param name="yLocation">�`�悷��e�N�X�`���̍��゙���W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		/// <param name="lotationAngle">�e�N�X�`���̉�]�p (���W�A���E��])�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void DrawCenter(String className, int xLocation, int yLocation, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, new Point(xLocation, yLocation), state, true, scaling, lotationAngle, filter);
		}


		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��),�܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		public static void DrawCenter(String keyName, Point location)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, true, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߖ��� / �g��E�k�����w�� / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��),�܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		public static void DrawCenter(String keyName, Point location, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, true, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��),�܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void DrawCenter(String keyName, Point location, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�w�� (���S���W) / ���ߖ��� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		public static void DrawCenter(String className, Point location, int state)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, true, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߖ��� / �g��E�k������ / 90 �x��]�w�� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="isLotation90">�e�N�X�`���� 90 �x�E��]�����ĕ`�悷��ꍇ�� true�A����ȊO�� false�B</param>
		public static void DrawCenter(String className, Point location, bool isLotation90)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, true, 1.0F, true, Color.White);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / �g��E�k������ / ��]���� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void DrawCenter(String className, Point location, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, true, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / �g��E�k�����w�� / ��]���� / �t�B���^�w��)
		/// </summary>
		/// <param name="keyName">�`�悷��L�[ (�N���X��), �܂��̓e�N�X�`�����B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void DrawCenter(String keyName, Point location, float scaling, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(keyName, location, 0, true, scaling, 0.0F, filter);
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state �w�� / ���W�w�� (���S���W) / ���ߓx�w�� / �g��E�k������ / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		public static void DrawCenter(String className, Point location, int state, byte alpha)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / ���ߓx�w�� / �g��E�k�����w�� / ��]���� / �t�B���^����)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="alpha">�e�N�X�`���̓��ߓx (0�`255)�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		public static void DrawCenter(String className, Point location, byte alpha, float scaling)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, 0, true, scaling, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// �e�N�X�`����`�悷�� (state = 0 / ���W�w�� (���S���W) / �g��E�k�����w�� / ��]�p�w�� / �t�B���^�w��)
		/// </summary>
		/// <param name="className">�`�悷��L�[ (�N���X��)�B</param>
		/// <param name="location">�`�悷��e�N�X�`���̍�����W�B</param>
		/// <param name="state">���݂̃N���X�̏�� (����ɂ��`�悷��e�N�X�`�����ς��)�B</param>
		/// <param name="scaling">�g��E�k�����B</param>
		/// <param name="lotationAngle">�e�N�X�`���̉�]�p (���W�A���E��])�B</param>
		/// <param name="filter">�X�v���C�g�`��̍ۂɃe�N�X�`���Ɗ|�����킹��A���ߏ����܂ސF�t�B���^�B</param>
		public static void DrawCenter(String className, Point location, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.DrawManager.Instance._DrawTexture(className, location, state, true, scaling, lotationAngle, filter);
		}

		#endregion

		#region Line

		/// <summary>
		/// �w�肳�ꂽ��`����ŕ`�悷��B
		/// </summary>
		/// <param name="line">�`�悷����̋�`�B</param>
		public static void DrawLine(Rectangle line)
		{
			Muphic.Manager.DrawManager.Instance._DrawLine(line, Color.Red, 2.0F);
		}

		/// <summary>
		/// �w�肳�ꂽ��`�Ő���`�悷��B
		/// </summary>
		/// <param name="line">�`�悷����̋�`�B</param>
		/// <param name="lineColor">�`�悷����̐F�B</param>
		/// <param name="lineWidth">�`�悷����̑����B</param>
		public static void DrawLine(Rectangle line, Color lineColor, float lineWidth)
		{
			Muphic.Manager.DrawManager.Instance._DrawLine(line, lineColor, lineWidth);
		}

		#endregion

		#endregion

	}
}
