using System;
using System.Drawing;

namespace muphic.Common
{
	#region version1
	/*
	/// <summary>
	/// ScoreTools の概要の説明です。
	/// </summary>
	public class ScoreTools
	{
		public const int AnimalWidth = 70;				//動物の最大横幅
		public const int Kugiri = 2;					//動物の区切り。1だと4分のみ、2だと8分のみになる。
		public ScoreTools()
		{
		}

		
		/// <summary>
		/// マウス座標を楽譜内の位置と音階に変える関数。
		/// 戻り値はXに位置、Yに音階が代入される。(絶対的なので、placeが負になることもありえる)
		/// </summary>
		/// <param name="p"></param>
		public static Point PointtoScore(Point p)
		{
			//楽譜の右上の座標 = (109,181)
			//C4 : 25  0
			//B3 : 75  1
			//A3 : 125 2
			//G3 : 175 3
			//F3 : 225 4
			//E3 : 275 5
			//D3 : 325 6
			//C3 : 375 7
			//これだと、24までが0になる。
			int x = ((p.X-109)-(AnimalWidth/2)) / (AnimalWidth/Kugiri);
			int y = ((p.Y-181)+25)/50;
			return new Point(x, y);
		}

		/// <summary>
		/// 楽譜内の位置と音階からマウス座標を割り出す関数。
		/// 一応中心座標が入るらしい(絶対的なので、Scoreからはみ出ることもある)
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
		/// 楽譜内での、追尾物の描画位置の決定(楽譜内では区切りつけないといけないから…)
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
		/// 現在のマウス座標が楽譜内で、どの位置でどの音階に一番近いかを決定する関数。
		/// 戻り値はXに位置、Yに音階が代入される。
		/// もし座標が楽譜外だった場合(0,0)になる。
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static Point DecidePlace(Point p)
		{			

			Point LowPlace = muphic.Common.ScoreTools.PointtoScore(p);
			Point Low = muphic.Common.ScoreTools.ScoretoPoint(LowPlace.X, LowPlace.Y);
			Point High = muphic.Common.ScoreTools.ScoretoPoint(LowPlace.X+1, LowPlace.Y+1);

			//位置の決定
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
			
			//音階の決定
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
			//今回決定した追尾物の座標が、楽譜からはみ出ないかチェック
			Point a = muphic.Common.ScoreTools.ScoretoPoint(neoX, neoY);
			if(109 <= a.X-(AnimalWidth/2) && a.X+(AnimalWidth/2) <= 109+555)
			{
				if(1 <= neoY && neoY <= 8)
				{
					//楽譜内なので、値を返す。
					return new Point(neoX, neoY);
				}
			}

			//楽譜からはみ出していたら意味がない。
			return new Point(0, 0);
		}

	}*/
	#endregion

	#region version2.1
	//version2 XGA化しただけ。あと、今までは楽譜の幅高さを数字で使っていたけど、変数を使うようにした。
	//version2.1 AnimalWidthを70から64に変更。最後の8分を表示するため61に変更

	/// <summary>
	/// ScoreTools の概要の説明です。
	/// </summary>
	public class ScoreTools
	{
		public const int AnimalWidth = 61;				//動物の最大横幅
		public const int AnimalHeight = 48;				//動物(というか道)の高さ
		public const int Kugiri = 2;					//動物の区切り。1だと4分のみ、2だと8分のみになる。
		public static Rectangle score = new Rectangle(112, 295, 773, 388);//楽譜の道部分の座標
		public ScoreTools()
		{
		}

		/// <summary>
		/// 座標が道の中にあるかどうかを調べる関数
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
		/// マウス座標を楽譜内の位置と音階に変える関数。
		/// 戻り値はXに位置、Yに音階が代入される。(絶対的なので、placeが負になることもありえる)
		/// </summary>
		/// <param name="p"></param>
		public static Point PointtoScore(Point p)
		{
			Rectangle score = muphic.Common.ScoreTools.score;
			//楽譜の左上の座標 = (109,181)
			//C4 : 25  0
			//B3 : 75  1
			//A3 : 125 2
			//G3 : 175 3
			//F3 : 225 4
			//E3 : 275 5
			//D3 : 325 6
			//C3 : 375 7
			//これだと、24までが0になる。
			int x = ((p.X-score.X)-(AnimalWidth/2)) / (AnimalWidth/Kugiri);
			int y = ((p.Y-score.Y)+(AnimalHeight/2))/AnimalHeight;
			return new Point(x, y);
		}

		/// <summary>
		/// 楽譜内の位置と音階からマウス座標を割り出す関数。
		/// 一応中心座標が入るらしい(絶対的なので、Scoreからはみ出ることもある)
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
		/// 楽譜内での、追尾物の描画位置の決定(楽譜内では区切りつけないといけないから…)
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
		/// 現在のマウス座標が楽譜内で、どの位置でどの音階に一番近いかを決定する関数。
		/// 戻り値はXに位置、Yに音階が代入される。
		/// もし座標が楽譜外だった場合(0,0)になる。
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static Point DecidePlace(Point p)
		{
			Rectangle score = muphic.Common.ScoreTools.score;
			Point LowPlace = muphic.Common.ScoreTools.PointtoScore(p);
			Point Low = muphic.Common.ScoreTools.ScoretoPoint(LowPlace.X, LowPlace.Y);
			Point High = muphic.Common.ScoreTools.ScoretoPoint(LowPlace.X+1, LowPlace.Y+1);

			//位置の決定
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
			
			//音階の決定
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
			//今回決定した追尾物の座標が、楽譜からはみ出ないかチェック
			Point a = muphic.Common.ScoreTools.ScoretoPoint(neoX, neoY);
			if(score.X <= a.X-(AnimalWidth/2) && a.X+(AnimalWidth/2) <= score.Right)
			{
				if(1 <= neoY && neoY <= 8)
				{
					//楽譜内なので、値を返す。
					return new Point(neoX, neoY);
				}
			}

			//楽譜からはみ出していたら意味がない。
			return new Point(0, 0);
		}

	}
	#endregion
}
