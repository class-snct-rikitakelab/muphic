using System;

namespace muphic.Story
{
	/// <summary>
	/// House �̊T�v�̐����ł��B
	/// </summary>
	public class House : Base
	{
		public StoryScreen parent;
		public House(StoryScreen story)
		{
			parent = story;
		}
	}
}
