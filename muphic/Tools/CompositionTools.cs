using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Muphic.Tools
{
	/// <summary>
	/// 作曲用ツールクラス（継承不可）
	/// <para>作曲画面に関連する座標計算等のツールとなる静的メソッドを公開する。</para>
	/// </summary>
	public static class CompositionTools
	{
		/// <summary>
		/// 楽譜上の道の部分を示す矩形。スクロールバー部分は含まれない。
		/// </summary>
		private static readonly Rectangle scoreArea = Muphic.CompositionScreenParts.Locations.ScoreArea;

		/// <summary>
		/// 楽譜上での動物 1 匹分の大きさ。
		/// </summary>
		private static readonly Size animalSize = Muphic.CompositionScreenParts.Locations.ScoreAnimalSize;

		/// <summary>
		/// 楽譜上での動物の配置基点座標。
		/// </summary>
		private static readonly Point scoreBasePoint = Muphic.CompositionScreenParts.Locations.ScoreBasePoint;


		/// <summary>
		/// 指定されたマウス座標が楽譜(道)内にあるかどうかを調べる。
		/// </summary>
		/// <param name="mouseLocation">調べるマウス座標。</param>
		/// <returns>楽譜(道)内であればtrue、それ以外はfalse。</returns>
		public static bool CheckInScore(Point mouseLocation)
		{
			// 設定ファイル内から楽譜の矩形領域を得る
			// mousePointが矩形内であればtrueを返し、それ以外ならfalseを返す。
			if (scoreArea.Contains(mouseLocation)) return true;
			else return false;
		}


		/// <summary>
		/// 指定されたマウス座標を楽譜内の位置 (横) と音階 (縦) に変換する。
		/// </summary>
		/// <param name="mouseLocation">マウス座標。</param>
		/// <returns>x に楽譜内での位置、y に楽譜内での音階が格納された System.Drawing.Point 型。</returns>
		public static Point PointToScore(Point mouseLocation)
		{
			return new Point(
				((mouseLocation.X - scoreBasePoint.X) - (animalSize.Width / 2)) / (animalSize.Width / Settings.System.Default.CompositionMeterNum),
				((mouseLocation.Y - scoreBasePoint.Y) + (animalSize.Height / 2)) / animalSize.Height
			);
		}

		/// <summary>
		/// 指定された楽譜内の位置 (横) と音階 (縦) をマウス座標に変換する。
		/// </summary>
		/// <param name="place">位置。</param>
		/// <param name="code">音階。</param>
		/// <param name="isCenter">画像の中央画像を取得する場合は true、それ以外は false。</param>
		/// <returns>マウス座標。</returns>
		public static Point ScoreToPoint(int place, int code, bool isCenter)
		{
			if (isCenter) return CompositionTools.ScoreToPoint(place, code);
			else return new Point(
				place * (animalSize.Width / Settings.System.Default.CompositionMeterNum) + scoreBasePoint.X - 1,
				(code - 1) * animalSize.Height + scoreBasePoint.Y + 1
			);
		}

		/// <summary>
		/// 指定された楽譜内の位置 (横) と音階 (縦) をマウス座標に変換する。
		/// </summary>
		/// <param name="place">位置。</param>
		/// <param name="code">音階。</param>
		/// <returns>マウス座標。</returns>
		public static Point ScoreToPoint(int place, int code)
		{
			return new Point(
				place * (animalSize.Width / Settings.System.Default.CompositionMeterNum) + (animalSize.Width / 2) + scoreBasePoint.X,
				code * animalSize.Height - (animalSize.Height / 2) + scoreBasePoint.Y
			);
		}

		/// <summary>
		/// 指定された動物の位置 (横) と音階 (縦) をマウス座標に変換する。
		/// </summary>
		/// <param name="animal">動物。</param>
		/// <returns>マウス座標。</returns>
		public static Point ScoreToPoint(CompositionScreenParts.Animal animal)
		{
			return CompositionTools.ScoreToPoint(animal.Place, animal.Code);
		}


		/// <summary>
		/// 主に楽譜上での動物を描画する座標を決定する。
		/// </summary>
		/// <param name="mouseLocation">マウス座標。</param>
		/// <returns>動物を描画する座標。</returns>
		public static Point GetAnimalDrawLocation(Point mouseLocation)
		{
			// マウス座標から最も近い楽譜上での位置と音階を得る
			Point nearestScore = CompositionTools.GetNearestScore(mouseLocation);

			// 位置と音階が空で返ってきたら、mousePointは楽譜外であると判断し、そのまま返す
			if (nearestScore == Point.Empty) return mouseLocation;

			// 位置と音階が決まったら、それに対応するマウス座標を返す
			return CompositionTools.ScoreToPoint(nearestScore.X, nearestScore.Y);
		}


		/// <summary>
		/// 指定されたマウス座標がどの位置・音階に一番近いかを返す。
		/// </summary>
		/// <param name="mousePoint">マウス座標。</param>
		/// <returns>xに楽譜内での位置、yに楽譜内での音階が格納されたPoint型の値。ただし、マウス座標が楽譜の外だった場合は(0,0)が返る。</returns>
		public static Point GetNearestScore(Point mousePoint)
		{
			// そもそもマウス座標が楽譜外だったらお話にならない
			if (!scoreArea.Contains(mousePoint)) return Point.Empty;

			Point result = new Point();
			Point lowPlace = CompositionTools.PointToScore(mousePoint);
			Point low = CompositionTools.ScoreToPoint(lowPlace.X, lowPlace.Y);
			Point high = CompositionTools.ScoreToPoint(lowPlace.X + 1, lowPlace.Y + 1);
			Rectangle scoreRectangle = scoreArea;

			// 楽譜上での位置(横)を決定する
			int tempLow = Math.Abs(low.X - mousePoint.X);
			int tempHigh = Math.Abs(high.X - mousePoint.X);

			if (tempLow <= tempHigh) result.X = lowPlace.X;
			else result.X = lowPlace.X + 1;


			// 楽譜上での音階(縦)を決定する
			tempLow = Math.Abs(low.Y - mousePoint.Y);
			tempHigh = Math.Abs(high.Y - mousePoint.Y);

			if (tempLow <= tempHigh) result.Y = lowPlace.Y;
			else result.Y = lowPlace.Y + 1;

			mousePoint = CompositionTools.ScoreToPoint(result.X, result.Y);
			tempLow = mousePoint.X - animalSize.Width / 2;
			tempHigh = mousePoint.X + animalSize.Width / 2;

			// 八分音符を使用しない設定で、かつ位置が奇数だった場合、偶数に調整して返す
			if (!Manager.ConfigurationManager.Current.IsUseEighthNote && result.X % 2 == 1) result.X++;

			if (scoreRectangle.Left <= tempLow && tempHigh <= scoreRectangle.Right && 1 <= result.Y && result.Y <= 9)
			{
				return result;
			}

			return Point.Empty;
		}


		/// <summary>
		/// 動物から再生する音声ファイル名を得る。
		/// </summary>
		/// <param name="animal">位置と音階を含む動物。</param>
		/// <returns>音声ファイル名。</returns>
		public static string GetSoundFileName(CompositionScreenParts.Animal animal)
		{
			return CompositionTools.GetSoundFileName(animal.Code, animal.ToString());
		}

		/// <summary>
		/// 動物の音階・動物名から再生する音声ファイル名を得る。
		/// </summary>
		/// <param name="code">動物の音階。</param>
		/// <param name="animal">動物名。</param>
		/// <returns>音声ファイル名。</returns>
		public static string GetSoundFileName(int code, string animal)
		{
			return Tools.CommonTools.GetResourceMessage(Settings.ResourceNames.SoundFile, animal, code.ToString());
		}

	}
}
