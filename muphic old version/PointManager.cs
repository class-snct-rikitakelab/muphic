using System;
using System.Collections;
using System.Drawing;

namespace muphic
{
	#region Ver.1
/*	/// <summary>
	/// PointManager
	/// </summary>
	public class PointManager
	{
		static private PointManager pointManager;					//�^�l
		private Hashtable PointTable;								//���W���i�[����n�b�V���e�[�u��							

		public PointManager()
		{
			PointTable = new Hashtable();
			muphic.PointManager.pointManager = this;
		}

		/// <summary>
		/// ���ۂɍ��W��o�^����
		/// </summary>
		/// <param name="ClassName">�n�b�V���e�[�u���ɓo�^����L�[(�N���X��)</param>
		/// <param name="r">�o�^������W�E���E�������i�[����Ă���l�p�`</param>
		public void SetPoint(String ClassName, Rectangle r)
		{
			if(!PointTable.Contains(ClassName))							//�������Ɋi�[����Ă��Ȃ��Ȃ�
			{
				PointTable.Add(ClassName, r);							//�ǉ�����
			}
		}

		/// <summary>
		/// �^����ꂽ�L�[�����ɊY��������W��Ԃ��B�Ȃ������ꍇ��Rectangle.Empty��Ԃ�
		/// </summary>
		/// <param name="ClassName">�T���L�[</param>
		/// <returns>�Y���������W</returns>
		public Rectangle GetPoint(String ClassName)
		{
			if(!PointTable.Contains(ClassName))							//�Ȃ������ꍇ
			{
				return Rectangle.Empty;									//"�����Ȃ�"��Ԃ�
			}
			return (Rectangle)PointTable[ClassName];					//�������ꍇ�͕��ʂɕԂ�
		}



		/// <summary>
		/// ���W��PointManager�ɓo�^���郁�\�b�h
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="x">�o�^������W�̉�����</param>
		/// <param name="y">�o�^������W�̏c����</param>
		/// <param name="width">�o�^���镝</param>
		/// <param name="height">�o�^���鍂��</param>
		public static void Set(String ClassName, int x, int y, int width, int height)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, new Rectangle(x, y, width, height));
		}

		/// <summary>
		/// ���W��PointManager�ɓo�^���郁�\�b�h
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="location">�o�^������W</param>
		/// <param name="size">�o�^���镝�E����</param>
		public static void Set(String ClassName, Point location, Size size)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, new Rectangle(location, size));
		}

		/// <summary>
		/// ���W��PointManager�ɓo�^���郁�\�b�h
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="r">�o�^������W�E���E����</param>
		public static void Set(String ClassName, Rectangle r)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, r);
		}

		/// <summary>
		/// �^����ꂽ�L�[�����ɊY��������W��T���o�����\�b�h�B�Ȃ��ꍇ��Rectangle.Empty��Ԃ��B
		/// </summary>
		/// <param name="ClassName">�T���L�[</param>
		/// <returns>�Y���������W</returns>
		public static Rectangle Get(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName);
		}


	}*/
	#endregion

