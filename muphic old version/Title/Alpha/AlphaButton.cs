using System;

namespace muphic.Titlemode
{
	/// <summary>
	/// AlphaButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class AlphaButton : Base
	{
		public AlphaScreen parent;
		public int num;
		public string ch;

		public AlphaButton(AlphaScreen parent,string c,int i)
		{
			this.parent = parent;
			ch = c;
			num = i;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if((parent.parent.Text == null)||(parent.parent.Text.Length < parent.parent.maxlength))
				parent.parent.Text += ch;
			System.Diagnostics.Debug.WriteLine(true,parent.parent.Text);
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

		public override string ToString()
		{
			return "AlphaButton" + this.num;					//—Í‹Z
		}

	}
}
