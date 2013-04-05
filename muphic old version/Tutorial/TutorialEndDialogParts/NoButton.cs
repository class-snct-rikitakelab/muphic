using System;

namespace muphic.Tutorial.TutorialEndDialogParts
{
	/// <summary>
	/// NoButton の概要の説明です。
	/// </summary>
	public class NoButton : Base
	{
		public TutorialEndDialog parent;
		
		public NoButton(TutorialEndDialog dialog)
		{
			this.parent = dialog;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// ダイアログ非表示
			parent.parent.isEndDialog = false;
			
			// チュートリアル管理側のダイアログが非表示になったことをTutorialStatusに通知
			muphic.Common.TutorialStatus.setDisableTutorialDialog();
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
