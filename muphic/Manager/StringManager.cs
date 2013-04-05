using System.Drawing;
using Microsoft.VisualBasic;

namespace Muphic.Manager
{
	/// <summary>
	/// 文字列管理クラス (シングルトン・継承不可) 
	/// <para>DrawManager を使用し、システム上で表示する文字列をテクスチャとして描画する。</para>
	/// </summary>
	public sealed class StringManager : Manager
	{
		/// <summary>
		/// 文字列管理クラスの静的ンスタンス (シングルトンパターン) 
		/// </summary>
		private static StringManager __instance = new StringManager();

		/// <summary>
		/// 文字列管理クラスの静的ンスタンス (シングルトンパターン) 
		/// </summary>
		private static StringManager Instance
		{
			get { return StringManager.__instance; }
		}


		/// <summary>
		/// 文字テクスチャの横幅及び縦幅を表わす System.Drawing.Size 。
		/// </summary>
		public static Size StringSize { get; private set; }


		#region コンストラクタ/初期化

		/// <summary>
		/// 文字列管理クラスの新しインスタンスを初期化する。
		/// </summary>
		private StringManager()
		{
		}


		/// <summary>
		/// 文字列管理クラスの静的インスタンス生成及び使用する統合文字テクスチャの登録を行う。
		/// </summary>
		private void _Initialize()
		{
			if (this._IsInitialized) return;

			// 文字テクスチャファイルを管理クラスに登録
			DrawManager.LoadTextureFiles(Tools.CommonTools.GetResourceFileNames(Settings.ResourceNames.CharacterImages));

			// 文字幅を取得
			StringManager.StringSize = TextureFileManager.GetRectangle("あ").Size;

			Tools.DebugTools.ConsolOutputMessage("StringManager -Initialize", "文字列管理クラス生成完了");

			this._IsInitialized = true;
		}

		#endregion


		#region 文字列描画

		/// <summary>
		/// 予めテクスチャとして用意された文字を使用し、テクスチャとして文字列を描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="isCenter">描画開始座標。</param>
		/// <param name="location">描画開始座標が文字列の中央の場合は true、それ以外は false、</param>
		/// <param name="alpha">透過度。</param>
		private void _DrawString(string drawString, Point location, bool isCenter, byte alpha)
		{
			drawString = Strings.StrConv(drawString, VbStrConv.Wide, 0x0411);	// 半角文字を全て全角にする

			if (isCenter)
			{
				int width = StringManager.StringSize.Width * drawString.Length;												// 文字列全体の横幅を計算
				location = Tools.CommonTools.CenterToOnreft(location, new Size(width, StringManager.StringSize.Height));	// 中央座標から左上座標を取得
			}

			for (int i = 0; i < drawString.Length; i++)
			{
				DrawManager.Draw(drawString[i].ToString(), location, alpha);	// i番目の文字を描画
				location.X += StringManager.StringSize.Width;					// 文字の幅の1.5分の1だけx座標をずらす(数字を半角サイズにするため)
			}
		}


		/// <summary>
		/// 予めテクスチャとして用意された文字を使用し、テクスチャとしてシステム用の数字を描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="isCenter">描画開始座標。</param>
		/// <param name="location">描画開始座標が文字列の中央の場合は true、それ以外は false、</param>
		/// <param name="alpha">透過度。</param>
		private void _DrawSystemNumber(int drawNumber, Point location, bool isCenter, byte alpha)
		{
			string drawString = Strings.StrConv(drawNumber.ToString(), VbStrConv.Wide, 0x0411);		// 半角文字を全て全角にした数値文字列を用意する

			if (isCenter)
			{
				int width = StringManager.StringSize.Width + (drawString.Length - 1) * (int)(StringManager.StringSize.Width / 1.5F);	// 文字列全体の横幅を計算
				location = Tools.CommonTools.CenterToOnreft(location, new Size(width, StringManager.StringSize.Height));				// 中央座標から左上座標を取得
			}

			for (int i = 0; i < drawString.Length; i++)
			{
				DrawManager.Draw(drawString[i].ToString(), location, alpha);	// i番目の文字を描画
				location.X += (int)(StringManager.StringSize.Width / 1.5F);		// 文字の幅の1.5分の1だけx座標をずらす(数字を半角サイズにするため)
			}
		}

