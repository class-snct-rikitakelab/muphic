using System;
using System.Drawing;
using System.Collections;
using System.Runtime.InteropServices ;  // for DllImport, Marshal

namespace muphic.Tutorial
{
	/// <summary>
	/// マウスカーソルの自動制御に関するメソッド
	/// MNDMにつき一時凍結
	/// </summary>
	public class CursorControl
	{
		public TutorialMain parent;
		
		
		[DllImport("user32.dll")]
		extern static uint SendInput(
			uint       nInputs,   // INPUT 構造体の数(イベント数)
			INPUT[]    pInputs,   // INPUT 構造体
			int        cbSize     // INPUT 構造体のサイズ
			) ;
		
		[StructLayout(LayoutKind.Sequential)]  // アンマネージ DLL 対応用 struct 記述宣言
			struct INPUT
		{ 
			public int        type ;  // 0 = INPUT_MOUSE(デフォルト), 1 = INPUT_KEYBOARD
			public MOUSEINPUT mi   ;
		}
		
		[StructLayout(LayoutKind.Sequential)]  // アンマネージ DLL 対応用 struct 記述宣言
			struct MOUSEINPUT
		{
			public int    dx ;
			public int    dy ;
			public int    mouseData ;  // amount of wheel movement
			public int    dwFlags   ;
			public int    time      ;  // time stamp for the event
			public IntPtr dwExtraInfo ;
			// Note: struct の場合、デフォルト(パラメータなしの)コンストラクタは、
			//       言語側で定義済みで、フィールドを 0 に初期化する。
		}
		
		// dwFlags
		const int MOUSEEVENTF_MOVED      = 0x0001 ;
		const int MOUSEEVENTF_LEFTDOWN   = 0x0002 ;  // 左ボタン Down
		const int MOUSEEVENTF_LEFTUP     = 0x0004 ;  // 左ボタン Up
		const int MOUSEEVENTF_RIGHTDOWN  = 0x0008 ;  // 右ボタン Down
		const int MOUSEEVENTF_RIGHTUP    = 0x0010 ;  // 右ボタン Up
		const int MOUSEEVENTF_MIDDLEDOWN = 0x0020 ;  // 中ボタン Down
		const int MOUSEEVENTF_MIDDLEUP   = 0x0040 ;  // 中ボタン Up
		const int MOUSEEVENTF_WHEEL      = 0x0080 ;
		const int MOUSEEVENTF_XDOWN      = 0x0100 ;
		const int MOUSEEVENTF_XUP        = 0x0200 ;
		const int MOUSEEVENTF_ABSOLUTE   = 0x8000 ;
		
		const int screen_length = 0x10000 ;			 // for MOUSEEVENTF_ABSOLUTE (この値は固定)
		
		
		[DllImport("user32.dll")]
		private static extern void mouse_event( 
			UInt32 dwFlags, 
			UInt32 dx, 
			UInt32 dy, 
			UInt32 dwData, 
			IntPtr dwExtraInfo 
			); 
		
		/// <summary>
		/// とりあえずコンストラクタ
		/// </summary>
		/// <param name="tm"></param>
		public CursorControl(TutorialMain tm)
		{
			this.parent = tm;
		}
		
		/// <summary>
		/// マウスカーソルを指定された座標まで移動させるメソッド
		/// </summary>
		public void CursorMove(Point begin, Point after)
		{
			// マウスカーソルのスクリーン座標とmuphic座標から、muphic座標(0,0)となるスクリーン座標ZeroPointを計算により求める
			Point NowPointMuphic = this.parent.parent.parent.parent.nowPoint;
			Point NowPointCursor = this.parent.parent.parent.parent.getCursorPos();
			Point ZeroPoint = new Point(NowPointCursor.X - NowPointMuphic.X, NowPointCursor.Y - NowPointMuphic.Y);
			
			// 移動開始座標の設定
			Point beginPoint = new Point(ZeroPoint.X + begin.X, ZeroPoint.Y + begin.Y);
			
			// マウスカーソルの位置を移動開始座標へ移動させる
			parent.parent.parent.parent.setCursorPos(beginPoint);
			
			// 計1イベントを格納
			INPUT[] input = new INPUT[1];  
			
			// 移動座標の計算 こんなんでいいのか激しく疑問
			input[0].mi.dx      = after.X - begin.X + (int)Math.Round((after.X - begin.X) / 50.0) + 5 ;
			input[0].mi.dy      = after.Y - begin.Y + (int)Math.Round((after.Y - begin.Y) / 50.0) + 5 ;
			input[0].mi.dwFlags = MOUSEEVENTF_MOVED;
			
			// カーソル移動操作の実行 計1イベントの一括生成
			SendInput(1, input, Marshal.SizeOf(input[0])) ;
		}
		
		
		/// <summary>
		/// マウスカーソルを指定した座標へ移動させるメソッド
		/// </summary>
		/// <param name="point"></param>
		public void CursorSet(Point point)
		{
			// マウスカーソルの位置(muphic座標)
			Point NowPointMuphic = this.parent.parent.parent.parent.nowPoint;
			
			// 計1イベントを格納
			INPUT[] input = new INPUT[1];  
			
			// 移動座標の計算 こんなんでいいのか激しく疑問
			if(NowPointMuphic.X > point.X) input[0].mi.dx = -(NowPointMuphic.X - point.X + (int)Math.Round((NowPointMuphic.X - point.X) / 50.0) + 5) ;
			else                           input[0].mi.dx = point.X - NowPointMuphic.X + (int)Math.Round((point.X - NowPointMuphic.X) / 50.0) + 5 ;
			
			if(NowPointMuphic.Y > point.Y) input[0].mi.dy = -(NowPointMuphic.Y - point.Y + (int)Math.Round((NowPointMuphic.Y - point.Y) / 50.0) + 5) ;
			else                           input[0].mi.dy = point.Y - NowPointMuphic.Y + (int)Math.Round((point.Y - NowPointMuphic.Y) / 50.0) + 5 ;

			input[0].mi.dwFlags = MOUSEEVENTF_MOVED;
			
			// カーソル移動操作の実行 計1イベントの一括生成
			SendInput(1, input, Marshal.SizeOf(input[0]));
		}
		
		
		/// <summary>
		/// その時点でのカーソル位置でクリックを行うメソッド
		/// </summary>
		public void CursorClick()
		{
			// 計2イベントを格納
			INPUT[] input = new INPUT[2];
			
			// 第1,第2イベントを、それぞれ左ボタンDownと左ボタンUpに設定
			input[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN ;
			input[1].mi.dwFlags = MOUSEEVENTF_LEFTUP ;
			
			// クリック操作の実行 計2イベントの一括生成
			SendInput(2, input, Marshal.SizeOf(input[0])) ;
		}
	}
}
