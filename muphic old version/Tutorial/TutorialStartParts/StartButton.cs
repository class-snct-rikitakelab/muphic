using System;

namespace muphic.Tutorial.TutorialStartParts
{
	/// <summary>
	/// StartButton の概要の説明です。
	/// </summary>
	public class StartButton : Base
	{
		public TutorialStart parent;
		
		public StartButton(TutorialStart ts)
		{
			this.parent = ts;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// チュートリアル画面 チュートリアルの開始
			// 続きからスタートにはしない
			parent.parnet.StartTutorial(false);
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
