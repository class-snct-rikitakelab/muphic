using System.Drawing;
using System.Windows.Forms;
using Muphic.Manager;

namespace Muphic.SubForms
{
	/// <summary>
	/// デバッグモード時に、簡易的なシステム情報やコンソール出力されたメッセージの表示機能を提供するウィンドウ。
	/// </summary>
	public partial class DebugWindow : Form
	{
		/// <summary>
		/// デバッグウィンドウの新しいインスタンスを初期化する。
		/// </summary>
		public DebugWindow()
		{
			InitializeComponent();

			this.Text += "  " + DebugWindow.version;
			this.MessegeLogBox.Text = "muphic DebugMode ::: Logging Window\r\n\r\n";

			// ウィンドウが表示される位置が muphic の下になるよう調整
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(0, 610);
		}

		/// <summary>
		/// デバッグウィンドウのバージョン。
		/// </summary>
		private const string version = "version 1.1";

		/// <summary>
		/// システム情報を取得し、コントロールに反映する。
		/// </summary>
		public void SetSystemInfo()
		{
			this.label_muphicVersion.Text = "muphic ver." + SystemInfoManager.MuphicVersion;

			this.label_WindowsName.Text = string.Format("{0}（{1}  {2}）", SystemInfoManager.WindowsName, SystemInfoManager.WindowsVersion, SystemInfoManager.ServicePack);
			this.label_GraphicDevice.Text = string.Format("{0}（{1}）", SystemInfoManager.GraphicAdapterName, SystemInfoManager.CurrentDisplaySize);
			this.label_Text1.Text = "";
			this.label_Text2.Text = "";

			//this.label_WindowsName.Text = Manager.SystemInfoManager.WindowsName;
			//this.label_WindowsVersion.Text = Manager.SystemInfoManager.WindowsVersion + "  " + Manager.SystemInfoManager.ServicePack;
			//this.label_GraphicDevice.Text = Manager.SystemInfoManager.GraphicAdapterName;
			//this.label_DisplaySize.Text = Manager.SystemInfoManager.CurrentDisplaySize;
		}

		
		/// <summary>
		/// テキストボックス内にメッセージを出力する
		/// </summary>
		/// <param name="messege">。</param>
		public void WriteLine(string messege)
		{
			this.MessegeLogBox.Text += messege + "\r\n";

			this.MessegeLogBox.SelectionStart = this.MessegeLogBox.Text.Length;
			this.MessegeLogBox.ScrollToCaret();
		}


		/// <summary>
		/// フォームが閉じられようとした時に発生する。
		/// </summary>
		/// <param name="sender">。</param>
		/// <param name="e">。</param>
		void DebugWindow_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			e.Cancel = true;	// フォームは閉じずに
			this.Hide();		// 非表示にする（muphic 上で d キーを押下することで再表示できる）
		}
	}
}
