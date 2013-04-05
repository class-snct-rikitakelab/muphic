using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Muphic.SubForms
{
	/// <summary>
	/// muphic 初回起動時に表示する、動作モード選択ウィンドウ。
	/// </summary>
	public partial class FirstLaunchWindow : Form
	{
		/// <summary>
		/// 初回起動時の動作モード選択ウィンドウの新しいインスタンスを初期化する。
		/// </summary>
		public FirstLaunchWindow()
		{
			InitializeComponent();

			// デフォルトは通常モードだけど、ラジオボタンは全て選択されていない状態
			// [muphic 起動] ボタンは最初は無効状態になる
			this.SetEnabledLaunchButton();

			this.DialogResult = DialogResult.None;
			this.SelectedMode = MuphicOperationMode.NormalMode;

			// 表示位置は画面中央
			this.StartPosition = FormStartPosition.CenterScreen;

			#region muphic 動作設定ツールの確認

			// 同じディレクトリに [muphic 動作設定.exe] が存在するかを確認
			if (File.Exists(Settings.System.Default.MuphicConfigFileName))
			{
				// [muphic 動作設定.exe] が存在する場合、バージョン情報を取得
				var versionInfo = FileVersionInfo.GetVersionInfo(Settings.System.Default.MuphicConfigFileName);

				// プロダクト名から正規の [muphic 動作設定.exe] かどうかを判断し、
				// 正規のものでなければ [muphic 詳細設定] ボタンを無効にする
				if (versionInfo.ProductName != Settings.System.Default.MuphicConfigProductName)
				{
					this.configurationButton.Enabled = false;
				}
			}
			else
			{
				this.configurationButton.Enabled = false;
			}

			#endregion
		}

		/// <summary>
		/// ユーザーに選択された動作モードを示す値を取得する。
		/// </summary>
		public MuphicOperationMode SelectedMode { get; private set; }


		#region ラジオボタン動作

		/// <summary>
		/// 動作モード選択ラジオボタンの状態に応じ、[muphic 起動] ボタンの有効 / 無効の切り替えを行う。
		/// </summary>
		private void SetEnabledLaunchButton()
		{
			this.launchButton.Enabled = (this.teacherModeRadioButton.Checked || this.studentModeRadioButton.Checked || this.normalModeRadioButton.Checked);
		}

		/// <summary>
		/// [授業用（講師）モード] ラジオボタンのチェックが変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TeacherModeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.SelectedMode = MuphicOperationMode.TeacherMode;
			this.SetEnabledLaunchButton();
		}

		/// <summary>
		/// [授業用（児童）モード] ラジオボタンのチェックが変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StudentModeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.SelectedMode = MuphicOperationMode.StudentMode;
			this.SetEnabledLaunchButton();
		}

		/// <summary>
		/// [通常モード] ラジオボタンのチェックが変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NormalModeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.SelectedMode = MuphicOperationMode.NormalMode;
			this.SetEnabledLaunchButton();
		}

		#endregion


		#region ボタン動作

		/// <summary>
		/// [muphic 起動] ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void launchButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// [キャンセル] ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// [muphic 詳細設定] ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void configurationButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Retry;
			this.Close();
		}

		#endregion
	}
}
