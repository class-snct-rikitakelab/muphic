using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScoreWindow �̊T�v�̐����ł��B
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
