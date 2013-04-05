using System.Drawing;
using Muphic.Tools;

namespace Muphic.ScoreScreenParts
{
	/// <summary>
	/// 楽譜画面の部品や、楽譜の成分の配置に関する静的メソッド及び静的プロパティを提供する。
	/// </summary>
	public sealed class Locations
	{
		private Locations() { }

		/// <summary>
		/// 楽譜画面の領域を取得する。
		/// </summary>
		public static Rectangle ScoreScreenArea
		{
			get { return new Rectangle(0, 0, 800, 600); }
		}


		#region 各種ボタン及び画面表示パーツの配置座標定義プロパティ

		/// <summary>
		/// 戻るボタンの配置座標を取得する。
		/// </summary>
		public static Point BackButton
		{
			get { return new Point(10, 10); }
		}

		/// <summary>
		/// ヒツジボタンの配置座標を取得する。
		/// </summary>
		public static Point SheepButton
		{
			get { return new Point(150, 544); }
		}

		/// <summary>
		/// ウサギボタンの配置座標を取得する。
		/// </summary>
		public static Point RabbitButton
		{
			get { return new Point(234, 544); }
		}

		/// <summary>
		/// 鳥ボタンの配置座標を取得する。
		/// </summary>
		public static Point BirdButton
		{
			get { return new Point(318, 544); }
		}

		/// <summary>
		/// 犬ボタンの配置座標を取得する。
		/// </summary>
		public static Point DogButton
		{
			get { return new Point(402, 544); }
		}

		/// <summary>
		/// ブタボタンの配置座標を取得する。
		/// </summary>
		public static Point PigButton
		{
			get { return new Point(486, 544); }
		}

		/// <summary>
		/// ネコボタンの配置座標を取得する。
		/// </summary>
		public static Point CatButton
		{
			get { return new Point(570, 544); }
		}

		/// <summary>
		/// 音声ボタンの配置座標を取得する。
		/// </summary>
		public static Point VoiceButton
		{
			get { return new Point(654, 544); }
		}

		/// <summary>
		/// フルスコアボタンの配置座標を取得する。
		/// </summary>
		public static Point FullScoreButton
		{
			get { return new Point(66, 544); }
		}

		/// <summary>
		/// 印刷ボタンの配置座標を取得する。
		/// </summary>
		public static Point PrintButton
		{
			get { return new Point(695, 31); }
		}

		/// <summary>
		/// スクロールボタンの配置座標を取得する。
		/// </summary>
		public static Point ScrollBackButton
		{
			get { return new Point(738, 113); }
		}

		/// <summary>
		/// スクロールボタンの配置座標を取得する。
		/// </summary>
		public static Point ScrollNextButton
		{
			get { return new Point(738, 501); }
		}


		/// <summary>
		/// 楽譜の背景に表示する動物の絵の配置座標を取得する。
		/// </summary>
		public static Point AnimalLabel
		{
			get { return new Point(575, 405); }
		}

		/// <summary>
		/// 楽譜の背景画像の配置座標を取得する。
		/// </summary>
		public static Point ScoreBackground
		{
			get { return new Point(20, 98); }
		}

		/// <summary>
		/// 印刷時に表示するロゴの配置座標を取得する。
		/// </summary>
		public static Point MuphicLogo
		{
			get { return new Point(585, 30); }
		}

		/// <summary>
		/// 題名表示部の背景テクスチャの配置座標を取得する。
		/// </summary>
		public static Point Title
		{
			get { return new Point(129, 31); }
		}

		/// <summary>
		/// 題名表示部の文字列の配置座標を取得する。
		/// </summary>
		public static Point TitleString
		{
			get { return new Point(Locations.Title.X + 13, Locations.Title.Y + 12); }
		}

		#endregion


		#region 楽譜基点座標

		/// <summary>
		/// 楽譜表示の全ての表示座標の基点となる X 座標を取得する。
		/// </summary>
		private static int BaseX { get; set; } 

		/// <summary>
		/// 楽譜表示の全ての表示座標の基点となる X 座標を取得する。
		/// </summary>
		private static int BaseY { get; set; } 

		/// <summary>
		/// 楽譜表示の全ての表示座標の基点となる座標を標準値に設定する。
		/// </summary>
		public static void SetDefaultBasePoint()
		{
			Locations.BaseX = 24;
			Locations.BaseY = 100;
		}

		/// <summary>
		/// 楽譜表示の全て表示座標の基点となる座標を任意の値に設定する。
		/// </summary>
		/// <param name="x">基点となる X 座標。</param>
		/// <param name="y">基点となる Y 座標。</param>
		public static void SetBasePoint(int x, int y)
		{
			Locations.BaseX = x;
			Locations.BaseY = y;
		}

		#endregion


		#region 楽譜表示座標

