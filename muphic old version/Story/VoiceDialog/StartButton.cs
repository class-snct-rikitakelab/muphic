using System;

namespace muphic.Story.VoiceDialog
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

			//parent.IsRecording = true;
			parent.isCountDown = true;

/*			if(isRecord == false)
			{
				isRecord = true;
				SoundManager.StartRecord();
				parent.back.Visible = false;
				this.State = 1;
			}
			else
			{
				isRecord = false;
				SoundManager.StopRecord();
				
				MakeSound ms = new MakeSound();
				ms.VoiceToWave();
				parent.back.Visible = true;
				this.State = 0;
			}*/
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
