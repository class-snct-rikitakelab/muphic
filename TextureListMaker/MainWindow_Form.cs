using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TextureListMaker
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// フォームが閉じられる際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ウィンドウ位置の保存
			Properties.Settings.Default.WindowLocation = this.Location;

			// 設定の書き込み
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// 自動読込を行う。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_AutoAddFile_Click(object sender, System.EventArgs e)
		{
			if (Directory.Exists(this.textBox_AutoAddFile.Text))
			{
				// 設定された自動読込対象のディレクトリから処理するファイル名を取得 (下位層は処理しない)
				this.DropFileAndDirectory(
					Directory.GetFiles(this.textBox_AutoAddFile.Text, "*.txt", SearchOption.TopDirectoryOnly),
					1
				);

				// リスト生成
				this.StartBackgroundWorker(this.FileList);

				// 自動読込先のディレクトリを保存
				Properties.Settings.Default.AutoAddFolder = this.textBox_AutoAddFile.Text;
			}
		}
	}
}
