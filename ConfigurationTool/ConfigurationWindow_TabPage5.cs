using System;
using System.IO;
using System.Windows.Forms;

namespace ConfigurationTool
{
	public partial class ConfigurationWindow : Form
	{
		// muphic 動作設定ウィンドウ
		// ページ５ "動作モード"タブの動作の記述


		#region 設定の読込と保存

		/// <summary>
		/// 指定した構成設定データから、"動作モード" タブのコントロールの状態を決定する。
		/// </summary>
		/// <param name="loadData">読み込む構成設定データ。</param>
		private void TabPage5_Load(ConfigurationData loadData)
		{
			// 動作モードの設定のロード
			this.SetOperationMode(loadData.MuphicOperationMode);

			// 児童用の設定のロード
			this.isShiftkeyProtectionCheckBox.Checked = loadData.IsProtection;

			// ネットワーク提出先のロード
			this.submissionPathTextBox.Text = loadData.SubmissionPath;
		}


		/// <summary>
		/// "画面とサウンド" タブのコントロールの状態から、指定した構成設定データへ各設定情報を書き込む。
		/// </summary>
		/// <param name="saveData">保存先の構成設定データ。</param>
		private void TabPage5_Save(ConfigurationData saveData)
		{
			// 動作モードの設定のセーブ
			if (this.operationModeNomalRadioButton.Checked) saveData.MuphicOperationMode = 0;
			else if (this.operationModeStudentRadioButton.Checked) saveData.MuphicOperationMode = 1;
			else if (this.operationModeTeacherRadioButton.Checked) saveData.MuphicOperationMode = 2;
			else saveData.MuphicOperationMode = 0;

			// 児童用の設定のセーブ
			saveData.IsProtection = this.isShiftkeyProtectionCheckBox.Checked;

			// ネットワーク提出先のセーブ
			saveData.SubmissionPath = this.submissionPathTextBox.Text;
		}


		/// <summary>
		/// "規定値に戻す" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void operationInfoTabResetButton_Click(object sender, EventArgs e)
		{
			if (!this.ConfirmReset()) return;

			this.TabPage5_Load(new ConfigurationData());

		}

		#endregion


		#region 動作モード

		/// <summary>
		/// 動作モードに応じ、各ラジオボタンの状態を変更する。
		/// </summary>
		/// <param name="operationMode"></param>
		private void SetOperationMode(int operationMode)
		{
			switch (operationMode)
			{
				case 0:
					this.operationModeNomalRadioButton.Checked = true;
					break;

				case 1:
					this.operationModeStudentRadioButton.Checked = true;
					break;

				case 2:
					this.operationModeTeacherRadioButton.Checked = true;
					break;

				default:
					goto case 0;
			}

			this.SetEnabledStudentGroupbox();
		}

		/// <summary>
		/// "通常動作" ラジオボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void operationModeNomalRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (operationModeNomalRadioButton.Checked) this.SetEnabledStudentGroupbox();
		}

		/// <summary>
		/// "通常動作" ラジオボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void operationModeStudentRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (operationModeStudentRadioButton.Checked) this.SetEnabledStudentGroupbox();
		}

		/// <summary>
		/// "通常動作" ラジオボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void operationModeTeacherRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (operationModeTeacherRadioButton.Checked) this.SetEnabledStudentGroupbox();
		}


		/// <summary>
		/// 動作モードの設定に応じ、児童用の設定のグループボックスの有効 / 無効を切り替える。
		/// </summary>
		private void SetEnabledStudentGroupbox()
		{
			this.studentGroupBox.Enabled = this.operationModeStudentRadioButton.Checked;
		}

		/// <summary>
		/// 動作モードに関するコントロールにマウスが入力された際に、ヘルプテキストを更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OperationModeControls_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescription(
				Properties.Resources.HelpText_ConfWindow_OperationMode_Title,
				Properties.Resources.HelpText_ConfWindow_OperationMode
			);
		}

		#endregion


		#region 児童用の設定

		#region ネットワーク

		#region ファイル提出先

		/// <summary>
		/// 曲の保存先の "参照" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SubmissionPathSelectButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();	// FolderBrowserDialogクラスのインスタンスを作成

			fbd.Description = Properties.Resources.DialogTitle_SubmissionPath;		// 上部に表示する説明テキストを指定

			fbd.RootFolder = Environment.SpecialFolder.Desktop;		// ルートフォルダを指定する (デフォルトでDesktop)

			if (Directory.Exists(this.submissionPathTextBox.Text))					// 初期状態で選択されているフォルダは、
			{																		// テキストボックスで指定されているフォルダを指定
				fbd.SelectedPath = this.submissionPathTextBox.Text;
			}
			else if (Directory.Exists(this.Current.SubmissionPath_DefaultValue))	// 上記がダメなら、
			{																		// 保存先のデフォルト値のパスを指定
				fbd.SelectedPath = this.Current.SubmissionPath_DefaultValue;
			}
			else																	// 上記２つがダメなら
			{																		// 仕方ないのでマイコンピュータを指定
				fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			}

			fbd.ShowNewFolderButton = false;						// ユーザーが新しいフォルダを作成できないようにする

			if (fbd.ShowDialog(this) == DialogResult.OK)			// ダイアログを表示する
			{														// OK ボタンで決定された場合のみ、テキストボックスにパスをコピー
				this.submissionPathTextBox.Text = fbd.SelectedPath;
			}

		}

		#endregion

		#endregion

		#endregion


		#region MouseEnter

		/// <summary>
		/// "動作モード" タブにマウスが入力された際に、ヘルプテキストをデフォルトに更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPage5_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescriptionDefault();
		}

		#endregion

	}
}
