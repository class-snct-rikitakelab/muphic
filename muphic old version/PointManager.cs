using System;
using System.Collections;
using System.Drawing;

namespace muphic
{
	#region Ver.1
/*	/// <summary>
	/// PointManager
	/// </summary>
	public class PointManager
	{
		static private PointManager pointManager;					//タネ
		private Hashtable PointTable;								//座標を格納するハッシュテーブル							

		public PointManager()
		{
			PointTable = new Hashtable();
			muphic.PointManager.pointManager = this;
		}

		/// <summary>
		/// 実際に座標を登録する
		/// </summary>
		/// <param name="ClassName">ハッシュテーブルに登録するキー(クラス名)</param>
		/// <param name="r">登録する座標・幅・高さが格納されている四角形</param>
		public void SetPoint(String ClassName, Rectangle r)
		{
			if(!PointTable.Contains(ClassName))							//もし既に格納されていないなら
			{
				PointTable.Add(ClassName, r);							//追加する
			}
		}

		/// <summary>
		/// 与えられたキーを元に該当する座標を返す。なかった場合はRectangle.Emptyを返す
		/// </summary>
		/// <param name="ClassName">探すキー</param>
		/// <returns>該当した座標</returns>
		public Rectangle GetPoint(String ClassName)
		{
			if(!PointTable.Contains(ClassName))							//なかった場合
			{
				return Rectangle.Empty;									//"何もない"を返す
			}
			return (Rectangle)PointTable[ClassName];					//あった場合は普通に返す
		}



		/// <summary>
		/// 座標をPointManagerに登録するメソッド
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">登録する座標の横成分</param>
		/// <param name="y">登録する座標の縦成分</param>
		/// <param name="width">登録する幅</param>
		/// <param name="height">登録する高さ</param>
		public static void Set(String ClassName, int x, int y, int width, int height)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, new Rectangle(x, y, width, height));
		}

		/// <summary>
		/// 座標をPointManagerに登録するメソッド
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="location">登録する座標</param>
		/// <param name="size">登録する幅・高さ</param>
		public static void Set(String ClassName, Point location, Size size)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, new Rectangle(location, size));
		}

		/// <summary>
		/// 座標をPointManagerに登録するメソッド
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="r">登録する座標・幅・高さ</param>
		public static void Set(String ClassName, Rectangle r)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, r);
		}

		/// <summary>
		/// 与えられたキーを元に該当する座標を探し出すメソッド。ない場合はRectangle.Emptyを返す。
		/// </summary>
		/// <param name="ClassName">探すキー</param>
		/// <returns>該当した座標</returns>
		public static Rectangle Get(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName);
		}


	}*/
	#endregion

	#region Ver.2
	/*
	/// <summary>
	/// PointManager version2 Delete関係が追加されてるよ
	///           　 あと、state毎に座標が変わるようになってるよ(座標も配列使うようになったよ)
	/// </summary>
	public class PointManager
	{
		static private PointManager pointManager;					//タネ
		private Hashtable PointTable;								//座標を格納するハッシュテーブル							

		public PointManager()
		{
			PointTable = new Hashtable();
			muphic.PointManager.pointManager = this;
		}

		/// <summary>
		/// 実際に座標を登録する
		/// </summary>
		/// <param name="ClassName">ハッシュテーブルに登録するキー(クラス名)</param>
		/// <param name="r">登録する座標・幅・高さが格納されている四角形</param>
		public void SetPoint(String ClassName, Rectangle[] r)
		{
			if(!PointTable.Contains(ClassName))							//もし既に格納されていないなら
			{
				PointTable.Add(ClassName, r);							//追加する
			}
		}

		/// <summary>
		/// 与えられたキーを元に該当する座標を配列のまま返す。なかった場合はnullを返す
		/// </summary>
		/// <param name="ClassName">探すキー</param>
		/// <returns>該当した座標達</returns>
		public Rectangle[] GetPointAll(String ClassName)
		{
			if(!PointTable.Contains(ClassName))							//なかった場合
			{
				return null;											//nullを返す
			}
			return (Rectangle[])PointTable[ClassName];					//あった場合は普通に返す
		}

		/// <summary>
		/// 与えられたキーとstateを元に該当する座標を返す。なかった場合はRectangle.Emptyを返す
		/// </summary>
		/// <param name="ClassName">探すキー</param>
		/// <param name="state">現在の状態</param>
		/// <returns>該当した座標</returns>
		public Rectangle GetPoint(String ClassName, int state)
		{
			if(!PointTable.Contains(ClassName))							//なかった場合
			{
				return Rectangle.Empty;									//"何もない"を返す
			}
			return ((Rectangle[])PointTable[ClassName])[state];			//あった場合は普通に返す
		}

		/// <summary>
		/// 与えられたキーに該当する座標を削除する。
		/// </summary>
		/// <param name="ClassName">与えられたキー</param>
		public void DeletePoint(String ClassName)
		{
			PointTable.Remove(ClassName);								//該当するものを削除
		}

		/// <summary>
		/// 座標をPointManagerに登録するメソッド
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="rs">登録する座標たち</param>
		public static void Set(String ClassName, Rectangle[] rs)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, rs);
		}

		/// <summary>
		/// 与えられたキーを元に該当する座標達を探し出すメソッド。ない場合はnullを返す。
		/// </summary>
		/// <param name="ClassName">探すキー</param>
		/// <returns>該当した座標</returns>
		public static Rectangle[] GetAll(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPointAll(ClassName);
		}

		/// <summary>
		/// 与えられたキーと元に該当する座標を探し出すメソッド。ない場合はRectangle.Emptyを返す(state=0としてやっている)
		/// </summary>
		/// <param name="ClassName">与えられたキー</param>
		/// <returns>該当した座標(ない場合はRectangle.Empty)</returns>
		public static Rectangle Get(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName, 0);
		}

		
		/// <summary>
		/// 与えられたキーと元に該当する座標を探し出すメソッド。ない場合はRectangle.Emptyを返す
		/// </summary>
		/// <param name="ClassName">与えられたキー</param>
		/// <param name="state">現在の状態</param>
		/// <returns>該当した座標(ない場合はRectangle.Empty)</returns>
		public static Rectangle Get(String ClassName, int state)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName, state);
		}

		/// <summary>
		/// 与えられたキーに該当する座標を削除するメソッド。
		/// </summary>
		/// <param name="ClassName">削除するキー</param>
		public static void Delete(String ClassName)
		{
			muphic.PointManager.pointManager.DeletePoint(ClassName);
		}
	}*/
	#endregion

