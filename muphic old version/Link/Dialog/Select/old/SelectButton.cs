//using System;
//
//namespace muphic.Link.Dialog.Select
//{
//	/// <summary>
//	/// SelectButton の概要の説明です。
//	/// </summary>
//	public class SelectButton : Base
//	{
//		public SelectButtons parent;
//		public int num;
//		public SelectButton(SelectButtons sbs, int num)
//		{
//			parent = sbs;
//			this.num = num;
//		}
//
//		public override void Click(System.Drawing.Point p)
//		{
//			// ↓テンション高いねーｗ
//			// ファイルが無ぇ！？ ならクリックなんざさせねぇぜ！
//			if(this.parent.GetSelectFileName(this.num) == null) return;
//			
//			base.Click (p);
//			
//			// ファイル読み込みクラスをインスタンス化してみる
//			LinkFileReader sfr = new LinkFileReader(this.parent.parent.parent.quest.AnimalList);
//			
//			// 1番上のファイル名の要素番号＋自分が何番目か(0～3)
//			string filename = this.parent.FileNames[this.parent.NowPage + num];
//			
//			// 空なら何もしない
//			if(filename == "") return;
//			
//			// 実際に読み込む ただし、読み込みに失敗したらそのまま
//			if( !sfr.Read(filename) ) return;
//			parent.parent.parent.titlebar.Title = sfr.Name;
//
//			parent.parent.parent.QuestionNum = 1;
//			parent.parent.parent.Tempo = sfr.Tempo;
//			parent.parent.parent.group.pattern = sfr.pattern;
//			parent.parent.parent.score.AnimalList.Clear();
//			for (int i = 0; i < parent.parent.parent.score.putFlag.Length; i++)
//			{
//				parent.parent.parent.score.putFlag[i] = false;
//			}
//			for (int i = 0; i < 10; i++)
//			{
//				for (int j = 0; j < 4; j++)
//				{
//					parent.parent.parent.score.ribbon[i, j] = false;
//				}
//			}
//
//			// 保存ダイアログの題名にファイル名をコピーしておく
//			//parent.parent.parent.sadialog.SetTitleName(this.parent.GetSelectFileName(num));
//			
//			this.State = 0;
//			
//			// もどるボタンを押したことにして、ダイアログを閉じる
//			//parent.parent.back.Click(System.Drawing.Point.Empty);
//			parent.parent.parent.LinkScreenMode = muphic.LinkScreenMode.ListenDialog;
//		}
//		
//		public override void MouseEnter()
//		{
//			base.MouseEnter ();
//			
//			if(this.parent.GetSelectFileName(this.num) == null) return;
//			this.State = 1;
//		}
//		
//		public override void MouseLeave()
//		{
//			base.MouseLeave ();
//			this.State = 0;
//		}
//		
//		public override string ToString()
//		{
//			return "SelectButton" + num;
//		}
//		
//	}
//}
