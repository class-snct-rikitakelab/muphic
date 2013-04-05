using System;

namespace muphic.Link
{
	/// <summary>
	/// ClearButton の概要の説明です。
	/// </summary>
	public class ClearButton : Base
	{
		LinkScreen parent;
		public ClearButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);

			if (!parent.score.isPlay && !parent.score.answerCheckFlag)
			{
				parent.links.BaseState0();
				if(parent.links.NowClick == muphic.Link.LinkButtonsClickMode.Cancel)		//既にキャンセルボタンを選択した状態なら
				{
					parent.links.NowClick = muphic.Link.LinkButtonsClickMode.None;		//何も選択していない状態にする
					this.State = 0;												//自分の選択解除
				}
				else															//キャンセルボタンを選択していないなら
				{
					parent.links.NowClick = muphic.Link.LinkButtonsClickMode.Cancel;		//キャンセルを選択している状態にする
					this.State = 1;												//自分を選択状態にする
				}
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
			if(parent.links.NowClick != muphic.Link.LinkButtonsClickMode.Cancel)
			{
				this.State = 0;
			}
		}
	}
}


