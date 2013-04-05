using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace muphic.ScoreScr
{
	public class ScoreFileReader
	{
		ArrayList AnimalList;
		StreamReader sr = null;

		public ScoreFileReader(ArrayList AnimalList)
		{
			this.AnimalList = AnimalList;
		}
		
		#region ver.SETO
		
		public bool Read(String Name)
		{
			AnimalList.Clear();

			try
			{
				sr = new StreamReader("ScoreData\\" + Name + ".msd");
			}
			catch(FileNotFoundException e)
			{
				MessageBox.Show("ファイルが見つかりません");
				return false;
			}

			String[] s;
			while((s=ReadCSV()) != null)
			{
				Animal a = new Animal(int.Parse(s[1]), int.Parse(s[2]), s[0]);
				AnimalList.Add(a);
			}
			sr.Close();
			
			return true;
		}
		
		#endregion
		
		#region ver.Gackt こっちを使う
		
		public bool ReadMSDFile(string filename)
		{
			// 薙ぎ払え！
			AnimalList.Clear();

			try
			{
				System.Console.WriteLine("muphicスコアデータファイル " + filename + " を読み込み");
				// 読み込みバッファ設定 filenameはパスも拡張子も入ってるからそのままでおｋ
				sr = new StreamReader(filename);
			}
			catch(FileNotFoundException)
			{
				// そんなことありえんと思うけどね
				MessageBox.Show("奇術！ファイルが見つからない");
				return false;
			}
			
			// 読み込んだデータを格納する
			string[] data;
			
			// ファイルの最後まで読み込む
			while( (data = this.ReadCSV()) != null )
			{
				// 読み込んだデータをAnimalクラス形式データに入れる
				// 動物の位置(data[1]), 動物の音階(data[2]), 動物の種類(data[0])
				Animal a = new Animal(int.Parse(data[1]), int.Parse(data[2]), data[0]);

				// Animalリストに追加
				AnimalList.Add(a);
			}
			
			// クローズ
			sr.Close();

			return true;

		}
		
		#endregion
		
		/// <summary>
		/// CSV形式で1行読み込むメソッド
		/// </summary>
		/// <returns></returns>
		private String[] ReadCSV()
		{
			// 一行読み込み
			String s = sr.ReadLine();
			
			// 読み込まなかったらnullを返す
			if(s == null) return null;
			
			// '.'で区切って文字列配列で返す
			return s.Split(new char[] {','});
		}
	}
}