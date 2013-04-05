using System;

namespace muphic.Link.Dialog.Listen
{
	/// <summary>
	/// BackButton ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class ListenBackButton : Base
	{
		ListenDialog parent;
		public ListenBackButton(ListenDialog dia)
		{
			parent = dia;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.bar.PlayCount = 0;
			parent.bar.PlayOffset = 0;
			parent.bar.isPlay = false;

			for (int i = 0; i < parent.bar.AnimalList.Count; i++)
			{
				Animal a = (Animal)parent.bar.AnimalList[i];
				a.Visible = true;
			}

			//parent.parent.parent.Screen = parent.parent;
			parent.parent.LinkScreenMode = muphic.LinkScreenMode.LinkScreen;
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.State = 0;
		}
	}
}