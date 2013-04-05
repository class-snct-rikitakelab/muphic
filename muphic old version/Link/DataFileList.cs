using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace muphic.Link
{
	public class DataIndex
	{
		private int num;
		private String title;
		private int level;
		public int easy;
		public int normal;
		public int hard;

		public int Num { get {return num;} set {num = value;} }
		public String Title { get {return title;} set {title = value;} }
		//public int Level { get {return level;} set {level = value;} }
		public int Level
		{
			get {return this.level;}
			set
			{
				this.level = value;

				if (level <= 10) normal = 1;
				else if (level <= 20) normal = 2;
				else if (level <= 30) normal = 3;
				else if (level <= 40) normal = 4;
				else normal = 5;

				if (level+5 <= 10) hard = 1;
				else if (level+5 <= 20) hard = 2;
				else if (level+5 <= 30) hard = 3;
				else if (level+5 <= 40) hard = 4;
				else hard = 5;

				if (level-5 <= 10) easy = 1;
				else if (level-5 <= 20) easy = 2;
				else if (level-5 <= 30) easy = 3;
				else if (level-5 <= 40) easy = 4;
				else easy = 5;

				level = normal;
			}
		}
	}

	/// <summary>
	/// DataFileList �̊T�v�̐����ł��B
	/// </summary>
	public class DataFileList
	{

		public ArrayList Index;

		public DataFileList()
		{
			Index = new ArrayList();
			ReadIndex();
		}

		public bool ReadIndex()
		{
			StreamReader sr;
			try
			{
				System.Console.WriteLine("LinkData_Index�t�@�C����ǂݍ���");
				
				sr = new StreamReader("LinkData\\QuesPatt.mdi", Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException)
			{
				// ����Ȃ��Ƃ��肦��Ǝv�����ǂ�
				//MessageBox.Show("('A`)");
				return false;
			}

			String[] data;
			while( (data = this.ReadLine(sr)) != null )
			{
				DataIndex di = new DataIndex();
				di.Num = int.Parse(data[0]);
				di.Title = data[1];
				di.Level = int.Parse(data[2]);
				Index.Add(di);
				
			}

			sr.Close();
			return true;
		}

		/// <summary>
		/// 1�s���X�y�[�X�ŋ�؂��ēǂݍ��ރ��\�b�h
		/// </summary>
		/// <returns></returns>
		private String[] ReadLine(StreamReader sr)
		{
			// ��s�ǂݍ���
			String s = sr.ReadLine();
			
			// �ǂݍ��܂Ȃ�������null��Ԃ�
			if(s == null) return null;
			
			// ' '�ŋ�؂��ĕ�����z��ŕԂ�
			return s.Split(new char[] {' '});
		}
	}
}