using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// House の概要の説明です。
	/// </summary>
	public class House : Base
	{
		public LinkMakeScreen parent;
		public House(LinkMakeScreen one)
		{
			parent = one;
		}
	}
}
