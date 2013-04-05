using System.Drawing;

namespace Muphic.CompositionScreenParts
{
	/// <summary>
	/// 汎用作曲画面で使用する各部品の座標や領域を定義したプロパティを公開する。
	/// </summary>
	public sealed class Locations
	{
		private Locations() { }

		/// <summary>
		/// 汎用作曲画面の領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle CompositionScreenArea
		{
			get { return new Rectangle(0, 0, 800, 600); }
		}

		/// <summary>
		/// 1番目の動物ボタンの座標を取得する。
		/// </summary>
		public static Point AnimalButton1
		{
			get { return new Point(710, 142); }
		}

		/// <summary>
		/// 2番目の動物ボタンの座標を取得する。
		/// </summary>
		public static Point AnimalButton2
		{
			get { return new Point(710, 197); }
		}

		/// <summary>
		/// 3番目の動物ボタンの座標を取得する。
		/// </summary>
		public static Point AnimalButton3
		{
			get { return new Point(710, 252); }
		}

		/// <summary>
		/// 4番目の動物ボタンの座標を取得する。
		/// </summary>
		public static Point AnimalButton4
		{
			get { return new Point(710, 307); }
		}

		/// <summary>
		/// 5番目の動物ボタンの座標を取得する。
		/// </summary>
		public static Point AnimalButton5
		{
			get { return new Point(710, 362); }
		}

		/// <summary>
		/// 6番目の動物ボタンの座標を取得する。
		/// </summary>
		public static Point AnimalButton6
		{
			get { return new Point(710, 417); }
		}

		/// <summary>
		/// 7番目の動物ボタンの座標を取得する。
		/// </summary>
		public static Point AnimalButton7
		{
			get { return new Point(710, 472); }
		}

		/// <summary>
		/// 8番目の動物ボタンの座標を取得する。
		/// </summary>
		public static Point AnimalButton8
		{
			get { return new Point(710, 537); }
		}

		/// <summary>
		/// 動物ボタンが配置される領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle AnimalButtonArea
		{
			get { return new Rectangle(696, 142, 97, 451); }
		}

		/// <summary>
		/// スクロールバー部分を含む、作曲を行う領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle CompositionArea
		{
			get { return new Rectangle(85, 142, 617, 449); }
		}

		/// <summary>
		/// 新規作成ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point CreateButton
		{
			get { return new Point(410, 98); }
		}

		/// <summary>
		/// 家の中央部分を示す座標を取得する。再生中に、動物がこの X 座標を超えた場合に音を鳴らす。
		/// </summary>
		public static Point HouseCenter
		{
			get { return new Point(45, 200); }
		}

		/// <summary>
		/// 家の窓を配置する位置を示す座標を取得する。
		/// このプロパティにより得られる座標は最上段レの音に対応する窓の座標であり、それ以下の窓は Y 座標を 40 ずつ加えた値となる。
		/// </summary>
		public static Point HouseWindow
		{
			get { return new Point(30, 197); }
		}

		/// <summary>
		/// 読み込みボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point LoadButton
		{
			get { return new Point(500, 8); }
		}

		/// <summary>
		/// 題名ボタンの座標を取得する。
		/// </summary>
		public static Point SaveButton
		{
			get { return new Point(410, 8); }
		}

		/// <summary>
		/// 再生ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point PlayButton
		{
			get { return new Point(106, 12); }
		}

		/// <summary>
		/// 続きから再生ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point PlayContinueButton
		{
			get { return new Point(196, 12); }
		}

		/// <summary>
		/// 楽譜テクスチャを表示する位置を示す座標を取得する。
		/// </summary>
		public static Point Score
		{
			get { return new Point(90, 190); }
		}

		/// <summary>
		/// 楽譜上での動物 1 匹分を示す大きさを取得する。
		/// このプロパティにより得られる値は、テクスチャの大きさではなく動物同士の間隔を示す。
		/// </summary>
		public static Size ScoreAnimalSize
		{
			get { return new Size(48, 39); }
		}

		/// <summary>
		/// 楽譜上での動物の配置基点位置を示す座標を取得する。
		/// 通常は、表示中の楽譜左上 (スクロール値 0 の場合、1 小節 1 音目のレの音) に対応する位置となる。
		/// </summary>
		public static Point ScoreBasePoint
		{
			get { return new Point(91, 195); }
		}

		/// <summary>
		///	楽譜ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point ScoreButton
		{
			get { return new Point(513, 100); }
		}

		/// <summary>
		/// スクロールバー部分は含まず、動物が配置できる領域のみとなる楽譜上の道を示す矩形を取得する。
		/// </summary>
		public static Rectangle ScoreArea
		{
			get { return new Rectangle(90, 198, 606, 347); }
		}

		/// <summary>
		/// スクロールバー部分の領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle ScrollArea
		{
			get { return new Rectangle(122, 556, 544, 27); }
		}

		/// <summary>
		/// 道を左にスクロールするボタンの表示位置を示す座標を取得する。
		/// </summary>
		public static Point ScrollLeftButton
		{
			get { return new Point(98, 555); }
		}
		
		/// <summary>
		/// 道を右にスクロールするボタンの表示位置を示す座標を取得する。
		/// </summary>
		public static Point ScrollRightButton
		{
			get { return new Point(666, 555); }
		}

		/// <summary>
		/// 楽譜での小節区切りとなる看板の表示位置を示す座標を取得する。
		/// </summary>
		public static Point SignBoard
		{
			get { return new Point(266, 153); }
		}

		/// <summary>
		/// テンポ表示の背景テクスチャの配置位置を示す座標を取得する。
		/// </summary>
		public static Point TempoBg
		{
			get { return new Point(126, 101); }
		}

		/// <summary>
		/// テンポを早くする左向きのボタンの表示位置を示す座標を取得する。
		/// </summary>
		public static Point TempoLeftButton
		{
			get { return new Point(104, 105); }
		}

		/// <summary>
		/// テンポを遅くする右向きのボタンの表示位置を示す座標を取得する。
		/// </summary>
		public static Point TempoRightButton
		{
			get { return new Point(253, 105); }
		}

		/// <summary>
		/// テンポ表示及びテンポ操作ボタンを配置する領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle TempoArea
		{
			get { return new Rectangle(95, 98, 190, 42); }
		}

		/// <summary>
		/// サムネイルを表示する位置を示す座標を取得する。
		/// </summary>
		public static Point Thumbnail
		{
			get { return new Point(592, 10); }
		}

		/// <summary>
		/// 題名表示パネルを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point Title
		{
			get { return new Point(293, 50); }
		}

		/// <summary>
		/// 題名ボタンの座標を取得する。
		/// </summary>
		public static Point TitleButton
		{
			get { return new Point(294, 8); }
		}

		/// <summary>
		/// 音階制限切り替えのメッセージを表示する座標を取得する。
		/// </summary>
		public static Point LimitModeMessage
		{
			get { return new Point(8, 570); }
		}
	}
}
