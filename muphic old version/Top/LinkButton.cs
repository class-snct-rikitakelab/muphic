using System;

namespace muphic.Top
{
	/// <summary>
	/// LinkButton の概要の説明です。
	/// </summary>
	public class LinkButton : Base
	{
		public TopScreen parent;

		public LinkButton(TopScreen ts)
		{
			parent = ts;

			// つなげて音楽を使用しない場合はボタンを押せなくする
			if (!muphic.Common.CommonSettings.getEnableLinkScreen()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			parent.ScreenMode = muphic.ScreenMode.LinkScreen;
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
