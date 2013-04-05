using System;
using muphic;

namespace muphic.Titlemode
{
    public enum InputMode { None, Alpha, Hira, Kata, Num };
    public enum TargetMode { Title, Sentence };

	public class TitleScreen : Screen
	{
        //parentなものたち
		public MakeStoryScreen Makescr;
        public StoryScreen Storyscr;
		public ScoreScreen Scorescr;
		public muphic.LinkMake.Dialog.Save.LinkSaveDialog Linksave;
		public OneScreen Onescr;
        //parentの判断
        public int ParentNum;
        //全削除
		DelAllButton delall;
		//一文字削除
        DelButton del;
		//編集終了
        DecisionButton decision;
		//入力切り替えボタン
        public ModeButton[] mode;

        //最大入力幅
        public int maxlength;
        //入力中文字列
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

		CharScreen chscrn;
		//Screen chscrn;
        //入力切替
		InputMode inputmode;
		public InputMode InputMode
		{
			get
			{
				return inputmode;
			}
			set
			{
				inputmode = value;
				switch (inputmode)
				{
					case InputMode.Alpha:
						chscrn = new AlphaScreen(this);
						break;
					case InputMode.Hira:
						chscrn = new HiraScreen(this);
						break;
					case InputMode.Kata:
						chscrn = new KataScreen(this);
						break;
					case InputMode.Num:
						chscrn = new NumScreen(this);
						break;
					case InputMode.None:
						chscrn = new CharScreen();
						break;
					default:
						chscrn = new CharScreen();
						break;
				}
			}
		}

        //
        TargetMode target;
        public TargetMode TargetMode
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
                switch (target)
                {
                    case TargetMode.Title:
                        this.maxlength = 15;
                        break;
                    case TargetMode.Sentence:
                        this.maxlength = 30;
                        break;
                    default:
                        this.maxlength = 15;
                        break;
                }
            }
        }

		public TitleScreen(MakeStoryScreen mss, TargetMode target)
		{
			this.Makescr = mss;
			this.ParentNum = 1;
			init();

			TargetMode = target;
			if (target == TargetMode.Title)
			{
				this.Text = mss.PictureStory.Title;
			}
			else if (target == TargetMode.Sentence)
			{
				this.Text = mss.PictureStory.Slide[mss.NowPage].Sentence;
			}
		}
		public TitleScreen(muphic.LinkMake.Dialog.Save.LinkSaveDialog link)
		{
			this.Linksave = link;
			this.ParentNum = 4;
			init();
			TargetMode = muphic.Titlemode.TargetMode.Title;
			this.Text = link.titlename;
		}
		public TitleScreen(OneScreen one)
		{
			this.Onescr = one;
			this.ParentNum = 5;
			init();
			this.Text = this.Onescr.ScoreTitle;
			TargetMode = muphic.Titlemode.TargetMode.Title;
		}

        private void init()
        {
			//chscrn = new CharScreen(this);
			chscrn = new CharScreen();
			delall = new DelAllButton(this);
            del = new DelButton(this);
            decision = new DecisionButton(this);
            mode = new ModeButton[4];

			this.InputMode = InputMode.Hira;
			//this.InputMode = InputMode.None;

            for (int i = 0; i < 4; i++)
                mode[i] = new ModeButton(this, i + 1);

            mode[1].State = 1;

            ///////////////////////////////////////////////////////////////////
            //部品のテクスチャ・座標の登録
            ///////////////////////////////////////////////////////////////////
            muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\Title\\background_non.png");
			muphic.DrawManager.Regist("sentence", 180, 50, "image\\Title\\sentence.png");
			muphic.DrawManager.Regist("title", 297, 40, "image\\Title\\title.png");
            muphic.DrawManager.Regist("TITLE", 0, 0, "image\\Title\\titleXGA.png");
			muphic.DrawManager.Regist("TitleBack", 100, 170, "image\\Title\\key_back.png");
			muphic.DrawManager.Regist(chscrn.ToString(), 100, 170, "image\\Title\\key_back.png");
            muphic.DrawManager.Regist(delall.ToString(), 798, 656, "image\\Title\\buttons\\delall_off.png", "image\\Title\\buttons\\delall_on.png");
            muphic.DrawManager.Regist(del.ToString(), 637, 656, "image\\Title\\buttons\\del_off.png", "image\\Title\\buttons\\del_on.png");
            muphic.DrawManager.Regist(decision.ToString(), 20, 14, "image\\Title\\buttons\\kettei_off.png", "image\\Title\\buttons\\kettei_on.png");
            muphic.DrawManager.Regist(mode[0].ToString(), 108, 657, "image\\Title\\buttons\\ABC_off.png", "image\\Title\\buttons\\ABC_on.png");
            muphic.DrawManager.Regist(mode[1].ToString(), 211, 657, "image\\Title\\buttons\\hira_off.png", "image\\Title\\buttons\\hira_on.png");
            muphic.DrawManager.Regist(mode[2].ToString(), 315, 657, "image\\Title\\buttons\\kata_off.png", "image\\Title\\buttons\\kata_on.png");
            muphic.DrawManager.Regist(mode[3].ToString(), 419, 657, "image\\Title\\buttons\\num_off.png", "image\\Title\\buttons\\num_on.png");
            ///////////////////////////////////////////////////////////////////
            //部品の画面への登録
            ///////////////////////////////////////////////////////////////////
			//BaseList.Add(chscrn);
			BaseList.Add(delall);
            BaseList.Add(del);
            BaseList.Add(decision);
            BaseList.Add(mode[0]);
            BaseList.Add(mode[1]);
            BaseList.Add(mode[2]);
            BaseList.Add(mode[3]);
        }

		public override void Click(System.Drawing.Point p)
		{
            this.State = this.State == 0 ? 1 : 0;
			base.Click (p);
			chscrn.Click(p);
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
			chscrn.MouseMove(p);
		}

		public override void Draw()
		{
			base.Draw ();
			muphic.DrawManager.Draw("TitleBack");
			chscrn.Draw();
			if(this.TargetMode == muphic.Titlemode.TargetMode.Title)
			{
				muphic.DrawManager.Draw("title");
				muphic.DrawManager.DrawString(this.Text, 315, 68);
			}
			else if(this.TargetMode == muphic.Titlemode.TargetMode.Sentence)
			{
				muphic.DrawManager.Draw("sentence");
				muphic.DrawManager.DrawString(this.Text, 190, 68);
			}
		}
	
	}
}