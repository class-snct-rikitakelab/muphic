using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using Muphic.Archive;

namespace Muphic.Tools
{
	/// <summary>
	/// デバッグ用ツールクラス (継承不可)
	/// <para>デバッグビルド時のみ利用できる静的メソッドを公開する。</para>
	/// </summary>
	/// <remarks>
	/// このクラス内のメソッドは DEBUG 定数が定義されている状態でビルドした場合のみ有効になる。
	/// </remarks>
	public static class DebugTools
	{

		#region デバッグ用ウィンドウ

		/// <summary>
		/// 【デバッグ用】デバッグモード時の専用ウィンドウ。
		/// </summary>
		private static SubForms.DebugWindow DebugWindow { get; set; }

		/// <summary>
		/// 【デバッグ用】デバッグモード時の専用ウィンドウを生成する。
		/// </summary>
		[Conditional("DEBUG")]
		public static void InitializeDebugWindow()
		{
			DebugTools.DebugWindow = new Muphic.SubForms.DebugWindow();
		}

		/// <summary>
		/// 【デバッグ用】デバッグモード時の専用ウィンドウを表示する。
		/// </summary>
		[Conditional("DEBUG")]
		public static void ShowDebugWindow()
		{
			DebugTools.DebugWindow.SetSystemInfo();
			DebugTools.DebugWindow.Show();
		}

		/// <summary>
		/// 【デバッグ用】デバッグモード時の専用ウィンドウを破棄する。
		/// </summary>
		[Conditional("DEBUG")]
		public static void DisposeDebugWindow()
		{
			DebugTools.DebugWindow.Dispose();
		}

		#endregion


		#region コンソール出力

		/// <summary>
		/// 【デバッグ用】エラーの際にその旨をコンソールに出力する。
		/// </summary>
		/// <param name="errorPlace">エラー発生個所。</param>
		/// <param name="errorMessage">発生内容等のメッセージ。</param>
		[Conditional("DEBUG")]
		public static void ConsolOutputError(string errorPlace, string errorMessage)
		{
			DebugTools.ConsolOutputError(errorPlace, errorMessage, false);
		}
		/// <summary>
		/// 【デバッグ用】エラーの際にその旨をコンソール及びログファイルに出力する。
		/// </summary>
		/// <param name="errorPlace">エラー発生個所。</param>
		/// <param name="errorMessage">発生内容等のメッセージ。</param>
		/// <param name="writeLogfile">ログファイルに書き込む場合は true、それ以外は false。</param>
		[Conditional("DEBUG")]
		public static void ConsolOutputError(string errorPlace, string errorMessage, bool writeLogfile)
		{
			string writeMsg = CommonTools.CreateLogMessage(errorPlace, errorMessage, false, true, false);

			System.Diagnostics.Debug.WriteLine(writeMsg);
			DebugWindow.WriteLine(writeMsg);

			if (writeLogfile) Manager.LogFileManager.WriteLineError(errorPlace, errorMessage);
		}


		/// <summary>
		/// 【デバッグ用】コンソールに何らかのメッセージを出力する。
		/// </summary>
		/// <param name="message">メッセージ。</param>
		[Conditional("DEBUG")]
		public static void ConsolOutputMessage(string message)
		{
			DebugTools.ConsolOutputMessage(message, false);
		}
		/// <summary>
		/// 【デバッグ用】コンソールに何らかのメッセージを出力する。
		/// </summary>
		/// <param name="title">メッセージタイトル。</param>
		/// <param name="message">メッセージ本文。</param>
		[Conditional("DEBUG")]
		public static void ConsolOutputMessage(string title, string message)
		{
			DebugTools.ConsolOutputMessage(title, message, false);
		}
		/// <summary>
		/// 【デバッグ用】コンソール及びログファイルに何らかのメッセージを出力する。
		/// </summary>
		/// <param name="message">メッセージ。</param>
		/// <param name="writeLogfile">ログファイルに書き込む場合は true、それ以外は false。</param>
		[Conditional("DEBUG")]
		public static void ConsolOutputMessage(string message, bool writeLogfile)
		{
			DebugTools.ConsolOutputMessage(message, "", writeLogfile);
		}
		/// <summary>
		/// 【デバッグ用】コンソール及びログファイルに何らかのメッセージを出力する。
		/// </summary>
		/// <param name="title">メッセージタイトル。</param>
		/// <param name="message">メッセージ本文。</param>
		/// <param name="writeLogfile">ログファイルに書き込む場合は true、それ以外は false。</param>
		[Conditional("DEBUG")]
		public static void ConsolOutputMessage(string title, string message, bool writeLogfile)
		{
			string writeMsg = CommonTools.CreateLogMessage(title, message, false, false, false);

			System.Diagnostics.Debug.WriteLine(writeMsg);
			DebugWindow.WriteLine(writeMsg);

			if (writeLogfile) Manager.LogFileManager.WriteLineDebug(title, message);
		}

