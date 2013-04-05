using System.IO;
using System.Windows.Forms;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// ファイルをドラッグ＆ドロップする際に呼び出される処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBox_FileList_DragEnter(object sender, DragEventArgs e)
		{
			// このコントロール (listBox) が受け取ることができる操作の種類 (コピーや移動など) を設定する
			// ここでは全て可能となるよう設定しておく
			e.Effect = DragDropEffects.All;
		}


		/// <summary>
		/// ファイルをドラッグ＆ドロップする際に呼び出される処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBox_FileList_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				this.DropFileAndDirectory((string[])(e.Data.GetData(DataFormats.FileDrop)), 10);
			}
		}


		/// <summary>
		/// ドロップされたファイルとフォルダの一覧を解析し、画像名のフルパスを登録する。
		/// </summary>
		/// <param name="dropPathes">ドロップされたファイルとフォルダ</param>
		/// <param name="searchLevel">調べるディレクトリの階層</param>
		public void DropFileAndDirectory(string[] dropPathes, int searchLevel)
		{
			// ドロップ数が0なら何もせず戻る
			if (dropPathes.Length == 0) return;

			foreach (string dropPath in (string[])dropPathes)
			{
				// ディレクトリ名かどうかチェック
				if (Directory.Exists(dropPath))
				{
					// ディレクトリ名なら、中のファイルとディレクトリもチェックする
					DropFileAndDirectory_(dropPath, searchLevel, 1);
				}
				else
				{
					// ディレクトリ名でなければ、ファイル名一覧に登録
					this.AddFile(dropPath, false);
				}
			}
		}


		/// <summary>
		/// ディレクトリの中身をチェックし、ファイルなら登録する。
		/// </summary>
		/// <param name="Path">ディレクトリパス</param>
		/// <param name="confLevel">調べるディレクトリの階層</param>
		/// <param name="nowLevel">現在調べている階層</param>
		private void DropFileAndDirectory_(string dropPath, int searchLevel, int nowLevel)
		{
			// 調べるべき階層より深かった場合は処理せず終了
			if (searchLevel < nowLevel) return;

			// ディレクトリの情報を得る
			DirectoryInfo di = new DirectoryInfo(dropPath);

			// ディレクトリ内の全てのディレクトリの中身をチェックする
			for (int i = 0; i < di.GetDirectories().Length; i++)
			{
				DropFileAndDirectory_(di.GetDirectories()[i].FullName, searchLevel, nowLevel + 1);
			}

			// ディレクトリ内の全てのファイルのパスを管理クラスに登録する。
			for (int i = 0; i < di.GetFiles().Length; i++)
			{
				this.AddFile(di.GetFiles()[i].FullName, false);
			}
		}

	}
}
