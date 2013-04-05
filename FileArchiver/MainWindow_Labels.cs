using System.Windows.Forms;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		// ==========================================================================
		// ウィンドウタイトル及びアーカイブ情報表示部の処理を記述する。
		// 注意：ウィンドウタイトル及びアーカイブ情報表示部のコントロールのみ変更可能
		// ==========================================================================


		/// <summary>
		/// 現在読み込まれているアーカイブ ファイルのファイル名を示す文字列を取得または設定する。
		/// </summary>
		private string NowArchiveFileName { get; set; }


		#region ウィンドウタイトル

		/// <summary>
		/// ウィンドウタイトルを初期化する。
		/// </summary>
		private void InitializeWindowTitle()
		{
			this.Text = "muphic データ アーカイバ";
		}

		/// <summary>
		/// 現在開かれているファイル名を基にウィンドウタイトルを設定する。
		/// </summary>
		private void SetWindowTitle()
		{
			this.InitializeWindowTitle();
			this.Text += (this.NowArchiveFileName == "") ? " " : "  - " + this.NowArchiveFileName;
		}

		/// <summary>
		/// 現在開かれているファイル名を基に、編集中であることを示す記号を加えたウィンドウタイトルを設定する。
		/// </summary>
		private void SetWindowTitleEdit()
		{
			this.SetWindowTitle();
			this.Text += " *";
		}

		#endregion


		#region アーカイブ情報表示部

		/// <summary>
		/// アーカイブ情報表示部の各ラベルを初期化する。
		/// </summary>
		private void InitializeArchiveInfoLabel()
		{
			this.label_ArchiveFileName.Text = this.label_ArchivedFileNum.Text = "";
			this.NowArchiveFileName = "";
		}


		/// <summary>
		/// 現在のアーカイブを編集中であることをアーカイブ情報表示部に表示する。
		/// </summary>
		private void SetArchiveInfoLabelEdit()
		{
			this.SetArchiveInfoLabel(this.NowArchiveFileName, true, this.FileList.Count, 0);
		}

		/// <summary>
		/// アーカイブ情報表示部の各ラベルを更新する。
		/// </summary>
		/// <param name="isEdited">編集中であれば true、編集中でなければ false。</param>
		/// <param name="fileSize">アーカイブのファイルサイズ。編集中は無効。表示しない際は 0 を指定。</param>
		private void SetArchiveInfoLabel(bool isEdited, int fileSize)
		{
			this.SetArchiveInfoLabel(isEdited, this.FileList.Count, fileSize);
		}

		/// <summary>
		/// アーカイブ情報表示部の各ラベルを更新する。
		/// </summary>
		/// <param name="isEdited">編集中であれば true、編集中でなければ false。</param>
		/// <param name="fileNum">予約ファイルを含めたアーカイブ内のファイル数。</param>
		/// <param name="fileSize">アーカイブのファイルサイズ。編集中は無効。表示しない際は 0 を指定。</param>
		private void SetArchiveInfoLabel(bool isEdited, int fileNum, int fileSize)
		{
			// アーカイブのファイル名を設定
			this.label_ArchiveFileName.Text = this.NowArchiveFileName;

			// 編集中かつ (編集中) ラベルが付いていなければ、 (編集中) ラベルを追加
			if (isEdited && this.label_ArchiveFileName.Text.Length > 0 && this.label_ArchiveFileName.Text[this.label_ArchiveFileName.Text.Length - 1] != ')')
			{
				this.label_ArchiveFileName.Text += " (編集中)";
			}

			// ファイル数を設定
			this.label_ArchivedFileNum.Text = "ファイル数 : " + fileNum.ToString();

			// ファイルサイズが 0 でなければ、ファイルサイズも追加
			if (!isEdited && fileSize > 0) this.label_ArchivedFileNum.Text += "  (" + fileSize.ToString("#,#") + " KB)";
		}

		/// <summary>
		/// アーカイブ情報表示部の各ラベルを更新する。
		/// </summary>
		/// <param name="fileName">アーカイブのファイル名。</param>
		/// <param name="isEdited">編集中であれば true、編集中でなければ false。</param>
		/// <param name="fileNum">予約ファイルを含めたアーカイブ内のファイル数。</param>
		/// <param name="fileSize">アーカイブのファイルサイズ。編集中は無効。表示しない際は 0 を指定。</param>
		private void SetArchiveInfoLabel(string fileName, bool isEdited, int fileNum, int fileSize)
		{
			// アーカイブのファイル名を設定
			this.NowArchiveFileName = fileName;

			this.SetArchiveInfoLabel(isEdited, fileNum, fileSize);
		}

		#endregion

	}
}
