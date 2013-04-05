using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.One
{
	public class ScoreFileWriter
	{
		ArrayList AnimalList;
		int tempo;
		StreamWriter sw = null;
		
		public const string SaveFileDirectory = "ScoreData\\";		// データを保存するディレクトリ
		public const string VoiceFileDirectory = "";				// 音声ファイルが保存されているディレクトリ 保存先ではないことに注意
		
		// 保存する際ファイルの上書きをするかどうか
		public const bool OverrideFlag = false;
		
		public ScoreFileWriter(ArrayList AnimalList, int tempo)
		{
			this.AnimalList = AnimalList;
			this.tempo = tempo;
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
			// ファイル名を決定 今は上書きしない
			string filename = DecideFilename(name, ScoreFileWriter.OverrideFlag, ".msd");
			
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
			
			// 以下書き込み
			
			// まずテンポの情報を書き込む
			sw.WriteLine(tempo);
			
			// 動物リストを書き込む
			for(int i=0; i<AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				WriteCSV(a.ToString(), a.place.ToString(), a.code.ToString());
			}

			// クローズ
			sw.Close();
			
			// 音声ファイルの保存
			for(int i=0; i<this.AnimalList.Count; i++)
			{
				// 動物リスト内に音声があれば音声ファイルの保存を行う
				Animal animal = (Animal)this.AnimalList[i];
				if(animal.AnimalName == "Voice")
				{
					this.SaveVoiceFile(name);
					break;
				}
			}
			
			// 書き込み成功
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
			string directoryname = ScoreFileWriter.DecideFilename(name, false, "");
			
			// 音声ファイル保存先のディレクトリを作成
			Directory.CreateDirectory(directoryname);
			
			// ファイルをコピー
			for(int i=1; i<=8; i++)
			{
				string filename = "Voice" + i.ToString() + ".wav";
				File.Copy(filename, directoryname + "\\" + filename, true);
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
				string filename = ScoreFileWriter.SaveFileDirectory + name + "_" + i.ToString() + extention;

				// 存在しないファイル名があればそれに決定
				if( !overrideflag || !File.Exists(filename) ) return filename;
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