using System;

namespace muphic.Titlemode
{
	/// <summary>
	/// ModeButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class ModeButton : Base
	{
		public TitleScreen parent;
		public int num;

		public ModeButton(TitleScreen parent,int i)
		{
			this.parent = parent;
			num = i;
		}

		public override void Click(System.Drawing.Point p)
		{
			for(int  i = 0;i < 4;i++)
				if(this.num-1 != i)
					this.parent.mode[i].State = 0;
			this.State = (this.State == 1 ? 0 : 1);
			if(this.State == 1)
			{
				parent.InputMode = (muphic.Titlemode.InputMode)num;
				base.Click (p);
			}
			else
			{ 
				parent.InputMode = muphic.Titlemode.InputMode.Hira;
				parent.mode[(int)parent.InputMode-1].State = 1;
			}
			System.Diagnostics.Debug.WriteLine(true,"Mode:"+(muphic.Titlemode.InputMode)num);
			return;
		}

		/*public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.InputMode != (muphic.Titlemode.InputMode)num)
			{
				this.State = 0;
			}
		}*/

		public override string ToString()
		{
			return "ModeButton" + this.num;					//—Í‹Z
		}

	}
}