		/// <summary>
		/// 指定した位置の拍の X 座標を取得する。
		/// </summary>
		/// <param name="meterLocation">行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		/// <returns>拍の X 座標。</returns>
		public static int GetX(int meterLocation)
		{
			// 基点位置 + 小節分 + 小節内分
			return Locations.BaseX + 109 + (ScoreTools.MeterToBar(meterLocation) * 146) + ((meterLocation % ScoreMain.MeterPerBar) * 17);
		}

		/// <summary>
		/// 指定された行の五線の Y 座標を取得する。
		/// </summary>
		/// <param name="line">取得する行。</param>
		/// <returns>五線の Y 座標。</returns>
		public static int GetY(int line)
		{
			// 基点位置 + 行数分
			return Locations.BaseY + 41 + 55 * (line - 1);
		}

		/// <summary>
		/// 音階による Y 座標オフセットを加えた音符画像の表示座標を取得する。
		/// </summary>
		/// <param name="line">取得する行。</param>
		/// <param name="code">音階。</param>
		/// <returns>Y 座標オフセットを加えた表示座標。</returns>
		public static int GetY(int line, int code)
		{
			return Locations.GetY(line) + ((code - 6) * 3);
		}

		/// <summary>
		/// 指定された行の五線の配置座標を取得する。
		/// </summary>
		/// <param name="line">取得する行。</param>
		/// <returns>五線の配置座標。</returns>
		public static Point GetStaffLocation(int line)
		{
			return new Point(Locations.BaseX + 62, Locations.GetY(line));
		}

		/// <summary>
		/// 指定された行のト音記号の配置座標を取得する。
		/// </summary>
		/// <param name="line">取得する行。</param>
		/// <returns>ト音記号の配置座標。</returns>
		public static Point GetGClefLocation(int line)
		{
			return new Point(Locations.BaseX + 65, Locations.BaseY + 33 + 55 * (line - 1));
		}

		/// <summary>
		/// 指定した行と位置の拍の座標を取得する。
		/// </summary>
		/// <param name="line">取得する座標の行。</param>
		/// <param name="meterLocation">行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		/// <returns>拍の座標。</returns>
		public static Point GetMeterLocation(int line, int meterLocation)
		{
			return new Point(GetX(meterLocation), GetY(line));
		}

		/// <summary>
		/// 指定した行、拍、音階の音符の表示座標を取得する。
		/// </summary>
		/// <param name="line">取得する座標の行。</param>
		/// <param name="meterLocation">行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		/// <param name="code">音階。</param>
		/// <returns>音符の表示座標。</returns>
		public static Point GetNoteLocation(int line, int meterLocation, int code)
		{
			return new Point(GetX(meterLocation), GetY(line, code));
		}

		/// <summary>
		/// 指定した行、拍、音階の音符の符幹の表示座標を取得する。
		/// </summary>
		/// <param name="line">取得する座標の行。</param>
		/// <param name="meterLocation">行頭からの拍の番号 (行頭の拍なら 0、行末の拍なら 31)。</param>
		/// <param name="code">音階。</param>
		/// <param name="isLeftSide">符幹を符頭の左に表示する場合は true、それ以外は false。</param>
		/// <param name="isUnder">符幹を符頭の下に表示する場合は true、それ以外は false。</param>
		/// <returns>音符の表示座標。</returns>
		public static Point GetNoteLineLocation(int line, int meterLocation, int code, bool isLeftSide, bool isUnder)
		{
			return new Point(GetX(meterLocation) - (isLeftSide ? 6 : 0), GetY(line, code + (isUnder ? 7 : 0)));
		}

		/// <summary>
		/// 終止線の表示座標を取得する。
		/// </summary>
		/// <param name="line">取得する座標の行。</param>
		/// <returns>終止線の表示座標。</returns>
		public static Point GetEndLineLocation(int line)
		{
			return new Point(Locations.BaseX + 696, Locations.GetY(line));
		}

		/// <summary>
		/// 五線の右端の座標を取得する。
		/// </summary>
		/// <param name="line">取得する座標の行。</param>
		/// <returns>終止線の表示座標。</returns>
		public static Point GetStaffEndLocation(int line)
		{
			return new Point(Locations.BaseX + 700, Locations.GetY(line));
		}

		/// <summary>
		/// 4/4 拍子の拍子記号の表示座標を取得する。
		/// </summary>
		/// <param name="line">取得する座標の行。</param>
		/// <returns>拍子記号の表示座標。</returns>
		public static Point GetTimeSignetureLocation(int line)
		{
			return new Point(Locations.BaseX + 87, Locations.GetY(line));
		}

		/// <summary>
		/// フルスコア表示時のラベルの配置座標を取得する。
		/// </summary>
		public static Point FullScoreLabel
		{
			get { return new Point(Locations.BaseX + 13, Locations.BaseY + 31); }
		}

		#endregion

	}
}
