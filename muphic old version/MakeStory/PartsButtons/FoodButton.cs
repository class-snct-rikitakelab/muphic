using System;
using System.Drawing;

namespace muphic.MakeStory
{
	/// <summary>
	/// FoodButton �̊T�v�̐����ł��B
	/// </summary>
	public class FoodButton : Base
	{
		public MakeStoryScreen parent;

		public FoodButton(MakeStoryScreen mss)
		{
			parent = mss;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Food)
			{
				parent.ButtonsMode = muphic.MakeStory.ButtonsMode.None;
				this.State = 0;
			}
			else
			{
				if(parent.isClear)
					parent.cb.Reset();
				parent.ButtonsMode = muphic.MakeStory.ButtonsMode.Food;
				this.State = 1;

				parent.man.State = 0;
				parent.lady.State = 0;
				parent.girl.State = 0;
				parent.boy.State = 0;

				parent.dog.State = 0;
				parent.bear.State = 0;
				parent.turtle.State = 0;
				parent.bird.State = 0;

				parent.forest.State = 0;
				parent.river.State = 0;
				parent.town.State = 0;
				parent.room.State = 0;

				parent.goods.State = 0;
				parent.fashion.State = 0;
				parent.furniture.State = 0;

				parent.cb.State = 0;
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
			if(parent.ButtonsMode != muphic.MakeStory.ButtonsMode.Food)
			{
				this.State = 0;
			}
		}
	}
}
