using System;

namespace muphic.LinkMake.Dialog.Select
{
	/// <summary>
	/// SelectButton の概要の説明です。
	/// </summary>
	public class SelectButton : Base
	{
		public SelectButtons parent;
		public int num;
		public SelectButton(SelectButtons sbs, int num)
		{
			parent = sbs;
			this.num = num;
		}

		public override void Click(System.Drawing.Point p)
		{
			// ↓テンション高いねーｗ
			// ファイルが無ぇ！？ ならクリックなんざさせねぇぜ！
			if (this.parent.parent.parent.dfList.Index.Count <= this.num) return;
			
			base.Click (p);
			
			// ファイル読み込みクラスをインスタンス化
			//LinkFileReader sfr = new LinkFileReader(this.parent.parent.quest.AnimalList, parent.level_select);
			LinkFileReader sfr = new LinkFileReader(this.parent.parent.parent.score.Animals.AnimalList);
			
			// 1番上のファイル名の要素番号＋自分が何番目か(0～3)
			if (parent.NowPage+num >= parent.parent.parent.dfList.Index.Count) return;

			DataIndex di = (DataIndex)parent.parent.parent.dfList.Index[parent.NowPage + num];//this.FileNames[this.NowPage + num];
			
			// 空なら何もしない
			if(di == null) return;

			string filename = "LinkData\\QuesPatt_" + di.Num + ".mdl";

			// 実際に読み込む ただし、読み込みに失敗したらそのまま
			if( !sfr.Read(filename) ) return;

			parent.parent.parent.tempo_l.TempoMode = sfr.Tempo;
			for(int i=0;i<5;i++)
			{
				parent.parent.parent.tempo_l.tempobutton_l[i].State = 0;						//本来のクリック処理を行う前に
			}	
			parent.parent.parent.tempo_l.tempobutton_l[sfr.Tempo-1].State = 1;
			// 保存ダイアログの題名にファイル名をコピーしておく
			//parent.parent.parent.sadialog.SetTitleName(this.parent.GetSelectFileName(num));
			parent.parent.parent.title = di.Title;
			parent.parent.parent.filenum = di.Num;
			
			this.State = 0;
			
			// もどるボタンを押したことにして、ダイアログを閉じる
			parent.parent.back.Click(System.Drawing.Point.Empty);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			
			//if(this.parent.GetSelectFileName(this.num) == null) return;
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
		
		public override string ToString()
		{
			return "SelectButton" + num;
		}
		
	}
}
