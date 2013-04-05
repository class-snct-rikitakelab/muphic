using System;

namespace muphic.Link
{
	/// <summary>
	/// Titlebar ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class Titlebar : Base
	{
		private String title;
		public LinkScreen parent;

		public String Title
		{
			get
			{
				return title;
			}
			set
			{
				title = value;
			}
		}
		
		public Titlebar(LinkScreen link)
		{
			parent = link;
			title = "";
		}

		public void Draw()
		{
			if (parent.LinkScreenMode == muphic.LinkScreenMode.LinkScreen && this.Visible == true)
			{
				if (muphic.Common.TutorialStatus.StringDrawFlag())
				{
					muphic.DrawManager.DrawString(Title, 420, 50);
				}
			}
		}
	}
}
