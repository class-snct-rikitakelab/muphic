using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;
using Muphic.Archive;

namespace FileArchiver
{
	public partial class MainWindow : Form
	{
		// ================================================================================
		// ファイル情報ラベル及びプレビュー機能の処理を記述する。
		// 注意：ファイル情報表示部のラベル及びプレビュー機能関連のコントロールのみ変更可能
		// ================================================================================


		/// <summary>
		/// ファイル情報ラベル及びプレビューをクリアする。
		/// </summary>
		private void InitializePreview()
		{
			// ファイル情報ラベルクリア
			this.label_FilePath.Text = this.label_Kind.Text = this.label_FileSize.Text = "";
			this.label_IsEncrypte.Visible = false;

			this.ChangeEnabledImagePreview(false);

			this.ChangeEnabledSoundPreview(false);

			this.ChangeEnabledTextPreview(false);
		}


		#region ファイル情報表示部の更新

		/// <summary>
		/// リストボックスで選択されているファイルに基づき、ファイル情報表示部の各ラベルを更新する。
		/// </summary>
		/// <param name="fileName">アーカイブされたファイル名もしくはデータソースのファイルパス。</param>
		/// <param name="fileType">ファイルの種類。</param>
		/// <param name="isArchived">アーカイブされたファイルであれば true、アーカイブされていなければ false。</param>
		/// <param name="fileSize">ファイルサイズ。表示しない場合は 0 を指定する。</param>
		private void SetPreviewLabels(string fileName, ArchiveFileType fileType, bool isArchived, int fileSize)
		{
			// ファイル名、もしくはファイルパスを設定
			this.label_FilePath.Text = fileName;

			// アーカイブ済みであれば (アーカイブ済み) ラベルを追加
			if (isArchived) this.label_FilePath.Text += " (アーカイブ済み)";

			// ファイルの種類を設定
			this.label_Kind.Text = "ファイル形式 : " + ArchiveFileTypeTools.GetFileType(fileType);

			// ファイルサイズが 0 でなれば、バイト単位でファイルサイズを設定
			if (fileSize > 0) this.label_FileSize.Text = "サイズ : " + fileSize.ToString("#,#") + " byte";
			else this.label_FileSize.Text = "";

			// 暗号化されるべきファイルであれば、暗号化ラベル表示
			this.label_IsEncrypte.Visible = ArchiveFileTypeTools.IsEncrypteFileType(fileType);
		}

		#endregion


		#region ファイルの種類

		#endregion


		#region 画像のプレビュー

		/// <summary>
		/// 画像のプレビュー機能を有効にする。
		/// </summary>
		/// <param name="imageSource">表示する画像ソース。</param>
		private void EnabledImagePreview(byte[] imageSource)
		{
			this.pictureBox_ImgeFilePreview.Image = Image.FromStream(new MemoryStream(imageSource));
			this.ChangeEnabledImagePreview(true);
		}

		/// <summary>
		/// 画像のプレビュー機能を有効にする。
		/// </summary>
		/// <param name="filePath">表示する画像のパス。</param>
		private void EnabledImagePreview(string filePath)
		{
			//this.pictureBox_ImgeFilePreview.Image = Image.FromFile(filePath);
			//this.ChangeEnabledImagePreview(true);

			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				this.pictureBox_ImgeFilePreview.Image = Image.FromStream(fs);
				this.ChangeEnabledImagePreview(true);
			}
		}

		/// <summary>
		/// 画像のプレビュー機能を無効化する。
		/// </summary>
		private void DisabledImagePreview()
		{
			if (this.pictureBox_ImgeFilePreview.Image != null)
			{
				this.pictureBox_ImgeFilePreview.Image.Dispose();
				this.pictureBox_ImgeFilePreview.Image = new Bitmap(1, 1);
				this.pictureBox_ImgeFilePreview.Invalidate();
			}
		}

