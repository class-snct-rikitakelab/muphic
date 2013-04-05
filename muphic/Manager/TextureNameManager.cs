using System;
using System.Collections.Generic;

namespace Muphic.Manager
{
	/// <summary>
	/// テクスチャ名管理クラス (シングルトン・継承不可) 
	/// <para>各部構成パーツのテクスチャ名を管理する (muphic ver.7 以前は FileNameManager)。</para>
	/// </summary>
	public sealed class TextureNameManager : Manager
	{
		/// <summary>
		/// テクスチャ名管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static TextureNameManager __instance = new TextureNameManager();

		/// <summary>
		/// テクスチャ名管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static TextureNameManager Instance
		{
			get { return TextureNameManager.__instance; }
		}


		/// <summary>
		/// クラス名とテクスチャ名の配列を関連付けるテーブル
		/// </summary>
		private Dictionary<string, string[]> TextureNameTable { get; set; }


		#region コンストラクタ/初期化

		/// <summary>
		/// テクスチャ名管理クラスの新しインスタンスを初期化する。
		/// </summary>
		private TextureNameManager()
		{
			this.TextureNameTable = new Dictionary<string, string[]>();
		}


		/// <summary>
		/// テクスチャファイル管理クラスの静的インスタンスの初期化を行う。
		/// メイン画面生成時にこのメソッドを呼び出すことで、シングルトンパターンの静的インスタンスを生成する。
		/// </summary>
		private void _Initialize()
		{
			if (this._IsInitialized) return;

			Tools.DebugTools.ConsolOutputMessage("TextureNameManager -Initialize", "テクスチャ名管理クラス生成完了");

			this._IsInitialized = true;
		}

		#endregion


		#region TextureNameTable を直接操作するメソッド群

		/// <summary>
		/// テクスチャ名の登録を行う。
		/// </summary>
		/// <param name="className">ハッシュに登録するキー(クラス名)。</param>
		/// <param name="textureNames">テクスチャ名の配列。</param>
		/// <returns>trueなら登録成功。</returns>
		private bool _Regist(string className, string[] textureNames)
		{
			// 既に登録されている場合は登録できない
			if (TextureNameTable.ContainsKey(className))
			{
				Muphic.Tools.DebugTools.ConsolOutputError("TextureNameManager -RegistTextureNames", "テクスチャ名登録失敗 (キー: " + className + " は既に登録されている) ");
				return false;
			}
			
			for (int i = 0; i < textureNames.Length; i++)
			{
				// テクスチャ名ひとつひとつについて、TextureFileTableに登録されているかをチェック
				if(!Muphic.Manager.TextureFileManager.Exist(textureNames[i]))
				{
					// 指定されたテクスチャ名のテクスチャが存在しない場合、その旨をコンソールに出力して登録中止
					Muphic.Tools.DebugTools.ConsolOutputError("TextureNameManager -RegistTextureNames", "テクスチャ名登録失敗 (テクスチャ名: " + textureNames[i] + " は登録されていない) ");
					return false;
				}
			}

			// クラス名をキーにしてテクスチャ名配列を登録
			TextureNameTable.Add(className, textureNames);
			
			return true;
		}


		/// <summary>
		/// 与えられたキーに該当するテクスチャ名の削除する。
		/// </summary>
		/// <param name="className">削除するキー(クラス名)。</param>
		private void _Delete(string className)
		{
			// 削除 結果は返さなくていいよね…？
			TextureNameTable.Remove(className);
		}


		/// <summary>
		/// 与えられたキーに該当するテクスチャ名を得る。
		/// </summary>
		/// <param name="className">欲しいテクスチャ名のキー(クラス名)。</param>
		/// <returns>テクスチャ名の配列(キーが存在しない場合は空)。</returns>
		private String[] _Get(string className)
		{
			// キーの存在チェック
			if(!TextureNameTable.ContainsKey(className))
			{
				// 存在しない場合は空の文字列を返す
				return new string[1] { "" };
			}

			// テクスチャ名を返す
			return TextureNameTable[className];
		}


		/// <summary>
		/// キーが存在するかチェックする。
		/// </summary>
		/// <param name="className">チェックするキー(クラス名)。</param>
		/// <returns>true なら存在している。</returns>
		private bool _Exist(string className)
		{
			return TextureNameTable.ContainsKey(className);
		}

		#endregion


		#region 外部から呼び出されるメソッド群

		/// <summary>
		/// テクスチャ名管理クラスの静的インスタンスを生成する。
		/// シングルトンパターンとして一度生成された後はこのメソッドは意味を為さないので注意。
		/// </summary>
		public static void Initialize()
		{
			Muphic.Manager.TextureNameManager.Instance._Initialize();
		}


		/// <summary>
		/// テクスチャ名の登録を行う。(テクスチャ1枚)
		/// </summary>
		/// <param name="className">ハッシュに登録するキー(クラス名)。</param>
		/// <param name="textureName">登録するテクスチャ名。</param>
		/// <returns>登録に成功したら true、それ以外は false。</returns>
		public static bool Regist(string className, string textureName)
		{
			return Muphic.Manager.TextureNameManager.Instance._Regist(className, new string[1] { textureName });
		}

		/// <summary>
		/// テクスチャ名の登録を行う。(テクスチャ2枚)
		/// </summary>
		/// <param name="className">ハッシュに登録するキー(クラス名)。</param>
		/// <param name="textureName1">登録するテクスチャ名1。</param>
		/// <param name="textureName2">登録するテクスチャ名2。</param>
		/// <returns>登録に成功したら true、それ以外は false。</returns>
		public static bool Regist(string className, string textureName1, string textureName2)
		{
			return Muphic.Manager.TextureNameManager.Instance._Regist(className, new string[2] { textureName1, textureName2 });
		}

		/// <summary>
		/// テクスチャ名の登録を行う。(テクスチャn枚)
		/// </summary>
		/// <param name="className">ハッシュに登録するキー(クラス名)。</param>
		/// <param name="textureNames">登録するテクスチャ名の配列。</param>
		/// <returns>登録に成功したら true、それ以外は false。</returns>
		public static bool Regist(string className, params string[] textureNames)
		{
			return Muphic.Manager.TextureNameManager.Instance._Regist(className, textureNames);
		}
		

		/// <summary>
		/// 指定したキーをハッシュから削除する。
		/// </summary>
		/// <param name="className">削除するキー(クラス名)。</param>
		public static void Delete(string className)
		{
			Muphic.Manager.TextureNameManager.Instance._Delete(className);
		}


		/// <summary>
		/// 指定したキーのテクスチャ名を得る。(state指定 1枚のみ)
		/// </summary>
		/// <param name="className">キー(クラス名)。</param>
		/// <param name="state">テクスチャ state。</param>
		/// <returns>テクスチャ名。</returns>
		public static string Get(string className, int state)
		{
			return Muphic.Manager.TextureNameManager.Instance._Get(className)[state];
		}


		/// <summary>
		/// 指定したキーのテクスチャ名を得る。(全て)
		/// </summary>
		/// <param name="className">キー(クラス名)。</param>
		/// <returns>テクスチャ名の配列。</returns>
		public static string[] GetAll(string className)
		{
			return Muphic.Manager.TextureNameManager.Instance._Get(className);
		}


		/// <summary>
		/// 指定したキーがハッシュ内に存在するかチェックする。
		/// </summary>
		/// <param name="className">キー(クラス名)。</param>
		/// <returns>存在する場合は true、それ以外は false。</returns>
		public static bool Exist(string className)
		{
			return Muphic.Manager.TextureNameManager.Instance._Exist(className);
		}

		#endregion
	}
}
