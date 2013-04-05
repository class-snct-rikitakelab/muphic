using System;
using System.Collections;
using System.Drawing;
using muphic;
using muphic.MakeStory.DesignList;

namespace muphic.MakeStory
{
	public class PictStory
	{
		public String Title;
		public Slide[] Slide;

		public PictStory()
		{
			Init();
		}

		public void Init()
		{
			Title = "";
			Slide = new Slide[muphic.StoryScreen.PageNum];
			for(int i = 0;i < muphic.StoryScreen.PageNum;i++)
				Slide[i] = new Slide();
		}
	}
}