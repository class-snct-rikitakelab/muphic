using System;
using System.Drawing;
using muphic;
using muphic.MakeStory;
using muphic.MakeStory.DesignList;

namespace muphic.MakeStory
{
	/// <summary>
	/// Tsuibi ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class Tsuibi : Base
	{
		public MakeStoryScreen parent;
		public Point point;							//Šù‚É‹æØ‚ç‚ê‚½À•W
		public Tsuibi(MakeStoryScreen mss)
		{
			parent = mss;
		}

		public void MouseMove(Point p)
		{
			System.Drawing.Rectangle rec = PointManager.Get(((ObjMode)parent.tsuibi.State).ToString());
			System.Drawing.Rectangle wind = PointManager.Get(parent.wind.ToString());

			if((p.X-rec.Width/2 > wind.Left)&&(wind.Right > p.X+rec.Width/2)
				&&(p.Y-rec.Height/2 > wind.Top)&&(wind.Bottom > p.Y+rec.Height/2))
			{
				this.point = p;
			}
		}
	}
}
