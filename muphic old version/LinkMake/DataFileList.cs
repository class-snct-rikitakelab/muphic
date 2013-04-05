using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace muphic.LinkMake
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
		public int Level { get {return level;} set {level = value;} }
	}

	/// <summary>
	/// DataFileList の概要の説明です。
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
				System.Console.WriteLine("LinkData_Indexファイルを読み込み");
				
				sr = new StreamReader("LinkData\\QuesPatt.mdi", Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException)
			{
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
		/// 1行をスペースで区切って読み込むメソッド
		/// </summary>
		/// <returns></returns>
		private String[] ReadLine(StreamReader sr)
		{
			// 一行読み込み
			String s = sr.ReadLine();
			
			// 読み込まなかったらnullを返す
			if(s == null) return null;
			
			// ' 'で区切って文字列配列で返す
			return s.Split(new char[] {' '});
		}
	}
}