using System.ComponentModel;
using System.Windows.Forms;

namespace Muphic.SubForms
{
	/// <summary>
	/// muphic 起動時に表示されるスプラッシュウィンドウ。
	/// </summary>
	public class SplashWindow : Form
	{
		// =============================================================================================================
		// muphic スプラッシュウィンドウのフォームに関するコードを記述（フォームデザイナは使ってない）
		// =============================================================================================================

		/// <summary>
		/// スプラッシュウィンドウの新しいインスタンスを初期化する。
		/// </summary>
		public SplashWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		#region Windows フォームデザイナで生成されたコード（のようなもの）

		/// <summary>
		/// コンポーネント
		/// </summary>
		IContainer components = null;

		/// <summary>
		/// スプラッシュウィンドウ上に表示される muphic バージョン情報ラベル。
		/// </summary>
		private Label versionLabel;


		/// <summary>
		/// スプラッシュウィンドウを初期化する
		/// </summary>
		private void InitializeComponent()
		{
			// バージョン情報ラベルをインスタンス化
			this.versionLabel = new Label();

			// ウィンドウの初期化開始を通知
			this.SuspendLayout();

			#region バージョン情報ラベル

			this.versionLabel.Name = "versionLabel";

			// ラベル位置設定
			this.versionLabel.Location = Settings.PartsLocation.Default.SplashWindow_VersionLabel;

			// ラベルのサイズを設定
			this.versionLabel.Size = new System.Drawing.Size(100, 12);

			// ラベルのテキストを設定
			this.versionLabel.Text = "Version " + Manager.SystemInfoManager.MuphicVersion;

			// 背景色をスプラッシュウィンドウに合わせる
			this.versionLabel.BackColor = Settings.System.Default.MuphicYellow;

			// テキストの位置を下端右寄せに設定
			this.versionLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;

			#endregion

			this.Name = "SplashWindow";

			// スプラッシュウィンドウテクスチャを背景に設定
			this.BackgroundImage = Muphic.Properties.Resources.Image_SplashWindow;

			// ウィンドウサイズを設定（スプラッシュウィンドウのテクスチャサイズ）
			this.ClientSize = new System.Drawing.Size(this.BackgroundImage.Width, this.BackgroundImage.Height);

			// カーソルをアプリケーション開始モードにする
			this.Cursor = System.Windows.Forms.Cursors.AppStarting;

			// フォームの境界線スタイルを無しにする
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

			// コントロールボックス・最大化ボタン・最小化ボタン・アイコンを非表示にする
			this.ControlBox = false;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.ShowInTaskbar = false;

			// タスクバーの表示を消す
			this.ShowInTaskbar = false;

			// サイズ変更グリップを非表示にする
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;

			// 表示時の位置を画面中央に設定
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

			// フォームのテキストを設定（表示されませんがね（ ´_ゝ`）
			this.Text = "muphic 起動中...";

			// バージョン情報ラベルを追加
			this.Controls.Add(this.versionLabel);

			// ウィンドウの初期化終了を通知
			this.ResumeLayout(true);
			this.PerformLayout();
		}

		#endregion
	}
}
