using System;
using System.Drawing;


namespace muphic.Common
{
	public enum ScreenMode {LinkScreen, OneScreen, StoryScreen, LinkMakeScreen, MakeStoryScreen};

	/// <summary>
	/// AllClearDialog の概要の説明です。
	/// </summary>
	public class AllClearDialog : Screen
	{
		public LinkScreen		parent_link;
		public OneScreen		parent_one;
		public LinkMakeScreen	parent_linkmake;
		public MakeStoryScreen	parent_makestory;
		public StoryScreen		parent_story;
		
		public AllClearYesButton yes;
		public AllClearNoButton no;

		private ScreenMode mode;
		public ScreenMode Mode {set{mode = value;} get{return mode;}}

		public AllClearDialog(LinkScreen link)
		{
			this.parent_link = link;
			this.Mode = ScreenMode.LinkScreen;
			Init();
		}

		public AllClearDialog(LinkMakeScreen linkmake)
		{
			this.parent_linkmake = linkmake;
			this.Mode = ScreenMode.LinkMakeScreen;
			Init();
		}

		public AllClearDialog(StoryScreen story)
		{
			this.parent_story = story;
			this.Mode = ScreenMode.StoryScreen;
			Init();
		}

		public AllClearDialog(OneScreen one)
		{
			this.parent_one = one;
			this.Mode = ScreenMode.OneScreen;
			Init();
		}

		public AllClearDialog(MakeStoryScreen makestory)
		{
			this.parent_makestory = makestory;
			this.Mode = ScreenMode.MakeStoryScreen;
			Init();
		}

		public void Init()
		{
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			yes = new AllClearYesButton(this);
			no = new AllClearNoButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 124+112, 167+84, "image\\common\\dialog_allclear\\dialog_allclear_bak.png");
			muphic.DrawManager.Regist(yes.ToString(), 380, 430, "image\\common\\dialog_allclear\\yes_off.png", "image\\common\\dialog_allclear\\yes_on.png");
			muphic.DrawManager.Regist(no.ToString(), 530, 430, "image\\common\\dialog_allclear\\no_off.png", "image\\common\\dialog_allclear\\no_on.png");
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(yes);
			BaseList.Add(no);
		}
	}
}