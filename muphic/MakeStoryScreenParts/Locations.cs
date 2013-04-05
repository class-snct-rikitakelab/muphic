using System.Drawing;
using Muphic.Tools;

namespace Muphic.MakeStoryScreenParts
{
	/// <summary>
	/// 物語作成画面の部品の配置や動作に関する静的メソッド及び静的プロパティを公開する。
	/// </summary>
	public sealed class Locations
	{
		private Locations() { }


		#region ボタン

		#region 物語作曲画面

		/// <summary>
		/// 音階制限選択領域を取得する。
		/// </summary>
		public static Rectangle LimiSelectArea
		{
			get { return new Rectangle(286, 2, 300, 44); }
		}

		/// <summary>
		/// 音階制限選択ボタンの配置座標を取得する。
		/// </summary>
		public static Point[] LimitSelectButtonLocations
		{
			get
			{
				return new Point[]
				{
					new Point(364, 7),
					new Point(408, 7),
					new Point(452, 7),
					new Point(496, 7),
					new Point(540, 7)
				};
			}
		}

		#endregion


		#endregion


		#region ダイアログ

		#region 作品提出ダイアログ

		/// <summary>
		/// 作品提出ダイアログで、提出待ちのアニメーションを表示する基点座標を取得する。
		/// ダイアログの左上を (0, 0) とした相対座標である点に注意。
		/// </summary>
		public static Point SubmitDialogWaitingAnimation
		{
			get { return new Point(60, 155); }
		}

		/// <summary>
		/// 作品提出ダイアログで、提出待ちのアニメーションの切り替え間隔を取得する。
		/// </summary>
		public static int SubmitDialogWaitingAnimationTimeSpan
		{
			get { return 400; }
		}

		/// <summary>
		/// 作品提出ダイアログで、提出待ちのアニメーションの切り替え時の移動距離を取得する。
		/// </summary>
		public static int SubmitDialogWaitingAnimationLocationSpan
		{
			get { return 50; }
		}

		/// <summary>
		/// 作品提出ダイアログで、提出待ちのアニメーションを切り替える回数を取得する。
		/// </summary>
		public static int SubmitDialogWaitingAnimationNum
		{
			get { return 7; }
		}

		#endregion

		#region 作者名入力ダイアログ

		/// <summary>
		/// 制作者名入力ダイアログで、"なまえ" ボタンを描画する座標群。
		/// </summary>
		private static Point[] __nameInputButtonLocations =
			new Point[] { new Point(16, 82), new Point(16, 126) };

		/// <summary>
		/// 制作者名入力ダイアログで、"なまえ" ボタンを描画する座標群を取得する。
		/// </summary>
		public static Point[] NameInputButtonLocations
		{
			get { return Locations.__nameInputButtonLocations; }
		}

		/// <summary>
		/// 制作者名入力ダイアログで、制作者名を描画する座標群。
		/// </summary>
		private static Point[] __nameInputTextLocations =
			new Point[] { new Point(104, 82), new Point(104, 126) };

		/// <summary>
		/// 制作者名入力ダイアログで、制作者名を描画する座標群を取得する。
		/// </summary>
		public static Point[] NameInputTextLocations
		{
			get { return Locations.__nameInputTextLocations; }
		}

		#endregion

		#endregion


		#region 印刷関連

		#region 児童向け物語印刷

		/// <summary>
		/// 児童向け物語印刷での "だいめい" を配置する座標を取得する。
		/// </summary>
		public static Point PrintForStudentTitleDescLocation
		{
			get { return new Point(80, 48); }
		}

		/// <summary>
		/// 児童向け物語印刷での "だいめい" 文字列を取得する。
		/// </summary>
		public static string PrintForStudentTitleDesc
		{
			get { return "だいめい"; }
		}

		/// <summary>
		/// 児童向け物語印刷での "だいめい" 文字列の文字の大きさを取得する。
		/// </summary>
		public static float PrintForStudentTitleDescFontSize
		{
			get { return 16F; }
		}
		

		/// <summary>
		/// 児童向け物語印刷での、物語の題名を配置する座標を取得する。
		/// </summary>
		public static Point PrintForStudentTitleLocation
		{
			get { return new Point(170, 40); }
		}

