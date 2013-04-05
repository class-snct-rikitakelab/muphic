//using System;
//using System.Collections;
//using System.IO;
//using System.Text;
//using System.Windows.Forms;
//using System.Drawing;
//
//
//namespace muphic.Link.Dialog.Select
//{
//	public class LinkFileReader
//	{
//		ArrayList AnimalList;
//		public String Name;
//		public int Tempo;
//		StreamReader sr = null;
//		public Point[][] pattern = new Point[10][];
//
//		public LinkFileReader(ArrayList AnimalList)
//		{
//			this.AnimalList = AnimalList;
//		}
//		
//		
//		public bool Read(string filename)
//		{
//			// 薙ぎ払え！
//			AnimalList.Clear();
//
//			try
//			{
//				System.Console.WriteLine("muphicスコアデータファイル " + filename + " を読み込み");
//				// 読み込みバッファ設定 filenameはパスも拡張子も入ってるからそのままでおｋ
//				sr = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
//			}
//			catch(FileNotFoundException)
//			{
//				// そんなことありえんと思うけどね
//				MessageBox.Show("奇術！ファイルが見つからない");
//				return false;
//			}
//			
//			// 読み込んだデータを格納する
//			string[] data;
//
//			Name = sr.ReadLine();
//			Tempo = int.Parse(sr.ReadLine());
//			
//
//			// ファイルの最後まで読み込む
//			int count = 0;
//			while( (data = this.ReadLine()) != null )
//			{
//				for (int i = 0; i < data.Length-1; i+=2)
//				{
//					int code = int.Parse(data[i+1]);
//					int place = 0;
//					switch (data[i].ToCharArray()[0])
//					{
//						case 'A':
//							place = 0;
//							break;
//						case 'B':
//							place = 1;
//							break;
//						case 'C':
//							place = 2;
//							break;
//						case 'D':
//							place = 3;
//							break;
//						case 'E':
//							place = 4;
//							break;
//						case 'F':
//							place = 5;
//							break;
//						case 'G':
//							place = 6;
//							break;
//						case 'H':
//							place = 7;
//							break;
//						default:
//							place = 0;
//							break;
//					}
//				
//					Animal a = new Animal(place+count*8, code+1, "Sheep");
//
//					// Animalリストに追加
//					AnimalList.Add(a);
//				}
//				
//				//時間ないので突貫で作ります
//				
//				pattern[count] = new Point[data.Length/2];
//				for (int i = 0; i < data.Length/2; i++)
//				{
//					switch (data[i*2].ToCharArray(0, 1)[0])
//					{
//						case 'A':
//							pattern[count][i].X = -3;
//							break;
//						case 'B':
//							pattern[count][i].X = -2;
//							break;
//						case 'C':
//							pattern[count][i].X = -1;
//							break;
//						case 'D':
//							pattern[count][i].X = 0;
//							break;
//						case 'E':
//							pattern[count][i].X = 1;
//							break;
//						case 'F':
//							pattern[count][i].X = 2;
//							break;
//						case 'G':
//							pattern[count][i].X = 3;
//							break;
//						case 'H':
//							pattern[count][i].X = 4;
//							break;
//						default:
//							pattern[count][i].X = 0;
//							break;
//					}
//
//					switch (data[i*2+1].ToCharArray(0, 1)[0])
//					{
//						case '0':
//							pattern[count][i].Y = 0; //25 + 181;
//							break;
//						case '1':
//							pattern[count][i].Y = 1; //75 + 181;
//							break;
//						case '2':
//							pattern[count][i].Y = 2; //125 + 181;
//							break;
//						case '3':
//							pattern[count][i].Y = 3; //175 + 181;
//							break;
//						case '4':
//							pattern[count][i].Y = 4; //225 + 181;
//							break;
//						case '5':
//							pattern[count][i].Y = 5; //275 + 181;
//							break;
//						case '6':
//							pattern[count][i].Y = 6; //325 + 181;
//							break;
//						case '7':
//							pattern[count][i].Y = 7; //375 + 181;
//							break;
//						default:
//							pattern[count][i].Y = 4; //225 + 181;
//							break;
//					}
//				}
//
//				count++;
//			}
//			
//			// クローズ
//			sr.Close();
//
//			//patternの重複チェック、ダミー解答の生成等
//			Random rand = new System.Random(); //乱数生成関クラスインスタンス化
//
////			//同様パターンが存在する　→　完全にランダムなパターンを生成して片方を置き換える
////			for (int i = 0; i < count-1; i++)
////			{
////				for (int j = i+1; j < count; j++)
////				{
////					if (pattern[i].Length == pattern[j].Length)
////					{
////						bool flag_k = true;
////						for (int k = 0; k < pattern[i].Length; k++)
////						{
////							bool flag_l = false;
////							for (int l = 0; l < pattern[j].Length; l++)
////							{
////								if (pattern[i][k].X == pattern[j][l].X && pattern[i][k].Y == pattern[j][l].Y)
////								{
////									flag_l = true;
////								}
////							}
////							if (!flag_l)
////							{
////								flag_k = false;
////								break;
////							}
////						}
////
////						if (flag_k)
////						{
////							int pat_num = rand.Next(2, 6);
////							pattern[j] = new Point[pat_num];
////							for (int k = 0; k < pat_num; k++)
////							{
////								pattern[j][k].X = rand.Next(7) - 3;
////								pattern[j][k].Y = rand.Next(7);
////							}
////						}
////					}
////				}
////			}
//
//			//空いている場所があればそこに類似パターンを詰め込む
//			//方法が方法だから同じパターンが出てくる可能性あり
//			//またランダムパターンを生成しない場合もあるので半々ぐらいで生成する
//			for (int i = count; i < 10; i++)
//			{
//
//				if (rand.Next(4) > -1)
//				{
//					int temp = rand.Next(count);
//					//pattern[temp].CopyTo(pattern[i], 0);
//					pattern[i] = (Point[])pattern[temp].Clone();
//					int ch_ud = rand.Next(2);
//					int ch_lr = rand.Next(2);
//					int ch_all = rand.Next(2);
//					int xp, xm, yp, ym;
//					xp = xm = yp = ym = 1;
//
//					for (int j = 0; j < pattern[temp].Length; j++)
//					{
//						#region old
////						if (ch_ud != 0) 
////						{
////							pattern[i][j].Y++;
////							if (pattern[i][j].Y > 7) pattern[i][j].Y = 0;
////						}
////						else 
////						{
////							pattern[i][j].Y--;
////							if (pattern[i][j].Y < 0) pattern[i][j].Y = 7;
////						}
////
////						if (ch_lr != 0) 
////						{
////							pattern[i][j].X++;
////							if (pattern[i][j].X > 4) pattern[i][j].X = -3;
////						}
////						else 
////						{
////							pattern[i][j].X--;
////							if (pattern[i][j].X < -3) pattern[i][j].X = 4;
////						}
//						#endregion
//						switch (ch_all)
//						{
//							case 0:
//								pattern[i][j].Y += yp;
//								if (pattern[i][j].Y > 7) pattern[i][j].Y = 0 + (yp-1);
//								break;
//							case 1:
//								pattern[i][j].Y -= ym;
//								if (pattern[i][j].Y < 0) pattern[i][j].Y = 7 - (ym-1);
//								break;
//							default:
//								break;
//						}
//					}
//
//					switch (ch_all)
//					{
//						case 0:
//							yp++;
//							break;
//						case 1:
//							ym++;
//							break;
//						default:
//							break;
//					}
//				}
//				else
//				{
//					int pat_num = rand.Next(2, 6);
//					pattern[i] = new Point[pat_num];
//					for (int k = 0; k < pat_num; k++)
//					{
//						pattern[i][k].X = rand.Next(7) - 3;
//						pattern[i][k].Y = rand.Next(7);
//					}
//				}
//			}
//
//			//同様パターンが存在する　→　完全にランダムなパターンを生成して片方を置き換える
//			for (int i = 0; i < 10-1; i++)
//			{
//				for (int j = i+1; j < 10; j++)
//				{
//					if (pattern[i].Length == pattern[j].Length)
//					{
//						bool flag_k = true;
//						for (int k = 0; k < pattern[i].Length; k++)
//						{
//							bool flag_l = false;
//							for (int l = 0; l < pattern[j].Length; l++)
//							{
//								if (pattern[i][k].X == pattern[j][l].X && pattern[i][k].Y == pattern[j][l].Y)
//								{
//									flag_l = true;
//								}
//							}
//							if (!flag_l)
//							{
//								flag_k = false;
//								break;
//							}
//						}
//
//						if (flag_k)
//						{
//							int pat_num = rand.Next(2, 6);
//							pattern[j] = new Point[pat_num];
//							for (int k = 0; k < pat_num; k++)
//							{
//								pattern[j][k].X = rand.Next(7) - 3;
//								pattern[j][k].Y = rand.Next(7);
//							}
//						}
//					}
//				}
//			}
//
//			if (!muphic.Common.TutorialStatus.getIsTutorial()) //並べ替え。ただしチュートリアル実行中の場合、選択肢パターンを固定
//			{
//				//パターンの並べ替え　15回ぐらいでー
//				for (int i = 0; i < 15; i++)
//				{
//					int n = rand.Next(10);
//					int m = rand.Next(10);
//					Point[] pat_temp;
//					if (n != m)
//					{
//						pat_temp = (Point[])pattern[n].Clone();
//						pattern[n] = (Point[])pattern[m].Clone();
//						pattern[m] = (Point[])pat_temp.Clone();
//					}
//				}
//			}
//			else
//			{
//				//1番と2番のみ入れ替え、後は放置
//				Point[] pat_temp;
//				pat_temp = (Point[])pattern[0].Clone();
//				pattern[0] = (Point[])pattern[1].Clone();
//				pattern[1] = (Point[])pat_temp.Clone();
//			}
//
//			return true;
//
//		}
//
//		
//		/// <summary>
//		/// CSV形式で1行読み込むメソッド
//		/// </summary>
//		/// <returns></returns>
//		private String[] ReadLine()
//		{
//			// 一行読み込み
//			String s = sr.ReadLine();
//			
//			// 読み込まなかったらnullを返す
//			if(s == null) return null;
//			
//			// ' 'で区切って文字列配列で返す
//			return s.Split(new char[] {' '});
//		}
//	}
//}