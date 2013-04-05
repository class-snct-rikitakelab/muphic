using System.Drawing;

namespace Muphic.EntitleScreenParts
{
	/// <summary>
	/// 汎用題名入力画面で使用する各部品の座標や領域を定義したプロパティを公開する。
	/// </summary>
	public sealed class Locations
	{
		private Locations() { }

		/// <summary>
		/// 汎用代目入力画面の領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle EntitleScreenArea
		{
			get { return new Rectangle(0, 0, 800, 600); }
		}

		/// <summary>
		/// 全削除ボタンの配置位置を示す座標を取得する。
		/// </summary>
		public static Point AllDeleteButton
		{
			get { return new Point(671, 523); }
		}

		/// <summary>
		/// アルファベットのカテゴリ選択ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point AlphabetCategoryButton
		{
			get { return new Point(230, 528); }
		}

		/// <summary>
		/// ひらがなのカテゴリ選択ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point HiraganaCategoryButton
		{
			get { return new Point(46, 528); }
		}

		/// <summary>
		/// カタカナのカテゴリ選択ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point KatakanaCategoryButton
		{
			get { return new Point(138, 528); }
		}

		/// <summary>
		/// 数字・記号のカテゴリ選択ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point NumberCategoryButton
		{
			get { return new Point(322, 528); }
		}

		/// <summary>
		/// 文字のボタンを配置する領域を示す矩形を取得する。
		/// </summary>
		public static Rectangle CharArea
		{
			get { return new Rectangle(60, 165, 680, 300); }
		}

		/// <summary>
		/// 文字のボタンのうち、基点となる左上に配置されるボタンの位置を示す座標を取得する。
		/// </summary>
		public static Point CharButtonBase
		{
			get { return new Point(71, 193); }
		}

		/// <summary>
		/// 文字ボタン同士の表示座標の差を示す大きさを取得する。
		/// </summary>
		public static Size CharButtonDiff
		{
			get { return new Size(51, 44); }
		}

		/// <summary>
		/// 決定ボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point DecisionButton
		{
			get { return new Point(12, 12); }
		}

		/// <summary>
		/// 選択されているカテゴリの名称を表示するラベルを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point EntitleCategoryLabel
		{
			get { return new Point(66, 125); }
		}

		/// <summary>
		/// 1 文字削除するボタンを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point SingleDeleteButton
		{
			get { return new Point(575, 523); }
		}

		/// <summary>
		/// 題名表示部分の背景テクスチャを配置する位置を示す座標を取得する。
		/// </summary>
		public static Point Title
		{
			get { return new Point(129, 45); }
		}

		/// <summary>
		/// 入力する題名の説明分を配置する位置を示す座標を取得する。
		/// </summary>
		public static Point ExplainText
		{
			get { return new Point(137, 19); }
		}

	}
}
