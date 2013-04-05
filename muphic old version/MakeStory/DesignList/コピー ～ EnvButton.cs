using System;
using System.Windows.Forms;
using muphic;
using muphic.MakeStory;

using muphic.Common;

namespace muphic.MakeStory.DesignList
{
	/// <summary>
	/// EnvButton の概要の説明です。
	/// </summary>
	public class EnvButton : Base
	{
		public BGDesignList parent;
		public int name;

		public EnvButton(BGDesignList dl, int name)
		{
			this.parent = dl;
			this.name = name;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click(p);

			if (this.parent.EnvironmentMode == (EnvironmentMode)name)
			{
				this.parent.EnvironmentMode = muphic.MakeStory.DesignList.EnvironmentMode.None;
				this.State = 0;
			}
			else
			{
				if(parent.parent.isClear)
					parent.parent.cb.Reset();
				for (int i = 0; i < parent.EnvB.Length; i++)
					parent.EnvB[i].State = 0;
				this.parent.EnvironmentMode = (EnvironmentMode)name;
				this.State = 1;

				//MessageBox.Show(((muphic.MakeStory.BGMode)parent.parent.wind.backscreen).ToString());
				parent.parent.PictureStory.Slide[parent.parent.NowPage].haikei.ObjName = ((muphic.MakeStory.BGMode)parent.parent.wind.backscreen).ToString();
				// チュートリアル実行中で、動作の待機状態だった場合
				if(TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
				{
					// ステート進行
					parent.parent.parent.tutorialparent.NextState();
				}
			}
		}

		public override void MouseEnter()
		{
			System.Diagnostics.Debug.WriteLine("AAAAAA");
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			if(parent.EnvironmentMode == (EnvironmentMode)this.name)
			{
				this.State = 0;
			}
		}


		public override string ToString()
		{
			return parent.ToString() + (EnvironmentMode)name + "B";
		}

	}
}
