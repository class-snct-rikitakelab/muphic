using System;

namespace muphic.Titlemode
{
	/// <summary>
	/// DelButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class DelButton : Base
	{
		public TitleScreen parent;

		public DelButton(TitleScreen ts)
		{
			parent = ts;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if((parent.Text != null)&&(parent.Text.Length > 0))
				parent.Text = parent.Text.Remove(parent.Text.Length-1,1);
			System.Diagnostics.Debug.WriteLine(true,"Del:"+parent.Text);
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
