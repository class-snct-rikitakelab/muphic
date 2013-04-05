using System;
using muphic.Tutorial.TutorialStartParts;

namespace muphic.Tutorial
{
	/// <summary>
	/// TutorialStart の概要の説明です。
	/// </summary>
	public class TutorialStart : Screen
	{
		public TutorialScreen parnet;
		
		public StartButton startbutton;
		public ContinueButton continuebutton;
		public ChapterStartButton chapterstartbutton;
		public BackButton backbutton;
		
		public bool Chapter;
		
		/// <summary>
		/// コンストラクタ
		/// こちらは、主にチュートリアル開始時に呼ばれる
		/// </summary>
		/// <param name="tscreen">親のスクリーン</param>
		public TutorialStart(TutorialScreen tscreen)
		{
			this.parnet = tscreen;
			this.Chapter = false;
			
			// スタートボタンをインスタンス化し、画像を登録
			startbutton = new StartButton(this);
			DrawManager.Regist(startbutton.ToString(), 223, 514, "image\\TutorialXGA\\TutorialStart\\start_off.png",  "image\\TutorialXGA\\TutorialStart\\start_on.png");
			BaseList.Add(startbutton);
			
			// 続きからボタンをインスタンス化し、画像を登録
			continuebutton = new ContinueButton(this);
			DrawManager.Regist(continuebutton.ToString(), 548, 514, new string[] {"image\\TutorialXGA\\TutorialStart\\continue_off.png",  "image\\TutorialXGA\\TutorialStart\\continue_on.png", "image\\TutorialXGA\\TutorialStart\\continue_off_off.png"});
			BaseList.Add(continuebutton);

			// 背景画像はあらかじめ決められたタイトル画像にする
			if(FileNameManager.GetFileExist(this.ToString())) DrawManager.Delete(this.ToString());
			DrawManager.Regist(this.ToString(), 0, 0, "image\\TutorialXGA\\TutorialStart\\TutorialStart_bak.png");
			
			TutorialStartConstructor2();
		}
		
		/// <summary>
		/// コンストラクタ
		/// こちらは、主にチャプター開始時に呼ばれる
		/// </summary>
		/// <param name="tscreen">親のスクリーン</param>
		/// <param name="filename"></param>
		public TutorialStart(TutorialScreen tscreen, string filename)
		{
			this.parnet = tscreen;
			this.Chapter = true;
			
			// 各チャプターのスタートボタンをインスタンス化し、画像を登録
			chapterstartbutton = new ChapterStartButton(this);
			DrawManager.Regist(chapterstartbutton.ToString(), 362, 500, "image\\TutorialXGA\\TutorialStart\\chapterstart_off.png",  "image\\TutorialXGA\\TutorialStart\\chapterstart_on.png");
			BaseList.Add(chapterstartbutton);
			
			// 背景画像は引数で渡された画像にする
			if(FileNameManager.GetFileExist(this.ToString())) DrawManager.Delete(this.ToString());
			DrawManager.Regist(this.ToString(), 0, 0, filename);
			
			TutorialStartConstructor2();
		} 

		public void TutorialStartConstructor2()
		{
			// 両モードに共通するのは、戻るボタン
			// 戻るボタンをインスタンス化し、画像を登録
			backbutton = new BackButton(this);
			DrawManager.Regist(backbutton.ToString(), 434, 659, "image\\TutorialXGA\\TutorialStart\\back_off.png",  "image\\TutorialXGA\\TutorialStart\\back_on.png");
			BaseList.Add(backbutton);
		}

		public override void Draw()
		{
			base.Draw ();
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}
	}
}
