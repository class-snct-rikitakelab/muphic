using System;

namespace muphic.One.RightButtons
{
	/// <summary>
	/// RabbitButton の概要の説明です。
	/// </summary>
	public class RabbitButton : Base
	{
		public OneButtons parent;
		public RabbitButton(OneButtons ones)
		{
			parent = ones;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.One.OneButtonsClickMode.Rabbit)		//既にウサギボタンを選択した状態なら
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.None;		//何も選択していない状態にする
				this.State = 0;												//自分の選択解除
			}
			else															//ウサギボタンを選択していないなら
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.Rabbit;		//ウサギを選択している状態にする
				this.State = 1;												//自分を選択状態にする
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
			if(parent.NowClick != muphic.One.OneButtonsClickMode.Rabbit)
			{
				this.State = 0;
			}
		}
	}
}
