using System.Collections.Generic;
using System.Drawing;

namespace Muphic.Manager
{
	/// <summary>
	/// 矩形管理クラス (シングルトン・継承不可) 
	/// <para>各部構成パーツの表示座標を含めた矩形を管理する (muphic ver.7 以前では PointManager)。</para>
	/// </summary>
	public sealed class RectangleManager : Manager
	{
		/// <summary>
		/// 矩形管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static RectangleManager __instance = new RectangleManager();

		/// <summary>
		/// 矩形管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static RectangleManager Instance
		{
			get { return RectangleManager.__instance; }
		}


		/// <summary>
		/// 各部構成パーツとその表示座標を含めた矩形を関連付けたテーブル
		/// </summary>
		private Dictionary<string, Rectangle[]> RectangleTable { get; set; }


		#region コンストラクタ/初期化

		/// <summary>
		/// 矩形管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private RectangleManager()
		{
			this.RectangleTable = new Dictionary<string, Rectangle[]>();
		}

		/// <summary>
		/// 矩形管理クラスの静的インスタンスの初期化を行う。
		/// メイン画面生成時にこのメソッドを呼び出すことで、シングルトンパターンの静的インスタンスを生成する。
		/// </summary>
		private void _Initialize()
		{
			if (this._IsInitialized) return;

			Tools.DebugTools.ConsolOutputMessage("RectangleManager -Initialize", "矩形管理クラス生成完了", true);

			this._IsInitialized = true;
		}

		#endregion


		#region RectangleTableを直接操作するメソッド群

		/// <summary>
		/// 座標を登録する
		/// </summary>
		/// <param name="className">登録するキー(座標名)。</param>
		/// <param name="partsRectangles">登録するキーの矩形。</param>
		private void _Regist(string className, Rectangle[] partsRectangles)
		{
			// 既にキーが存在する場合は削除してから登録しなおす
			if (RectangleTable.ContainsKey(className)) RectangleTable.Remove(className);
			
			// キーとその座標の登録
			RectangleTable.Add(className, partsRectangles);
		}


		/// <summary>
		/// 与えられたキーに該当する座標を削除する
		/// </summary>
		/// <param name="className">削除するキー(クラス名)。</param>
		private void _Delete(string className)
		{
			// ふつーに削除 結果は気にしない
			RectangleTable.Remove(className);
		}


		/// <summary>
		/// 与えられたキーに該当する座標を得る
		/// </summary>
		/// <param name="className">探すキー(クラス名)。</param>
		/// <returns>座標の配列。</returns>
		private Rectangle[] _GetAll(string className)
		{
			// キーの存在チェック
			if (!RectangleTable.ContainsKey(className))
			{
				// 存在しない場合はエラーメッセージをコンソール出力しnullを返す
				Muphic.Tools.DebugTools.ConsolOutputError("RectangleManager - GetPointAll", "クラス: " + className + " は未登録のためテクスチャ名参照失敗", true);
				return null;
			}

			// 座標を返す
			return RectangleTable[className];
		}


		/// <summary>
		/// 与えられたキーとstateに該当する座標を得る
		/// </summary>
		/// <param name="className">探すキー(クラス名)。</param>
		/// <param name="state">state。</param>
		/// <returns>座標の配列。</returns>
		private Rectangle _GetSingle(string className, int state)
		{
			// キーの存在チェック
			if (!RectangleTable.ContainsKey(className))
			{
				// 存在しない場合はエラーメッセージをコンソール出力し空で返す
				Muphic.Tools.DebugTools.ConsolOutputError("RectangleManager - GetPointAll", "クラス: " + className + " は未登録のためテクスチャ名参照失敗", true);
				return new Rectangle(-1, -1, -1, -1);
			}

			// 座標を返す
			return ((Rectangle[])RectangleTable[className])[state];
		}

		
		/// <summary>
		/// キーが存在するかチェックする
		/// </summary>
		/// <param name="className">チェックするキー(クラス名)。</param>
		/// <returns>trueなら存在している。</returns>
		private bool _Exist(string className)
		{
			return RectangleTable.ContainsKey(className);
		}
		
		#endregion


		#region 外部から呼び出されるメソッド群

		/// <summary>
		/// 矩形管理クラスの静的インスタンスを生成する。
		/// シングルトンパターンとして一度生成された後はこのメソッドは意味を為さないので注意。
		/// </summary>
		public static void Initialize()
		{
			Muphic.Manager.RectangleManager.Instance._Initialize();
		}

		/// <summary>
		/// 座標の登録を行う
		/// </summary>
		/// <param name="className">ハッシュに登録するキー(クラス名)。</param>
		/// <param name="partsRectangles">登録する座標。</param>
		public static void Regist(string className, Rectangle[] partsRectangles)
		{
			Muphic.Manager.RectangleManager.Instance._Regist(className, partsRectangles);
		}


		/// <summary>
		/// 指定したキーをハッシュから削除する
		/// </summary>
		/// <param name="className">削除するキー(クラス名)。</param>
		public static void Delete(string className)
		{
			Muphic.Manager.RectangleManager.Instance._Delete(className);
		}


		/// <summary>
		/// 指定したキーに該当する座標を全て得る
		/// 存在しない場合はnullを返す
		/// </summary>
		/// <param name="className">キー(クラス名)。</param>
		/// <returns>該当する座標の配列。</returns>
		public static Rectangle[] GetAll(string className)
		{
			return Muphic.Manager.RectangleManager.Instance._GetAll(className);
		}


		/// <summary>
		/// 指定したキーに該当する矩形を1つ得る(state0)
		/// </summary>
		/// <param name="className">キー(クラス名)。</param>
		/// <returns>該当する座標。</returns>
		public static Rectangle Get(string className)
		{
			return Muphic.Manager.RectangleManager.Instance._GetSingle(className, 0);
		}

		/// <summary>
		/// 指定したキーとstateに該当する座標を1つ得る
		/// </summary>
		/// <param name="className">キー(クラス名)。</param>
		/// <param name="state">state。</param>
		/// <returns>該当する座標。</returns>
		public static Rectangle Get(string className, int state)
		{
			return Muphic.Manager.RectangleManager.Instance._GetSingle(className, state);
		}


		/// <summary>
		/// 指定したキーが存在するかをチェックする
		/// </summary>
		/// <param name="className">キー(クラス名)。</param>
		/// <returns>。</returns>
		public static bool Exist(string className)
		{
			return Muphic.Manager.RectangleManager.Instance._Exist(className);
		}

		#endregion
	}
}
