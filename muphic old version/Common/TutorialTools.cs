using System;
using System.IO;
using System.Text;
using System.Drawing;
using muphic.Common;
using muphic.ADV;

namespace muphic.Common
{
	/// <summary>
	/// TutorialTools の概要の説明です。
	/// </summary>
	public class TutorialTools
	{
		public TutorialTools()
		{
		}
		
		/// <summary>
		/// パターンファイルを読み込むメソッド
		/// 主にADVパートかチュートリアルで使用
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static PatternData ReadPatternFile(string filename)
		{
			string str;
			PatternData data = new PatternData();
			
			// 読み込みバッファ設定 ファイル名はパターンファイル一覧から取得
			Console.WriteLine("パターンファイル: " + filename + "読み込み");
			StreamReader reader = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
			
			// 行末まで1行ごと読み込み
			while( (str = reader.ReadLine()) != null )
			{
				string[] temp;
				
				// 先ずは、コメント&タブの削除
				str = TutorialTools.RemoveStr(str, "/");
				str = TutorialTools.RemoveStr(str, "\t");
				
				// 次に、パターン本文を'='で分割
				temp = str.Split( new char[] {'='} );
				
				// 読み込んだ文字列が何の値かによって処理が変わる
				switch(temp[0])
				{
					case "NowLoading":
						// NowLoading画面を呼び出すor消す
						int registnum = int.Parse(temp[1]);
						// 値が1以上ならその数を引数にしてNowLoadingを呼び出す
						// それ以外ならNowLoading終了とする
						if(registnum > 0) DrawManager.BeginRegist(registnum);
						else DrawManager.EndRegist();
						break;
						
					case "Regist":
					case "NextButtonRegist":
						// 登録する画像ファイル名 登録名とパス付きファイル名を得る
						// まずCVS形式で登録名、パス付きファイル名を切り離して配列に格納
						string[] registdata = temp[1].Split(new char[] {','});
						// ただし、CVS形式でファイル名と登録名がなければダメ
						if(registdata.Length < 2) break;
						// 登録する画像ファイル名をfilenames配列に格納
						string[] filenames = new string[registdata.Length-1];
						for(int i=0; i<filenames.Length; i++) filenames[i] = registdata[1+i];
						if(temp[0] == "Regist")
							// 画像を登録 filenames配列を丸々登録ー 座標は適当
							DrawManager.Regist(registdata[0], 0, 0, filenames);
						else
							// メッセージウィンドウの遅延描画後、次衛ボタン表示の直前に画像を読込むよう設定
							data.addNBRegist(registdata[0], 0, 0, filenames);
						break;
						
					case "Image":
						// 画像ファイルの設定 同時に座標の設定も行う
						// まずCVS形式で登録画像ファイル、座標を切り離して配列に格納
						string[] image = temp[1].Split(new char[] {','});
						// ただし、CVS形式で画像名と座標がなければダメ
						if(image.Length != 3) break;
						data.addUseImage(image[0], int.Parse(image[1]), int.Parse(image[2]));
						break;
						
					case "MsgWindow":
						// メッセージウィンドウの表示設定
						data.Window = int.Parse(temp[1]);
						break;

					case "Text":
						// シナリオ本文の設定
						data.addText(temp[1]);
						break;
						
					case "MsgAssistant":
						// メッセージウィンドウのアシスタント動物の選択
						data.assistant = temp[1];
						break;
						
					case "NextButton":
						// メッセージウィンドウの次へボタンを描画するか否か
						if(temp[1] == "false") data.NextButton = false;
						break;

					case "Trigger":
						// トリガーリストへ追加
						TutorialStatus.addTriggerPartsList(temp[1]);
						break;

					case "Permission":
						// クリック許可リストへ追加
						TutorialStatus.addPermissionPartsList(temp[1]);
						break;
						
					case "StandBy":
						// trueだったら動作待機状態にする
						if(temp[1] == "true") TutorialStatus.setEnableNextStateStandBy();
						break;
						
					case "NextState":
						// trueだったらそのまま次のステートへ進ませる
						if(temp[1] == "true") data.NextState = true;
						break;
						
					case "ClickRectangle":
						// クリックを座標で制限する場合
						string[] rect = temp[1].Split(new char[] {','});
						data.rect = new Rectangle(int.Parse(rect[0]), int.Parse(rect[1]), int.Parse(rect[2]), int.Parse(rect[3]));
						break;
						
					case "MouseClick":
						// 座標を指定しクリックさせたい場合
						string[] p = temp[1].Split(new char[] {','});
						data.MouseClick = new Point(int.Parse(p[0]), int.Parse(p[1]));
						break;
						
					case "ChapterTop":
						// 各チャプターのトップ画面を描画する
						data.ChapterTop = temp[1];
						break;
						
					case "BackTopScreen":
						// trueだったらトップスクリーンに戻る
						if(temp[1] == "true") data.TopScreen = true;
						break;
						
					case "SPMode":
						// 所謂特殊コマンド使用時に
						data.SPMode = temp[1];
						break;
						
					case "DrawString":
						// チュートリアル管理側以外の文字列の描画の設定
						if( temp[1] == "false" ) data.DrawString = false;
						break;
						
					case "Fade":
						// フェード効果を付加する場合
						string[] temp5 = temp[1].Split(new char[] {','});
						if(temp5.Length != 2) break;
						data.Fade.X = int.Parse(temp5[0]);
						data.Fade.Y = int.Parse(temp5[1]);
						break;
						
					case "BGM":
						// BGMファイル名の設定 もしくは、BGMを止める"STOP"など
						data.BGM = temp[1];
						break;
						
					case "SE":
						// 効果音の設定
						data.SE = temp[1];
						break;
						
					case "Voice":
						// Voiceの設定
						data.Voice = temp[1];
						break;
						
					default:
						break;
				}
			}
			
			reader.Close();
			
			return data;
		}
		
		
		/// <summary>
		/// チュートリアルの状態をファイルにセーブするメソッド
		/// </summary>
		/// <param name="savefile">セーブファイル名</param>
		/// <param name="chapter">チャプター番号</param>
		/// <param name="chaptername">チャプター名</param>
		/// <param name="chapternum">全チャプター数</param>
		public static void WriteSaveFile(string savefile, int chapter, string chaptername, int chapternum)
		{
			// 書き込みバッファの設定
			StreamWriter writer = new StreamWriter(savefile, false, Encoding.GetEncoding("Shift_JIS"));
			
			// チャプター番号とチャプター名、全チャプター数の各項目をを書き込む
			writer.WriteLine("Chapter=" + chapter);
			writer.WriteLine("ChapterName=" + chaptername);
			writer.WriteLine("AllChapterNum=" + chapternum);
			System.Console.WriteLine("TutrialSave : " + savefile);
			
			writer.Close();
		}
		
		
		/// <summary>
		/// チュートリアルの状態をファイルから読み込むメソッド
		/// 正しいセーブデータなのかいろいろチェックする予定だったが時間無いんで現時点では最低限の機能のみ持たせる
		///	読み込んだ後は削除できる
		/// </summary>
		/// <param name="savefile">セーブファイル名</param>
		/// <param name="delete">読み込み後に削除するかどうか</param>
		/// <returns>再開するチャプター番号</returns>
		public static int ReadSaveFile(string savefile, bool delete)
		{
			// まず与えられたファイル名の存在チェック
			// ファイルが存在しなかった場合は0を返す
			if( !File.Exists(savefile) ) return 0;
			
			// 読み込みバッファの設定
			System.Console.WriteLine("TutorialSaveRead : " + savefile);
			StreamReader reader = new StreamReader(savefile, Encoding.GetEncoding("Shift_JIS"));
			string str;
			
			// ファイル末尾まで行ごとに読み込み
			while( (str = reader.ReadLine()) != null )
			{
				// チャプター数の項目を見つけたら、チャプター数部分を切り取ってその値を返す
				// 時間無いんで例外云々は今は割愛 時間あったら書きます
				if( str.IndexOf("Chapter=") != -1 )
				{
					reader.Close();
					if(delete) File.Delete(savefile);	// 削除する設定になっていたら
					return int.Parse( str.Split(new char[] {'='})[1] );
				}
			}
			
			// ファイル内にチャプター数っぽいのが書いてなかった場合も0を返す
			reader.Close();
			if(delete) File.Delete(savefile);
			return 0;
		}
		
		
		/// <summary>
		/// 与えられた文字列から指定された文字列を削除するメソッド
		/// </summary>
		/// <param name="str">元の文字列</param>
		/// <param name="remove">削除する文字列</param>
		/// <returns>元の文字列から指定された文字列を削除した文字列</returns>
		public static string RemoveStr(string str, string remove)
		{
			int num = str.IndexOf(remove);
			if(num != -1)
			{
				str = str.Remove(num, str.Length - num);
			}
			return str;
		}
		
		
		/// <summary>
		/// 指定された拡張子のファイル一覧を得るメソッド
		/// </summary>
		/// <param name="directoryname">一覧を得るディレクトリ</param>
		/// <returns>パターンファイル一覧</returns>
		public static string[] getFileNames(string directoryname, string extension)
		{
			// まずファイル一覧を得る
			string[] AllFileNames = TutorialTools.getFileNames(directoryname);
			
			// ファイル一覧の中からパターンファイルだけを抽出
			int num = 0;
			for(int i=0; i<AllFileNames.Length; i++)
			{
				// ファイル一覧の中で".pat"以外のファイルを全て空にし、".pat"のファイルをカウント
				if( Path.GetExtension(AllFileNames[i]) != extension ) AllFileNames[i] = "";
				else num++;
			}

			// パターンファイル一覧用配列を容易 要素数はカウントした".pat"ファイルの数
			string[] FileNames = new string[num];
			num = 0;
			
			for(int i=0; i<AllFileNames.Length; i++)
			{
				if(AllFileNames[i] != "") FileNames[num++] = AllFileNames[i]; 
			}

			return FileNames;
		}
		
		
		/// <summary>
		/// 指定されたディレクトリのファイルの一覧を得るメソッド
		/// </summary>
		/// <param name="directoryname">一覧を得るディレクトリ</param>
		/// <returns>ファイル一覧</returns>
		public static string[] getFileNames(string directoryname)
		{
			return System.IO.Directory.GetFiles(directoryname);
		}
		
		
		
