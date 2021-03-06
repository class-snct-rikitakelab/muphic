using System;
using System.Windows.Forms;

namespace muphic.Common
{
	/// <summary>
	/// FadeTimer の概要の説明です。
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
				if(value)						//FadeTimerが起動した
				{
					parent.FrameCounter.Stop();	//排他制御のため、FrameCounterを停止
				}
				else
				{
					parent.FrameCounter.Start();//排他制御のため、FrameCounterを解除
				}
			}
		}

	}
}
