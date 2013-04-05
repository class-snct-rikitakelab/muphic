using System;
using System.IO;
using System.Windows.Forms;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{

		#region ファイル(F) メニュー

		/// <summary>
		/// ファイル(F) メニューの 終了(X) がクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_File_Exit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// ファイル(F) メニューの 名前を付けて保存(S) がクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_File_CreateArchive_Click(object sender, EventArgs e)
		{
			this.CreateArchive();
		}

		/// <summary>
		/// ファイル(F) メニューの 新規作成(N) がクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_File_NewArchive_Click(object sender, EventArgs e)
		{
			this.InitializeArchive(true);
		}

		/// <summary>
		/// ファイル(F) メニューの 開く(O) がクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_File_ReadArchive_Click(object sender, EventArgs e)
		{
			this.OpenArchive();
		}

		#endregion


		#region 編集(E) メニュー

		/// <summary>
		/// 編集(E) メニューの ファイルを追加(A) がクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_Edit_AddFile_Click(object sender, System.EventArgs e)
		{
			// OpenFileDialogクラスのインスタンスを作成
			OpenFileDialog ofd = new OpenFileDialog();

			// はじめのファイル名を指定する
			// はじめに「ファイル名」で表示される文字列を指定する
			ofd.FileName = "*.*";

			// はじめに表示されるフォルダを指定する
			// 指定しない（空の文字列）の時は、現在のディレクトリが表示される
			if (Directory.Exists(@"G:\My Documents\SNCT Works\Takayuki Lab\専攻研究\muphic"))
				ofd.InitialDirectory = @"G:\My Documents\SNCT Works\Takayuki Lab\専攻研究\muphic";
			else
				ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			// [ファイルの種類]に表示される選択肢を指定する
			// 指定しないとすべてのファイルが表示される
			ofd.Filter = "画像ファイル(*.jpg;*.png;*.bmp;*.gif)|*.jpg;*.png;*.bmp;*.gif|音声ファイル(*.wav)|*.wav|すべてのファイル(*.*)|*.*";

			// [ファイルの種類]ではじめに
			// 「すべてのファイル」が選択されているようにする
			ofd.FilterIndex = 3;

			// タイトルを設定する
			ofd.Title = "追加するファイルを…選択…してほしいな… ///";

			// ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			ofd.RestoreDirectory = true;

			// 存在しないファイルの名前が指定されたとき警告を表示する
			// デフォルトでTrueなので指定する必要はない
			ofd.CheckFileExists = true;

			// 存在しないパスが指定されたとき警告を表示する
			// デフォルトでTrueなので指定する必要はない
			ofd.CheckPathExists = true;

			// 複数選択を可能にする
			ofd.Multiselect = true;

			// ダイアログを表示する
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				// OK ボタンがクリックされたらファイル追加
				this.AddFiles(ofd.FileNames, false);
			}
		}


		/// <summary>
		/// 編集(E) メニューの ファイルを削除(D) がクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_Edit_DeleteFile_Click(object sender, System.EventArgs e)
		{
			this.DeleteFile(this.listBox_FileList.SelectedIndex);
		}

		#endregion


		#region ヘルプ(H) メニュー

		/// <summary>
		/// ヘルプ(H) メニューの バージョン情報(A) がクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_Help_VersionInfo_Click(object sender, System.EventArgs e)
		{
			using (VersionInfoWindow versionInfoWindow = new VersionInfoWindow())
			{
				versionInfoWindow.ShowDialog();
			}
		}

		#endregion

	}
}
