using System;

namespace muphic.Story.VoiceDialog
{
	/// <summary>
	/// OneMoreCloseButton の概要の説明です。
	/// </summary>
	public class OneMoreCloseButton : Base
	{
		VoiceRegistOneMoreDialog parent;
		public OneMoreCloseButton(VoiceRegistOneMoreDialog voice)
		{
			parent = voice;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.StoryScreenMode = muphic.StoryScreenMode.VoiceRegistDialog;
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
