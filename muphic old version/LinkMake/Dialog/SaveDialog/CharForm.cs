using System;

namespace muphic.LinkMake.Dialog.Save
{
	/// <summary>
	/// CharForm �̊T�v�̐����ł��B
	/// </summary>
	public class CharForm : Base
	{
		public LinkSaveDialog parent;

		public CharForm(LinkSaveDialog dialog)
		{
			this.parent = dialog;
		}
	}
}
