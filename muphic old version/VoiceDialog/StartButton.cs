using System;

namespace muphic.One.VoiceDialog
{
	/// <summary>
	/// StartButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class StartButton : Base
	{
		bool isRecord = false;
		VoiceRegistDialog parent;
		public StartButton(VoiceRegistDialog voice)
		{
			parent = voice;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(isRecord == false)
			{
				isRecord = true;
				SoundManager.StartRecord();
			}
			else
			{
				isRecord = false;
				SoundManager.StopRecord();
			}
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
