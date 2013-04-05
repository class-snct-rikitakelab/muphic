using System;

namespace muphic.MakeStory
{
	/// <summary>
	/// Thumbnail ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class Thumbnail : Screen
	{
		public MakeStoryScreen parent;
		public BeforeSlide before;
		public NextSlide next;
		public miniWindow[] mini;

		public int numberX = 254;
		public int numberY = 186;
		public int numberXwidth = 108;

		public Thumbnail(MakeStoryScreen mss)
		{
			parent = mss;

			before = new BeforeSlide(this);
			next = new NextSlide(this);

            mini = new miniWindow[muphic.MakeStoryScreen.PageNum];
			muphic.DrawManager.Regist(before.ToString(), 185, 188, "image\\MakeStory\\thumbnail\\before.png");
			muphic.DrawManager.Regist(next.ToString(), 790, 188, "image\\MakeStory\\thumbnail\\next.png");

            for(int i = 0;i < muphic.MakeStoryScreen.PageNum; i++)
            {
                mini[i] = new miniWindow(this,i+1);
                BaseList.Add(mini[i]);
				if(i < 5)
				{
					mini[i].Visible = true;
				}
				else
				{
					mini[i].Visible = false;
				}
            }
            BaseList.Add(before);
            BaseList.Add(next);
		}

		
		public override void Click(System.Drawing.Point p)
		{
			int d = 0;
			base.Click (p);
			for(int i = 0;i < muphic.MakeStoryScreen.PageNum;i++)
			{
				if(mini[i].Visible && mini[i].inRect(p))
				{
					mini[i].Click(p);
					d = (numberX + numberXwidth * 2)-mini[parent.NowPage].x;
					if((2 <= parent.NowPage)&&(parent.NowPage <= 7))
					{
						for(int j = 0;j < muphic.MakeStoryScreen.PageNum;j++)
						{
							mini[j].x += d;
						}
						return;
					}
					else if(7 < parent.NowPage)
					{
						for(int j = 0;j < muphic.MakeStoryScreen.PageNum;j++)
						{
							mini[j].x = numberX +numberXwidth*(j-5);
						}
						return;
					}
					else if(parent.NowPage < 2)
					{
						for(int j = 0;j < muphic.MakeStoryScreen.PageNum;j++)
						{
							mini[j].x = numberX +numberXwidth*(j);
						}
						return;
					}
				}
			}

		}

		public override void Draw()
		{
			for(int i = 0;i < muphic.MakeStoryScreen.PageNum;i++)
			{
				if((numberX <= mini[i].x)&&(mini[i].x < 700))
				{
					mini[i].Visible = true;
				}
				else
				{
					mini[i].Visible = false;
				}
			}
			base.Draw ();
		}

		public void init()
		{
			for (int i = 0; i < muphic.MakeStoryScreen.PageNum; i++)
			{
				mini[i].init();
			}
		}
	}
}
