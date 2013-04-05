namespace ConfigurationMaker
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
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_Window = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_TopMost = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_VersionInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.textBox_Summary = new System.Windows.Forms.TextBox();
			this.label_Summary = new System.Windows.Forms.Label();
			this.label_Type = new System.Windows.Forms.Label();
			this.textBox_PropertyName = new System.Windows.Forms.TextBox();
			this.label_PropertyName = new System.Windows.Forms.Label();
			this.label_DefaultValue = new System.Windows.Forms.Label();
			this.textBox_Default = new System.Windows.Forms.TextBox();
			this.label_Summary2 = new System.Windows.Forms.Label();
			this.textBox_Result = new System.Windows.Forms.TextBox();
			this.button_Create = new System.Windows.Forms.Button();
			this.button_Clear = new System.Windows.Forms.Button();
			this.checkBox_Copy = new System.Windows.Forms.CheckBox();
			this.comboBox_Type = new System.Windows.Forms.ComboBox();
			this.MainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.toolStripMenuItem_Window,
            this.toolStripMenuItem_Help});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
			this.MainMenu.Size = new System.Drawing.Size(494, 28);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "menuStrip1";
			// 
			// toolStripMenuItem_File
			// 
			this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Exit});
			this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
			this.toolStripMenuItem_File.Size = new System.Drawing.Size(85, 22);
			this.toolStripMenuItem_File.Text = "ファイル(&F)";
			// 
			// toolStripMenuItem_Exit
			// 
			this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
			this.toolStripMenuItem_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.toolStripMenuItem_Exit.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItem_Exit.Text = "終了(&X)";
			this.toolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
			// 
			// toolStripMenuItem_Window
			// 
			this.toolStripMenuItem_Window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_TopMost});
			this.toolStripMenuItem_Window.Name = "toolStripMenuItem_Window";
			this.toolStripMenuItem_Window.Size = new System.Drawing.Size(102, 22);
			this.toolStripMenuItem_Window.Text = "ウィンドウ(&W)";
			// 
			// toolStripMenuItem_TopMost
			// 
			this.toolStripMenuItem_TopMost.Name = "toolStripMenuItem_TopMost";
			this.toolStripMenuItem_TopMost.Size = new System.Drawing.Size(154, 22);
			this.toolStripMenuItem_TopMost.Text = "常に最前面(&T)";
			this.toolStripMenuItem_TopMost.Click += new System.EventHandler(this.toolStripMenuItem_AllTopMost_Click);
			// 
			// toolStripMenuItem_Help
			// 
			this.toolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_VersionInfo});
			this.toolStripMenuItem_Help.Name = "toolStripMenuItem_Help";
			this.toolStripMenuItem_Help.Size = new System.Drawing.Size(75, 22);
			this.toolStripMenuItem_Help.Text = "ヘルプ(&H)";
			// 
			// toolStripMenuItem_VersionInfo
			// 
			this.toolStripMenuItem_VersionInfo.Name = "toolStripMenuItem_VersionInfo";
			this.toolStripMenuItem_VersionInfo.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.toolStripMenuItem_VersionInfo.Size = new System.Drawing.Size(200, 22);
			this.toolStripMenuItem_VersionInfo.Text = "バージョン情報(&A)";
			this.toolStripMenuItem_VersionInfo.Click += new System.EventHandler(this.ToolStripMenuItem_VersionInfo_Click);
			// 
			// textBox_Summary
			// 
			this.textBox_Summary.Location = new System.Drawing.Point(110, 39);
			this.textBox_Summary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBox_Summary.Name = "textBox_Summary";
			this.textBox_Summary.Size = new System.Drawing.Size(300, 25);
			this.textBox_Summary.TabIndex = 2;
			// 
			// label_Summary
			// 
			this.label_Summary.AutoSize = true;
			this.label_Summary.Location = new System.Drawing.Point(12, 42);
			this.label_Summary.Name = "label_Summary";
			this.label_Summary.Size = new System.Drawing.Size(80, 18);
			this.label_Summary.TabIndex = 1;
			this.label_Summary.Text = "設定の説明：";
			// 
			// label_Type
			// 
			this.label_Type.AutoSize = true;
			this.label_Type.Location = new System.Drawing.Point(12, 107);
			this.label_Type.Name = "label_Type";
			this.label_Type.Size = new System.Drawing.Size(68, 18);
			this.label_Type.TabIndex = 6;
			this.label_Type.Text = "設定の型：";
			// 
			// textBox_PropertyName
			// 
			this.textBox_PropertyName.Location = new System.Drawing.Point(110, 72);
			this.textBox_PropertyName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBox_PropertyName.Name = "textBox_PropertyName";
			this.textBox_PropertyName.Size = new System.Drawing.Size(300, 25);
			this.textBox_PropertyName.TabIndex = 5;
			// 
			// label_PropertyName
			// 
			this.label_PropertyName.AutoSize = true;
			this.label_PropertyName.Location = new System.Drawing.Point(12, 75);
			this.label_PropertyName.Name = "label_PropertyName";
			this.label_PropertyName.Size = new System.Drawing.Size(92, 18);
			this.label_PropertyName.TabIndex = 4;
			this.label_PropertyName.Text = "プロパティ名：";
			// 
			// label_DefaultValue
			// 
			this.label_DefaultValue.AutoSize = true;
			this.label_DefaultValue.Location = new System.Drawing.Point(12, 140);
			this.label_DefaultValue.Name = "label_DefaultValue";
			this.label_DefaultValue.Size = new System.Drawing.Size(92, 18);
			this.label_DefaultValue.TabIndex = 8;
			this.label_DefaultValue.Text = "デフォルト値：";
			// 
			// textBox_Default
			// 
			this.textBox_Default.Location = new System.Drawing.Point(110, 137);
			this.textBox_Default.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBox_Default.Name = "textBox_Default";
			this.textBox_Default.Size = new System.Drawing.Size(300, 25);
			this.textBox_Default.TabIndex = 9;
			// 
			// label_Summary2
			// 
			this.label_Summary2.AutoSize = true;
			this.label_Summary2.Location = new System.Drawing.Point(416, 42);
			this.label_Summary2.Name = "label_Summary2";
			this.label_Summary2.Size = new System.Drawing.Size(68, 18);
			this.label_Summary2.TabIndex = 3;
			this.label_Summary2.Text = "を示す値。";
			// 
			// textBox_Result
			// 
			this.textBox_Result.AcceptsReturn = true;
			this.textBox_Result.AcceptsTab = true;
			this.textBox_Result.Location = new System.Drawing.Point(12, 212);
			this.textBox_Result.Multiline = true;
			this.textBox_Result.Name = "textBox_Result";
			this.textBox_Result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_Result.Size = new System.Drawing.Size(470, 249);
			this.textBox_Result.TabIndex = 13;
			// 
			// button_Create
			// 
			this.button_Create.Location = new System.Drawing.Point(419, 105);
			this.button_Create.Name = "button_Create";
			this.button_Create.Size = new System.Drawing.Size(63, 57);
			this.button_Create.TabIndex = 11;
			this.button_Create.Text = "生成";
			this.button_Create.UseVisualStyleBackColor = true;
			this.button_Create.Click += new System.EventHandler(this.button_Create_Click);
			// 
			// button_Clear
			// 
			this.button_Clear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Clear.Location = new System.Drawing.Point(419, 72);
			this.button_Clear.Name = "button_Clear";
			this.button_Clear.Size = new System.Drawing.Size(63, 25);
			this.button_Clear.TabIndex = 10;
			this.button_Clear.Text = "クリア";
			this.button_Clear.UseVisualStyleBackColor = true;
			this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
			// 
			// checkBox_Copy
			// 
			this.checkBox_Copy.AutoSize = true;
			this.checkBox_Copy.Checked = true;
			this.checkBox_Copy.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Copy.Location = new System.Drawing.Point(15, 184);
			this.checkBox_Copy.Name = "checkBox_Copy";
			this.checkBox_Copy.Size = new System.Drawing.Size(279, 22);
			this.checkBox_Copy.TabIndex = 12;
			this.checkBox_Copy.Text = "生成と同時に、結果をクリップボードへコピー";
			this.checkBox_Copy.UseVisualStyleBackColor = true;
			// 
			// comboBox_Type
			// 
			this.comboBox_Type.FormattingEnabled = true;
			this.comboBox_Type.Items.AddRange(new object[] {
            "",
            "bool",
            "string",
            "int",
            "float",
            "double"});
			this.comboBox_Type.Location = new System.Drawing.Point(110, 104);
			this.comboBox_Type.Name = "comboBox_Type";
			this.comboBox_Type.Size = new System.Drawing.Size(300, 26);
			this.comboBox_Type.TabIndex = 7;
			// 
			// MainWindow
			// 
			this.AcceptButton = this.button_Create;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_Clear;
			this.ClientSize = new System.Drawing.Size(494, 473);
			this.Controls.Add(this.comboBox_Type);
			this.Controls.Add(this.checkBox_Copy);
			this.Controls.Add(this.button_Clear);
			this.Controls.Add(this.button_Create);
			this.Controls.Add(this.textBox_Result);
			this.Controls.Add(this.label_Summary2);
			this.Controls.Add(this.label_DefaultValue);
			this.Controls.Add(this.textBox_Default);
			this.Controls.Add(this.label_PropertyName);
			this.Controls.Add(this.textBox_PropertyName);
			this.Controls.Add(this.label_Type);
			this.Controls.Add(this.label_Summary);
			this.Controls.Add(this.textBox_Summary);
			this.Controls.Add(this.MainMenu);
			this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.MainMenu;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "MainWindow";
			this.Text = "設定プロパティ自動生成ツール";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
		private System.Windows.Forms.TextBox textBox_Summary;
		private System.Windows.Forms.Label label_Summary;
		private System.Windows.Forms.Label label_Type;
		private System.Windows.Forms.TextBox textBox_PropertyName;
		private System.Windows.Forms.Label label_PropertyName;
		private System.Windows.Forms.Label label_DefaultValue;
		private System.Windows.Forms.TextBox textBox_Default;
		private System.Windows.Forms.Label label_Summary2;
		private System.Windows.Forms.TextBox textBox_Result;
		private System.Windows.Forms.Button button_Create;
		private System.Windows.Forms.Button button_Clear;
		private System.Windows.Forms.CheckBox checkBox_Copy;
		private System.Windows.Forms.ComboBox comboBox_Type;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Help;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_VersionInfo;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Window;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_TopMost;
	}
}

