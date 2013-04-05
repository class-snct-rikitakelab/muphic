using System;

namespace muphic.Link.Dialog.Select
{
	/// <summary>
	/// LevelNormalButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LevelNormalButton : Base
	{
		public SelectDialog parent;
		public bool clicked = false;

		public LevelNormalButton(SelectDialog sd)
		{
			parent = sd;
			this.clicked = true;
			parent.level_select = 0;
			this.State = 1;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			this.clicked = true;
			parent.level_select = 0;
			parent.easy.clicked = false;
			parent.easy.State = 0;
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