		/// <summary>
		/// 指定されたディレクトリ内の特定の文字列を持つディレクトリ一覧を得るメソッド
		/// indexnameに"ABC"と指定すると、directoryname内の"ABC"という文字列を含むディレクトリ名の一覧を返す
		/// </summary>
		/// <param name="directoryname">一覧を得るディレクトリ</param>
		/// <param name="indexname"></param>
		/// <returns></returns>
		public static string[] getDirectoryNames(string directoryname, string indexname)
		{
			// まずディレクトリ一覧を得る
			string[] AllDirectoryNames = TutorialTools.getDirectoryNames(directoryname);

			// ディレクトリ一覧の中から特定の文字列を持つものだけを抽出
			int num = 0;
			for(int i=0; i<AllDirectoryNames.Length; i++)
			{
				// ディレクトリ一覧の中で特定の文字列を持たないものを空に上書きし、持つディレクトリ名をカウント
				if( AllDirectoryNames[i].IndexOf(indexname) == -1 ) AllDirectoryNames[i] = "";
				else num++;
			}
			
			// ディレクトリ一覧用配列を用意 要素数はカウントしたindexnameを含むディレクトリ名の数
			string[] DirectoryNames = new string[num];
			num = 0;

			for(int i=0; i<AllDirectoryNames.Length; i++)
			{
				if(AllDirectoryNames[i] != "") DirectoryNames[num++] = AllDirectoryNames[i];
			}
			
			return DirectoryNames;
		}
		

		/// <summary>
		/// 指定されたディレクトリ内のディレクトリ一覧を得るメソッド
		/// </summary>
		/// <param name="directoryname">一覧を得るディレクトリ</param>
		/// <returns></returns>
		public static string[] getDirectoryNames(string directoryname)
		{
			return System.IO.Directory.GetDirectories(directoryname);
		}
	}
}
