using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScoreTitleArea の概要の説明です。
	/// </summary>
	public class ScoreTitleArea : Screen
	{
		public ScoreScreen parent;
		public string ScoreTitle;		
		
		public ScoreTitleArea(ScoreScreen score)
		{
			this.parent = score;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			// ﾈｰﾖ ヽ(`Д´)ﾉｳﾜｰﾝ
			
			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			DrawManager.Regist(this.ToString(), 170, 42, "image\\ScoreXGA\\titlearea.png");
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			// ﾈｰﾖ ヽ(`Д´)ﾉｳﾜｰﾝ
			
			switch(this.parent.ParentScreenMode)
			{
				case muphic.ScreenMode.OneScreen:
					// ひとりで音楽から呼び出された場合 
					this.ScoreTitle = ((OneScreen)this.parent.parent).ScoreTitle;
					break;
				case muphic.ScreenMode.LinkScreen:
					// つなげて音楽（回答）から呼び出された場合
					this.ScoreTitle = ((LinkScreen)this.parent.parent).titlebar.Title;
					break;
				case muphic.ScreenMode.LinkMakeScreen:
					// つなげて音楽（問題作成）から呼び出された場合
					this.Visible = false;
					break;
				case muphic.ScreenMode.StoryScreen:
					// ものたがり音楽から呼び出された場合
					// 無題だった場合はこちらで題をつけ、全角数字でスライドのページ数を付加する
					string title = ((StoryScreen)this.parent.parent).parent.PictureStory.Title;
					if( title == "" || title == null ) title = "あたらしいものがたり"; 
					string page = "　" + ((char)((int)'１' + ((StoryScreen)this.parent.parent).NowPage)).ToString() + "ページ";
					this.ScoreTitle = title + page;
					break;
				default:
					break;
			}
			
		}
		
		public override void Draw()
		{
			base.Draw ();
			DrawManager.DrawString(this.ScoreTitle, 191, 57);
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}
		
		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}



	}
}
