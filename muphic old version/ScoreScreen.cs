using System;
using System.Collections;
using muphic.ScoreScr;
using muphic.Titlemode;

namespace muphic
{	
	#region SVGA (～ver.0.8.8)
	/*
	/// <summary>
	/// 楽譜画面Screenクラス
	/// </summary>
	public class ScoreScreen : Screen
	{
		
		public Screen parent;				// 親Screenクラス
		public ScreenMode ParentScreenMode;	// 親ScreenのScreenModeを保持

		public ArrayList AnimalList = new ArrayList();		// 動物リストのコピー

		public BackButton back;				// もどるボタン
		public ReadButton read;				// よびだすボタン
		public WriteButton write;			// のこすボタン6
		public UpScrollButton up;			// 上スクロールボタン
		public DownScrollButton down;		// 下スクロールボタン
		public ScoreWindow scorewindow;		// 楽譜表示ウィンドウ
		public ScoreButtons scores;			// 各動物ボタンを管理
		public ScoreMain scoremain;			// 楽譜本体
		public ScoreSelectDialog sedialog;	// 楽譜読み込みダイアログ
		public ScoreSaveDialog sadialog;	// 楽譜保存ダイアログ
		public DeleteTexture delete;		// 画像開放用

		public bool isSelectDialog = false;	// 読み込みダイアログが開かれているかどうかのフラグ
		public bool isSaveDialog = false;	// 保存ダイアログが開かれているかどうかのフラグ

		/// <summary>
		/// 楽譜画面Screenクラス コンストラクタ
		/// ひとりで音楽モードからの呼び出し時に使用
		/// </summary>
		/// <param name="screen">ひとりで音楽(OneScreen)クラス</param>
		public ScoreScreen(OneScreen onescreen)
		{
			this.parent = (Screen)onescreen;				// parentはScreen型なのでキャストして使用
			this.ParentScreenMode = ScreenMode.OneScreen;	// 親ScreenのScreenModeをひとりで音楽モードに設定
			this.ScoreScreenPreparation();					// 各部品のインスタンス化等
		}

		/// <summary>
		/// 楽譜画面Screenクラス コンストラクタ
		/// つなげて音楽モードからの呼び出し時に使用
		/// </summary>
		/// <param name="screen">つなげて音楽(LinkScreen)クラス</param>
		public ScoreScreen(LinkScreen linkscreen)
		{
			this.parent = (Screen)linkscreen;				// parentはScreen型なのでキャストして使用
			this.ParentScreenMode = ScreenMode.LinkScreen;	// 親ScreenのScreenModeをひとりで音楽モードに設定
			this.ScoreScreenPreparation();						// 各部品のインスタンス化等
		}

		/// <summary>
		/// 楽譜画面Screenクラス コンストラクタ
		/// ものがたり音楽モードからの呼び出し時に使用
		/// </summary>
		/// <param name="screen">ものがたり音楽(StoryScreen)クラス</param>
		public ScoreScreen(StoryScreen storyscreen)
		{
			this.parent = (Screen)storyscreen;				// parentはScreen型なのでキャストして使用
			this.ParentScreenMode = ScreenMode.StoryScreen;	// 親ScreenのScreenModeをひとりで音楽モードに設定
			this.ScoreScreenPreparation();					// 各部品のインスタンス化等
		}


		/// <summary>
		/// 楽譜画面コンストラクタ２
		/// 各部品のインスタンス化と登録を行う
		/// </summary>
		private void ScoreScreenPreparation()
		{
			System.Console.WriteLine("楽譜画面呼び出し元:{0}", this.ParentScreenMode);
			this.AnimalList.Clear();

			// 呼び出し元Screenに毎 それぞれのAnimalListをコピーする
			switch(this.ParentScreenMode)
			{
				case ScreenMode.OneScreen:
					this.AnimalList = ((OneScreen)this.parent).score.Animals.AnimalList;
					break;
				case ScreenMode.LinkScreen:
					for(int i=0; i<((LinkScreen)this.parent).score.AnimalList.Count; i++)
					{
						// LinkScreenのAnimalListから要素をひとつひとつ取り出し、動物名を"Sheep"にして手動コピー
						Animal temp = (Animal)(((LinkScreen)this.parent).score.AnimalList[i]);
						Animal animal = new Animal(temp.place, temp.code, "Sheep");
						this.AnimalList.Add(animal);
					}
					// this.AnimalList = ((LinkScreen)this.parent).score.AnimalList;
					break;
				case ScreenMode.StoryScreen:
					this.AnimalList = ((StoryScreen)this.parent).score.Animals.AnimalList;
					break;
				default:
					break;
			}

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			back = new BackButton(this);
			read = new ReadButton(this);
			write = new WriteButton(this);
			up = new UpScrollButton(this);
			down = new DownScrollButton(this);
			scorewindow = new ScoreWindow(this);
			scores = new ScoreButtons(this);
			scoremain = new ScoreMain(this);
			sedialog = new ScoreSelectDialog(this);
			sadialog = new ScoreSaveDialog(this);
			delete = new DeleteTexture(this);


			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\Score\\score.png");	// 本当の背景
			//muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\Score\\gakuhu.png");	// ボタン配置見本背景
			muphic.DrawManager.Regist(back.ToString(), 25, 7, "image\\Score\\button\\modoru.png", "image\\score\\button\\modoru_on.png");
			muphic.DrawManager.Regist(read.ToString(), 403, 15, "image\\Score\\button\\yobidasu.png", "image\\score\\button\\yobidasu_on.png");
			muphic.DrawManager.Regist(write.ToString(), 576, 16, "image\\Score\\button\\nokosu.png", "image\\score\\button\\nokosu_on.png");
			muphic.DrawManager.Regist(up.ToString(), 704, 126, "image\\Score\\button\\up.png", "image\\score\\button\\up.png");
			muphic.DrawManager.Regist(down.ToString(), 704, 488, "image\\Score\\button\\down.png", "image\\score\\button\\down.png");
			muphic.DrawManager.Regist(scorewindow.ToString(), 33, 124, "image\\Score\\score_haikei.png");
			muphic.DrawManager.Regist(scores.ToString(), 35, 533, "image\\Score\\scores.png");
			
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(back);
			BaseList.Add(read);
			BaseList.Add(write);
			BaseList.Add(scorewindow);
			BaseList.Add(up);
			BaseList.Add(down);
			BaseList.Add(scores);
			BaseList.Add(scoremain);
		}

		public override void Draw()
		{
			base.Draw ();
			if(isSelectDialog)
			{
				sedialog.Draw();
			}
			else if(isSaveDialog)
			{
				sadialog.Draw();
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
			if(isSelectDialog)
			{
				sedialog.MouseMove(p);
			}
			else if(isSaveDialog)
			{
				sadialog.MouseMove(p);
			}
		}

		public override void Click(System.Drawing.Point p)
		{
			if(isSelectDialog)
			{
				sedialog.Click(p);
			}
			else if(isSaveDialog)
			{
				sadialog.Click(p);
			}
			else
			{
				base.Click (p);
			}
		}
	}
		
	*/
	#endregion
		
