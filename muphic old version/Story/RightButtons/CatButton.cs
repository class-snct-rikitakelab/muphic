using System;

namespace muphic.Story.RightButtons
{
	/// <summary>
	/// CatButton の概要の説明です。
	/// </summary>
	public class CatButton : Base
	{
		public StoryButtons parent;
		public CatButton(StoryButtons stories)
		{
			parent = stories;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.NowClick == muphic.Story.StoryButtonsClickMode.Cat)//既に猫ボタンを選択した状態なら
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.None;	//何も選択していない状態にする
				this.State = 0;												//自分の選択解除
			}
			else															//猫ボタンを選択していないなら
			{
				parent.NowClick = muphic.Story.StoryButtonsClickMode.Cat;	//猫を選択している状態にする
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
			if(parent.NowClick != muphic.Story.StoryButtonsClickMode.Cat)
			{
				this.State = 0;
			}
		}
	}
}
