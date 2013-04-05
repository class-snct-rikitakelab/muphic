using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace muphic.MakeStory
{
	public class StoryFileReader
	{
		PictStory PictureStory;
		StreamReader sr = null;
		public StoryFileReader(PictStory PictureStory)
		{
			this.PictureStory = PictureStory;
		}

		public bool Read(String Name)
		{
			PictureStory.Init();							//PictureStoryの初期化
			try
			{
				sr = new StreamReader("StoryData\\" + Name + ".txt");
			}
			catch(FileNotFoundException e)
			{
				MessageBox.Show("ファイルが見つかりません");
				return false;
			}

			String[] s;
			int NowPage=-1;
			bool NowMode=false;			//true=部品, false=楽譜
			while((s=ReadCSV()) != null)
			{
				if(s.Length == 1)
				{
					PictureStory.Title = GetTitle(s[0]);
				}
				if(s.Length == 2)	//2なら，ページ数の指定とモードの指定．
				{
					NowPage = int.Parse(s[0]);
					NowMode = (s[1]=="Slide") ? true : false;
					if(NowMode)											//もしSlideなら，そのあとに，背景とテンポの設定が格納されている
					{
						s = ReadCSV();
						Slide slide = PictureStory.Slide[NowPage];
						slide.haikei = new Obj( 182, 267, s[1]);			//背景の読み込み
						s = ReadCSV();
						slide.tempo = int.Parse(s[1]);					//テンポの読み込み
						s = ReadCSV();
						if(s[0] != "sentence")
						{
							MessageBox.Show("ファイルデータが旧式です");
						}
						slide.Sentence = s[1];

						PictureStory.Slide[NowPage] = slide;
					}
				}
				else if(s.Length == 3)
				{
					Slide slide = PictureStory.Slide[NowPage];
					if(NowMode)							//部品の追加
					{
						Obj o = new Obj(int.Parse(s[1]), int.Parse(s[2]), s[0]);
						//o.ObjInit(int.Parse(s[1]), int.Parse(s[2]), s[0]);
						slide.ObjList.Add(o);
					}
					else								//動物の追加
					{
						Animal a = new Animal(int.Parse(s[1]), int.Parse(s[2]), s[0]);
						slide.AnimalList.Add(a);
					}
					PictureStory.Slide[NowPage] = slide;
				}
			}
			sr.Close();

			// 音声ファイルの読み込み
			this.LoadVoiceFile(Name);
			
			return true;
		}

		public void LoadVoiceFile(string name)
		{
			// 読み込みファイル名が格納されたディレクトリ名を得る
			string directoryname = "StoryData";
			
			// 読み込みファイル名から拡張子を抜いた文字列を得る これが音声ファイルが保存されているディレクトリ名になる
			string voicedirectoryname = Path.GetFileNameWithoutExtension(directoryname + "\\" + name);
			
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

		/// <summary>
		/// Title:タイトル名
		/// となっている行からタイトル名だけを取り除くメソッド
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private String GetTitle(String s)
		{
			char[] separater = {':'};
			return s.Split(separater)[1];
		}

		private String[] ReadCSV()
		{
			String s = sr.ReadLine();
			char[] separater = {','};
			if(s == null)
			{
				return null;
			}
			return s.Split(separater);
		}
	}
}