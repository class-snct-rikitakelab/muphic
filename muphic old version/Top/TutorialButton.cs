using System;

namespace muphic.Top
{
	/// <summary>
	/// TutorialButton の概要の説明です。
	/// </summary>
	public class TutorialButton : Base
	{
		public TopScreen parent;

		public TutorialButton(TopScreen ts)
		{
			parent = ts;
			
			// チュートリアルを使用しない場合押せなくする
			if (!muphic.Common.CommonSettings.getEnableTutotial()) this.State = 2;
		}

		public override void Click(System.Drawing.Point p)
		{
			if (this.State == 2) return;
			base.Click (p);
			this.parent.ScreenMode = muphic.ScreenMode.TutorialScreen;
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