	#region Ver.2
	/*
	/// <summary>
	/// PointManager version2 Delete�֌W���ǉ�����Ă��
	///           �@ ���ƁAstate���ɍ��W���ς��悤�ɂȂ��Ă��(���W���z��g���悤�ɂȂ�����)
	/// </summary>
	public class PointManager
	{
		static private PointManager pointManager;					//�^�l
		private Hashtable PointTable;								//���W���i�[����n�b�V���e�[�u��							

		public PointManager()
		{
			PointTable = new Hashtable();
			muphic.PointManager.pointManager = this;
		}

		/// <summary>
		/// ���ۂɍ��W��o�^����
		/// </summary>
		/// <param name="ClassName">�n�b�V���e�[�u���ɓo�^����L�[(�N���X��)</param>
		/// <param name="r">�o�^������W�E���E�������i�[����Ă���l�p�`</param>
		public void SetPoint(String ClassName, Rectangle[] r)
		{
			if(!PointTable.Contains(ClassName))							//�������Ɋi�[����Ă��Ȃ��Ȃ�
			{
				PointTable.Add(ClassName, r);							//�ǉ�����
			}
		}

		/// <summary>
		/// �^����ꂽ�L�[�����ɊY��������W��z��̂܂ܕԂ��B�Ȃ������ꍇ��null��Ԃ�
		/// </summary>
		/// <param name="ClassName">�T���L�[</param>
		/// <returns>�Y���������W�B</returns>
		public Rectangle[] GetPointAll(String ClassName)
		{
			if(!PointTable.Contains(ClassName))							//�Ȃ������ꍇ
			{
				return null;											//null��Ԃ�
			}
			return (Rectangle[])PointTable[ClassName];					//�������ꍇ�͕��ʂɕԂ�
		}

		/// <summary>
		/// �^����ꂽ�L�[��state�����ɊY��������W��Ԃ��B�Ȃ������ꍇ��Rectangle.Empty��Ԃ�
		/// </summary>
		/// <param name="ClassName">�T���L�[</param>
		/// <param name="state">���݂̏��</param>
		/// <returns>�Y���������W</returns>
		public Rectangle GetPoint(String ClassName, int state)
		{
			if(!PointTable.Contains(ClassName))							//�Ȃ������ꍇ
			{
				return Rectangle.Empty;									//"�����Ȃ�"��Ԃ�
			}
			return ((Rectangle[])PointTable[ClassName])[state];			//�������ꍇ�͕��ʂɕԂ�
		}

		/// <summary>
		/// �^����ꂽ�L�[�ɊY��������W���폜����B
		/// </summary>
		/// <param name="ClassName">�^����ꂽ�L�[</param>
		public void DeletePoint(String ClassName)
		{
			PointTable.Remove(ClassName);								//�Y��������̂��폜
		}

		/// <summary>
		/// ���W��PointManager�ɓo�^���郁�\�b�h
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="rs">�o�^������W����</param>
		public static void Set(String ClassName, Rectangle[] rs)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, rs);
		}

		/// <summary>
		/// �^����ꂽ�L�[�����ɊY��������W�B��T���o�����\�b�h�B�Ȃ��ꍇ��null��Ԃ��B
		/// </summary>
		/// <param name="ClassName">�T���L�[</param>
		/// <returns>�Y���������W</returns>
		public static Rectangle[] GetAll(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPointAll(ClassName);
		}

		/// <summary>
		/// �^����ꂽ�L�[�ƌ��ɊY��������W��T���o�����\�b�h�B�Ȃ��ꍇ��Rectangle.Empty��Ԃ�(state=0�Ƃ��Ă���Ă���)
		/// </summary>
		/// <param name="ClassName">�^����ꂽ�L�[</param>
		/// <returns>�Y���������W(�Ȃ��ꍇ��Rectangle.Empty)</returns>
		public static Rectangle Get(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName, 0);
		}

		
		/// <summary>
		/// �^����ꂽ�L�[�ƌ��ɊY��������W��T���o�����\�b�h�B�Ȃ��ꍇ��Rectangle.Empty��Ԃ�
		/// </summary>
		/// <param name="ClassName">�^����ꂽ�L�[</param>
		/// <param name="state">���݂̏��</param>
		/// <returns>�Y���������W(�Ȃ��ꍇ��Rectangle.Empty)</returns>
		public static Rectangle Get(String ClassName, int state)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName, state);
		}

		/// <summary>
		/// �^����ꂽ�L�[�ɊY��������W���폜���郁�\�b�h�B
		/// </summary>
		/// <param name="ClassName">�폜����L�[</param>
		public static void Delete(String ClassName)
		{
			muphic.PointManager.pointManager.DeletePoint(ClassName);
		}
	}*/
	#endregion

	#region Ver.3
	/// <summary>
	/// PointManager version2 Delete�֌W���ǉ�����Ă��
	///           �@ ���ƁAstate���ɍ��W���ς��悤�ɂȂ��Ă��(���W���z��g���悤�ɂȂ�����)
	/// PointManager version3 ���ɓo�^����Ă����Ԃ�Set���\�b�h���ĂԂƁA���ۂł͂Ȃ��㏑���ɂ����B
	/// </summary>
	public class PointManager
	{
		static private PointManager pointManager;					//�^�l
		private Hashtable PointTable;								//���W���i�[����n�b�V���e�[�u��							

		public PointManager()
		{
			PointTable = new Hashtable();
			muphic.PointManager.pointManager = this;
		}

