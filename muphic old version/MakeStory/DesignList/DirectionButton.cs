using System;
using System.Windows.Forms;
using muphic;
using muphic.MakeStory;

namespace muphic.MakeStory.DesignList
{
	/// <summary>
	/// DirectionButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class DirectionButton : Base
	{
		public LivesDesignList parent;
		public int name;

		public DirectionButton(LivesDesignList dl, int name)
		{
			this.parent = dl;
			this.name = name;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click(p);
			if (this.parent.DirectionMode == (DirectionMode)name)
			{
				this.parent.DirectionMode = muphic.MakeStory.DesignList.DirectionMode.None;
				this.State = 0;
			}
			else
			{
				if(parent.parent.isClear)
					parent.parent.cb.Reset();
				for (int i = 0; i < parent.DirectB.Length; i++)
					parent.DirectB[i].State = 0;
				this.parent.DirectionMode = (DirectionMode)name;
				this.State = 1;
			}
		}

		public override void MouseEnter()
		{
			this.State = 1;
			base.MouseEnter ();
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.DirectionMode != (DirectionMode)this.name)
			{
				this.State = 0;
			}
		}


		public override string ToString()
		{
			return parent.ToString() + (DirectionMode)name +"B";
		}

	}
}
