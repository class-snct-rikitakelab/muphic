using System;

namespace muphic.LinkMake.ScoreParts
{
	/// <summary>
	/// ClearButton の概要の説明です。
	/// </summary>
	public class ClearButton : Base
	{
		Score parent;
		public ClearButton(Score one)
		{
			parent = one;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.parent.linkmakes.NowClick == muphic.LinkMake.LinkMakeButtonsClickMode.Cancel)		//既にボタンを選択した状態なら
			{
				parent.parent.linkmakes.NowClick = muphic.LinkMake.LinkMakeButtonsClickMode.None;		//何も選択していない状態にする
				this.State = 0;												//自分の選択解除
			}
			else															//鳥ボタンを選択していないなら
			{
				parent.parent.linkmakes.NowClick = muphic.LinkMake.LinkMakeButtonsClickMode.Cancel;		//鳥を選択している状態にする
				this.State = 1;												//自分を選択状態にする
			}
			//parent.Animals.Delete();
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.parent.linkmakes.NowClick != muphic.LinkMake.LinkMakeButtonsClickMode.Cancel)
			{
				this.State = 0;
			}
		}
	}
}
