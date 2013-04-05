using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace muphic.Link.Dialog.Select
{
	public class LinkFileReader
	{
		ArrayList AnimalList;
		public String Name;
		public int Tempo;
		public int Level;
		public int Level_select;
		StreamReader sr = null;
		public Point[][] pattern = new Point[10][];

		public LinkFileReader(ArrayList AnimalList, int level_select)
		{
			this.AnimalList = AnimalList;
			this.Level_select = level_select + 1;
		}
		
		
		public int Read(string filename)
		{
			AnimalList.Clear();

			try
			{
				System.Console.WriteLine("muphicスコアデータファイル " + filename + " を読み込み");
				// 読み込みバッファ設定 filenameはパスも拡張子も入ってるからそのままでおｋ
				sr = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException)
			{
				// そんなことありえんと思うけどね
				MessageBox.Show("奇術！ファイルが見つからない");
				return -1;
			}
			
			// 読み込んだデータを格納する
			string[] data;

			Name = sr.ReadLine();
			Level = int.Parse(sr.ReadLine());
			Tempo = int.Parse(sr.ReadLine());
			

			// ファイルの最後まで読み込む
			int count = 0;
			int noncount = 0;
			while( (data = this.ReadLine()) != null )
			{
				if (data[0].Equals(""))
				{
					noncount++;
					//continue;
				}
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
				
					//Animal a = new Animal(place+count*8, code+1, "Sheep");
					place += (count+noncount)*8;
					code++;

					// Animalリストに追加
					//AnimalList.Add(a);
					int j;
					for (j = 0; j < AnimalList.Count; j++)
					{
						Animal a = (Animal)AnimalList[j];
						if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
						{
							return -1;
						}
						if(a.place == place && a.code > code)							//設定する位置と同じでも、
						{																//音階が設定する位置より遠いものが存在したら、
							break;														//遠いものが現れたということはすでにOK
						}
						if(a.place > place)												//設定する位置より遠いものが存在した
						{																//AnimalListは昇順にソートしてあるから、
							break;														//遠いものが現れたということはすでにOK
						}
					}
					Animal newAnimal = new Animal(place, code, "Sheep");					//Animalオブジェクトをインスタンス化
					AnimalList.Insert(j, newAnimal);									//遠いようになったところに割り込む
					//こうすることによって昇順が保たれる
				}
				
				if (!data[0].Equals(""))
				{
					//時間ないので突貫で作ります
					if (data.Length > 0)
					{
						pattern[count] = new Point[data.Length/2];
						for (int i = 0; i < data.Length/2; i++)
						{
							switch (data[i*2].ToCharArray(0, 1)[0])
							{
								case 'A':
									pattern[count][i].X = -3;
									break;
								case 'B':
									pattern[count][i].X = -2;
									break;
								case 'C':
									pattern[count][i].X = -1;
									break;
								case 'D':
									pattern[count][i].X = 0;
									break;
								case 'E':
									pattern[count][i].X = 1;
									break;
								case 'F':
									pattern[count][i].X = 2;
									break;
								case 'G':
									pattern[count][i].X = 3;
									break;
								case 'H':
									pattern[count][i].X = 4;
									break;
								default:
									pattern[count][i].X = 0;
									break;
							}

							switch (data[i*2+1].ToCharArray(0, 1)[0])
							{
								case '0':
									pattern[count][i].Y = 0; //25 + 181;
									break;
								case '1':
									pattern[count][i].Y = 1; //75 + 181;
									break;
								case '2':
									pattern[count][i].Y = 2; //125 + 181;
									break;
								case '3':
									pattern[count][i].Y = 3; //175 + 181;
									break;
								case '4':
									pattern[count][i].Y = 4; //225 + 181;
									break;
								case '5':
									pattern[count][i].Y = 5; //275 + 181;
									break;
								case '6':
									pattern[count][i].Y = 6; //325 + 181;
									break;
								case '7':
									pattern[count][i].Y = 7; //375 + 181;
									break;
								default:
									pattern[count][i].Y = 4; //225 + 181;
									break;
							}
						}

						count++;
					}
				}
			}
			
			// クローズ
			sr.Close();	

			//小節数が3以下の場合はパターン数が制限されている
			int num = 10;
				
			if (count+noncount == 3) num = 7;
			if (count+noncount == 2) num = 5;
			if (count+noncount == 1) num = 3;

			Random rand = new System.Random();

			switch (Level_select)
			{
				case 0:
					AutoGenEasy(count, noncount, rand, num);
					break;
				case 1:
					AutoGenNormal(count, noncount, rand, num);
					break;
				case 2:
					AutoGenHard(count, noncount, rand, num);
					break;
				default:
					AutoGenNormal(count, noncount, rand, num);
					break;
			}

			if (!muphic.Common.TutorialStatus.getIsTutorial()) //並べ替え。ただしチュートリアル実行中の場合、選択肢パターンを固定
			{
				//パターンの並べ替え　15回ぐらいでー

				for (int i = 0; i < 15; i++)
				{
					
					int n = rand.Next(num);
					int m = rand.Next(num);
					Point[] pat_temp;
					if (n != m)
					{
						pat_temp = (Point[])pattern[n].Clone();
						pattern[n] = (Point[])pattern[m].Clone();
						pattern[m] = (Point[])pat_temp.Clone();
					}
				}
			}
			else
			{
				//1番と2番のみ入れ替え、後は放置
				Point[] pat_temp;
				pat_temp = (Point[])pattern[0].Clone();
				pattern[0] = (Point[])pattern[1].Clone();
				pattern[1] = (Point[])pat_temp.Clone();
			}

			return count+noncount;

		}


		/// <summary>
		/// パターン自動生成(通常版)
		/// </summary>
		/// <returns></returns>
		private void AutoGenNormal(int count, int noncount, Random rand, int num)
		{
			//patternの重複チェック、ダミー解答の生成等

			bool[] marker = new Boolean[10];
			for (int i = 0; i < count; i++) marker[i] = true;
			for (int i = count; i < 10; i++) marker[i] = false;

			//同様パターンが存在する　→　完全にランダムなパターンを生成して片方を置き換える
			for (int i = 0; i < count-1; i++)
			{
				for (int j = i+1; j < count; j++)
				{
					if (pattern[i].Length == pattern[j].Length)
					{
						bool flag_k = true;
						for (int k = 0; k < pattern[i].Length; k++)
						{
							bool flag_l = false;
							for (int l = 0; l < pattern[j].Length; l++)
							{
								if (pattern[i][k].X == pattern[j][l].X && pattern[i][k].Y == pattern[j][l].Y)
								{
									flag_l = true;
								}
							}
							if (!flag_l)
							{
								flag_k = false;
								break;
							}
						}

						if (flag_k)
						{
							
							marker[j] = false;
							
						}
					}
				}
			}

			//空いている場所があればそこに類似パターンを詰め込む
			//方法が方法だから同じパターンが出てくる可能性あり
			//またランダムパターンを生成しない場合もあるので半々ぐらいで生成する

			//生成数の補正
			int ch_num = 0;
			
			switch (count+noncount)
			{
				case 1:
					ch_num = 2;
					break;
				case 2:
					ch_num = 2;
					break;
				case 3:
					ch_num = 3;
					break;
				case 4:
					ch_num = 4;
					break;
				case 5:
					ch_num = 3;
					break;
				case 6:
					ch_num = 2;
					break;
				case 7:
					ch_num = 2;
					break;
				case 8:
					ch_num = 1;
					break;
				case 9:
					break;
				case 10:
					break;
				default:
					break;
			}

			int[] chk = new int[count];
			int[] mv_p = new int[count];
			int[] mv_m = new int[count];

			for (int i = 0; i < count; i++)
			{
				chk[i] = 0;
				mv_p[i] = 0;
				mv_m[i] = 0;
			}

			//for (int i = 0; i < num; i++)
			int n = 0;
			do
			{
				if (!marker[n])
				{
					bool another = false;
					if (ch_num > 0)
					{
						another = true;
						int temp;

						do
						{
							temp = rand.Next(count);
						} while (chk[temp] > 2);
						
						chk[temp]++;

						pattern[n] = (Point[])pattern[temp].Clone();
						
						int ch_all = rand.Next(2);
						int uLine, tLine;
						tLine = 7;
						uLine = 0;

						for (int j = 0; j < pattern[temp].Length; j++)
						{
							if (tLine > pattern[temp][j].Y) tLine = pattern[temp][j].Y;
							if (uLine < pattern[temp][j].Y) uLine = pattern[temp][j].Y;
						}
						
						if (tLine - mv_p[temp] == 0 && uLine + mv_m[temp] == 7)
						{
							ch_all = 2;
						}
						else
						{
							if (tLine - mv_p[temp] == 0) ch_all = 1;
							if (uLine + mv_m[temp] == 7) ch_all = 0;
						}

						
						
						switch (ch_all)
						{
							case 0:
								mv_p[temp]++;
								break;
							case 1:
								mv_m[temp]++;
								break;
							default:
								break;
						}

						for (int j = 0; j < pattern[temp].Length; j++)
						{

							switch (ch_all)
							{
								case 0:
									pattern[n][j].Y -= mv_p[temp];
									break;
								case 1:
									pattern[n][j].Y += mv_m[temp];
									break;
								default:
									another = false;
									break;
							}
						}

						for (int j = 0; j < n; j++)
						{
							if (pattern[n].Length == pattern[j].Length)
							{
								bool flag = true;
								for (int k = 0; k < pattern[n].Length; k++)
								{
									if (pattern[n][k].X != pattern[j][k].X || pattern[n][k].Y != pattern[j][k].Y)
									{
										flag = false;
										break;
									}
								}
								if (flag) another = false;
							}
						}
						
						ch_num--;
					}

					if (!another)
					{
						int temp;

						temp = rand.Next(count);

						int ry = rand.Next(1, 3);

						pattern[n] = (Point[])pattern[temp].Clone();
						
						int ch_all = rand.Next(2);
						int offset = rand.Next(pattern[n].Length);

						if (ch_all == 0)
						{
							if (pattern[n][offset].Y - ry < 0)
							{
								pattern[n][offset].Y += ry;
							}
							else
							{
								pattern[n][offset].Y -= ry;
							}
						}

						if (ch_all == 1)
						{
							if (pattern[n][offset].Y + ry > 7)
							{
								pattern[n][offset].Y -= ry;
							}
							else
							{
								pattern[n][offset].Y += ry;
							}
						}

					}

					marker[n] = true;
				}

				//重複チェック
				bool flag_k = true;
				for (int j = 0; j < n; j++)
				{
					if (pattern[n].Length == pattern[j].Length)
					{
						bool checkAll = true;
						for (int m = 0; m < pattern[n].Length; m++)
						{
							if (pattern[n][m].X != pattern[j][m].X || pattern[n][m].Y != pattern[j][m].Y)
							{
								checkAll = false;
								break;
							}
						}

						if (checkAll)
						{
							flag_k = false;
							break;
						}
					}
				}

				if (!flag_k)
				{
					//pattern
					marker[n] = false;
					continue;
				}

				n++;
			} while (n < num);
		}


		/// <summary>
		/// パターン自動生成(低難易度版)
		/// </summary>
		/// <returns></returns>
		private void AutoGenEasy(int count, int noncount, Random rand, int num)
		{
			//patternの重複チェック、ダミー解答の生成等

			bool[] marker = new Boolean[10];
			for (int i = 0; i < count; i++) marker[i] = true;
			for (int i = count; i < 10; i++) marker[i] = false;

			//同様パターンが存在する　→　完全にランダムなパターンを生成して片方を置き換える
			for (int i = 0; i < count-1; i++)
			{
				for (int j = i+1; j < count; j++)
				{
					if (pattern[i].Length == pattern[j].Length)
					{
						bool flag_k = true;
						for (int k = 0; k < pattern[i].Length; k++)
						{
							bool flag_l = false;
							for (int l = 0; l < pattern[j].Length; l++)
							{
								if (pattern[i][k].X == pattern[j][l].X && pattern[i][k].Y == pattern[j][l].Y)
								{
									flag_l = true;
								}
							}
							if (!flag_l)
							{
								flag_k = false;
								break;
							}
						}

						if (flag_k)
						{
							
							marker[j] = false;
							
						}
					}
				}
			}

			//空いている場所があればそこに類似パターンを詰め込む
			//方法が方法だから同じパターンが出てくる可能性あり
			//またランダムパターンを生成しない場合もあるので半々ぐらいで生成する
			for (int i = 0; i < num; i++)
			{
				if (!marker[i])
				{
					bool[] putFlag = new Boolean[8];
					for (int j = 0; j < 8; j++) putFlag[j] = false;
					
					int pat_num = rand.Next(2, 6);
					pattern[i] = new Point[pat_num];
					
					int check = 0;
					while (pat_num != check)
					{
						int x, y;
						x = rand.Next(7);
						y = rand.Next(7);
						
						if (putFlag[x])
						{
							continue;
						}

						pattern[i][check].X = x - 3;
						pattern[i][check].Y = y;

						putFlag[x] = true;

						check++;
					}
				}
			}
		}


		/// <summary>
		/// パターン自動生成(高難易度版)
		/// </summary>
		/// <returns></returns>
		private void AutoGenHard(int count, int noncount, Random rand, int num)
		{
			//patternの重複チェック、ダミー解答の生成等

			bool[] marker = new Boolean[10];
			for (int i = 0; i < count; i++) marker[i] = true;
			for (int i = count; i < 10; i++) marker[i] = false;

			//同様パターンが存在する　→　完全にランダムなパターンを生成して片方を置き換える
			for (int i = 0; i < count-1; i++)
			{
				for (int j = i+1; j < count; j++)
				{
					if (pattern[i].Length == pattern[j].Length)
					{
						bool flag_k = true;
						for (int k = 0; k < pattern[i].Length; k++)
						{
							bool flag_l = false;
							for (int l = 0; l < pattern[j].Length; l++)
							{
								if (pattern[i][k].X == pattern[j][l].X && pattern[i][k].Y == pattern[j][l].Y)
								{
									flag_l = true;
								}
							}
							if (!flag_l)
							{
								flag_k = false;
								break;
							}
						}

						if (flag_k)
						{
							
							marker[j] = false;
							
						}
					}
				}
			}
			
			//空いている場所があればそこに類似パターンを詰め込む
			//方法が方法だから同じパターンが出てくる可能性あり
			//またランダムパターンを生成しない場合もあるので半々ぐらいで生成する

			//生成数の補正
			int ch_num = 0;
			switch (count+noncount)
			{
				case 1:
					ch_num = 2; //2
					break;
				case 2:
					ch_num = 2; //3
					break;
				case 3:
					ch_num = 3; //4
					break;
				case 4:
					ch_num = 4; //6
					break;
				case 5:
					ch_num = 3; //5
					break;
				case 6:
					ch_num = 2; //4
					break;
				case 7:
					ch_num = 2; //3
					break;
				case 8:
					ch_num = 1; //2
					break;
				case 9:
					ch_num = 1; //1
					break;
				case 10:
					break;
				default:
					break;
			}

			for (int i = 0; i < num; i++)
			{
				if (!marker[i])
				{
					bool another = false;
					if (ch_num > 0)
					{
						another = true;
						int temp = rand.Next(count);
						//pattern[temp].CopyTo(pattern[i], 0);
						pattern[i] = (Point[])pattern[temp].Clone();

						int ch_all = rand.Next(2);

						bool[] checker = new Boolean[8];
						for (int j = 0; j < 8; j++) checker[j] = false;

						for (int j = 0; j < pattern[i].Length; j++)
						{
							checker[pattern[i][j].X+3] = true;
						}

						int first = 3;
						for (int j = 0; j < pattern[i].Length; j++)
						{
							if (checker[j])
							{
								first = pattern[i][j].X+3;
								break;
							}
						}
				
						int open = (8 - pattern[i].Length) - first;

						int anaMax = 0;
						int anaMin = 7;

						for (int j = first; j < 8; j++)
						{
							if (!checker[j])
							{
								if (anaMax < j) anaMax = j;
								if (anaMin > j) anaMin = j;
							}
						}

						if (open > 0)
						{
							int ana = rand.Next(1, open);
						
							int ana_count = 0;
							for (int j = first; j < 8; j++)
							{
								if (!checker[j]) ana_count++;
								if (ana == ana_count)
								{
									ana = j;
									break;
								}
							}

							///////////////////////////////////
							if (ch_all == 0)
							{
								if (ana == anaMin)
								{
									for (int j = 0; j < pattern[i].Length; j++)
									{
										if (pattern[i][j].X+3 < ana && j != first)
										{
											pattern[i][j].X += 1;
										}
									}
								}
								else
								{
									for (int j = 0; j < pattern[i].Length; j++)
									{
										if (pattern[i][j].X+3 > ana && j != first)
										{
											pattern[i][j].X -= 1;
										}
									}
								}
							}

							if (ch_all == 1)
							{
								if (ana == anaMax)
								{
									for (int j = 0; j < pattern[i].Length; j++)
									{
										if (pattern[i][j].X+3 > ana && j != first)
										{
											pattern[i][j].X -= 1;
										}
									}
								}
								else
								{
									for (int j = 0; j < pattern[i].Length; j++)
									{
										if (pattern[i][j].X+3 < ana && j != first)
										{
											pattern[i][j].X += 1;
										}
									}
								}
							}

							for (int j = 0; j < i; j++)
							{
								if (pattern[i].Length == pattern[j].Length)
								{
									bool flag = true;
									for (int k = 0; k < pattern[i].Length; k++)
									{
										if (pattern[i][k].X != pattern[j][k].X || pattern[i][k].Y != pattern[i][k].Y)
										{
											flag = false;
											break;
										}
									}
									if (flag) another = false;
								}
							}
						}
						else
						{
							another = false;
						}

						ch_num--;
					}
					
					if (!another)
					{
						int temp;
						temp = rand.Next(count);
						//} //while (chk[temp] > 2);
						
						//chk[temp]++;

						int ry = rand.Next(1, 3);

						pattern[i] = (Point[])pattern[temp].Clone();
						
						int ch_all = rand.Next(2);
						int offset = rand.Next(pattern[i].Length);

						if (ch_all == 0)
						{
							if (pattern[i][offset].Y - ry < 0)
							{
								pattern[i][offset].Y += ry;
							}
							else
							{
								pattern[i][offset].Y -= ry;
							}
						}

						if (ch_all == 1)
						{
							if (pattern[i][offset].Y + ry > 7)
							{
								pattern[i][offset].Y -= ry;
							}
							else
							{
								pattern[i][offset].Y += ry;
							}
						}

					}

					marker[i] = true;
				}
			}
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