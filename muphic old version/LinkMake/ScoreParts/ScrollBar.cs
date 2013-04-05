using System;
using System.Drawing;

namespace muphic.LinkMake.ScoreParts
{
	/// <summary>
	/// ScrollBar の概要の説明です。
	/// </summary>
	public class ScrollBar : Screen
	{
		Score parent;
		Point min = new Point(145, 697);							//スクロールバーの値が最小の時の部品座標
		Point max = new Point(543, 697);							//スクロールバーの値が最大の時の部品座標
		Size BarSize = new Size(306, 38);
		public float Percent = 0;									//スクロールバーが一体何%の域にあるかを示したもの

		public ScrollBar(Score score)
		{
			parent = score;
			DrawManager.Regist("ScrollBar", 145, 697, "image\\one\\parts\\scroll\\bar.png");//min 145 max 543
		}

		public override void Draw()
		{
			int nowX;
			nowX = this.PercenttoPoint(Percent);
			DrawManager.Draw("ScrollBar", nowX, min.Y);
		}

		public override void DragBegin(Point begin)
		{
			base.DragBegin (begin);
			Percent = this.PointtoPercent(begin.X - BarSize.Width / 2);		//ドラッグが始まったとき
			parent.ChangeScroll(Percent);									//まずバーの位置をマウスの中央に持ってくる
		}


		public override void Drag(Point begin, Point p)
		{
			base.Drag (begin, p);
			Percent = this.PointtoPercent(p.X - BarSize.Width/2);			//ドラッグ中も
			parent.ChangeScroll(Percent);									//バーの位置をマウスの中央に持ってくる
		}

		/// <summary>
		/// 現在の座標をパーセント表示に変換するメソッド
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		private float PointtoPercent(int p)
		{
			return (float)(p - min.X) / (max.X - min.X) * 100;
		}

		/// <summary>
		/// 現在のパーセントを座標に変換するメソッド
		/// </summary>
		/// <param name="percent"></param>
		/// <returns></returns>
		private int PercenttoPoint(float percent)
		{
			return min.X + (int)((max.X - min.X) * percent / 100);
		}
	}
}
