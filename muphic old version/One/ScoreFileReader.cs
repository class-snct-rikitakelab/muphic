using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace muphic.One
{
	public class ScoreFileReader
	{
		ArrayList AnimalList;
		int tempo;
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
			
			// 以下読み込み

			// テンポ情報の読み込み
			this.tempo = int.Parse(sr.ReadLine());
						
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
			
			// 音声ファイルの読み込み
			this.LoadVoiceFile(filename);
			
			return true;
		}
		
		/// <summary>
		/// テンポをゲッツ
		/// </summary>
		/// <returns></returns>
		public int getTempo()
		{
			return this.tempo;
		}
		
		
		public void LoadVoiceFile(string pass)
		{
			// 読み込みファイル名が格納されたディレクトリ名を得る
			string directoryname = Path.GetDirectoryName(pass);
			
			// 読み込みファイル名から拡張子を抜いた文字列を得る これが音声ファイルが保存されているディレクトリ名になる
			string voicedirectoryname = Path.GetFileNameWithoutExtension(Path.GetFileName(pass));
			
			// 音声ファイルが保存されているパス付きディレクトリ名を生成
			voicedirectoryname = directoryname + "\\" + voicedirectoryname;
			
			// ディレクトリが存在しなければ音声ファイルはロードしない
			if( !Directory.Exists(voicedirectoryname) ) return;
			
			// 音声ファイルをコピー
			for(int i=1; i<=8; i++)
			{
				string filename = "Voice" + i.ToString() + ".wav";
				SoundManager.Delete(filename);
				File.Copy(voicedirectoryname + "\\" + filename, filename, true);
			}
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