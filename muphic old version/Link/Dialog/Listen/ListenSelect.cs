using System;

namespace muphic.Link.Dialog.Listen
{
	/// <summary>
	/// ListenSelect �̊T�v�̐����ł��B
	/// </summary>
	public class ListenSelect : Base
	{
		ListenDialog parent;
		public ListenSelect(ListenDialog dia)
		{
			parent = dia;
			this.State = parent.parent.QuestionNum;
		}
	}
}