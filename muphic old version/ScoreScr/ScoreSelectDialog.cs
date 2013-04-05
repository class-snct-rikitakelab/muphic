using System;
using muphic.ScoreScr.SelectDialog;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScoreSelectDialog の概要の説明です。
	/// </summary>
	public class ScoreSelectDialog : Screen
	{
		public ScoreScreen parent;
		public SelectButtons sbs;
		public UpperScrollButton upper;
		public LowerScrollButton lower;
		public muphic.ScoreScr.SelectDialog.BackButton back;

		public ScoreSelectDialog(ScoreScreen score)
		{
			parent = score;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			sbs = new SelectButtons(this);
			upper = new UpperScrollButton(this);
			lower = new LowerScrollButton(this);
			back = new muphic.ScoreScr.SelectDialog.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\ScoreXGA\\dialog_new\\yobidasu\\background.png");
			muphic.DrawManager.Regist(sbs.ToString(), 230, 290, "image\\ScoreXGA\\dialog_new\\yobidasu\\dialog_nokosu.png");
			muphic.DrawManager.Regist(upper.ToString(), 640, 388, "image\\ScoreXGA\\dialog_new\\yobidasu\\scroll_up.png");
			muphic.DrawManager.Regist(lower.ToString(), 642, 470, "image\\ScoreXGA\\dialog_new\\yobidasu\\scroll_down.png");
			muphic.DrawManager.Regist(back.ToString(), 680, 435, "image\\ScoreXGA\\dialog_new\\back_off.png", "image\\ScoreXGA\\dialog_new\\back_on.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(sbs);
			BaseList.Add(upper);
			BaseList.Add(lower);
			BaseList.Add(back);
		}
		
		public override void Draw()
		{
			base.Draw ();
		}
	}
}
