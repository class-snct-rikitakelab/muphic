using System;

namespace muphic.Story.ScoreParts
{
	/// <summary>
	/// VoiceRecordButton の概要の説明です。
	/// </summary>
	public class VoiceRecordButton : Base
	{
		Score parent;
		public VoiceRecordButton(Score one)
		{
			parent = one;

			// 録音しない設定の場合は押させぬ
			if (!muphic.Common.CommonSettings.getEnableVoice()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			parent.parent.StoryScreenMode = StoryScreenMode.VoiceRegistDialog;
		}

		public override void MouseEnter()
		{
			if (this.State == 2) return;
			base.MouseEnter();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			if (this.State == 2) return;
			base.MouseLeave();
			this.State = 0;
		}
	}
}
