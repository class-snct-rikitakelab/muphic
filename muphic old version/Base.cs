using System;
using System.Drawing;
using muphic.Common;

namespace muphic
{
	/// <summary>
	/// Base の概要の説明です。
	/// </summary>
	public class Base
	{
		private bool visible;
		private int state;

		public bool Visible 
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;
			}
		}

		public int State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;
			}
		}

		public Base()
		{
			this.Visible = true;
			this.State = 0;
		}

		/// <summary>
		/// マウスが部品内に入ってきたときに呼ばれるメソッド
		/// </summary>
		public virtual void MouseEnter()
		{
		}

		/// <summary>
		/// マウスが部品内から出て行ったときに呼ばれるメソッド
		/// </summary>
		public virtual void MouseLeave()
		{
		}

		/// <summary>
		/// マウスが部品内でクリック(厳密にはマウスボタンが離された)た時に呼ばれるメソッド
		/// </summary>
		/// <param name="p">現在のマウス座標</param>
		public virtual void Click(Point p)
		{
		}

		/// <summary>
		/// マウスが部品内でドラッグされた時に呼ばれるメソッド
		/// </summary>
		/// <param name="begin">ドラッグ開始座標</param>
		/// <param name="p">現在のマウス座標</param>
		public virtual void Drag(Point begin, Point p)
		{
		}
	}
}
