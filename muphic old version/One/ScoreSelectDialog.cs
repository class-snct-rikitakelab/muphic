using System;
using muphic.One.SelectDialog;

namespace muphic.One
{
	/// <summary>
	/// ScoreSelectDialog の概要の説明です。
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
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			sbs = new SelectButtons(this);
			upper = new UpperScrollButton(this);
			lower = new LowerScrollButton(this);
			back = new muphic.One.SelectDialog.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\one\\selectdialog\\background.png");
			muphic.DrawManager.Regist(sbs.ToString(), 230, 290, "image\\one\\selectdialog\\dialog_nokosu.png");
			muphic.DrawManager.Regist(upper.ToString(), 640, 388, "image\\one\\selectdialog\\scroll_up.png");
			muphic.DrawManager.Regist(lower.ToString(), 642, 470, "image\\one\\selectdialog\\scroll_down.png");
			muphic.DrawManager.Regist(back.ToString(), 680, 435, "image\\one\\selectdialog\\back_off.png", "image\\one\\selectdialog\\back_on.png");

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
