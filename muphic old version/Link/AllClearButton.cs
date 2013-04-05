using System;

namespace muphic.Link
{
	/// <summary>
	/// AllClearButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class AllClearButton : Base
	{
		LinkScreen parent;
		public AllClearButton(LinkScreen link)
		{
			parent = link;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if (!parent.score.isPlay && !parent.score.answerCheckFlag)
			{
				parent.LinkScreenMode = muphic.LinkScreenMode.AllClearDialog;
//				parent.score.AnimalList.Clear();
//				for (int i = 0; i < parent.score.putFlag.Length; i++)
//				{
//					parent.score.putFlag[i] = false;
//				}
//				for (int i = 0; i < 10; i++)
//				{
//					for (int j = 0; j < 4; j++)
//					{
//						parent.score.ribbon[i, j] = false;
//					}
//				}
			}
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