		#endregion


		#region ツール

		/// <summary>
		/// 与えられた文字列に含まれる半角文字を全て全角文字に変換する。
		/// </summary>
		/// <param name="str">変換する文字列。</param>
		/// <returns>全て全角文字に変換された文字列。</returns>
		private string _convertHalfsizeToFullsize(string str)
		{
			return Strings.StrConv(str, VbStrConv.Wide, 0x0411);
		}

		#endregion


		#region 外部から呼ばれるメソッド群

		/// <summary>
		/// 文字列管理クラスの静的インスタンス生成及び使用する統合文字テクスチャの登録を行う。
		/// <para>シングルトンパターンとして一度生成された後はこのメソッドは意味を為さないので注意。</para>
		/// </summary>
		public static void Initialize()
		{
			Muphic.Manager.StringManager.Instance._Initialize();
		}

		/// <summary>
		/// 与えられた文字列に含まれる半角文字を全て全角文字に変換する。
		/// </summary>
		/// <param name="str">変換する文字列。</param>
		/// <returns>全て全角文字に変換された文字列。</returns>
		public static string ConvertHalfsizeToFullsize(string str)
		{
			return Muphic.Manager.StringManager.Instance._convertHalfsizeToFullsize(str);
		}


		#region Draw

		/// <summary>
		/// 指定された文字列に対応するテクスチャを描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="x">描画開始 x 座標。</param>
		/// <param name="y">描画開始 y 座標。</param>
		public static void Draw(string drawString, int x, int y)
		{
			Muphic.Manager.StringManager.Instance._DrawString(drawString, new Point(x, y), false, (byte)255);
		}

		/// <summary>
		/// 指定された文字列に対応するテクスチャを描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="location">描画開始座標。</param>
		public static void Draw(string drawString, Point location)
		{
			Muphic.Manager.StringManager.Instance._DrawString(drawString, location, false, (byte)255);
		}

		/// <summary>
		/// 指定された文字列に対応するテクスチャを描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="x">描画開始 x 座標。</param>
		/// <param name="y">描画開始 y 座標。</param>
		/// <param name="alpha">透過度。</param>
		public static void Draw(string drawString, int x, int y, byte alpha)
		{
			Muphic.Manager.StringManager.Instance._DrawString(drawString, new Point(x, y), false, alpha);
		}

		/// <summary>
		/// 指定された文字列に対応するテクスチャを描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="location">描画開始座標。</param>
		/// <param name="alpha">透過度。</param>
		public static void Draw(string drawString, Point location, byte alpha)
		{
			Muphic.Manager.StringManager.Instance._DrawString(drawString, location, false, alpha);
		}

		#endregion

		#region DrawCenter

		/// <summary>
		/// 指定された文字列に対応するテクスチャを描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="x">描画する文字列の中心 x 座標。</param>
		/// <param name="y">描画する文字列の中心 y 座標。</param>
		public static void DrawCenter(string drawString, int x, int y)
		{
			Muphic.Manager.StringManager.Instance._DrawString(drawString, new Point(x, y), true, (byte)255);
		}

		/// <summary>
		/// 指定された文字列に対応するテクスチャを描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="location">描画する文字列の中心座標。</param>
		public static void DrawCenter(string drawString, Point location)
		{
			Muphic.Manager.StringManager.Instance._DrawString(drawString, location, true, (byte)255);
		}

		/// <summary>
		/// 指定された文字列に対応するテクスチャを描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="x">描画する文字列の中心 x 座標。</param>
		/// <param name="y">描画する文字列の中心 y 座標。</param>
		/// <param name="alpha">透過度。</param>
		public static void DrawCenter(string drawString, int x, int y, byte alpha)
		{
			Muphic.Manager.StringManager.Instance._DrawString(drawString, new Point(x, y), true, alpha);
		}

