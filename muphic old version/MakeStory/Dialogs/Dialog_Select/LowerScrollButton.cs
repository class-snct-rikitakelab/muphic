using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// LowerScrollButton の概要の説明です。
	/// </summary>
	public class LowerScrollButton : Base
	{
		public StorySelectDialog parent;
		public LowerScrollButton(StorySelectDialog story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.sbs.Lower();
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
		}
	}
}