		#endregion


		#region ウィンドウ内の情報表示

		/// <summary>
		/// 【デバッグ用】画面左上にマウスポインタ座標を描画する。 DrawNowStatus メソッドで行っているので普通は使わない。
		/// </summary>
		[Conditional("DEBUG")]
		public static void DrawNowPoint()
		{
			Muphic.Manager.DrawManager.DrawString(Muphic.MainWindow.Instance.NowMouseLocation.ToString(), 0, 0);
		}


		/// <summary>
		/// 【デバッグ用】画面右下にFPS値を描画する。 DrawNowStatus メソッドで行っているので普通は使わない。
		/// </summary>
		[Conditional("DEBUG")]
		public static void DrawNowFPS()
		{
			Muphic.Manager.DrawManager.DrawString(Muphic.Manager.FrameManager.Fps.ToString("00.000"), 750, 580);
		}


		/// <summary>
		/// 【デバッグ用】画面左上に描画する、マウス情報・FPS などのステータス文字列の内部変数。
		/// １フレーム毎にメモリ確保操作実行するよりは、予め領域を確保しておきその領域のみ何度も使った方が速いのかと思った。
		/// </summary>
		private static StringBuilder strUpperLeft = new StringBuilder(50);

		/// <summary>
		/// 【デバッグ用】画面右下に描画する、muphic のバージョン情報などのステータス文字列。
		/// </summary>
		private static readonly string strLowerRight =
			"                 muphic v7 \r\n" + 
			"Debug Mode, Build " + 
			FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;

		/// <summary>
		/// 【デバッグ用】画面左上にマウスや FPS の情報を、画面右下に muphic の情報を描画する。
		/// </summary>
		[Conditional("DEBUG")]
		public static void DrawNowStatus()
		{
			// 左上用のバッファをクリア
			strUpperLeft.Remove(0, strUpperLeft.Length);

			// 左上用のバッファに、マウス座標とクリックの状態、FPS値を書き込む
			strUpperLeft.Append("Mouse ");
			strUpperLeft.Append("X=").Append(Muphic.MainWindow.Instance.NowMouseLocation.X.ToString("000")).Append(" ");
			strUpperLeft.Append("Y=").Append(Muphic.MainWindow.Instance.NowMouseLocation.Y.ToString("000")).Append(" ");
			strUpperLeft.Append("Click:").AppendLine(Muphic.MainWindow.Instance.IsClicked ? "ON " : "OFF ");
			strUpperLeft.Append(Muphic.Manager.FrameManager.Fps.ToString("00.000")).Append(" FPS");

			// 左上用のバッファの内容を画面左上に書き出し
			Muphic.Manager.DrawManager.DrawString(strUpperLeft.ToString(), 2, 2);

			// 右下用の文字列を画面右下に書き出し
			Muphic.Manager.DrawManager.DrawString(strLowerRight, 589, 568);
		}

		#endregion


		#region その他

		/// <summary>
		/// 【デバッグ用】任意のメッセージで例外を発生させる。
		/// </summary>
		/// <param name="message">例外発生時のメッセージ。</param>
		/// <exception cref="System.Exception">任意のメッセージで意図的に発生させる例外</exception>
		[Conditional("DEBUG")]
		public static void ThrowException(string message)
		{
			throw new System.Exception(message);
		}

		/// <summary>
		/// 【デバッグ用】意図的に遅延を発生させ, 処理落ちさせる。
		/// </summary>
		[Conditional("DEBUG")]
		public static void Delay()
		{
			for (long i = 0; i < 10000000; i++) ;
		}

		#endregion