		/// <summary>
		/// 指定された文字列に対応するテクスチャを描画する。
		/// </summary>
		/// <param name="drawString">描画する文字列。</param>
		/// <param name="location">描画する文字列の中心座標。</param>
		/// <param name="alpha">透過度。</param>
		public static void DrawCenter(string drawString, Point location, byte alpha)
		{
			Muphic.Manager.StringManager.Instance._DrawString(drawString, location, true, alpha);
		}

		#endregion

		#region SystemDraw

		/// <summary>
		/// 指定された数値に対応するテクスチャをシステム用に描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="x">描画開始 x 座標。</param>
		/// <param name="y">描画開始 y 座標。</param>
		public static void SystemDraw(int drawNumber, int x, int y)
		{
			Muphic.Manager.StringManager.Instance._DrawSystemNumber(drawNumber, new Point(x, y), false, (byte)255);
		}

		/// <summary>
		/// 指定された数値に対応するテクスチャをシステム用に描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="location">描画開始座標。</param>
		public static void SystemDraw(int drawNumber, Point location)
		{
			Muphic.Manager.StringManager.Instance._DrawSystemNumber(drawNumber, location, false, (byte)255);
		}

		/// <summary>
		/// 指定された数値に対応するテクスチャをシステム用に描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="x">描画開始 x 座標。</param>
		/// <param name="y">描画開始 y 座標。</param>
		/// <param name="alpha">透過度。</param>
		public static void SystemDraw(int drawNumber, int x, int y, byte alpha)
		{
			Muphic.Manager.StringManager.Instance._DrawSystemNumber(drawNumber, new Point(x, y), false, alpha);
		}

		/// <summary>
		/// 指定された数値に対応するテクスチャをシステム用に描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="location">描画開始座標。</param>
		/// <param name="alpha">透過度。</param>
		public static void SystemDraw(int drawNumber, Point location, byte alpha)
		{
			Muphic.Manager.StringManager.Instance._DrawSystemNumber(drawNumber, location, false, alpha);
		}

		#endregion

		#region SystemDrawCenter

		/// <summary>
		/// 指定された数値に対応するテクスチャをシステム用に描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="x">描画する文字列の中心 x 座標。</param>
		/// <param name="y">描画する文字列の中心 y 座標。</param>
		public static void SystemDrawCenter(int drawNumber, int x, int y)
		{
			Muphic.Manager.StringManager.Instance._DrawSystemNumber(drawNumber, new Point(x, y), true, (byte)255);
		}

		/// <summary>
		/// 指定された数値に対応するテクスチャをシステム用に描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="location">描画する文字列の中心座標。</param>
		public static void SystemDrawCenter(int drawNumber, Point location)
		{
			Muphic.Manager.StringManager.Instance._DrawSystemNumber(drawNumber, location, true, (byte)255);
		}

		/// <summary>
		/// 指定された数値に対応するテクスチャをシステム用に描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="x">描画する文字列の中心 x 座標。</param>
		/// <param name="y">描画する文字列の中心 y 座標</param>
		/// <param name="alpha">透過度。</param>
		public static void SystemDrawCenter(int drawNumber, int x, int y, byte alpha)
		{
			Muphic.Manager.StringManager.Instance._DrawSystemNumber(drawNumber, new Point(x, y), true, alpha);
		}

		/// <summary>
		/// 指定された数値に対応するテクスチャをシステム用に描画する。
		/// </summary>
		/// <param name="drawNumber">描画する整数。</param>
		/// <param name="location">描画する文字列の中心座標。</param>
		/// <param name="alpha">透過度。</param>
		public static void SystemDrawCenter(int drawNumber, Point location, byte alpha)
		{
			Muphic.Manager.StringManager.Instance._DrawSystemNumber(drawNumber, location, true, alpha);
		}

		#endregion

		#endregion

	}
}
