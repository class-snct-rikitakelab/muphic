using System;
using System.Windows.Forms;
using muphic;
using muphic.MakeStory;

namespace muphic.MakeStory.DesignList
{
	/// <summary>
	/// DirectionButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class ItemButton : Base
	{
		public ItemDesignList parent;
		public int name;

		public ItemButton(ItemDesignList dl, int name)
		{
			this.parent = dl;
			this.name = name;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click(p);
			if (this.parent.ItemMode == (ItemMode)name)
			{
				this.parent.ItemMode = muphic.MakeStory.DesignList.ItemMode.None;
				this.State = 0;
			}
			else
			{
				if(parent.parent.isClear)
					parent.parent.cb.Reset();
				for (int i = 0; i < parent.ItemB.Length; i++)
					parent.ItemB[i].State = 0;
				this.parent.ItemMode = (ItemMode)name;
				this.State = 1;
			}
		}

		public override string ToString()
		{
			return parent.ToString() + (ItemMode)name +"B";
		}

	}
}
