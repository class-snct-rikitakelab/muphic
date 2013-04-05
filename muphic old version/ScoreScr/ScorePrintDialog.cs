using System;
using muphic.ScoreScr.PrintDialog;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScorePrintDialog �̊T�v�̐����ł��B
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
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			yes = new YesButton(this);
			no = new NoButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\ScoreXGA\\dialog_new\\print\\printdialog.png");
			muphic.DrawManager.Regist(yes.ToString(), 386, 434, "image\\ScoreXGA\\dialog_new\\print\\yes_off.png", "image\\ScoreXGA\\dialog_new\\print\\yes_on.png");
			muphic.DrawManager.Regist(no.ToString(), 535, 434, "image\\ScoreXGA\\dialog_new\\print\\no_off.png", "image\\ScoreXGA\\dialog_new\\print\\no_on.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
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
