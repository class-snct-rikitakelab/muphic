using System;
using System.Collections.Generic;
using System.Drawing;

namespace Muphic.Manager
{
	/// <summary>
	/// テクスチャファイル管理クラス (シングルトン・継承不可) 
	/// <para>統合画像ファイル内の各テクスチャ名とその位置を含めた矩形を管理する。</para>
	/// </summary>
	public sealed class TextureFileManager : Manager
	{
		/// <summary>
		/// テクスチャファイル管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static TextureFileManager __instance = new TextureFileManager();

		/// <summary>
		/// テクスチャファイル管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static TextureFileManager Instance
		{
			get { return TextureFileManager.__instance; }
		}


		/// <summary>
		/// テクスチャ名とソーステクスチャファイル名を関連付けたテーブル
		/// </summary>
		private Dictionary<string, string> TextureFileTable { get; set; }

		/// <summary>
		/// テクスチャ名とソーステクスチャファイル内での座標・サイズを関連付けたテーブル
		/// </summary>
		private Dictionary<string, Rectangle> TextureRectangleTable { get; set; }


		#region コンストラクタ/初期化

		/// <summary>
		/// テクスチャファイル管理クラスの新しインスタンスの初期化する。
		/// </summary>
		private TextureFileManager()
		{
			this.TextureFileTable = new Dictionary<string, string>();
			this.TextureRectangleTable = new Dictionary<string, Rectangle>();
		}


		/// <summary>
		/// テクスチャファイル管理クラスの静的インスタンスの初期化を行う。
		/// メイン画面生成時にこのメソッドを呼び出すことで、シングルトンパターンの静的インスタンスを生成する。
		/// </summary>
		private void _Initialize()
		{
			if (this._IsInitialized) return;

			TextureFileManager.LoadTextureNameFile(Settings.ResourceNames.TextureNameListFile);
			Tools.DebugTools.ConsolOutputMessage("TextureFileManager -Initialize", "テクスチャファイル管理クラス生成完了");

			this._IsInitialized = true;
		}

		#endregion


		#region TextureFileTable と TextureRectangleTable を直接操作するメソッド群

		/// <summary>
		/// ソーステクスチャファイル名と座標・サイズを登録する。
		/// </summary>
		/// <param name="textureName">キー(テクスチャ名)。</param>
		/// <param name="fileName">テクスチャが入っているソーステクスチャファイル名。</param>
		/// <param name="textureRectangle">ソーステクスチャファイル内での位置とサイズ。</param>
		/// <returns>登録に成功した場合は true、それ以外は false。</returns>
		private bool _Regist(string textureName, string fileName, Rectangle textureRectangle)
		{
			// 既にテクスチャ名が登録されているなら登録しない
			if (TextureFileTable.ContainsKey(textureName))
			{
				throw new Exception("キー: " + textureName + " は既に登録されている");
			}

			// ソーステクスチャファイル名と座標・サイズをそれぞれ登録
			TextureFileTable.Add(textureName, fileName);
			TextureRectangleTable.Add(textureName, textureRectangle);

			return true;
		}


		/// <summary>
		/// 与えられたキーに該当するソーステクスチャファイル名と座標・サイズを削除する。
		/// </summary>
		/// <param name="textureName">キー(テクスチャ名)。</param>
		private void _Delete(string textureName)
		{
			TextureFileTable.Remove(textureName);
			TextureRectangleTable.Remove(textureName);
		}


		/// <summary>
		/// 与えられたキーに該当するソーステクスチャファイルパスを返す。
		/// </summary>
		/// <param name="textureName">キー(テクスチャ名)。</param>
		/// <returns>ソーステクスチャファイルパス。</returns>
		private string _GetFilePath(string textureName)
		{
			// キーの存在チェック
			if (!TextureFileTable.ContainsKey(textureName))
			{
				throw new Exception("ソーステクスチャファイルパス参照失敗 (キー: " + textureName + " は未登録) ");
			}

			// ソーステクスチャファイルパスを返す
			return TextureFileTable[textureName];
		}


