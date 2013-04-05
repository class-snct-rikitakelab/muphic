using System.Drawing;

namespace Muphic.NameInputScreenParts
{
	/// <summary>
	/// プレイヤー名入力画面の部品の配置や動作に関する静的メソッド及び静的プロパティを公開する。
	/// </summary>
	public sealed class Locations
	{
		private Locations() { }

		/// <summary>
		/// プレイヤー1 選択ボタンの配置座標を取得する。
		/// </summary>
		public static Point Player1Button
		{
			get { return new Point(282, 28); }
		}

		/// <summary>
		/// プレイヤー1 表示領域の配置座標を取得する。
		/// </summary>
		public static Point Player1NameArea
		{
			get { return new Point(365, 29); }
		}


		/// <summary>
		/// プレイヤー2 選択ボタンの配置座標を取得する。
		/// </summary>
		public static Point Player2Button
		{
			get { return new Point(282, 76); }
		}

		/// <summary>
		/// プレイヤー2 表示領域の配置座標を取得する。
		/// </summary>
		public static Point Player2NameArea
		{
			get { return new Point(365, 77); }
		}
		

		/// <summary>
		/// プレイヤー名入力を促す画像の配置座標を取得する。
		/// </summary>
		public static Point PlayerInputTitle
		{
			get { return new Point(100, 34); }
		}


		/// <summary>
		/// プレイヤー1 性別選択の男児ボタンの配置座標を取得する。
		/// </summary>
		public static Point Player1BoyButton
		{
			get { return new Point(670, 31); }
		}

		/// <summary>
		/// プレイヤー1 性別選択の女児ボタンの配置座標を取得する。
		/// </summary>
		public static Point Player1GirlButton
		{
			get { return new Point(725, 31); }
		}

		/// <summary>
		/// プレイヤー2 性別選択の男児ボタンの配置座標を取得する。
		/// </summary>
		public static Point Player2BoyButton
		{
			get { return new Point(670, 79); }
		}

		/// <summary>
		/// プレイヤー2 性別選択の女児ボタンの配置座標を取得する。
		/// </summary>
		public static Point Player2GirlButton
		{
			get { return new Point(725, 79); }
		}

	}
}
