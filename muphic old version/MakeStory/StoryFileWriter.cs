using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace muphic.MakeStory
{
	public class StoryFileWriter
	{
		PictStory PictureStory;
		StreamWriter sw = null;
		public const string SaveFileDirectory = "StoryData\\";		// データを保存するディレクトリ

		public StoryFileWriter(PictStory PictureStory)
		{
			this.PictureStory = PictureStory;
		}

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

			WriteTitle(PictureStory.Title);						//タイトルの描画
			for(int i=0;i<10;i++)
			{
				Slide slide = PictureStory.Slide[i];
				WriteCSV(i.ToString(), "Slide");				//i枚目の部品の保存開始
				WriteCSV("haikei", PictureStory.Slide[i].haikei.ToString());//背景の保存
				WriteCSV("tempo", PictureStory.Slide[i].tempo.ToString());	//テンポの保存
				WriteCSV("sentence", PictureStory.Slide[i].Sentence);	//文章の保存
				for(int j=0;j<slide.ObjList.Count;j++)
				{
					Obj o = (Obj)slide.ObjList[j];
					WriteCSV(o.ToString(), o.X.ToString(), o.Y.ToString());
				}
				WriteCSV(i.ToString(), "Animal");
				for(int j=0;j<slide.AnimalList.Count;j++)
				{
					Animal a = (Animal)slide.AnimalList[j];
					WriteCSV(a.ToString(), a.place.ToString(), a.code.ToString());
				}
			}
			sw.Close();

			// 音声ファイルの保存
			for(int i=0; i<10; i++)
			{
				Slide slide = PictureStory.Slide[i];
				for(int j=0;j<slide.AnimalList.Count;j++)
				{
					// 動物リスト内に音声があれば音声ファイルの保存を行う
					Animal animal = (Animal)slide.AnimalList[j];
					if(animal.AnimalName == "Voice")
					{
						this.SaveVoiceFile(Name);
						break;
					}
				}
			}	
			return true;
		}

		/// <summary>
		/// 音声ファイルを保存するメソッド
		/// </summary>
		/// <param name="name">保存するディレクトリ名</param>
		public void SaveVoiceFile(string name)
		{
			// 音声ファイルを保存するディレクトリ名を決める
			// 保存ファイル名と同じディレクトリ名にするため、ファイル名決定のメソッドを流用
			//string directoryname = StoryFileWriter.DecideFilename(name, false, "");
			string directoryname = "StoryData\\" + name;
			
			if(!Directory.Exists(directoryname))
			{
				// 音声ファイル保存先のディレクトリを作成
				Directory.CreateDirectory(directoryname);
			}
			
			// ファイルをコピー
			for(int i=1; i<=8; i++)
			{
				string filename = "Voice" + i.ToString() + ".wav";
				File.Copy(filename, directoryname + "\\" + filename,  true);
			}
		}

		/// <summary>
		/// 与えられた文字列からファイル名を生成するメソッド
		/// </summary>
		/// <param name="filename">文字列</param>
		/// <param name="overrideflag">上書きするかどうか</param>
		/// <returns>ファイル名</returns>
		public static string DecideFilename(string name, bool overrideflag, string extention)
		{
			for(int i=0; i<37564; i++)
			{
				// 0から順番に"文字列_番号.msd"が存在するかチェックしていく
				string filename = StoryFileWriter.SaveFileDirectory + name + "_" + i.ToString() + extention;

				// 存在しないファイル名があればそれに決定
				if( !overrideflag || !File.Exists(filename) ) return filename;
			}

			return null;
		}

		private void WriteTitle(String title)
		{
			String s = "Title:" + title;
			sw.WriteLine(s);
		}

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