		/// <summary>
		/// 与えられたキーに該当するソーステクスチャファイル内での座標・サイズを返す。
		/// </summary>
		/// <param name="textureName">キー(テクスチャ名)。</param>
		/// <returns>ソーステクスチャファイル内での座標・サイズ。</returns>
		private Rectangle _GetRectangle(string textureName)
		{
			// キーの存在チェック
			if (!TextureRectangleTable.ContainsKey(textureName))
			{
				throw new Exception("ソーステクスチャ座標及びサイズの参照失敗 (キー: " + textureName + " は未登録) ");
			}

			// ソーステクスチャ内での座標とサイズを返す
			return TextureRectangleTable[textureName];
		}


		/// <summary>
		/// 与えられたキーのテクスチャが存在するかチェックする。
		/// </summary>
		/// <param name="textureName">キー(テクスチャ名)。</param>
		/// <returns>存在する場合は true、それ以外は false。</returns>
		private bool _Exist(string textureName)
		{
			return TextureFileTable.ContainsKey(textureName) && TextureRectangleTable.ContainsKey(textureName);
		}

		#endregion

	
		#region テクスチャ一覧ファイル関連メソッド群

		/// <summary>
		/// テクスチャ一覧ファイルを読み込み、テーブルに追加する。
		/// </summary>
		/// <param name="textureListFileName">テクスチャファイル名。</param>
		/// <returns>読み込みに成功した場合は true、それ以外は false。</returns>
		private bool _LoadFile(string textureListFileName)
		{
			return this._LoadFile(ArchiveFileManager.GetData(textureListFileName));
		}

		/// <summary>
		/// テクスチャ名リスト ファイルを読み込み、テーブルに追加する。
		/// </summary>
		/// <param name="textureNameListData">テクスチャ名リスト ファイルのバイト配列。</param>
		/// <returns>読み込みに成功した場合は true、それ以外は false。</returns>
		private bool _LoadFile(byte[] textureNameListData)
		{
			// バイト配列から XML 逆シリアル化を行い、テクスチャ名リストを取得
			TextureFileInfo textureFileInfo = Tools.IO.XmlFileReader.ReadSaveData<TextureFileInfo>(textureNameListData, true);

			// テクスチャ名リストが default(TextureFileInfo) だった場合、読み込み失敗と判断
			if (textureFileInfo == new TextureFileInfo()) return false;

			// 取得したテクスチャ名リストから、この管理クラスが使用する
			for (int i = 0; i < textureFileInfo.TextureName.Count; i++)
			{
				this._Regist(textureFileInfo.TextureName[i], textureFileInfo.SourceFileName[i], textureFileInfo.SourceRectangle[i]);
			}

			return true;
		}


		/// <summary>
		/// 与えられたテクスチャ一覧ファイルにあるテクスチャ名とテクスチャ位置・サイズをテーブルから削除する。
		/// </summary>
		/// <param name="fileName">テクスチャ一覧ファイルパス。</param>
		/// <returns>削除に成功した場合はtrue。</returns>
		private bool _UnloadFile(string fileName)
		{
			return false;
		}

		#endregion


		#region テクスチャ一覧ファイル関連メソッド群 (旧 *.txt 方式)

		///// <summary>
		///// テクスチャ一覧ファイルを読み込み、テーブルに追加する。
		///// </summary>
		///// <param name="fileName">テクスチャファイルパス。</param>
		///// <returns>読み込みに成功したらtrue。</returns>
		//private bool _LoadFile(string fileName)
		//{
		//    // テクスチャファイルパスの拡張子を変更し、テクスチャ一覧ファイルのパスを得る
		//    string txtfileName = Path.ChangeExtension(fileName, "txt");

		//    // テクスチャ一覧ファイルが存在するかをチェックし、存在しなければfalseを返す
		//    if (!ArchiveFileManager.Exists(txtfileName))
		//    {
		//        LogFileManager.WriteLineError("TextureFileManager -LoadFile", "テクスチャ読み込み失敗 (テクスチャ一覧ファイル\"" + txtfileName + "\"が見つからない) ");
		//        return false;
		//    }

		//    string str;						// 作業用文字列
		//    var r = new Rectangle();		// 作業用四角形領域 (前の行のテクスチャの位置・サイズを記憶するため) 
		//    StreamReader reader;			// 読み込みバッファ
		//    int line_num = 0;				// なんとなく行数をカウントしてみる

