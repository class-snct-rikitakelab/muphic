using System;

namespace muphic.Story
{
	/// <summary>
	/// House の概要の説明です。
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
