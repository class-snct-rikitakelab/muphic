using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace muphic
{
	#region ver.1.0.0
	/*
	/// <summary>
	/// PrintManager �̊T�v�̐����ł��B
	/// </summary>
	public class PrintManager
	{
		int NowRegistingNum = -1;										//(�o�^���Ɍ���)������PointQueue�̂ǂ̗v�f��o�^�����Ă邩�𒲂ׂ�ϐ�
		int NowPrintingNum = 0;											//(������Ɍ���)������PointQueue�̂ǂ̗v�f��������Ă��邩�𒲂ׂ�ϐ�(PrintPage�Ŏg�p)
		private static PrintManager printManager;
		private PrintDocument pd;
		private Hashtable BitmapTable;									//�r�b�g�}�b�v�t�@�C�����i�[���Ă���悤�Ȃ���(�L�[�̓t�@�C����)
		private ArrayList[] PointQueue;									//�������Ƃ��̍��W���i�[���Ă���҂��s��(�L�[�̓t�@�C����)3���ڂɈ��������̂́A�v�f2�ɓ����΂悢

		public PrintManager()
		{
			BitmapTable = new Hashtable();
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(PrintPage);
			pd.EndPrint += new PrintEventHandler(EndPrint);
			muphic.PrintManager.printManager = this;
		}

		/// <summary>
		/// �r�b�g�}�b�v�t�@�C��������ł����Ԃɂ��đҋ@������(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�o�^����r�b�g�}�b�v�t�@�C���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void RegistBitmap(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			RegistBitmap(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// �r�b�g�}�b�v�t�@�C��������ł����Ԃɂ��đҋ@������(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�o�^����r�b�g�}�b�v�t�@�C���̃L�[</param>
		/// <param name="location">�o�^������W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ēo�^�����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void RegistBitmap(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);	//�o�^����t�@�C���̃t�@�C�������擾
			if(fname == null)														//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;																//�K�E"�������Ȃ�"
			}

			if(BitmapTable.ContainsKey(fname) == false)
			{
				Bitmap b = new Bitmap(fname);										//�t�@�C���������Ƃ�Bitmap�N���X���C���X�^���X��
				BitmapTable.Add(fname, b);											//BitmapTable�ɒǉ�
			}
			if(isCenter)															//�^�񒆂̏ꍇ�̏C��
			{
				Bitmap b = (Bitmap)BitmapTable[fname];
				location.X -= b.Width / 2;
				location.Y -= b.Height / 2;
			}
			PointQueue[NowRegistingNum].Add(fname);
			PointQueue[NowRegistingNum].Add(location);
			
//			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
//			if(fname == null)												//�t�@�C�������o�^����Ă��Ȃ��ꍇ
//			{
//				return;														//�K�E"�������Ȃ�"
//			}
//			Texture texture = (Texture)TextureTable[fname];					//�t�@�C��������e�N�X�`�����擾
//			Point center = new Point(0, 0);
//			if(isCenter)													//�^�񒆂ŕ\������ꍇ
//			{
//				Rectangle r = muphic.PointManager.Get(ClassName, state);
//				center.X = r.Width / 2;
//				center.Y = r.Height / 2;									//����ł��傤�ǉ摜�̐^�񒆂��Z���^�[�ɂȂ�
//			}
//			//�������́A1�{�Œ�o�[�W����
//			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
//			//�������͔{����ς��邱�Ƃ��ł���o�[�W����
//			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
//			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width, ra.Height), center, 0, new Point(ra.X, ra.Y), Color.FromArgb(255, 255, 255));

		}

		/// <summary>
		/// PointQueue�ɓo�^����Ώۂ̗v�f��ύX����
		/// </summary>
		/// <param name="NewNum"></param>
		public void ChangeRegistingPage(int NewNum)
		{
			if(NowRegistingNum >= NewNum)								//�����A�V�����y�[�W���܂ł̋��e�͈͓��Ȃ�
			{
				this.NowPrintingNum = NewNum;							//�ύX��������
				return;													//�I��
			}
			this.NowRegistingNum = NewNum;								//�����A���e�͈͊O�Ȃ�
			ArrayList[] als = new ArrayList[NewNum+1];					//�V�������Ȃ���΂Ȃ�Ȃ�
			int i;

			for(i=0;i<PointQueue.Length;i++)							//�Â����e�͈͂̕���
			{															//���ʂɃR�s�[
				als[i] = PointQueue[i];
			}
			for(i=i;i<als.Length;i++)									//�Â����e�͈͊O�̕���
			{															//�V�����C���X�^���X��
				als[i] = new ArrayList();
			}
			PointQueue = als;											//�V������������̂�PointQueue�ɑ��
		}

		/// <summary>
		/// �y�[�W�̈�����J�n���郁�\�b�h
		/// </summary>
		public void BeginPrint()
		{
			pd.DefaultPageSettings.Landscape = true;
			//pd.PrinterSettings.LandscapeAngle = 90;
			pd.Print();
		}

		Rectangle RealField;														//����p�̃t�B�[���h�ŁA���4:3�ɏC����������
		SizeF div;																	//���܂ł̃t�B�[���h�ƐV�����t�B�[���h�̃T�C�Y��

		/// <summary>
		/// ChangeField�Ŏg�����܂ł̃t�B�[���h�ƐV�����t�B�[���h�̃T�C�Y������肷�郁�\�b�h
		/// </summary>
		/// <param name="Field">����p�̃t�B�[���h</param>
		private void DecideDiv(Rectangle Field)
		{
			if(Field.Width/4 > Field.Height/3)										//�c�̕����䂪�������̂�
			{																		//�c�ɍ��킹��
				Size RealSize = new Size(Field.Height/3*4, Field.Height);
				RealField = new Rectangle(Field.X+(Field.Width-RealSize.Width)/2, Field.Y, RealSize.Width, RealSize.Height);
			}
			else if(Field.Width/4 < Field.Height/3)									//���̕����䂪�������̂�
			{																		//���ɍ��킹��
				Size RealSize = new Size(Field.Width, Field.Width/4*3);
				RealField = new Rectangle(Field.X, Field.Y+(Field.Height-RealSize.Height)/2, RealSize.Width, RealSize.Height);
			}
			else
			{
				RealField = Field;
			}
			div = new SizeF(RealField.Width / 1024, RealField.Height / 768);	//���܂ł̃t�B�[���h�ƐV�����t�B�[���h�̃T�C�Y��
		}

		/// <summary>
		/// ���܂ł�1024�~768�̃t�B�[���h�������p�̃t�B�[���h�ւƐ؂�ւ��郁�\�b�h
		/// </summary>
		/// <param name="src">�؂�ւ���Ώۂ̎l�p�`</param>
		/// <param name="Field">����p�̃t�B�[���h</param>
		/// <returns></returns>
		private void ChangeField(ref Rectangle src)
		{
//			Rectangle answer = new Rectangle(0, 0, 0, 0);
//			answer.Width = (int)(src.Width / div.Width);
//			answer.Height = (int)(src.Height / div.Height);
//			answer.X = (int)(src.X / div.Width + RealField.X);
//			answer.Y = (int)(src.Y / div.Height + RealField.Y);
//			return answer;
			src.Width = (int)(src.Width / div.Width);
			src.Height = (int)(src.Height / div.Height);
			src.X = (int)(src.X / div.Width + RealField.X);
			src.Y = (int)(src.Y / div.Height + RealField.Y);
		}

		/// <summary>
		/// ���ۂɈ�������郁�\�b�h�B�������y�[�W�̕��������ꂪ�Ă΂��B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(this.NowPrintingNum == 0)																//���߂ĂȂ�
			{
				this.DecideDiv(e.MarginBounds);															//�t�B�[���h�̔�����肳����
			}
			Graphics g = e.Graphics;
			for(int i=0;i<PointQueue[this.NowPrintingNum].Count/2;i++)
			{
				String fname = (String)PointQueue[NowPrintingNum][i*2];									//PointQueue����t�@�C���������o��
				Point location = (Point)PointQueue[NowPrintingNum][i*2+1];								//PointQueue������W�����o��
				Bitmap b = (Bitmap)BitmapTable[fname];													//�t�@�C���������Ƃ�Bitmap�N���X�����o��
				Rectangle src = new Rectangle(0, 0, b.Width, b.Height);									//�r�b�g�}�b�v�t�@�C���̕��E�������w�肷��
				//src = this.ChangeField(src);															//����p�ɍ��W��ϊ�����
				Rectangle dest = new Rectangle(location.X, location.Y, b.Width, b.Height);				//�\��t����̍��W���w�肷��
				g.DrawImage(b, dest, src, GraphicsUnit.Pixel);											//�\��t��
			}
			this.NowPrintingNum++;
			if(NowPrintingNum == PointQueue.Length)														//�����A���ׂĈ�����I�����Ȃ�
			{
				e.HasMorePages = false;																	//����I��
			}
			else
			{
				e.HasMorePages = true;
			}System.Diagnostics.Debug.WriteLine(e.PageBounds, "Page");
			System.Diagnostics.Debug.WriteLine(e.MarginBounds, "Margin");
		}

		/// <summary>
		/// ���ۂɈ�����J�n���ꂽ�Ƃ��ɂ�����C�x���g�B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void StartPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//this.NowPrintingNum = 0;												//�y�[�W�̏�����
		}

		/// <summary>
		/// ���ۂ̈�����I�����ꂽ�Ƃ��ɂ�����C�x���g�B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BitmapTable.Clear();
			for(int i=0;i<PointQueue.Length;i++)
			{
				PointQueue[i].Clear();
			}
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			this.NowPrintingNum = 0;												//�y�[�W�̏�����
			this.NowRegistingNum = -1;												//�o�^�Ώۃy�[�W�̕���������
		}

//		public static void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//		{
//		}

		
		/// <summary>
		/// �o�^����Ώۂ̃y�[�W��ύX����
		/// </summary>
		/// <param name="NewPage"></param>
		static public void ChangePage(int NewPage)
		{
			muphic.PrintManager.printManager.ChangeRegistingPage(NewPage-1);			//�����ł̕ϐ��̓y�[�W���ŁA�������ŌĂ΂��̂͗v�f���Ȃ��Ƃɒ���
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Regist(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Regist(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Regist(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Regist(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void RegistCenter(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, true);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void RegistCenter(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, true);
		}

		
		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void RegistCenter(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void RegistCenter(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �y�[�W�̈�����J�n����
		/// </summary>
		public static void Print()
		{
			muphic.PrintManager.printManager.BeginPrint();
		}
	}
	*/
	#endregion
	
	#region ver.2.0.0 ���������@�\�t��
	/*
	/// <summary>
	/// PrintManager �̊T�v�̐����ł��B
	/// </summary>
	public class PrintManager
	{
		int NowRegistingNum = -1;										//(�o�^���Ɍ���)������PointQueue�̂ǂ̗v�f��o�^�����Ă邩�𒲂ׂ�ϐ�
		int NowPrintingNum = 0;											//(������Ɍ���)������PointQueue�̂ǂ̗v�f��������Ă��邩�𒲂ׂ�ϐ�(PrintPage�Ŏg�p)
		private static PrintManager printManager;
		private PrintDocument pd;
		private Hashtable BitmapTable;									//�r�b�g�}�b�v�t�@�C�����i�[���Ă���悤�Ȃ���(�L�[�̓t�@�C����)
		private ArrayList[] PointQueue;									//�������Ƃ��̍��W���i�[���Ă���҂��s��(�L�[�̓t�@�C����)3���ڂɈ��������̂́A�v�f2�ɓ����΂悢
		
		private ArrayList[] TextList;									// ������f�[�^ ������Ex���W�Ey���W�E�F�E�傫�� �̏�
		private const string fontname = "MeiryoKe_Gothic";				// �g�p����t�H���g

		public PrintManager()
		{
			BitmapTable = new Hashtable();
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			TextList = new ArrayList[1];
			TextList[0] = new ArrayList();
			pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(PrintPage);
			pd.EndPrint += new PrintEventHandler(EndPrint);
			muphic.PrintManager.printManager = this;
		}
		

		/// <summary>
		/// �r�b�g�}�b�v�t�@�C��������ł����Ԃɂ��đҋ@������(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�o�^����r�b�g�}�b�v�t�@�C���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void RegistBitmap(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			RegistBitmap(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// �r�b�g�}�b�v�t�@�C��������ł����Ԃɂ��đҋ@������(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�o�^����r�b�g�}�b�v�t�@�C���̃L�[</param>
		/// <param name="location">�o�^������W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ēo�^�����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void RegistBitmap(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);	//�o�^����t�@�C���̃t�@�C�������擾
			if(fname == null)														//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;																//�K�E"�������Ȃ�"
			}

			if(BitmapTable.ContainsKey(fname) == false)
			{
				Bitmap b = new Bitmap(fname);										//�t�@�C���������Ƃ�Bitmap�N���X���C���X�^���X��
				BitmapTable.Add(fname, b);											//BitmapTable�ɒǉ�
			}
			if(isCenter)															//�^�񒆂̏ꍇ�̏C��
			{
				Bitmap b = (Bitmap)BitmapTable[fname];
				location.X -= b.Width / 2;
				location.Y -= b.Height / 2;
			}
			PointQueue[NowRegistingNum].Add(fname);
			PointQueue[NowRegistingNum].Add(location);
		}

		/// <summary>
		/// PointQueue�ɓo�^����Ώۂ̗v�f��ύX����
		/// </summary>
		/// <param name="NewNum"></param>
		public void ChangeRegistingPage(int NewNum)
		{
			if(NowRegistingNum >= NewNum)								//�����A�V�����y�[�W���܂ł̋��e�͈͓��Ȃ�
			{
				this.NowPrintingNum = NewNum;							//�ύX��������
				return;													//�I��
			}
			this.NowRegistingNum = NewNum;								//�����A���e�͈͊O�Ȃ�
			ArrayList[] als = new ArrayList[NewNum+1];					//�V�������Ȃ���΂Ȃ�Ȃ�
			ArrayList[] alst= new ArrayList[NewNum+1];
			int i;

			for(i=0;i<PointQueue.Length;i++)							//�Â����e�͈͂̕���
			{															//���ʂɃR�s�[
				als[i] = PointQueue[i];
				alst[i]= TextList[i];
			}
			for(i=i;i<als.Length;i++)									//�Â����e�͈͊O�̕���
			{															//�V�����C���X�^���X��
				als[i] = new ArrayList();
				alst[i]= new ArrayList();
			}
			PointQueue = als;											//�V������������̂�PointQueue�ɑ��
			TextList = alst;
		}

		/// <summary>
		/// �y�[�W�̈�����J�n���郁�\�b�h
		/// </summary>
		public void BeginPrint()
		{
			pd.DefaultPageSettings.Landscape = true;
			//pd.PrinterSettings.LandscapeAngle = 90;
			pd.Print();
		}

		Rectangle RealField;														//����p�̃t�B�[���h�ŁA���4:3�ɏC����������
		SizeF div;																	//���܂ł̃t�B�[���h�ƐV�����t�B�[���h�̃T�C�Y��

		/// <summary>
		/// ChangeField�Ŏg�����܂ł̃t�B�[���h�ƐV�����t�B�[���h�̃T�C�Y������肷�郁�\�b�h
		/// </summary>
		/// <param name="Field">����p�̃t�B�[���h</param>
		private void DecideDiv(Rectangle Field)
		{
			if(Field.Width/4 > Field.Height/3)										//�c�̕����䂪�������̂�
			{																		//�c�ɍ��킹��
				Size RealSize = new Size(Field.Height/3*4, Field.Height);
				RealField = new Rectangle(Field.X+(Field.Width-RealSize.Width)/2, Field.Y, RealSize.Width, RealSize.Height);
			}
			else if(Field.Width/4 < Field.Height/3)									//���̕����䂪�������̂�
			{																		//���ɍ��킹��
				Size RealSize = new Size(Field.Width, Field.Width/4*3);
				RealField = new Rectangle(Field.X, Field.Y+(Field.Height-RealSize.Height)/2, RealSize.Width, RealSize.Height);
			}
			else
			{
				RealField = Field;
			}
			div = new SizeF(RealField.Width / 1024, RealField.Height / 768);	//���܂ł̃t�B�[���h�ƐV�����t�B�[���h�̃T�C�Y��
		}

		/// <summary>
		/// ���܂ł�1024�~768�̃t�B�[���h�������p�̃t�B�[���h�ւƐ؂�ւ��郁�\�b�h
		/// </summary>
		/// <param name="src">�؂�ւ���Ώۂ̎l�p�`</param>
		/// <param name="Field">����p�̃t�B�[���h</param>
		/// <returns></returns>
		private void ChangeField(ref Rectangle src)
		{
			//			Rectangle answer = new Rectangle(0, 0, 0, 0);
			//			answer.Width = (int)(src.Width / div.Width);
			//			answer.Height = (int)(src.Height / div.Height);
			//			answer.X = (int)(src.X / div.Width + RealField.X);
			//			answer.Y = (int)(src.Y / div.Height + RealField.Y);
			//			return answer;
			src.Width = (int)(src.Width / div.Width);
			src.Height = (int)(src.Height / div.Height);
			src.X = (int)(src.X / div.Width + RealField.X);
			src.Y = (int)(src.Y / div.Height + RealField.Y);
		}

		/// <summary>
		/// ���ۂɈ�������郁�\�b�h�B�������y�[�W�̕��������ꂪ�Ă΂��B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(this.NowPrintingNum == 0)																//���߂ĂȂ�
			{
				this.DecideDiv(e.MarginBounds);															//�t�B�[���h�̔�����肳����
			}
			Graphics g = e.Graphics;
			for(int i=0;i<PointQueue[this.NowPrintingNum].Count/2;i++)
			{
				String fname = (String)PointQueue[NowPrintingNum][i*2];									//PointQueue����t�@�C���������o��
				Point location = (Point)PointQueue[NowPrintingNum][i*2+1];								//PointQueue������W�����o��
				Bitmap b = (Bitmap)BitmapTable[fname];													//�t�@�C���������Ƃ�Bitmap�N���X�����o��
				Rectangle src = new Rectangle(0, 0, b.Width, b.Height);									//�r�b�g�}�b�v�t�@�C���̕��E�������w�肷��
				//src = this.ChangeField(src);															//����p�ɍ��W��ϊ�����
				Rectangle dest = new Rectangle(location.X, location.Y, b.Width, b.Height);				//�\��t����̍��W���w�肷��
				g.DrawImage(b, dest, src, GraphicsUnit.Pixel);											//�\��t��
			}
			
			// �e�L�X�g�����
			for(int i=0; i<this.TextList[this.NowPrintingNum].Count/5; i++)
			{
				// �e�f�[�^�̎��o��
				string str = (string)this.TextList[this.NowPrintingNum][i*5];
				int x = (int)this.TextList[this.NowPrintingNum][i*5+1];
				int y = (int)this.TextList[this.NowPrintingNum][i*5+2];
				Brush color = (Brush)this.TextList[this.NowPrintingNum][i*5+3];
				int size = (int)this.TextList[this.NowPrintingNum][i*5+4];
				
				// �t�H���g����
				System.Drawing.Font font = new System.Drawing.Font(fontname, size);
				
				// �\��t��
				g.DrawString(str, font, color, (float)x, (float)y, new StringFormat());
			}
			// �e�L�X�g���X�g�̃N���A
			TextList[this.NowPrintingNum].Clear();
			
			this.NowPrintingNum++;
			if(NowPrintingNum == PointQueue.Length)														//�����A���ׂĈ�����I�����Ȃ�
			{
				e.HasMorePages = false;																	//����I��
			}
			else
			{
				e.HasMorePages = true;
			}System.Diagnostics.Debug.WriteLine(e.PageBounds, "Page");
			System.Diagnostics.Debug.WriteLine(e.MarginBounds, "Margin");
		}

		/// <summary>
		/// ���ۂɈ�����J�n���ꂽ�Ƃ��ɂ�����C�x���g�B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void StartPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//this.NowPrintingNum = 0;												//�y�[�W�̏�����
		}

		/// <summary>
		/// ���ۂ̈�����I�����ꂽ�Ƃ��ɂ�����C�x���g�B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BitmapTable.Clear();
			for(int i=0;i<PointQueue.Length;i++)
			{
				PointQueue[i].Clear();
			}
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			this.NowPrintingNum = 0;												//�y�[�W�̏�����
			this.NowRegistingNum = -1;												//�o�^�Ώۃy�[�W�̕���������
		}
		
		/// <summary>
		/// �������e�L�X�g�����X�g�֓o�^����
		/// </summary>
		/// <param name="str"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		public void AddText(String str, int x, int y, System.Drawing.Brush color, int size)
		{
			TextList[this.NowRegistingNum].Add(str);
			TextList[this.NowRegistingNum].Add(x);
			TextList[this.NowRegistingNum].Add(y);
			TextList[this.NowRegistingNum].Add(color);
			TextList[this.NowRegistingNum].Add(size);
		}
		
		
		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// �o�^����Ώۂ̃y�[�W��ύX����
		/// </summary>
		/// <param name="NewPage"></param>
		static public void ChangePage(int NewPage)
		{
			muphic.PrintManager.printManager.ChangeRegistingPage(NewPage-1);			//�����ł̕ϐ��̓y�[�W���ŁA�������ŌĂ΂��̂͗v�f���Ȃ��Ƃɒ���
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Regist(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Regist(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Regist(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Regist(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void RegistCenter(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, true);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void RegistCenter(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, true);
		}

		
		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void RegistCenter(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void RegistCenter(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �y�[�W�̈�����J�n����
		/// </summary>
		public static void Print()
		{
			muphic.PrintManager.printManager.BeginPrint();
		}
		
		/// <summary>
		/// ������镶�����o�^���郁�\�b�h
		/// </summary>
		/// <param name="str">������镶����</param>
		/// <param name="x">������镶����̍���x���W</param>
		/// <param name="y">������镶����̍���y���W</param>
		static public void RegistString(String str, int x, int y)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, System.Drawing.Brushes.Black, 20);
		}
		
		/// <summary>
		/// ������镶�����o�^���郁�\�b�h
		/// </summary>
		/// <param name="str">������镶����</param>
		/// <param name="x">������镶����̍���x���W</param>
		/// <param name="y">������镶����̍���y���W</param>
		/// <param name="color">������镶����̐F</param>
		static public void RegistString(String str, int x, int y, System.Drawing.Brush color)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, color, 20);
		}
		
		/// <summary>
		/// ������镶�����o�^���郁�\�b�h
		/// </summary>
		/// <param name="str">������镶����</param>
		/// <param name="x">������镶����̍���x���W</param>
		/// <param name="y">������镶����̍���y���W</param>
		/// <param name="size">������镶����̑傫��(�w�肵�Ȃ����20)</param>
		static public void RegistString(String str, int x, int y, int size)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, System.Drawing.Brushes.Black, size);
		}
		
		/// <summary>
		/// ������镶�����o�^���郁�\�b�h
		/// </summary>
		/// <param name="str">������镶����</param>
		/// <param name="x">������镶����̍���x���W</param>
		/// <param name="y">������镶����̍���y���W</param>
		/// <param name="color">������镶����̐F</param>
		/// <param name="size">������镶����̑傫��(�w�肵�Ȃ����20)</param>
		static public void RegistString(String str, int x, int y, System.Drawing.Brush color, int size)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, color, size);
		}
	}
	*/
	#endregion
	
	#region ver.3.0.0 ����͈͕ύX�@�\�ǉ�
	
	/// <summary>
	/// PrintManager �̊T�v�̐����ł��B
	/// </summary>
	public class PrintManager
	{
		int NowRegistingNum = -1;										//(�o�^���Ɍ���)������PointQueue�̂ǂ̗v�f��o�^�����Ă邩�𒲂ׂ�ϐ�
		int NowPrintingNum = 0;											//(������Ɍ���)������PointQueue�̂ǂ̗v�f��������Ă��邩�𒲂ׂ�ϐ�(PrintPage�Ŏg�p)
		private static PrintManager printManager;
		private PrintDocument pd;
		private Hashtable BitmapTable;									//�r�b�g�}�b�v�t�@�C�����i�[���Ă���悤�Ȃ���(�L�[�̓t�@�C����)
		private ArrayList[] PointQueue;									//�������Ƃ��̍��W���i�[���Ă���҂��s��(�L�[�̓t�@�C����)3���ڂɈ��������̂́A�v�f2�ɓ����΂悢
		
		Rectangle RealField;											//����p�̃t�B�[���h�ŁA���4:3�ɏC����������
		Rectangle VirtualField;											//���܂ł̃t�B�[���h�A������w�肷�鉼�z�̈�

		private ArrayList[] TextList;									// ������f�[�^ ������Ex���W�Ey���W�E�F�E�傫�� �̏�
		private const string fontname = "MeiryoKe_Gothic";				// �g�p����t�H���g
		
		public bool isExpand;											//�p���̑傫���ɍ��킹�Ċg�傷�邩�ǂ����̃t���O
		public bool isNotUseMarginBounds;								//�v�����^�ɐݒ肳��Ă���]�����g�p���Ȃ����ǂ���
		public PrintManager()
		{
			BitmapTable = new Hashtable();
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			TextList = new ArrayList[1];
			TextList[0] = new ArrayList();
			pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(PrintPage);
			pd.EndPrint += new PrintEventHandler(EndPrint);
			pd.PrintController = new System.Drawing.Printing.StandardPrintController();
			muphic.PrintManager.printManager = this;
		}
		
		/// <summary>
		/// �r�b�g�}�b�v�t�@�C��������ł����Ԃɂ��đҋ@������(�����ō��W���������Ă����)
		/// </summary>
		/// <param name="ClassName">�o�^����r�b�g�}�b�v�t�@�C���̃L�[</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ĕ`�悷��e�N�X�`����ς���)</param>
		/// <param name="isCenter">PointManager�ɓ����Ă�����W���摜�̒����ɂ��邩�A�摜�̍���ɂ��邩</param>
		public void RegistBitmap(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassName���L�[�ɍ��W�f�[�^������
			if(r == Rectangle.Empty)										//�N���X���o�^����Ă��Ȃ��ꍇ
			{
				return;														//�K�E"�����ł��Ȃ�"
			}
			RegistBitmap(ClassName, r.Location, state, isCenter);			//�I�[�o�[���[�h�̂����Е����Ă�
		}

		/// <summary>
		/// �r�b�g�}�b�v�t�@�C��������ł����Ԃɂ��đҋ@������(�����ō��W���w�肷��)
		/// </summary>
		/// <param name="ClassName">�o�^����r�b�g�}�b�v�t�@�C���̃L�[</param>
		/// <param name="location">�o�^������W</param>
		/// <param name="state">���̃N���X�̏��(��Ԃɂ���ēo�^�����ς���)</param>
		/// <param name="isCenter">location�̍��W����ʂ̒����ɂ��邩�A��ʂ̍���ɂ��邩</param>
		public void RegistBitmap(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);	//�o�^����t�@�C���̃t�@�C�������擾
			if(fname == null)														//�t�@�C�������o�^����Ă��Ȃ��ꍇ
			{
				return;																//�K�E"�������Ȃ�"
			}

			if(BitmapTable.ContainsKey(fname) == false)
			{
				Bitmap b = new Bitmap(fname);										//�t�@�C���������Ƃ�Bitmap�N���X���C���X�^���X��
				BitmapTable.Add(fname, b);											//BitmapTable�ɒǉ�
			}
			if(isCenter)															//�^�񒆂̏ꍇ�̏C��
			{
				Bitmap b = (Bitmap)BitmapTable[fname];
				location.X -= b.Width / 2;
				location.Y -= b.Height / 2;
			}
			PointQueue[NowRegistingNum].Add(fname);
			PointQueue[NowRegistingNum].Add(location);
		}
		
		/// <summary>
		/// PointQueue�ɓo�^����Ώۂ̗v�f��ύX����
		/// </summary>
		/// <param name="NewNum"></param>
		public void ChangeRegistingPage(int NewNum)
		{
			if(NowRegistingNum >= NewNum)								//�����A�V�����y�[�W���܂ł̋��e�͈͓��Ȃ�
			{
				this.NowPrintingNum = NewNum;							//�ύX��������
				return;													//�I��
			}
			this.NowRegistingNum = NewNum;								//�����A���e�͈͊O�Ȃ�
			ArrayList[] als = new ArrayList[NewNum+1];					//�V�������Ȃ���΂Ȃ�Ȃ�
			ArrayList[] alst= new ArrayList[NewNum+1];
			int i;

			for(i=0;i<PointQueue.Length;i++)							//�Â����e�͈͂̕���
			{															//���ʂɃR�s�[
				als[i] = PointQueue[i];
				alst[i]= TextList[i];
			}
			for(i=i;i<als.Length;i++)									//�Â����e�͈͊O�̕���
			{															//�V�����C���X�^���X��
				als[i] = new ArrayList();
				alst[i]= new ArrayList();
			}
			PointQueue = als;											//�V������������̂�PointQueue�ɑ��
			TextList = alst;
		}

		/// <summary>
		/// ChangeField�Ŏg�����܂ł̃t�B�[���h�ƐV�����t�B�[���h�̃T�C�Y������肷�郁�\�b�h
		/// </summary>
		/// <param name="Field">����p�̃t�B�[���h</param>
		private void DecideDiv(Rectangle Field)
		{
			RectangleF RealF;
			if(Field.Width/VirtualField.Width > Field.Height/VirtualField.Height)	//�c�̕����䂪�������̂�
			{																		//�c�ɍ��킹��
				SizeF RealSize = new SizeF((float)Field.Height/VirtualField.Height*VirtualField.Width, Field.Height);
				RealF = new RectangleF(Field.X+(Field.Width-RealSize.Width)/2, Field.Y, RealSize.Width, RealSize.Height);
			}
			else if(Field.Width/4 < Field.Height/3)									//���̕����䂪�������̂�
			{																		//���ɍ��킹��
				SizeF RealSize = new SizeF(Field.Width, (float)Field.Width/VirtualField.Width*VirtualField.Height);
				RealF = new RectangleF(Field.X, Field.Y+(Field.Height-RealSize.Height)/2, RealSize.Width, RealSize.Height);
			}
			else
			{
				RealF = Field;
			}
			RealField = new Rectangle((int)RealF.X, (int)RealF.Y, (int)RealF.Width, (int)RealF.Height);
		}

		/// <summary>
		/// ���܂ł̉��z�t�B�[���h�������p�̃t�B�[���h�ւƐ؂�ւ��郁�\�b�h
		/// </summary>
		/// <param name="src">�؂�ւ���Ώۂ̎l�p�`</param>
		/// <param name="Field">����p�̃t�B�[���h</param>
		/// <returns></returns>
		private Rectangle ChangeField(Rectangle r)
		{
			float divX = (float)RealField.Width / VirtualField.Width;
			float divY = (float)RealField.Height / VirtualField.Height;
			r.X = r.X - VirtualField.X;
			r.X = (int)(r.X * divX);
			r.Width = (int)(r.Width * divX);
			r.X = r.X + RealField.X;
			r.Y = r.Y - VirtualField.Y;
			r.Y = (int)(r.Y * divY);
			r.Height = (int)(r.Height * divY);
			r.Y = r.Y + RealField.Y;
			//			src.Width = (int)(src.Width / div.Width);
			//			src.Height = (int)(src.Height / div.Height);
			//			src.X = (int)(src.X / div.Width + RealField.X);
			//			src.Y = (int)(src.Y / div.Height + RealField.Y);
			return r;
		}


		/// <summary>
		/// �y�[�W�̈�����J�n���郁�\�b�h
		/// </summary>
		/// <param name="VirtualField"></param>
		/// <param name="isExpand">�����p���ɍ��킹�Ċg�傷�邩�ǂ���</param>
		public void BeginPrint(Rectangle VirtualField, bool isExpand)
		{
			this.VirtualField = VirtualField;
			pd.DefaultPageSettings.Landscape = true;
			//pd.PrinterSettings.LandscapeAngle = 90;
			this.isExpand = isExpand;
			this.isNotUseMarginBounds = false;
			pd.PrinterSettings.PrintToFile = true;
			pd.Print();
		}

		/// <summary>
		/// �y�[�W�̈�����J�n���郁�\�b�h
		/// </summary>
		/// <param name="VirtualField"></param>
		/// <param name="isExpand">�����p���ɍ��킹�Ċg�傷�邩�ǂ���</param>
		/// <param name="isNotUseMarginBounds">�]�����Ԃɓ���Ȃ����ǂ���</param>
		public void BeginPrint(Rectangle VirtualField, bool isExpand, bool isNotUseMarginBounds)
		{
			this.VirtualField = VirtualField;
			pd.DefaultPageSettings.Landscape = true;
			//pd.PrinterSettings.LandscapeAngle = 90;
			this.isExpand = isExpand;
			this.isNotUseMarginBounds = isNotUseMarginBounds;
			pd.PrinterSettings.PrintToFile = true;
			pd.Print();
		}

		/// <summary>
		/// ���ۂɈ�������郁�\�b�h�B�������y�[�W�̕��������ꂪ�Ă΂��B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(this.NowPrintingNum == 0)																//���߂ĂȂ�
			{
				if(this.isExpand)
				{
					if(this.isNotUseMarginBounds)
					{
						this.DecideDiv(new Rectangle(e.PageBounds.X+20, e.PageBounds.Y+20, e.PageBounds.Width-40, e.PageBounds.Height-40));
					}
					else
					{
						this.DecideDiv(e.MarginBounds);								//�t�B�[���h�̔�����肳����
					}
				}
				else
				{
					this.DecideDiv(new Rectangle((e.PageBounds.Width-VirtualField.Width)/2, (e.PageBounds.Height-VirtualField.Height)/2,
						VirtualField.Width, VirtualField.Height));										//�f�t�H���g�̃t�B�[���h�̂܂܁A�^�񒆂Ɉ��������
				}
			}
			Graphics g = e.Graphics;
			String fname;
			Point location;
			Bitmap b;
			Rectangle src, dest;
			for(int i=0;i<PointQueue[this.NowPrintingNum].Count/2;i++)
			{
				fname = (String)PointQueue[NowPrintingNum][i*2];									//PointQueue����t�@�C���������o��
				location = (Point)PointQueue[NowPrintingNum][i*2+1];								//PointQueue������W�����o��
				b = (Bitmap)BitmapTable[fname];														//�t�@�C���������Ƃ�Bitmap�N���X�����o��
				src = new Rectangle(0, 0, b.Width, b.Height);								//�r�b�g�}�b�v�t�@�C���̕��E�������w�肷��
				dest = new Rectangle(location.X, location.Y, b.Width, b.Height);				//�\��t����̍��W���w�肷��
				dest = this.ChangeField(dest);																//����p�ɍ��W��ϊ�����
				g.DrawImage(b, dest, src, GraphicsUnit.Pixel);											//�\��t��
			}
			
			// �e�L�X�g�����
			for(int i=0; i<this.TextList[this.NowPrintingNum].Count/5; i++)
			{
				// �e�f�[�^�̎��o��
				string str = (string)this.TextList[this.NowPrintingNum][i*5];
				int x = (int)this.TextList[this.NowPrintingNum][i*5+1];
				int y = (int)this.TextList[this.NowPrintingNum][i*5+2];
				Rectangle r = new Rectangle(x, y, 0, 0);
				r = this.ChangeField(r);
				Brush color = (Brush)this.TextList[this.NowPrintingNum][i*5+3];
				int size = (int)this.TextList[this.NowPrintingNum][i*5+4];
				
				// �t�H���g����
				System.Drawing.Font font = new System.Drawing.Font(fontname, size);
				
				// �\��t��
				g.DrawString(str, font, color, (float)r.X, (float)r.Y, new StringFormat());
			}
			// �e�L�X�g���X�g�̃N���A
			TextList[this.NowPrintingNum].Clear();
			
			this.NowPrintingNum++;
			if(NowPrintingNum == PointQueue.Length)														//�����A���ׂĈ�����I�����Ȃ�
			{
				e.HasMorePages = false;																	//����I��
			}
			else
			{
				e.HasMorePages = true;
			}System.Diagnostics.Debug.WriteLine(e.PageBounds, "Page");
			System.Diagnostics.Debug.WriteLine(e.MarginBounds, "Margin");
		}

		/// <summary>
		/// ���ۂɈ�����J�n���ꂽ�Ƃ��ɂ�����C�x���g�B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void StartPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//this.NowPrintingNum = 0;												//�y�[�W�̏�����
		}

		/// <summary>
		/// ���ۂ̈�����I�����ꂽ�Ƃ��ɂ�����C�x���g�B
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BitmapTable.Clear();
			for(int i=0;i<PointQueue.Length;i++)
			{
				PointQueue[i].Clear();
			}
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			this.NowPrintingNum = 0;												//�y�[�W�̏�����
			this.NowRegistingNum = -1;												//�o�^�Ώۃy�[�W�̕���������
		}
		
		/// <summary>
		/// �������e�L�X�g�����X�g�֓o�^����
		/// </summary>
		/// <param name="str"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		public void AddText(String str, int x, int y, System.Drawing.Brush color, int size)
		{
			TextList[this.NowRegistingNum].Add(str);
			TextList[this.NowRegistingNum].Add(x);
			TextList[this.NowRegistingNum].Add(y);
			TextList[this.NowRegistingNum].Add(color);
			TextList[this.NowRegistingNum].Add(size);
		}
		
		
		///////////////////////////////////////////////////////////////////////
		//�O����Ă΂�郁�\�b�h�Ƃ��̃I�[�o�[���[�h����
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// �o�^����Ώۂ̃y�[�W��ύX����
		/// </summary>
		/// <param name="NewPage"></param>
		static public void ChangePage(int NewPage)
		{
			muphic.PrintManager.printManager.ChangeRegistingPage(NewPage-1);		//�����ł̕ϐ��̓y�[�W���ŁA�������ŌĂ΂��̂͗v�f���Ȃ��Ƃɒ���
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void Regist(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Regist(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void Regist(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void Regist(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,state=0,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		static public void RegistCenter(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, true);
		}

		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,���W�͂����瑤�Ō���)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void RegistCenter(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, true);
		}

		
		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S,state=0)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		static public void RegistCenter(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// �ǂ��Ƀr�b�g�}�b�v�t�@�C�����������̂���o�^����(���W�͒��S)
		/// </summary>
		/// <param name="ClassName">�T������L�[</param>
		/// <param name="x">�e�N�X�`����`�悷��x���W</param>
		/// <param name="y">�e�N�X�`����`�悷��y���W</param>
		/// <param name="state">���݂̃N���X�̏��(����ɂ���ĕ`�悷��摜���ς��)</param>
		static public void RegistCenter(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// �y�[�W�̈�����J�n����(�f�t�H���g�Ȃ̂ŗ̈��1024�~768)
		/// </summary>
		/// <param name="isExpand">�����p���ɂ��킹�Ċg�傷�邩�ǂ���</param>
		public static void Print(bool isExpand)
		{
			muphic.PrintManager.printManager.BeginPrint(new Rectangle(0, 0, 1024, 768), isExpand);
		}

		/// <summary>
		/// �y�[�W�̈�����J�n����
		/// </summary>
		/// <param name="VirtualField">���܂܂łǂ̗̈�ō��W���w�肵�Ă�����</param>
		/// <param name="isExpand">�����p���ɂ��킹�Ċg�傷�邩�ǂ���</param>
		public static void Print(Rectangle VirtualField, bool isExpand)
		{
			muphic.PrintManager.printManager.BeginPrint(VirtualField, isExpand);
		}

		/// <summary>
		/// �y�[�W�̈�����J�n����
		/// </summary>
		/// <param name="VirtualField">���܂܂łǂ̗̈�ō��W���w�肵�Ă�����</param>
		/// <param name="isExpand">�����p���ɍ��킹�Ċg�傷�邩�ǂ���</param>
		/// <param name="isNotUseMarginBounds">�f�t�H���g�̌��Ԃ��g��Ȃ����ǂ���(false�Ȃ�S�̈���g��)</param>
		public static void Print(Rectangle VirtualField, bool isExpand, bool isNotUseMarginBounds)
		{
			muphic.PrintManager.printManager.BeginPrint(VirtualField, isExpand, isNotUseMarginBounds);
		}
		
		/// <summary>
		/// ������镶�����o�^���郁�\�b�h
		/// </summary>
		/// <param name="str">������镶����</param>
		/// <param name="x">������镶����̍���x���W</param>
		/// <param name="y">������镶����̍���y���W</param>
		static public void RegistString(String str, int x, int y)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, System.Drawing.Brushes.Black, 20);
		}
		
		/// <summary>
		/// ������镶�����o�^���郁�\�b�h
		/// </summary>
		/// <param name="str">������镶����</param>
		/// <param name="x">������镶����̍���x���W</param>
		/// <param name="y">������镶����̍���y���W</param>
		/// <param name="color">������镶����̐F</param>
		static public void RegistString(String str, int x, int y, System.Drawing.Brush color)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, color, 20);
		}
		
		/// <summary>
		/// ������镶�����o�^���郁�\�b�h
		/// </summary>
		/// <param name="str">������镶����</param>
		/// <param name="x">������镶����̍���x���W</param>
		/// <param name="y">������镶����̍���y���W</param>
		/// <param name="size">������镶����̑傫��(�w�肵�Ȃ����20)</param>
		static public void RegistString(String str, int x, int y, int size)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, System.Drawing.Brushes.Black, size);
		}
		
		/// <summary>
		/// ������镶�����o�^���郁�\�b�h
		/// </summary>
		/// <param name="str">������镶����</param>
		/// <param name="x">������镶����̍���x���W</param>
		/// <param name="y">������镶����̍���y���W</param>
		/// <param name="color">������镶����̐F</param>
		/// <param name="size">������镶����̑傫��(�w�肵�Ȃ����20)</param>
		static public void RegistString(String str, int x, int y, System.Drawing.Brush color, int size)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, color, size);
		}
	}
	
	#endregion
}
