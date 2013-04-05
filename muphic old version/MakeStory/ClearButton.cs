using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// ClearButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class ClearButton : Base
	{
		public MakeStoryScreen parent;

		public ClearButton(MakeStoryScreen mss)
		{
			parent = mss;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Clear)
			{
				parent.isClear = false;
				parent.ButtonsMode = (ButtonsMode)parent.BeforeState;
				parent.tsuibi.State = 0;
				
				this.State = 0;
			}
			else
			{
				parent.isClear = true;
				this.State = 1;
				parent.BeforeState = (int)(parent.ButtonsMode);

				parent.ButtonsMode = muphic.MakeStory.ButtonsMode.Clear;
				parent.tsuibi.State = 13*8 +8*4+1;
			}
		}

		public void Reset()
		{
			//parent.ButtonsMode = (ButtonsMode)parent.BeforeState;
			System.Diagnostics.Debug.WriteLine("Clear:"+((ButtonsMode)parent.BeforeState).ToString());
			parent.tsuibi.State = 0;
			parent.isClear = false;
			this.State = 0;
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.ButtonsMode != muphic.MakeStory.ButtonsMode.Clear)
			{
				this.State = 0;
			}
		}

	}
}