	#region Ver.3
	/// <summary>
	/// PointManager version2 Delete関係が追加されてるよ
	///           　 あと、state毎に座標が変わるようになってるよ(座標も配列使うようになったよ)
	/// PointManager version3 既に登録されている状態でSetメソッドを呼ぶと、拒否ではなく上書きにした。
	/// </summary>
	public class PointManager
	{
		static private PointManager pointManager;					//タネ
		private Hashtable PointTable;								//座標を格納するハッシュテーブル							

		public PointManager()
		{
			PointTable = new Hashtable();
			muphic.PointManager.pointManager = this;
		}

		/// <summary>
		/// 実際に座標を登録する
		/// </summary>
		/// <param name="ClassName">ハッシュテーブルに登録するキー(クラス名)</param>
		/// <param name="r">登録する座標・幅・高さが格納されている四角形</param>
		public void SetPoint(String ClassName, Rectangle[] r)
		{
			if(PointTable.Contains(ClassName))							//もし既に格納されているなら
			{
				PointTable.Remove(ClassName);							//すでにあるものを削除してから
			}
			PointTable.Add(ClassName, r);							//追加する
		}

		/// <summary>
		/// 与えられたキーを元に該当する座標を配列のまま返す。なかった場合はnullを返す
		/// </summary>
		/// <param name="ClassName">探すキー</param>
		/// <returns>該当した座標達</returns>
		public Rectangle[] GetPointAll(String ClassName)
		{
			if(!PointTable.Contains(ClassName))							//なかった場合
			{
				return null;											//nullを返す
			}
			return (Rectangle[])PointTable[ClassName];					//あった場合は普通に返す
		}

		/// <summary>
		/// 与えられたキーとstateを元に該当する座標を返す。なかった場合はRectangle.Emptyを返す
		/// </summary>
		/// <param name="ClassName">探すキー</param>
		/// <param name="state">現在の状態</param>
		/// <returns>該当した座標</returns>
		public Rectangle GetPoint(String ClassName, int state)
		{
			if(!PointTable.Contains(ClassName))							//なかった場合
			{
				return Rectangle.Empty;									//"何もない"を返す
			}
			return ((Rectangle[])PointTable[ClassName])[state];			//あった場合は普通に返す
		}

		/// <summary>
		/// 与えられたキーに該当する座標を削除する。
		/// </summary>
		/// <param name="ClassName">与えられたキー</param>
		public void DeletePoint(String ClassName)
		{
			PointTable.Remove(ClassName);								//該当するものを削除
		}

		/// <summary>
		/// 座標をPointManagerに登録するメソッド
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="rs">登録する座標たち</param>
		public static void Set(String ClassName, Rectangle[] rs)
		{
			muphic.PointManager.pointManager.SetPoint(ClassName, rs);
		}

		/// <summary>
		/// 与えられたキーを元に該当する座標達を探し出すメソッド。ない場合はnullを返す。
		/// </summary>
		/// <param name="ClassName">探すキー</param>
		/// <returns>該当した座標</returns>
		public static Rectangle[] GetAll(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPointAll(ClassName);
		}

		/// <summary>
		/// 与えられたキーと元に該当する座標を探し出すメソッド。ない場合はRectangle.Emptyを返す(state=0としてやっている)
		/// </summary>
		/// <param name="ClassName">与えられたキー</param>
		/// <returns>該当した座標(ない場合はRectangle.Empty)</returns>
		public static Rectangle Get(String ClassName)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName, 0);
		}

		
		/// <summary>
		/// 与えられたキーと元に該当する座標を探し出すメソッド。ない場合はRectangle.Emptyを返す
		/// </summary>
		/// <param name="ClassName">与えられたキー</param>
		/// <param name="state">現在の状態</param>
		/// <returns>該当した座標(ない場合はRectangle.Empty)</returns>
		public static Rectangle Get(String ClassName, int state)
		{
			return muphic.PointManager.pointManager.GetPoint(ClassName, state);
		}

		/// <summary>
		/// 与えられたキーに該当する座標を削除するメソッド。
		/// </summary>
		/// <param name="ClassName">削除するキー</param>
		public static void Delete(String ClassName)
		{
			muphic.PointManager.pointManager.DeletePoint(ClassName);
		}
	}
	#endregion
}
