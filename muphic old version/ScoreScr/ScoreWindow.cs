using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScoreWindow の概要の説明です。
	/// </summary>
	public class ScoreWindow : Base
	{
		public ScoreScreen parent;

		public ScoreWindow(ScoreScreen score)
		{
			parent = score;
		}
	}
}
