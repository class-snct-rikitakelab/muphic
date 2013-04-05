using System;
using System.Drawing;

namespace muphic.Common
{
	/// <summary>
	/// CommonTools の概要の説明です。
	/// </summary>
	public class CommonTools
	{
		public CommonTools()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		/// <summary>
		/// 文字列を指定された文字数の場合に、中央寄せのような状態に変更させるメソッド
		/// </summary>
		/// <param name="str">変更する文字列</param>
		/// <param name="length">文字数</param>
		public static String StringCenter(String str, int length)
		{
			String newStr = str;
			if (str != null)
			{
				for(int i=0;i<(length-str.Length)/2;i++)
				{
					newStr = newStr.Insert(0, "　");
					newStr = newStr.Insert(newStr.Length, "　");
				}
				if(((length-str.Length)%2) == 1)
				{
					newStr = newStr.Insert(0, " ");
					newStr = newStr.Insert(newStr.Length, " ");
				}
			}
			return newStr;
		}

		/// <summary>
		/// 指定された座標が、指定された矩形領域の中に入っているかどうか
		/// 調べるメソッド
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="r">矩形領域</param>
		/// <returns></returns>
		public static bool inRect(Point p, Rectangle r)
		{
			if(r.Left <= p.X && p.X <= r.Right)
			{
				if(r.Top <= p.Y && p.Y <= r.Bottom)
				{
					return true;
				}
			}
			return false;
		}
	}
}
