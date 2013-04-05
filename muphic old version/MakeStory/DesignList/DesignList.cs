using muphic;
using muphic.MakeStory;

namespace muphic.MakeStory.DesignList
{
	public enum FaceMode
	{
		None, Glad, Angry, Sad, Enjoy
	};
	public enum DirectionMode
	{
		None, Front, Left, Right, Back
	};
	public enum ItemMode
	{
		None, A, B, C, D, E, F, G, H
	};
	public enum EnvironmentMode
	{
		None, Day1, Night1, Bad1, Day2, Night2, Bad2
	}


	public class DesignList : Screen
	{
		public MakeStoryScreen parent;
		public int name;

		public DesignList()
		{
		}

		public virtual void NowClick()
		{
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

		public override void MouseEnter()
		{
			System.Diagnostics.Debug.WriteLine("DesignListEnter");
			base.MouseEnter ();
		}
		public override void MouseLeave()
		{
			System.Diagnostics.Debug.WriteLine("DesignListLeave");
			//base.MouseLeave ();
		}


		public void init()
		{
			this.DirectionMode = muphic.MakeStory.DesignList.DirectionMode.None;
			this.FaceMode = muphic.MakeStory.DesignList.FaceMode.None;
			this.ItemMode = muphic.MakeStory.DesignList.ItemMode.None;
			for(int i = 0;i < BaseList.Count;i++)
				((Base)BaseList[i]).State = 0;
		}

		private FaceMode facemode;
		public int facevalue;
		public FaceMode FaceMode
		{
			get
			{
				return facemode;
			}
			set
			{
				facemode = value;
				switch(value)
				{
					case muphic.MakeStory.DesignList.FaceMode.None:
						facevalue = 0;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.FaceMode.Glad:
						facevalue = 1;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.FaceMode.Angry:
						facevalue = 2;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.FaceMode.Sad:
						facevalue = 3;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.FaceMode.Enjoy:
						facevalue = 4;
						NowClick();
						break;
					default:
						facevalue = 0;
						NowClick();
						break;
				}
			}
		}

		private DirectionMode directionmode;
		public int directionvalue;
		public DirectionMode DirectionMode
		{
			get
			{
				return directionmode;
			}
			set
			{
				directionmode = value;
				switch(value)
				{
					case muphic.MakeStory.DesignList.DirectionMode.None:
						directionvalue = 0;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.DirectionMode.Front:
						directionvalue = 0;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.DirectionMode.Left:
						directionvalue = 4;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.DirectionMode.Right:
						directionvalue = 8;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.DirectionMode.Back:
						directionvalue = 12;
						NowClick();
						break;
					default:
						directionvalue = 0;
						NowClick();
						break;
				}
			}
		}

		private ItemMode itemmode;
		public int itemvalue;
		public ItemMode ItemMode
		{
			get
			{
				return itemmode;
			}
			set
			{
				itemmode = value;
				switch(value)
				{
					case muphic.MakeStory.DesignList.ItemMode.None:
						itemvalue = 0;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.ItemMode.A:
						itemvalue = 1;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.ItemMode.B:
						itemvalue = 2;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.ItemMode.C:
						itemvalue = 3;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.ItemMode.D:
						itemvalue = 4;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.ItemMode.E:
						itemvalue = 5;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.ItemMode.F:
						itemvalue = 6;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.ItemMode.G:
						itemvalue = 7;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.ItemMode.H:
						itemvalue = 8;
						NowClick();
						break;
					default:
						itemvalue = 0;
						NowClick();
						break;
				}
			}
		}

		private EnvironmentMode envmode;
		public int envvalue;
		public EnvironmentMode EnvironmentMode
		{
			get
			{
				return envmode;
			}
			set
			{
				envmode = value;
				switch(value)
				{
					case muphic.MakeStory.DesignList.EnvironmentMode.None:
						envvalue = 0;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.EnvironmentMode.Day1:
						envvalue = 1;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.EnvironmentMode.Night1:
						envvalue = 2;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.EnvironmentMode.Bad1:
						envvalue = 3;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.EnvironmentMode.Day2:
						envvalue = 4;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.EnvironmentMode.Night2:
						envvalue = 5;
						NowClick();
						break;
					case muphic.MakeStory.DesignList.EnvironmentMode.Bad2:
						envvalue = 6;
						NowClick();
						break;
					default:
						envvalue = 0;
						NowClick();
						break;
				}
			}
		}
	}
}
