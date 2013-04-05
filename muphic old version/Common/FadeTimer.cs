using System;
using System.Windows.Forms;

namespace muphic.Common
{
	/// <summary>
	/// FadeTimer ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class FadeTimer : Timer
	{
		MainScreen parent;
		public FadeTimer(MainScreen main, System.ComponentModel.IContainer c) : base(c)
		{
			parent = main;
		}

		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
				if(value)						//FadeTimer‚ª‹N“®‚µ‚½
				{
					parent.FrameCounter.Stop();	//”r‘¼§Œä‚Ì‚½‚ßAFrameCounter‚ğ’â~
				}
				else
				{
					parent.FrameCounter.Start();//”r‘¼§Œä‚Ì‚½‚ßAFrameCounter‚ğ‰ğœ
				}
			}
		}

	}
}
