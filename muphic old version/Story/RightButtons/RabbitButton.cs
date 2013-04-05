using System;

namespace muphic.Story.RightButtons
{
	/// <summary>
	/// RabbitButton の概要の説明です。
	/// </summary>
	public class RabbitButton : Base
	{
		public StoryButtons parent;
		public RabbitButton(StoryButtons stories)
		{
			parent = stories;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.Story.StoryButtonsClickMode.Rabbit)		//既にウサギボタンを選択した状態なら
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.None;		//何も選択していない状態にする
				this.State = 0;												//自分の選択解除
			}
			else															//ウサギボタンを選択していないなら
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.Rabbit;		//ウサギを選択している状態にする
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
			if(parent.NowClick != muphic.Story.StoryButtonsClickMode.Rabbit)
			{
				this.State = 0;
			}
		}
	}
}
