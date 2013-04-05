using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace ConfigurationTool
{
	public partial class ConfigurationWindow : Form
	{
		// muphic 動作設定ウィンドウ
		// ページ３ "ファイルの保存先"タブの動作の記述


		#region 設定の読込と保存

		/// <summary>
		/// 指定した構成設定データから、"ファイルの保存先" タブのコントロールの状態を決定する。
		/// </summary>
		/// <param name="loadData">読み込む構成設定データ。</param>
		private void TabPage3_Load(ConfigurationData loadData)
		{
			// ログ機能のロード
			this.enabledLoggingCheckBox.Checked = loadData.IsLogging;
			this.SetEnabledLogging();

			// ログ保存先のロード
			this.loggingFilePathTextBox.Text = loadData.LogFileSavePath;


			// 曲保存先のロード
			this.scoreSavePathTextBox.Text = loadData.ScoreSaveFolder;
			this.ToggleScoreSaveLinkLabel();

			// 物語保存先のロード
			this.storySavePathTextBox.Text = loadData.StorySaveFolder;
			this.ToggleStorySaveLinkLabel();

			// ものがたりおんがくでの既存曲使用の設定のロード
			this.canUseScoreSaveInMakeStory.Checked = loadData.EnabledStoryScoreLoad;
		}


		/// <summary>
		/// "ファイルの保存先" タブのコントロールの状態から、指定した構成設定データへ各設定情報を書き込む。
		/// </summary>
		/// <param name="saveData">保存先の構成設定データ。</param>
		private void TabPage3_Save(ConfigurationData saveData)
		{
			// ログ機能のセーブ
			saveData.IsLogging = this.enabledLoggingCheckBox.Checked;
			saveData.LogFileSavePath = this.loggingFilePathTextBox.Text;

			// 曲保存先のセーブ
			saveData.ScoreSaveFolder = this.scoreSavePathTextBox.Text;

			// 物語保存先のセーブ
			saveData.StorySaveFolder = this.storySavePathTextBox.Text;

			// ものがたりおんがくでの既存曲使用の設定のセーブ
			saveData.EnabledStoryScoreLoad = this.canUseScoreSaveInMakeStory.Checked;
			saveData.EnabledStoryScoreSave = this.canUseScoreSaveInMakeStory.Checked;
		}


		/// <summary>
		/// "規定値に戻す" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPagePathInfoResetButton_Click(object sender, EventArgs e)
		{
			if (!this.ConfirmReset()) return;

			this.TabPage3_Load(new ConfigurationData());
		}

		#endregion


		#region ログ機能

		/// <summary>
		/// "ログ機能を利用する (推奨)" チェックボックスの状態が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IsLoggingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.SetEnabledLogging();
		}

		/// <summary>
		/// "ログ機能を利用する (推奨)" チェックボックスの状態に応じ、保存先テキストボックスと参照ボタンの有効 / 無効を切り替える。
		/// </summary>
		private void SetEnabledLogging()
		{
			this.loggingFilePathTextBox.Enabled = this.enabledLoggingCheckBox.Checked;
			this.loggingFileSelectButton.Enabled = this.enabledLoggingCheckBox.Checked;
			this.OpenLogFileLinkLabel.Enabled = this.enabledLoggingCheckBox.Checked && File.Exists(this.loggingFilePathTextBox.Text);
		}

		/// <summary>
		/// ログファイル保存先指定テキストボックスの値が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loggingFilePathTextBox_TextChanged(object sender, EventArgs e)
		{
			this.SetEnabledLogging();
		}

		/// <summary>
		/// "ログファイルを開く" リンクをクリックした際の処置。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenLogFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (File.Exists(this.loggingFilePathTextBox.Text))
			{
				Process.Start(this.loggingFilePathTextBox.Text);
			}
		}

		/// <summary>
		/// ログファイル保存先選択の "参照" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogFileSelectButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();					// SaveFileDialogクラスのインスタンスを作成

			sfd.FileName = Properties.Settings.Default.MuphicLogFileName;			// 初期状態のファイル名を指定する

			if (Directory.Exists(this.loggingFilePathTextBox.Text))					// 初期状態のフォルダは、テキストボックスで指定されている
			{																		// パスのフォルダを指定
				sfd.InitialDirectory = Path.GetDirectoryName(this.loggingFilePathTextBox.Text);
			}
			else if (Directory.Exists(this.Current.LogFileSavePath_DefaultValue))	// 上記のパスが存在しなかった場合、
			{																		// ログ保存先デフォルト値のパスが存在すればそのパスを設定
				sfd.InitialDirectory = Path.GetDirectoryName(this.Current.LogFileSavePath_DefaultValue);
			}
			else																	// 上記２つがダメなら
			{																		// 仕方ないのでマイドキュメントを指定
				sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}

			sfd.Filter = "ログファイル(*.log)|*.log";					// [ファイルの種類]に表示される選択肢を指定する
			sfd.FilterIndex = 1;										// [ファイルの種類]ではじめに「ログファイル」が選択されているようにする

			sfd.Title = Properties.Resources.DialogTitle_LogFileSave;	// タイトルを設定する

			sfd.RestoreDirectory = true;								// ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			sfd.OverwritePrompt = true;									// 既に存在するファイル名を指定したとき警告する
			sfd.CheckPathExists = true;									// 存在しないパスが指定されたとき警告を表示する

			if (sfd.ShowDialog() == DialogResult.OK)					// ダイアログを表示する
			{															// OK ボタンで決定された場合のみ、テキストボックスにパスをコピー
				this.loggingFilePathTextBox.Text = Path.GetFullPath(sfd.FileName);
			}
		}

		/// <summary>
		/// ロギングに関するコントロールにマウスが入力された際に、ヘルプテキストを更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoggingControls_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescription(
				Properties.Resources.HelpText_ConfWindow_Logging_Title,
				Properties.Resources.HelpText_ConfWindow_Logging
			);
		}

		#endregion


		#region 作品の保存

		#region 曲の保存

		/// <summary>
		/// 曲の保存先指定テキストボックスの値が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScoreSavePathTextBox_TextChanged(object sender, EventArgs e)
		{
			this.ToggleScoreSaveLinkLabel();
		}

		/// <summary>
		/// 曲の保存先指定テキストボックスのパスが存在する場合はリンクラベルを、存在しない場合は通常のラベルを表示する。
		/// </summary>
		private void ToggleScoreSaveLinkLabel()
		{
			bool isExisted = Directory.Exists(scoreSavePathTextBox.Text);

			this.openScoreSaveFolderLinkLabel.Visible = isExisted;
			this.scoreSaveLabel.Visible = !isExisted;
		}

		/// <summary>
		/// "曲の保存先" リンクがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenScoreSaveFolderLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (!Directory.Exists(scoreSavePathTextBox.Text)) return;

			Process.Start("EXPLORER.EXE", @"/n, " + scoreSavePathTextBox.Text);
		}

		/// <summary>
		/// 曲の保存先の "参照" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScoreSavePathSelectButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();	// FolderBrowserDialogクラスのインスタンスを作成

			fbd.Description = Properties.Resources.DialogTitle_ScoreDataSaveFolder;	// 上部に表示する説明テキストを指定

			fbd.RootFolder = Environment.SpecialFolder.Desktop;		// ルートフォルダを指定する (デフォルトでDesktop)

			if (Directory.Exists(this.scoreSavePathTextBox.Text))					// 初期状態で選択されているフォルダは、
			{																		// テキストボックスで指定されているフォルダを指定
				fbd.SelectedPath = this.scoreSavePathTextBox.Text;
			}
			else if(Directory.Exists(this.Current.ScoreSaveFolder_DefaultValue))	// 上記がダメなら、
			{																		// 保存先のデフォルト値のパスを指定
				fbd.SelectedPath = this.Current.ScoreSaveFolder_DefaultValue;
			}
			else if (Directory.Exists(ConfigurationData.UserDataFolder))			// 上記２つがダメなら、
			{																		// Roming の muphic フォルダを指定
				fbd.SelectedPath = ConfigurationData.UserDataFolder;
			}
			else																	// 上記全てがダメなら
			{																		// 仕方ないのでマイドキュメントを指定
				fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}

			fbd.ShowNewFolderButton = true;							// ユーザーが新しいフォルダを作成できるようにする

			if (fbd.ShowDialog(this) == DialogResult.OK)			// ダイアログを表示する
			{														// OK ボタンで決定された場合のみ、テキストボックスにパスをコピー
				this.scoreSavePathTextBox.Text = fbd.SelectedPath;		
			}

		}

		#endregion 

		#region 物語の保存

		/// <summary>
		/// 物語の保存先指定テキストボックスの値が変更された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StorySavePathTextBox_TextChanged(object sender, EventArgs e)
		{
			this.ToggleStorySaveLinkLabel();
		}
		
		/// <summary>
		/// 物語の保存先指定テキストボックスのパスが存在する場合はリンクラベルを、存在しない場合は通常のラベルを表示する。
		/// </summary>
		private void ToggleStorySaveLinkLabel()
		{
			bool isExisted = Directory.Exists(storySavePathTextBox.Text);

			this.openStorySaveFolderLinkLabel.Visible = isExisted;
			this.storySaveLabel.Visible = !isExisted;
		}

		/// <summary>
		/// "物語の保存先" リンクがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenStorySaveFolderLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (!Directory.Exists(storySavePathTextBox.Text)) return;

			Process.Start("EXPLORER.EXE", @"/n, " + storySavePathTextBox.Text);
		}

		/// <summary>
		/// 物語の保存先の "参照" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StorySavePathSelectButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();	// FolderBrowserDialogクラスのインスタンスを作成

			fbd.Description = Properties.Resources.DialogTitle_StoryDataSaveFolder;	// 上部に表示する説明テキストを指定

			fbd.RootFolder = Environment.SpecialFolder.Desktop;		// ルートフォルダを指定する (デフォルトでDesktop)

			if (Directory.Exists(this.storySavePathTextBox.Text))					// 初期状態で選択されているフォルダは、
			{																		// テキストボックスで指定されているフォルダを指定
				fbd.SelectedPath = this.storySavePathTextBox.Text;
			}
			else if (Directory.Exists(this.Current.StorySaveFolder_DefaultValue))	// 上記がダメなら、
			{																		// 保存先のデフォルト値のパスを指定
				fbd.SelectedPath = this.Current.StorySaveFolder_DefaultValue;
			}
			else if (Directory.Exists(ConfigurationData.UserDataFolder))			// 上記２つがダメなら、
			{																		// Roming の muphic フォルダを指定
				fbd.SelectedPath = ConfigurationData.UserDataFolder;
			}
			else																	// 上記全てがダメなら
			{																		// 仕方ないのでマイドキュメントを指定
				fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}

			fbd.ShowNewFolderButton = true;							// ユーザーが新しいフォルダを作成できるようにする

			if (fbd.ShowDialog(this) == DialogResult.OK)			// ダイアログを表示する
			{														// OK ボタンで決定された場合のみ、テキストボックスにパスをコピー
				this.storySavePathTextBox.Text = fbd.SelectedPath;
			}
		}


		/// <summary>
		/// 保存先に関するコントロールにマウスが入力された際に、ヘルプテキストを更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SavePathControls_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescription(
				Properties.Resources.HelpText_ConfWindow_SavePath_Title,
				Properties.Resources.HelpText_ConfWindow_SavePath
			);
		}

		/// <summary>
		/// 物語作曲時の既存曲利用に関するコントロールにマウスが入力された際に、ヘルプテキストを更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CanUseSavedScoreInMakeStory_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescription(
				Properties.Resources.HelpText_ConfWindow_CanUseSavedScoreInMakeStory_Title,
				Properties.Resources.HelpText_ConfWindow_CanUseSavedScoreInMakeStory
			);
		}

		#endregion

		#endregion


		#region MouseEnter

		/// <summary>
		/// "機能と作曲の動作"タブにマウスが入力された際に、ヘルプテキストをデフォルトに更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPage3_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateTextBoxDescriptionDefault();
		}

		#endregion
	}
}
