using System;
using System.Drawing;

namespace muphic.One
{
	/// <summary>
	/// Tsuibi の概要の説明です。
	/// </summary>
	public class Tsuibi : Base
	{
		OneScreen parent;
		public Point point;							//既に区切られた座標
		public Tsuibi(OneScreen one)
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
			Rectangle rones = PointManager.Get(parent.ones.ToString());		//右のボタン群の領域
			//Rectangle r = PointManager.Get(parent.score.ToString());				//Scoreの座標を取得
//			r.Width = 1024 - r.X;													//右のほうまで追尾をつける
//			if(r.Left <= p.X && p.X <= r.Right && r.Top <= p.Y && p.Y <= r.Bottom)
//			{
//				parent.tsuibi.Visible = true;										//楽譜の中なら表示する
//			}
//			else
//			{
//				parent.tsuibi.Visible = false;										//楽譜の外なら表示しない
//			}
			if(Common.CommonTools.inRect(p, rscore) || Common.CommonTools.inRect(p, rones))
			{
				parent.tsuibi.Visible = true;
			}
			else
			{
				parent.tsuibi.Visible = false;
			}

			if(parent.score.isPlay)
			{
				this.Visible = false;
			}
		}
	}
}
