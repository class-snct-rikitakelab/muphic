using System;
using System.Drawing;

namespace muphic.Common
{
	#region version1
	/*
	/// <summary>
	/// ScoreTools �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreTools
	{
		public const int AnimalWidth = 70;				//�����̍ő剡��
		public const int Kugiri = 2;					//�����̋�؂�B1����4���̂݁A2����8���݂̂ɂȂ�B
		public ScoreTools()
		{
		}

		
		/// <summary>
		/// �}�E�X���W���y�����̈ʒu�Ɖ��K�ɕς���֐��B
		/// �߂�l��X�Ɉʒu�AY�ɉ��K����������B(��ΓI�Ȃ̂ŁAplace�����ɂȂ邱�Ƃ����肦��)
		/// </summary>
		/// <param name="p"></param>
		public static Point PointtoScore(Point p)
		{
			//�y���̉E��̍��W = (109,181)
			//C4 : 25  0
			//B3 : 75  1
			//A3 : 125 2
			//G3 : 175 3
			//F3 : 225 4
			//E3 : 275 5
			//D3 : 325 6
			//C3 : 375 7
			//���ꂾ�ƁA24�܂ł�0�ɂȂ�B
			int x = ((p.X-109)-(AnimalWidth/2)) / (AnimalWidth/Kugiri);
			int y = ((p.Y-181)+25)/50;
			return new Point(x, y);
		}

		/// <summary>
		/// �y�����̈ʒu�Ɖ��K����}�E�X���W������o���֐��B
		/// �ꉞ���S���W������炵��(��ΓI�Ȃ̂ŁAScore����͂ݏo�邱�Ƃ�����)
		/// </summary>
		/// <param name="code"></param>
		/// <param name="place"></param>
		/// <returns></returns>
		public static Point ScoretoPoint(int place, int code)
		{
			int x = place * (AnimalWidth/Kugiri) + (AnimalWidth/2) + 109;
			int y = code * 50 - 25 + 181;
			return new Point(x, y);
		}

		/// <summary>
		/// �y�����ł́A�ǔ����̕`��ʒu�̌���(�y�����ł͋�؂���Ȃ��Ƃ����Ȃ�����c)
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static Point DecidePoint(Point p)
		{
			Point a = muphic.Common.ScoreTools.DecidePlace(p);
			Point b = muphic.Common.ScoreTools.ScoretoPoint(a.X, a.Y);
			if(a.X == 0 && a.Y == 0)
			{
				return p;
			}
			return b;
		}

		/// <summary>
		/// ���݂̃}�E�X���W���y�����ŁA�ǂ̈ʒu�łǂ̉��K�Ɉ�ԋ߂��������肷��֐��B
		/// �߂�l��X�Ɉʒu�AY�ɉ��K����������B
		/// �������W���y���O�������ꍇ(0,0)�ɂȂ�B
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static Point DecidePlace(Point p)
		{			

			Point LowPlace = muphic.Common.ScoreTools.PointtoScore(p);
			Point Low = muphic.Common.ScoreTools.ScoretoPoint(LowPlace.X, LowPlace.Y);
			Point High = muphic.Common.ScoreTools.ScoretoPoint(LowPlace.X+1, LowPlace.Y+1);

			//�ʒu�̌���
			int neoLowX = Math.Abs(Low.X - p.X);
			int neoHighX = Math.Abs(High.X - p.X);
			int neoX;

			if(neoLowX <= neoHighX)
			{
				neoX = LowPlace.X;
			}
			else
			{
				neoX = LowPlace.X+1;
			}
			
			//���K�̌���
			int neoLowY = Math.Abs(Low.Y - p.Y);
			int neoHighY = Math.Abs(High.Y - p.Y);
			int neoY;

			if(neoLowY <= neoHighY)
			{
				neoY = LowPlace.Y;
			}
			else
			{
				neoY = LowPlace.Y+1;
			}


			//Animal.Size = (76,55)
			//road.Point = (109,181);
			//road.Size = (555,410)
			//���񌈒肵���ǔ����̍��W���A�y������͂ݏo�Ȃ����`�F�b�N
			Point a = muphic.Common.ScoreTools.ScoretoPoint(neoX, neoY);
			if(109 <= a.X-(AnimalWidth/2) && a.X+(AnimalWidth/2) <= 109+555)
			{
				if(1 <= neoY && neoY <= 8)
				{
					//�y�����Ȃ̂ŁA�l��Ԃ��B
					return new Point(neoX, neoY);
				}
			}

			//�y������͂ݏo���Ă�����Ӗ����Ȃ��B
			return new Point(0, 0);
		}

	}*/
	#endregion

	#region version2.1
	//version2 XGA�����������B���ƁA���܂ł͊y���̕������𐔎��Ŏg���Ă������ǁA�ϐ����g���悤�ɂ����B
	//version2.1 AnimalWidth��70����64�ɕύX�B�Ō��8����\�����邽��61�ɕύX

