using System.Collections.Generic;
using System.Windows.Forms;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// データアーカイバのメインウィンドウの新しいインスタンスを初期化する。
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			this.InitializeArchive(false);

			// ウィンドウの位置を復元
			this.StartPosition = FormStartPosition.Manual;
			this.Location = Properties.Settings.Default.WindowLocation;

			this.textBox_AutoAddFile.Text = Properties.Settings.Default.AutoAddFolder;
		}


		/// <summary>
		/// ファイルのアーカイブ化や展開を行うアーカイバ。
		/// </summary>
		private ArchiveControllerPlus ArchiveControllerPlus { get; set; }

		/// <summary>
		/// アーカイブ化を行うファイル (アーカイブ予約ファイル) のファイルパスのリスト。
		/// </summary>
		private List<string> FileList { get; set; }


		#region 編集フラグ

		/// <summary>
		/// 読み込んだアーカイブを編集中であることを示すフラグ。
		/// </summary>
		private bool __isChanged;

		/// <summary>
		/// 読み込んだアーカイブを編集中であることを示すフラグを取得または設定する。
		/// </summary>
		private bool IsChanged
		{
			get
			{
				return this.__isChanged;
			}
			set
			{
				if (value)	// 編集中となった場合
				{			// ウィンドウタイトルとアーカイブ情報ラベルを更新
					this.SetWindowTitleEdit();
					this.SetArchiveInfoLabelEdit();
				}

				this.__isChanged = value;
			}
		}

		#endregion


		#region アーカイバの初期化

		/// <summary>
		/// アーカイバの初期化を行う。
		/// </summary>
		/// <param name="confirm">アーカイブクリアの許可を求める場合は true、それ以外は false。</param>
		/// <returns>初期化を行った場合は true、それ外は false。</returns>
		private bool InitializeArchive(bool confirm)
		{
			if(confirm && !this.ConfirmInitialize()) return false;

			// アーカイバが保有するファイルを解放
			if(this.ArchiveControllerPlus != null) this.ArchiveControllerPlus.Dispose();

			// アーカイバを初期化
			this.ArchiveControllerPlus = new ArchiveControllerPlus();

			// リストボックスの内容をクリア
			this.listBox_FileList.Items.Clear();
			this.SelectPreviewFile(-1);

			// ファイルリストクリア
			this.FileList = new List<string>();

			// ウィンドウタイトルをくりあ
			this.InitializeWindowTitle();

			// アーカイブ情報ラベルクリア
			this.InitializeArchiveInfoLabel();

			// ファイル情報とプレビューのクリア
			this.InitializePreview();

			// アーカイブ生成機能の有効 / 無効の切替
			this.ChangeEnabledCreateArchive();

			// 編集フラグを降ろす
			this.IsChanged = false;

			return true;
		}


		/// <summary>
		/// ファイルリストをクリアして良いかをユーザーに尋ねる。
		/// </summary>
		/// <returns>クリアして良い場合は true、それ以外は false。</returns>
		public bool ConfirmInitialize()
		{
			if (this.FileList == null || this.FileList.Count <= 0) return true;

			return (MessageBox.Show(
						this,
						"現在のアーカイブはクリアされますわ。よろしいですの？",
						"リストが消えますのよ",
						MessageBoxButtons.OKCancel
					) == DialogResult.OK);
		}

		#endregion

	}
}
