using System;
using muphic.Top;
using muphic.Tutorial;

namespace muphic
{
	public enum ScreenMode{TopScreen, OneScreen, LinkScreen, LinkMakeScreen, StoryScreen, ScoreScreen, TutorialScreen};

	/// <summary>
	/// TopScreen の概要の説明です。
	/// </summary>
	public class TopScreen : Screen
	{
		public MainScreen parent;
		public TutorialMain tutorialparent;
		public OneButton onebutton;
		public LinkButton linkbutton;
		public StoryButton storybutton;
		public TutorialButton tutorialbutton;
		public EndButton endbutton;
		public Screen Screen;
		private ScreenMode screenmode;


		///////////////////////////////////////////////////////////////////////
		//プロパティの宣言
		///////////////////////////////////////////////////////////////////////
		public ScreenMode ScreenMode
		{
			get
			{
				return screenmode;
			}
			set
			{
				screenmode = value;
				switch(value)
				{
					case muphic.ScreenMode.TopScreen:
						Screen = null;									//子のウィンドウは表示せず、自分自身を描画する
						break;
					case muphic.ScreenMode.OneScreen:
						Screen = new OneScreen(this);					//OneScreenをインスタンス化し、そちらを描画する
						break;
					case muphic.ScreenMode.LinkScreen:
						Screen = new LinkTop(this);
						break;
					case muphic.ScreenMode.LinkMakeScreen:
						Screen = new LinkTop(this);
						break;
					case muphic.ScreenMode.StoryScreen:
						Screen = new MakeStoryScreen(this);
						break;
					case muphic.ScreenMode.TutorialScreen:
						Screen = new TutorialScreen(this);
						break;
					default:
						Screen = null;									//とりあえずTopScreenに描画を戻す
						break;
				}
			}
		}


		public TopScreen(MainScreen main)
		{
			muphic.Common.TutorialStatus.initTutorialStatus();
			//muphic.Common.TutorialStatus.test();
			this.parent = main;
			this.TopScreenPreparation();
		}
		public TopScreen(TutorialMain tscreen)
		{
			this.tutorialparent = tscreen;
			this.TopScreenPreparation();
		}

		public void TopScreenPreparation()
		{
			this.ScreenMode = muphic.ScreenMode.TopScreen;

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			onebutton = new OneButton(this);
			linkbutton = new LinkButton(this);
			storybutton = new StoryButton(this);
			tutorialbutton = new TutorialButton(this);
			endbutton = new EndButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\top\\background.png");
			//muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\top\\topXGA.png");
			muphic.DrawManager.Regist(onebutton.ToString(), 458, 350, new string[] {"image\\top\\button\\one\\one_off.png", "image\\top\\button\\one\\one_on.png", "image\\top\\button\\one\\one_non.png"});
			muphic.DrawManager.Regist(linkbutton.ToString(), 750, 350, new string[] {"image\\top\\button\\link\\link_off.png", "image\\top\\button\\link\\link_on.png", "image\\top\\button\\link\\link_non.png"});
			muphic.DrawManager.Regist(storybutton.ToString(), 605, 453, new string[] {"image\\top\\button\\story\\story_off.png", "image\\top\\button\\story\\story_on.png", "image\\top\\button\\story\\story_non.png"});
			muphic.DrawManager.Regist(tutorialbutton.ToString(), 549, 272, new string[] {"image\\top\\button\\tutorial\\startmuphic_off.png", "image\\top\\button\\tutorial\\startmuphic_on.png", "image\\top\\button\\tutorial\\startmuphic_non.png"});
			muphic.DrawManager.Regist(endbutton.ToString(), 600, 630, new string[] {"image\\top\\button\\end\\end_off.png", "image\\top\\button\\end\\end_on.png", "image\\top\\button\\end\\end_non.png"});
			DrawManager.Regist("Nowloading", 222, 173, "image\\Nowloading\\Nowloading.png");
			DrawManager.Regist("Nowloading_bak", 0, 0, "image\\Nowloading\\Nowloading_bak.png");
			DrawManager.Regist("Nowloading_all", 0, 0, "image\\Nowloading\\Nowloading_all.png");
			DrawManager.Regist("1px", 0, 0, "image\\Nowloading\\1px.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(onebutton);
			BaseList.Add(linkbutton);
			BaseList.Add(storybutton);
			BaseList.Add(tutorialbutton);
			BaseList.Add(endbutton);
			
			muphic.Common.AutoSave.initAutoSave();
			muphic.Common.AutoSave.onAutoSave();
		}

		public override void Click(System.Drawing.Point p)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)	//TopScreenのClickを呼ぶ
			{
				base.Click (p);
			}
			else
			{
				Screen.Click(p);								//TopScreenじゃないなら、そちらのほうのClickを呼ぶ
			}
		}

		public override void Draw()
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)	//TopScreenを描画する
			{
				base.Draw ();
			}
			else
			{
				Screen.Draw();									//TopScreenじゃないなら、そちらのほうの描画をする
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)
			{
				base.MouseMove (p);								//TopScreenのMouseMoveを呼ぶ
			}
			else
			{
				Screen.MouseMove(p);							//TopScreenじゃないなら、そちらのほうのMouseMoveを呼ぶ
			}
		}

		public override void DragBegin(System.Drawing.Point begin)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)
			{
				base.DragBegin(begin);							//TopScreenのDragBeginを呼ぶ
			}
			else
			{
				Screen.DragBegin(begin);						//TopScreenじゃないなら、そちらのほうのDragBeginを呼ぶ
			}
		}

		public override void DragEnd(System.Drawing.Point begin, System.Drawing.Point p)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)
			{
				base.DragEnd(begin, p);							//TopScreenのDragEndを呼ぶ
			}
			else
			{
				Screen.DragEnd(begin, p);						//TopScreenじゃないなら、そちらのほうのDragを呼ぶ
			}
		}

		public override void Drag(System.Drawing.Point begin, System.Drawing.Point p)
		{
			if(this.ScreenMode == muphic.ScreenMode.TopScreen)
			{
				base.Drag(begin, p);							//TopScreenのDragを呼ぶ
			}
			else
			{
				Screen.Drag(begin, p);							//TopScreenじゃないなら、そちらのほうのDragを呼ぶ
			}
		}
	}
}