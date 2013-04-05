using System;
using System.Drawing;

namespace muphic.LinkMake
{
	/// <summary>
	/// Tsuibi の概要の説明です。
	/// </summary>
	public class Tsuibi : Base
	{
		LinkMakeScreen parent;
		public Point point;							//既に区切られた座標
		public Tsuibi(LinkMakeScreen one)
		{
			parent = one;
			this.Visible = true;
		}

		public void MouseMove(Point p)
		{
			this.point = muphic.Common.ScoreTools.DecidePoint(p);//もしマウスが楽譜の中であれば、座標を、音階や位置などで
																//区切る。
			Rectangle rscore = Common.ScoreTools.score;						//Scoreの道の領域を取得
			rscore.Width = 1024 - rscore.X;									//Scoreと右との間のわずかな隙間を防ぐ
			Rectangle rones = PointManager.Get(parent.linkmakes.ToString());//右のボタン群の領域

			if(Common.CommonTools.inRect(p, rscore) || Common.CommonTools.inRect(p, rones))
			{
				parent.tsuibi.Visible = true;
			}
			else
			{
				parent.tsuibi.Visible = false;
			}

			if (parent.score.isPlay == true)
			{
				this.Visible = false;
			}
		}
	}
}
