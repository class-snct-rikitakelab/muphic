using System;

namespace muphic.Tutorial.TutorialStartParts
{
	/// <summary>
	/// StartButton の概要の説明です。
	/// </summary>
	public class ChapterStartButton : Base
	{
		public TutorialStart parent;
		
		public ChapterStartButton(TutorialStart ts)
		{
			this.parent = ts;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// スタートボタン押したらステート進行でいいや
			this.parent.parnet.tutorialmain.NextState();
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
