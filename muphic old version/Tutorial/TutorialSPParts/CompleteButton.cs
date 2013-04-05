using System;

namespace muphic.Tutorial.TutorialSPParts
{
	/// <summary>
	/// CompleteButton の概要の説明です。
	/// </summary>
	public class CompleteButton : Base
	{
		public TutorialMain parent;
		
		public CompleteButton(TutorialMain tm)
		{
			this.parent = tm;
		}
		
		public bool Check()
		{
			OneScreen one = ((OneScreen)this.parent.topscreen.Screen);
			
			// 正解の条件 動物が10匹以上、小節数が４以上であること
			if( !(one.score.Animals.AnimalList.Count >= 10 && ((Animal)one.score.Animals.AnimalList[one.score.Animals.AnimalList.Count-1]).place >= 24) )
			{
				this.parent.msgwindow.getText(new string[] {"これでは　みじかすぎて　のろいが　とけない。", "もういちど　がんばってみよう！"});
				this.parent.msgwindow.ChangeWindowCoordinate(1);
				this.parent.SetVoice("PT04_One02_1.mp3");
				
				return false;
			}
			
			// 上記をクリアしたら正解
			muphic.Common.TutorialStatus.setDisableIsSPMode();		// 次へボタンクリックでステート進行できるようにする
			this.parent.msgwindow.getText(new string[] {"きょくができたね！　これで　のろいは　とける　はずだ！" });
			this.parent.msgwindow.ChangeWindowCoordinate(1);
			this.parent.SetVoice("PT04_One02_2.mp3");
			
			return true;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// チェックボタンがクリックされたら楽譜の採点(？)を行い、その結果に基き特殊コマンドモードを再度呼び出す
			this.parent.SPCommand("PT04_One02_" + this.Check().ToString());
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
