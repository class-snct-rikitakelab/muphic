using System;

namespace muphic.Link.Dialog.Select
{
	/// <summary>
	/// LevelEasyButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LevelEasyButton : Base
	{
		public SelectDialog parent;
		public bool clicked = false;

		public LevelEasyButton(SelectDialog sd)
		{
			parent = sd;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			this.clicked = true;
			parent.level_select = -1;
			parent.normal.clicked = false;
			parent.normal.State = 0;
			parent.hard.clicked = false;
			parent.hard.State = 0;
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if (!clicked) this.State = 0;
		}
	}
}
