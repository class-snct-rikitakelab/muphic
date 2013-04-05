using System;

namespace muphic.Link.RightButtons
{
	/// <summary>
	/// CancelButton の概要の説明です。
	/// </summary>
	public class CancelButton : Base
	{
		public LinkButtons parent;
		public CancelButton(LinkButtons links)
		{
			parent = links;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.Link.LinkButtonsClickMode.Cancel)		//既にキャンセルボタンを選択した状態なら
			{
				parent.NowClick = muphic.Link.LinkButtonsClickMode.None;		//何も選択していない状態にする
				this.State = 0;												//自分の選択解除
			}
			else															//キャンセルボタンを選択していないなら
			{
				parent.NowClick = muphic.Link.LinkButtonsClickMode.Cancel;		//キャンセルを選択している状態にする
				this.State = 1;												//自分を選択状態にする
			}
		}
	}
}
