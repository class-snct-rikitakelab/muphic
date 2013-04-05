namespace Muphic.SubForms
{
	partial class FirstLaunchWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.firstLaunchLabel = new System.Windows.Forms.Label();
			this.launchExplainLabel = new System.Windows.Forms.Label();
			this.configurationButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.launchButton = new System.Windows.Forms.Button();
			this.teacherModeRadioButton = new System.Windows.Forms.RadioButton();
			this.teacherModeExplainLabel = new System.Windows.Forms.Label();
			this.studentModeExplainLabel = new System.Windows.Forms.Label();
			this.studentModeRadioButton = new System.Windows.Forms.RadioButton();
			this.normalModeExplainLabel = new System.Windows.Forms.Label();
			this.normalModeRadioButton = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// firstLaunchLabel
			// 
			this.firstLaunchLabel.AutoSize = true;
			this.firstLaunchLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.firstLaunchLabel.Location = new System.Drawing.Point(14, 14);
			this.firstLaunchLabel.Name = "firstLaunchLabel";
			this.firstLaunchLabel.Size = new System.Drawing.Size(271, 20);
			this.firstLaunchLabel.TabIndex = 0;
			this.firstLaunchLabel.Text = "コンピュータで muphic を初めて起動します";
			// 
			// launchExplainLabel
			// 
			this.launchExplainLabel.AutoEllipsis = true;
			this.launchExplainLabel.Location = new System.Drawing.Point(14, 44);
			this.launchExplainLabel.Name = "launchExplainLabel";
			this.launchExplainLabel.Size = new System.Drawing.Size(530, 42);
			this.launchExplainLabel.TabIndex = 1;
			this.launchExplainLabel.Text = "このコンピュータで使用する muphic の動作モードを選択してください。\r\nより詳細な設定を行う場合は、[muphic 詳細設定] ボタンをクリックしてください" +
				"。";
			// 
			// configurationButton
			// 
			this.configurationButton.Location = new System.Drawing.Point(12, 347);
			this.configurationButton.Name = "configurationButton";
			this.configurationButton.Size = new System.Drawing.Size(137, 34);
			this.configurationButton.TabIndex = 23;
			this.configurationButton.Text = "muphic 詳細設定";
			this.configurationButton.UseVisualStyleBackColor = true;
			this.configurationButton.Click += new System.EventHandler(this.configurationButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(412, 347);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(120, 34);
			this.cancelButton.TabIndex = 22;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// launchButton
			// 
			this.launchButton.Location = new System.Drawing.Point(286, 347);
			this.launchButton.Name = "launchButton";
			this.launchButton.Size = new System.Drawing.Size(120, 34);
			this.launchButton.TabIndex = 21;
			this.launchButton.Text = "muphic 起動";
			this.launchButton.UseVisualStyleBackColor = true;
			this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
			// 
			// teacherModeRadioButton
			// 
			this.teacherModeRadioButton.AutoSize = true;
			this.teacherModeRadioButton.Location = new System.Drawing.Point(24, 100);
			this.teacherModeRadioButton.Name = "teacherModeRadioButton";
			this.teacherModeRadioButton.Size = new System.Drawing.Size(157, 24);
			this.teacherModeRadioButton.TabIndex = 11;
			this.teacherModeRadioButton.TabStop = true;
			this.teacherModeRadioButton.Text = "授業用（講師）モード";
			this.teacherModeRadioButton.UseVisualStyleBackColor = true;
			this.teacherModeRadioButton.CheckedChanged += new System.EventHandler(this.TeacherModeRadioButton_CheckedChanged);
			// 
			// teacherModeExplainLabel
			// 
			this.teacherModeExplainLabel.AutoEllipsis = true;
			this.teacherModeExplainLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.teacherModeExplainLabel.Location = new System.Drawing.Point(39, 122);
			this.teacherModeExplainLabel.Name = "teacherModeExplainLabel";
			this.teacherModeExplainLabel.Size = new System.Drawing.Size(466, 42);
			this.teacherModeExplainLabel.TabIndex = 12;
			this.teacherModeExplainLabel.Text = "主に授業の講師が使用するのに適したモードです。ネットワークを使用し、提出された成果物の管理や印刷する機能を利用できます。";
			// 
			// studentModeExplainLabel
			// 
			this.studentModeExplainLabel.AutoEllipsis = true;
			this.studentModeExplainLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.studentModeExplainLabel.Location = new System.Drawing.Point(39, 198);
			this.studentModeExplainLabel.Name = "studentModeExplainLabel";
			this.studentModeExplainLabel.Size = new System.Drawing.Size(466, 42);
			this.studentModeExplainLabel.TabIndex = 15;
			this.studentModeExplainLabel.Text = "主に授業を受ける児童が使用するのに適したモードです。一部のボタンが制限され、ネットワークを使用した成果物の提出機能を利用できます。";
			// 
			// studentModeRadioButton
			// 
			this.studentModeRadioButton.AutoSize = true;
			this.studentModeRadioButton.Location = new System.Drawing.Point(24, 176);
			this.studentModeRadioButton.Name = "studentModeRadioButton";
			this.studentModeRadioButton.Size = new System.Drawing.Size(157, 24);
			this.studentModeRadioButton.TabIndex = 14;
			this.studentModeRadioButton.TabStop = true;
			this.studentModeRadioButton.Text = "授業用（児童）モード";
			this.studentModeRadioButton.UseVisualStyleBackColor = true;
			this.studentModeRadioButton.CheckedChanged += new System.EventHandler(this.StudentModeRadioButton_CheckedChanged);
			// 
			// normalModeExplainLabel
			// 
			this.normalModeExplainLabel.AutoEllipsis = true;
			this.normalModeExplainLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.normalModeExplainLabel.Location = new System.Drawing.Point(39, 274);
			this.normalModeExplainLabel.Name = "normalModeExplainLabel";
			this.normalModeExplainLabel.Size = new System.Drawing.Size(466, 42);
			this.normalModeExplainLabel.TabIndex = 18;
			this.normalModeExplainLabel.Text = "授業以外で使用するのに適したモードです。ネットワークは使用せず、印刷などローカルな機能のみが利用できます。";
			// 
			// normalModeRadioButton
			// 
			this.normalModeRadioButton.AutoSize = true;
			this.normalModeRadioButton.Location = new System.Drawing.Point(24, 252);
			this.normalModeRadioButton.Name = "normalModeRadioButton";
			this.normalModeRadioButton.Size = new System.Drawing.Size(92, 24);
			this.normalModeRadioButton.TabIndex = 17;
			this.normalModeRadioButton.TabStop = true;
			this.normalModeRadioButton.Text = "通常モード";
			this.normalModeRadioButton.UseVisualStyleBackColor = true;
			this.normalModeRadioButton.CheckedChanged += new System.EventHandler(this.NormalModeRadioButton_CheckedChanged);
			// 
			// FirstLaunchWindow
			// 
			this.AcceptButton = this.launchButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(544, 393);
			this.Controls.Add(this.normalModeExplainLabel);
			this.Controls.Add(this.normalModeRadioButton);
			this.Controls.Add(this.studentModeExplainLabel);
			this.Controls.Add(this.studentModeRadioButton);
			this.Controls.Add(this.teacherModeExplainLabel);
			this.Controls.Add(this.teacherModeRadioButton);
			this.Controls.Add(this.launchButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.configurationButton);
			this.Controls.Add(this.launchExplainLabel);
			this.Controls.Add(this.firstLaunchLabel);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "FirstLaunchWindow";
			this.Text = "muphic 初回起動";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label firstLaunchLabel;
		private System.Windows.Forms.Label launchExplainLabel;
		private System.Windows.Forms.Button configurationButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button launchButton;
		private System.Windows.Forms.RadioButton teacherModeRadioButton;
		private System.Windows.Forms.Label teacherModeExplainLabel;
		private System.Windows.Forms.Label studentModeExplainLabel;
		private System.Windows.Forms.RadioButton studentModeRadioButton;
		private System.Windows.Forms.Label normalModeExplainLabel;
		private System.Windows.Forms.RadioButton normalModeRadioButton;
	}
}