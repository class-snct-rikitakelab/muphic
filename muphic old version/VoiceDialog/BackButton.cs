using System;

namespace muphic.One.VoiceDialog
{
	/// <summary>
	/// BackButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class BackButton : Base
	{
		VoiceRegistDialog parent;
		public BackButton(VoiceRegistDialog voice)
		{
			parent = voice;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.OneScreenMode = OneScreenMode.OneScreen;
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
