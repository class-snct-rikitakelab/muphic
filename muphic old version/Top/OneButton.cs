using System;

namespace muphic.Top
{
	/// <summary>
	/// OneButton の概要の説明です。
	/// </summary>
	public class OneButton : Base
	{
		public TopScreen parent;

		public OneButton(TopScreen ts)
		{
			parent = ts;
			
			// ひとりで音楽を使用しない場合は押せなくする
			if (!muphic.Common.CommonSettings.getEnableOneScreen()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			parent.ScreenMode = muphic.ScreenMode.OneScreen;
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
