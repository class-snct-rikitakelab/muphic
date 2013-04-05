using System;

namespace muphic.Link
{
	/// <summary>
	/// HouseLight ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class HouseLight : Base
	{
		public LinkScreen parent;

		int[] lightCount = new int[8];

		public HouseLight(LinkScreen link)
		{
			parent = link;
			Reset();
		}

		public void Reset()
		{
			for (int i = 0; i < 8; i++)
			{
				lightCount[i] = 0;
			}
		}

		public void Add(int code)
		{
			lightCount[code-1] = 50;
		}

		public void Draw()
		{
			for (int i = 0; i < 8; i++)
			{
				if (lightCount[i] > 0)
				{
					DrawManager.DrawCenter(this.ToString(), 48, 205+i*50);
					lightCount[i]--;
				}
			}
		}
	}
}