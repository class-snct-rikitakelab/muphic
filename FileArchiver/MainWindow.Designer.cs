namespace FileArchiver
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
			this.pictureBox_ImgeFilePreview = new System.Windows.Forms.PictureBox();
			this.button_CreateArchive = new System.Windows.Forms.Button();
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.mainMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_File_NewArchive = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_File_ReadArchive = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_File_Separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuItem_File_CreateArchive = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_File_Separator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuItem_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Edit_AddFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Edit_DeleteFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuItem_Help_VersionInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.listBox_FileList = new System.Windows.Forms.ListBox();
			this.groupBox_FileInfo = new System.Windows.Forms.GroupBox();
			this.label_IsEncrypte = new System.Windows.Forms.Label();
			this.label_FileSize = new System.Windows.Forms.Label();
			this.label_Kind = new System.Windows.Forms.Label();
			this.label_FilePath = new System.Windows.Forms.Label();
			this.groupBox_ArchiveInfo = new System.Windows.Forms.GroupBox();
			this.label_ArchivedFileNum = new System.Windows.Forms.Label();
			this.label_ArchiveFileName = new System.Windows.Forms.Label();
			this.button_Play = new System.Windows.Forms.Button();
			this.button_Stop = new System.Windows.Forms.Button();
			this.label_TextPreview = new System.Windows.Forms.Label();
			this.button_AutoAddFile = new System.Windows.Forms.Button();
			this.textBox_AutoAddFile = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_ImgeFilePreview)).BeginInit();
			this.mainMenu.SuspendLayout();
			this.groupBox_FileInfo.SuspendLayout();
			this.groupBox_ArchiveInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox_ImgeFilePreview
			// 
			this.pictureBox_ImgeFilePreview.Location = new System.Drawing.Point(272, 101);
			this.pictureBox_ImgeFilePreview.Name = "pictureBox_ImgeFilePreview";
			this.pictureBox_ImgeFilePreview.Size = new System.Drawing.Size(600, 507);
			this.pictureBox_ImgeFilePreview.TabIndex = 0;
			this.pictureBox_ImgeFilePreview.TabStop = false;
			// 
			// button_CreateArchive
			// 
			this.button_CreateArchive.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button_CreateArchive.Location = new System.Drawing.Point(12, 614);
			this.button_CreateArchive.Name = "button_CreateArchive";
			this.button_CreateArchive.Size = new System.Drawing.Size(254, 37);
			this.button_CreateArchive.TabIndex = 20;
			this.button_CreateArchive.Text = "アーカイブ生成";
			this.button_CreateArchive.UseVisualStyleBackColor = true;
			this.button_CreateArchive.Click += new System.EventHandler(this.button_create_Click);
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
			this.mainMenu.TabIndex = 0;
			this.mainMenu.Text = "menuStrip1";
			// 
			// mainMenuItem_File
			// 
			this.mainMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuItem_File_NewArchive,
            this.mainMenuItem_File_ReadArchive,
            this.mainMenuItem_File_Separator1,
            this.mainMenuItem_File_CreateArchive,
            this.mainMenuItem_File_Separator2,
            this.mainMenuItem_File_Exit});
			this.mainMenuItem_File.Name = "mainMenuItem_File";
			this.mainMenuItem_File.Size = new System.Drawing.Size(85, 22);
			this.mainMenuItem_File.Text = "ファイル(&F)";
			// 
			// mainMenuItem_File_NewArchive
			// 
			this.mainMenuItem_File_NewArchive.Name = "mainMenuItem_File_NewArchive";
			this.mainMenuItem_File_NewArchive.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mainMenuItem_File_NewArchive.Size = new System.Drawing.Size(237, 22);
			this.mainMenuItem_File_NewArchive.Text = "新規作成(&N)";
			this.mainMenuItem_File_NewArchive.Click += new System.EventHandler(this.mainMenuItem_File_NewArchive_Click);
			// 
			// mainMenuItem_File_ReadArchive
			// 
			this.mainMenuItem_File_ReadArchive.Name = "mainMenuItem_File_ReadArchive";
			this.mainMenuItem_File_ReadArchive.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mainMenuItem_File_ReadArchive.Size = new System.Drawing.Size(237, 22);
			this.mainMenuItem_File_ReadArchive.Text = "開く(&O)";
			this.mainMenuItem_File_ReadArchive.Click += new System.EventHandler(this.mainMenuItem_File_ReadArchive_Click);
			// 
			// mainMenuItem_File_Separator1
			// 
			this.mainMenuItem_File_Separator1.Name = "mainMenuItem_File_Separator1";
			this.mainMenuItem_File_Separator1.Size = new System.Drawing.Size(234, 6);
			// 
			// mainMenuItem_File_CreateArchive
			// 
			this.mainMenuItem_File_CreateArchive.Name = "mainMenuItem_File_CreateArchive";
			this.mainMenuItem_File_CreateArchive.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mainMenuItem_File_CreateArchive.Size = new System.Drawing.Size(237, 22);
			this.mainMenuItem_File_CreateArchive.Text = "名前を付けて保存(&S)";
			this.mainMenuItem_File_CreateArchive.Click += new System.EventHandler(this.mainMenuItem_File_CreateArchive_Click);
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
            this.mainMenuItem_Edit_AddFile,
            this.mainMenuItem_Edit_DeleteFile});
			this.mainMenuItem_Edit.Name = "mainMenuItem_Edit";
			this.mainMenuItem_Edit.Size = new System.Drawing.Size(61, 22);
			this.mainMenuItem_Edit.Text = "編集(&E)";
			// 
			// mainMenuItem_Edit_AddFile
			// 
			this.mainMenuItem_Edit_AddFile.Name = "mainMenuItem_Edit_AddFile";
			this.mainMenuItem_Edit_AddFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.mainMenuItem_Edit_AddFile.Size = new System.Drawing.Size(225, 22);
			this.mainMenuItem_Edit_AddFile.Text = "ファイルを追加(&A)";
			this.mainMenuItem_Edit_AddFile.Click += new System.EventHandler(this.mainMenuItem_Edit_AddFile_Click);
			// 
			// mainMenuItem_Edit_DeleteFile
			// 
			this.mainMenuItem_Edit_DeleteFile.Name = "mainMenuItem_Edit_DeleteFile";
			this.mainMenuItem_Edit_DeleteFile.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.mainMenuItem_Edit_DeleteFile.Size = new System.Drawing.Size(225, 22);
			this.mainMenuItem_Edit_DeleteFile.Text = "ファイルを削除(&D)";
			this.mainMenuItem_Edit_DeleteFile.Click += new System.EventHandler(this.mainMenuItem_Edit_DeleteFile_Click);
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
			// listBox_FileList
			// 
			this.listBox_FileList.AllowDrop = true;
			this.listBox_FileList.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listBox_FileList.FormattingEnabled = true;
			this.listBox_FileList.ItemHeight = 18;
			this.listBox_FileList.Location = new System.Drawing.Point(12, 100);
			this.listBox_FileList.Name = "listBox_FileList";
			this.listBox_FileList.Size = new System.Drawing.Size(254, 508);
			this.listBox_FileList.TabIndex = 4;
			this.listBox_FileList.SelectedIndexChanged += new System.EventHandler(this.listBox_FileList_SelectedIndexChanged);
			this.listBox_FileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox_FileList_DragDrop);
			this.listBox_FileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox_FileList_DragEnter);
			// 
			// groupBox_FileInfo
			// 
			this.groupBox_FileInfo.Controls.Add(this.label_IsEncrypte);
			this.groupBox_FileInfo.Controls.Add(this.label_FileSize);
			this.groupBox_FileInfo.Controls.Add(this.label_Kind);
			this.groupBox_FileInfo.Controls.Add(this.label_FilePath);
			this.groupBox_FileInfo.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox_FileInfo.Location = new System.Drawing.Point(272, 29);
			this.groupBox_FileInfo.Name = "groupBox_FileInfo";
			this.groupBox_FileInfo.Size = new System.Drawing.Size(600, 66);
			this.groupBox_FileInfo.TabIndex = 10;
			this.groupBox_FileInfo.TabStop = false;
			this.groupBox_FileInfo.Text = "ファイル情報";
			// 
			// label_IsEncrypte
			// 
			this.label_IsEncrypte.AutoSize = true;
			this.label_IsEncrypte.Location = new System.Drawing.Point(550, 42);
			this.label_IsEncrypte.Name = "label_IsEncrypte";
			this.label_IsEncrypte.Size = new System.Drawing.Size(44, 18);
			this.label_IsEncrypte.TabIndex = 14;
			this.label_IsEncrypte.Text = "暗号化";
			// 
			// label_FileSize
			// 
			this.label_FileSize.AutoSize = true;
			this.label_FileSize.Location = new System.Drawing.Point(269, 42);
			this.label_FileSize.Name = "label_FileSize";
			this.label_FileSize.Size = new System.Drawing.Size(92, 18);
			this.label_FileSize.TabIndex = 13;
			this.label_FileSize.Text = "ファイルサイズ";
			// 
			// label_Kind
			// 
			this.label_Kind.AutoSize = true;
			this.label_Kind.Location = new System.Drawing.Point(6, 42);
			this.label_Kind.Name = "label_Kind";
			this.label_Kind.Size = new System.Drawing.Size(92, 18);
			this.label_Kind.TabIndex = 12;
			this.label_Kind.Text = "ファイルの種類";
			// 
			// label_FilePath
			// 
			this.label_FilePath.AutoEllipsis = true;
			this.label_FilePath.Location = new System.Drawing.Point(6, 21);
			this.label_FilePath.Name = "label_FilePath";
			this.label_FilePath.Size = new System.Drawing.Size(588, 23);
			this.label_FilePath.TabIndex = 11;
			this.label_FilePath.Text = "ファイルパス or アーカイブ済みファイル名";
			// 
			// groupBox_ArchiveInfo
			// 
			this.groupBox_ArchiveInfo.Controls.Add(this.label_ArchivedFileNum);
			this.groupBox_ArchiveInfo.Controls.Add(this.label_ArchiveFileName);
			this.groupBox_ArchiveInfo.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.groupBox_ArchiveInfo.Location = new System.Drawing.Point(12, 29);
			this.groupBox_ArchiveInfo.Name = "groupBox_ArchiveInfo";
			this.groupBox_ArchiveInfo.Size = new System.Drawing.Size(254, 65);
			this.groupBox_ArchiveInfo.TabIndex = 1;
			this.groupBox_ArchiveInfo.TabStop = false;
			this.groupBox_ArchiveInfo.Text = "アーカイブ情報";
			// 
			// label_ArchivedFileNum
			// 
			this.label_ArchivedFileNum.AutoSize = true;
			this.label_ArchivedFileNum.Location = new System.Drawing.Point(6, 42);
			this.label_ArchivedFileNum.Name = "label_ArchivedFileNum";
			this.label_ArchivedFileNum.Size = new System.Drawing.Size(68, 18);
			this.label_ArchivedFileNum.TabIndex = 3;
			this.label_ArchivedFileNum.Text = "ファイル数";
			// 
			// label_ArchiveFileName
			// 
			this.label_ArchiveFileName.AutoSize = true;
			this.label_ArchiveFileName.Location = new System.Drawing.Point(6, 22);
			this.label_ArchiveFileName.Name = "label_ArchiveFileName";
			this.label_ArchiveFileName.Size = new System.Drawing.Size(128, 18);
			this.label_ArchiveFileName.TabIndex = 2;
			this.label_ArchiveFileName.Text = "アーカイブファイル名";
			// 
			// button_Play
			// 
			this.button_Play.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button_Play.Location = new System.Drawing.Point(272, 101);
			this.button_Play.Name = "button_Play";
			this.button_Play.Size = new System.Drawing.Size(120, 40);
			this.button_Play.TabIndex = 14;
			this.button_Play.Text = "再生";
			this.button_Play.UseVisualStyleBackColor = true;
			this.button_Play.Click += new System.EventHandler(this.button_Play_Click);
			// 
			// button_Stop
			// 
			this.button_Stop.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button_Stop.Location = new System.Drawing.Point(398, 101);
			this.button_Stop.Name = "button_Stop";
			this.button_Stop.Size = new System.Drawing.Size(120, 40);
			this.button_Stop.TabIndex = 15;
			this.button_Stop.Text = "停止";
			this.button_Stop.UseVisualStyleBackColor = true;
			this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
			// 
			// label_TextPreview
			// 
			this.label_TextPreview.AutoSize = true;
			this.label_TextPreview.Font = new System.Drawing.Font("MeiryoKe_Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label_TextPreview.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label_TextPreview.Location = new System.Drawing.Point(279, 108);
			this.label_TextPreview.Name = "label_TextPreview";
			this.label_TextPreview.Size = new System.Drawing.Size(133, 15);
			this.label_TextPreview.TabIndex = 16;
			this.label_TextPreview.Text = "テキストプレビュー";
			// 
			// button_AutoAddFile
			// 
			this.button_AutoAddFile.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button_AutoAddFile.Location = new System.Drawing.Point(272, 625);
			this.button_AutoAddFile.Name = "button_AutoAddFile";
			this.button_AutoAddFile.Size = new System.Drawing.Size(120, 27);
			this.button_AutoAddFile.TabIndex = 21;
			this.button_AutoAddFile.Text = "自動読込";
			this.button_AutoAddFile.UseVisualStyleBackColor = true;
			this.button_AutoAddFile.Click += new System.EventHandler(this.button_AutoAddFile_Click);
			// 
			// textBox_AutoAddFile
			// 
			this.textBox_AutoAddFile.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox_AutoAddFile.Location = new System.Drawing.Point(398, 626);
			this.textBox_AutoAddFile.Name = "textBox_AutoAddFile";
			this.textBox_AutoAddFile.Size = new System.Drawing.Size(474, 25);
			this.textBox_AutoAddFile.TabIndex = 22;
			this.textBox_AutoAddFile.Text = "G:\\My Documents\\SNCT Works\\Takayuki Lab\\専攻研究\\muphic\\画像／画面設計\\アーカイブソース";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 663);
			this.Controls.Add(this.textBox_AutoAddFile);
			this.Controls.Add(this.button_AutoAddFile);
			this.Controls.Add(this.button_Stop);
			this.Controls.Add(this.button_Play);
			this.Controls.Add(this.groupBox_ArchiveInfo);
			this.Controls.Add(this.groupBox_FileInfo);
			this.Controls.Add(this.listBox_FileList);
			this.Controls.Add(this.button_CreateArchive);
			this.Controls.Add(this.pictureBox_ImgeFilePreview);
			this.Controls.Add(this.mainMenu);
			this.Controls.Add(this.label_TextPreview);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainWindow";
			this.Text = "muphic データアーカイバ";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_ImgeFilePreview)).EndInit();
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.groupBox_FileInfo.ResumeLayout(false);
			this.groupBox_FileInfo.PerformLayout();
			this.groupBox_ArchiveInfo.ResumeLayout(false);
			this.groupBox_ArchiveInfo.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox_ImgeFilePreview;
		private System.Windows.Forms.Button button_CreateArchive;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File;
		private System.Windows.Forms.ListBox listBox_FileList;
		private System.Windows.Forms.GroupBox groupBox_FileInfo;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File_ReadArchive;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File_CreateArchive;
		private System.Windows.Forms.ToolStripSeparator mainMenuItem_File_Separator1;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File_Exit;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Edit;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Edit_AddFile;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Help;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Help_VersionInfo;
		private System.Windows.Forms.Label label_FilePath;
		private System.Windows.Forms.Label label_Kind;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_File_NewArchive;
		private System.Windows.Forms.ToolStripSeparator mainMenuItem_File_Separator2;
		private System.Windows.Forms.Label label_FileSize;
		private System.Windows.Forms.GroupBox groupBox_ArchiveInfo;
		private System.Windows.Forms.Label label_ArchivedFileNum;
		private System.Windows.Forms.Label label_ArchiveFileName;
		private System.Windows.Forms.Button button_Play;
		private System.Windows.Forms.Button button_Stop;
		private System.Windows.Forms.Label label_TextPreview;
		private System.Windows.Forms.ToolStripMenuItem mainMenuItem_Edit_DeleteFile;
		private System.Windows.Forms.Label label_IsEncrypte;
		private System.Windows.Forms.Button button_AutoAddFile;
		private System.Windows.Forms.TextBox textBox_AutoAddFile;

	}
}

