using System;

namespace muphic.MakeStory.Dialog
{
	/// <summary>
	/// UpperScrollButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class UpperScrollButton : Base
	{
		public StorySelectDialog parent;
		public UpperScrollButton(StorySelectDialog story)
		{
			parent = story;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.sbs.Upper();
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
