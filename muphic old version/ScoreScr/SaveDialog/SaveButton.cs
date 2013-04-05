using System;

namespace muphic.ScoreScr.SaveDialog
{
	/// <summary>
	/// SaveButton の概要の説明です。
	/// </summary>
	public class SaveButton : Base
	{
		public ScoreSaveDialog parent;

		public SaveButton(ScoreSaveDialog dialog)
		{
			this.parent = dialog;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// 題名が空なら何もしない
			if( this.parent.titlename == null ) return;

			// ファイル書き込みクラスをインスタンス化してみる
			ScoreFileWriter sfw = new ScoreFileWriter(this.parent.parent.AnimalList);
			
			// 実際に書き込む ただし、書き込みに失敗したらそのまま
			if( !sfw.WriteMSDFile(this.parent.titlename) ) return;

			// 戻るボタンを押したことにして、ダイアログを閉じる
			parent.back.Click(System.Drawing.Point.Empty);
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}

	}
}
