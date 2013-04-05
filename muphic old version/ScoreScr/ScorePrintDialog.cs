using System;
using muphic.ScoreScr.PrintDialog;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScorePrintDialog の概要の説明です。
	/// </summary>
	public class ScorePrintDialog : Screen
	{
		public ScoreScreen parent;
		
		public YesButton yes;
		public NoButton no;
		
		public ScorePrintDialog(ScoreScreen scorescreen)
		{
			this.parent = scorescreen;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			yes = new YesButton(this);
			no = new NoButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\ScoreXGA\\dialog_new\\print\\printdialog.png");
			muphic.DrawManager.Regist(yes.ToString(), 386, 434, "image\\ScoreXGA\\dialog_new\\print\\yes_off.png", "image\\ScoreXGA\\dialog_new\\print\\yes_on.png");
			muphic.DrawManager.Regist(no.ToString(), 535, 434, "image\\ScoreXGA\\dialog_new\\print\\no_off.png", "image\\ScoreXGA\\dialog_new\\print\\no_on.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(yes);
			BaseList.Add(no);
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}
		
		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}
		
		public override void Draw()
		{
			base.Draw ();
		}
	}
}
