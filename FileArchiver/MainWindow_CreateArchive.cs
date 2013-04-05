using System;
using System.IO;
using System.Windows.Forms;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// 現在のファイルリストからアーカイブを生成する。
		/// </summary>
		private void CreateArchive()
		{
			// SaveFileDialogクラスのインスタンスを作成
			SaveFileDialog sfd = new SaveFileDialog();

			// はじめのファイル名を指定する
			sfd.FileName = "*.dat";

			// はじめに表示されるフォルダを指定する
			if (Directory.Exists(Properties.Settings.Default.SaveFolder))
			{
				sfd.InitialDirectory = Properties.Settings.Default.SaveFolder;
			}
			else if (Directory.Exists(@"G:\My Documents\SNCT Works\Takayuki Lab\専攻研究\muphic"))
			{
				sfd.InitialDirectory = @"G:\My Documents\SNCT Works\Takayuki Lab\専攻研究\muphic";
			}
			else
			{
				sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}

			// [ファイルの種類]に表示される選択肢を指定する
			sfd.Filter = "アーカイブ ファイル(*.dat)|*.dat|すべてのファイル(*.*)|*.*";

			// [ファイルの種類]ではじめに「アーカイブ ファイル」が選択されているようにする
			sfd.FilterIndex = 1;

			// タイトルを設定する
			sfd.Title = "保存先のアーカイブ ファイルを指定しなさいよっ！";

			// ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			sfd.RestoreDirectory = true;

			// 既に存在するファイル名を指定したとき警告する
			// デフォルトでTrueなので指定する必要はない
			sfd.OverwritePrompt = true;

			// 存在しないパスが指定されたとき警告を表示する
			// デフォルトでTrueなので指定する必要はない
			sfd.CheckPathExists = true;

			// ダイアログを表示する
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				// 保存先のディレクトリ情報を保存
				Properties.Settings.Default.SaveFolder = Path.GetFullPath(Path.GetDirectoryName(sfd.FileName));

				// OKボタンがクリックされたらアーカイブ化
				this.CreateArchive(sfd.FileName);
			}
		}


		/// <summary>
		/// 現在のファイルリストからアーカイブを生成する。
		/// </summary>
		/// <param name="savePath">アーカイブ保存先のパス。</param>
		private void CreateArchive(string savePath)
		{
			// アーカイバが保有するファイルを解放
			if (this.ArchiveControllerPlus != null) this.ArchiveControllerPlus.Dispose();

			// ファイル情報ラベルをクリア
			this.InitializePreview();

			// アーカイブ生成
			this.ArchiveControllerPlus.CreateArchive(savePath, this.FileList);

			// 生成したアーカイブを開き直す
			this.OpenArchive(savePath, false);
		}

	}
}
