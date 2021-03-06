using System;

namespace muphic.ADV.MsgWindowParts
{
	/// <summary>
	/// MsgWindow の概要の説明です。
	/// </summary>
	public class MainWindow : Base
	{
		public MsgWindow parent;
		
		public MainWindow(MsgWindow msgwindow)
		{
			this.parent = msgwindow;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
		}
	}
}
