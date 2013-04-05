using System;
using System.Drawing;

namespace muphic.Common
{
	/// <summary>
	/// CommonTools �̊T�v�̐����ł��B
	/// </summary>
	public class CommonTools
	{
		public CommonTools()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		/// <summary>
		/// ��������w�肳�ꂽ�������̏ꍇ�ɁA�����񂹂̂悤�ȏ�ԂɕύX�����郁�\�b�h
		/// </summary>
		/// <param name="str">�ύX���镶����</param>
		/// <param name="length">������</param>
		public static String StringCenter(String str, int length)
		{
			String newStr = str;
			if (str != null)
			{
				for(int i=0;i<(length-str.Length)/2;i++)
				{
					newStr = newStr.Insert(0, "�@");
					newStr = newStr.Insert(newStr.Length, "�@");
				}
				if(((length-str.Length)%2) == 1)
				{
					newStr = newStr.Insert(0, " ");
					newStr = newStr.Insert(newStr.Length, " ");
				}
			}
			return newStr;
		}

		/// <summary>
		/// �w�肳�ꂽ���W���A�w�肳�ꂽ��`�̈�̒��ɓ����Ă��邩�ǂ���
		/// ���ׂ郁�\�b�h
		/// </summary>
		/// <param name="p">���W</param>
		/// <param name="r">��`�̈�</param>
		/// <returns></returns>
		public static bool inRect(Point p, Rectangle r)
		{
			if(r.Left <= p.X && p.X <= r.Right)
			{
				if(r.Top <= p.Y && p.Y <= r.Bottom)
				{
					return true;
				}
			}
			return false;
		}
	}
}
