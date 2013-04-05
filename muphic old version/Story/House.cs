using System;

namespace muphic.Story
{
	/// <summary>
	/// House ‚ÌŠT—v‚Ìà–¾‚Å‚·B
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
