using System;
using System.Windows.Forms;

namespace ConfigurationMaker
{
	/// <summary>
	/// 設定プロパティ自動生成ツールのメインウィンドウ。
	/// </summary>
	public partial class MainWindow : Form
	{
		/// <summary>
		/// せっていのメインウィンドウの当たらしインスタンスを初期化する。
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			this.Clear();

			this.StartPosition = FormStartPosition.Manual;
			this.Location = Properties.Settings.Default.WindowLocation;
			this.AllTopMost = Properties.Settings.Default.AllTopMost;
		}


		#region フォーム関連

		/// <summary>
		/// 生成ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Create_Click(object sender, EventArgs e)
		{
			this.Create();
		}

		/// <summary>
		/// クリアボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Clear_Click(object sender, EventArgs e)
		{
			this.Clear();
		}

		/// <summary>
		/// 終了メニューがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// バージョン情報メニューがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItem_VersionInfo_Click(object sender, EventArgs e)
		{
			using (var versionInfo = new VersionInfoWindow())
			{
				versionInfo.TopMost = this.AllTopMost;
				versionInfo.ShowDialog();
			}
		}


		/// <summary>
		/// フォームが閉じられる際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.WindowLocation = this.Location;
			Properties.Settings.Default.AllTopMost = this.AllTopMost;
			Properties.Settings.Default.Save();
		}


		/// <summary>
		/// 常に最前面に表示するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool AllTopMost
		{
			get
			{
				return this.TopMost;
			}
			set
			{
				this.toolStripMenuItem_TopMost.Checked = value;
				this.TopMost = value;
			}
		}

		/// <summary>
		/// 常に最前面メニューがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItem_AllTopMost_Click(object sender, EventArgs e)
		{
			this.AllTopMost = !this.AllTopMost;
		}

		#endregion


		/// <summary>
		/// フォームの各コントロールの値に応じて設定プロパティを生成する。
		/// </summary>
		private void Create()
		{
			this.Create(
				this.textBox_Summary.Text,
				this.comboBox_Type.Text,
				this.textBox_PropertyName.Text,
				this.textBox_Default.Text,
				this.checkBox_Copy.Checked
			);
		}

		/// <summary>
		/// 設定プロパティを生成する。
		/// </summary>
		/// <param name="summary">プロパティの説明。</param>
		/// <param name="type">プロパティの型。</param>
		/// <param name="propertyName">プロパティ名。</param>
		/// <param name="defaultValue">プロパティの初期値。</param>
		/// <param name="isCopy">生成後にクリップボードにコピーする場合は true。それ以外は false。</param>
		private void Create(string summary, string type, string propertyName, string defaultValue, bool isCopy)
		{
			if (type == "string" || type == "String" || type == "STRING")
			{
				if (defaultValue.Length <= 0)
				{
					defaultValue = "\"\"";
				}
				else if (defaultValue[0] != '"' || defaultValue[defaultValue.Length - 1] != '"')
				{
					defaultValue = "\"" + defaultValue + "\"";
				}
			}

			string result = Properties.Resources.PropertySource;

			result = result.Replace("%Summary%", summary);
			result = result.Replace("%Type%", type);
			result = result.Replace("%PropertyName%", propertyName);
			result = result.Replace("%DefaultValue%", defaultValue);

			if (isCopy) Clipboard.SetDataObject(result, true);

			this.textBox_Result.Text = result;
		}


		/// <summary>
		/// 各コントロールを初期化する。
		/// </summary>
		private void Clear()
		{
			this.Clear("", "", "", "", "", true);
		}

		/// <summary>
		/// 各コントロールを指定した値で初期化する。
		/// </summary>
		/// <param name="summary">プロパティの説明。</param>
		/// <param name="type">プロパティの型。</param>
		/// <param name="propertyName">プロパティ名。</param>
		/// <param name="defaultValue">プロパティの初期値。</param>
		/// <param name="result">プロパティの生成結果。</param>
		/// <param name="isCopy">生成後にクリップボードにコピーする場合は true。それ以外は false。</param>
		private void Clear(string summary, string type, string propertyName, string defaultValue, string result, bool isCopy)
		{
			this.textBox_Summary.Text = summary;
			this.comboBox_Type.Text = type;
			this.textBox_PropertyName.Text = propertyName;
			this.textBox_Default.Text = defaultValue;
			this.textBox_Result.Text = result;
			this.checkBox_Copy.Checked = isCopy;
		}

	}
}
