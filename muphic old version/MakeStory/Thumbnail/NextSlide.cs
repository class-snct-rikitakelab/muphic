using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class NextSlide : Base
	{
		Thumbnail parent;
		public NextSlide(Thumbnail thumb)
		{
			parent = thumb;
		}

		public override void Click(System.Drawing.Point p)
		{
			if(parent.parent.NowPage < 9)
			{
				parent.parent.NowPage++;
				if((parent.mini[5].x > parent.numberX)
                    && (parent.mini[parent.parent.NowPage].x == parent.numberX + parent.numberXwidth * 3))
				{
					for(int i = 0; i < parent.mini.Length; i++)
						parent.mini[i].x -= parent.numberXwidth;
				}
                base.Click(p);
			}
		}
	}
}
