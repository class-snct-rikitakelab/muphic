using System;

namespace muphic.ScoreScr.SelectDialog
{
	public class SelectButtons : Screen
	{
		public ScoreSelectDialog parent;
		public String[] FileNames;
		public SelectButton[] sb;
		
		// 要は1番上に表示されている文字列の要素番号のこと
		public int NowPage;
		
		public SelectButtons(ScoreSelectDialog dialog)
		{
			parent = dialog;
			NowPage = 0;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			this.doSearchScoreDataFiles();
			//this.FileNames = this.SearchScoreDataFiles();
			
			sb = new SelectButton[4];
			for(int i=0;i<sb.Length;i++)
			{
				sb[i] = new SelectButton(this, i);
			}
			
			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				if(i+1+this.NowPage > this.FileNames.Length) continue;
				muphic.DrawManager.Regist(sb[i].ToString(), 307, 383 + i*28, "image\\ScoreXGA\\dialog_new\\yobidasu\\sbutton_off.png", "image\\ScoreXGA\\dialog_new\\yobidasu\\sbutton_on.png");
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
			for(int i=0; i<sb.Length; i++)
			{
				muphic.DrawManager.Draw(sb[i].ToString(), sb[i].State);
				if(NowPage + i < FileNames.Length)
				{
					// ファイル名からアンダースコア以下を除いた文字列を表示
					muphic.DrawManager.DrawString(this.GetSelectFileName(i), 342, 386 + i*28);
				}
			}
		}
		
		#region ver.SETO
		
		/*
		/// <summary>
		/// フォルダStoryDataに格納されている物語ファイルの名前(拡張子を除く)の配列を
		/// 取得するメソッド
		/// </summary>
		/// <returns></returns>
		private String[] SearchScoreDataFiles()
		{
			char[] separator = {'.'};
			char[] yen = {'\\'};
			String[] strs = System.IO.Directory.GetFiles("ScoreData");					//StoryData内のファイル名を取得
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
		*/
		
		#endregion
		
		#region ver.Gackt こっちを使う
		
		/// <summary>
		/// 楽譜データフォルダ内のファイル一覧を取得するメソッド
		/// </summary>
		/// <returns>取得したファイル一覧</returns>
		private string[] SearchScoreDataFiles()
		{
			// ScoreDataフォルダ内のファイル一覧の取得
			string[] scorefiles = System.IO.Directory.GetFiles("ScoreData");
			
			// パスと拡張子もそのままにしたファイル名の一覧を返す
			return scorefiles;
		}

		
		/// <summary>
		/// ファイル一覧を取得し、FileNamesフィールドに格納するメソッド
		/// 外部クラスからも実行できるようにするためメソッドにした
		/// </summary>
		public void doSearchScoreDataFiles()
		{
			this.FileNames = this.SearchScoreDataFiles();
		}
		
		
		/// <summary>
		/// よびだす一覧に表示する文字列を得るメソッド
		/// 酔ったまま作ったんでみょんな感じ
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public string GetSelectFileName(int num)
		{
			// リストのサイズを超えた位置のファイル名を得ようとしてたらnullを返す
			if(FileNames.Length <= NowPage + num) return null;
			
			// ファイル名 "ScoreData\\*_?.msd" 中の "*_?.msd" の部分を抽出し、さらにアンダースコア以下を切り取って返す
			return FileNames[NowPage + num].Split(new char[] {'\\'})[1].Split(new char[] {'_'})[0];
		}
		
		#endregion
		
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
	}
}