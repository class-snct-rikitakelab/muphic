using System;

namespace muphic
{
	/// <summary>
	/// HouseLight ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class HouseLight : Base
	{
		int[] lightCount = new int[8];

		public HouseLight()
		{
			Reset();
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\link\\parts\\light.png");
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
			lightCount[code-1] = 20;
		}

		public void Draw()
		{
			for (int i = 0; i < 8; i++)
			{
				if (lightCount[i] > 0)
				{
					DrawManager.DrawCenter(this.ToString(), 63, 315+i*50);
					lightCount[i]--;
				}
			}
		}
	}
}
