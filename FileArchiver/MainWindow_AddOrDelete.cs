using System.IO;
using System.Windows.Forms;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// アーカイブ化を行う複数のファイルを追加する。
		/// </summary>
		/// <param name="files">追加するファイルのパス。</param>
		private void AddFiles(string[] files)
		{
			this.AddFiles(files, false);
		}

		/// <summary>
		/// アーカイブ化を行う複数のファイルを追加する。
		/// </summary>
		/// <param name="files">追加するファイルのパス。</param>
		/// <param name="isArchived">既にアーカイブ化されているファイルの場合は true、それ以外は false。</param>
		private void AddFiles(string[] files, bool isArchived)
		{
			// アーカイブされていないファイルが追加された場合、編集中を表す * をタイトルに加える
			if (!isArchived) this.IsChanged = true;

			for (int i = 0; i < files.Length; i++)
			{
				this.AddFile(files[i], isArchived);
			}
		}


		/// <summary>
		/// アーカイブ化を行うファイルを追加する。
		/// </summary>
		/// <param name="filePath">追加するファイルのファイル名もしくはファイルパス。</param>
		/// <param name="isArchived">既にアーカイブ化されているファイルの場合は true、それ以外は false。</param>
		private void AddFile(string filePath, bool isArchived)
		{
			// ファイルリストに追加
			this.FileList.Add(filePath);

			// アーカイブ済みのファイルの場合は ture、新しく追加するファイルの場合は false を追加
			this.ArchiveControllerPlus.IsArchivedFlags.Add(isArchived);

			// リストボックスにパスを除いたファイル名を追加 (アーカイブ化されていない場合は * を加える)
			this.listBox_FileList.Items.Add((isArchived ? "" : "* ") + Path.GetFileName(filePath) + "\r\n");

			// アーカイブ生成機能の有効 / 無効の切替
			this.ChangeEnabledCreateArchive();

			// 新しく追加するファイルの場合、編集中であることをウィンドウに表示
			if (!isArchived) this.IsChanged = true;
		}


		/// <summary>
		/// 指定したインデックス番号に対応するファイルを削除する。
		/// </summary>
		/// <param name="index"></param>
		private void DeleteFile(int index)
		{
			// アーカイブ化済みファイルを削除する場合、アーカイバに当該番号のファイルの削除を要求
			if (this.ArchiveControllerPlus.IsArchivedFlags[index])
				this.ArchiveControllerPlus.Delete(index);

			// アーカイブ済みでない場合、当該番号のフラグのみ削除
			else this.ArchiveControllerPlus.IsArchivedFlags.RemoveAt(index);

			// ファイルリストとリストボックスから当該番号のファイルの情報を削除
			this.FileList.RemoveAt(index);
			this.listBox_FileList.Items.RemoveAt(index);
			this.listBox_FileList.SelectedIndex = -1;

			// ウィンドウタイトルを編集済みに変更
			this.IsChanged = true;

			// アーカイブ生成機能の有効 / 無効の切替
			this.ChangeEnabledCreateArchive();
		}

	}
}
