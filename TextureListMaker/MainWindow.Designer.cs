namespace TextureListMaker
{
	partial class MainWindow
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.mainMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_File_CreateNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_File_Open = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_File_Separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuItem_File_Save = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_File_Separator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuItem_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Edit_AddFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Help_VersionInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.label_TextureName = new System.Windows.Forms.Label();
			this.textBox_TextureName = new System.Windows.Forms.TextBox();
			this.textBox_SorceFileName = new System.Windows.Forms.TextBox();
			this.label_SorceFileName = new System.Windows.Forms.Label();
			this.label_SizeComma = new System.Windows.Forms.Label();
			this.label_LocationComma = new System.Windows.Forms.Label();
			this.label_Size = new System.Windows.Forms.Label();
			this.textBox_SizeHeight = new System.Windows.Forms.TextBox();
			this.textBox_SizeWidth = new System.Windows.Forms.TextBox();
			this.textBox_LocationY = new System.Windows.Forms.TextBox();
			this.textBox_LocationX = new System.Windows.Forms.TextBox();
			this.label_Location = new System.Windows.Forms.Label();
			this.button_CreateTextureList = new System.Windows.Forms.Button();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.checkBox_EnabledPreview = new System.Windows.Forms.CheckBox();
			this.pictureBox_Preview = new System.Windows.Forms.PictureBox();
			this.button_Update = new System.Windows.Forms.Button();
			this.button_Delete = new System.Windows.Forms.Button();
			this.textBox_PreviewSourceDirectory = new System.Windows.Forms.TextBox();
			this.label_SourceFileNotFound = new System.Windows.Forms.Label();
			this.label_PreviewSourceDirectory = new System.Windows.Forms.Label();
			this.groupBox_Preview = new System.Windows.Forms.GroupBox();
			this.button_Add = new System.Windows.Forms.Button();
			this.listBox_TextureList = new TextureListMaker.DragDropListBox();
			this.button_AutoAddFile = new System.Windows.Forms.Button();
			this.textBox_AutoAddFile = new System.Windows.Forms.TextBox();
			this.mainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).BeginInit();
			this.groupBox_Preview.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuItem_File,
            this.mainMenuItem_Edit,
            this.mainMenuItem_Help});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(884, 26);
			this.mainMenu.TabIndex = 1;
			this.mainMenu.Text = "menuStrip1";
			// 
			// mainMenuItem_File
			// 
			this.mainMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuItem_File_CreateNew,
            this.mainMenuItem_File_Open,
            this.mainMenuItem_File_Separator1,
            this.mainMenuItem_File_Save,
            this.mainMenuItem_File_Separator2,
            this.mainMenuItem_File_Exit});
			this.mainMenuItem_File.Name = "mainMenuItem_File";
			this.mainMenuItem_File.Size = new System.Drawing.Size(85, 22);
			this.mainMenuItem_File.Text = "ファイル(&F)";
			// 
			// mainMenuItem_File_CreateNew
			// 
			this.mainMenuItem_File_CreateNew.Name = "mainMenuItem_File_CreateNew";
			this.mainMenuItem_File_CreateNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mainMenuItem_File_CreateNew.Size = new System.Drawing.Size(237, 22);
			this.mainMenuItem_File_CreateNew.Text = "新規作成(&N)";
			this.mainMenuItem_File_CreateNew.Click += new System.EventHandler(this.mainMenuItem_File_CreateNew_Click);
			// 
			// mainMenuItem_File_Open
			// 
			this.mainMenuItem_File_Open.Name = "mainMenuItem_File_Open";
			this.mainMenuItem_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mainMenuItem_File_Open.Size = new System.Drawing.Size(237, 22);
			this.mainMenuItem_File_Open.Text = "開く(&O)";
			this.mainMenuItem_File_Open.Click += new System.EventHandler(this.mainMenuItem_File_Open_Click);
			// 
			// mainMenuItem_File_Separator1
			// 
			this.mainMenuItem_File_Separator1.Name = "mainMenuItem_File_Separator1";
			this.mainMenuItem_File_Separator1.Size = new System.Drawing.Size(234, 6);
			// 
			// mainMenuItem_File_Save
			// 
			this.mainMenuItem_File_Save.Name = "mainMenuItem_File_Save";
			this.mainMenuItem_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mainMenuItem_File_Save.Size = new System.Drawing.Size(237, 22);
			this.mainMenuItem_File_Save.Text = "名前を付けて保存(&S)";
			this.mainMenuItem_File_Save.Click += new System.EventHandler(this.mainMenuItem_File_Save_Click);
			// 
			// mainMenuItem_File_Separator2
			// 
			this.mainMenuItem_File_Separator2.Name = "mainMenuItem_File_Separator2";
			this.mainMenuItem_File_Separator2.Size = new System.Drawing.Size(234, 6);
			// 
			// mainMenuItem_File_Exit
			// 
			this.mainMenuItem_File_Exit.Name = "mainMenuItem_File_Exit";
			this.mainMenuItem_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.mainMenuItem_File_Exit.Size = new System.Drawing.Size(237, 22);
			this.mainMenuItem_File_Exit.Text = "終了(&X)";
			this.mainMenuItem_File_Exit.Click += new System.EventHandler(this.mainMenuItem_File_Exit_Click);
			// 
			// mainMenuItem_Edit
			// 
			this.mainMenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuItem_Edit_AddFile});
			this.mainMenuItem_Edit.Name = "mainMenuItem_Edit";
			this.mainMenuItem_Edit.Size = new System.Drawing.Size(61, 22);
			this.mainMenuItem_Edit.Text = "編集(&E)";
			// 
			// mainMenuItem_Edit_AddFile
			// 
			this.mainMenuItem_Edit_AddFile.Name = "mainMenuItem_Edit_AddFile";
			this.mainMenuItem_Edit_AddFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.mainMenuItem_Edit_AddFile.Size = new System.Drawing.Size(225, 22);
			this.mainMenuItem_Edit_AddFile.Text = "ファイルの追加(&A)";
			this.mainMenuItem_Edit_AddFile.Click += new System.EventHandler(this.mainMenuItem_Edit_AddFile_Click);
			// 
			// mainMenuItem_Help
			// 
			this.mainMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuItem_Help_VersionInfo});
			this.mainMenuItem_Help.Name = "mainMenuItem_Help";
			this.mainMenuItem_Help.Size = new System.Drawing.Size(75, 22);
			this.mainMenuItem_Help.Text = "ヘルプ(&H)";
			// 
			// mainMenuItem_Help_VersionInfo
			// 
			this.mainMenuItem_Help_VersionInfo.Name = "mainMenuItem_Help_VersionInfo";
			this.mainMenuItem_Help_VersionInfo.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.mainMenuItem_Help_VersionInfo.Size = new System.Drawing.Size(200, 22);
			this.mainMenuItem_Help_VersionInfo.Text = "バージョン情報(&A)";
			this.mainMenuItem_Help_VersionInfo.Click += new System.EventHandler(this.mainMenuItem_Help_VersionInfo_Click);
			// 
			// label_TextureName
			// 
			this.label_TextureName.AutoSize = true;
			this.label_TextureName.Location = new System.Drawing.Point(468, 37);
			this.label_TextureName.Name = "label_TextureName";
			this.label_TextureName.Size = new System.Drawing.Size(80, 18);
			this.label_TextureName.TabIndex = 2;
			this.label_TextureName.Text = "テクスチャ名";
			// 
			// textBox_TextureName
			// 
			this.textBox_TextureName.Location = new System.Drawing.Point(468, 58);
			this.textBox_TextureName.Name = "textBox_TextureName";
			this.textBox_TextureName.Size = new System.Drawing.Size(404, 25);
			this.textBox_TextureName.TabIndex = 3;
			// 
			// textBox_SorceFileName
			// 
			this.textBox_SorceFileName.Location = new System.Drawing.Point(468, 117);
			this.textBox_SorceFileName.Name = "textBox_SorceFileName";
			this.textBox_SorceFileName.Size = new System.Drawing.Size(404, 25);
			this.textBox_SorceFileName.TabIndex = 5;
			// 
			// label_SorceFileName
			// 
			this.label_SorceFileName.AutoSize = true;
			this.label_SorceFileName.Location = new System.Drawing.Point(468, 96);
			this.label_SorceFileName.Name = "label_SorceFileName";
			this.label_SorceFileName.Size = new System.Drawing.Size(128, 18);
			this.label_SorceFileName.TabIndex = 4;
			this.label_SorceFileName.Text = "ソース画像ファイル名";
			// 
			// label_SizeComma
			// 
			this.label_SizeComma.AutoSize = true;
			this.label_SizeComma.Location = new System.Drawing.Point(780, 174);
			this.label_SizeComma.Name = "label_SizeComma";
			this.label_SizeComma.Size = new System.Drawing.Size(12, 18);
			this.label_SizeComma.TabIndex = 21;
			this.label_SizeComma.Text = ",";
			// 
			// label_LocationComma
			// 
			this.label_LocationComma.AutoSize = true;
			this.label_LocationComma.Location = new System.Drawing.Point(585, 174);
			this.label_LocationComma.Name = "label_LocationComma";
			this.label_LocationComma.Size = new System.Drawing.Size(12, 18);
			this.label_LocationComma.TabIndex = 20;
			this.label_LocationComma.Text = ",";
			// 
			// label_Size
			// 
			this.label_Size.AutoSize = true;
			this.label_Size.Location = new System.Drawing.Point(695, 150);
			this.label_Size.Name = "label_Size";
			this.label_Size.Size = new System.Drawing.Size(138, 18);
			this.label_Size.TabIndex = 19;
			this.label_Size.Text = "サイズ (Width, Height)";
			// 
			// textBox_SizeHeight
			// 
			this.textBox_SizeHeight.Location = new System.Drawing.Point(792, 171);
			this.textBox_SizeHeight.Name = "textBox_SizeHeight";
			this.textBox_SizeHeight.Size = new System.Drawing.Size(80, 25);
			this.textBox_SizeHeight.TabIndex = 18;
			this.textBox_SizeHeight.WordWrap = false;
			// 
			// textBox_SizeWidth
			// 
			this.textBox_SizeWidth.Location = new System.Drawing.Point(698, 171);
			this.textBox_SizeWidth.Name = "textBox_SizeWidth";
			this.textBox_SizeWidth.Size = new System.Drawing.Size(80, 25);
			this.textBox_SizeWidth.TabIndex = 17;
			this.textBox_SizeWidth.WordWrap = false;
			// 
			// textBox_LocationY
			// 
			this.textBox_LocationY.Location = new System.Drawing.Point(597, 171);
			this.textBox_LocationY.Name = "textBox_LocationY";
			this.textBox_LocationY.Size = new System.Drawing.Size(80, 25);
			this.textBox_LocationY.TabIndex = 16;
			this.textBox_LocationY.WordWrap = false;
			// 
			// textBox_LocationX
			// 
			this.textBox_LocationX.Location = new System.Drawing.Point(503, 171);
			this.textBox_LocationX.Name = "textBox_LocationX";
			this.textBox_LocationX.Size = new System.Drawing.Size(80, 25);
			this.textBox_LocationX.TabIndex = 15;
			this.textBox_LocationX.WordWrap = false;
			// 
			// label_Location
			// 
			this.label_Location.AutoSize = true;
			this.label_Location.Location = new System.Drawing.Point(503, 150);
			this.label_Location.Name = "label_Location";
			this.label_Location.Size = new System.Drawing.Size(70, 18);
			this.label_Location.TabIndex = 14;
			this.label_Location.Text = "位置 (X, Y)";
			// 
			// button_CreateTextureList
			// 
			this.button_CreateTextureList.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button_CreateTextureList.Location = new System.Drawing.Point(12, 614);
			this.button_CreateTextureList.Name = "button_CreateTextureList";
			this.button_CreateTextureList.Size = new System.Drawing.Size(254, 37);
			this.button_CreateTextureList.TabIndex = 22;
			this.button_CreateTextureList.Text = "テクスチャ名リスト生成";
			this.button_CreateTextureList.UseVisualStyleBackColor = true;
			this.button_CreateTextureList.Click += new System.EventHandler(this.button_CreateTextureList_Click);
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			// 
			// checkBox_EnabledPreview
			// 
			this.checkBox_EnabledPreview.AutoSize = true;
			this.checkBox_EnabledPreview.Location = new System.Drawing.Point(6, 24);
			this.checkBox_EnabledPreview.Name = "checkBox_EnabledPreview";
			this.checkBox_EnabledPreview.Size = new System.Drawing.Size(147, 22);
			this.checkBox_EnabledPreview.TabIndex = 23;
			this.checkBox_EnabledPreview.Text = "プレビュー表示を行う";
			this.checkBox_EnabledPreview.UseVisualStyleBackColor = true;
			this.checkBox_EnabledPreview.CheckedChanged += new System.EventHandler(this.checkBox_EnabledPreview_CheckedChanged);
			// 
			// pictureBox_Preview
			// 
			this.pictureBox_Preview.Location = new System.Drawing.Point(6, 83);
			this.pictureBox_Preview.Name = "pictureBox_Preview";
			this.pictureBox_Preview.Size = new System.Drawing.Size(389, 267);
			this.pictureBox_Preview.TabIndex = 24;
			this.pictureBox_Preview.TabStop = false;
			// 
			// button_Update
			// 
			this.button_Update.Location = new System.Drawing.Point(706, 207);
			this.button_Update.Name = "button_Update";
			this.button_Update.Size = new System.Drawing.Size(80, 30);
			this.button_Update.TabIndex = 25;
			this.button_Update.Text = "更新";
			this.button_Update.UseVisualStyleBackColor = true;
			// 
			// button_Delete
			// 
			this.button_Delete.Location = new System.Drawing.Point(792, 207);
			this.button_Delete.Name = "button_Delete";
			this.button_Delete.Size = new System.Drawing.Size(80, 30);
			this.button_Delete.TabIndex = 26;
			this.button_Delete.Text = "削除";
			this.button_Delete.UseVisualStyleBackColor = true;
			// 
			// textBox_PreviewSourceDirectory
			// 
			this.textBox_PreviewSourceDirectory.Location = new System.Drawing.Point(6, 52);
			this.textBox_PreviewSourceDirectory.Name = "textBox_PreviewSourceDirectory";
			this.textBox_PreviewSourceDirectory.Size = new System.Drawing.Size(389, 25);
			this.textBox_PreviewSourceDirectory.TabIndex = 27;
			this.textBox_PreviewSourceDirectory.Text = "G:\\My Documents\\SNCT Works\\Takayuki Lab\\専攻研究\\muphic\\画像／画面設計\\アーカイブソース\\Images";
			// 
			// label_SourceFileNotFound
			// 
			this.label_SourceFileNotFound.AutoSize = true;
			this.label_SourceFileNotFound.Location = new System.Drawing.Point(6, 83);
			this.label_SourceFileNotFound.Name = "label_SourceFileNotFound";
			this.label_SourceFileNotFound.Size = new System.Drawing.Size(96, 18);
			this.label_SourceFileNotFound.TabIndex = 28;
			this.label_SourceFileNotFound.Text = "File Not Found.";
			// 
			// label_PreviewSourceDirectory
			// 
			this.label_PreviewSourceDirectory.AutoSize = true;
			this.label_PreviewSourceDirectory.Location = new System.Drawing.Point(267, 25);
			this.label_PreviewSourceDirectory.Name = "label_PreviewSourceDirectory";
			this.label_PreviewSourceDirectory.Size = new System.Drawing.Size(128, 18);
			this.label_PreviewSourceDirectory.TabIndex = 29;
			this.label_PreviewSourceDirectory.Text = "↓ソース画像フォルダ";
			// 
			// groupBox_Preview
			// 
			this.groupBox_Preview.Controls.Add(this.label_SourceFileNotFound);
			this.groupBox_Preview.Controls.Add(this.checkBox_EnabledPreview);
			this.groupBox_Preview.Controls.Add(this.label_PreviewSourceDirectory);
			this.groupBox_Preview.Controls.Add(this.pictureBox_Preview);
			this.groupBox_Preview.Controls.Add(this.textBox_PreviewSourceDirectory);
			this.groupBox_Preview.Location = new System.Drawing.Point(471, 245);
			this.groupBox_Preview.Name = "groupBox_Preview";
			this.groupBox_Preview.Size = new System.Drawing.Size(401, 356);
			this.groupBox_Preview.TabIndex = 30;
			this.groupBox_Preview.TabStop = false;
			this.groupBox_Preview.Text = "プレビュー";
			// 
			// button_Add
			// 
			this.button_Add.Location = new System.Drawing.Point(620, 207);
			this.button_Add.Name = "button_Add";
			this.button_Add.Size = new System.Drawing.Size(80, 30);
			this.button_Add.TabIndex = 31;
			this.button_Add.Text = "追加";
			this.button_Add.UseVisualStyleBackColor = true;
			// 
			// listBox_TextureList
			// 
			this.listBox_TextureList.AllowDrop = true;
			this.listBox_TextureList.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listBox_TextureList.FormattingEnabled = true;
			this.listBox_TextureList.HorizontalScrollbar = true;
			this.listBox_TextureList.ItemHeight = 20;
			this.listBox_TextureList.Location = new System.Drawing.Point(12, 37);
			this.listBox_TextureList.Name = "listBox_TextureList";
			this.listBox_TextureList.ScrollAlwaysVisible = true;
			this.listBox_TextureList.Size = new System.Drawing.Size(450, 564);
			this.listBox_TextureList.TabIndex = 0;
			// 
			// button_AutoAddFile
			// 
			this.button_AutoAddFile.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button_AutoAddFile.Location = new System.Drawing.Point(272, 625);
			this.button_AutoAddFile.Name = "button_AutoAddFile";
			this.button_AutoAddFile.Size = new System.Drawing.Size(120, 27);
			this.button_AutoAddFile.TabIndex = 32;
			this.button_AutoAddFile.Text = "自動読込";
			this.button_AutoAddFile.UseVisualStyleBackColor = true;
			this.button_AutoAddFile.Click += new System.EventHandler(this.button_AutoAddFile_Click);
			// 
			// textBox_AutoAddFile
			// 
			this.textBox_AutoAddFile.Location = new System.Drawing.Point(398, 626);
			this.textBox_AutoAddFile.Name = "textBox_AutoAddFile";
			this.textBox_AutoAddFile.Size = new System.Drawing.Size(474, 25);
			this.textBox_AutoAddFile.TabIndex = 33;
			this.textBox_AutoAddFile.Text = "G:\\My Documents\\SNCT Works\\Takayuki Lab\\専攻研究\\muphic\\画像／画面設計\\新システム統合画像（Photoshop）";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 663);
			this.Controls.Add(this.textBox_AutoAddFile);
			this.Controls.Add(this.button_AutoAddFile);
			this.Controls.Add(this.button_Add);
			this.Controls.Add(this.groupBox_Preview);
			this.Controls.Add(this.button_Delete);
			this.Controls.Add(this.button_Update);
			this.Controls.Add(this.button_CreateTextureList);
			this.Controls.Add(this.label_SizeComma);
			this.Controls.Add(this.label_LocationComma);
			this.Controls.Add(this.label_Size);
			this.Controls.Add(this.textBox_SizeHeight);
			this.Controls.Add(this.textBox_SizeWidth);
			this.Controls.Add(this.textBox_LocationY);
			this.Controls.Add(this.textBox_LocationX);
			this.Controls.Add(this.label_Location);
			this.Controls.Add(this.textBox_SorceFileName);
			this.Controls.Add(this.label_SorceFileName);
			this.Controls.Add(this.textBox_TextureName);
			this.Controls.Add(this.label_TextureName);
			this.Controls.Add(this.listBox_TextureList);
			this.Controls.Add(this.mainMenu);
			this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "MainWindow";
			this.Text = "muphic テクスチャ名リスト作成ツール";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).EndInit();
			this.groupBox_Preview.ResumeLayout(false);
			this.groupBox_Preview.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DragDropListBox listBox_TextureList;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File_Exit;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Edit;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Help;
		private System.Windows.Forms.Label label_TextureName;
		private System.Windows.Forms.TextBox textBox_TextureName;
		private System.Windows.Forms.TextBox textBox_SorceFileName;
		private System.Windows.Forms.Label label_SorceFileName;
		private System.Windows.Forms.Label label_SizeComma;
		private System.Windows.Forms.Label label_LocationComma;
		private System.Windows.Forms.Label label_Size;
		private System.Windows.Forms.TextBox textBox_SizeHeight;
		private System.Windows.Forms.TextBox textBox_SizeWidth;
		private System.Windows.Forms.TextBox textBox_LocationY;
		private System.Windows.Forms.TextBox textBox_LocationX;
		private System.Windows.Forms.Label label_Location;
		private System.Windows.Forms.Button button_CreateTextureList;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Help_VersionInfo;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.CheckBox checkBox_EnabledPreview;
		private System.Windows.Forms.PictureBox pictureBox_Preview;
		private System.Windows.Forms.Button button_Update;
		private System.Windows.Forms.Button button_Delete;
		private System.Windows.Forms.TextBox textBox_PreviewSourceDirectory;
		private System.Windows.Forms.Label label_SourceFileNotFound;
		private System.Windows.Forms.Label label_PreviewSourceDirectory;
		private System.Windows.Forms.GroupBox groupBox_Preview;
		private System.Windows.Forms.Button button_Add;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Edit_AddFile;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File_Save;
		private System.Windows.Forms.ToolStripSeparator mainMenuItem_File_Separator2;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File_CreateNew;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File_Open;
		private System.Windows.Forms.ToolStripSeparator mainMenuItem_File_Separator1;
		private System.Windows.Forms.Button button_AutoAddFile;
		private System.Windows.Forms.TextBox textBox_AutoAddFile;
	}
}

