using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.ScoreScr
{
	public class ScoreFileWriter
	{
		ArrayList AnimalList;
		StreamWriter sw = null;
		public ScoreFileWriter(ArrayList AnimalList)
		{
			this.AnimalList = AnimalList;
		}
		
		#region ver.SETO
		
		public bool Write(String Name)
		{
			try
			{
				sw = new StreamWriter("StoryData\\" + Name + ".txt");
			}
			catch(FileNotFoundException e)
			{
				MessageBox.Show("ファイルが見つかりません");
				return false;
			}

			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				WriteCSV(a.ToString(), a.place.ToString(), a.code.ToString());
			}
			sw.Close();
			return true;
		}
		
		#endregion
		
		#region ver.Gackt こっちを使う
		
		/// <summary>
		/// ファイル書き込みメソッド
		/// </summary>
		/// <param name="name">ファイル名になる文字列</param>
		/// <returns>書き込み成功か否か</returns>
		public bool WriteMSDFile(string name)
		{
			// ファイル名を決定
			string filename = DecideFilename(name);

			// ファイル名取得に失敗した場合 書き込み失敗
			if(filename == null) return false;
			
			try
			{
				System.Console.WriteLine("muphicスコアデータファイル " + filename + " へ書き出し");
				// 書き込みバッファの設定
				sw = new StreamWriter(filename, false, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException)
			{
				// ファイルオープンに失敗した場合 書き込み失敗
				MessageBox.Show("奇術！ファイルが開けない");
				return false;
			}
			
			// 書き込み
			for(int i=0; i<AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				WriteCSV(a.ToString(), a.place.ToString(), a.code.ToString());
			}

			// クローズ
			sw.Close();

			// 書き込み成功
			return true;
		}
		
		
		/// <summary>
		/// 与えられた文字列からファイル名を生成するメソッド
		/// </summary>
		/// <param name="filename">文字列</param>
		/// <returns>ファイル名</returns>
		public static string DecideFilename(string name)
		{
			for(int i=0; i<37564; i++)
			{
				// 0から順番に"文字列_番号.msd"が存在するかチェックしていく
				string filename = "ScoreData\\" + name + "_" + i.ToString() + ".msd";

				// 存在しないファイル名があればそれに決定
				if( !File.Exists(filename) ) return filename;
			}

			return null;
		}
		
		#endregion
		
		private void WriteCSV(String s1, String s2)
		{
			WriteCSV(new String[] {s1, s2});
		}

		private void WriteCSV(String s1, String s2, String s3)
		{
			WriteCSV(new String[] {s1, s2, s3});
		}

		private void WriteCSV(String[] strs)
		{
			String s = "";
			for(int i=0;i<strs.Length;i++)
			{
				if(s != "")
				{
					s += ",";
				}
				s += strs[i];
			}
			sw.WriteLine(s);
		}
	}
}