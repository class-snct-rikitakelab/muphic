using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace muphic
{
	#region Ver.1
	/*
	/// <summary>
	/// DrawManager �̊T�v�̐����ł��B
	/// </summary>
	public class DrawManager
	{
		private static DrawManager drawManager;
		private Hashtable TextureTable;
		private Device device;
		private Sprite sprite = null;

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			muphic.DrawManager.drawManager = this;
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.Windowed = true;								//�E�B���h�E���[�h�̐ݒ�(�E�B���h�E���[�h)
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)

	#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture[] texture = new Texture[FileName.Length];

			if(TextureTable.Contains(ClassName))
			{
				return;																		//���ɓo�^����Ă�����I��
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture[i] = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);	//�e�N�X�`���̃C���X�^���X��
				if(i == 0)
				{
					muphic.PointManager.Set(ClassName, p, bitmap.Size);						//���W�f�[�^�̓o�^(�ŏ��̉摜�̍��W����)
				}
			}

			TextureTable.Add(ClassName, texture);
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName);				//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!TextureTable.ContainsKey(ClassName))						//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			Texture[] texture = (Texture[])TextureTable[ClassName];
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			sprite.Draw2D(texture[state], center, 0, location, Color.FromArgb(255, 255, 255));
		}

		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		public void BeginDevice()
		{
			device.Clear(ClearFlags.Target, Color.White, 0, 0);				//��ʂ̃N���A
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
		}

		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void Begin()
		{
			muphic.DrawManager.drawManager.BeginDevice();
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}
	}*/
	#endregion

	#region Ver.2
	/*
	/// <summary>
	/// DrawManager version2 �n�b�V���e�[�u����2�ɂ��ē����e�N�X�`�����d���Ăяo�����Ȃ��悤�ɂ����B
	/// </summary>
	public class DrawManager
	{
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//�t�@�C�����ƃe�N�X�`�����֘A�t���Ă���
		private Hashtable FileNameTable;						//�N���X���C�ƃt�@�C�����̔z����֘A�t���Ă���
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//���̒��ɂ́A���ꂼ��`��҂��̕����񂽂����A(������Ax�Ay�AColor)�̏��ɓ����Ă���

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			FileNameTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// �t�H���g�f�[�^�̍\���̂��쐬
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();

			// �\���̂ɕK�v�ȃf�[�^���Z�b�g
			fd.Height = 24;
			fd.FaceName = "�l�r �S�V�b�N";
			try
			{
				// �t�H���g���쐬
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// ��O����
				MessageBox.Show("������`��G���[");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.Windowed = true;								//�E�B���h�E���[�h�̐ݒ�(�E�B���h�E���[�h)
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)

	#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(FileNameTable.Contains(ClassName))
			{
				return;																		//���ɓo�^����Ă�����I��
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//�e�N�X�`���̃C���X�^���X��
				rs[i] = new Rectangle(p, bitmap.Size);										//���W�����Ƃ�
				if(!TextureTable.Contains(FileName[i]))										//�e�N�X�`�������ɓo�^����Ă��Ȃ����
				{
					TextureTable.Add(FileName[i], texture);									//TextureTable�Ɋi�[
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//���W�f�[�^�̓o�^
			FileNameTable.Add(ClassName, FileName);											//FileNameTable�Ɋi�[
		}

		public void DeleteTexture(String ClassName)
		{
			String[] filename = (String[])FileNameTable[ClassName];
			FileNameTable.Remove(ClassName);												//�폜
			muphic.PointManager.Delete(ClassName);											//�Ή����Ă�����W���폜
			for(int i=0;i<filename.Length;i++)
			{
				if(!FileNameTable.Contains(filename[i]))									//�������ɓ����t�@�C�����Q�Ƃ��Ă���
				{																			//�N���X���Ȃ�������
					TextureTable.Remove(filename[i]);										//�Y������e�N�X�`�����폜
				}
			}
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!FileNameTable.ContainsKey(ClassName))						//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			String fname = (String)((String[])FileNameTable[ClassName])[state];//�N���X����state����Y������e�N�X�`���̃t�@�C�������擾
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
		}

		public void AddText(String str, int x, int y, Color color)
		{
			//�������sprite���I���Ă���`�悵�Ȃ��Ƃ����Ȃ��̂ŁAsprite���I��܂ňꎞTextList�ɂ��߂Ă���
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextList����̃f�[�^�̎��o��
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//������̕`��
			}
			TextList.Clear();												//���ߍ���ł�������������ׂč폜
		}

		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		public void BeginDevice()
		{
			device.Clear(ClearFlags.Target, Color.White, 0, 0);				//��ʂ̃N���A
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//�X�v���C�g���I�������̂ŁA�������`�悷
		}

		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void Begin()
		{
			muphic.DrawManager.drawManager.BeginDevice();
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// �������`�悷�郁�\�b�h
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}*/
	#endregion
	
	#region Ver.3
	/// <summary>
	/// DrawManager version2 �n�b�V���e�[�u����2�ɂ��ē����e�N�X�`�����d���Ăяo�����Ȃ��悤�ɂ����B
	/// DrawManager version3 DrawString�@�\��ǉ��B�ڍׂ�DrawText���\�b�h���Q�ƁB
	/// </summary>
	/*
	public class DrawManager
	{
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//�t�@�C�����ƃe�N�X�`�����֘A�t���Ă���
		private Hashtable FileNameTable;						//�N���X���C�ƃt�@�C�����̔z����֘A�t���Ă���
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//���̒��ɂ́A���ꂼ��`��҂��̕����񂽂����A(������Ax�Ay�AColor)�̏��ɓ����Ă���

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			FileNameTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// �t�H���g�f�[�^�̍\���̂��쐬
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();

			// �\���̂ɕK�v�ȃf�[�^���Z�b�g
			fd.Height = 24;
			fd.FaceName = "�l�r �S�V�b�N";
			try
			{
				// �t�H���g���쐬
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// ��O����
				MessageBox.Show("������`��G���[");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.Windowed = true;								//�E�B���h�E���[�h�̐ݒ�(�E�B���h�E���[�h)
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)

	#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(FileNameTable.Contains(ClassName))
			{
				return;																		//���ɓo�^����Ă�����I��
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//�e�N�X�`���̃C���X�^���X��
				rs[i] = new Rectangle(p, bitmap.Size);										//���W�����Ƃ�
				if(!TextureTable.Contains(FileName[i]))										//�e�N�X�`�������ɓo�^����Ă��Ȃ����
				{
					TextureTable.Add(FileName[i], texture);									//TextureTable�Ɋi�[
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//���W�f�[�^�̓o�^
			FileNameTable.Add(ClassName, FileName);											//FileNameTable�Ɋi�[
		}

		public void DeleteTexture(String ClassName)
		{
			String[] filename = (String[])FileNameTable[ClassName];
			FileNameTable.Remove(ClassName);												//�폜
			muphic.PointManager.Delete(ClassName);											//�Ή����Ă�����W���폜
			for(int i=0;i<filename.Length;i++)
			{
				if(!FileNameTable.Contains(filename[i]))									//�������ɓ����t�@�C�����Q�Ƃ��Ă���
				{																			//�N���X���Ȃ�������
					TextureTable.Remove(filename[i]);										//�Y������e�N�X�`�����폜
				}
			}
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!FileNameTable.ContainsKey(ClassName))						//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			String fname = (String)((String[])FileNameTable[ClassName])[state];//�N���X����state����Y������e�N�X�`���̃t�@�C�������擾
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������́A1�{�Œ�o�[�W����
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
			//�������͔{����ς��邱�Ƃ��ł���o�[�W����
			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width, ra.Height), center, 0, new Point(ra.X, ra.Y), Color.FromArgb(255, 255, 255));

		}

		public void AddText(String str, int x, int y, Color color)
		{
			//�������sprite���I���Ă���`�悵�Ȃ��Ƃ����Ȃ��̂ŁAsprite���I��܂ňꎞTextList�ɂ��߂Ă���
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextList����̃f�[�^�̎��o��
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//������̕`��
			}
			TextList.Clear();												//���ߍ���ł�������������ׂč폜
		}

		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		public void BeginDevice()
		{
			device.Clear(ClearFlags.Target, Color.White, 0, 0);				//��ʂ̃N���A
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//�X�v���C�g���I�������̂ŁA�������`�悷
		}

		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">�璷����</param>
		/// <param name="y">�璷����</param>
		/// <param name="FileName">�璷����</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void Begin()
		{
			muphic.DrawManager.drawManager.BeginDevice();
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// �������`�悷�郁�\�b�h
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}*/
	#endregion

	#region Ver.4
	/*
	/// <summary>
	/// DrawManager version2 �n�b�V���e�[�u����2�ɂ��ē����e�N�X�`�����d���Ăяo�����Ȃ��悤�ɂ����B
	/// DrawManager version3 DrawString�@�\��ǉ��B�ڍׂ�DrawText���\�b�h���Q�ƁB
	/// DrawManager version4 isWindow���[�h��ݒ肷�邱�Ƃɂ��E�B���h�E���[�h���t���X�N���[�����[�h����I���\
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//�E�B���h�E���[�h���ǂ���(false�Ńt���X�N���[�����[�h)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//�t�@�C�����ƃe�N�X�`�����֘A�t���Ă���
		private Hashtable FileNameTable;						//�N���X���C�ƃt�@�C�����̔z����֘A�t���Ă���
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//���̒��ɂ́A���ꂼ��`��҂��̕����񂽂����A(������Ax�Ay�AColor)�̏��ɓ����Ă���

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			FileNameTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// �t�H���g�f�[�^�̍\���̂��쐬
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();

			// �\���̂ɕK�v�ȃf�[�^���Z�b�g
			fd.Height = 24;
			fd.FaceName = "�l�r �S�V�b�N";
			try
			{
				// �t�H���g���쐬
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// ��O����
				MessageBox.Show("������`��G���[");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)
			if(isWindow)											//�E�B���h�E���[�h�̏ꍇ�̐ݒ�
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//�E�B���h�E���[�h�̐ݒ�(true�Ȃ̂ŃE�B���h�E���[�h)
			}
			else
			{														//�t���X�N���[�����[�h�̏ꍇ�̐ݒ�
				form.Size = new Size(1024, 768);
				pParameters.Windowed = false;							//�E�B���h�E���[�h�̐ݒ�(false�Ȃ̂Ńt���X�N���[�����[�h)
				pParameters.EnableAutoDepthStencil = true;				// �[�x�X�e���V���o�b�t�@�̐ݒ�
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// �����[�x�X�e���V�� �T�[�t�F�C�X�̃t�H�[�}�b�g�̐ݒ�

				// �g�p�ł���f�B�X�v���C���[�h���������A�ړI�̃��[�h��T��
				bool flag = false;

				// �f�B�v���C���[�h��񋓂��A�T�C�Y���u1024�~768�v����
				// ���t���b�V�����[�g���u60�v�̃��[�h��T��
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// �����Ɍ������Ύg�p����
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// �����������Ƃ������t���O�𗧂Ă�
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// �ړI�̃��[�h���Ȃ���΂��̂܂܏I��
					MessageBox.Show("�w�肵���f�B�v���C���[�h�͌�����܂���ł����B",
						"�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


	#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[c(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(FileNameTable.Contains(ClassName))
			{
				return;																		//���ɓo�^����Ă�����I��
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//�e�N�X�`���̃C���X�^���X��
				rs[i] = new Rectangle(p, bitmap.Size);										//���W�����Ƃ�
				if(!TextureTable.Contains(FileName[i]))										//�e�N�X�`�������ɓo�^����Ă��Ȃ����
				{
					TextureTable.Add(FileName[i], texture);									//TextureTable�Ɋi�[
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//���W�f�[�^�̓o�^
			FileNameTable.Add(ClassName, FileName);											//FileNameTable�Ɋi�[
		}

		/// <summary>
		/// �e�N�X�`�����f���[�g���邱�Ƃɂ���ă������̎c��c�ʂ𑝂₷���\�b�h
		/// </summary>
		/// <param name="ClassName">�����N���X�̖��O</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = (String[])FileNameTable[ClassName];
			FileNameTable.Remove(ClassName);												//�폜
			muphic.PointManager.Delete(ClassName);											//�Ή����Ă�����W���폜
			for(int i=0;i<filename.Length;i++)
			{
				if(!FileNameTable.Contains(filename[i]))									//�������ɓ����t�@�C�����Q�Ƃ��Ă���
				{																			//�N���X���Ȃ�������
					TextureTable.Remove(filename[i]);										//�Y������e�N�X�`�����폜
				}
			}
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!FileNameTable.ContainsKey(ClassName))						//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			String fname = (String)((String[])FileNameTable[ClassName])[state];//�N���X����state����Y������e�N�X�`���̃t�@�C�������擾
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������́A1�{�Œ�o�[�W����
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
			//�������͔{����ς��邱�Ƃ��ł���o�[�W����
			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width, ra.Height), center, 0, new Point(ra.X, ra.Y), Color.FromArgb(255, 255, 255));

		}

		public void AddText(String str, int x, int y, Color color)
		{
			//�������sprite���I���Ă���`�悵�Ȃ��Ƃ����Ȃ��̂ŁAsprite���I��܂ňꎞTextList�ɂ��߂Ă���
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextList����̃f�[�^�̎��o��
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//������̕`��
			}
			TextList.Clear();												//���ߍ���ł�������������ׂč폜
		}

		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		public void BeginDevice()
		{
			device.Clear(ClearFlags.Target, Color.White, 0, 0);				//��ʂ̃N���A
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//�X�v���C�g���I�������̂ŁA�������`�悷
		}

		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">�璷����</param>
		/// <param name="y">�璷����</param>
		/// <param name="FileName">�璷����</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void Begin()
		{
			muphic.DrawManager.drawManager.BeginDevice();
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// �������`�悷�郁�\�b�h
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}*/
	#endregion

	#region Ver.5
	/*
	/// <summary>
	/// DrawManager version2 �n�b�V���e�[�u����2�ɂ��ē����e�N�X�`�����d���Ăяo�����Ȃ��悤�ɂ����B
	/// DrawManager version3 DrawString�@�\��ǉ��B�ڍׂ�DrawText���\�b�h���Q�ƁB
	/// DrawManager version4 isWindow���[�h��ݒ肷�邱�Ƃɂ��E�B���h�E���[�h���t���X�N���[�����[�h����I���\
	/// DrawManager version5 BeginRegist���\�b�h���ĂԂƁANowLoading�̉�ʂ��\�������悤�ɂȂ�
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//�E�B���h�E���[�h���ǂ���(false�Ńt���X�N���[�����[�h)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//�t�@�C�����ƃe�N�X�`�����֘A�t���Ă���
		private Hashtable FileNameTable;						//�N���X���C�ƃt�@�C�����̔z����֘A�t���Ă���
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//���̒��ɂ́A���ꂼ��`��҂��̕����񂽂����A(������Ax�Ay�AColor)�̏��ɓ����Ă���

		private bool isNowLoading;								//NowLoading�̉�ʂ�\������ׂ����ǂ���
		private int NumRegistTextureMax;						//NowLoading�̉�ʂ̊Ԃɓo�^���ׂ��e�N�X�`����
		private int NumRegistTexture;							//���ݓǂݍ��񂾃e�N�X�`���̐�

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			FileNameTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// �t�H���g�f�[�^�̍\���̂��쐬
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();


			// �\���̂ɕK�v�ȃf�[�^���Z�b�g
			fd.Height = 25;
			//fd.FaceName = "�l�r �S�V�b�N";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// �t�H���g���쐬
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// ��O����
				MessageBox.Show("������`��G���[");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)
			if(isWindow)											//�E�B���h�E���[�h�̏ꍇ�̐ݒ�
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//�E�B���h�E���[�h�̐ݒ�(true�Ȃ̂ŃE�B���h�E���[�h)
			}
			else
			{														//�t���X�N���[�����[�h�̏ꍇ�̐ݒ�
				form.Size = new Size(1024, 768);
				pParameters.Windowed = false;							//�E�B���h�E���[�h�̐ݒ�(false�Ȃ̂Ńt���X�N���[�����[�h)
				pParameters.EnableAutoDepthStencil = true;				// �[�x�X�e���V���o�b�t�@�̐ݒ�
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// �����[�x�X�e���V�� �T�[�t�F�C�X�̃t�H�[�}�b�g�̐ݒ�

				// �g�p�ł���f�B�X�v���C���[�h���������A�ړI�̃��[�h��T��
				bool flag = false;

				// �f�B�v���C���[�h��񋓂��A�T�C�Y���u1024�~768�v����
				// ���t���b�V�����[�g���u60�v�̃��[�h��T��
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// �����Ɍ������Ύg�p����
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// �����������Ƃ������t���O�𗧂Ă�
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// �ړI�̃��[�h���Ȃ���΂��̂܂܏I��
					MessageBox.Show("�w�肵���f�B�v���C���[�h�͌�����܂���ł����B",
						"�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


	#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		/// <summary>
		/// ���ꂩ��e�N�X�`���̓o�^��Ƃ��n�߂邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁAEndRegistTexture���Ăяo���܂�NowLoading��ʂ��\�������
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				// �摜�𕪗�����K�v���������ɂ���
				DrawTexture("Nowloading_bak", 0, false);	// �w�i�Z����
				//DrawTexture("Nowloading", 0, false);
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}
		}

		/// <summary>
		/// NowLoading��ʂ�`�悷�郁�\�b�h
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("���[�h��..." + this.NumRegistTexture, 0, 100);
				return;
			}
			float percent = this.NumRegistTexture / (float)this.NumRegistTextureMax;		//���݂̓ǂݍ��ݏ󋵂�0�`1(�p�[�Z���g�̂悤�Ȃ���)�͈̔͂ɒ���������
			Rectangle BarRect = new Rectangle(269, 494, 484, 30);							//���ꂪ�o�[�̎l�p�`

			int NowBarWidth = (int)(BarRect.Width * percent);								//���݂̓ǂݍ��ݏ󋵂��o�[�̕��ɒ���������
			for(int i=0;i<NowBarWidth;i++)
			{
				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			}
		}

		/// <summary>
		/// ����Ńe�N�X�`���̓o�^��Ƃ��I���邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁANowLoading��ʂ̕\�����I����āA���ʂ̕`��ɐ؂�ւ��
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[c(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(FileNameTable.Contains(ClassName))
			{
				return;																		//���ɓo�^����Ă�����I��
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//�e�N�X�`���̃C���X�^���X��
				rs[i] = new Rectangle(p, bitmap.Size);										//���W�����Ƃ�
				if(!TextureTable.Contains(FileName[i]))										//�e�N�X�`�������ɓo�^����Ă��Ȃ����
				{
					TextureTable.Add(FileName[i], texture);									//TextureTable�Ɋi�[
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//���W�f�[�^�̓o�^
			FileNameTable.Add(ClassName, FileName);											//FileNameTable�Ɋi�[

			if(this.isNowLoading)															//�����ANowLoading��ʂ�`�悵�Ȃ��Ƃ����Ȃ��Ȃ�
			{
				this.NumRegistTexture += FileName.Length;
				if(this.NumRegistTextureMax == 0)
				{
					DrawManager.Begin(true);												//�e�X�g�p��NowLoading�̏ꍇ�͉�ʂ��N���A����
				}
				else
				{
					DrawManager.Begin(false);												//�{�ԗp��NowLoading�̏ꍇ�͉�ʂ��N���A���Ȃ�
				}
				this.DrawNowLoading();
				DrawManager.End();
			}
		}

		/// <summary>
		/// �e�N�X�`�����f���[�g���邱�Ƃɂ���ă������̎c��c�ʂ𑝂₷���\�b�h
		/// </summary>
		/// <param name="ClassName">�����N���X�̖��O</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = (String[])FileNameTable[ClassName];
			FileNameTable.Remove(ClassName);												//�폜
			muphic.PointManager.Delete(ClassName);											//�Ή����Ă�����W���폜
			for(int i=0;i<filename.Length;i++)
			{
				if(!FileNameTable.Contains(filename[i]))									//�������ɓ����t�@�C�����Q�Ƃ��Ă���
				{																			//�N���X���Ȃ�������
					TextureTable.Remove(filename[i]);										//�Y������e�N�X�`�����폜
				}
			}
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!FileNameTable.ContainsKey(ClassName))						//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			String fname = (String)((String[])FileNameTable[ClassName])[state];//�N���X����state����Y������e�N�X�`���̃t�@�C�������擾
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������́A1�{�Œ�o�[�W����
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
			//�������͔{����ς��邱�Ƃ��ł���o�[�W����
			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width, ra.Height), center, 0, new Point(ra.X, ra.Y), Color.FromArgb(255, 255, 255));

		}

		public void AddText(String str, int x, int y, Color color)
		{
			//�������sprite���I���Ă���`�悵�Ȃ��Ƃ����Ȃ��̂ŁAsprite���I��܂ňꎞTextList�ɂ��߂Ă���
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextList����̃f�[�^�̎��o��
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//������̕`��
			}
			TextList.Clear();												//���ߍ���ł�������������ׂč폜
		}

		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//��ʂ̃N���A
			}
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//�X�v���C�g���I�������̂ŁA�������`�悷
		}

		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ���ĉ�NowLoading��ʂ̕`�悪�J�n����(�e�N�X�`���ǂݍ��݂̑����𒲂ׂ�Ƃ��ɗL��)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�J�n����
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���I�����邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�I������
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">�璷����</param>
		/// <param name="y">�璷����</param>
		/// <param name="FileName">�璷����</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// �������`�悷�郁�\�b�h
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}
	*/
	#endregion
	
	#region Ver.6
	/*
	/// <summary>
	/// DrawManager version2 �n�b�V���e�[�u����2�ɂ��ē����e�N�X�`�����d���Ăяo�����Ȃ��悤�ɂ����B
	/// DrawManager version3 DrawString�@�\��ǉ��B�ڍׂ�DrawText���\�b�h���Q�ƁB
	/// DrawManager version4 isWindow���[�h��ݒ肷�邱�Ƃɂ��E�B���h�E���[�h���t���X�N���[�����[�h����I���\
	/// DrawManager version5 BeginRegist���\�b�h���ĂԂƁANowLoading�̉�ʂ��\�������悤�ɂȂ�
	/// DrawManager version6 FileNameTable�̊Ǘ���FileNameManager�ɔC����(����@�\�����̂���) ���ƁADeleteTexture�̖�蔭�o(���̃e�N�X�`�����g���Ă��邩�ǂ������ׂ邱�Ƃ��ł��Ȃ�)
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//�E�B���h�E���[�h���ǂ���(false�Ńt���X�N���[�����[�h)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//�t�@�C�����ƃe�N�X�`�����֘A�t���Ă���
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//���̒��ɂ́A���ꂼ��`��҂��̕����񂽂����A(������Ax�Ay�AColor)�̏��ɓ����Ă���

		private bool isNowLoading;								//NowLoading�̉�ʂ�\������ׂ����ǂ���
		private int NumRegistTextureMax;						//NowLoading�̉�ʂ̊Ԃɓo�^���ׂ��e�N�X�`����
		private int NumRegistTexture;							//���ݓǂݍ��񂾃e�N�X�`���̐�

		private static Rectangle BarRect = new Rectangle(268, 494, 482, 30);	// NowLoading �o�[�̎l�p�`
		private int BarWidth;
		
		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// �t�H���g�f�[�^�̍\���̂��쐬
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
			
			// �\���̂ɕK�v�ȃf�[�^���Z�b�g
			fd.Height = 24;
			//fd.FaceName = "�l�r �S�V�b�N";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// �t�H���g���쐬
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// ��O����
				MessageBox.Show("������`��G���[");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)
			if(isWindow)											//�E�B���h�E���[�h�̏ꍇ�̐ݒ�
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//�E�B���h�E���[�h�̐ݒ�(true�Ȃ̂ŃE�B���h�E���[�h)
			}
			else
			{														//�t���X�N���[�����[�h�̏ꍇ�̐ݒ�
				form.Size = new Size(1024, 768);
				pParameters.Windowed = false;							//�E�B���h�E���[�h�̐ݒ�(false�Ȃ̂Ńt���X�N���[�����[�h)
				pParameters.EnableAutoDepthStencil = true;				// �[�x�X�e���V���o�b�t�@�̐ݒ�
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// �����[�x�X�e���V�� �T�[�t�F�C�X�̃t�H�[�}�b�g�̐ݒ�

				// �g�p�ł���f�B�X�v���C���[�h���������A�ړI�̃��[�h��T��
				bool flag = false;

				// �f�B�v���C���[�h��񋓂��A�T�C�Y���u1024�~768�v����
				// ���t���b�V�����[�g���u60�v�̃��[�h��T��
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// �����Ɍ������Ύg�p����
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// �����������Ƃ������t���O�𗧂Ă�
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// �ړI�̃��[�h���Ȃ���΂��̂܂܏I��
					MessageBox.Show("�w�肵���f�B�v���C���[�h�͌�����܂���ł����B",
						"�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


	#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		/// <summary>
		/// ���ꂩ��e�N�X�`���̓o�^��Ƃ��n�߂邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁAEndRegistTexture���Ăяo���܂�NowLoading��ʂ��\�������
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			this.BarWidth = 0;
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				DrawTexture("Nowloading_bak", 0, false);	// �w�i�Z����
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}
		}
		
		/// <summary>
		/// NowLoading��ʂ�`�悷�郁�\�b�h
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("���[�h��..." + this.NumRegistTexture, 0, 100);
				return;
			}
			
//			float percent = this.NumRegistTexture / (float)this.NumRegistTextureMax;		//���݂̓ǂݍ��ݏ󋵂�0�`1(�p�[�Z���g�̂悤�Ȃ���)�͈̔͂ɒ���������
//			Rectangle BarRect = new Rectangle(268, 494, 482, 30);							//���ꂪ�o�[�̎l�p�`
//
//			int NowBarWidth = (int)(BarRect.Width * percent);								//���݂̓ǂݍ��ݏ󋵂��o�[�̕��ɒ���������
//			for(int i=0;i<NowBarWidth;i++)
//			{
//				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
//			}
			
			int NowBarWidth = (int)(BarRect.Width * (this.NumRegistTexture / (float)this.NumRegistTextureMax));
			
			for(int i=this.BarWidth;i<NowBarWidth;i++)
			{
				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			}
			
			this.BarWidth = NowBarWidth;
		}

		/// <summary>
		/// ����Ńe�N�X�`���̓o�^��Ƃ��I���邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁANowLoading��ʂ̕\�����I����āA���ʂ̕`��ɐ؂�ւ��
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[c(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(muphic.FileNameManager.Regist(ClassName, FileName) == false)					//FileNameManager�Ƀt�@�C������o�^
			{
				//return;																		//���ɓo�^����Ă�����I�����Ȃ�(PrintManager��
			}																				//FileNameManager�ɓo�^�����\�����c����Ă��邽��)
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//�e�N�X�`���̃C���X�^���X��
				rs[i] = new Rectangle(p, bitmap.Size);										//���W�����Ƃ�
				if(!TextureTable.Contains(FileName[i]))										//�e�N�X�`�������ɓo�^����Ă��Ȃ����
				{
					TextureTable.Add(FileName[i], texture);									//TextureTable�Ɋi�[
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//���W�f�[�^�̓o�^

			if(this.isNowLoading)															//�����ANowLoading��ʂ�`�悵�Ȃ��Ƃ����Ȃ��Ȃ�
			{
				this.NumRegistTexture += FileName.Length;
				if(this.NumRegistTextureMax == 0)
				{
					DrawManager.Begin(true);												//�e�X�g�p��NowLoading�̏ꍇ�͉�ʂ��N���A����
				}
				else
				{
					DrawManager.Begin(false);												//�{�ԗp��NowLoading�̏ꍇ�͉�ʂ��N���A���Ȃ�
				}
				this.DrawNowLoading();
				DrawManager.End();
			}
		}

		/// <summary>
		/// �e�N�X�`�����f���[�g���邱�Ƃɂ���ă������̎c��c�ʂ𑝂₷���\�b�h
		/// </summary>
		/// <param name="ClassName">�����N���X�̖��O</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = muphic.FileNameManager.GetFileNames(ClassName);
			muphic.FileNameManager.Delete(ClassName);										//�폜
			muphic.PointManager.Delete(ClassName);											//�Ή����Ă�����W���폜
			for(int i=0;i<filename.Length;i++)
			{
//				if(!FileNameTable.Contains(filename[i]))									//�������ɓ����t�@�C�����Q�Ƃ��Ă���
//				{																			//�N���X���Ȃ�������
//					TextureTable.Remove(filename[i]);										//�Y������e�N�X�`�����폜
//				}										//��������������
			}
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
			Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������́A1�{�Œ�o�[�W����
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
			//�������͔{����ς��邱�Ƃ��ł���o�[�W����
			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width/2, ra.Height/2), center, 0, location, Color.FromArgb(255, 255, 255));
		}
		
		public void AddText(String str, int x, int y, Color color)
		{
			//�������sprite���I���Ă���`�悵�Ȃ��Ƃ����Ȃ��̂ŁAsprite���I��܂ňꎞTextList�ɂ��߂Ă���
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextList����̃f�[�^�̎��o��
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//������̕`��
			}
			TextList.Clear();												//���ߍ���ł�������������ׂč폜
		}

		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//��ʂ̃N���A
			}
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//�X�v���C�g���I�������̂ŁA�������`�悷
		}

		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ���ĉ�NowLoading��ʂ̕`�悪�J�n����(�e�N�X�`���ǂݍ��݂̑����𒲂ׂ�Ƃ��ɗL��)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�J�n����
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���I�����邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�I������
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">�璷����</param>
		/// <param name="y">�璷����</param>
		/// <param name="FileName">�璷����</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// �������`�悷�郁�\�b�h
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}*/
	#endregion

	#region Ver.7.1
	/*
	/// <summary>
	/// DrawManager version2 �n�b�V���e�[�u����2�ɂ��ē����e�N�X�`�����d���Ăяo�����Ȃ��悤�ɂ����B
	/// DrawManager version3 DrawString�@�\��ǉ��B�ڍׂ�DrawText���\�b�h���Q�ƁB
	/// DrawManager version4 isWindow���[�h��ݒ肷�邱�Ƃɂ��E�B���h�E���[�h���t���X�N���[�����[�h����I���\
	/// DrawManager version5 BeginRegist���\�b�h���ĂԂƁANowLoading�̉�ʂ��\�������悤�ɂȂ�
	/// DrawManager version6 FileNameTable�̊Ǘ���FileNameManager�ɔC����(����@�\�����̂���) ���ƁADeleteTexture�̖�蔭�o(���̃e�N�X�`�����g���Ă��邩�ǂ������ׂ邱�Ƃ��ł��Ȃ�)[
	/// DrawManager version7 ���ꉹ�y�Ŏg���T���l�C���@�\(�g��E�k���\���@�\)�ǉ�
	/// DrawManager version7.1 �t���X�N���[�����[�h�̂Ƃ��A�^�C�g���o�[�������悤�ɒ�����
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//�E�B���h�E���[�h���ǂ���(false�Ńt���X�N���[�����[�h)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//�t�@�C�����ƃe�N�X�`�����֘A�t���Ă���
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//���̒��ɂ́A���ꂼ��`��҂��̕����񂽂����A(������Ax�Ay�AColor)�̏��ɓ����Ă���

		private bool isNowLoading;								//NowLoading�̉�ʂ�\������ׂ����ǂ���
		private int NumRegistTextureMax;						//NowLoading�̉�ʂ̊Ԃɓo�^���ׂ��e�N�X�`����
		private int NumRegistTexture;							//���ݓǂݍ��񂾃e�N�X�`���̐�

		private static Rectangle BarRect = new Rectangle(268, 494, 482, 30);	// NowLoading �o�[�̎l�p�`
		private int BarWidth;

		//�g��E�k���֌W�ϐ�
		Rectangle src;											//���z�E�B���h�E�̎l�p�`
		Rectangle dest;											//���ۂɃE�B���h�E�ɕ`�悷��ۂ̎l�p�`
		
		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// �t�H���g�f�[�^�̍\���̂��쐬
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
			
			// �\���̂ɕK�v�ȃf�[�^���Z�b�g
			fd.Height = 24;
			//fd.FaceName = "�l�r �S�V�b�N";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// �t�H���g���쐬
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// ��O����
				MessageBox.Show("������`��G���[");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)
			if(isWindow)											//�E�B���h�E���[�h�̏ꍇ�̐ݒ�
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//�E�B���h�E���[�h�̐ݒ�(true�Ȃ̂ŃE�B���h�E���[�h)
			}
			else
			{														//�t���X�N���[�����[�h�̏ꍇ�̐ݒ�
				form.Size = new Size(1024, 768);
				form.ControlBox = false;							//�^�C�g���o�[����
				form.Text = "";										//�^�C�g���o�[�����������C����������ł���Ȃ�Microsoft
				pParameters.Windowed = false;							//�E�B���h�E���[�h�̐ݒ�(false�Ȃ̂Ńt���X�N���[�����[�h)
				pParameters.EnableAutoDepthStencil = true;				// �[�x�X�e���V���o�b�t�@�̐ݒ�
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// �����[�x�X�e���V�� �T�[�t�F�C�X�̃t�H�[�}�b�g�̐ݒ�

				// �g�p�ł���f�B�X�v���C���[�h���������A�ړI�̃��[�h��T��
				bool flag = false;

				// �f�B�v���C���[�h��񋓂��A�T�C�Y���u1024�~768�v����
				// ���t���b�V�����[�g���u60�v�̃��[�h��T��
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// �����Ɍ������Ύg�p����
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// �����������Ƃ������t���O�𗧂Ă�
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// �ړI�̃��[�h���Ȃ���΂��̂܂܏I��
					MessageBox.Show("�w�肵���f�B�v���C���[�h�͌�����܂���ł����B",
						"�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


			#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
			#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		/// <summary>
		/// ���ꂩ��e�N�X�`���̓o�^��Ƃ��n�߂邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁAEndRegistTexture���Ăяo���܂�NowLoading��ʂ��\�������
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			this.BarWidth = 0;
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				DrawTexture("Nowloading_bak", 0, false);	// �w�i�Z����
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}
		}
		
		/// <summary>
		/// NowLoading��ʂ�`�悷�郁�\�b�h
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("���[�h��..." + this.NumRegistTexture, 0, 100);
				return;
			}
			
			//			float percent = this.NumRegistTexture / (float)this.NumRegistTextureMax;		//���݂̓ǂݍ��ݏ󋵂�0�`1(�p�[�Z���g�̂悤�Ȃ���)�͈̔͂ɒ���������
			//			Rectangle BarRect = new Rectangle(268, 494, 482, 30);							//���ꂪ�o�[�̎l�p�`
			//
			//			int NowBarWidth = (int)(BarRect.Width * percent);								//���݂̓ǂݍ��ݏ󋵂��o�[�̕��ɒ���������
			//			for(int i=0;i<NowBarWidth;i++)
			//			{
			//				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			//			}
			
			int NowBarWidth = (int)(BarRect.Width * (this.NumRegistTexture / (float)this.NumRegistTextureMax));
			
			for(int i=this.BarWidth;i<NowBarWidth;i++)
			{
				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			}
			
			this.BarWidth = NowBarWidth;
		}

		/// <summary>
		/// ����Ńe�N�X�`���̓o�^��Ƃ��I���邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁANowLoading��ʂ̕\�����I����āA���ʂ̕`��ɐ؂�ւ��
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[c(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(muphic.FileNameManager.Regist(ClassName, FileName) == false)					//FileNameManager�Ƀt�@�C������o�^
			{
				//return;																		//���ɓo�^����Ă�����I�����Ȃ�(PrintManager��
			}																				//FileNameManager�ɓo�^�����\�����c����Ă��邽��)
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//�e�N�X�`���̃C���X�^���X��
				rs[i] = new Rectangle(p, bitmap.Size);										//���W�����Ƃ�
				if(!TextureTable.Contains(FileName[i]))										//�e�N�X�`�������ɓo�^����Ă��Ȃ����
				{
					TextureTable.Add(FileName[i], texture);									//TextureTable�Ɋi�[
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//���W�f�[�^�̓o�^

			if(this.isNowLoading)															//�����ANowLoading��ʂ�`�悵�Ȃ��Ƃ����Ȃ��Ȃ�
			{
				this.NumRegistTexture += FileName.Length;
				if(this.NumRegistTextureMax == 0)
				{
					DrawManager.Begin(true);												//�e�X�g�p��NowLoading�̏ꍇ�͉�ʂ��N���A����
				}
				else
				{
					DrawManager.Begin(false);												//�{�ԗp��NowLoading�̏ꍇ�͉�ʂ��N���A���Ȃ�
				}
				this.DrawNowLoading();
				DrawManager.End();
			}
		}

		/// <summary>
		/// �e�N�X�`�����f���[�g���邱�Ƃɂ���ă������̎c��c�ʂ𑝂₷���\�b�h
		/// </summary>
		/// <param name="ClassName">�����N���X�̖��O</param>
		public void DeleteTexture(String ClassName)
		{
//			String[] filename = muphic.FileNameManager.GetFileNames(ClassName);
//			muphic.FileNameManager.Delete(ClassName);										//�폜
//			muphic.PointManager.Delete(ClassName);											//�Ή����Ă�����W���폜
//			for(int i=0;i<filename.Length;i++)
//			{
//				if(!FileNameTable.Contains(filename[i]))									//�������ɓ����t�@�C�����Q�Ƃ��Ă���
//				{																			//�N���X���Ȃ�������
//					TextureTable.Remove(filename[i]);										//�Y������e�N�X�`�����폜
//				}									//��������������
//			}
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������́A1�{�Œ�o�[�W����
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
		}

		///////////////////////////////////////////////////////////////////////
		//�g��E�k���`��֌W
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �g��E�k���̍ہA���ɗp����f�B�X�v���C�ƁA���ۂɕ`�悷��̈��ύX����
		/// </summary>
		/// <param name="srcRect">���W���w�肷��ۂ̕`��̈�(�e����Ō���Window�̗̈�)</param>
		/// <param name="destRect">���ۂɕ`�悷��̈�(�e����Ō����T���l�C���̈�)</param>
		public void ChangeWindowSize(Rectangle srcRect, Rectangle destRect)
		{
			this.src = srcRect;
			this.dest = destRect;
		}

		/// <summary>
		/// �g��E�k���������{�����e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="state"></param>
		/// <param name="isCenter"></param>
		public void DrawDivTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawDivTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// �g��E�k���������{�����e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�N���X�̃L�[</param>
		/// <param name="location">src���ɂ�������W(�g��E�k�������O���W)</param>
		/// <param name="state">���</param>
		/// <param name="isCenter">�^�񒆂ɂ��邩�ǂ���</param>
		public void DrawDivTexture(String ClassName, Point location, int state, bool isCenter)
		{
			float divX = (float)dest.Width / src.Width;
			float divY = (float)dest.Height / src.Height;
			PointF newLocation = new Point(0, 0);
			newLocation.X = location.X - src.X;
			newLocation.X = newLocation.X * divX;
			newLocation.X = newLocation.X + dest.X;
			newLocation.Y = location.Y - src.Y;
			newLocation.Y = newLocation.Y * divY;
			newLocation.Y = newLocation.Y + dest.Y;
			DrawTexture(ClassName, divX, divY, newLocation, state, isCenter);
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="divX">�`�悷����̂̉��̔{��</param>
		/// <param name="divY">�`�悷����̂̏c�̔{��</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		private void DrawTexture(String ClassName, float divX, float divY, PointF location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			PointF center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width * divX / 2;
				center.Y = r.Height * divX / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������͔{����ς��邱�Ƃ��ł���o�[�W����
			Rectangle ra = muphic.PointManager.Get(ClassName, state);
			sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new SizeF(ra.Width*divX, ra.Height*divY), center, 0, location, Color.FromArgb(255, 255, 255));
		}
		
		public void AddText(String str, int x, int y, Color color)
		{
			//�������sprite���I���Ă���`�悵�Ȃ��Ƃ����Ȃ��̂ŁAsprite���I��܂ňꎞTextList�ɂ��߂Ă���
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextList����̃f�[�^�̎��o��
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//������̕`��
			}
			TextList.Clear();												//���ߍ���ł�������������ׂč폜
		}

		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//��ʂ̃N���A
			}
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//�X�v���C�g���I�������̂ŁA�������`�悷
		}

		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ���ĉ�NowLoading��ʂ̕`�悪�J�n����(�e�N�X�`���ǂݍ��݂̑����𒲂ׂ�Ƃ��ɗL��)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�J�n����
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���I�����邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�I������
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		#region Regist

		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		#endregion

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">�璷����</param>
		/// <param name="y">�璷����</param>
		/// <param name="FileName">�璷����</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		#region Draw

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		#region DrawDiv

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawDiv(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDiv(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawDiv(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDiv(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawDivCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDivCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawDivCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDivCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		/// <summary>
		/// �g��E�k���̍ۂ̔{�������肷�郁�\�b�h
		/// </summary>
		/// <param name="srcRect">�`�悷����W��o�^���鉼�z�E�B���h�E�̎l�p�`(�o�^�l�p�`)</param>
		/// <param name="destRect">���ۂɃE�B���h�E�ɕ`�悷��ۂɕ`�悳���l�p�`(�`��l�p�`)</param>
		static public void Change(Rectangle srcRect, Rectangle destRect)
		{
			muphic.DrawManager.drawManager.ChangeWindowSize(srcRect, destRect);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// �������`�悷�郁�\�b�h
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}
	*/
	#endregion

	#region Ver.8.2
	/*	
	public delegate void FadeEventHandler();
	/// <summary>
	/// DrawManager version2 �n�b�V���e�[�u����2�ɂ��ē����e�N�X�`�����d���Ăяo�����Ȃ��悤�ɂ����B
	/// DrawManager version3 DrawString�@�\��ǉ��B�ڍׂ�DrawText���\�b�h���Q�ƁB
	/// DrawManager version4 isWindow���[�h��ݒ肷�邱�Ƃɂ��E�B���h�E���[�h���t���X�N���[�����[�h����I���\
	/// DrawManager version5 BeginRegist���\�b�h���ĂԂƁANowLoading�̉�ʂ��\�������悤�ɂȂ�
	/// DrawManager version6 FileNameTable�̊Ǘ���FileNameManager�ɔC����(����@�\�����̂���) ���ƁADeleteTexture�̖�蔭�o(���̃e�N�X�`�����g���Ă��邩�ǂ������ׂ邱�Ƃ��ł��Ȃ�)[
	/// DrawManager version7 ���ꉹ�y�Ŏg���T���l�C���@�\(�g��E�k���\���@�\)�ǉ�
	/// DrawManager version7.1 �t���X�N���[�����[�h�̂Ƃ��A�^�C�g���o�[�������悤�ɒ�����
	/// DrawManager version8 �t�F�[�h�C���A�t�F�[�h�A�E�g�@�\��ǉ�
	/// DrawManager version8.1 �}�E�X�J�[�\����NowLoading���ɕ\������Ă���_���C��
	/// DrawManager version8.2 �k���`��̍ۂ̖����C��(DrawDiv��Draw2D���\�b�h���ύX����Ă���)
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//�E�B���h�E���[�h���ǂ���(false�Ńt���X�N���[�����[�h)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//�t�@�C�����ƃe�N�X�`�����֘A�t���Ă���
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//���̒��ɂ́A���ꂼ��`��҂��̕����񂽂����A(������Ax�Ay�AColor)�̏��ɓ����Ă���

		private bool isNowLoading;								//NowLoading�̉�ʂ�\������ׂ����ǂ���
		private int NumRegistTextureMax;						//NowLoading�̉�ʂ̊Ԃɓo�^���ׂ��e�N�X�`����
		private int NumRegistTexture;							//���ݓǂݍ��񂾃e�N�X�`���̐�

		private static Rectangle BarRect = new Rectangle(268, 494, 482, 30);	// NowLoading �o�[�̎l�p�`
		private int BarWidth;

		//�g��E�k���֌W�ϐ�
		Rectangle src;											//���z�E�B���h�E�̎l�p�`
		Rectangle dest;											//���ۂɃE�B���h�E�ɕ`�悷��ۂ̎l�p�`

		//�t�F�[�h�C���E�t�F�[�h�A�E�g�p�ϐ�
		int alpha = 255;										//���ߓx������킷�ϐ�(255�ŕ��ʂɕ`��)
		int fade;												//�t�F�[�h�x������킷�ϐ�(255�ŕ��ʂɕ`��)
		Color FadeColor = Color.Black;							//�t�F�[�h�C���̎n�߁A�t�F�[�h�A�E�g�̍Ō�̐F
		Color FadeColor2 = Color.White;							//�t�F�[�h�C���̍Ō�A�t�F�[�h�A�E�g�̎n�߂̐F
		Color FilterColor = Color.White;						//���݃e�N�X�`����`�悷��ۂɗp����t�B���^�J���[
		public static event FadeEventHandler FadeInEvent;		//�t�F�[�h�C���̍ۂɌĂ�Draw���\�b�h��o�^����C�x���g
		public static event FadeEventHandler FadeOutEvent;		//�t�F�[�h�A�E�g�̍ۂɌĂ�Draw���\�b�h��o�^����C�x���g
		Timer FadeTimer;										//�t�F�[�h�C���E�t�F�[�h�A�E�g�̍ۂ̃t���[���J�E���^�[
		int NowFadeFrameCount;									//�t�F�[�h�����̍ۂ̌��݂̃t���[����
		int FadeFrameCount;										//�t�F�[�h����������t���[����

		//�}�E�X�J�[�\���̕`��֌W
		String MouseClassName;
		Point MousePoint;
		int MouseState;
		bool isDrawMouseCursor = true;

		public DrawManager(MainScreen form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// �t�H���g�f�[�^�̍\���̂��쐬
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
			
			// �\���̂ɕK�v�ȃf�[�^���Z�b�g
			fd.Height = 24;
			//fd.FaceName = "�l�r �S�V�b�N";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// �t�H���g���쐬
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// ��O����
				MessageBox.Show("������`��G���[");
				return;
			}

			//FadeTimer = new Timer(new System.ComponentModel.Container());
			//FadeTimer.Interval = 16;
			FadeTimer = form.FadeTimer;
		}

		private void InitDevice(MainScreen form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)
			if(isWindow)											//�E�B���h�E���[�h�̏ꍇ�̐ݒ�
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//�E�B���h�E���[�h�̐ݒ�(true�Ȃ̂ŃE�B���h�E���[�h)
			}
			else
			{														//�t���X�N���[�����[�h�̏ꍇ�̐ݒ�
				form.Size = new Size(1024, 768);
				form.ControlBox = false;							//�^�C�g���o�[����
				form.Text = "";										//�^�C�g���o�[�����������C����������ł���Ȃ�Microsoft
				pParameters.Windowed = false;							//�E�B���h�E���[�h�̐ݒ�(false�Ȃ̂Ńt���X�N���[�����[�h)
				pParameters.EnableAutoDepthStencil = true;				// �[�x�X�e���V���o�b�t�@�̐ݒ�
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// �����[�x�X�e���V�� �T�[�t�F�C�X�̃t�H�[�}�b�g�̐ݒ�

				// �g�p�ł���f�B�X�v���C���[�h���������A�ړI�̃��[�h��T��
				bool flag = false;

				// �f�B�v���C���[�h��񋓂��A�T�C�Y���u1024�~768�v����
				// ���t���b�V�����[�g���u60�v�̃��[�h��T��
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// �����Ɍ������Ύg�p����
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// �����������Ƃ������t���O�𗧂Ă�
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// �ړI�̃��[�h���Ȃ���΂��̂܂܏I��
					MessageBox.Show("�w�肵���f�B�v���C���[�h�͌�����܂���ł����B",
						"�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


			#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
			#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		#region �e�N�X�`���o�^�ENowLoading�֌W
		///////////////////////////////////////////////////////////////////////
		//�e�N�X�`���o�^�A�폜�ENowLoading�֌W
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// ���ꂩ��e�N�X�`���̓o�^��Ƃ��n�߂邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁAEndRegistTexture���Ăяo���܂�NowLoading��ʂ��\�������
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			this.BarWidth = 0;
			this.isDrawMouseCursor = false;					//NowLoading���̓}�E�X�J�[�\����\�����Ȃ�
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				DrawTexture("Nowloading_bak", 0, false);	// �w�i�Z����
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}
		}
		
		/// <summary>
		/// NowLoading��ʂ�`�悷�郁�\�b�h
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("���[�h��..." + this.NumRegistTexture, 0, 100);
				return;
			}
			
			//			float percent = this.NumRegistTexture / (float)this.NumRegistTextureMax;		//���݂̓ǂݍ��ݏ󋵂�0�`1(�p�[�Z���g�̂悤�Ȃ���)�͈̔͂ɒ���������
			//			Rectangle BarRect = new Rectangle(268, 494, 482, 30);							//���ꂪ�o�[�̎l�p�`
			//
			//			int NowBarWidth = (int)(BarRect.Width * percent);								//���݂̓ǂݍ��ݏ󋵂��o�[�̕��ɒ���������
			//			for(int i=0;i<NowBarWidth;i++)
			//			{
			//				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			//			}
			
			int NowBarWidth = (int)(BarRect.Width * (this.NumRegistTexture / (float)this.NumRegistTextureMax));
			
			for(int i=this.BarWidth;i<NowBarWidth;i++)
			{
				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			}
			
			this.BarWidth = NowBarWidth;
		}

		/// <summary>
		/// ����Ńe�N�X�`���̓o�^��Ƃ��I���邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁANowLoading��ʂ̕\�����I����āA���ʂ̕`��ɐ؂�ւ��
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			this.isDrawMouseCursor = true;				//NowLoading���I������̂ŁA�}�E�X�J�[�\����`�悷��
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[c(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(muphic.FileNameManager.Regist(ClassName, FileName) == false)					//FileNameManager�Ƀt�@�C������o�^
			{
				//return;																		//���ɓo�^����Ă�����I�����Ȃ�(PrintManager��
			}																				//FileNameManager�ɓo�^�����\�����c����Ă��邽��)
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//�e�N�X�`���̃C���X�^���X��
				rs[i] = new Rectangle(p, bitmap.Size);										//���W�����Ƃ�
				if(!TextureTable.Contains(FileName[i]))										//�e�N�X�`�������ɓo�^����Ă��Ȃ����
				{
					TextureTable.Add(FileName[i], texture);									//TextureTable�Ɋi�[
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//���W�f�[�^�̓o�^

			if(this.isNowLoading)															//�����ANowLoading��ʂ�`�悵�Ȃ��Ƃ����Ȃ��Ȃ�
			{
				this.NumRegistTexture += FileName.Length;
				if(this.NumRegistTextureMax == 0)
				{
					DrawManager.Begin(true);												//�e�X�g�p��NowLoading�̏ꍇ�͉�ʂ��N���A����
				}
				else
				{
					DrawManager.Begin(false);												//�{�ԗp��NowLoading�̏ꍇ�͉�ʂ��N���A���Ȃ�
				}
				this.DrawNowLoading();
				DrawManager.End();
			}
		}

		/// <summary>
		/// �e�N�X�`�����f���[�g���邱�Ƃɂ���ă������̎c��c�ʂ𑝂₷���\�b�h
		/// </summary>
		/// <param name="ClassName">�����N���X�̖��O</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = muphic.FileNameManager.GetFileNames(ClassName);
			muphic.FileNameManager.Delete(ClassName);										//�폜
			muphic.PointManager.Delete(ClassName);											//�Ή����Ă�����W���폜
			for(int i=0;i<filename.Length;i++)
			{
//				if(!FileNameTable.Contains(filename[i]))									//�������ɓ����t�@�C�����Q�Ƃ��Ă���
//				{																			//�N���X���Ȃ�������
//					TextureTable.Remove(filename[i]);										//�Y������e�N�X�`�����폜
//				}										//��������������
			}
		}
		#endregion

		#region �e�N�X�`���`��֌W
		///////////////////////////////////////////////////////////////////////
		//�e�N�X�`���`��֌W
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������́A1�{�Œ�o�[�W����
			sprite.Draw2D(texture, center, 0, location,  Color.FromArgb(alpha, FilterColor.R, FilterColor.G, FilterColor.B));
		}
		#endregion

		#region �g��E�k���`��֌W
		///////////////////////////////////////////////////////////////////////
		//�g��E�k���`��֌W
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �g��E�k���̍ہA���ɗp����f�B�X�v���C�ƁA���ۂɕ`�悷��̈��ύX����
		/// </summary>
		/// <param name="srcRect">���W���w�肷��ۂ̕`��̈�(�e����Ō���Window�̗̈�)</param>
		/// <param name="destRect">���ۂɕ`�悷��̈�(�e����Ō����T���l�C���̈�)</param>
		public void ChangeWindowSize(Rectangle srcRect, Rectangle destRect)
		{
			this.src = srcRect;
			this.dest = destRect;
		}

		/// <summary>
		/// �g��E�k���������{�����e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="state"></param>
		/// <param name="isCenter"></param>
		public void DrawDivTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawDivTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// �g��E�k���������{�����e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�N���X�̃L�[</param>
		/// <param name="location">src���ɂ�������W(�g��E�k�������O���W)</param>
		/// <param name="state">���</param>
		/// <param name="isCenter">�^�񒆂ɂ��邩�ǂ���</param>
		public void DrawDivTexture(String ClassName, Point location, int state, bool isCenter)
		{
			float divX = (float)dest.Width / src.Width;
			float divY = (float)dest.Height / src.Height;
			PointF newLocation = new Point(0, 0);
			newLocation.X = location.X - src.X;
			newLocation.X = newLocation.X * divX;
			newLocation.X = newLocation.X + dest.X;
			newLocation.Y = location.Y - src.Y;
			newLocation.Y = newLocation.Y * divY;
			newLocation.Y = newLocation.Y + dest.Y;
			DrawTexture(ClassName, divX, divY, newLocation, state, isCenter);
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="divX">�`�悷����̂̉��̔{��</param>
		/// <param name="divY">�`�悷����̂̏c�̔{��</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		private void DrawTexture(String ClassName, float divX, float divY, PointF location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			PointF center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width * divX / 2;
				center.Y = r.Height * divY / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������͔{����ς��邱�Ƃ��ł���o�[�W����
			Rectangle ra = muphic.PointManager.Get(ClassName, state);
			sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new SizeF(ra.Width*divX, ra.Height*divY), PointF.Empty, 0, new PointF(location.X-center.X, location.Y-center.Y),  Color.FromArgb(alpha, FilterColor));
		}
		#endregion

		#region ������`��֌W
		///////////////////////////////////////////////////////////////////////
		//������`��֌W
		///////////////////////////////////////////////////////////////////////
		public void AddText(String str, int x, int y, Color color)
		{
			//�������sprite���I���Ă���`�悵�Ȃ��Ƃ����Ȃ��̂ŁAsprite���I��܂ňꎞTextList�ɂ��߂Ă���
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextList����̃f�[�^�̎��o��
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, Color.FromArgb(alpha, color));						//������̕`��
			}
			TextList.Clear();												//���ߍ���ł�������������ׂč폜
		}
		#endregion

		#region �t�F�[�h�C���E�t�F�[�h�A�E�g�֌W
		///////////////////////////////////////////////////////////////////////
		//�t�F�[�h�C���E�t�F�[�h�A�E�g�֌W
		///////////////////////////////////////////////////////////////////////
		public void FadeIn(int FrameCount)
		{
			FadeFrameCount = FrameCount;
			NowFadeFrameCount = 0;
			
			FadeTimer.Tick += new EventHandler(FadeInRender);
			FadeTimer.Enabled = true;
		}

		private void FadeInRender(object sender, System.EventArgs e)
		{																	//���݂̃t�F�[�h�̐F�̌���
			float PerOnce = (float)NowFadeFrameCount / FadeFrameCount;		//���݂̃t���[���J�E���g�I���̊���(0�`1)
			int r = (int)((float)FadeColor2.R * PerOnce + (float)FadeColor.R * (1-PerOnce));
			int g = (int)((float)FadeColor2.G * PerOnce + (float)FadeColor.G * (1-PerOnce));
			int b = (int)((float)FadeColor2.B * PerOnce + (float)FadeColor.B * (1-PerOnce));
			this.FilterColor = Color.FromArgb(r, g, b);						//�F�̌���
				
			DrawManager.Begin(false);
			muphic.DrawManager.FadeInEvent();								//����ʂ̕`��
			DrawManager.End();

			NowFadeFrameCount++;											//�t���[���J�E���g���C���N�������g
			
			if(NowFadeFrameCount >= FadeFrameCount)							//�t���[���J�E���g���I��������
			{
				FadeTimer.Enabled = false;
				FilterColor = Color.White;
				FadeTimer.Tick -= new EventHandler(FadeInRender);
			}
		}

		public void FadeOut(int FrameCount)
		{
			FadeFrameCount = FrameCount;
			NowFadeFrameCount = 0;
			//this.FilterColor = Color.White;
			
			FadeTimer.Tick += new EventHandler(FadeOutRender);
			FadeTimer.Enabled = true;
			//while(FadeTimer.Enabled);										//�`�悪�I���܂őҋ@
		}

		private void FadeOutRender(object sender, System.EventArgs e)
		{																	//���݂̃t�F�[�h�̐F�̌���
			float PerOnce = (float)NowFadeFrameCount / FadeFrameCount;		//���݂̃t���[���J�E���g�I���̊���(0�`1)
			int r = (int)((float)FadeColor.R * PerOnce + (float)FadeColor2.R * (1-PerOnce));
			int g = (int)((float)FadeColor.G * PerOnce + (float)FadeColor2.G * (1-PerOnce));
			int b = (int)((float)FadeColor.B * PerOnce + (float)FadeColor2.B * (1-PerOnce));
			this.FilterColor = Color.FromArgb(r, g, b);						//�F�̌���
				
			DrawManager.Begin(false);
			muphic.DrawManager.FadeOutEvent();								//����ʂ̕`��
			DrawManager.End();

			NowFadeFrameCount++;											//�t���[���J�E���g���C���N�������g
			
			if(NowFadeFrameCount >= FadeFrameCount)							//�t���[���J�E���g���I��������
			{
				FadeTimer.Enabled = false;
				//FilterColor = Color.White;
				FadeTimer.Tick -= new EventHandler(FadeOutRender);
			}
		}

		#endregion

		#region �t�F�[�h�C�x���g���M�p���\�b�h
		///////////////////////////////////////////////////////////////////////
		//�t�F�[�h�C�x���g���M�p���\�b�h
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �t�F�[�h�C���̍ە`�悷��Draw���\�b�h���Ăяo��
		/// </summary>
		/// <param name="e"></param>
		private void OnFadeIn()
		{
			if(FadeInEvent != null) FadeInEvent();
		}

		/// <summary>
		/// �t�F�[�h�A�E�g�̍ە`�悷��Draw���\�b�h���Ăяo��
		/// </summary>
		private void OnFadeOut()
		{
			if(FadeOutEvent != null) FadeOutEvent();
		}

		#endregion

		#region �f�o�C�X�֌W
		///////////////////////////////////////////////////////////////////////
		//�f�o�C�X�֌W
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//��ʂ̃N���A
			}
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//�X�v���C�g���I�������̂ŁA�������`�悷
			sprite.Begin(SpriteFlags.AlphaBlend);
			if(isDrawMouseCursor)
			{
				DrawManager.Draw(MouseClassName, MousePoint.X, MousePoint.Y, MouseState);
			}
			sprite.End();
		}

		#endregion

		#region �O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ���ĉ�NowLoading��ʂ̕`�悪�J�n����(�e�N�X�`���ǂݍ��݂̑����𒲂ׂ�Ƃ��ɗL��)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�J�n����
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���I�����邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�I������
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		#region Regist

		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		#endregion

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">�璷����</param>
		/// <param name="y">�璷����</param>
		/// <param name="FileName">�璷����</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		#region Draw

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		#region DrawDiv

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawDiv(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDiv(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawDiv(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDiv(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawDivCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDivCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawDivCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDivCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		/// <summary>
		/// �g��E�k���̍ۂ̔{�������肷�郁�\�b�h
		/// </summary>
		/// <param name="srcRect">�`�悷����W��o�^���鉼�z�E�B���h�E�̎l�p�`(�o�^�l�p�`)</param>
		/// <param name="destRect">���ۂɃE�B���h�E�ɕ`�悷��ۂɕ`�悳���l�p�`(�`��l�p�`)</param>
		static public void Change(Rectangle srcRect, Rectangle destRect)
		{
			muphic.DrawManager.drawManager.ChangeWindowSize(srcRect, destRect);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// �������`�悷�郁�\�b�h
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}

		/// <summary>
		/// �t�F�[�h�C�����n�߂郁�\�b�h(1�b��)
		/// </summary>
		static public void StartFadeIn()
		{
			muphic.DrawManager.drawManager.FadeIn(60);
		}

		/// <summary>
		/// �t�F�[�h�C���Ƃ͂��߂郁�\�b�h(�t���[�����Ŏw��)
		/// </summary>
		/// <param name="FrameCount">�t�F�[�h�C��������t���[����</param>
		static public void StartFadeIn(int FrameCount)
		{
			muphic.DrawManager.drawManager.FadeIn(FrameCount);
		}

		/// <summary>
		/// �t�F�[�h�A�E�g���n�߂郁�\�b�h(1�b��)
		/// </summary>
		static public void StartFadeOut()
		{
			muphic.DrawManager.drawManager.FadeOut(60);
		}

		/// <summary>
		/// �t�F�[�h�A�E�g���n�߂郁�\�b�h(�t���[�����Ŏw��)
		/// </summary>
		/// <param name="FrameCount">�t�F�[�h�A�E�g������t���[����</param>
		static public void StartFadeOut(int FrameCount)
		{
			muphic.DrawManager.drawManager.FadeOut(FrameCount);
		}

		/// <summary>
		/// �J�[�\���̃f�[�^��ݒ肷�郁�\�b�h
		/// </summary>
		/// <param name="ClassName">�J�[�\���̓o�^��</param>
		/// <param name="p">�}�E�X�̍��W</param>
		/// <param name="state">�}�E�X�̏��</param>
		static public void SetCursor(String ClassName, Point p, int state)
		{
			muphic.DrawManager.drawManager.MousePoint = p;
			muphic.DrawManager.drawManager.MouseState = state;
			muphic.DrawManager.drawManager.MouseClassName = ClassName;
		}

		#endregion
	}*/
	#endregion
	
	#region Ver.9
		
	public delegate void FadeEventHandler();
	/// <summary>
	/// DrawManager version2 �n�b�V���e�[�u����2�ɂ��ē����e�N�X�`�����d���Ăяo�����Ȃ��悤�ɂ����B
	/// DrawManager version3 DrawString�@�\��ǉ��B�ڍׂ�DrawText���\�b�h���Q�ƁB
	/// DrawManager version4 isWindow���[�h��ݒ肷�邱�Ƃɂ��E�B���h�E���[�h���t���X�N���[�����[�h����I���\
	/// DrawManager version5 BeginRegist���\�b�h���ĂԂƁANowLoading�̉�ʂ��\�������悤�ɂȂ�
	/// DrawManager version6 FileNameTable�̊Ǘ���FileNameManager�ɔC����(����@�\�����̂���) ���ƁADeleteTexture�̖�蔭�o(���̃e�N�X�`�����g���Ă��邩�ǂ������ׂ邱�Ƃ��ł��Ȃ�)[
	/// DrawManager version7 ���ꉹ�y�Ŏg���T���l�C���@�\(�g��E�k���\���@�\)�ǉ�
	/// DrawManager version7.1 �t���X�N���[�����[�h�̂Ƃ��A�^�C�g���o�[�������悤�ɒ�����
	/// DrawManager version8 �t�F�[�h�C���A�t�F�[�h�A�E�g�@�\��ǉ�
	/// DrawManager version8.1 �}�E�X�J�[�\����NowLoading���ɕ\������Ă���_���C��
	/// DrawManager version8.2 �k���`��̍ۂ̖����C��(DrawDiv��Draw2D���\�b�h���ύX����Ă���)
	/// DrawManager version9 NowLoading�̍�����
	/// DrawManager version9.1 �t�F�[�h�C���E�t�F�[�h�A�E�g�ɂ��āA�I�������烁�\�b�h����o��d�l�ɕύX
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = muphic.Common.CommonSettings.getIsWindow();		//�E�B���h�E���[�h���ǂ���(false�Ńt���X�N���[�����[�h)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//�t�@�C�����ƃe�N�X�`�����֘A�t���Ă���
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//���̒��ɂ́A���ꂼ��`��҂��̕����񂽂����A(������Ax�Ay�AColor)�̏��ɓ����Ă���

		//NowLoading�p�ϐ�
		private bool isNowLoading;								//NowLoading�̉�ʂ�\������ׂ����ǂ���
		private int NumRegistTextureMax;						//NowLoading�̉�ʂ̊Ԃɓo�^���ׂ��e�N�X�`����
		private int NumRegistTexture;							//���ݓǂݍ��񂾃e�N�X�`���̐�
		private int NowDrawingPercent;							//���݉�ʂɕ`�悳��Ă��銄��(%)
		private int DrawingPercentOnce=5;						//1�x�ɉ�ʂɕ`�悷�銄��

		private static Rectangle BarRect = new Rectangle(268, 494, 482, 30);	// NowLoading �o�[�̎l�p�`
		private int BarWidth;

		private bool flagNowLoading;

		//�g��E�k���֌W�ϐ�
		Rectangle src;											//���z�E�B���h�E�̎l�p�`
		Rectangle dest;											//���ۂɃE�B���h�E�ɕ`�悷��ۂ̎l�p�`

		//�t�F�[�h�C���E�t�F�[�h�A�E�g�p�ϐ�
		int alpha = 255;										//���ߓx������킷�ϐ�(255�ŕ��ʂɕ`��)
		int fade;												//�t�F�[�h�x������킷�ϐ�(255�ŕ��ʂɕ`��)
		Color FadeColor = Color.Black;							//�t�F�[�h�C���̎n�߁A�t�F�[�h�A�E�g�̍Ō�̐F
		Color FadeColor2 = Color.White;							//�t�F�[�h�C���̍Ō�A�t�F�[�h�A�E�g�̎n�߂̐F
		Color FilterColor = Color.White;						//���݃e�N�X�`����`�悷��ۂɗp����t�B���^�J���[
		public static event FadeEventHandler FadeInEvent;		//�t�F�[�h�C���̍ۂɌĂ�Draw���\�b�h��o�^����C�x���g
		public static event FadeEventHandler FadeOutEvent;		//�t�F�[�h�A�E�g�̍ۂɌĂ�Draw���\�b�h��o�^����C�x���g
		Timer FadeTimer;										//�t�F�[�h�C���E�t�F�[�h�A�E�g�̍ۂ̃t���[���J�E���^�[
		int NowFadeFrameCount;									//�t�F�[�h�����̍ۂ̌��݂̃t���[����
		int FadeFrameCount;										//�t�F�[�h����������t���[����

		//�}�E�X�J�[�\���̕`��֌W
		String MouseClassName;
		Point MousePoint;
		int MouseState;
		bool isDrawMouseCursor = true;

		public DrawManager(MainScreen form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// �t�H���g�f�[�^�̍\���̂��쐬
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
			
			// �\���̂ɕK�v�ȃf�[�^���Z�b�g
			fd.Height = 24;
			//fd.FaceName = "�l�r �S�V�b�N";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// �t�H���g���쐬
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// ��O����
				MessageBox.Show("������`��G���[");
				return;
			}

			//FadeTimer = new Timer(new System.ComponentModel.Container());
			//FadeTimer.Interval = 16;
			FadeTimer = form.FadeTimer;
		}

		private void InitDevice(MainScreen form)
		{
			PresentParameters pParameters = new PresentParameters();	//�p�����[�^�ݒ�N���X�̃C���X�^���X��
			pParameters.SwapEffect = SwapEffect.Discard;				//�X���b�v�̐ݒ�(Discard:�ł������I�ȕ��@���f�B�X�v���C���Ō��肷��)
			if(isWindow)											//�E�B���h�E���[�h�̏ꍇ�̐ݒ�
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//�E�B���h�E���[�h�̐ݒ�(true�Ȃ̂ŃE�B���h�E���[�h)
			}
			else
			{														//�t���X�N���[�����[�h�̏ꍇ�̐ݒ�
				form.Size = new Size(1024, 768);
				form.ControlBox = false;							//�^�C�g���o�[����
				form.Text = "";										//�^�C�g���o�[�����������C����������ł���Ȃ�Microsoft
				pParameters.Windowed = false;							//�E�B���h�E���[�h�̐ݒ�(false�Ȃ̂Ńt���X�N���[�����[�h)
				pParameters.EnableAutoDepthStencil = true;				// �[�x�X�e���V���o�b�t�@�̐ݒ�
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// �����[�x�X�e���V�� �T�[�t�F�C�X�̃t�H�[�}�b�g�̐ݒ�

				// �g�p�ł���f�B�X�v���C���[�h���������A�ړI�̃��[�h��T��
				bool flag = false;

				// �f�B�v���C���[�h��񋓂��A�T�C�Y���u1024�~768�v����
				// ���t���b�V�����[�g���u60�v�̃��[�h��T��
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// �����Ɍ������Ύg�p����
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// �����������Ƃ������t���O�𗧂Ă�
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// �ړI�̃��[�h���Ȃ���΂��̂܂܏I��
					MessageBox.Show("�w�肵���f�B�v���C���[�h�͌�����܂���ł����B",
						"�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


			#region �f�o�C�X�������
			//�f�o�C�X����
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//���_�`���ݒ�Ŏ��s���Ă����炱��łȂ�
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//�f�o�C�X�^�C�v�Ŏ��s���Ă����炱��łȂ�
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//�������s���Ă����炱��łȂ�
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//����ł����߂�������ǂ����悤���Ȃ�
							MessageBox.Show("����");
							Application.Exit();
						}
					}
				}
			}
			#endregion

			sprite = new Sprite(device);								//Sprite�I�u�W�F�N�g�̃C���X�^���X��
		}

		#region �e�N�X�`���o�^�ENowLoading�֌W
		///////////////////////////////////////////////////////////////////////
		//�e�N�X�`���o�^�A�폜�ENowLoading�֌W
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// ���ꂩ��e�N�X�`���̓o�^��Ƃ��n�߂邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁAEndRegistTexture���Ăяo���܂�NowLoading��ʂ��\�������
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NowDrawingPercent = 0;
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			this.BarWidth = 0;
			this.isDrawMouseCursor = false;					//NowLoading���̓}�E�X�J�[�\����\�����Ȃ�
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				DrawTexture("Nowloading_bak", 0, false);	// �w�i�Z����
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}

			flagNowLoading = true;
		}
		
		/// <summary>
		/// NowLoading��ʂ�`�悷�郁�\�b�h
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("���[�h��..." + this.NumRegistTexture, 0, 100);
				return;
			}
			
			int NowPercent = (int)((float)NumRegistTexture / NumRegistTextureMax * 100);	//���݂̓ǂݍ��݊���(%)
			if(this.NowDrawingPercent + this.DrawingPercentOnce <= NowPercent)				//���݁A�O�ɕ`�悵�����̂������1��؂蕪�����ǂݍ��݂��I�������
			{
				int NowBarWidth = (int)((float)BarRect.Width * NowDrawingPercent / 100);	//���܂ŕ`�悵�Ă�����
				int NewBarWidth = (int)((float)BarRect.Width * (NowDrawingPercent + DrawingPercentOnce) / 100);//����V�����t����������̕�
				//int NewBarWidth = (int)(BarRect.Width * (this.NumRegistTexture / (float)this.NumRegistTextureMax));
				DrawManager.Begin(false);
				for(int i=0;i<NewBarWidth;i++)									//���̊Ԃ�for�����܂킷
				{
					DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
				}

				if (flagNowLoading)
				{
					DrawTexture("Nowloading_bak", 0, false);
					DrawTexture("Nowloading_all", 0, false);
					flagNowLoading = false;

				}
				DrawManager.End();
				this.NowDrawingPercent += DrawingPercentOnce;
			}	
		}

		/// <summary>
		/// ����Ńe�N�X�`���̓o�^��Ƃ��I���邱�Ƃ�DrawManager�ɋ����郁�\�b�h�B
		/// ���Ȃ݂ɂ�����Ăяo���ƁANowLoading��ʂ̕\�����I����āA���ʂ̕`��ɐ؂�ւ��
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			this.isDrawMouseCursor = true;				//NowLoading���I������̂ŁA�}�E�X�J�[�\����`�悷��
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// �e�N�X�`�������ۂɓo�^����N���X�A�����ɍ��W�ƕ��E�����̓o�^���s��
		/// </summary>
		/// <param name="ClassName">�n�b�V���ɓo�^����L�[c(�N���X��)</param>
		/// <param name="p">�摜��\��������W(�Œ�摜�̏ꍇ�A���I�摜�̏ꍇ�͂Ƃ肠����(0,0)�ł����Ǝv��)</param>
		/// <param name="FileName">�e�N�X�`���ɂ���摜�t�@�C���̖��O(������)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(muphic.FileNameManager.Regist(ClassName, FileName) == false)					//FileNameManager�Ƀt�@�C������o�^
			{
				//return;																		//���ɓo�^����Ă�����I�����Ȃ�(PrintManager��
			}																				//FileNameManager�ɓo�^�����\�����c����Ă��邽��)
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNG�t�@�C���̓ǂݍ���
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//�e�N�X�`���̃C���X�^���X��
				rs[i] = new Rectangle(p, bitmap.Size);										//���W�����Ƃ�
				if(!TextureTable.Contains(FileName[i]))										//�e�N�X�`�������ɓo�^����Ă��Ȃ����
				{
					TextureTable.Add(FileName[i], texture);									//TextureTable�Ɋi�[
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//���W�f�[�^�̓o�^

			if(this.isNowLoading)															//�����ANowLoading��ʂ�`�悵�Ȃ��Ƃ����Ȃ��Ȃ�
			{
				this.NumRegistTexture += FileName.Length;
//				if(this.NumRegistTextureMax == 0)
//				{
//					DrawManager.Begin(true);												//�e�X�g�p��NowLoading�̏ꍇ�͉�ʂ��N���A����
//				}
//				else
//				{
//					DrawManager.Begin(false);												//�{�ԗp��NowLoading�̏ꍇ�͉�ʂ��N���A���Ȃ�
//				}
				this.DrawNowLoading();
				
//				DrawManager.End();
			}
		}

		/// <summary>
		/// �e�N�X�`�����f���[�g���邱�Ƃɂ���ă������̎c��c�ʂ𑝂₷���\�b�h
		/// </summary>
		/// <param name="ClassName">�����N���X�̖��O</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = muphic.FileNameManager.GetFileNames(ClassName);
			muphic.FileNameManager.Delete(ClassName);										//�폜
			muphic.PointManager.Delete(ClassName);											//�Ή����Ă�����W���폜
			for(int i=0;i<filename.Length;i++)
			{
//				if(!FileNameTable.Contains(filename[i]))									//�������ɓ����t�@�C�����Q�Ƃ��Ă���
//				{																			//�N���X���Ȃ�������
//					TextureTable.Remove(filename[i]);										//�Y������e�N�X�`�����폜
//				}										//��������������
			}
		}
		#endregion

		#region �e�N�X�`���`��֌W
		///////////////////////////////////////////////////////////////////////
		//�e�N�X�`���`��֌W
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			Point center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������́A1�{�Œ�o�[�W����
			sprite.Draw2D(texture, center, 0, location,  Color.FromArgb(alpha, FilterColor.R, FilterColor.G, FilterColor.B));
		}
		#endregion

		#region �g��E�k���`��֌W
		///////////////////////////////////////////////////////////////////////
		//�g��E�k���`��֌W
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �g��E�k���̍ہA���ɗp����f�B�X�v���C�ƁA���ۂɕ`�悷��̈��ύX����
		/// </summary>
		/// <param name="srcRect">���W���w�肷��ۂ̕`��̈�(�e����Ō���Window�̗̈�)</param>
		/// <param name="destRect">���ۂɕ`�悷��̈�(�e����Ō����T���l�C���̈�)</param>
		public void ChangeWindowSize(Rectangle srcRect, Rectangle destRect)
		{
			this.src = srcRect;
			this.dest = destRect;
		}

		/// <summary>
		/// �g��E�k���������{�����e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="state"></param>
		/// <param name="isCenter"></param>
		public void DrawDivTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			DrawDivTexture(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// �g��E�k���������{�����e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�N���X�̃L�[</param>
		/// <param name="location">src���ɂ�������W(�g��E�k�������O���W)</param>
		/// <param name="state">���</param>
		/// <param name="isCenter">�^�񒆂ɂ��邩�ǂ���</param>
		public void DrawDivTexture(String ClassName, Point location, int state, bool isCenter)
		{
			float divX = (float)dest.Width / src.Width;
			float divY = (float)dest.Height / src.Height;
			PointF newLocation = new Point(0, 0);
			newLocation.X = location.X - src.X;
			newLocation.X = newLocation.X * divX;
			newLocation.X = newLocation.X + dest.X;
			newLocation.Y = location.Y - src.Y;
			newLocation.Y = newLocation.Y * divY;
			newLocation.Y = newLocation.Y + dest.Y;
			DrawTexture(ClassName, divX, divY, newLocation, state, isCenter);
		}

		/// <summary>
		/// ���ۂɃe�N�X�`������ʂɕ`�悷��(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�`�悷��e�N�X�`���̃L�[</param>
		/// <param name="divX">�`�悷����̂̉��̔{��</param>
		/// <param name="divY">�`�悷����̂̏c�̔{��</param>
		/// <param name="location">�`�悷����W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		private void DrawTexture(String ClassName, float divX, float divY, PointF location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�������Ȃ�"
			}
			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
			PointF center = new Point(0, 0);
			if(isCenter)													//�^�񒆂ŕ\������ꍇ
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width * divX / 2;
				center.Y = r.Height * divY / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
			}
			//�������͔{����ς��邱�Ƃ��ł���o�[�W����
			Rectangle ra = muphic.PointManager.Get(ClassName, state);
			sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new SizeF(ra.Width*divX, ra.Height*divY), PointF.Empty, 0, new PointF(location.X-center.X, location.Y-center.Y),  Color.FromArgb(alpha, FilterColor));
		}
		#endregion

		#region ������`��֌W
		///////////////////////////////////////////////////////////////////////
		//������`��֌W
		///////////////////////////////////////////////////////////////////////
		public void AddText(String str, int x, int y, Color color)
		{
			//�������sprite���I���Ă���`�悵�Ȃ��Ƃ����Ȃ��̂ŁAsprite���I��܂ňꎞTextList�ɂ��߂Ă���
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextList����̃f�[�^�̎��o��
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, Color.FromArgb(alpha, color));						//������̕`��
			}
			TextList.Clear();												//���ߍ���ł�������������ׂč폜
		}
		#endregion

		#region �t�F�[�h�C���E�t�F�[�h�A�E�g�֌W
		///////////////////////////////////////////////////////////////////////
		//�t�F�[�h�C���E�t�F�[�h�A�E�g�֌W
		///////////////////////////////////////////////////////////////////////
		public void FadeIn(int FrameCount)
		{
			FadeFrameCount = FrameCount;
			NowFadeFrameCount = 0;
			
			FadeTimer.Tick += new EventHandler(FadeInRender);
			FadeTimer.Enabled = true;
			while(FadeTimer.Enabled)
			{
				Application.DoEvents();
			}										//�`�悪�I���܂őҋ@
		}

		private void FadeInRender(object sender, System.EventArgs e)
		{																	//���݂̃t�F�[�h�̐F�̌���
			float PerOnce = (float)NowFadeFrameCount / FadeFrameCount;		//���݂̃t���[���J�E���g�I���̊���(0�`1)
			int r = (int)((float)FadeColor2.R * PerOnce + (float)FadeColor.R * (1-PerOnce));
			int g = (int)((float)FadeColor2.G * PerOnce + (float)FadeColor.G * (1-PerOnce));
			int b = (int)((float)FadeColor2.B * PerOnce + (float)FadeColor.B * (1-PerOnce));
			this.FilterColor = Color.FromArgb(r, g, b);						//�F�̌���
				
			DrawManager.Begin(false);
			muphic.DrawManager.FadeInEvent();								//����ʂ̕`��
			DrawManager.End();

			NowFadeFrameCount++;											//�t���[���J�E���g���C���N�������g
			
			if(NowFadeFrameCount >= FadeFrameCount)							//�t���[���J�E���g���I��������
			{
				FadeTimer.Enabled = false;
				FilterColor = Color.White;
				FadeTimer.Tick -= new EventHandler(FadeInRender);
			}
		}

		public void FadeOut(int FrameCount)
		{
			FadeFrameCount = FrameCount;
			NowFadeFrameCount = 0;
			//this.FilterColor = Color.White;
			
			FadeTimer.Tick += new EventHandler(FadeOutRender);
			FadeTimer.Enabled = true;
			while(FadeTimer.Enabled)
			{
				Application.DoEvents();
			}										//�`�悪�I���܂őҋ@
		}

		private void FadeOutRender(object sender, System.EventArgs e)
		{																	//���݂̃t�F�[�h�̐F�̌���
			float PerOnce = (float)NowFadeFrameCount / FadeFrameCount;		//���݂̃t���[���J�E���g�I���̊���(0�`1)
			int r = (int)((float)FadeColor.R * PerOnce + (float)FadeColor2.R * (1-PerOnce));
			int g = (int)((float)FadeColor.G * PerOnce + (float)FadeColor2.G * (1-PerOnce));
			int b = (int)((float)FadeColor.B * PerOnce + (float)FadeColor2.B * (1-PerOnce));
			this.FilterColor = Color.FromArgb(r, g, b);						//�F�̌���
				
			DrawManager.Begin(false);
			muphic.DrawManager.FadeOutEvent();								//����ʂ̕`��
			DrawManager.End();

			NowFadeFrameCount++;											//�t���[���J�E���g���C���N�������g
			
			if(NowFadeFrameCount >= FadeFrameCount)							//�t���[���J�E���g���I��������
			{
				FadeTimer.Enabled = false;
				//FilterColor = Color.White;
				FadeTimer.Tick -= new EventHandler(FadeOutRender);
			}
		}

		#endregion

		#region �t�F�[�h�C�x���g���M�p���\�b�h
		///////////////////////////////////////////////////////////////////////
		//�t�F�[�h�C�x���g���M�p���\�b�h
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �t�F�[�h�C���̍ە`�悷��Draw���\�b�h���Ăяo��
		/// </summary>
		/// <param name="e"></param>
		private void OnFadeIn()
		{
			if(FadeInEvent != null) FadeInEvent();
		}

		/// <summary>
		/// �t�F�[�h�A�E�g�̍ە`�悷��Draw���\�b�h���Ăяo��
		/// </summary>
		private void OnFadeOut()
		{
			if(FadeOutEvent != null) FadeOutEvent();
		}

		#endregion

		#region �f�o�C�X�֌W
		///////////////////////////////////////////////////////////////////////
		//�f�o�C�X�֌W
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �f�o�C�X�̕`��J�n���\�b�h���ĂԂ���
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//��ʂ̃N���A
			}
			device.BeginScene();											//�`��J�n
		}

		/// <summary>
		/// �f�o�C�X�̕`��I�����\�b�h���ĂԂ���
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//�`��I��
			device.Present();												//�T�[�t�F�C�X�ƃo�b�t�@�ƌ�������
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// �X�v���C�g�`����I����Ƃ��ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//�X�v���C�g���I�������̂ŁA�������`�悷
			sprite.Begin(SpriteFlags.AlphaBlend);
			if(isDrawMouseCursor)
			{
				DrawManager.Draw(MouseClassName, MousePoint.X, MousePoint.Y, MouseState);
			}
			sprite.End();
		}

		#endregion

		#region �O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ���ĉ�NowLoading��ʂ̕`�悪�J�n����(�e�N�X�`���ǂݍ��݂̑����𒲂ׂ�Ƃ��ɗL��)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���J�n���邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�J�n����
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// �e�N�X�`���̓o�^���I�����邱�Ƃ�DrawManager�ɓ`���郁�\�b�h�B
		/// ������ĂԂ��Ƃɂ����NowLoading��ʂ̕`�悪�I������
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		#region Regist

		/// <summary>
		/// �e�N�X�`����o�^����(1����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(2����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName1">�o�^����1�ڂ̉摜�t�@�C����</param>
		/// <param name="FileName2">�o�^����2�ڂ̉摜�t�@�C����</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// �e�N�X�`����o�^����(����)
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�摜��\������Ƃ���x���W�̒l</param>
		/// <param name="y">�摜��\������Ƃ���y���W�̒l</param>
		/// <param name="FileName">�o�^����摜�t�@�C�����̔z��</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		#endregion

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// �e�N�X�`�����폜����
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">�璷����</param>
		/// <param name="y">�璷����</param>
		/// <param name="FileName">�璷����</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		#region Draw

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		#region DrawDiv

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawDiv(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDiv(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawDiv(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �e�N�X�`����`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDiv(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void DrawDivCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, true);
		}

		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDivCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void DrawDivCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �e�N�X�`�������W�𒆐S�Ƃ��ĕ`�悷��
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void DrawDivCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		/// <summary>
		/// �g��E�k���̍ۂ̔{�������肷�郁�\�b�h
		/// </summary>
		/// <param name="srcRect">�`�悷����W��o�^���鉼�z�E�B���h�E�̎l�p�`(�o�^�l�p�`)</param>
		/// <param name="destRect">���ۂɃE�B���h�E�ɕ`�悷��ۂɕ`�悳���l�p�`(�`��l�p�`)</param>
		static public void Change(Rectangle srcRect, Rectangle destRect)
		{
			muphic.DrawManager.drawManager.ChangeWindowSize(srcRect, destRect);
		}

		/// <summary>
		/// �X�v���C�g�`����n�߂鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		/// <param name="isClear">��ʂ��N���A���邩�ǂ���</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// �X�v���C�g�`����I���鎞�ɌĂԕK�v�����郁�\�b�h
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// �������`�悷�郁�\�b�h
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}

		/// <summary>
		/// �t�F�[�h�C�����n�߂郁�\�b�h(1�b��)
		/// </summary>
		static public void StartFadeIn()
		{
			muphic.DrawManager.drawManager.FadeIn(60);
		}

		/// <summary>
		/// �t�F�[�h�C���Ƃ͂��߂郁�\�b�h(�t���[�����Ŏw��)
		/// </summary>
		/// <param name="FrameCount">�t�F�[�h�C��������t���[����</param>
		static public void StartFadeIn(int FrameCount)
		{
			muphic.DrawManager.drawManager.FadeIn(FrameCount);
		}

		/// <summary>
		/// �t�F�[�h�A�E�g���n�߂郁�\�b�h(1�b��)
		/// </summary>
		static public void StartFadeOut()
		{
			muphic.DrawManager.drawManager.FadeOut(60);
		}

		/// <summary>
		/// �t�F�[�h�A�E�g���n�߂郁�\�b�h(�t���[�����Ŏw��)
		/// </summary>
		/// <param name="FrameCount">�t�F�[�h�A�E�g������t���[����</param>
		static public void StartFadeOut(int FrameCount)
		{
			muphic.DrawManager.drawManager.FadeOut(FrameCount);
		}

		/// <summary>
		/// �J�[�\���̃f�[�^��ݒ肷�郁�\�b�h
		/// </summary>
		/// <param name="ClassName">�J�[�\���̓o�^��</param>
		/// <param name="p">�}�E�X�̍��W</param>
		/// <param name="state">�}�E�X�̏��</param>
		static public void SetCursor(String ClassName, Point p, int state)
		{
			muphic.DrawManager.drawManager.MousePoint = p;
			muphic.DrawManager.drawManager.MouseState = state;
			muphic.DrawManager.drawManager.MouseClassName = ClassName;
		}

		#endregion
	}
	#endregion
}