		/// <summary>
		/// 児童向け物語印刷での、物語の題名の文字の大きさを取得する。
		/// </summary>
		public static float PrintForStudentTitleFontSize
		{
			get { return 24F; }
		}

		/// <summary>
		/// 児童向け物語印刷での、物語の題名を取得する。通常、%str1% を題名に置き換えて利用する。
		/// </summary>
		public static string PrintForStudentTitle
		{
			get { return "「%str1%」"; }
		}


		/// <summary>
		/// 児童向け物語印刷での、絵を配置する座標を取得する。
		/// </summary>
		public static Point PrintForStudentSlideLocation
		{
			get { return new Point(90 , 100); }
		}

		/// <summary>
		/// 児童向け物語印刷での、絵の拡大・縮小率を取得する。
		/// </summary>
		public static float PrintForStudentSlideScaling
		{
			get { return 1F; }
		}


		/// <summary>
		/// 児童向け物語印刷での、楽譜の配置座標を取得する。
		/// </summary>
		public static Point[] PrintForStudentScorePageLocation
		{
			get { return new Point[] { new Point(80, 600), new Point(800, 600) }; }
		}

		/// <summary>
		/// 児童向け物語印刷での、楽譜の拡大・縮小率を取得する。
		/// </summary>
		public static float PrintForStudentScorePageScaling
		{
			get { return 0.75F; }
		}


		/// <summary>
		/// 児童向け物語印刷での、ページ数の文字列を取得する。通常、%str1% を現在のページ数に置き換えて利用する。
		/// </summary>
		public static string PrintForStudentPageDesc
		{
			//get { return "ページ  %str1% / %str2%"; }
			get { return "%str1% まいめ"; }
		}

		/// <summary>
		/// 児童向け物語印刷での、ページ数の文字列を配置する座標を取得する。
		/// </summary>
		public static Point PrintForStudentPageDescLocation
		{
			get { return new Point(1030, 765); }
		}

		/// <summary>
		/// 児童向け物語印刷での、ページ数の文字列の文字の大きさを取得する。
		/// </summary>
		public static float PrintForStudentPageDescFontSize
		{
			get { return 15F; }
		}


		/// <summary>
		/// 児童向け物語印刷での、muphic のロゴの配置座標を取得する。
		/// </summary>
		public static Point PrintForStudentLogoLocation
		{
			get { return new Point(920, 25); }
		}


		/// <summary>
		/// 児童向け物語印刷での、文章の配置座標を取得する。
		/// </summary>
		public static Point[] PrintForStudentSentenceLocation
		{
			get { return new Point[] { new Point(690, 332), new Point(690, 372), new Point(690, 412) }; }
		}

		/// <summary>
		/// 児童向け物語印刷での、文章の文字の大きさを取得する。
		/// </summary>
		public static float PrintForStudentSentenceFontSize
		{
			get { return 20; }
		}


		/// <summary>
		/// 児童向け物語印刷での、制作者の文字列の配置座標を取得する。
		/// </summary>
		public static Point[] PrintForStudentAuthorLocations
		{
			get { return new Point[] { new Point(720, 130), new Point(720, 160) }; }
		}

		/// <summary>
		/// 児童向け物語印刷での、制作者の文字列の文字の大きさを取得する。
		/// </summary>
		public static int PrintForStudentAuthorFontSize
		{
			get { return 20; }
		}


		/// <summary>
		/// 児童向け物語印刷での、"つくったひと" 文字列の配置座標を取得する。
		/// </summary>
		public static Point PrintForStudentAuthorDescLocation
		{
			get { return new Point(690, 100); }
		}

		/// <summary>
		/// 児童向け物語印刷での、"つくったひと" 文字列を取得する。
		/// </summary>
		public static string PrintForStudentAuthorDesc
		{
			get { return "つくったひと"; }
		}

		/// <summary>
		/// 児童向け物語印刷での、制作者の文字列の文字の大きさを取得する。
		/// </summary>
		public static int PrintForStudentAuthorDescFontSize
		{
			get { return 14; }
		}

		#endregion

		#endregion

	}
}