		/// <summary>
		/// ���ۂɍ��W��o�^����
		/// </summary>
		/// <param name="ClassName">�n�b�V���e�[�u���ɓo�^����L�[(�N���X��)</param>
		/// <param name="r">�o�^������W�E���E�������i�[����Ă���l�p�`</param>
		public void SetPoint(String ClassName, Rectangle[] r)
		{
			if(PointTable.Contains(ClassName))							//�������Ɋi�[����Ă���Ȃ�
			{
				PointTable.Remove(ClassName);							//���łɂ�����̂��폜���Ă���
			}
			PointTable.Add(ClassName, r);							//�ǉ�����
		}

		/// <summary>
		/// �^����ꂽ�L�[�����ɊY��������W��z��̂܂ܕԂ��B�Ȃ������ꍇ��null��Ԃ�
		/// </summary>
		/// <param name="ClassName">�T���L�[</param>
		/// <returns>�Y���������W�B</returns>
		public Rectangle[] GetPointAll(String ClassName)
		{
			if(!PointTable.Contains(ClassName))							//�Ȃ������ꍇ
			{
				return null;											//null��Ԃ�
			}
			return (Rectangle[])PointTable[ClassName];					//�������ꍇ�͕��ʂɕԂ�
		}

		/// <summary>
		/// �^����ꂽ�L�[��state�����ɊY��������W��Ԃ��B�Ȃ������ꍇ��Rectangle.Empty��Ԃ�
		/// </summary>
		/// <param name="ClassName">�T���L�[</param>
		/// <param name="state">���݂̏��</param>
		/// <returns>�Y���������W</returns>
		public Rectangle GetPoint(String ClassName, int state)
		{
			if(!PointTable.Contains(ClassName))							//�Ȃ������ꍇ
			{
				return Rectangle.Empty;									//"�����Ȃ�"��Ԃ�
			}
			return ((Rectangle[])PointTable[ClassName])[state];			//�������ꍇ�͕��ʂɕԂ�
		}

		/// <summary>
		/// �^����ꂽ�L�[�ɊY��������W���폜����B
		/// </summary>
		/// <param name="ClassName">�^����ꂽ�L�[</param>
		public void DeletePoint(String ClassName)
		{
			PointTable.Remove(ClassName);								//�Y��������̂��폜
		}

		/// <summary>
		/// ���W��PointManager�ɓo�^���郁�\�b�h
		/// </summary>
		/// <param name="ClassName">�o�^����L�[(�N���X��)</param>
		/// <param name="rs">�o�^������W����</param>
		public static void Set(String ClassName, Rectangle[] rs)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, rs);
		}

		/// <summary>
		/// �^����ꂽ�L�[�����ɊY��������W�B��T���o�����\�b�h�B�Ȃ��ꍇ��null��Ԃ��B
		/// </summary>
		/// <param name="ClassName">�T���L�[</param>
		/// <returns>�Y���������W</returns>
		public static Rectangle[] GetAll(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPointAll(ClassName);
		}

		/// <summary>
		/// �^����ꂽ�L�[�ƌ��ɊY��������W��T���o�����\�b�h�B�Ȃ��ꍇ��Rectangle.Empty��Ԃ�(state=0�Ƃ��Ă���Ă���)
		/// </summary>
		/// <param name="ClassName">�^����ꂽ�L�[</param>
		/// <returns>�Y���������W(�Ȃ��ꍇ��Rectangle.Empty)</returns>
		public static Rectangle Get(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName, 0);
		}

		
		/// <summary>
		/// �^����ꂽ�L�[�ƌ��ɊY��������W��T���o�����\�b�h�B�Ȃ��ꍇ��Rectangle.Empty��Ԃ�
		/// </summary>
		/// <param name="ClassName">�^����ꂽ�L�[</param>
		/// <param name="state">���݂̏��</param>
		/// <returns>�Y���������W(�Ȃ��ꍇ��Rectangle.Empty)</returns>
		public static Rectangle Get(String ClassName, int state)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName, state);
		}

		/// <summary>
		/// �^����ꂽ�L�[�ɊY��������W���폜���郁�\�b�h�B
		/// </summary>
		/// <param name="ClassName">�폜����L�[</param>
		public static void Delete(String ClassName)
		{
			muphic.PointManager.pointManager.DeletePoint(ClassName);
		}
	}
	#endregion
}
