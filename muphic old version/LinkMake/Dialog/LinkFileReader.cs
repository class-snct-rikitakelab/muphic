using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.LinkMake.Dialog.Select
{
	public class LinkFileReader
	{
		ArrayList AnimalList;
		String Name;
		public int Tempo;
		StreamReader sr = null;

		public LinkFileReader(ArrayList AnimalList)
		{
			this.AnimalList = AnimalList;
		}
		
		
		public bool Read(string filename)
		{
			// 薙ぎ払え！
			AnimalList.Clear();

			try
			{
				System.Console.WriteLine("muphicスコアデータファイル " + filename + " を読み込み");
				// 読み込みバッファ設定 filenameはパスも拡張子も入ってるからそのままでおｋ
				sr = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException e)
			{
				// そんなことありえんと思うけどね
				MessageBox.Show("奇術！ファイルが見つからない");
				return false;
			}
			
			// 読み込んだデータを格納する
			string[] data;

			Name = sr.ReadLine();
			sr.ReadLine();//レベルは飛ばす
			Tempo = int.Parse(sr.ReadLine());
			
			// ファイルの最後まで読み込む
			int count = 0;
			while( (data = this.ReadLine()) != null )
			{
				for (int i = 0; i < data.Length-1; i+=2)
				{
					int code = int.Parse(data[i+1]);
					int place = 0;
					switch (data[i].ToCharArray()[0])
					{
						case 'A':
							place = 0;
							break;
						case 'B':
							place = 1;
							break;
						case 'C':
							place = 2;
							break;
						case 'D':
							place = 3;
							break;
						case 'E':
							place = 4;
							break;
						case 'F':
							place = 5;
							break;
						case 'G':
							place = 6;
							break;
						case 'H':
							place = 7;
							break;
						default:
							place = 0;
							break;
					}
				
					Animal a = new Animal(place+count*8, code+1, "Sheep");

					// Animalリストに追加
					AnimalList.Add(a);
				}
				count++;
			}
			
			// クローズ
			sr.Close();

			return true;

		}

		
		/// <summary>
		/// CSV形式で1行読み込むメソッド
		/// </summary>
		/// <returns></returns>
		private String[] ReadLine()
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