using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ConfigurationTool
{
	public partial class ConfigurationWindow : Form
	{
		// muphic 動作設定ウィンドウ
		// ページ２ "機能と作曲"タブの動作の記述


		#region 設定の読込と保存

		/// <summary>
		/// 指定した構成設定データから、"機能と作曲" タブのコントロールの状態を決定する。
		/// </summary>
		/// <param name="loadData">読み込む構成設定データ。</param>
		private void TabPage2_Load(ConfigurationData loadData)
		{
			// 機能の制限の設定のロード
			this.isLimitedOneScrCheckBox.Checked = !loadData.EnabledOneScreen;
			this.isLimitedLinkScrCheckBox.Checked = !loadData.EnabledLinkScreen;
			this.isLimitedStoryScrCheckBox.Checked = !loadData.EnabledStoryScreen;
			this.isLimitedTutorialScrCheckBox.Checked = !loadData.EnabledTutorial;


			// 自動保存の設定のロード
			if (loadData.AutoSaveInterval == 0)
			{
				this.enabledAutoSaveCheckBox.Checked = false;
				this.SetAutoSaveIntervalNumericUpDown(loadData.AutoSaveInterval_DefaultValue);
			}
			else
			{
				this.enabledAutoSaveCheckBox.Checked = true;
				this.SetAutoSaveIntervalNumericUpDown(loadData.AutoSaveInterval);
			}
			this.SetEnabledAutoSaveInterval();


			// 印刷機能の設定をロード
			this.enabledPrintCheckBox.Checked = loadData.EnabledPrint;
			this.SetEnabledPrintControls();

			// コンボボックスにプリンタ選択のデフォルト値を設定し、選択した状態にする
			this.selectPrintComboBox.Items.Clear();
			this.selectPrintComboBox.Items.Add(Properties.Settings.Default.PrinterSelectDefaultValue);
			this.selectPrintComboBox.SelectedIndex = 0;

			// プリンタの一覧を列挙しコンボボックスに追加
			foreach (string printerName in PrinterSettings.InstalledPrinters)
			{
				this.selectPrintComboBox.Items.Add(printerName);

				// この時、読み込んだ構成設定ファイルのプリンタ名と同じプリンタ名を発見したら、コンボボックスでそのプリンタを選択させる
				if (loadData.PrinterName == printerName) this.selectPrintComboBox.SelectedItem = printerName;
			}


			// 小節数の設定をロード
			this.SetLineNumericUpDown(loadData.CompositionMaxLine);
			
			// 八分音符の設定をロード
			this.isUseEighthNoteCheckBox.Checked = loadData.IsUseEighthNote;

			// 和音の制限の設定をロード
			if (loadData.HarmonyNum == 0)
			{
				this.accordCheckBox.Checked = false;
				this.SetAccordNumericUpDown(loadData.HarmonyNum_DefaultValue);
			}
			else
			{
				this.accordCheckBox.Checked = true;
				this.SetAccordNumericUpDown(loadData.HarmonyNum);
			}
			this.SetEnabledAccordNumberControls();

			// アシスタント機能の設定をロード
			this.enabledLimitModeCheckBox.Checked = loadData.EnabledLimitMode;
		}


		/// <summary>
		/// "機能と作曲の動作" タブのコントロールの状態から、指定した構成設定データへ各設定情報を書き込む。
		/// </summary>
		/// <param name="saveData">保存先の構成設定データ。</param>
		private void TabPage2_Save(ConfigurationData saveData)
		{
			// 動作モードの設定のセーブ
			if (this.operationModeNomalRadioButton.Checked) saveData.MuphicOperationMode = 0;
			else if (this.operationModeStudentRadioButton.Checked) saveData.MuphicOperationMode = 1;
			else if (this.operationModeTeacherRadioButton.Checked) saveData.MuphicOperationMode = 2;
			else saveData.MuphicOperationMode = 0;

			// 機能の制限の設定のセーブ
			saveData.EnabledOneScreen = !this.isLimitedOneScrCheckBox.Checked;
			saveData.EnabledLinkScreen = !this.isLimitedLinkScrCheckBox.Checked;
			saveData.EnabledStoryScreen = !this.isLimitedStoryScrCheckBox.Checked;
			saveData.EnabledTutorial = !this.isLimitedTutorialScrCheckBox.Checked;

			// 自動保存の設定のセーブ
			saveData.AutoSaveInterval = this.enabledAutoSaveCheckBox.Checked ? (int)this.autoSaveIntervalNumericUpDown.Value : 0;

			// 印刷機能の設定をセーブ
			saveData.EnabledPrint = this.enabledPrintCheckBox.Checked;
			saveData.PrinterName =
				(this.selectPrintComboBox.SelectedItem.ToString() == Properties.Settings.Default.PrinterSelectDefaultValue)
				? "" : this.selectPrintComboBox.SelectedItem.ToString();

			// 小節数の設定をセーブ
			saveData.CompositionMaxLine = (int)this.lineNumNumericUpDown.Value;

			// 八分音符の設定をセーブ
			saveData.IsUseEighthNote = this.isUseEighthNoteCheckBox.Checked;

			// 和音の制限の設定をセーブ
			saveData.HarmonyNum = this.accordCheckBox.Checked ? (int)this.harmonyNumNumericUpDown.Value : 0;

			// アシスタント機能の設定をセーブ
			saveData.EnabledLimitMode = this.enabledLimitModeCheckBox.Checked;
		}


		/// <summary>
		/// "規定値に戻す" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MuphicInfoTabResetButton_Click(object sender, EventArgs e)
		{
			if (!this.ConfirmReset()) return;

			this.TabPage2_Load(new ConfigurationData());
		}

		#endregion


		#region 機能の制限

		/// <summary>
		/// 機能の制限に関するコントロールにマウスが入力された際に、ヘルプテキストを更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LimitModeGroupBox_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescription(
				Properties.Resources.HelpText_ConfWindow_LimitMode_Title,
				Properties.Resources.HelpText_ConfWindow_LimitMode
			);
		}

		#endregion


		#region 自動保存

		/// <summary>
		/// "自動保存を行う (推奨)" チェックボックスの状態が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AutoSaveCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.SetEnabledAutoSaveInterval();
		}

		/// <summary>
		/// "自動保存を行う (推奨)" チェックボックスの状態に応じ、自動保存間隔指定テキストボックスと"分間隔で保存"ラベルの有効/無効の設定を行う。
		/// </summary>
		private void SetEnabledAutoSaveInterval()
		{
			this.autoSaveIntervalNumericUpDown.Enabled = this.enabledAutoSaveCheckBox.Checked;
			this.autoSaveIntervalLabel.Enabled = this.enabledAutoSaveCheckBox.Checked;
		}

		/// <summary>
		/// 自動保存の設定に関するコントロールにマウスが入力された際に、ヘルプテキストを更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AutoSaveControls_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescription(
				Properties.Resources.HelpText_ConfWindow_AutoSave_Title,
				Properties.Resources.HelpText_ConfWindow_AutoSave
			);
		}

		/// <summary>
		/// 小節数スピンボックスに指定された値をセットする。ただし、最大値および最小値が許可されていない値だった場合は修正する。
		/// </summary>
		/// <param name="value">セットする値。</param>
		private void SetAutoSaveIntervalNumericUpDown(int value)
		{
			if (value < this.autoSaveIntervalNumericUpDown.Minimum) this.autoSaveIntervalNumericUpDown.Value = this.autoSaveIntervalNumericUpDown.Minimum;
			else if (value > this.autoSaveIntervalNumericUpDown.Maximum) this.autoSaveIntervalNumericUpDown.Value = this.autoSaveIntervalNumericUpDown.Maximum;
			else this.autoSaveIntervalNumericUpDown.Value = value;
		}

		#endregion


		#region 印刷機能

		/// <summary>
		/// "印刷機能を有効にする"チェックボックスの状態が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.SetEnabledPrintControls();
		}

		/// <summary>
		/// "印刷機能を有効にする"チェックボックスの状態に応じ、印刷関連のコントロールの有効/無効の切替を行う。
		/// </summary>
		private void SetEnabledPrintControls()
		{
			this.selectPrintComboBox.Enabled = this.enabledPrintCheckBox.Checked;
		}

		/// <summary>
		/// プリンタ選択コンボボックスの何らかの要素が選択された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// デフォルト値以外の要素が選択された場合、デフォルト値を削除する
			if (this.selectPrintComboBox.Items[0].ToString() == Properties.Settings.Default.PrinterSelectDefaultValue && this.selectPrintComboBox.SelectedIndex != 0)
			{
				this.selectPrintComboBox.Items.RemoveAt(0);
			}
		}


		#region プリンタ毎の詳細設定ダイアログを直接呼び出す場合

		//[DllImport("winspool.drv", SetLastError = true)]
		//static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

		//[DllImport("winspool.drv", SetLastError = true)]
		//static extern int ClosePrinter(IntPtr hPrinter);
		
		//[DllImport("winspool.drv", EntryPoint = "DocumentPropertiesW", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		//static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);


		///// <summary>
		///// プリンタ選択ダイアログを介さず、プリンタ毎の詳細設定ダイアログを直接呼び出す。
		///// </summary>
		///// <remarks>http://d.hatena.ne.jp/machi_pon/20061004/1159953490</remarks>
		///// <param name="hwnd"></param>
		///// <param name="ps"></param>
		///// <returns></returns>
		//private bool ShowProperties(IntPtr hwnd, PrinterSettings ps)
		//{
		//    IntPtr hPrt = IntPtr.Zero;

		//    IntPtr hDevMode = ps.GetHdevmode(ps.DefaultPageSettings);
		//    IntPtr pDevModeInput = Marshal.AllocHGlobal(hDevMode);

		//    if (OpenPrinter(ps.PrinterName, out hPrt, IntPtr.Zero) == false)
		//    {
		//        return (false);
		//    }
		//    if (hPrt == IntPtr.Zero)
		//    {
		//        return (false);
		//    }

		//    int size = DocumentProperties(hwnd, hPrt, ps.PrinterName, IntPtr.Zero, IntPtr.Zero, 0);

		//    IntPtr pDevModeOutput = Marshal.AllocHGlobal(size);

		//    int ret = DocumentProperties(hwnd, hPrt, ps.PrinterName, pDevModeOutput, pDevModeInput, 14);
		//    if (ret == 1)
		//    {
		//        ps.SetHdevmode(pDevModeOutput);
		//    }

		//    Marshal.FreeHGlobal(pDevModeOutput);
		//    Marshal.FreeHGlobal(pDevModeInput);
		//    ClosePrinter(hPrt);

		//    return (true);
		//}

		#endregion


		/// <summary>
		/// 印刷機能の設定に関するコントロールにマウスが入力された際に、ヘルプテキストを更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintGroupBox_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescription(
				Properties.Resources.HelpText_ConfWindow_Print_Title,
				Properties.Resources.HelpText_ConfWindow_Print
			);
		}

		#endregion


		#region 作曲動作

		/// <summary>
		/// 小節数スピンボックスに指定された値をセットする。ただし、最大値および最小値が許可されていない値だった場合は修正する。
		/// </summary>
		/// <param name="value">セットする値。</param>
		private void SetLineNumericUpDown(int value)
		{
			if (value < this.lineNumNumericUpDown.Minimum) this.lineNumNumericUpDown.Value = this.lineNumNumericUpDown.Minimum;
			else if (value > this.lineNumNumericUpDown.Maximum) this.lineNumNumericUpDown.Value = this.lineNumNumericUpDown.Maximum;
			else this.lineNumNumericUpDown.Value = value;
		}

		/// <summary>
		/// 小節数スピンボックスの値が変更された際の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LineNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			// ４の倍数になるよう調整
			this.lineNumNumericUpDown.Value -= this.lineNumNumericUpDown.Value % 4;
		}

		/// <summary>
		/// スピンボックスの数値が無効だった場合にメッセージを表示する。
		/// </summary>
		/// <param name="place">無効な数値が入力されたコントロール名。</param>
		/// <param name="min">スピンボックスで許可される最小値。</param>
		/// <param name="max">スピンボックスで許可される最大値。</param>
		private void ShowNumericUpDownErrorMessage(string place, decimal min, decimal max)
		{
			if (this == null) return;

			MessageBox.Show(
				Properties.Resources.ErrorMsg_Show_NumericUpDown.Replace("%str1%", place).Replace("%str2%", min.ToString()).Replace("%str3%", max.ToString()),
				Properties.Resources.ErrorMsg_Show_NumericUpDown_Caption,
				MessageBoxButtons.OK,
				MessageBoxIcon.Asterisk
			);
		}


		/// <summary>
		/// "和音を制限する"チェックボックスの状態が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AccordCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.SetEnabledAccordNumberControls();
		}

		/// <summary>
		/// "和音を制限する"チェックボックスの状態に応じ、和音制限のコントロールの有効/無効の切替を行う。
		/// </summary>
		private void SetEnabledAccordNumberControls()
		{
			this.harmonyNumNumericUpDown.Enabled = this.accordCheckBox.Checked;
			this.harmonyNumLabel.Enabled = this.accordCheckBox.Checked;
		}

		/// <summary>
		/// 和音スピンボックスに指定された値をセットする。ただし、最大値および最小値が許可されていない値だった場合は修正する。
		/// </summary>
		/// <param name="value">セットする値。</param>
		private void SetAccordNumericUpDown(int value)
		{
			if (value < this.harmonyNumNumericUpDown.Minimum) this.harmonyNumNumericUpDown.Value = this.harmonyNumNumericUpDown.Minimum;
			else if (value > this.harmonyNumNumericUpDown.Maximum) this.harmonyNumNumericUpDown.Value = this.harmonyNumNumericUpDown.Maximum;
			else this.harmonyNumNumericUpDown.Value = value;
		}

		#endregion


		#region MouseEnter

		/// <summary>
		/// "機能と作曲の動作"タブにマウスが入力された際に、ヘルプテキストをデフォルトに更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPage2_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescriptionDefault();
		}

		#endregion
	}
}
