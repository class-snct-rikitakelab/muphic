using System;
using System.IO;
using System.Windows.Forms;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// アーカイブファイルを展開する。
		/// </summary>
		private void OpenArchive()
		{
			if (!this.ConfirmInitialize()) return;

			// OpenFileDialogクラスのインスタンスを作成
			OpenFileDialog ofd = new OpenFileDialog();

			// はじめのファイル名を指定する
			// はじめに「ファイル名」で表示される文字列を指定する
			ofd.FileName = "*.dat";

			// はじめに表示されるフォルダを指定する
			// 指定しない（空の文字列）の時は、現在のディレクトリが表示される
			if (Directory.Exists(Properties.Settings.Default.SaveFolder))
			{
				ofd.InitialDirectory = Properties.Settings.Default.SaveFolder;
			}
			else if (Directory.Exists(@"G:\My Documents\SNCT Works\Takayuki Lab\専攻研究\muphic"))
			{
				ofd.InitialDirectory = @"G:\My Documents\SNCT Works\Takayuki Lab\専攻研究\muphic";
			}
			else
			{
				ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}

			// [ファイルの種類]に表示される選択肢を指定する
			// 指定しないとすべてのファイルが表示される
			ofd.Filter = "アーカイブ ファイル(*.dat)|*.dat|すべてのファイル(*.*)|*.*";

			// [ファイルの種類]ではじめに
			// 「すべてのファイル」が選択されているようにする
			ofd.FilterIndex = 1;

			// タイトルを設定する
			ofd.Title = "開くアーカイブ ファイルを選択してよねっ ///";

			// ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			ofd.RestoreDirectory = true;

			// 存在しないファイルの名前が指定されたとき警告を表示する
			// デフォルトでTrueなので指定する必要はない
			ofd.CheckFileExists = true;

			// 存在しないパスが指定されたとき警告を表示する
			// デフォルトでTrueなので指定する必要はない
			ofd.CheckPathExists = true;

			// ダイアログを表示する
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				// OK ボタンがクリックされたらアーカイブ展開
				this.OpenArchive(ofd.FileName, false);
			}
		}

		/// <summary>
		/// 指定されたアーカイブファイルを展開する。
		/// </summary>
		/// <param name="filePath">開くアーカイブのパス。</param>
		/// <param name="confirm">アーカイブクリアの許可を求める場合は true、それ以外は false。</param>
		public void OpenArchive(string filePath, bool confirm)
		{
			if (!this.InitializeArchive(confirm)) return;

			// アーカイブ展開
			this.ArchiveControllerPlus = new ArchiveControllerPlus(filePath);
			this.AddFiles(this.ArchiveControllerPlus.FileList, true);

			// アーカイブ情報ラベル更新
			this.SetArchiveInfoLabel(Path.GetFileName(filePath), false, this.FileList.Count, this.ArchiveControllerPlus.ArchiveFileSize / 1000);

			// ウィンドウタイトルにアーカイブファイル名を追加
			this.SetWindowTitle();
		}
	}
}