		/// <summary>
		/// 【デバッグ用】プログラム開始時に実行される。主にテストコード実行に使用。
		/// </summary>
		[Conditional("DEBUG")]
		public static void Test()
		{
			#region	画像切り出しに使ったようなもの1
			//var fileList = Directory.GetFiles(@"E:\Documents\Research\muphic\muphic ver.7b\muphic\bin\x86\Debug\temp");

			//foreach (string fileName in fileList)
			//{
			//    File.Copy(@"E:\Documents\Research\muphic\muphic ver.7b\muphic\bin\x86\Debug\source.png", Path.Combine(@"E:\Documents\Research\muphic\muphic ver.7b\muphic\bin\x86\Debug\make", Path.GetFileName(fileName)));
			//}
			#endregion

			#region	画像切り出しに使ったようなもの2
			//Dictionary<string, Rectangle> hogeList = new Dictionary<string, Rectangle>();

			//string txtfileName = @"作業\def.txt";

			//string str;						// 作業用文字列
			//var r = new Rectangle();		// 作業用四角形領域 (前の行のテクスチャの位置・サイズを記憶するため) 
			//StreamReader reader;			// 読み込みバッファ
			//int line_num = 0;				// なんとなく行数をカウントしてみる

			//try															// 読み込みバッファを設定
			//{
			//    reader = new StreamReader(txtfileName, System.Text.Encoding.GetEncoding("Shift_JIS"));
			//}
			//catch (System.Exception)									// 読み込みに失敗したらfalseを返す
			//{
			//    return;
			//}

			//while ((str = reader.ReadLine()) != null)					// 行末まで1行ごと読み込み
			//{
			//    string[] temp;
			//    string[] temp_r;
			//    line_num++;

			//    str = Muphic.Tools.CommonTools.RemoveStr(str, "//");	// 先ずは、コメント部分の削除

			//    temp = str.Split(new char[] { '\t' }, System.StringSplitOptions.RemoveEmptyEntries);		// 文字列をテクスチャ名部とRectangle部に分割

			//    if (temp.Length == 0) continue;							// 分割して得た文字列の数が0だった場合は空行と判断し読み飛ばす

			//    if (temp.Length != 2)									// 分割して得た文字列の数が2つでなかった場合は不正なフォーマットとして弾く
			//    {
			//        continue;
			//    }

			//    temp_r = temp[1].Split(new char[] { '\t', ' ', ',' }, System.StringSplitOptions.RemoveEmptyEntries);	// Rectangle部の文字列をカンマで分割

			//    if (temp_r.Length != 4 && temp_r.Length != 2)			// 分割したRectangleの文字列の数が4か2でなかった場合は不正なフォーマットとして弾く
			//    {
			//        continue;
			//    }

			//    if (temp_r[0].IndexOf('+') == 0) r.X += int.Parse(temp_r[0].Substring(1));			// RectangleのXの先頭文字が'+'だった場合、前の行のXに足し合わせる
			//    else if (temp_r[0].IndexOf('-') == 0) r.Y -= int.Parse(temp_r[0].Substring(1));		// RectangleのXの先頭文字が'-'だった場合、前の行のXから引く
			//    else r.X = int.Parse(temp_r[0]);													// それ以外であれば、Xにそのまま値を入れる

			//    if (temp_r[1].IndexOf('+') == 0) r.Y += int.Parse(temp_r[1].Substring(1));			// RectangleのYの先頭文字が'+'だった場合、前の行のYに足し合わせる
			//    else if (temp_r[1].IndexOf('-') == 0) r.Y -= int.Parse(temp_r[1].Substring(1));		// RectangleのYの先頭文字が'-'だった場合、前の行のYから引く
			//    else r.Y = int.Parse(temp_r[1]);													// それ以外であれば、Yにそのまま値を入れる

			//    if (temp_r.Length == 4)									// Rectangleの文字列数が4だった場合は横幅と縦幅も設定する
			//    {														// Rectangleの文字列数が2だった場合は前の行の横幅と縦幅をそのまま使用
			//        r.Width = int.Parse(temp_r[2]);						// 横幅設定
			//        r.Height = int.Parse(temp_r[3]);					// 縦幅設定
			//    }

			//    hogeList.Add(temp[0], r);
			//}


			//foreach (KeyValuePair<string, Rectangle> texInfo in hogeList)
			//{
			//    Image result = new Bitmap(texInfo.Value.Width, texInfo.Value.Height);

			//    Graphics g = Graphics.FromImage(result);
			//    g.DrawImage(Bitmap.FromFile(@"作業\source.png"), new Rectangle(new Point(0, 0), texInfo.Value.Size), texInfo.Value, GraphicsUnit.Pixel);


			//    result.Save(Path.Combine(@"作業\make_oo", texInfo.Key + ".png"), System.Drawing.Imaging.ImageFormat.Png);
			//}
			#endregion

		}
	}
}