		/// <summary>
		/// 画像のプレビュー機能の有効 / 無効を切り替える。
		/// </summary>
		/// <param name="enabled">有効にする場合は true、それ以外は false。</param>
		private void ChangeEnabledImagePreview(bool enabled)
		{
			if (enabled)
			{
				this.ChangeEnabledSoundPreview(false);
				this.ChangeEnabledTextPreview(false);
			}
			else
			{
				this.DisabledImagePreview();
			}

			this.pictureBox_ImgeFilePreview.Visible = enabled;
		}

		#endregion


		#region 音声のプレビュー

		/// <summary>
		/// 音声ファイルのサンプル再生の為のプレイヤー。
		/// </summary>
		private SoundPlayer SoundPlayer { get; set; }


		/// <summary>
		/// 音声のプレビュー機能を有効にする。
		/// </summary>
		/// <param name="soundSource">再生する音声ソース。</param>
		private void EnabledSoundPreview(byte[] soundSource)
		{
			this.SoundPlayer = new SoundPlayer(new MemoryStream(soundSource));
			this.ChangeEnabledSoundPreview(true);
		}

		/// <summary>
		/// 音声のプレビュー機能を有効にする。
		/// </summary>
		/// <param name="filePath">再生する音声のパス。</param>
		private void EnabledSoundPreview(string filePath)
		{
			this.SoundPlayer = new SoundPlayer(filePath);
			this.ChangeEnabledSoundPreview(true);

			//using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			//{
			//    this.SoundPlayer = new SoundPlayer(fs);
			//    this.ChangeEnabledSoundPreview(true);
			//}
		}

		/// <summary>
		/// 音声のプレビュー機能を無効化する。
		/// </summary>
		private void DisabledSoundPreview()
		{
			if (this.SoundPlayer != null)
			{
				this.SoundPlayer = null;
			}
		}

		/// <summary>
		/// 音声のプレビュー機能の有効 / 無効を切り替える。
		/// </summary>
		/// <param name="enabled">有効にする場合は true、それ以外は false。</param>
		private void ChangeEnabledSoundPreview(bool enabled)
		{
			this.button_Play.Visible = enabled;
			this.button_Stop.Visible = enabled;

			if (enabled)
			{
				this.ChangeEnabledImagePreview(false);
				this.ChangeEnabledTextPreview(false);
			}
			else
			{
				this.DisabledSoundPreview();
			}
		}

		#endregion


		#region テキストのプレビュー

		/// <summary>
		/// テキストのプレビュー機能を有効にする。
		/// </summary>
		/// <param name="textSource">表示するテキストソース。</param>
		private void EnabledTextPreview(byte[] textSource)
		{
			this.label_TextPreview.Text = new StreamReader(new MemoryStream(textSource), Encoding.GetEncoding("Shift_JIS")).ReadToEnd();
			this.ChangeEnabledTextPreview(true);
		}

		/// <summary>
		/// テキストのプレビュー機能を有効にする。
		/// </summary>
		/// <param name="filePath">表示するテキストのパス。</param>
		private void EnabledTextPreview(string filePath)
		{
			using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS")))
			{
				this.label_TextPreview.Text = sr.ReadToEnd();
				this.ChangeEnabledTextPreview(true);
			}
		}

		/// <summary>
		/// テキストのプレビュー機能を無効化する。
		/// </summary>
		private void DisabledTextPreview()
		{
			this.label_TextPreview.Text = "";
		}

		/// <summary>
		/// テキストのプレビュー機能の有効 / 無効を切り替える。
		/// </summary>
		/// <param name="enabled">有効にする場合は true、それ以外は false。</param>
		private void ChangeEnabledTextPreview(bool enabled)
		{
			this.label_TextPreview.Visible = enabled;

			if (enabled)
			{
				this.ChangeEnabledImagePreview(false);
				this.ChangeEnabledSoundPreview(false);
			}
		}

		#endregion

	}
}
