//using System;
//
//namespace muphic.Link.Dialog.Select
//{
//	/// <summary>
//	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
//	/// </summary>
//	public class SelectBackButton : Base
//	{
//		SelectDialog parent;
//		public SelectBackButton(SelectDialog dia)
//		{
//			parent = dia;
//		}
//
//		public override void Click(System.Drawing.Point p)
//		{
//			base.Click (p);
//			//parent.parent.parent.Screen = parent.parent;
//			parent.parent.Screen = null;
//		}
//
//		public override void MouseEnter()
//		{
//			base.MouseEnter();
//			this.State = 1;
//		}
//
//		public override void MouseLeave()
//		{
//			base.MouseLeave();
//			this.State = 0;
//		}
//	}
//}