	#region XGA (ver.0.9.0～)

	/// <summary>
	/// 楽譜画面Screenクラス
	/// </summary>
	public class ScoreScreen : Screen
	{
		public Screen parent;				// 親Screenクラス
		public ScreenMode ParentScreenMode;	// 親ScreenのScreenModeを保持

		public ArrayList AnimalList = new ArrayList();		// 動物リストのコピー

		public BackButton back;				// もどるボタン
		public ReadButton read;				// よびだすボタン
		public WriteButton write;			// のこすボタン6
		public UpScrollButton up;			// 上スクロールボタン
		public DownScrollButton down;		// 下スクロールボタン
		public ScoreWindow scorewindow;		// 楽譜表示ウィンドウ
		public ScoreButtons scores;			// 各動物ボタンを管理
		public ScoreMain scoremain;			// 楽譜本体
		public TitleScreen title;			// 題名入力ダイアログ
		public ScoreSelectDialog sedialog;	// 楽譜読み込みダイアログ
		public ScoreSaveDialog sadialog;	// 楽譜保存ダイアログ
		public ScorePrintDialog pdialog;	// 印刷確認ダイアログ
		public PrintButton print;			// 楽譜印刷
		public ScoreTitleArea tarea;		// 曲名表示
		public DeleteTexture delete;		// 画像開放用
		
