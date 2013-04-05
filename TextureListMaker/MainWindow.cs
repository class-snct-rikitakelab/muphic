using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TextureListMaker
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// テクスチャ名リスト作成ツールのメインウィンドウの新しいインスタンスを初期化する。
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			this.FileList = new List<string>();
			this.RegistedTextureNameList = new List<string>();

			// 各イベントの登録 (フォームデザイナに任せると partial クラス無視して新しく作ってくれやがるので)
			this.listBox_TextureList.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox_TextureList_DragDrop);
			this.listBox_TextureList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox_TextureList_DragEnter);
			this.listBox_TextureList.SelectedIndexChanged += new System.EventHandler(this.listBox_TextureList_SelectedIndexChanged);
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			this.checkBox_EnabledPreview.CheckedChanged += new System.EventHandler(this.checkBox_EnabledPreview_CheckedChanged);
			this.pictureBox_Preview.Paint += new PaintEventHandler(this.pictureBox_Preview_Paint);

			this.checkBox_EnabledPreview.Checked = true;
			this.button_Add.Enabled = false;
			this.Initialize();

			this.StartPosition = FormStartPosition.Manual;
			this.Location = Properties.Settings.Default.WindowLocation;

			this.textBox_AutoAddFile.Text = Properties.Settings.Default.AutoAddFolder;
		}


		/// <summary>
		/// テクスチャ名リスト生成ツールのメインウィンドウの初期化を行う。
		/// </summary>
		private void Initialize()
		{
			// 各リストの初期化
			this.listBox_TextureList.Items.Clear();
			this.FileList.Clear();
			this.RegistedTextureNameList.Clear();

			// 各テキストボックスの初期化
			this.textBox_TextureName.Text = "";
			this.textBox_SorceFileName.Text = "";
			this.textBox_LocationX.Text = "";
			this.textBox_LocationY.Text = "";
			this.textBox_SizeWidth.Text = "";
			this.textBox_SizeHeight.Text = "";

			// 初期状態では生成ボタンを無効化
			this.button_CreateTextureList.Enabled = false;
			this.mainMenuItem_File_Save.Enabled = false;

			this.SelectedIndexChanged();
		}


		/// <summary>
		/// テクスチャ名リストファイルパスのリスト。
		/// </summary>
		private List<string> FileList { get; set; }

		/// <summary>
		/// 登録されたテクスチャ名のリスト。重複したテクスチャ名を防ぐため。
		/// </summary>
		private List<string> RegistedTextureNameList { get; set; }


		/// <summary>
		/// テクスチャ名リストに追加する、テクスチャ名とそのテクスチャが含まれるソース画像ファイルの情報を保持するクラス。
		/// </summary>
		private class TextureListItem
		{
			/// <summary>
			/// テクスチャのソース画像ファイル名。
			/// </summary>
			public string SourceFileName { get; set; }

			/// <summary>
			/// テクスチャ名。
			/// </summary>
			public string TextureName { get; set; }

			/// <summary>
			/// テクスチャのソース画像内での位置とサイズ。
			/// </summary>
			public Rectangle SourceRectangle { get; set; }


			/// <summary>
			/// テクスチャ名リストに追加するテクスチャ名の新しいインスタンスを初期化する。
			/// </summary>
			/// <param name="textureName"></param>
			/// <param name="sourceFileName"></param>
			/// <param name="sourceRectangle"></param>
			public TextureListItem(string textureName, string sourceFileName, Rectangle sourceRectangle)
			{
				this.TextureName = textureName;
				this.SourceFileName = sourceFileName;
				this.SourceRectangle = sourceRectangle;
			}


			/// <summary>
			/// 現在の TextureName を表す System.String を返す。
			/// </summary>
			/// <returns>現在の TextureName を表す System.String。</returns>
			public override string ToString()
			{
				return this.TextureName;
			}
		}


		/// <summary>
		/// ファイルリストをクリアして良いかをユーザーに尋ねる。
		/// </summary>
		/// <returns>クリアして良い場合は true、それ以外は false。</returns>
		public bool ConfirmInitialize()
		{
			if (this.listBox_TextureList.Items == null || this.listBox_TextureList.Items.Count <= 0) return true;

			return (MessageBox.Show(
						this,
						"現在のテクスチャ名 リストはクリアされますわ。よろしいですの？",
						"リストが消えますのよ",
						MessageBoxButtons.OKCancel
					) == DialogResult.OK);
		}

	}
}
