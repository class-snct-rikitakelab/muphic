using System.Drawing;

namespace Muphic.TopScreenParts
{
	/// <summary>
	/// トップ画面の部品の配置や動作に関する静的メソッド及び静的プロパティを公開する。
	/// </summary>
	public sealed class Locations
	{
		private Locations() { }


		/// <summary>
		/// ひとりでおんがく選択ボタンの配置座標を取得する。
		/// </summary>
		public static Point OneScreenButton
		{
			get { return new Point(366, 320); }
		}


		/// <summary>
		/// ものがたりおんがく選択ボタンの配置座標を取得する。
		/// </summary>
		public static Point StoryScreenButton
		{
			get { return new Point(576, 320); }
		}


		/// <summary>
		/// 名前入力ボタンの配置座標を取得する。
		/// </summary>
		public static Point NameInputScreenButton
		{
			get { return new Point(429, 229); }
		}


		/// <summary>
		/// 終了ボタンの配置座標を取得する。
		/// </summary>
		public static Point EndButton
		{
			get { return new Point(468, 500); }
		}
	}
}
