using System;
using muphic;
using muphic.MakeStory;

namespace muphic.MakeStory.DesignList
{
	/// <summary>
	/// FaceButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class FaceButton : Base
	{
		public LivesDesignList parent;
		public int name;

		public FaceButton(LivesDesignList dl, int name)
		{
			this.parent = dl;
			this.name = name;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click(p);

			if (this.parent.FaceMode == (FaceMode)name)
			{
				this.parent.FaceMode = muphic.MakeStory.DesignList.FaceMode.None;
				this.State = 0;
			}
			else
			{
				if(parent.parent.isClear)
					parent.parent.cb.Reset();
				for (int i = 0; i < parent.FaceB.Length; i++)
					parent.FaceB[i].State = 0;
				this.parent.FaceMode = (FaceMode)name;
				this.State = 1;
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
			if(parent.FaceMode != (FaceMode)name)
			{
				this.State = 0;
			}
		}



		public override string ToString()
		{
			return parent.ToString() + (FaceMode)name + "B";
		}

	}
}
