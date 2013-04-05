using System;
using System.Windows.Forms;

namespace muphic.Common
{
	/// <summary>
	/// FadeTimer �̊T�v�̐����ł��B
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
				if(value)						//FadeTimer���N������
				{
					parent.FrameCounter.Stop();	//�r������̂��߁AFrameCounter���~
				}
				else
				{
					parent.FrameCounter.Start();//�r������̂��߁AFrameCounter������
				}
			}
		}

	}
}