		public bool isSelectDialog = false;	// 読み込みダイアログが開かれているかどうかのフラグ
		public bool isSaveDialog = false;	// 保存ダイアログが開かれているかどうかのフラグ
		public bool isPrintDialog = false;	// 印刷確認ダイアログが開かれているかどうかのフラグ
		public bool isTitleScreen = false;	// 題名入力画面かどうかのフラグ

		/// <summary>
		/// 楽譜画面Screenクラス コンストラクタ
		/// ひとりで音楽モードからの呼び出し時に使用
		/// </summary>
		/// <param name="screen">ひとりで音楽(OneScreen)クラス</param>
		public ScoreScreen(OneScreen onescreen)
		{
			this.parent = (Screen)onescreen;				// parentはScreen型なのでキャストして使用
			this.ParentScreenMode = ScreenMode.OneScreen;	// 親ScreenのScreenModeをひとりで音楽モードに設定
			this.ScoreScreenPreparation();					// 各部品のインスタンス化等
		}
		
		/// <summary>
		/// 楽譜画面Screenクラス コンストラクタ
		/// つなげて音楽モードからの呼び出し時に使用
		/// </summary>
		/// <param name="screen">つなげて音楽(LinkScreen)クラス</param>
		public ScoreScreen(LinkScreen linkscreen)
		{
			this.parent = (Screen)linkscreen;				// parentはScreen型なのでキャストして使用
			this.ParentScreenMode = ScreenMode.LinkScreen;	// 親ScreenのScreenModeをひとりで音楽モードに設定
			this.ScoreScreenPreparation();					// 各部品のインスタンス化等
		}

		/// <summary>
		/// 楽譜画面Screenクラス コンストラクタ
		/// つなげて音楽モードからの呼び出し時に使用
		/// </summary>
		/// <param name="linkmakescreen">つなげて音楽(LinkMakeScreen)クラス</param>
		public ScoreScreen(LinkMakeScreen linkscreen)
		{
			this.parent = (Screen)linkscreen;					// parentはScreen型なのでキャストして使用
			this.ParentScreenMode = ScreenMode.LinkMakeScreen;	// 親ScreenのScreenModeをひとりで音楽モードに設定
			this.ScoreScreenPreparation();						// 各部品のインスタンス化等
		}
		
