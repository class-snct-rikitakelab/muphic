using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.DirectX.DirectInput;
using Microsoft.International.Converters;

namespace Muphic.Manager
{
	/// <summary>
	/// 日本語入力管理クラス (シングルトン・継承付加)
	/// <para>キーボードによる日本語入力の為の静的メソッドを提供する。</para>
	/// </summary>
	public class JpnLangInputManager : Manager
	{

		#region フィールドとプロパティたち

		/// <summary>
		/// 日本語入力管理クラスの静的インスタンス (シングルトンパターン)
		/// </summary>
		private static JpnLangInputManager __instance = new JpnLangInputManager();

		/// <summary>
		/// 日本語入力管理クラスの静的インスタンス (シングルトンパターン)
		/// </summary>
		private static JpnLangInputManager Instance
		{
			get { return JpnLangInputManager.__instance; }
		}


		/// <summary>
		/// 入力中で未確定のローマ字を示す文字列を取得または設定する。
		/// </summary>
		private string _NowKeys { get; set; }


		/// <summary>
		/// 入力中で未確定のローマ字を示す文字列を取得する。
		/// </summary>
		public static string NowKeys
		{
			get { return Muphic.Manager.JpnLangInputManager.Instance._NowKeys; }
		}

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// 日本語入力管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private JpnLangInputManager()
		{
			this._NowKeys = "";
		}


		/// <summary>
		/// 日本語入力管理クラスの初期化を行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>正常に初期化が行われた場合は true、それ以外は false。</returns>
		private bool _Initialize()
		{
			if (this._IsInitialized) return false;


			return this._IsInitialized = true;
		}

		#endregion


		#region キー入力

		/// <summary>
		/// 未確定のローマ字にアルファベットを 1 文字入力する。
		/// </summary>
		/// <param name="key">入力するアルファベットを示す文字列。</param>
		/// <param name="isShiftKey">Shift キーが押されている場合は true、それ以外は false。</param>
		/// <returns>この入力によりかなが確定した場合はそのかな、それ以外の場合は空の文字列。</returns>
		private string _KeyInput(Key key, bool isShiftKey)
		{
			if (key == Key.BackSpace)
			{							// EntitleScreen からの呼び出しの場合、ここに到達することは無い
				this._KeyDelete();		// (BackSpaceKey の処理は別途行っているため)
				return "";
			}
			else
			{
				string input = Tools.CommonTools.KeyToAlphabet(key, isShiftKey);	// まず、入力されたキーをアルファベットへの変換を試みる

				if (string.IsNullOrEmpty(input))
				{																	// アルファベットに変換できなかった場合、
					input = Tools.CommonTools.KeyToSymbol(key, isShiftKey, true);	// 記号への変換を試みる
					if (!string.IsNullOrEmpty(input))
					{																// 記号に変換できた場合、
						this._KeyClear();											// 未確定文字をクリアし、
						return input;												// 入力された記号を確定文字として返す
					}
					else return "";					// アルファベットにも記号にも変換出来なければ、何もせずに終了
				}

				return this._KeyInput(input);		// アルファベットに変換できた場合、未確定ローマ字の処理に移行する
			}
		}


		/// <summary>
		/// 未確定のローマ字にアルファベットを 1 文字入力する。
		/// </summary>
		/// <param name="key">入力するアルファベットを示す文字列。</param>
		/// <returns>この入力によりかなが確定した場合はそのかな、それ以外の場合は空の文字列。</returns>
		private string _KeyInput(string key)
		{
			string tempKeys = this._NowKeys + key;			// 未確定ローマ字文字列に、新たに入力された文字を追加
			string tempKana = this._KeysToKana(tempKeys);	// ローマ字->かな変換
			if (tempKeys == "n") tempKana = "n";			// n → ん となるのを防ぐ

			if (tempKeys != tempKana)		// 元のローマ字文字列と違う文字列が得られた場合、
			{								// ローマ字->仮名変換に変換に成功したと判断する。
				this._KeyClear();			// 入力中の未確定文字列を削除
				return tempKana;			// 確定したかなを返す
			}
			else
			{								// 上記でない場合、かなが完成していないローマ字なので
				this._NowKeys = tempKeys;	// 未確定のローマ字とする
				return "";					// 空の文字列を返す
			}
		}


		/// <summary>
		/// 入力中で未確定のローマ字を 1 文字削除する。
		/// </summary>
		/// <returns>1 文字削除を行った場合は true、未確定の文字が未入力で削除を行わなかった場合は false。</returns>
		private bool _KeyDelete()
		{
			if (this._NowKeys.Length >= 1)
			{
				this._NowKeys = this._NowKeys.Remove(this._NowKeys.Length - 1);
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 未確定のローマ字をクリアする。
		/// </summary>
		private void _KeyClear()
		{
			this._NowKeys = "";
		}


		/// <summary>
		/// 与えられたローマ字を平仮名に変換する。
		/// </summary>
		/// <param name="keys">変換するローマ字。</param>
		/// <returns>平仮名に変換出来た場合はそのかな、それ以外は引数 keys の値。</returns>
		private string _KeysToKana(string keys)
		{
			return KanaConverter.RomajiToHiragana(keys);
		}

		#endregion


		#region 文字変換

		/// <summary>
		/// 与えられたひらがなの文字列をカタカナに変換する。
		/// </summary>
		/// <param name="hiragana">変換するひらがなの文字列。</param>
		/// <returns>変換されたカタカナの文字列。</returns>
		private string _HiraganaToKatakana(string hiragana)
		{
			return KanaConverter.HiraganaToKatakana(hiragana);
		}

		#endregion


		#region 外部から呼ばれるメソッド群

		/// <summary>
		/// 日本語入力管理クラスの初期化を行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>正常に初期化が行われた場合は true、それ以外は false。</returns>
		public static bool Initialize()
		{
			return Muphic.Manager.JpnLangInputManager.Instance._Initialize();
		}
		

		/// <summary>
		/// 未確定のローマ字にアルファベットを 1 文字入力する。
		/// </summary>
		/// <param name="key">入力するアルファベットを示す DirectInput のキー識別コード。</param>
		/// <param name="isShiftKey">Shift キーが押されている場合は true、それ以外は false。</param>
		/// <returns>この入力によりかなが確定した場合はそのかな、それ以外の場合は空の文字列。</returns>
		public static string KeyInput(Key key, bool isShiftKey)
		{
			return Muphic.Manager.JpnLangInputManager.Instance._KeyInput(key, isShiftKey);
		}


		/// <summary>
		/// 入力中で未確定のローマ字を 1 文字削除する。
		/// </summary>
		/// <returns>1 文字削除を行った場合は true、未確定の文字が未入力で削除を行わなかった場合は false。</returns>
		public static bool KeyDelete()
		{
			return Muphic.Manager.JpnLangInputManager.Instance._KeyDelete();
		}


		/// <summary>
		/// 未確定のローマ字をクリアする。
		/// </summary>
		public static void KeyClear()
		{
			Muphic.Manager.JpnLangInputManager.Instance._KeyClear();
		}


		/// <summary>
		/// 与えられたひらがなの文字列をカタカナに変換する。
		/// </summary>
		/// <param name="hiragana">変換するひらがなの文字列。</param>
		/// <returns>変換されたカタカナの文字列。</returns>
		public static string HiraganaToKatakana(string hiragana)
		{
			return Muphic.Manager.JpnLangInputManager.Instance._HiraganaToKatakana(hiragana);
		}

		#endregion

	}
}
