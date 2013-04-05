using System;
using System.IO;
using System.Windows.Forms;
using Muphic.Archive;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		// ================================================
		// リストボックスの選択等により発生する処理を記述
		// 注意：フォームのコントロールへの変更は許可しない
		// ================================================

		/// <summary>
		/// リストボックスの要素が選択された際に呼び出される処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBox_FileList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.SelectPreviewFile(this.listBox_FileList.SelectedIndex);
		}


		/// <summary>
		/// ファイルリスト内の指定したインデックス番号のファイルを選択し、ファイル情報の更新とプレビューの表示を行う。
		/// </summary>
		/// <param name="index">選択されたファイルのファイルリスト内のインデックス番号。</param>
		private void SelectPreviewFile(int index)
		{
			// 選択された要素がリスト範囲外だった場合は、プレビューをクリアし終了
			if (index < 0 || index >= this.FileList.Count)
			{
				this.InitializePreview();
				this.mainMenuItem_Edit_DeleteFile.Enabled = false;
				return;
			}

			// 当該ファイルのパスとファイルの種類を取得
			string fileName = this.FileList[index];
			ArchiveFileType fileType = ArchiveFileTypeTools.GetFileType(Path.GetExtension(fileName));

			// アーカイブ済みかを確認
			bool isArchived = this.ArchiveControllerPlus.IsArchivedFlags[index];

			// ファイルサイズを取得 (アーカイブ済みの時のみ  アーカイブされていなければ 0 とする)
			int fileSize = isArchived ? this.ArchiveControllerPlus.ArchivedData[index].Length : 0;

			// プレビューラベル更新
			this.SetPreviewLabels(fileName, fileType, isArchived, fileSize);

			// ファイルが画像データの場合
			if (fileType == ArchiveFileType.Image)			// アーカイブ済みであればバイト配列からプレビュー生成し、
			{												// アーカイブ済みでなければファイルパスからプレビュー生成
				if (isArchived) this.EnabledImagePreview(this.ArchiveControllerPlus.ArchivedData[index]);
				else this.EnabledImagePreview(fileName);
			}

			// ファイルが音声データの場合
			else if (fileType == ArchiveFileType.Sound)		// アーカイブ済みであればバイト配列からプレビュー生成し、
			{												// アーカイブ済みでなければファイルパスからプレビュー生成
				if (isArchived) this.EnabledSoundPreview(this.ArchiveControllerPlus.ArchivedData[index]);
				else this.EnabledSoundPreview(fileName);
			}

			// ファイルがテキストデータの場合
			else if (fileType == ArchiveFileType.Text)		// アーカイブ済みであればバイト配列からプレビュー生成し、
			{												// アーカイブ済みでなければファイルパスからプレビュー生成
				if (isArchived) this.EnabledTextPreview(this.ArchiveControllerPlus.ArchivedData[index]);
				else this.EnabledTextPreview(fileName);
			}

			// ファイルが上記以外 (認識できないデータ) の場合
			else
			{
				this.ChangeEnabledImagePreview(false);
				this.ChangeEnabledSoundPreview(false);
				this.ChangeEnabledTextPreview(false);
			}

			// メニューの削除を有効にする
			this.mainMenuItem_Edit_DeleteFile.Enabled = true;
		}
	}
}
