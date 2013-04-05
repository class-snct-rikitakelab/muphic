namespace Muphic.SubForms
{
	partial class DebugWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.。</param>
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
			this.MessegeLogBox = new System.Windows.Forms.TextBox();
			this.label_muphicVersion = new System.Windows.Forms.Label();
			this.label_muphicMode = new System.Windows.Forms.Label();
			this.panel_SysInfo = new System.Windows.Forms.Panel();
			this.label_GraphicDevice = new System.Windows.Forms.Label();
			this.label_Text2 = new System.Windows.Forms.Label();
			this.label_Text1 = new System.Windows.Forms.Label();
			this.label_WindowsName = new System.Windows.Forms.Label();
			this.panel_SysInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// MessegeLogBox
			// 
			this.MessegeLogBox.BackColor = System.Drawing.Color.White;
			this.MessegeLogBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.MessegeLogBox.HideSelection = false;
			this.MessegeLogBox.Location = new System.Drawing.Point(28, 62);
			this.MessegeLogBox.MaxLength = 2147483647;
			this.MessegeLogBox.Multiline = true;
			this.MessegeLogBox.Name = "MessegeLogBox";
			this.MessegeLogBox.ReadOnly = true;
			this.MessegeLogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.MessegeLogBox.Size = new System.Drawing.Size(760, 200);
			this.MessegeLogBox.TabIndex = 0;
			this.MessegeLogBox.TabStop = false;
			this.MessegeLogBox.Text = "muphic DebugMode ::: Consol Logging Window";
			this.MessegeLogBox.WordWrap = false;
			// 
			// label_muphicVersion
			// 
			this.label_muphicVersion.AutoSize = true;
			this.label_muphicVersion.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label_muphicVersion.Location = new System.Drawing.Point(12, 9);
			this.label_muphicVersion.Name = "label_muphicVersion";
			this.label_muphicVersion.Size = new System.Drawing.Size(156, 24);
			this.label_muphicVersion.TabIndex = 0;
			this.label_muphicVersion.Text = "muphic ver.7.x.x.x";
			// 
			// label_muphicMode
			// 
			this.label_muphicMode.AutoSize = true;
			this.label_muphicMode.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label_muphicMode.Location = new System.Drawing.Point(12, 33);
			this.label_muphicMode.Name = "label_muphicMode";
			this.label_muphicMode.Size = new System.Drawing.Size(90, 20);
			this.label_muphicMode.TabIndex = 1;
			this.label_muphicMode.Text = "Debug Mode";
			// 
			// panel_SysInfo
			// 
			this.panel_SysInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel_SysInfo.Controls.Add(this.label_GraphicDevice);
			this.panel_SysInfo.Controls.Add(this.label_WindowsName);
			this.panel_SysInfo.Location = new System.Drawing.Point(388, 12);
			this.panel_SysInfo.Name = "panel_SysInfo";
			this.panel_SysInfo.Size = new System.Drawing.Size(400, 44);
			this.panel_SysInfo.TabIndex = 2;
			// 
			// label_GraphicDevice
			// 
			this.label_GraphicDevice.AutoSize = true;
			this.label_GraphicDevice.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label_GraphicDevice.Location = new System.Drawing.Point(3, 21);
			this.label_GraphicDevice.Name = "label_GraphicDevice";
			this.label_GraphicDevice.Size = new System.Drawing.Size(95, 18);
			this.label_GraphicDevice.TabIndex = 5;
			this.label_GraphicDevice.Text = "Graphic Device";
			// 
			// label_Text2
			// 
			this.label_Text2.AutoSize = true;
			this.label_Text2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label_Text2.Location = new System.Drawing.Point(228, 34);
			this.label_Text2.Name = "label_Text2";
			this.label_Text2.Size = new System.Drawing.Size(41, 18);
			this.label_Text2.TabIndex = 6;
			this.label_Text2.Text = "Text2";
			// 
			// label_Text1
			// 
			this.label_Text1.AutoSize = true;
			this.label_Text1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label_Text1.Location = new System.Drawing.Point(228, 17);
			this.label_Text1.Name = "label_Text1";
			this.label_Text1.Size = new System.Drawing.Size(41, 18);
			this.label_Text1.TabIndex = 4;
			this.label_Text1.Text = "Text1";
			// 
			// label_WindowsName
			// 
			this.label_WindowsName.AutoSize = true;
			this.label_WindowsName.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label_WindowsName.Location = new System.Drawing.Point(3, 3);
			this.label_WindowsName.Name = "label_WindowsName";
			this.label_WindowsName.Size = new System.Drawing.Size(99, 18);
			this.label_WindowsName.TabIndex = 3;
			this.label_WindowsName.Text = "Windows Name";
			// 
			// DebugWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 273);
			this.Controls.Add(this.label_Text2);
			this.Controls.Add(this.panel_SysInfo);
			this.Controls.Add(this.label_Text1);
			this.Controls.Add(this.label_muphicMode);
			this.Controls.Add(this.label_muphicVersion);
			this.Controls.Add(this.MessegeLogBox);
			this.Cursor = System.Windows.Forms.Cursors.AppStarting;
			this.ForeColor = System.Drawing.Color.Black;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = global::Muphic.Properties.Resources.muphic;
			this.MaximizeBox = false;
			this.Name = "DebugWindow";
			this.Text = "muphic DebugWindow";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindow_FormClosing);
			this.panel_SysInfo.ResumeLayout(false);
			this.panel_SysInfo.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox MessegeLogBox;
		private System.Windows.Forms.Label label_muphicVersion;
		private System.Windows.Forms.Label label_muphicMode;
		private System.Windows.Forms.Panel panel_SysInfo;
		private System.Windows.Forms.Label label_WindowsName;
		private System.Windows.Forms.Label label_GraphicDevice;
		private System.Windows.Forms.Label label_Text2;
		private System.Windows.Forms.Label label_Text1;
	}
}