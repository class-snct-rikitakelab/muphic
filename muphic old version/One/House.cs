using System;

namespace muphic.One
{
	/// <summary>
	/// House �̊T�v�̐����ł��B
	/// </summary>
	public class House : Base
	{
		public OneScreen parent;
		public House(OneScreen one)
		{
			parent = one;
		}
	}
}
