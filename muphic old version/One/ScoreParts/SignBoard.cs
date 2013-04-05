using System;
using System.Drawing;

namespace muphic.One.ScoreParts
{
	/// <summary>
	/// �Ŕ𗧂Ă�N���X�B����͕��i�Ƃ��čl����̂ł͂Ȃ��āA���̃N���X���ŕ����̊Ŕ𗧂ĂĂ���ƍl�����ق��������B
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
			int MaxAnimals = muphic.One.Score.MaxAnimals;						//����́AKugiri�������Ă�����
			int Kugiri = muphic.Common.ScoreTools.Kugiri;

			//Kugiri=2�̂Ƃ��́A���߂̍Ō��4��������6,14,22�c�ƂȂ��Ă��邱�Ƃ���A���̎��ɂȂ�
			for(int i=1;i<MaxAnimals;i++)
			{
				int a = i + score.nowPlace + Kugiri;							//����ŁA���߂̍Ōオ8,16,24�c�ƂȂ�(�܂�4*Kugiri�̔{��)
				if(a % (4*Kugiri) == 0)											
				{
					//muphic.DrawManager.DrawString((i+score.nowPlace).ToString(), 0, i*5);
					Point BoardPoint = new Point(muphic.Common.ScoreTools.ScoretoPoint(i+1, 0).X-10, 260);
					//					if(score.isPlay)
					//					{
					//						BoardPoint.X -= score.Animals.PlayOffset;				//�Đ����Ȃ�APlayOffset��������
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
