using System.Drawing;

namespace Muphic.Common.DialogParts
{
	/// <summary>
	/// 汎用ダイアログで使用する各部品の座標や領域を定義したプロパティを公開する。
	/// </summary>
	public sealed class Locations
	{
		private Locations() { }

		/// <summary>
		/// ダイアログに表示するタイトルの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point Title
		{
			get { return new Point(69, 24); }
		}

		/// <summary>
		/// ダイアログのアイコンの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point Icon
		{
			get { return new Point(11, 12); }
		}

		/// <summary>
		/// ダイアログに表示するメッセージの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point Message
		{
			get { return new Point(40, 65); }
		}

		/// <summary>
		/// "もどる" ボタンの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point BackButton
		{
			get { return new Point(348, 171); }
		}

		/// <summary>
		/// "けってい" ボタンの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point DecisionButton
		{
			get { return new Point(289, 171); }
		}

		/// <summary>
		/// ファイル選択ダイアログの選択領域の背景テクスチャの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point SelectArea
		{
			get { return new Point(9, 109); }
		}

		/// <summary>
		/// ファイル選択ダイアログ内で選択可能なファイル一覧を下へスクロールさせるボタンの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point ScrollDownButton
		{
			get { return new Point(316, 183); }
		}

		/// <summary>
		/// ファイル選択ダイアログ内で選択可能なファイル一覧を上へスクロールさせるボタンの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point ScrollUpButton
		{
			get { return new Point(316, 119); }
		}

		/// <summary>
		/// はい / いいえ のボタンを表示する確認ダイアログで、"はい" のボタンの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point YesButton
		{
			get { return new Point(100, 163); }
		}

		/// <summary>
		/// "はい" / "いいえ" のボタンを表示する確認ダイアログで、"いいえ" のボタンの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point NoButton
		{
			get { return new Point(237, 163); }
		}

		/// <summary>
		/// "はい" のボタンのみが表示されるダイアログで、"はい" ボタンの配置位置を示す、ダイアログの左上を基点とした座標を取得する。
		/// </summary>
		public static Point YesButtonCenter
		{
			get { return new Point(168, 163); }
		}

		/// <summary>
		/// 
		/// </summary>
		public static Point ListBase
		{
			get { return new Point(17, 117); }
		}
	}
}
