using System;

namespace muphic.Link
{
	/// <summary>
	/// SelectButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class SelectButton : Base
	{
		public LinkScreen parent;
		public SelectButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.score.isPlay && !parent.score.answerCheckFlag)
			{
				parent.LinkScreenMode = LinkScreenMode.SelectDialog;
			}
			//new muphic.Link.Dialog.Select.SelectDialog(1, parent);
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
