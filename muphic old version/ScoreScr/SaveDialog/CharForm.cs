using System;

namespace muphic.ScoreScr.SaveDialog
{
	/// <summary>
	/// CharForm の概要の説明です。
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