	/// <summary>
	/// ScoreTools �̊T�v�̐����ł��B
	/// </summary>
	public class ScoreTools
	{
		public const int AnimalWidth = 61;				//�����̍ő剡��
		public const int AnimalHeight = 48;				//����(�Ƃ�������)�̍���
		public const int Kugiri = 2;					//�����̋�؂�B1����4���̂݁A2����8���݂̂ɂȂ�B
		public static Rectangle score = new Rectangle(112, 295, 773, 388);//�y���̓������̍��W
		public ScoreTools()
		{
		}

		/// <summary>
		/// ���W�����̒��ɂ��邩�ǂ����𒲂ׂ�֐�
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool inScore(Point p)
		{
			if(score.X <= p.X && p.X <= score.Right)
			{
				if(score.Y <= p.Y && p.Y <= score.Bottom)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// �}�E�X���W���y�����̈ʒu�Ɖ��K�ɕς���֐��B
		/// �߂�l��X�Ɉʒu�AY�ɉ��K����������B(��ΓI�Ȃ̂ŁAplace�����ɂȂ邱�Ƃ����肦��)
		/// </summary>
		/// <param name="p"></param>
		public static Point PointtoScore(Point p)
		{
			Rectangle score = muphic.Common.ScoreTools.score;
			//�y���̍���̍��W = (109,181)
			//C4 : 25  0
			//B3 : 75  1
			//A3 : 125 2
			//G3 : 175 3
			//F3 : 225 4
			//E3 : 275 5
			//D3 : 325 6
			//C3 : 375 7
			//���ꂾ�ƁA24�܂ł�0�ɂȂ�B
			int x = ((p.X-score.X)-(AnimalWidth/2)) / (AnimalWidth/Kugiri);
			int y = ((p.Y-score.Y)+(AnimalHeight/2))/AnimalHeight;
			return new Point(x, y);
		}

		/// <summary>
		/// �y�����̈ʒu�Ɖ��K����}�E�X���W������o���֐��B
		/// �ꉞ���S���W������炵��(��ΓI�Ȃ̂ŁAScore����͂ݏo�邱�Ƃ�����)
		/// </summary>
		/// <param name="code"></param>
		/// <param name="place"></param>
		/// <returns></returns>
		public static Point ScoretoPoint(int place, int code)
		{
			Rectangle score = muphic.Common.ScoreTools.score;
			int x = place * (AnimalWidth/Kugiri) + (AnimalWidth/2) + score.X;
			int y = code * AnimalHeight - (AnimalHeight/2) + score.Y;
			return new Point(x, y);
		}

		/// <summary>
		/// �y�����ł́A�ǔ����̕`��ʒu�̌���(�y�����ł͋�؂���Ȃ��Ƃ����Ȃ�����c)
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static Point DecidePoint(Point p)
		{
			Point a = muphic.Common.ScoreTools.DecidePlace(p);
			Point b = muphic.Common.ScoreTools.ScoretoPoint(a.X, a.Y);
			if(a.X == 0 && a.Y == 0)
			{
				return p;
			}
			return b;
		}

		/// <summary>
		/// ���݂̃}�E�X���W���y�����ŁA�ǂ̈ʒu�łǂ̉��K�Ɉ�ԋ߂��������肷��֐��B
		/// �߂�l��X�Ɉʒu�AY�ɉ��K����������B
		/// �������W���y���O�������ꍇ(0,0)�ɂȂ�B
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static Point DecidePlace(Point p)
		{
			Rectangle score = muphic.Common.ScoreTools.score;
			Point LowPlace = muphic.Common.ScoreTools.PointtoScore(p);
			Point Low = muphic.Common.ScoreTools.ScoretoPoint(LowPlace.X, LowPlace.Y);
			Point High = muphic.Common.ScoreTools.ScoretoPoint(LowPlace.X+1, LowPlace.Y+1);

			//�ʒu�̌���
			int neoLowX = Math.Abs(Low.X - p.X);
			int neoHighX = Math.Abs(High.X - p.X);
			int neoX;

			if(neoLowX <= neoHighX)
			{
				neoX = LowPlace.X;
			}
			else
			{
				neoX = LowPlace.X+1;
			}
			
			//���K�̌���
			int neoLowY = Math.Abs(Low.Y - p.Y);
			int neoHighY = Math.Abs(High.Y - p.Y);
			int neoY;

			if(neoLowY <= neoHighY)
			{
				neoY = LowPlace.Y;
			}
			else
			{
				neoY = LowPlace.Y+1;
			}


			//Animal.Size = (76,55)
			//road.Point = (109,181);
			//road.Size = (555,410)
			//���񌈒肵���ǔ����̍��W���A�y������͂ݏo�Ȃ����`�F�b�N
			Point a = muphic.Common.ScoreTools.ScoretoPoint(neoX, neoY);
			if(score.X <= a.X-(AnimalWidth/2) && a.X+(AnimalWidth/2) <= score.Right)
			{
				if(1 <= neoY && neoY <= 8)
				{
					//�y�����Ȃ̂ŁA�l��Ԃ��B
					return new Point(neoX, neoY);
				}
			}

			//�y������͂ݏo���Ă�����Ӗ����Ȃ��B
			return new Point(0, 0);
		}

	}
	#endregion
}