		//    try															// 読み込みバッファを設定
		//    {
		//        reader = new StreamReader(new MemoryStream(ArchiveFileManager.GetData(txtfileName)), System.Text.Encoding.GetEncoding("Shift_JIS"));
		//    }
		//    catch (Exception exception)									// 読み込みに失敗したらfalseを返す
		//    {
		//        LogFileManager.WriteLineError("TextureFileManager -LoadFile", exception.ToString());
		//        return false;
		//    }

		//    while ((str = reader.ReadLine()) != null)					// 行末まで1行ごと読み込み
		//    {
		//        string[] temp;
		//        string[] temp_r;
		//        line_num++;

		//        str = Muphic.Tools.CommonTools.RemoveStr(str, "//");	// 先ずは、コメント部分の削除

		//        temp = str.Split(new char[] { '\t' }, System.StringSplitOptions.RemoveEmptyEntries);		// 文字列をテクスチャ名部とRectangle部に分割

		//        if (temp.Length == 0) continue;							// 分割して得た文字列の数が0だった場合は空行と判断し読み飛ばす

		//        if (temp.Length != 2)									// 分割して得た文字列の数が2つでなかった場合は不正なフォーマットとして弾く
		//        {
		//            LogFileManager.WriteLineError(
		//                "TextureFileManager -LoadFile",
		//                "不正なフォーマット (テクスチャ一覧ファイル\"" + txtfileName + "\" " + (line_num + 1) + "行目) によりテクスチャ名登録失敗 読み飛ばして続行"
		//            );
		//            continue;
		//        }

		//        temp_r = temp[1].Split(new char[] { '\t', ' ', ',' }, System.StringSplitOptions.RemoveEmptyEntries);	// Rectangle部の文字列をカンマで分割

		//        if (temp_r.Length != 4 && temp_r.Length != 2)			// 分割したRectangleの文字列の数が4か2でなかった場合は不正なフォーマットとして弾く
		//        {
		//            LogFileManager.WriteLineError(
		//                "TextureFileManager -LoadFile",
		//                "不正なフォーマット (テクスチャ一覧ファイル\"" + txtfileName + "\" " + (line_num + 1) + "行目) によりテクスチャ名登録失敗 読み飛ばして続行"
		//            );
		//            continue;
		//        }

		//        if (temp_r[0].IndexOf('+') == 0) r.X += int.Parse(temp_r[0].Substring(1));			// RectangleのXの先頭文字が'+'だった場合、前の行のXに足し合わせる
		//        else if (temp_r[0].IndexOf('-') == 0) r.Y -= int.Parse(temp_r[0].Substring(1));		// RectangleのXの先頭文字が'-'だった場合、前の行のXから引く
		//        else r.X = int.Parse(temp_r[0]);													// それ以外であれば、Xにそのまま値を入れる

		//        if (temp_r[1].IndexOf('+') == 0) r.Y += int.Parse(temp_r[1].Substring(1));			// RectangleのYの先頭文字が'+'だった場合、前の行のYに足し合わせる
		//        else if (temp_r[1].IndexOf('-') == 0) r.Y -= int.Parse(temp_r[1].Substring(1));		// RectangleのYの先頭文字が'-'だった場合、前の行のYから引く
		//        else r.Y = int.Parse(temp_r[1]);													// それ以外であれば、Yにそのまま値を入れる

		//        if (temp_r.Length == 4)									// Rectangleの文字列数が4だった場合は横幅と縦幅も設定する
		//        {														// Rectangleの文字列数が2だった場合は前の行の横幅と縦幅をそのまま使用
		//            r.Width = int.Parse(temp_r[2]);						// 横幅設定
		//            r.Height = int.Parse(temp_r[3]);					// 縦幅設定
		//        }

		//        TextureFileManager.Regist(temp[0], fileName, r);			// テクスチャ名とその位置・サイズを登録
		//    }

		//    return true;
		//}


		///// <summary>
		///// 与えられたテクスチャ一覧ファイルにあるテクスチャ名とテクスチャ位置・サイズをテーブルから削除する。
		///// </summary>
		///// <param name="fileName">テクスチャ一覧ファイルパス。</param>
		///// <returns>削除に成功した場合はtrue。</returns>
		//private bool _UnloadFile(string fileName)
		//{
		//    // テクスチャファイルパスの拡張子を変更し、テクスチャ一覧ファイルのパスを得る
		//    string txtfileName = Path.ChangeExtension(fileName, "txt");

