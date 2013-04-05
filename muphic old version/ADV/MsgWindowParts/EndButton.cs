using System;
using muphic.Common;

namespace muphic.ADV.MsgWindowParts
{
	/// <summary>
	/// BackButton の概要の説明です。
	/// </summary>
	public class EndButton : Base
	{
		public MsgWindow parent;
		
		public EndButton(MsgWindow msgwindow)
		{
			this.parent = msgwindow;
			
			// チュートリアルでなければ表示させない
			this.Visible = TutorialStatus.getIsTutorial()? true : false;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// ダイアログ表示
			((TutorialScreen)this.parent.parent.parent).tutorialmain.isEndDialog = true;
			
			// チュートリアル管理側のダイアログが表示されたことをTutorialStatusに知らせる
			TutorialStatus.setEnableTutorialDialog();
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
