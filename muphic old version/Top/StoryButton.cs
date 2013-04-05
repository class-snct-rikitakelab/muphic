using System;

namespace muphic.Top
{
	/// <summary>
	/// StoryButton �̊T�v�̐����ł��B
	/// </summary>
	public class StoryButton : Base
	{
		public TopScreen parent;

		public StoryButton(TopScreen ts)
		{
			parent = ts;

			// ���̂����艹�y���g�p���Ȃ��ꍇ�����Ȃ�����
			if (!muphic.Common.CommonSettings.getEnableStoryScreen()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			this.parent.ScreenMode = muphic.ScreenMode.StoryScreen;
		}

		public override void MouseEnter()
		{
			if (this.State == 2) return;
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			if (this.State == 2) return;
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
