using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// House �̊T�v�̐����ł��B
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