		//    // テクスチャ一覧ファイルが存在するかをチェックし、存在しなければfalseを返す
		//    if (!ArchiveFileManager.Exists(txtfileName))
		//    {
		//        LogFileManager.WriteLineError("TextureFileManager -UnloadFile", "テクスチャ削除失敗 (テクスチャ一覧ファイル\"" + fileName + "\"が見つからない) ");
		//        return false; ;
		//    }

		//    string str;						// 作業用文字列
		//    StreamReader reader;			// 読み込みバッファ
		//    int line_num = 0;				// なんとなく行数をカウントしてみる

		//    try															// 読み込みバッファを設定
		//    {
		//        reader = new StreamReader(new MemoryStream(ArchiveFileManager.GetData(txtfileName)), System.Text.Encoding.GetEncoding("Shift_JIS"));
		//    }
		//    catch (Exception exception)									// 読み込みに失敗したらfalseを返す
		//    {
		//        LogFileManager.WriteLineError("TextureFileManager -UnloadFile", exception.ToString());
		//        return false;
		//    }

		//    while ((str = reader.ReadLine()) != null)					// 行末まで1行ごと読み込み
		//    {
		//        string[] temp;
		//        line_num++;

		//        str = Muphic.Tools.CommonTools.RemoveStr(str, "//");	// 先ずは、コメント部分の削除

		//        temp = str.Split(new char[] { '\t' }, System.StringSplitOptions.RemoveEmptyEntries);		// 文字列をテクスチャ名部とRectangle部に分割

		//        if (temp.Length == 0) continue;							// 分割して得た文字列の数が0だった場合は空行と判断し読み飛ばす

		//        if (temp.Length != 2)									// 分割して得た文字列の数が2つでなかった場合は不正なフォーマットとして弾く
		//        {
		//            LogFileManager.WriteLineError(
		//                "TextureFileManager -UnloadFile",
		//                "不正なフォーマット (テクスチャ一覧ファイル\"" + txtfileName + "\" " + (line_num + 1) + "行目) によりテクスチャ名削除失敗 読み飛ばして続行"
		//            );
		//            continue;
		//        }

		//        TextureFileManager.Delete(temp[0]);						// テクスチャ名から削除
		//    }

		//    return true;
		//}

		#endregion


		#region 外部から呼び出されるメソッド群

		/// <summary>
		/// テクスチャファイル管理クラスの静的インスタンスを生成する。
		/// シングルトンパターンとして一度生成された後はこのメソッドは意味を為さないので注意。
		/// </summary>
		public static void Initialize()
		{
			Muphic.Manager.TextureFileManager.Instance._Initialize();
		}


		/// <summary>
		/// テクスチャ名とソーステクスチャ内での位置・サイズを登録する。
		/// </summary>
		/// <param name="textureName">ハッシュに登録するキー(テクスチャ名)。</param>
		/// <param name="fileName">ソーステクスチャのファイルパス。</param>
		/// <param name="textureRectangle">ソーステクスチャ内での位置・サイズ。</param>
		/// <returns>登録に成功した場合は true、それ以外は false。</returns>
		public static bool Regist(string textureName, string fileName, Rectangle textureRectangle)
		{
			return Muphic.Manager.TextureFileManager.Instance._Regist(textureName, fileName, textureRectangle);
		}


		/// <summary>
		/// 指定したキーをハッシュから削除する。
		/// </summary>
		/// <param name="textureName">削除するキー(テクスチャ名)。</param>
		public static void Delete(string textureName)
		{
			Muphic.Manager.TextureFileManager.Instance._Delete(textureName);
		}


		/// <summary>
		/// 指定したキーに該当するソーステクスチャファイルパスを取得する。
		/// </summary>
		/// <param name="textureName">キー(テクスチャ名)。</param>
		/// <returns>ソーステクスチャファイルパス。</returns>
		public static string GetFilePath(string textureName)
		{
			return Muphic.Manager.TextureFileManager.Instance._GetFilePath(textureName);
		}


