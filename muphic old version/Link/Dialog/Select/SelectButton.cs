using System;

namespace muphic.Link.Dialog.Select
{
	/// <summary>
	/// SelectButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class SelectButton : Base
	{
		public SelectButtons parent;
		public int num;
		public int level;
		public SelectButton(SelectButtons sbs, int num)
		{
			parent = sbs;
			this.num = num;

		}

		public override void Click(System.Drawing.Point p)
		{
			//if(this.parent.GetSelectFileName(this.num) == null) return;
			if (this.parent.parent.parent.dfList.Index.Count <= this.num) return;
			
			base.Click (p);
			this.State = 0;
			parent.ReadFile(num);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter();
			int level = 0;
			DataIndex di = (DataIndex)parent.parent.parent.dfList.Index[parent.NowPage+this.num];
			switch (parent.parent.level_select+1)
			{
				case 0:
					level = di.easy;
					break;
				case 1:
					level = di.normal;
					break;
				case 2:
					level = di.hard;
					break;
				default: break;
			}
			parent.parent.Level = level;
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave();
			parent.parent.Level = -1;
			this.State = 0;
		}
		
		public override string ToString()
		{
			return "SelectButton" + num;
		}
		
	}
}
