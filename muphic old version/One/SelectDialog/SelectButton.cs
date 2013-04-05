using System;
using System.Collections;

namespace muphic.One.SelectDialog
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
			// ファイルが無ぇ！？ ならクリックなんざさせねぇぜ！
			if(this.parent.GetSelectFileName(this.num) == null) return;
			
			base.Click (p);
			
			// ファイル読み込みクラスをインスタンス化してみる
			ScoreFileReader sfr = new ScoreFileReader(parent.parent.parent.score.Animals.AnimalList);
			
			// 1番上のファイル名の要素番号＋自分が何番目か(0～3)
			string filename = this.parent.FileNames[this.parent.NowPage + num];
			
			// 空なら何もしない
			if(filename == "") return;
			
			// 実際に読み込む ただし、読み込みに失敗したらそのまま
			if( !sfr.ReadMSDFile(filename) ) return;
			
			// 曲名をひとりで音楽本体に渡す
			this.parent.parent.parent.ScoreTitle = this.parent.GetSelectFileName(num);
			
			// テンポの情報を渡す
			this.parent.parent.parent.tempo.TempoMode = sfr.getTempo();
			
			// 水色ボタンをoff
			this.State = 0;
			
			// もどるボタンを押したことにして、ダイアログを閉じる
			parent.parent.back.Click(System.Drawing.Point.Empty);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			
			if(this.parent.GetSelectFileName(this.num) == null) return;
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
