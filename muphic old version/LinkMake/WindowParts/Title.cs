using System;

namespace muphic.LinkMake.WindowParts
{
	/// <summary>
	/// Title �̊T�v�̐����ł��B
	/// </summary>
	public class Title : Base
	{
		public Window parent;
		public Title(Window win)
		{
			parent = win;
		}
	}
}
