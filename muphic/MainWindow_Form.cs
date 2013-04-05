using Muphic.Tools;
using System.ComponentModel;
using System.Windows.Forms;

namespace Muphic
{
	/// <summary>
	/// muphic メインウィンドウ
	/// </summary>
	public partial class MainWindow : System.Windows.Forms.Form
	{
		// =====================================================================================
		// muphic メインウィンドウのフォームに関するコードを記述 (フォームデザイナは使ってない) 
		// =====================================================================================


		/// <summary>
		/// ウィンドウの [×] ボタンがクリックされると同時に実行される。
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			e.Cancel = true;				// [×] ボタンで直接閉じるのではなく
			MainWindow.Running = false;		// メインループから脱出する
		}


		/// <summary>
		/// 使用されているリソースに後処理を実行する。
		/// </summary>
		/// <param name="disposing">マネージ リソースとアンマネージ リソースの両方を解放する場合は true。アンマネージ リソースだけを解放する場合は false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)	// 各管理クラスのアンマネージ リソースを破棄
			{
				Manager.DrawManager.Dispose();
				Manager.SoundManager.Dispose();
				Manager.KeyboardInputManager.Dispose();
				Manager.LogFileManager.Dispose();
			}

			base.Dispose(disposing);
		}


		#region Windows フォーム デザイナで生成されたコード（のようなもの）

		private IContainer components;
		private NotifyIcon nitifyIcon;
		private ContextMenuStrip trayIconMenu;
		private ToolStripMenuItem trayIconMenuItem_Exit;
		private ToolStripSeparator trayIconMenuItem_Separator1;
		private ToolStripMenuItem trayIconMenuItem_Activate;
		private ToolStripSeparator trayIconMenuItem_Separator2;
		private ToolStripMenuItem trayIconMenuItem_Version;

		/// <summary>
		/// Windows フォームデザイナで生成されたコード  …を改造したメインウィンドウ初期化メソッド
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));

			// ウィンドウ初期化の開始を通知
			this.SuspendLayout();

			this.Name = "MainScreen";

			// ウィンドウサイズを設定（通常は800x600）
			this.ClientSize = new System.Drawing.Size(CommonTools.GetSettings<int>("WindowSize_Width"), CommonTools.GetSettings<int>("WindowSize_Height"));

			// ウィンドウタイトルを設定
			this.Text = CommonTools.GetSettings<string>("WindowTitle");

			// ウィンドウのアイコンを設定
			this.Icon = Properties.Resources.muphic;

			// フォームの境界線スタイルを設定 固定された一重線に設定(サイズ変更できなくする)
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

			// 最大化ボタンは押せなくする
			this.MaximizeBox = false;

			// 開始時のウィンドウ位置を設定 ディスプレイの左上に出るようにする
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

			this.ImeMode = ImeMode.Disable;

			// 表示時の位置を画面中央に設定
			// this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

			#region システムトレイアイコンメニューの設定

			// メニューのインスタンス化
			this.trayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);

			// システムトレイアイコンメニューの初期化の通知
			this.trayIconMenu.SuspendLayout();

			// メニューアイテムのインスタンス化
			this.trayIconMenuItem_Exit = new ToolStripMenuItem();
			this.trayIconMenuItem_Separator1 = new ToolStripSeparator();
			this.trayIconMenuItem_Activate = new ToolStripMenuItem();
			this.trayIconMenuItem_Separator2 = new ToolStripSeparator();
			this.trayIconMenuItem_Version = new ToolStripMenuItem();

			// メニューにアイテムを追加
			this.trayIconMenu.Items.AddRange(new ToolStripItem[] {
				this.trayIconMenuItem_Version,
				this.trayIconMenuItem_Separator2,
				this.trayIconMenuItem_Activate,
				this.trayIconMenuItem_Separator1,
				this.trayIconMenuItem_Exit
			});

			// メニューの名前とサイズを設定
			this.trayIconMenu.Name = "trayIconMenu";
			this.trayIconMenu.Size = new System.Drawing.Size(220, 55);

			// 終了(&E) メニュー
			this.trayIconMenuItem_Exit.Name = "trayIconMenuItem_Exit";
			this.trayIconMenuItem_Exit.Size = new System.Drawing.Size(220, 22);
			this.trayIconMenuItem_Exit.Text = "終了 (&E)";
			this.trayIconMenuItem_Exit.Click += new System.EventHandler(this.TrayIconMenuItem_Exit_Click);

			// セパレータ1
			this.trayIconMenuItem_Separator1.Name = "trayIconMenuItem_Separator1";
			this.trayIconMenuItem_Separator1.Size = new System.Drawing.Size(217, 6);

			// アクティブ化(&S) メニュー
			this.trayIconMenuItem_Activate.Name = "trayIconMenuItem_Active";
			this.trayIconMenuItem_Activate.Size = new System.Drawing.Size(220, 22);
			this.trayIconMenuItem_Activate.Text = "ウィンドウ表示 (&S)";
			this.trayIconMenuItem_Activate.Click += new System.EventHandler(this.TrayIconMenuItem_Activate_Click);

			// セパレータ2
			this.trayIconMenuItem_Separator2.Name = "trayIconMenuItem_Separator2";
			this.trayIconMenuItem_Separator2.Size = new System.Drawing.Size(217, 6);

			// バージョン情報(&A) メニュー
			this.trayIconMenuItem_Version.Name = "trayIconMenuItem_Version";
			this.trayIconMenuItem_Version.Size = new System.Drawing.Size(220, 22);
			this.trayIconMenuItem_Version.Text = "バージョン情報 (&A)";
			this.trayIconMenuItem_Version.Click += new System.EventHandler(trayIconMenuItem_Version_Click);

			// マウスカーソル表示のためのイベント登録
			this.trayIconMenu.MouseEnter += new System.EventHandler(trayIconMenu_MouseEnter);
			this.trayIconMenu.MouseLeave += new System.EventHandler(trayIconMenu_MouseLeave);

			// システムトレイアイコンメニューの初期化終了の通知
			this.trayIconMenu.ResumeLayout(false);

			#endregion

			#region システムトレイアイコンの設定

			// システムトレイアイコンのインスタンス化
			this.nitifyIcon = new System.Windows.Forms.NotifyIcon(this.components);

			// アイコンの設定
			this.nitifyIcon.Icon = Properties.Resources.muphic;

			// メッセージ（マウスオーバー時にポップアップされる文字列）の設定
			this.nitifyIcon.Text = "muphic";
			
			// コンテキストメニューの設定
			this.nitifyIcon.ContextMenuStrip = this.trayIconMenu;

			// ダブルクリックされたらウィンドウをアクティブにする設定
			this.nitifyIcon.DoubleClick += new System.EventHandler(this.TrayIconMenuItem_Activate_Click);

			#endregion

			// ウィンドウ初期化の終了の通知
			this.ResumeLayout(false);
		}

		#endregion


		#region システムトレイ関連

		/// <summary>
		/// システムトレイアイコンメニューの "終了 (E)" がクリックされた際の処理。
		/// </summary>
		/// <param name="sender">。</param>
		/// <param name="e">。</param>
		private void TrayIconMenuItem_Exit_Click(object sender, System.EventArgs e)
		{
			MainWindow.Running = false;
		}

		/// <summary>
		/// システムトレイアイコンメニューの "ウィンドウ表示 (S)" がクリックされた際の処理。
		/// </summary>
		/// <param name="sender">。</param>
		/// <param name="e">。</param>
		private void TrayIconMenuItem_Activate_Click(object sender, System.EventArgs e)
		{
			this.Activate();
		}

		/// <summary>
		/// システムトレイアイコンメニューの "バージョン情報 (A)" がクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void trayIconMenuItem_Version_Click(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// システムトレイのメニューにマウスが入った際の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void trayIconMenu_MouseEnter(object sender, System.EventArgs e)
		{
			// カーソルを表示
			System.Windows.Forms.Cursor.Show();
		}

		/// <summary>
		/// システムトレイのメニューからマウスが出た際の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void trayIconMenu_MouseLeave(object sender, System.EventArgs e)
		{
			// カーソルを非表示
			System.Windows.Forms.Cursor.Hide();
		}

		#endregion

	}
}
