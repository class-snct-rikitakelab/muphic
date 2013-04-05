using System;
using System.Drawing;

namespace muphic.One.ScoreParts
{
	/// <summary>
	/// 看板を立てるクラス。これは部品として考えるのではなくて、このクラス内で複数の看板を立てていると考えたほうがいい。
	/// </summary>
	public class SignBoard : Screen
	{
		Score parent;
		public SignBoard(Score one)
		{
			parent = one;
		}

		public override void Draw()
		{
			//base.Draw();
			muphic.DrawManager.Draw("Sign");
			Score score = parent;
			int MaxAnimals = muphic.One.Score.MaxAnimals;						//これは、Kugiriも入っているやつ
			int Kugiri = muphic.Common.ScoreTools.Kugiri;

			//Kugiri=2のときは、小節の最後の4分音符は6,14,22…となっていることから、下の式になる
			for(int i=1;i<MaxAnimals;i++)
			{
				int a = i + score.nowPlace + Kugiri;							//これで、小節の最後が8,16,24…となる(つまり4*Kugiriの倍数)
				if(a % (4*Kugiri) == 0)											
				{
					//muphic.DrawManager.DrawString((i+score.nowPlace).ToString(), 0, i*5);
					Point BoardPoint = new Point(muphic.Common.ScoreTools.ScoretoPoint(i+1, 0).X-10, 260);
					//					if(score.isPlay)
					//					{
					//						BoardPoint.X -= score.Animals.PlayOffset;				//再生中なら、PlayOffsetを加える
					//					}
					muphic.DrawManager.Draw("Sign", BoardPoint.X, BoardPoint.Y);

					int space;
					if(a/(4*Kugiri) < 10)
					{
						space = 10;
					}
					else
					{
						space = 5;
					}
					if(muphic.Common.TutorialStatus.StringDrawFlag())
					{
						if(parent.parent.OneScreenMode == muphic.OneScreenMode.OneScreen)
						{
							if(parent.parent.StringDraw()) muphic.DrawManager.DrawString((a/(4*Kugiri)).ToString(), BoardPoint.X+space, 266);
						}
					}
				}
			}
		}
	}
}
