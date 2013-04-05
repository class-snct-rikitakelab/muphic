using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace muphic.Link
{
	/// <summary>
	/// QuestionPattern の概要の説明です。
	/// GroupPattern同様Link以外では使いそうにないのでひとまずコメント無しの方向でお願いします
	/// </summary>
	public class QuestionPattern
	{

		public ArrayList AnimalList;

		public Animal[][] Question;

		public QuestionPattern()
		{
			AnimalList = new ArrayList();
			Question = new Animal[6][];
			//			for (int i = 1; i < 7; i++)
			//			{
			//				readPattern("data\\QuesPatt0" + i.ToString() + ".mdm", i-1);
			//			}
		}

		//		private bool Insert(int place, int code)
		//		{
		//			int i;
		//			for(i=0;i<AnimalList.Count;i++)
		//			{
		//				Animal a = (Animal)AnimalList[i];
		//				if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
		//				{
		//					return false;
		//				}
		//				if(a.place > place)												//設定する位置より遠いものが存在した
		//				{																//AnimalListは昇順にソートしてあるから、
		//					break;														//遠いものが現れたということはすでにplaceを
		//				}																//越しているので、かぶっていないことが確定する
		//			}
		//			Animal newAnimal = new Animal(place, code);	//Animalオブジェクトをインスタンス化
		//			AnimalList.Insert(i, newAnimal);									//遠いようになったところに割り込む
		//			//こうすることによって昇順が保たれる
		//			return true;
		//		}

		//		private void readPattern(string filename, int num)
		//		{
		//			if (!File.Exists(filename)) return ;  // ファイルの有無をチェック
		//
		//			StreamReader reader = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS")) ;
		//			string readdata;
		//			string[] tempdata;
		//			int count = 0;
		//
		//
		//			while ((readdata = reader.ReadLine()) != null) 
		//			{
		//				tempdata = readdata.Split(' ');
		//				Point insert = new Point();
		//				for (int i = 0; i < tempdata.Length/2; i++)
		//				{
		//					switch (tempdata[i*2].ToCharArray(0, 1)[0])
		//					{
		//						case 'A':
		//							insert.X = 0;
		//							break;
		//						case 'B':
		//							insert.X = 1;
		//							break;
		//						case 'C':
		//							insert.X = 2;
		//							break;
		//						case 'D':
		//							insert.X = 3;
		//							break;
		//						case 'E':
		//							insert.X = 4;
		//							break;
		//						case 'F':
		//							insert.X = 5;
		//							break;
		//						case 'G':
		//							insert.X = 6;
		//							break;
		//						case 'H':
		//							insert.X = 7;
		//							break;
		//						default:
		//							insert.X = 0;
		//							break;
		//					}
		//
		//					switch (tempdata[i*2+1].ToCharArray(0, 1)[0])
		//					{
		//						case '0':
		//							insert.Y = 1; //25 + 181;
		//							break;
		//						case '1':
		//							insert.Y = 2; //75 + 181;
		//							break;
		//						case '2':
		//							insert.Y = 3; //125 + 181;
		//							break;
		//						case '3':
		//							insert.Y = 4; //175 + 181;
		//							break;
		//						case '4':
		//							insert.Y = 5; //225 + 181;
		//							break;
		//						case '5':
		//							insert.Y = 6; //275 + 181;
		//							break;
		//						case '6':
		//							insert.Y = 7; //325 + 181;
		//							break;
		//						case '7':
		//							insert.Y = 8; //375 + 181;
		//							break;
		//						default:
		//							insert.Y = 0; //225 + 181;
		//							break;
		//					}
		//					Insert(count*8+insert.X, insert.Y);
		//				}
		//				count++;
		//			}
			
		//			Question[num] = new Animal[AnimalList.Count];
		//			AnimalList.CopyTo(Question[num]);
		//			AnimalList.Clear();
		//
		//			reader.Close() ;
		//		}
		//	}
	}
}