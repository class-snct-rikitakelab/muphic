using System;
using muphic;
using muphic.MakeStory;

namespace muphic.MakeStory
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class BeforeSlide : Base
	{
		Thumbnail parent;
		public BeforeSlide(Thumbnail thumb)
		{
			parent = thumb;
		}

		public override void Click(System.Drawing.Point p)
		{
			if(parent.parent.NowPage > 0)
			{
				parent.parent.NowPage--;
                if((parent.mini[0].x < parent.numberX)
                    &&(parent.mini[parent.parent.NowPage+1].x == parent.numberX+parent.numberXwidth*2))
                {
                    for (int i = 0; i < parent.mini.Length; i++)
                        parent.mini[i].x += parent.numberXwidth;
                }
                base.Click(p);
			}
		}
	}
}
