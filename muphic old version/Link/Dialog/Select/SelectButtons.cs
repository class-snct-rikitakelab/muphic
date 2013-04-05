using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.Link.Dialog.Select
{
	public class SelectButtons : Screen
	{
		public SelectDialog parent;
		public String[] FileNames;
		public SelectButton[] sb;

		//public LinkFileData[] lfd;
		
		// 要は1番上に表示されている文字列の要素番号のこと
		public int NowPage;
		
		public SelectButtons(SelectDialog dialog)
		{
			parent = dialog;
			NowPage = 0;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			//this.doSearchScoreDataFiles();
			//this.FileNames = this.SearchScoreDataFiles();

			sb = new SelectButton[6];
			for(int i=0;i<sb.Length;i++)
			{
				sb[i] = new SelectButton(this, i);
			}

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				if(i+1+this.NowPage > parent.parent.dfList.Index.Count) continue;
					//this.FileNames.Length) continue;
				muphic.DrawManager.Regist(sb[i].ToString(), 324, 378 + i*28, "image\\link\\dialog\\select_new\\sbutton_off.png", "image\\link\\dialog\\select_new\\sbutton_on.png");
			}
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			for(int i=0;i<sb.Length;i++)
			{
				BaseList.Add(sb[i]);
			}
		}

		public SelectButtons(int num, SelectDialog dialog)
		{
			parent = dialog;
			//this.doSearchScoreDataFiles();
			ReadFile(num);
			
		}
		
		public override void Draw()
		{
			//base.Draw ();
			for(int i=0; i<sb.Length; i++)
			{
				muphic.DrawManager.Draw(sb[i].ToString(), sb[i].State);

				if(NowPage + i < parent.parent.dfList.Index.Count && !muphic.Common.TutorialStatus.getisTutorialDialog())
					//FileNames.Length   && !muphic.Common.TutorialStatus.getisTutorialDialog())
				{
					//muphic.DrawManager.DrawString(this.GetSelectFileName(i).Title, 358, 381 + i*28);
					DataIndex di = (DataIndex)parent.parent.dfList.Index[NowPage+i];
					muphic.DrawManager.DrawString(di.Title, 358, 381 + i*28);
				}
			}
		}
		
		
//		/// <summary>
//		/// データフォルダ内のファイル一覧を取得するメソッド
//		/// </summary>
//		/// <returns>取得したファイル一覧</returns>
//		private string[] SearchScoreDataFiles()
//		{
//			// ScoreDataフォルダ内のファイル一覧の取得
//			//string[] scorefiles = System.IO.Directory.GetFiles("LinkData");
//
//			ArrayList linkfiles = new ArrayList();
//			for (int i = 0; i < 100; i++)
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
//			
//			lfd = new LinkFileData[FileNames.Length];
//			for (int i = 0; i < FileNames.Length; i++)
//			{
//				lfd[i] = GetSelectFileName(i);
//			}
//		}
//		
//		
//		/// <summary>
//		/// null
//		/// </summary>
//		/// <param name="num"></param>
//		/// <returns></returns>
//		public LinkFileData GetSelectFileName(int num)
//		{
//			if(FileNames.Length <= NowPage + num) return null;
//			LinkFileData lfd = new LinkFileData();
//			
//			try
//			{
//				StreamReader sr = new StreamReader(FileNames[NowPage + num], Encoding.GetEncoding("Shift_JIS"));
//
//				lfd.Title = sr.ReadLine();
//				lfd.Level = int.Parse(sr.ReadLine());
//				sr.Close();
//				return lfd;
//			}
//			catch(FileNotFoundException e)
//			{
//				return null;
//			}
//		}
		
		
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
			if(NowPage+6 < parent.parent.dfList.Index.Count)//FileNames.Length)
			{
				NowPage++;
			}
		}

		/// <summary>
		/// ファイル読み込み
		/// </summary>
		public void ReadFile(int num)
		{
			// ファイル読み込みクラスをインスタンス化
			LinkFileReader sfr = new LinkFileReader(this.parent.parent.quest.AnimalList, parent.level_select);
			
			// 1番上のファイル名の要素番号＋自分が何番目か(0～3)
			if (this.NowPage+num >= parent.parent.dfList.Index.Count) return;

			DataIndex di = (DataIndex)parent.parent.dfList.Index[this.NowPage + num];//this.FileNames[this.NowPage + num];
			
			// 空なら何もしない
			if(di == null) return;

			string filename = "LinkData\\QuesPatt_" + di.Num + ".mdl";
					

			// 実際に読み込む ただし、読み込みに失敗したらそのまま
			int count;
			if((count = sfr.Read(filename)) < 0) return;
			
			parent.parent.score.barNum = count;
			
			parent.parent.links.ButtonVisibleOn();
			parent.parent.links.ButtonVisibleOff(count);
			parent.parent.titlebar.Title = sfr.Name;

			parent.parent.links.BaseState0(); //あらかじめ選択ボタンの選択を解除しておく
			parent.parent.tsuibi.State = 11;

			parent.parent.QuestionNum = 1;
			parent.parent.Tempo = sfr.Tempo;
			parent.parent.group.pattern = sfr.pattern;
			parent.parent.score.AnimalList.Clear();
			for (int i = 0; i < parent.parent.score.putFlag.Length; i++)
			{
				parent.parent.score.putFlag[i] = false;
			}
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					parent.parent.score.ribbon[i, j] = false;
				}
			}

			parent.parent.LinkScreenMode = muphic.LinkScreenMode.ListenDialog;
		}
	}
}