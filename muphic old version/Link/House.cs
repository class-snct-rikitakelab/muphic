using System;

namespace muphic.Link
{
	/// <summary>
	/// House �̊T�v�̐����ł��B
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
