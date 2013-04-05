using System.Drawing;
using Microsoft.DirectX.DirectInput;

namespace Muphic
{
	/// <summary>
	/// マウスイベント時のデータを提供する。
	/// </summary>
	public class MouseStatusArgs
	{
		/// <summary>
		/// マウスの現在位置を表わす System.Drawing.Point 構造体を取得または設定する。
		/// </summary>
		public Point NowLocation { get; private set; }

		/// <summary>
		/// マウスのドラッグ開始位置を表わす System.Drawing.Point 構造体を取得または設定する。
		/// </summary>
		public Point BeginLocation { get; private set; }


		/// <summary>
		/// ドラッグ中であることを表すフラグを取得する。
		/// </summary>
		public bool IsDrag
		{
			get { return this.NowLocation != this.BeginLocation; }
		}

		/// <summary>
		/// マウスの現在位置の x 座標を取得する。
		/// </summary>
		public int X
		{
			get { return this.NowLocation.X; }
		}
		/// <summary>
		/// マウスの現在位置の y 座標を取得する。
		/// </summary>
		public int Y
		{
			get { return this.NowLocation.Y; }
		}


		/// <summary>
		/// MouseStatusArgs の新しいインスタンスを生成する。
		/// </summary>
		/// <param name="location">マウス位置。</param>
		public MouseStatusArgs(Point location)
			: this(location, location)
		{
		}
		/// <summary>
		/// MouseStatusArgs の新しいインスタンスを生成する。
		/// </summary>
		/// <param name="nowLocation">マウスの現在位置。</param>
		/// <param name="beginLocation">ドラッグ開始位置。</param>
		public MouseStatusArgs(Point nowLocation, Point beginLocation)
		{
			this.NowLocation = nowLocation;
			this.BeginLocation = beginLocation;
		}
	}


	/// <summary>
	/// キーイベント時のデータを提供する。
	/// </summary>
	public class KeyboardStatusArgs
	{
		/// <summary>
		/// 押されたキーボード コードを取得する。
		/// </summary>
		public Key KeyCode { get; private set; }

		/// <summary>
		/// Shift キーが押されたかどうかを示す値を取得する。
		/// </summary>
		public bool Shift { get; private set; }

		/// <summary>
		/// Control キーが押されたかどうかを示す値を取得する。
		/// </summary>
		public bool Control { get; private set; }

		/// <summary>
		/// Alt キーが押されたかどうかを示す値を取得する。
		/// </summary>
		public bool Alt { get; private set; }

		/// <summary>
		/// KeyboardStatusArgs の新しいインスタンスを生成する。
		/// </summary>
		/// <param name="keyCode">押されたキーボード コード。</param>
		/// <param name="shift">Shift キーが押されたかどうか。</param>
		/// <param name="control">Control キーが押されたかどうか。</param>
		/// <param name="alt">Alt キーが押されたかどうか。</param>
		public KeyboardStatusArgs(Key keyCode, bool shift, bool control, bool alt)
		{
			this.KeyCode = keyCode;
			this.Shift = shift;
			this.Control = control;
			this.Alt = alt;
		}
	}


	/// <summary>
	/// 画面描画時のデータを提供する。
	/// </summary>
	public class DrawStatusArgs
	{
		/// <summary>
		/// ダイアログ表示中であることを表すフラグを取得または設定する。
		/// </summary>
		public bool ShowDialog { get; set; }

		/// <summary>
		/// カーソルを描画する際の透過度を取得または設定する。
		/// </summary>
		public byte CursorAlpha { get; set; }


		/// <summary>
		/// DrawStatusArgs の新しいインスタンスを初期化する。
		/// </summary>
		public DrawStatusArgs()
		{
			this.ShowDialog = false;
			this.CursorAlpha = 255;
		}
	}
}
