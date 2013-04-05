using System;
using System.IO;
using System.Windows.Forms;

namespace FileArchiver
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
			// ウィンドウの位置を記憶する
			Properties.Settings.Default.WindowLocation = this.Location;
			Properties.Settings.Default.Save();
		}

	}
}
