using System;

namespace muphic.Link.RightButtons
{
	/// <summary>
	/// SheepButton の概要の説明です。
	/// </summary>
	public class Sheep09Button : Base
	{
		public LinkButtons parent;
		public Sheep09Button(LinkButtons links)
		{
			parent = links;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.parent.score.isPlay && !parent.parent.score.answerCheckFlag)
			{
				if(parent.NowClick == muphic.Link.LinkButtonsClickMode.Sheep09)		//既に羊ボタンを選択した状態なら
				{
					parent.NowClick = muphic.Link.LinkButtonsClickMode.None;		//何も選択していない状態にする
					this.State = 0;												//自分の選択解除
				}
				else															//羊ボタンを選択していないなら
				{
					parent.NowClick = muphic.Link.LinkButtonsClickMode.Sheep09;		//羊を選択している状態にする
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
			if(parent.NowClick != muphic.Link.LinkButtonsClickMode.Sheep09)
			{
				this.State = 0;
			}
		}
	}
}
