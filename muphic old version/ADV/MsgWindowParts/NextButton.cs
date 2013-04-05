using System;
using muphic.Common;

namespace muphic.ADV.MsgWindowParts
{
	/// <summary>
	/// NextButton の概要の説明です。
	/// </summary>
	public class NextButton : Base
	{
		public MsgWindow parent;
		
		public NextButton(MsgWindow msgwindow)
		{
			this.parent = msgwindow;
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// チュートリアル実行中はクリックだけで勝手に進むんでパス
			if(!TutorialStatus.getIsTutorial())
			{	
				parent.parent.NextState();
			}
			else if(TutorialStatus.getIsSPMode() == "PT02_Link30" || TutorialStatus.getIsSPMode() == "PT03_Story50" || TutorialStatus.getIsSPMode() == "PT04_One02")
			{
				// 自由操作中の場合はヒントで出てるハズなので、次へボタンでウィンドウを消す
				this.parent.ChangeWindowCoordinate(0);
			}
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
