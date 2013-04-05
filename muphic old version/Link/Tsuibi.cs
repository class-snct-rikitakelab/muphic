using System;
using System.Drawing;

namespace muphic.Link
{
	/// <summary>
	/// Tsuibi の概要の説明です。
	/// </summary>
	public class Tsuibi : Base
	{
		LinkScreen parent;
		public Point point;							//既に区切られた座標
		public Tsuibi(LinkScreen link)
		{
			parent = link;
			State = 11;								//初期状態はなにも表示させない
		}

		public void MouseMove(Point p)
		{
			this.point = muphic.Common.ScoreTools.DecidePoint(p);//もしマウスが楽譜の中であれば、座標を、音階や位置などで区切る。

			if (parent.score.isPlay == true || parent.LinkScreenMode == muphic.LinkScreenMode.AnswerDialog)
			{
				this.Visible = false;
			}
		}
	}
}
