using System;

namespace muphic.Link.Dialog.Listen
{
	/// <summary>
	/// ListenSelect の概要の説明です。
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