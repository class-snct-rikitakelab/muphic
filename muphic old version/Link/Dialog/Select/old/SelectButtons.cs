//using System;
//using System.Collections;
//using System.IO;
//using System.Text;
//using System.Windows.Forms;
//
//namespace muphic.Link.Dialog.Select
//{
//	public class SelectButtons : Screen
//	{
//		public SelectDialog parent;
//		public String[] FileNames;
//		public SelectButton[] sb;
//		
//		// 要は1番上に表示されている文字列の要素番号のこと
//		public int NowPage;
//		
//		public SelectButtons(SelectDialog dialog)
//		{
//			parent = dialog;
//			NowPage = 0;
//			
//			///////////////////////////////////////////////////////////////////
//			//部品のインスタンス化
//			///////////////////////////////////////////////////////////////////
//			this.doSearchScoreDataFiles();
//			//this.FileNames = this.SearchScoreDataFiles();
//
//			sb = new SelectButton[4];
//			for(int i=0;i<sb.Length;i++)
//			{
//				sb[i] = new SelectButton(this, i);
//			}
//
//			///////////////////////////////////////////////////////////////////
//			//部品のテクスチャ・座標の登録
//			///////////////////////////////////////////////////////////////////
//			for(int i=0;i<sb.Length;i++)
//			{
//				if(i+1+this.NowPage > this.FileNames.Length) continue;
//				muphic.DrawManager.Regist(sb[i].ToString(), 345, 383 + i*28, "image\\link\\dialog\\select\\sbutton_off.png", "image\\link\\dialog\\select\\sbutton_on.png");
//			}
//			
//			///////////////////////////////////////////////////////////////////
//			//部品の画面への登録
//			///////////////////////////////////////////////////////////////////
//			for(int i=0;i<sb.Length;i++)
//			{
//				BaseList.Add(sb[i]);
//			}
//		}
//		
//		public override void Draw()
//		{
//			//base.Draw ();
//			for(int i=0; i<sb.Length; i++)
//			{
//				muphic.DrawManager.Draw(sb[i].ToString(), sb[i].State);
//				if(NowPage + i < FileNames.Length   && !muphic.Common.TutorialStatus.getisTutorialDialog())
//				{
//					// ファイル名からアンダースコア以下を除いた文字列を表示
//					muphic.DrawManager.DrawString(this.GetSelectFileName(i), 382, 386 + i*28);
//				}
//			}
//		}
//		
//		
//		/// <summary>
//		/// 楽譜データフォルダ内のファイル一覧を取得するメソッド
//		/// </summary>
//		/// <returns>取得したファイル一覧</returns>
//		private string[] SearchScoreDataFiles()
//		{
//			// ScoreDataフォルダ内のファイル一覧の取得
//			//string[] scorefiles = System.IO.Directory.GetFiles("LinkData");
//
//			ArrayList linkfiles = new ArrayList();
//			for (int i = 0; i < 37564; i++)
//			{
//				// 0から順番に"文字列_番号.mdl"が存在するかチェックしていく
//				string filename = "LinkData\\QuesPatt_" + i.ToString() + ".mdl";
//
//				//　ファイルが存在する場合追加
//				if (File.Exists(filename)) linkfiles.Add(filename);
//			}
//			
//			// パスと拡張子もそのままにしたファイル名の一覧を返す
//
//			string[] str = (string[])linkfiles.ToArray(typeof(string));
//			return str;
//		}
//
//		
//		/// <summary>
//		/// ファイル一覧を取得し、FileNamesフィールドに格納するメソッド
//		/// 外部クラスからも実行できるようにするためメソッドにした
//		/// </summary>
//		public void doSearchScoreDataFiles()
//		{
//			this.FileNames = this.SearchScoreDataFiles();
//		}
//		
//		
//		/// <summary>
//		/// よびだす一覧に表示する文字列を得るメソッド
//		/// 酔ったまま作ったんでみょんな感じ
//		/// </summary>
//		/// <param name="num"></param>
//		/// <returns></returns>
//		public string GetSelectFileName(int num)
//		{
//			// リストのサイズを超えた位置のファイル名を得ようとしてたらnullを返す
//			if(FileNames.Length <= NowPage + num) return null;
//			
//			////			// ファイル名 "ScoreData\\*_?.msd" 中の "*_?.msd" の部分を抽出し、さらにアンダースコア以下を切り取って返す
//			////			return FileNames[NowPage + num].Split(new char[] {'\\'})[1].Split(new char[] {'_'})[0];
//			try
//			{
//				StreamReader sr = new StreamReader(FileNames[NowPage + num], Encoding.GetEncoding("Shift_JIS"));
//				String str = sr.ReadLine();
//				sr.Close();
//				return str;
//			}
//			catch(FileNotFoundException e)
//			{
//				// そんなことありえんと思うけどね
//				//MessageBox.Show("奇術！ファイルが見つからない");
//				return null;
//			}
//		}
//		
//		
//		/// <summary>
//		/// ファイル名を上にスクロールさせるときに呼ぶメソッド
//		/// </summary>
//		public void Upper()
//		{
//			if(0 < NowPage)
//			{
//				NowPage--;
//			}
//		}
//
//		/// <summary>
//		/// ファイル名を下にスクロールさせるときに呼ぶメソッド
//		/// </summary>
//		public void Lower()
//		{
//			if(NowPage+4 < FileNames.Length)
//			{
//				NowPage++;
//			}
//		}
//	}
//}