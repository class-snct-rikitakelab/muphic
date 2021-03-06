using System;
using System.Drawing;
using muphic;
using muphic.MakeStory;
using muphic.MakeStory.DesignList;

namespace muphic.MakeStory
{
	/// <summary>
	/// Tsuibi の概要の説明です。
	/// </summary>
	public class Tsuibi : Base
	{
		public MakeStoryScreen parent;
		public Point point;							//既に区切られた座標
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
