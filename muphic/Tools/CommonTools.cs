using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace Muphic.Tools
{
	/// <summary>
	/// 共通ツールクラス（継承不可）
	/// <para>プログラム全体で使用する静的メソッドを公開する。</para>
	/// </summary>
	public static class CommonTools
	{

		#region 座標計算メソッド群

		/// <summary>
		/// テクスチャの中央座標とサイズから、テクスチャの左上座標を算出する。
		/// </summary>
		/// <param name="rectangle">求めるテクスチャの中央座標・サイズ。</param>
		/// <returns>左上座標。</returns>
		public static Point CenterToOnreft(Rectangle rectangle)
		{
			return CommonTools.CenterToOnreft(rectangle.Location, rectangle.Size);
		}

		/// <summary>
		/// テクスチャの中央座標とサイズから、テクスチャの左上座標を算出する。
		/// </summary>
		/// <param name="centerLoction">中央座標。</param>
		/// <param name="size">サイズ。</param>
		/// <returns>左上座標。</returns>
		public static Point CenterToOnreft(Point centerLoction, Size size)
		{
			return new Point(centerLoction.X - size.Width / 2, centerLoction.Y - size.Height / 2);
		}

		/// <summary>
		/// テクスチャの左上座標とサイズから、テクスチャの中央座標を算出する。
		/// </summary>
		/// <param name="onreftLocation">左上座標。</param>
		/// <param name="size">サイズ。</param>
		/// <returns>中央座標。</returns>
		public static Point OnreftToCenter(Point onreftLocation, Size size)
		{
			return new Point(onreftLocation.X + size.Width / 2, onreftLocation.Y + size.Height / 2);
		}


		/// <summary>
		/// 二つの System.Drawing.Point を足し合わせる。
		/// </summary>
		/// <param name="point1">足し合わせる System.Drawing.Point。</param>
		/// <param name="point2">足し合わせる System.Drawing.Point。</param>
		/// <returns>足し合わせた結果の System.Drawing.Point。</returns>
		public static Point AddPoints(Point point1, Point point2)
		{
			return new Point(point1.X + point2.X, point1.Y + point2.Y);
		}


		#endregion


		#region 文字列操作メソッド群

		/// <summary>
		/// 与えられた文字列から指定された文字列を削除する。
		/// </summary>
		/// <param name="str">元の文字列。</param>
		/// <param name="remove">削除する文字列。</param>
		/// <returns>元の文字列から指定された文字列を削除した文字列。</returns>
		public static string RemoveStr(string str, string remove)
		{
			int num = str.IndexOf(remove);
			if (num != -1)
			{
				str = str.Remove(num, str.Length - num);
			}
			return str;
		}


		/// <summary>
		/// セミコロンで区切られた統合画像ファイル名リストを分割し、描画管理クラスに読み込まれているかどうかを示すブール値とのジェネリック・コレクションに追加する。
		/// </summary>
		/// <param name="fileNameList">追加する対象のジェネリック・コレクション。</param>
		/// <param name="resourceFileNames">セミコロンで区切られたファイル名リスト。</param>
		public static void GetResourceFileNames(Dictionary<string, bool> fileNameList, string resourceFileNames)
		{
			// セミコロンで分割されたファイル名を、それぞれ先頭と末尾にある空白を削除してリストに追加。
			foreach (string fileName in resourceFileNames.Split(new string[] { ";" }, System.StringSplitOptions.RemoveEmptyEntries))
			{
				if(string.IsNullOrEmpty(fileName.Trim())) continue;

				fileNameList.Add(fileName.Trim(), Manager.DrawManager.ExistsTextureFile(fileName.Trim()));
			}
		}

		/// <summary>
		/// 統合画像ファイル名リストを分割し、描画管理クラスに読み込まれているかどうかを示すブール値とのジェネリック・コレクションに追加する。
		/// </summary>
		/// <param name="fileNameList">追加する対象のジェネリック・コレクション。</param>
		/// <param name="resourceFileNames">ファイル名リスト。</param>
		public static void GetResourceFileNames(Dictionary<string, bool> fileNameList, string[] resourceFileNames)
		{
			// セミコロンで分割されたファイル名を、それぞれ先頭と末尾にある空白を削除してリストに追加。
			foreach (string fileName in resourceFileNames)
			{
				if (string.IsNullOrEmpty(fileName.Trim())) continue;

				fileNameList.Add(fileName.Trim(), Manager.DrawManager.ExistsTextureFile(fileName.Trim()));
			}
		}

		/// <summary>
		/// セミコロンで区切られた統合画像ファイル名リストを分割し、配列にして返戻する。
		/// </summary>
		/// <param name="resourceFileNames">セミコロンで区切られたファイル名リスト。</param>
		public static string[] GetResourceFileNames(string resourceFileNames)
		{
			List<string> fileNameList = new List<string>();

			// セミコロンで分割されたファイル名を、それぞれ先頭と末尾にある空白を削除してリストに追加。
			foreach (string fileName in resourceFileNames.Split(new string[] { ";" }, System.StringSplitOptions.RemoveEmptyEntries))
			{
				if (string.IsNullOrEmpty(fileName.Trim())) continue;

				fileNameList.Add(fileName.Trim());
			}

			return fileNameList.ToArray();
		}


		/// <summary>
		/// 指定した文字数に収まるように文字列をカットし、カットしたことを示す文字列 "..." を挿入した表示用の文字列を生成する。
		/// <para>(例: "SampleDisplayString", 13, "..." → "SampleDisp...")</para>
		/// </summary>
		/// <param name="source">元の文字列。</param>
		/// <param name="length">収める文字数。</param>
		/// <returns>表示用のカットされた文字列。</returns>
		public static string TrimDisplayString(string source, int length)
		{
			return CommonTools.TrimDisplayString(source, length, "...");
		}

		/// <summary>
		/// 指定した文字数に収まるように文字列をカットし、カットしたことを示す文字列を挿入した表示用の文字列を生成する。
		/// <para>(例: "SampleDisplayString", 13, "..." → "SampleDisp...")</para>
		/// </summary>
		/// <param name="source">元の文字列。</param>
		/// <param name="length">収める文字数。</param>
		/// <param name="trimStr">カットしたことを示す文字列。</param>
		/// <returns>表示用のカットされた文字列。</returns>
		public static string TrimDisplayString(string source, int length, string trimStr)
		{
			if (source.Length <= length) return source;

			return source.Substring(0, length - trimStr.Length) + trimStr;
		}

		#endregion


		#region DirectInput キーコード変換

		/// <summary>
		/// DirectInput のキー識別コードから、入力されるアルファベットの文字列を取得する。
		/// </summary>
		/// <param name="key">取得する DirectInput のキー識別コード。</param>
		/// <param name="isShiftKey">Shift キーが押されている場合は true、それ以外は false。</param>
		/// <returns>入力されるアルファベットの文字列。</returns>
		public static string KeyToAlphabet(Key key, bool isShiftKey)
		{
			switch (key)
			{
				case Key.A:
					return isShiftKey ? "A" : "a";
				case Key.B:
					return isShiftKey ? "B" : "b";
				case Key.C:
					return isShiftKey ? "C" : "c";
				case Key.D:
					return isShiftKey ? "D" : "d";
				case Key.E:
					return isShiftKey ? "E" : "e";
				case Key.F:
					return isShiftKey ? "F" : "f";
				case Key.G:
					return isShiftKey ? "G" : "g";
				case Key.H:
					return isShiftKey ? "H" : "h";
				case Key.I:
					return isShiftKey ? "I" : "i";
				case Key.J:
					return isShiftKey ? "J" : "j";
				case Key.K:
					return isShiftKey ? "K" : "k";
				case Key.L:
					return isShiftKey ? "L" : "l";
				case Key.M:
					return isShiftKey ? "M" : "m";
				case Key.N:
					return isShiftKey ? "N" : "n";
				case Key.O:
					return isShiftKey ? "O" : "o";
				case Key.P:
					return isShiftKey ? "P" : "p";
				case Key.Q:
					return isShiftKey ? "Q" : "q";
				case Key.R:
					return isShiftKey ? "R" : "r";
				case Key.S:
					return isShiftKey ? "S" : "s";
				case Key.T:
					return isShiftKey ? "T" : "t";
				case Key.U:
					return isShiftKey ? "U" : "u";
				case Key.V:
					return isShiftKey ? "V" : "v";
				case Key.W:
					return isShiftKey ? "W" : "w";
				case Key.X:
					return isShiftKey ? "X" : "x";
				case Key.Y:
					return isShiftKey ? "Y" : "y";
				case Key.Z:
					return isShiftKey ? "Z" : "z";

				default:
					return "";
			}
		}


		/// <summary>
		/// DirectInput のキー識別コードから、入力される記号の文字列を取得する。
		/// </summary>
		/// <param name="key">取得する DirectInput のキー識別コード。</param>
		/// <param name="isShiftKey">Shift キーが押されている場合は true、それ以外は false。</param>
		/// <param name="isJapanese">日本語入力の場合は true、アルファベット入力の場合は false。</param>
		/// <returns>入力されるアルファベットの文字列。</returns>
		public static string KeyToSymbol(Key key, bool isShiftKey, bool isJapanese)
		{
			switch (key)
			{
				case Key.D1:
					return isShiftKey ? "！" : "1";
				case Key.D2:
					return isShiftKey ? "♪" : "2";
				case Key.D3:
					return isShiftKey ? "＃" : "3";
				case Key.D4:
					return isShiftKey ? "＄" : "4";
				case Key.D5:
					return isShiftKey ? "％" : "5";
				case Key.D6:
					return isShiftKey ? "＆" : "6";
				case Key.D7:
					return isShiftKey ? "" : "7";
				case Key.D8:
					return isShiftKey ? "（" : "8";
				case Key.D9:
					return isShiftKey ? "）" : "9";
				case Key.D0:
					return isShiftKey ? "" : "0";
				case Key.Minus:
					return isShiftKey ? "＝" : isJapanese ? "ー" : "－";
				case Key.Circumflex:
					return isShiftKey ? "～" : "～";
				case Key.Yen:
					return isShiftKey ? "Σ" : "￥";
				case Key.At:
					return isShiftKey ? "※" : "＠";
				case Key.LeftBracket:
					return isShiftKey ? "【" : "「";
				case Key.SemiColon:
					return isShiftKey ? "＋" : "；";
				case Key.Colon:
					return isShiftKey ? "＊" : "：";
				case Key.RightBracket:
					return isShiftKey ? "】" : "」";
				case Key.Comma:
					return isShiftKey ? "＜" : isJapanese ? "、" : "，";
				case Key.Period:
					return isShiftKey ? "＞" : isJapanese ? "。" : "．";
				case Key.Slash:
					return isShiftKey ? "？" : isJapanese ? "・" : "／";
				case Key.BackSlash:
					return isShiftKey ? "＿" : isJapanese ? "￥" : "＼";

				case Key.NumPad1:
					return "1";
				case Key.NumPad2:
					return "2";
				case Key.NumPad3:
					return "3";
				case Key.NumPad4:
					return "4";
				case Key.NumPad5:
					return "5";
				case Key.NumPad6:
					return "6";
				case Key.NumPad7:
					return "7";
				case Key.NumPad8:
					return "8";
				case Key.NumPad9:
					return "9";
				case Key.NumPad0:
					return "0";
				case Key.NumPadComma:
					return "，";
				case Key.NumPadEquals:
					return "＝";
				case Key.NumPadMinus:
					return "－";
				case Key.NumPadPeriod:
					return "．";
				case Key.NumPadPlus:
					return "＋";
				case Key.NumPadSlash:
					return "／";
				case Key.NumPadStar:
					return "＊";

				case Key.Space:
					return "　";

				default:
					return "";
			}
		}


		/// <summary>
		/// DirectInput のキー識別コードから、入力されるかなの文字列を取得する (かなモード用)。
		/// </summary>
		/// <param name="key">取得する DirectInput のキー識別コード。</param>
		/// <returns>入力されるかなの文字列。</returns>
		public static string KeyToKana(Key key)
		{
			switch (key)
			{
				case Key.A:
					return "ち";
				case Key.B:
					return "こ";
				case Key.C:
					return "そ";
				case Key.D:
					return "し";
				case Key.E:
					return "い";
				case Key.F:
					return "は";
				case Key.G:
					return "き";
				case Key.H:
					return "く";
				case Key.I:
					return "に";
				case Key.J:
					return "ま";
				case Key.K:
					return "の";
				case Key.L:
					return "り";
				case Key.M:
					return "も";
				case Key.N:
					return "み";
				case Key.O:
					return "ら";
				case Key.P:
					return "せ";
				case Key.Q:
					return "た";
				case Key.R:
					return "す";
				case Key.S:
					return "と";
				case Key.T:
					return "か";
				case Key.U:
					return "な";
				case Key.V:
					return "ひ";
				case Key.W:
					return "て";
				case Key.X:
					return "さ";
				case Key.Y:
					return "ん";
				case Key.Z:
					return "つ";

				case Key.D1:
					return "ぬ";
				case Key.D2:
					return "ふ";
				case Key.D3:
					return "あ";
				case Key.D4:
					return "う";
				case Key.D5:
					return "え";
				case Key.D6:
					return "お";
				case Key.D7:
					return "や";
				case Key.D8:
					return "ゆ";
				case Key.D9:
					return "よ";
				case Key.D0:
					return "わ";
				case Key.Minus:
					return "ほ";
				case Key.Circumflex:
					return "へ";
				case Key.Yen:
					return "＿";
				case Key.At:
					return "゛";
				case Key.LeftBracket:
					return "゜";
				case Key.SemiColon:
					return "れ";
				case Key.Colon:
					return "け";
				case Key.RightBracket:
					return "む";
				case Key.Comma:
					return "ね";
				case Key.Period:
					return "る";
				case Key.Slash:
					return "め";
				case Key.BackSlash:
					return "ろ";

				default:
					return "";
			}
		}

		#endregion


		#region 設定取得メソッド群

		/// <summary>
		/// Muphic.Settings.System から設定情報を取得する。
		/// </summary>
		/// <typeparam name="Type">取得する設定情報の型</typeparam>
		/// <param name="key">取得する設定名。</param>
		/// <returns>設定値。</returns>
		public static Type GetSettings<Type>(string key)
		{
			try
			{
				return (Type)Settings.System.Default[key];
			}
			catch (System.Configuration.SettingsPropertyNotFoundException)
			{
				Tools.DebugTools.ConsolOutputError("CommonTools.GetSettings", "設定\"" + key + "\"の取得に失敗(設定が存在しない)");
			}
			catch (System.FormatException)
			{
				Tools.DebugTools.ConsolOutputError("CommonTools.GetSettings", "設定\"" + key + "\"の取得に失敗(設定値の型が異なる)");
			}

			return default(Type);
		}

		#endregion


		#region メッセージ関連メソッド群

		/// <summary>
		/// 与えられたメッセージ内に含まれるフィールドを、特定の文字列に置換する。
		/// </summary>
		/// <param name="resourceMsg">文字列リソースのメッセージ。</param>
		/// <param name="str1">メッセージ内の %str1% と置き換える文字列。</param>
		/// <returns>メッセージ内の %str1% が引数 str1 に置き換えられた文字列。</returns>
		public static string GetResourceMessage(string resourceMsg, string str1)
		{
			return resourceMsg.Replace("%str1%", str1);
		}

		/// <summary>
		/// 与えられたメッセージ内に含まれるフィールドを、特定の文字列に置換する。
		/// </summary>
		/// <param name="resourceMsg">文字列リソースのメッセージ。</param>
		/// <param name="str1">メッセージ内の %str1% と置き換える文字列。</param>
		/// <param name="str2">メッセージ内の %str2% と置き換える文字列。</param>
		/// <returns>メッセージ内の %strX% が引数 strX に置き換えられた文字列。</returns>
		public static string GetResourceMessage(string resourceMsg, string str1, string str2)
		{
			return GetResourceMessage(resourceMsg.Replace("%str2%", str2), str1);
		}

		/// <summary>
		/// 与えられたメッセージ内に含まれるフィールドを、特定の文字列に置換する。
		/// </summary>
		/// <param name="resourceMsg">文字列リソースのメッセージ。</param>
		/// <param name="str1">メッセージ内の %str1% と置き換える文字列。</param>
		/// <param name="str2">メッセージ内の %str2% と置き換える文字列。</param>
		/// <param name="str3">メッセージ内の %str3% と置き換える文字列。</param>
		/// <returns>メッセージ内の %strX% が引数 strX に置き換えられた文字列。</returns>
		public static string GetResourceMessage(string resourceMsg, string str1, string str2, string str3)
		{
			return GetResourceMessage(resourceMsg.Replace("%str3%", str3), str1, str2);
		}


		/// <summary>
		/// ログ用のメッセージ文字列を生成する。
		/// </summary>
		/// <param name="message1">メッセージ1 (主にメッセージタイトル)。</param>
		/// <param name="message2">メッセージ2 (主にメッセージ本文)。</param>
		/// <param name="isDatetime">日付を含める場合は true、それ以外は false。</param>
		/// <param name="isError">エラーメッセージであると明示する場合は true、それ以外は false。</param>
		/// <param name="isDebug">デバッグメッセージであると明示する場合は true、それ以外は false。</param>
		public static string CreateLogMessage(string message1, string message2, bool isDatetime, bool isError, bool isDebug)
		{
			// 生成するメッセージ
			StringBuilder messageResult = new StringBuilder();

			if (isDatetime)						// 日付を含めるよう指定されている場合
			{									// システム時刻を取得し、追加
				messageResult.Append(System.DateTime.Now.ToString());
				messageResult.Append(" - ");
			}
			if (isDebug)						// デバッグメッセージを明示するよう指定されている場合
			{									// デバッグメッセージであることを示す文字列を追加
				messageResult.Append("$ ");
			}
			if (isError)						// エラーメッセージを明示するよう指定されている場合
			{									// エラーメッセージであることを示す文字列を追加
				messageResult.Append("# ERROR # ");
			}

			messageResult.Append(message1);		// メッセージ１を追加


			if (string.IsNullOrEmpty(message2))
			{
				// メッセージ２が NULL もしくは String.Empty に相当するものだった場合
				// メッセージ２の書き込みは行わず、スキップ
			}
			else
			{
				// メッセージ２の書き込みを行う場合

				// メッセージ１まで追加した文字列のバイト数をチェック
				int byteCount = Encoding.GetEncoding("Shift_JIS").GetByteCount(messageResult.ToString());

				// バイト数に応じたスペーサを追加
				if (isDatetime)
				{
					if (byteCount <= 55)
					{
						for (int i = 0; i < 55 - byteCount; i++) messageResult.Append(" ");
					}
					else if (byteCount <= 65)
					{
						for (int i = 0; i < 65 - byteCount; i++) messageResult.Append(" ");
					}
					else if (byteCount <= 75)
					{
						for (int i = 0; i < 75 - byteCount; i++) messageResult.Append(" ");
					}
				}
				else
				{
					if (byteCount <= 35)
					{
						for (int i = 0; i < 35 - byteCount; i++) messageResult.Append(" ");
					}
					else if (byteCount <= 45)
					{
						for (int i = 0; i < 45 - byteCount; i++) messageResult.Append(" ");
					}
					else if (byteCount <= 55)
					{
						for (int i = 0; i < 55 - byteCount; i++) messageResult.Append(" ");
					}
				}

				messageResult.Append(" : ");					// 区切り文字を追加
				messageResult.Append(message2);					// メッセージ２を追加
			}

			return messageResult.ToString();
		}

		#endregion


		#region エラー関連メソッド群
		
		/// <summary>
		/// 発生したエラーの情報をエラーログに書き込む。
		/// </summary>
		/// <param name="e">発生した例外。</param>
		/// <returns>書き込みに成功した場合は true、それ以外は false。</returns>
		public static bool CreateErrorLogFile(Exception e)
		{
			try
			{
				// フォルダを生成し、エラー用ログファイルに書き込み
				Directory.CreateDirectory(Configuration.ConfigurationData.UserDataFolder);
				File.AppendAllText(
					Path.Combine(Configuration.ConfigurationData.UserDataFolder, Settings.ResourceNames.ErrorLogFile),
					Properties.Resources.ErrorMsg_Log_ThreadException.Replace("%str1%", DateTime.Now.ToString()).Replace("%str2%", e.ToString())
				);
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		#endregion


		#region その他

		/// <summary>
		/// 与えられたシリアル化可能なオブジェクトのディープコピーを作成する。
		/// </summary>
		/// <param name="source">コピー元のシリアル化可能なオブジェクト。</param>
		/// <returns>作成されたオブジェクト。</returns>
		public static object DeepCopy(object source)
		{
			object target = null;

			using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
			{
				var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				formatter.Serialize(stream, source);
				stream.Position = 0;
				target = formatter.Deserialize(stream);
			}
			
			return target;
		}

		#endregion

	}
}
