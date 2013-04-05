using System;
using System.Drawing;

namespace muphic.MakeStory
{
	/// <summary>
	/// DogButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class DogButton : Base
	{
		public MakeStoryScreen parent;

		public DogButton(MakeStoryScreen mss)
		{
			parent = mss;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Wolf)
			{
				parent.ButtonsMode = muphic.MakeStory.ButtonsMode.None;
				this.State = 0;
			}
			else
			{
				if(parent.isClear)
					parent.cb.Reset();
				parent.ButtonsMode = muphic.MakeStory.ButtonsMode.Wolf;
				this.State = 1;
				parent.man.State = 0;
				parent.lady.State = 0;
				parent.girl.State = 0;
				parent.boy.State = 0;

				parent.bear.State = 0;
				parent.turtle.State = 0;
				parent.bird.State = 0;
				
				parent.forest.State = 0;
				parent.river.State = 0;
				parent.town.State = 0;
				parent.room.State = 0;

				parent.goods.State = 0;
				parent.fashion.State = 0;
				parent.food.State = 0;
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
			if(parent.ButtonsMode != muphic.MakeStory.ButtonsMode.Wolf)
			{
				this.State = 0;
			}
		}
	}
}
