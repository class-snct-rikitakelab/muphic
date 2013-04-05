using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextureListMaker
{
	public partial class MainWindow : Form
	{

		/// <summary>
		/// リストボックスの要素が選択された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBox_TextureList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SelectedIndexChanged();
		}

		/// <summary>
		/// リストボックスで選択されている要素の変更に応じ、各コントロールの状態を変更する。
		/// </summary>
		private void SelectedIndexChanged()
		{
			if (this.EnabledCurrentSelected)
			{
				this.textBox_TextureName.Text = this.CurrentSelectedItem.TextureName;
				this.textBox_SorceFileName.Text = this.CurrentSelectedItem.SourceFileName;
				this.textBox_LocationX.Text = this.CurrentSelectedItem.SourceRectangle.X.ToString();
				this.textBox_LocationY.Text = this.CurrentSelectedItem.SourceRectangle.Y.ToString();
				this.textBox_SizeWidth.Text = this.CurrentSelectedItem.SourceRectangle.Width.ToString();
				this.textBox_SizeHeight.Text = this.CurrentSelectedItem.SourceRectangle.Height.ToString();
			}

			this.button_Update.Enabled = this.EnabledCurrentSelected;
			this.button_Delete.Enabled = this.EnabledCurrentSelected;

			this.pictureBox_Preview.Invalidate();
		}


		/// <summary>
		/// リストボックスで選択されている要素が有効 (リスト内の要素番号が選択されている状態) かどうかを示す値を取得する。
		/// </summary>
		private bool EnabledCurrentSelected
		{
			get
			{
				return (
					this.listBox_TextureList.Items.Count > 0 &&
					this.listBox_TextureList.SelectedIndex >= 0 &&
					this.listBox_TextureList.SelectedIndex < this.listBox_TextureList.Items.Count
				);
			}
		}


		/// <summary>
		/// リストボックスで選択されているテクスチャ名とそのソース画像ファイルの情報を取得する。
		/// </summary>
		private TextureListItem CurrentSelectedItem
		{
			get { return (TextureListItem)(this.listBox_TextureList.SelectedItem); }
		}


		/// <summary>
		/// プレビュー表示のチェックボックスが変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBox_EnabledPreview_CheckedChanged(object sender, System.EventArgs e)
		{
			this.textBox_PreviewSourceDirectory.Enabled = this.checkBox_EnabledPreview.Checked;
			this.label_PreviewSourceDirectory.Enabled = this.checkBox_EnabledPreview.Checked;
			this.pictureBox_Preview.Invalidate();
		}


		/// <summary>
		/// プレビュー表示を更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_Preview_Paint(object sender, PaintEventArgs e)
		{
			this.label_SourceFileNotFound.Visible = false;

			// プレビュー表示が有効かつリストから選択されたテクスチャ名が有効な場合のみ実行
			if (this.checkBox_EnabledPreview.Checked && this.EnabledCurrentSelected)
			{
				// ファイルパス生成  テキストボックスで指定されたディレクトリとテクスチャのソース画像ファイル名を結合
				string filePath = Path.Combine(this.textBox_PreviewSourceDirectory.Text, this.CurrentSelectedItem.SourceFileName);

				// 生成されたパスのファイルが存在した場合のみ、そのソース画像の指定領域を描画
				if (File.Exists(filePath))
				{
					e.Graphics.DrawImage(
						Image.FromFile(filePath),
						new Rectangle(new Point(0, 0), this.CurrentSelectedItem.SourceRectangle.Size),
						this.CurrentSelectedItem.SourceRectangle,
						GraphicsUnit.Pixel
					);

					return;
				}
				else
				{
					this.label_SourceFileNotFound.Visible = true;
				}
			}

			// 上記条件を満たさない場合、ダミー画像描画
			e.Graphics.DrawImage(new Bitmap(5, 5), 0, 0);
		}
	}
}