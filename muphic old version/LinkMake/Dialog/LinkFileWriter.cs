using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

//亀谷さんの頂きましたー
namespace muphic.LinkMake.Dialog
{
	public class LinkFileWriter
	{
		ArrayList AnimalList;
		int Tempo;
		int Level;
		int Filenum;
		String Name;
		StreamWriter sw = null;

		public LinkFileWriter(ArrayList AnimalList, int tempo, String name, int filenum, int level)
		{
			this.AnimalList = AnimalList;
			this.Tempo = tempo;
			this.Name = name;
			this.Filenum = filenum;
			this.Level = level;
		}
		
		/// <summary>
		/// ファイル書き込みメソッド
		/// </summary>
		/// <param name="name">ファイル名になる文字列</param>
		/// <returns>書き込み成功か否か</returns>
		public bool Write()
		{
			// ファイル名を決定
			string filename = DecideFilename(Filenum);
			String[] code = {"A","B","C","D","E","F","G","H"};

			// ファイル名取得に失敗した場合 書き込み失敗
			if(filename == null) return false;
			
			try
			{
				System.Console.WriteLine("muphicMDLファイル " + filename + " へ書き出し");
				// 書き込みバッファの設定
				sw = new StreamWriter(filename, false, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException e)
			{
				// ファイルオープンに失敗した場合 書き込み失敗
				MessageBox.Show("奇術！ファイルが開けない");
				return false;
			}
			
			int maxplace = 0;
			String str = "";
			// 書き込み
			sw.WriteLine(Name);
			sw.WriteLine(Level);
			sw.WriteLine(Tempo);

			//最大値取得
			for(int i=0; i<AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				if (maxplace < a.place)
				{
					maxplace = a.place;
				}
			}
			
			int temp = 8;
			//1小節ごとに書き込み
			for (int i = 0; i <= maxplace/8; i++)
			{
				for (int j = 0; j < AnimalList.Count; j++)
				{
					Animal a = (Animal)AnimalList[j];
					if (a.place < temp && a.place > temp-9)
					{
						str += code[a.place%8] + " " + (a.code-1) + " ";
					}
				}
				temp += 8;
				sw.WriteLine(str);
				str = "";
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
		public static string DecideFilename(int filenum)
		{
			if (filenum < 10)
			{
				for(int i=10; i<37564; i++)
				{
					// 0から順番に"文字列_番号.msd"が存在するかチェックしていく
					string filename = "LinkData\\QuesPatt_" + i.ToString() + ".mdl";

					// 存在しないファイル名があればそれに決定
					if( !File.Exists(filename) ) return filename;
				}
			}
			else
			{
				string filename = "LinkData\\QuesPatt_" + filenum.ToString() + ".mdl";
				return filename;
			}

			return null;
		}
		

	}
}