		/// <summary>
		/// 楽譜画面Screenクラス コンストラクタ
		/// ものがたり音楽モードからの呼び出し時に使用
		/// </summary>
		/// <param name="screen">ものがたり音楽(StoryScreen)クラス</param>
		public ScoreScreen(StoryScreen storyscreen)
		{
			this.parent = (Screen)storyscreen;					// parentはScreen型なのでキャストして使用
			this.ParentScreenMode = ScreenMode.StoryScreen;		// 親ScreenのScreenModeをひとりで音楽モードに設定
			this.ScoreScreenPreparation();						// 各部品のインスタンス化等
		}
		
		
		/// <summary>
		/// 楽譜画面コンストラクタ２
		/// 各部品のインスタンス化と登録を行う
		/// </summary>
		private void ScoreScreenPreparation()
		{			
			System.Console.WriteLine("楽譜画面呼び出し元:{0}", this.ParentScreenMode);
			this.AnimalList.Clear();

			// 呼び出し元Screenに毎 それぞれのAnimalListをコピーする
			switch(this.ParentScreenMode)
			{
				case ScreenMode.OneScreen:
					this.AnimalList = ((OneScreen)this.parent).score.Animals.AnimalList;
					break;
				case ScreenMode.LinkScreen:
					for(int i=0; i<((LinkScreen)this.parent).score.AnimalList.Count; i++)
					{
						// LinkScreenのAnimalListから要素をひとつひとつ取り出し、動物名を"Sheep"にして手動コピー
						Animal temp = (Animal)(((LinkScreen)this.parent).score.AnimalList[i]);
						Animal animal = new Animal(temp.place, temp.code, "Sheep");
						this.AnimalList.Add(animal);
					}
					// this.AnimalList = ((LinkScreen)this.parent).score.AnimalList;
					break;
				case ScreenMode.LinkMakeScreen:
					for(int i=0;i<((LinkMakeScreen)this.parent).score.Animals.AnimalList.Count; i++)
					{
						// LinkMakeScreenのAnimalsのAnimalListから要素をひとつひとつ取り出し、動物名を"Sheep"にして手動コピー
						Animal temp = (Animal)(((LinkMakeScreen)this.parent).score.Animals[i]);
						Animal animal = new Animal(temp.place, temp.code, "Sheep");
						this.AnimalList.Add(animal);
					}
					break;
				case ScreenMode.StoryScreen:
					this.AnimalList = ((StoryScreen)this.parent).score.Animals.AnimalList;
					break;
				default:
					break;
			}

			// NowLoading 呼び出し
			DrawManager.BeginRegist(67);

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			back = new BackButton(this);
			//read = new ReadButton(this);
			//write = new WriteButton(this);
			up = new UpScrollButton(this);
			down = new DownScrollButton(this);
			scorewindow = new ScoreWindow(this);
			scores = new ScoreButtons(this);
			scoremain = new ScoreMain(this);
			//title = new TitleScreen(this);
			//sedialog = new ScoreSelectDialog(this);
			//sadialog = new ScoreSaveDialog(this);
			print = new PrintButton(this);
			pdialog = new ScorePrintDialog(this);
			tarea = new ScoreTitleArea(this);
			//delete = new DeleteTexture(this);


			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\ScoreXGA\\background.png");	// 本当の背景
			//muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\ScoreXGA\\scoreXGA.png");	// ボタン配置見本背景
			muphic.DrawManager.Regist(back.ToString(), 15, 10, "image\\ScoreXGA\\button\\back_off.png", "image\\ScoreXGA\\button\\back_on.png");
			//muphic.DrawManager.Regist(read.ToString(), 489, 24, "image\\ScoreXGA\\button\\load_off.png", "image\\ScoreXGA\\button\\load_on.png");
			//muphic.DrawManager.Regist(write.ToString(), 655, 24, "image\\ScoreXGA\\button\\save_off.png", "image\\ScoreXGA\\button\\save_on.png");
			muphic.DrawManager.Regist(print.ToString(), 821, 24, "image\\ScoreXGA\\button\\print_off.png", "image\\ScoreXGA\\button\\print_on.png");
			muphic.DrawManager.Regist(up.ToString(), 936, 142, "image\\ScoreXGA\\scrool\\up.png");
			muphic.DrawManager.Regist(down.ToString(), 936, 628, "image\\ScoreXGA\\scrool\\down.png");
			muphic.DrawManager.Regist(scorewindow.ToString(), 37, 125, "image\\ScoreXGA\\score_back.png", "image\\ScoreXGA\\score_back_.png");
			muphic.DrawManager.Regist(scores.ToString(), 75, 680, "image\\ScoreXGA\\scores.png");
			
			// NowLoading 終わり
			DrawManager.EndRegist();
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(back);
			//BaseList.Add(read);
			//BaseList.Add(write);
			BaseList.Add(print);
			BaseList.Add(scorewindow);
			BaseList.Add(up);
			BaseList.Add(down);
			BaseList.Add(scores);
			BaseList.Add(scoremain);
		}

		public override void Draw()
		{
			base.Draw ();
			tarea.Draw();
			if(isTitleScreen)
			{
				title.Draw();
			}
			else if(isSelectDialog)
			{
				sedialog.Draw();
			}
			else if(isSaveDialog)
			{
				sadialog.Draw();
			}
			else if(isPrintDialog)
			{
				pdialog.Draw();
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			if(isTitleScreen)
			{
				title.MouseMove(p);
			}
			else if(isSelectDialog)
			{
				sedialog.MouseMove(p);
			}
			else if(isSaveDialog)
			{
				sadialog.MouseMove(p);
			}
			else if(isPrintDialog)
			{
				pdialog.MouseMove(p);
			}
			else
			{
				base.MouseMove (p);
			}
		}

		public override void Click(System.Drawing.Point p)
		{
			if(isTitleScreen)
			{
				title.Click(p);
			}
			else if(isSelectDialog)
			{
				sedialog.Click(p);
			}
			else if(isSaveDialog)
			{
				sadialog.Click(p);
			}
			else if(isPrintDialog)
			{
				pdialog.Click(p);
			}
			else
			{
				base.Click (p);
			}
		}
	}
	
	#endregion
}
