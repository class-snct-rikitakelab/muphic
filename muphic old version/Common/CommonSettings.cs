using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

namespace muphic.Common
{
	class CommonSettings
	{
		// ウィンドウモード/フルスクリーンモード
		private static bool isWindow = true;
		public static bool getIsWindow() { return CommonSettings.isWindow; }
		
		// 印刷機能使用の可否
		private static bool EnablePrint = true;
		public static bool getEnablePrint() { return CommonSettings.EnablePrint; }
		
		// 出前授業等で児童や生徒が使用するモード
		private static bool ClientMode = false;
		public static bool getClientMode() { return CommonSettings.ClientMode; }

		// ひとりで音楽モード使用の可否
		private static bool EnableOneScreen = true;
		public static bool getEnableOneScreen() { return CommonSettings.EnableOneScreen; }

		// つなげて音楽モード使用の可否
		private static bool EnableLinkScreen = true;
		public static bool getEnableLinkScreen() { return CommonSettings.EnableLinkScreen; }

		// つなげて音楽モード使用の可否
		private static bool EnableStoryScreen = true;
		public static bool getEnableStoryScreen() { return CommonSettings.EnableStoryScreen; }
		
		// チュートリアル使用の可否
		private static bool EnableTutorial = true;
		public static bool getEnableTutotial() { return CommonSettings.EnableTutorial; }
		
		// 録音によるボイス使用の可否
		private static bool EnableVoice = true;
		public static bool getEnableVoice() { return CommonSettings.EnableVoice; }
		
		

		/// <summary>
		/// 設定ファイルを読み込むメソッド
		/// 主に起動時に使用
		/// </summary>
		public static void ReadCommonSettings()
		{
			string str;
			string Filename = "muphic.ini";		// 設定ファイル名指定
			StreamReader reader;				// 読み込みバッファ

			try
			{
				// 読み込みバッファを設定
				Console.WriteLine("muphic設定ファイル\"" + Filename + "\"読み込み");
				reader = new StreamReader(Filename, Encoding.GetEncoding("Shift_JIS"));
			}
			catch (FileNotFoundException)
			{
				System.Windows.Forms.MessageBox.Show("設定ファイル\"" + Filename + "\"が見つかりません.");
				System.Console.WriteLine("### 設定ファイル\"" + Filename + "\"が見つかりません ###");
				return;
			}

			// 行末まで1行ごと読み込み
			while ((str = reader.ReadLine()) != null)
			{
				string[] temp;
				
				// 先ずは、コメント&タブの削除
				str = TutorialTools.RemoveStr(str, "/");
				str = TutorialTools.RemoveStr(str, "\t");
				
				// 次に、本文を'='で分割
				temp = str.Split( new char[] {'='} );

				try
				{

					// 読み込んだ文字列が何の値かによって処理が変わる
					switch (temp[0])
					{
						case "isWindow":
							// 言わずもがな
							CommonSettings.isWindow = bool.Parse(temp[1]);
							break;
							
						case "EnablePrint":
							// 印刷機能使用の可否
							CommonSettings.EnablePrint = bool.Parse(temp[1]);
							break;

						case "ClientMode":
							// 出前授業等で児童や生徒が使用する際に指定
							CommonSettings.ClientMode = bool.Parse(temp[1]);
							break;

						case "EnableOneScreen":
							// ひとりで音楽モードを使用させないならfalseを指定
							CommonSettings.EnableOneScreen = bool.Parse(temp[1]);
							break;

						case "EnableLinkScreen":
							// つなげて音楽モードを使用させないならfalseを指定
							CommonSettings.EnableLinkScreen = bool.Parse(temp[1]);
							break;

						case "EnableStoryScreen":
							// ものがたり音楽モードを使用させないならfalseを指定
							CommonSettings.EnableStoryScreen = bool.Parse(temp[1]);
							break;

						case "EnableTutorial":
							// チュートリアルを使用させないならfalseを指定
							CommonSettings.EnableTutorial = bool.Parse(temp[1]);
							break;

						case "EnableVoice":
							// 録音や音声を使用させないならfalseを指定
							CommonSettings.EnableVoice = bool.Parse(temp[1]);
							break;

						default:
							break;
					}
				}
				catch (System.FormatException)
				{
					System.Windows.Forms.MessageBox.Show("設定ファイル\"" + Filename + "\"の構文に間違いがあります.");
					System.Console.WriteLine("### 設定ファイル\"" + Filename + "\"の構文に間違いがあります ###");
				}
			}

			reader.Close();

			return;
		}
	}
}
