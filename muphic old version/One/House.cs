using System;

namespace muphic.One
{
	/// <summary>
	/// House の概要の説明です。
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
