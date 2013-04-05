using System.Drawing;

namespace Muphic.Common.ExplorerParts
{
	/// <summary>
	/// ファイル エクスプローラで使用する各部品の座標や領域を定義した静的メソッド及びプロパティを提供する。
	/// </summary>
	public sealed class Settings
	{
		/// <summary>
		/// ファイル エクスプローラの領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle ExplorerArea
		{
			get { return new Rectangle(0, 0, 800, 600); }
		}

		/// <summary>
		/// ディレクトリ選択領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle DirectorySelectArea
		{
			get { return new Rectangle(25, 51, 322, 530); }
		}

		/// <summary>
		/// ファイル選択領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle FileSelectArea
		{
			get { return new Rectangle(376, 51, 397, 247); }
		}

		/// <summary>
		/// 作品情報領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle WorkSelectArea
		{
			get { return new Rectangle(376, 352, 397, 166); }
		}


		/// <summary>
		/// 選択ボタンの表示座標から、そのボタンで表示するテキストの表示座標を取得する。
		/// </summary>
		/// <param name="selectButtonLocation">選択ボタンの表示座標。</param>
		/// <returns>テキストの表示座標。</returns>
		public static Point GetSelectButtonTextLocation(Point selectButtonLocation)
		{
			return new Point(selectButtonLocation.X + 9, selectButtonLocation.Y + 2);
		}

		/// <summary>
		/// テキストの行間隔を取得する。
		/// </summary>
		public static int TextLineSpacing
		{
			get { return 24; }
		}

		/// <summary>
		/// エクスプローラ上の読込ボタンの配置座標を取得する。
		/// </summary>
		public static Point LoadButtonLocation
		{
			get { return new Point(594, 535); }
		}

		/// <summary>
		/// エクスプローラ上のキャンセルボタンの配置座標を取得する。
		/// </summary>
		public static Point CancelButtonLocation
		{
			get { return new Point(688, 535); }
		}

		/// <summary>
		/// エクスプローラ上の印刷ボタンの配置座標を取得する。
		/// </summary>
		public static Point PrintButtonLocation
		{
			get { return new Point(500, 535); }
		}


		#region ディレクトリ選択領域

		/// <summary>
		/// ディレクトリ名として使用できる最大の文字数を取得する。通常、コンピュータ名として許可された文字数である 15。
		/// </summary>
		public static int MaxDirectoryNameLength
		{
			get { return 15; }
		}

		/// <summary>
		/// 表示できる最大のディレクトリ数を取得する。
		/// </summary>
		public static int MaxShowDirectoryNum
		{
			get { return 19; }
		}

		/// <summary>
		/// ディレクトリ数カウントの際に除外する条件となる文字を取得する。
		/// </summary>
		public static char DeselectionKeyChar
		{
			get { return '#'; }
		}

		/// <summary>
		/// 対象ディレクトリ直下のファイルを含む特殊ディレクトリの名前を取得する。
		/// </summary>
		public static string TargetDirectoryDefaultName
		{
			get { return "ほぞんしたものがたり"; }
		}


		/// <summary>
		/// ディレクトリ選択領域の、ディレクトリ名選択ボタンの表示座標を取得する。
		/// </summary>
		/// <param name="index">取得する選択ボタンのリストの番号。</param>
		/// <returns>選択ボタンの表示座標。</returns>
		public static Point GetDirecyorySelectButtonLocation(int index)
		{
			return new Point(30, 60 + (index * Settings.TextLineSpacing));
		}


		/// <summary>
		/// 更新ボタンの配置座標を取得する。
		/// </summary>
		public static Point ReloadButtonLocation
		{
			get { return new Point(257, 532); }
		}

		/// <summary>
		/// ディレクトリ数の配置座標を取得する。
		/// </summary>
		public static Point DirectoryNumLocation
		{
			get { return new Point(147, 544); }
		}

		/// <summary>
		/// ディレクトリ選択領域の上スクロールボタンの配置座標を取得する。
		/// </summary>
		public static Point DirectoryUpperScrollButton
		{
			get { return new Point(323, 55); }
		}

		/// <summary>
		/// ディレクトリ選択領域の下スクロールボタンの配置座標を取得する。
		/// </summary>
		public static Point DirectoryLowerScrollButton
		{
			get { return new Point(323, 497); }
		}

		#endregion


		#region ファイル選択領域

		/// <summary>
		/// ディレクトリ名として使用できる最大の文字数を取得する。通常、コンピュータ名として許可された文字数である 15。
		/// </summary>
		public static int MaxFileNameLength
		{
			get { return Muphic.Settings.System.Default.StoryMake_MaxTitleLength; }
		}

		/// <summary>
		/// 表示できる最大のディレクトリ数を取得する。
		/// </summary>
		public static int MaxShowFileNum
		{
			get { return 9; }
		}


		/// <summary>
		/// ファイル選択領域の、ファイル名選択ボタンの表示座標を取得する。
		/// </summary>
		/// <param name="index">取得する選択ボタンのリストの番号。</param>
		/// <returns>選択ボタンの表示座標。</returns>
		public static Point GetFileSelectButtonLocation(int index)
		{
			return new Point(380, 60 + (index * Settings.TextLineSpacing));
		}


		/// <summary>
		/// ファイル選択領域の上スクロールボタンの配置座標を取得する。
		/// </summary>
		public static Point FileUpperScrollButton
		{
			get { return new Point(323, 55); }
		}

		/// <summary>
		/// ファイル選択領域の下スクロールボタンの配置座標を取得する。
		/// </summary>
		public static Point FileLowerScrollButton
		{
			get { return new Point(323, 497); }
		}

		#endregion


		#region 作品情報領域

		/// <summary>
		/// 作品情報領域の、作品情報の表示座標を取得する。
		/// </summary>
		/// <param name="line">取得する作品情報の行数を示す 0 から始まる整数。</param>
		/// <returns>作品情報の表示座標。</returns>
		public static Point GetWorkInfoTextLocation(int line)
		{
			return new Point(468, 365 + (line * Settings.TextLineSpacing));
		}


		#endregion

	}
}
