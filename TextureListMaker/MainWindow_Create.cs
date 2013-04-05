using System;
using System.IO;
using System.Windows.Forms;

using Muphic.Manager;
using Muphic.Tools.IO;

namespace TextureListMaker
{
	public partial class MainWindow : Form
	{

		/// <summary>
		/// テクスチャ名リスト生成ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_CreateTextureList_Click(object sender, System.EventArgs e)
		{
			this.SaveTextureListFile();
		}


		/// <summary>
		/// リストボックスに追加されたテクスチャ名リストから、muphic で使用するテクスチャ名リストを生成し、ファイルに保存する。
		/// </summary>
		/// <returns>正常にファイルに保存された場合は true、それ以外は false。</returns>
		private bool SaveTextureListFile()
		{
			// SaveFileDialogクラスのインスタンスを作成
			SaveFileDialog sfd = new SaveFileDialog();

			// はじめのファイル名を指定する
			sfd.FileName = "ConsolidatedImages.texturelist";

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
			sfd.Filter = "テクスチャ名リスト ファイル(*.texturelist)|*.texturelist|すべてのファイル(*.*)|*.*";

			// [ファイルの種類]ではじめに「アーカイブ ファイル」が選択されているようにする
			sfd.FilterIndex = 0;

			// タイトルを設定する
			sfd.Title = "保存先のテクスチャ名リスト ファイルを指定しなさいよっ！";

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

				// OKボタンがクリックされたらテクスチャリスト生成
				return this.SaveTextureListFile(sfd.FileName);
			}

			return false;
		}


		/// <summary>
		/// リストボックスに追加されたテクスチャ名リストから、muphic で使用するテクスチャ名リストを生成し、ファイルに保存する。
		/// </summary>
		/// <returns>正常にファイルに保存された場合は true、それ以外は false。</returns>
		private bool SaveTextureListFile(string savePath)
		{
			TextureFileInfo saveData = new TextureFileInfo();

			foreach (MainWindow.TextureListItem item in this.listBox_TextureList.Items)
			{
				saveData.TextureName.Add(item.TextureName);
				saveData.SourceFileName.Add(item.SourceFileName);
				saveData.SourceRectangle.Add(item.SourceRectangle);
			}

			XmlFileWriter.WriteSaveData<TextureFileInfo>(saveData, false, savePath);

			this.FileList.Clear();
			this.button_CreateTextureList.Enabled = false;
			this.mainMenuItem_File_Save.Enabled = false;

			return true;
		}

	}
}
