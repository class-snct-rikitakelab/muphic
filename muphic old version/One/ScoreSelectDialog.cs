using System;
using muphic.One.SelectDialog;

namespace muphic.One
{
	/// <summary>
	/// ScoreSelectDialog �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreSelectDialog : Screen
	{
		public OneScreen parent;

		public SelectButtons sbs;
		public UpperScrollButton upper;
		public LowerScrollButton lower;
		public muphic.One.SelectDialog.BackButton back;

		public ScoreSelectDialog(OneScreen one)
		{
			parent = one;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			sbs = new SelectButtons(this);
			upper = new UpperScrollButton(this);
			lower = new LowerScrollButton(this);
			back = new muphic.One.SelectDialog.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\one\\selectdialog\\background.png");
			muphic.DrawManager.Regist(sbs.ToString(), 230, 290, "image\\one\\selectdialog\\dialog_nokosu.png");
			muphic.DrawManager.Regist(upper.ToString(), 640, 388, "image\\one\\selectdialog\\scroll_up.png");
			muphic.DrawManager.Regist(lower.ToString(), 642, 470, "image\\one\\selectdialog\\scroll_down.png");
			muphic.DrawManager.Regist(back.ToString(), 680, 435, "image\\one\\selectdialog\\back_off.png", "image\\one\\selectdialog\\back_on.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
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
