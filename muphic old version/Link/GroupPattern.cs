using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace muphic.Link
{
	/// <summary>
	/// GroupPattern の概要の説明です。
	/// Link以外では使いそうにないのでひとまずコメント無しの方向でお願いします
	/// </summary>
	public class GroupPattern
	{
//		public GroupPattern(string filename)
//		{
//            readPattern(filename);
//		}

		public Point[][] pattern = null;

		public GroupPattern()
		{
			pattern = new Point[10][];
			for (int i = 0; i < 10; i++)
			{
				pattern[i] = new Point[25];
			}
        }

		private void readPattern(string filename)
		{
			if (!File.Exists(filename)) return ;  // ファイルの有無をチェック

			StreamReader reader = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS")) ;
			string readdata;
			string[] tempdata;
			int count = 0;

			while ((readdata = reader.ReadLine()) != null) 
			{
				tempdata = readdata.Split(' ');
				pattern[count] = new Point[tempdata.Length/2];
				for (int i = 0; i < tempdata.Length/2; i++)
				{
					switch (tempdata[i*2].ToCharArray(0, 1)[0])
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

					switch (tempdata[i*2+1].ToCharArray(0, 1)[0])
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
			reader.Close() ;
		}

		public Point getPattern(int a, int b)
		{
			if (a < 10)
			{
				if (pattern[a].Length > b)
				{
					return pattern[a][b];
				}
			}

			Point ret = new Point();
			ret.Y = -1;
			return ret;
		}

	}
}