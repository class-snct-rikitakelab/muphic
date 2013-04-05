using System;
using muphic.Link.Dialog.Select;

namespace muphic.Link.Dialog.Select
{
	/// <summary>
	/// SelectDialog の概要の説明です。
	/// </summary>
	public class SelectDialog : Screen
	{
		public LinkScreen parent;
		public SelectButtons sbs;
		public UpperScrollButton upper;
		public LowerScrollButton lower;
		public muphic.Link.Dialog.Select.BackButton back;
		public LevelEasyButton easy;
		public LevelNormalButton normal;
		public LevelHardButton hard;

		public int level = -1;
		public int level_select = 0;

		public int Level { set{this.level = value;} get{return this.level;}}

		public SelectDialog(LinkScreen link)
		{
			parent = link;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			sbs = new SelectButtons(this);
			upper = new UpperScrollButton(this);
			lower = new LowerScrollButton(this);
			back = new muphic.Link.Dialog.Select.BackButton(this);

			easy = new LevelEasyButton(this);
			normal = new LevelNormalButton(this);
			hard = new LevelHardButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 125, "image\\link\\dialog\\select_new\\dialog_select_bak.png");
			muphic.DrawManager.Regist(sbs.ToString(), 230, 290, "image\\link\\dialog\\select_new\\dialog_select.png");
			muphic.DrawManager.Regist(upper.ToString(), 666, 380, "image\\link\\dialog\\select_new\\scroll_up.png");
			muphic.DrawManager.Regist(lower.ToString(), 668, 520, "image\\link\\dialog\\select_new\\scroll_down.png");
			muphic.DrawManager.Regist(back.ToString(), 680, 555, "image\\link\\dialog\\back_off.png", "image\\link\\dialog\\back_on.png");

			muphic.DrawManager.Regist(easy.ToString(), 330, 265, "image\\link\\dialog\\select_new\\button\\easy_off.png", "image\\link\\dialog\\select_new\\button\\easy_on.png");
			muphic.DrawManager.Regist(normal.ToString(), 450, 265, "image\\link\\dialog\\select_new\\button\\normal_off.png", "image\\link\\dialog\\select_new\\button\\normal_on.png");
			muphic.DrawManager.Regist(hard.ToString(), 562, 261, "image\\link\\dialog\\select_new\\button\\hard_off.png", "image\\link\\dialog\\select_new\\button\\hard_on.png");

			muphic.DrawManager.Regist("star", 1030, 0, new String[3]{
												"image\\link\\dialog\\select_new\\star_easy.png", 
												"image\\link\\dialog\\select_new\\star_normal.png", 
												"image\\link\\dialog\\select_new\\star_hard.png"});

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(sbs);
			BaseList.Add(upper);
			BaseList.Add(lower);
			BaseList.Add(back);

			BaseList.Add(easy);
			BaseList.Add(normal);
			BaseList.Add(hard);

		}

		public SelectDialog(int num, LinkScreen link)
		{
			parent = link;
			sbs = new SelectButtons(num, this);
			//sbs.ReadFile(num);
		}
		
		public override void Draw()
		{
			base.Draw();

			for (int i = 0; i < level; i++)
			{
				muphic.DrawManager.DrawCenter("star", 470+i*35, 580, level_select+1);
			}
		}
	}
}
