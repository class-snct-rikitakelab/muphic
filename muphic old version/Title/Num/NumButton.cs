using System;

namespace muphic.Titlemode
{
	/// <summary>
	/// NumButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class NumButton : Base
	{
		public NumScreen parent;
		public int num;
		public string ch;

		public NumButton(NumScreen parent,string str,int i)
		{
			this.parent = parent;
			ch = str;
			num = i;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
            if ((parent.parent.Text == null) || (parent.parent.Text.Length < parent.parent.maxlength))
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
			return "NumButton" + this.num;					//—Í‹Z
		}

	}
}
