using System;

namespace muphic.Story.ScoreParts
{
	/// <summary>
	/// VoiceRecordButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class VoiceRecordButton : Base
	{
		Score parent;
		public VoiceRecordButton(Score one)
		{
			parent = one;

			// ˜^‰¹‚µ‚È‚¢İ’è‚Ìê‡‚Í‰Ÿ‚³‚¹‚Ê
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
