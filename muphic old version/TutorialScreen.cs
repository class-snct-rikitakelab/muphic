using System;
using System.IO;
using muphic.Common;
using muphic.Tutorial;

namespace muphic
{
	/// <summary>
	/// TutorialScreen の概要の説明です。
	/// </summary>
	public class TutorialScreen : Screen
	{
		public TopScreen parent;

		public TutorialStart tutorialstart;
		public TutorialMain tutorialmain;

		public bool isTutorialStart;	// スタート画面かどうかのフラグ
		
		public const string TutorialPass = "TutorialData\\TutorialMain";
		
		/// <summary>
		/// Tutorialのコンストラクタ
		/// </summary>
		/// <param name="screen">親Screen</param>
		public TutorialScreen(TopScreen topscreen)
		{
			this.parent = topscreen;
			
			// フェード効果の設定
			DrawManager.FadeOutEvent += new FadeEventHandler(this.Draw);
			DrawManager.FadeInEvent += new FadeEventHandler(this.Draw);
			
			// スタート画面のインスタンス化
			tutorialstart = new TutorialStart(this);
			
			// スタート画面をonにする
			this.isTutorialStart = true;
		}
		
		
		/// <summary>
		/// チュートリアルを開始する
		/// </summary>
		public void StartTutorial(bool continueflag)
		{
			// スタート画面off
			this.isTutorialStart = false;
			
			// チュートリアルの実行をTutorialStatusに通知
			TutorialStatus.setEnableTutorial();
			
			// NowLoading画面呼び出し
			DrawManager.BeginRegist(45);
			
			// チュートリアル画面のインスタンス化
			this.tutorialmain = new TutorialMain(this, TutorialPass, continueflag);
			
			// NowLoading画面終了
			//DrawManager.EndRegist();

			// チュートリアル開始 最初のチャプターへ
			this.tutorialmain.NextChapter();
		}
		
		
		/// <summary>
		/// チュートリアルを終了する
		/// </summary>
		public void EndTutorial()
		{
			// チュートリアル終了
			TutorialStatus.initTutorialStatus();
			
			// Top画面に戻る
			this.parent.ScreenMode = muphic.ScreenMode.TopScreen;
			
			Console.WriteLine("チュートリアル終了");
		}
		
		public string getTutorialPass()
		{
			return TutorialScreen.TutorialPass;
		}
		
		
		/// <summary>
		/// 描画メソッド
		/// </summary>
		public override void Draw()
		{
			base.Draw();

			// スタート画面がonなら
			if(isTutorialStart)
			{
				// スタート画面のみ描画して終わり
				this.tutorialstart.Draw();
				return;
			}
			
			// チュートリアルの描画
			this.tutorialmain.Draw();
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			// スタート画面がonなら
			if(isTutorialStart)
			{
				// スタート画面の動作のみ
				this.tutorialstart.Click(p);
				return;
			}
			
			// チュートリアルのクリック動作
			this.tutorialmain.Click(p);
		}
		
		
		public override void MouseMove(System.Drawing.Point p)
		{
			// スタート画面がonなら
			if(isTutorialStart)
			{
				// スタート画面の動作のみ
				this.tutorialstart.MouseMove(p);
				return;
			}
			
			// チュートリアルの動作
			this.tutorialmain.MouseMove(p);
		}
	}
}
