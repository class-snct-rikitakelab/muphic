using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ScoreWindow ‚ÌŠT—v‚Ìà–¾‚Å‚·B
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