		/// <summary>
		/// 指定したキーに該当するソーステクスチャファイル内での位置・サイズを取得する。
		/// </summary>
		/// <param name="textureName">キー(テクスチャ名)。</param>
		/// <returns>ソーステクスチャファイル内での位置・サイズ。</returns>
		public static Rectangle GetRectangle(string textureName)
		{
			return Muphic.Manager.TextureFileManager.Instance._GetRectangle(textureName);
		}


		/// <summary>
		/// 指定したキーがハッシュ内に存在するかチェックする。
		/// </summary>
		/// <param name="textureName">キー(テクスチャ名)。</param>
		/// <returns>存在する場合は true、それ以外は false。</returns>
		public static bool Exist(string textureName)
		{
			return Muphic.Manager.TextureFileManager.Instance._Exist(textureName);
		}


		/// <summary>
		/// 与えられたパスのテクスチャのテクスチャ一覧ファイルを読み込み、各テクスチャ名とテクスチャ位置・サイズをテーブルに登録する。
		/// </summary>
		/// <param name="fileName">読み込むテクスチャのパス。</param>
		/// <returns>読み込みに成功した場合は true、それ以外は false。</returns>
		public static bool LoadTextureNameFile(string fileName)
		{
			return Muphic.Manager.TextureFileManager.Instance._LoadFile(fileName);
		}


		/// <summary>
		/// 与えられたパスのテクスチャのテクスチャ一覧ファイルにある各テクスチャ名とテクスチャ位置・サイズをテーブルから削除する。
		/// </summary>
		/// <param name="fileName">削除するテクスチャのパス。</param>
		/// <returns>削除に成功したら true、それ以外は false。</returns>
		public static bool UnloadTextureNameFile(string fileName)
		{
			return Muphic.Manager.TextureFileManager.Instance._UnloadFile(fileName);
		}

		#endregion

	}



	/// <summary>
	/// テクスチャ名とそのテクスチャが含まれるソーステクスチャファイルに関する情報を保持するクラス。
	/// テクスチャ一覧ファイルを構成する、テクスチャ名やソーステクスチャファイルのリストを持つ。
	/// </summary>
	[Serializable]
	public class TextureFileInfo
	{
		/// <summary>
		/// テクスチャ名のリスト。
		/// </summary>
		public List<string> TextureName { get; set; }

		/// <summary>
		/// テクスチャ名のリストと対応した、テクスチャが含まれるソーステクスチャファイル (*.png) 名のリスト。
		/// </summary>
		public List<string> SourceFileName { get; set; }

		/// <summary>
		/// テクスチャ名のリストと対応した、テクスチャが含まれるソーステクスチャファイル内での位置とサイズのリスト。
		/// </summary>
		public List<Rectangle> SourceRectangle { get; set; }


		/// <summary>
		/// TextureFileInfo クラスの新しいインスタンスを初期化する。
		/// </summary>
		public TextureFileInfo()
		{
			this.TextureName = new List<string>();
			this.SourceFileName = new List<string>();
			this.SourceRectangle = new List<Rectangle>();
		}


		/// <summary>
		/// テクスチャの情報を追加する。
		/// </summary>
		/// <param name="textureName">テクスチャ名。</param>
		/// <param name="sourceFileName">テクスチャが含まれるソーステクスチャファイル名。</param>
		/// <param name="sourceRectangle">テクスチャのソーステクスチャファイル内での位置とサイズ。</param>
		public void Add(string textureName, string sourceFileName, Rectangle sourceRectangle)
		{
			this.TextureName.Add(textureName);
			this.SourceFileName.Add(sourceFileName);
			this.SourceRectangle.Add(sourceRectangle);
		}
	
		
		/// <summary>
		/// テクスチャの情報をリスト内の指定した位置に追加する。
		/// </summary>
		/// <param name="index">追加するリスト内の位置。</param>
		/// <param name="textureName">テクスチャ名。</param>
		/// <param name="sourceFileName">テクスチャが含まれるソーステクスチャファイル名。</param>
		/// <param name="sourceRectangle">テクスチャのソーステクスチャファイル内での位置とサイズ。</param>
		public void Add(int index, string textureName, string sourceFileName, Rectangle sourceRectangle)
		{
			this.TextureName.Insert(index, textureName);
			this.SourceFileName.Insert(index, sourceFileName);
			this.SourceRectangle.Insert(index, sourceRectangle);
		}
	}

}
