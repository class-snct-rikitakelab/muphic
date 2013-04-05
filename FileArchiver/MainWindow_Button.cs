using System;
using System.IO;
using System.Windows.Forms;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// アーカイブ生成 ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_create_Click(object sender, EventArgs e)
		{
			// アーカイブ生成
			this.CreateArchive();
		}

		/// <summary>
		/// 再生 ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Play_Click(object sender, EventArgs e)
		{
			// サウンドを再生
			this.SoundPlayer.Play();
		}

		/// <summary>
		/// 停止 ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Stop_Click(object sender, EventArgs e)
		{
			// サウンドを停止
			this.SoundPlayer.Stop();
		}



		/// <summary>
		/// アーカイブ済みまたはアーカイブ予約ファイル数に応じ、アーカイブ生成機能の有効 / 無効を切り替える。
		/// </summary>
		private void ChangeEnabledCreateArchive()
		{
			bool enabled = (this.FileList.Count > 0);

			this.button_CreateArchive.Enabled = enabled;
			this.mainMenuItem_File_CreateArchive.Enabled = enabled;
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
					Directory.GetFiles(this.textBox_AutoAddFile.Text, "*", SearchOption.TopDirectoryOnly),
					1
				);
				
				this.DropFileAndDirectory(
					Directory.GetDirectories(this.textBox_AutoAddFile.Text, "*", SearchOption.TopDirectoryOnly),
					10
				);

				// 自動読込先のディレクトリを保存
				Properties.Settings.Default.AutoAddFolder = this.textBox_AutoAddFile.Text;
			}
		}
	}
}
