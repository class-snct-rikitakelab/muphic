using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;

namespace Muphic
{
	/// <summary>
	/// muphic メインウィンドウ
	/// </summary>
	public partial class MainWindow : Form
	{
		// ==============================================
		// 入力インタフェースデバイスに関するコードを記述
		// ==============================================


		#region マウス / マウスポインタ関連

		/// <summary>
		/// マウスカーソルの状態  0:通常 1:クリック
		/// </summary>
		public int CursorState { get; private set; }

		/// <summary>
		/// ドラッグ中であることを表すフラグ
		/// </summary>
		public bool IsClicked { get; private set; }

		/// <summary>
		/// ドラッグ開始時のマウス座標
		/// </summary>
		public Point BeginLocation { get; private set; }

		/// <summary>
		/// 現在のマウスポインタ座標
		/// </summary>
		public Point NowMouseLocation { get; private set; }


		/// <summary>
		/// マウスカーソルを描画する。
		/// </summary>
		private void DrawCursor()
		{
			this.DrawCursor(255);
		}

		private void DrawCursor(byte alpha)
		{
			Manager.DrawManager.Draw("muphicCursor", this.NowMouseLocation.X - 10, this.NowMouseLocation.Y - 12, this.CursorState, alpha);
		}


		/// <summary>
		/// マウスポインタ移動の動作
		/// </summary>
		/// <param name="e">。</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			// イベント発生時のマウス位置を取得
			this.NowMouseLocation = e.Location;
			
			if (IsClicked == true)
			{
				// クリック中であればドラッグ処理
				this.TopScreen.MouseMove(new MouseStatusArgs(this.NowMouseLocation, this.BeginLocation));
			}
			else
			{
				// クリック中でなければ通常のポインタ移動処理
				this.TopScreen.MouseMove(new MouseStatusArgs(this.NowMouseLocation));
			}
		}


		/// <summary>
		/// クリックボタンが押された時の動作
		/// </summary>
		/// <param name="e">。</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			// イベント発生時のマウス位置を取得
			// これがドラッグ開始時のマウス位置となる
			this.BeginLocation = e.Location;

			// マウスポインタをクリック状態にする
			this.CursorState = 1;
			this.IsClicked = true;

			// ドラッグ開始メソッドを呼ぶ
			this.TopScreen.DragBegin(new MouseStatusArgs(this.BeginLocation));
		}


		/// <summary>
		/// クリックボタンが上がった時の動作
		/// </summary>
		/// <param name="e">。</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			// クリック処理
			// ボタンが押された時点の部品と上がった時点の部品が違う場合、ScreenクラスのClickメソッド内で弾かれる点に注意
			Tools.DebugTools.ConsolOutputMessage("Click", this.NowMouseLocation.ToString(), true);
			this.TopScreen.Click(new MouseStatusArgs(this.NowMouseLocation, this.BeginLocation));
			Tools.DebugTools.ConsolOutputMessage(" ", "", true);

			// ドラッグ終了メソッドを呼ぶ
			this.TopScreen.DragEnd(new MouseStatusArgs(this.NowMouseLocation, this.BeginLocation));

			// マウスポインタをクリックしてない状態にする
			this.CursorState = 0;
			this.IsClicked = false;

			// ドラッグ開始位置初期化
			this.BeginLocation = Point.Empty;

			// マウス操作があった場合はShiftキー操作無効化
			// this.IsShiftKey = false;
		}


		#endregion


		#region キーボード入力 (DirectInput からの呼び出し)

		/// <summary>
		/// キーボードからの入力に対し、トップ画面へキー入力の情報を伝達させる。
		/// </summary>
		/// <param name="key">押されたキー。</param>
		public void TopScreenKeyDown(Key key)
		{
			//if (Manager.KeyboardInputManager.IsProtection)
			//{
			//    // 児童/生徒による操作の場合、シフトキー以外のキーボードは無効にする
			//    Manager.LogFileManager.WriteLine(
			//        Properties.Resources.Msg_MainWindow_KeyPress,
			//        Properties.Resources.Msg_MainWindow_KeyPress_Block
			//    );
			//    return;
			//}

			this.TopScreen.KeyDown(new KeyboardStatusArgs(
				key,
				Manager.KeyboardInputManager.IsShift,
				Manager.KeyboardInputManager.IsControl,
				Manager.KeyboardInputManager.IsAlternate
			));
		}

		#endregion


		#region キーボード関連 (OnKeyDown 使用 / 廃止済み)

		///// <summary>
		///// シフトキーが押されると立つフラグ。Client 動作時の戻るボタン等で使用する。
		///// </summary>
		// public bool IsShiftKey { get; private set; }

		///// <summary>
		///// キーボード入力された時の動作
		///// </summary>
		///// <param name="e">。</param>
		//protected override void OnKeyDown(KeyEventArgs e)
		//{
		//    if (MainWindow.MuphicOperationMode == MuphicOperationMode.StudentMode && !(e.Shift))
		//    {
		//        // 児童/生徒による操作の場合、シフトキー以外のキーボードは無効にする
		//        Manager.LogFileManager.WriteLine(
		//            Properties.Resources.Msg_MainWindow_KeyPress,
		//            Properties.Resources.Msg_MainWindow_KeyPress_Block
		//        );
		//        return;
		//    }

		//    base.OnKeyDown(e);

		//    // 押されたキーによって処理
		//    switch (e.KeyCode)
		//    {
		//        case Keys.ShiftKey:				// シフトキー
		//            this.IsShiftKey = true;		// 次のクリックまでシフトキーフラグON
		//            Manager.LogFileManager.WriteLine(
		//                Properties.Resources.Msg_MainWindow_KeyPress,
		//                Properties.Resources.Msg_MainWindow_KeyPress_ShiftKey
		//            );
		//            break;

		//        case Keys.Enter:										// リターンキー
		//            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)	// 同時にAltキーが押されているかをチェック
		//            {													// Altキーも押されていた場合
		//                Manager.DrawManager.ChangeWindowMode();			// ウィンドウ/フルスクリーンモードの切り替え
		//                Manager.LogFileManager.WriteLine(
		//                    Properties.Resources.Msg_MainWindow_KeyPress,
		//                    Properties.Resources.Msg_MainWindow_KeyPress_AltEnter
		//                );
		//                return;
		//            }
		//            else goto default;

		//        case Keys.Escape:					// エスケープキー
		//            MainWindow.Running = false;		// プログラム終了
		//            Manager.LogFileManager.WriteLine(
		//                Properties.Resources.Msg_MainWindow_KeyPress,
		//                Properties.Resources.Msg_MainWindow_KeyPress_Esc
		//            );
		//            return;

		//        case Keys.D:
		//            Tools.DebugTools.ShowDebugWindow();
		//            goto default;

		//        default:							// その他
		//            Tools.DebugTools.ConsolOutputMessage(
		//                Properties.Resources.Msg_MainWindow_KeyPress,
		//                e.KeyCode.ToString(),
		//                true
		//            );
		//            break;
		//    }

		//    this.TopScreen.KeyDown(new KeyboardStatusArgs(e.KeyCode, e.Shift, e.Control, e.Alt));
		//}

		#endregion

	}
}
