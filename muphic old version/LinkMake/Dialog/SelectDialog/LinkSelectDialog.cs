using System;
using muphic.LinkMake.Dialog.Select;

namespace muphic.LinkMake.Dialog.Select
{
	/// <summary>
	/// ScoreSelectDialog �̊T�v�̐����ł��B
	/// </summary>
	public class LinkSelectDialog : Screen
	{
		public LinkMakeScreen parent;
		public SelectButtons sbs;
		public UpperScrollButton upper;
		public LowerScrollButton lower;
		public muphic.LinkMake.Dialog.Select.BackButton back;

		public LinkSelectDialog(LinkMakeScreen link)
		{
			parent = link;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			sbs = new SelectButtons(this);
			upper = new UpperScrollButton(this);
			lower = new LowerScrollButton(this);
			back = new muphic.LinkMake.Dialog.Select.BackButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\link\\dialog\\select\\dialog_select_bak.png");
			muphic.DrawManager.Regist(sbs.ToString(), 230, 290, "image\\link\\dialog\\select\\dialog_nokosu.png");
			muphic.DrawManager.Regist(upper.ToString(), 640, 388, "image\\link\\dialog\\select\\scroll_up.png");
			muphic.DrawManager.Regist(lower.ToString(), 642, 470, "image\\link\\dialog\\select\\scroll_down.png");
			muphic.DrawManager.Regist(back.ToString(), 680, 435, "image\\link\\dialog\\back_off.png", "image\\link\\dialog\\back_on.png");

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
