using System;

namespace muphic.One.RightButtons
{
	/// <summary>
	/// PigButton の概要の説明です。
	/// </summary>
	public class PigButton : Base
	{
		public OneButtons parent;
		public PigButton(OneButtons ones)
		{
			parent = ones;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.One.OneButtonsClickMode.Pig)		//既に豚ボタンを選択した状態なら
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.None;		//何も選択していない状態にする
				this.State = 0;												//自分の選択解除
			}
			else															//豚ボタンを選択していないなら
			{
				parent.NowClick = muphic.One.OneButtonsClickMode.Pig;		//豚を選択している状態にする
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
			if(parent.NowClick != muphic.One.OneButtonsClickMode.Pig)
			{
				this.State = 0;
			}
		}
	}
}
