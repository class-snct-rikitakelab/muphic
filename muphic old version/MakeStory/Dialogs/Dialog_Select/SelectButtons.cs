using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// こいつは今までとは少し違って、4つのSelectButtonは常に固定しておく。
	/// そして、NowPageの値によって描画するファイル名を変えていくことになる。だから、UpperやLower
	/// ではそのNowPageの値を変えて、描画するのが変更されるのは文字列だけになる。
	/// んで、SelectButtonがクリックされたときに、現在関連付けされている文字列を用いてファイルを開く。
	/// </summary>
	public class SelectButtons : Screen
	{
		public StorySelectDialog parent;
		public String[] FileNames;
		public SelectButton[] sb;
		public int NowPage;															//要は1番上に表示されている文字列の要素番号のこと
		public SelectButtons(StorySelectDialog dialog)
		{
			parent = dialog;
			NowPage = 0;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			FileNames = this.SearchStoryDataFiles();
			if(FileNames.Length < 4)
			{
				sb = new SelectButton[FileNames.Length];
			}
			else
			{
				sb = new SelectButton[4];
			}

			for(int i=0;i<sb.Length;i++)
			{
				sb[i] = new SelectButton(this, i);
			}

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				muphic.DrawManager.Regist(sb[i].ToString(), 307, 383 + i*28, @"image\MakeStory\dialog\select\sbutton_off.png", @"image\MakeStory\dialog\select\sbutton_on.png");
			}

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				BaseList.Add(sb[i]);
			}
		}

		public override void Draw()
		{
			//base.Draw ();
			for(int i=0;i<sb.Length;i++)
			{
				muphic.DrawManager.Draw(sb[i].ToString(), sb[i].State);
				if(NowPage + i < FileNames.Length      && !muphic.Common.TutorialStatus.getisTutorialDialog())
				{
					muphic.DrawManager.DrawString(FileNames[i+this.NowPage], 342, 386 + i*28);
				}
			}
		}

		/// <summary>
		/// SelectButtonのClickメソッドでこれを呼ぶ。
		/// 自分のSelectButtonに対応したファイル名を取得する。
		/// </summary>
		/// <param name="num"></param>
		public String GetSelectFileName(int num)
		{
			if(FileNames.Length <= NowPage + num)
			{
				return null;
			}
			return FileNames[NowPage + num];
		}

		/// <summary>
		/// ファイル名を上にスクロールさせるときに呼ぶメソッド
		/// </summary>
		public void Upper()
		{
			if(0 < NowPage)
			{
				NowPage--;
			}
		}

		/// <summary>
		/// ファイル名を下にスクロールさせるときに呼ぶメソッド
		/// </summary>
		public void Lower()
		{
			if(NowPage+4 < FileNames.Length)
			{
				NowPage++;
			}
		}

		/// <summary>
		/// フォルダStoryDataに格納されている物語ファイルの名前(拡張子を除く)の配列を
		/// 取得するメソッド
		/// </summary>
		/// <returns></returns>
		private String[] SearchStoryDataFiles()
		{
			char[] separator = {'.'};
			char[] yen = {'\\'};
			String[] strs = System.IO.Directory.GetFiles("StoryData");					//StoryData内のファイル名を取得
			String[] answer = new String[strs.Length];
			for(int i=0;i<strs.Length;i++)
			{
				strs[i] = strs[i].Split(yen, 2)[1];										//"StoryData\\○○.txt"の、"StoryData"の部分を取り除く
			}
			for(int i=0;i<strs.Length;i++)												//"○○.txt"の、".txt"の部分を取り除く											
			{
				answer[i] = "";
				String[] s = strs[i].Split(separator);
				for(int j=0;j<s.Length-1;j++)											//拡張子以外に.を使っている時のための対策
				{																		//Splitした際の拡張子以外の要素をすべてつなぎ合わせればいい。
					if(answer[i] != "")
					{
						answer[i] += '.';
					}
					answer[i] += s[j];
				}
			}
			return answer;
		}
	}
}