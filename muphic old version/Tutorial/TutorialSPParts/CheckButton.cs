using System;

namespace muphic.Tutorial.TutorialSPParts
{
	/// <summary>
	/// CheckButton の概要の説明です。
	/// </summary>
	public class CheckButton : Base
	{
		public TutorialMain parent;
		
		public CheckButton(TutorialMain tm)
		{
			this.parent = tm;
		}
		
		
		public bool Check()
		{
			StoryScreen story = (StoryScreen)((MakeStoryScreen)this.parent.topscreen.Screen).Screen;
			
			// 正解の条件1 動物を4つ使っていること
			if( story.score.Animals.AnimalList.Count != 4)
			{
				this.parent.msgwindow.getText(new string[] {"まだまだ、かなしそう　じゃないかも。", "どうぶつは　ちゃんと　よっつつかってね。", "もういちど　がんばってみよう！" });
				this.parent.msgwindow.ChangeWindowCoordinate(1);
				this.parent.SetVoice("PT03_Story50_1.mp3");
				
				return false;
			}
			
			// 正解の条件2 全ての音の音階が下がっていく
			int code0 = ((Animal)story.score.Animals.AnimalList[0]).code;
			int code1 = ((Animal)story.score.Animals.AnimalList[1]).code;
			int code2 = ((Animal)story.score.Animals.AnimalList[2]).code;
			int code3 = ((Animal)story.score.Animals.AnimalList[3]).code;
			if( !((code0 < code1) && (code1 < code2) && (code2 < code3)) )
			{
				this.parent.msgwindow.getText(new string[] {"まだまだ、かなしそう　じゃないかも。", "おとのたかさをを　さげていくんだよ。", "もういちど　がんばってみよう！" });
				this.parent.msgwindow.ChangeWindowCoordinate(1);
				this.parent.SetVoice("PT03_Story50_2.mp3");
				
				return false;
			}
			
			// 上記2つをクリアしたら正解
			muphic.Common.TutorialStatus.setDisableIsSPMode();		// 次へボタンクリックでステート進行できるようにする
			this.parent.msgwindow.getText(new string[] {"いいメロディができたね！", "これで　てしたは　やっつけられるはずだ！" });
			this.parent.msgwindow.ChangeWindowCoordinate(1);
			this.parent.SetVoice("PT03_Story50_3.mp3");
			
			return true;
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// チェックボタンがクリックされたら楽譜の採点(？)を行い、その結果に基き特殊コマンドモードを再度呼び出す
			this.parent.SPCommand("PT03_Story50_" + this.Check().ToString());
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
