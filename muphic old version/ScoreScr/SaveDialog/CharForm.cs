using System;

namespace muphic.ScoreScr.SaveDialog
{
	/// <summary>
	/// CharForm �̊T�v�̐����ł��B
	/// </summary>
	public class CharForm : Base
	{
		public ScoreSaveDialog parent;

		public CharForm(ScoreSaveDialog dialog)
		{
			this.parent = dialog;
		}
	}
}
