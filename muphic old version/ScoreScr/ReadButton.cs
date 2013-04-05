using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ReadButton の概要の説明です。
	/// </summary>
	public class ReadButton : Base
	{
		public ScoreScreen parent;

		public ReadButton(ScoreScreen score)
		{
			this.parent = score;
			if(this.parent.ParentScreenMode == muphic.ScreenMode.LinkScreen) this.Visible = false;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.isSelectDialog = true;

			// ファイル一覧の取得
			// 新しく保存したファイルもファイル一覧に出せるようにするため
			//parent.sedialog.sbs.doSearchScoreDataFiles(); ボタンのほうが増えないので却下
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
