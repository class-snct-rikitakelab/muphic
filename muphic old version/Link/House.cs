using System;

namespace muphic.Link
{
	/// <summary>
	/// House の概要の説明です。
	/// </summary>
	public class House : Base
	{
		public LinkScreen parent;
		public House(LinkScreen link)
		{
			parent = link;
		}
	}
}
