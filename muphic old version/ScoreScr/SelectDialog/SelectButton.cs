using System;

namespace muphic.ScoreScr.SelectDialog
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
			ScoreFileReader sfr = new ScoreFileReader(this.parent.parent.parent.AnimalList);
			
			// 1番上のファイル名の要素番号＋自分が何番目か(0～3)
			string filename = this.parent.FileNames[this.parent.NowPage + num];
			
			// 空なら何もしない
			if(filename == "") return;
			
			// 実際に読み込む ただし、読み込みに失敗したらそのまま
			if( !sfr.ReadMSDFile(filename) ) return;
			
			// 読み込んだデータから音符リストを作成し、描画させる
			System.Console.WriteLine("音符リスト読み込み 再描画");
			parent.parent.parent.scoremain.CreateScoreListAll();
			parent.parent.parent.scoremain.ReDraw();
			
			// 保存ダイアログの題名にファイル名をコピーしておく
			parent.parent.parent.sadialog.SetTitleName(this.parent.GetSelectFileName(num));
			
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
