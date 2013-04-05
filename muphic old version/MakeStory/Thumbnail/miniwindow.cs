using System;
using System.Drawing;

namespace muphic.MakeStory
{
	/// <summary>
	/// miniwindow ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class miniWindow : Screen
	{
		public Thumbnail parent;
		private int num;
		private int x0;
		private int y0;
		public int x;
		public int y;

		public miniWindow(Thumbnail parent, int no)
		{
			this.parent = parent;
			this.num = no;
			x0 = parent.numberX +parent.numberXwidth*(no-1);
			y0 = parent.numberY;
			x = x0;
			y = y0;
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\MakeStory\\thumbnail\\miniwindow.png");//, "image\\MakeStory\\thumbnail\\miniwindow_on.png");
		}

		public override void Draw()
		{
			if((parent.numberX <= this.x)&&(this.x < 700))
			{
				if(this.num == 10)
				{
					muphic.DrawManager.Draw("1", this.x+2, this.y-24);
					muphic.DrawManager.Draw("10", this.x+12, this.y-24);
				}
				else
				{
					muphic.DrawManager.Draw(this.num.ToString(), this.x+2, this.y-24);
				}
				muphic.DrawManager.Draw(this.ToString(), this.x, this.y);

				Rectangle src = PointManager.Get(parent.parent.wind.ToString());
				src.X -= 5;src.Y -= 5;
				src.Width += 10;src.Height += 10;
				Rectangle dist = PointManager.Get(this.ToString());
				dist.X = x;
				dist.Y = y;
				DrawManager.Change(src,dist);

				Slide slide = parent.parent.PictureStory.Slide[num-1];
				//”wŒi‚Ì•`‰æ
				if(slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
				}
				else
				{
					DrawManager.DrawDiv(slide.haikei.ToString(),
						slide.haikei.X, slide.haikei.Y);
				}
				//”z’u•¨‚Ì•`‰æ
				for(int i = 0;i < slide.ObjList.Count;i++)
				{
					Obj o = (Obj)(slide.ObjList[i]);
					DrawManager.DrawDivCenter((slide.ObjList[i].ToString()), o.X, o.Y);
				}

				if((slide.AnimalList.Count != 0))
				{
					muphic.DrawManager.Draw("AddMusic", this.x+20, this.y-22);
				}

				if (parent.parent.NowPage == num-1)
					muphic.DrawManager.Draw("select", this.x-6, this.y-6);
			}
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.parent.NowPage = this.num-1;
			System.Diagnostics.Debug.WriteLine(parent.parent.NowPage.ToString());
		}

		public bool inRect(System.Drawing.Point p)
		{
			Rectangle rec = PointManager.Get(this.ToString());
			if((this.x < p.X && p.X < this.x+rec.Width)
				&&(this.y < p.Y && p.Y < this.y+rec.Height))
				return true;
			return false;
		}

		public override String ToString()
		{
			return "miniwin" + this.num;					//—Í‹Z
		}

		public void init()
		{
			this.x = x0;
			this.y = y0;
		}
	}
}
