using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using ConfigurationTool.Manager;

namespace ConfigurationTool
{
	/// <summary>
	/// muphic の動作設定ウィンドウ。
	/// </summary>
	public partial class ConfigurationWindow : Form
	{

		#region プロパティ

		/// <summary>
		/// 編集対象の構成設定データを取得する。
		/// </summary>
		private ConfigurationData Current
		{
			get { return ConfigurationManager.Current; }
		}

		/// <summary>
		/// ConfigurationTool の設定を取得する。。
		/// </summary>
		private Properties.Settings Settings
		{
			get { return Properties.Settings.Default; }
		}

		#endregion


		#region コンストラクタ

		/// <summary>
		/// 動作設定ウィンドウの新しいインスタンスを初期化する。
		/// </summary>
		public ConfigurationWindow()
			: this(true)
		{
		}

		/// <summary>
		/// 動作設定ウィンドウの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="enabledMuphicLaunchButton">muphic 起動ボタンを有効にする場合は true、それ以外は false。</param>
		public ConfigurationWindow(bool enabledMuphicLaunchButton)
		{
			// フォーム上でハンドルされない例外が発生した場合の処理の登録
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

			InitializeComponent();

			// フォームの表示位置を設定
			if (this.Current.ConfigurationToolWindowLocation.IsEmpty)
			{
				// デフォルト値のままだったら、画面中央に表示
				this.StartPosition = FormStartPosition.CenterScreen;
			}
			else
			{
				// デフォルト値以外だったら、前回保存された任意の位置に表示
				this.StartPosition = FormStartPosition.Manual;
				this.Location = this.Current.ConfigurationToolWindowLocation;
			}

			// 設定項目説明用の太字フォントを設定
			this.__descriptionBoldFont = new Font(
				this.textBoxDescription.SelectionFont.FontFamily,
				this.textBoxDescription.SelectionFont.Size,
				this.textBoxDescription.SelectionFont.Style | FontStyle.Bold
			);

			// 設定項目説明用の通常フォントを設定
			this.__descriptionRegularFont = new Font(
				this.textBoxDescription.SelectionFont.FontFamily,
				this.textBoxDescription.SelectionFont.Size,
				this.textBoxDescription.SelectionFont.Style | FontStyle.Regular
			);

			this.UpdateTextBoxDescriptionDefault();

			this.Load();


			#region イベント登録

			// TabPage1 イベント登録
			this.systemInfoTabPage.MouseEnter += new EventHandler(this.TabPage1_MouseEnter);

			// TabPage2 イベント登録
			this.muphicInfoTabPage.MouseEnter += new EventHandler(this.TabPage2_MouseEnter);
			this.operationModeGroupBox.MouseEnter += new EventHandler(this.OperationModeControls_MouseEnter);
			this.autoSaveGroupBox.MouseEnter += new EventHandler(this.AutoSaveControls_MouseEnter);
			this.printGroupBox.MouseEnter += new EventHandler(this.PrintGroupBox_MouseEnter);
			this.limitModeGroupBox.MouseEnter += new EventHandler(this.LimitModeGroupBox_MouseEnter);

			// TabPage3 イベント登録
			this.pathInfoTabPage.MouseEnter += new EventHandler(this.TabPage3_MouseEnter);
			this.savePathGroupBox.MouseEnter += new EventHandler(this.SavePathControls_MouseEnter);
			this.loggingGroupBox.MouseEnter += new EventHandler(this.LoggingControls_MouseEnter);
			this.canUseScoreSaveInMakeStory.MouseEnter += new EventHandler(this.CanUseSavedScoreInMakeStory_MouseEnter);

			// TabPage5 イベント登録
			this.operationInfoTabPage.MouseEnter += new EventHandler(this.TabPage5_MouseEnter);

			#endregion

			#region 未実装のため設定できない項目
			this.isLimitedLinkScrCheckBox.Enabled = false;
			this.isLimitedTutorialScrCheckBox.Enabled = false;
			#endregion

			this.muphicStartButton.Visible = this.muphicStartButton.Visible && enabledMuphicLaunchButton;

			this.DialogResult = DialogResult.None;
		}

		#endregion


		#region 構成設定の読み込みと書き込み

		/// <summary>
		/// 構成設定データの設定情報に応じ、構成設定ウィンドウの各コントロールの状態を変更する。
		/// </summary>
		/// <returns>書込が正常に行われた場合は true、それ以外は false。</returns>
		private new bool Load()
		{
			this.TabPage1_Load(this.Current);
			this.TabPage2_Load(this.Current);
			this.TabPage3_Load(this.Current);
			this.TabPage4_Load();
			this.TabPage5_Load(this.Current);

			return true;
		}


		/// <summary>
		/// 構成設定ウィンドウの各コントロールの状態に応じ、構成設定データへ設定情報の書き込みを行う。
		/// </summary>
		/// <returns>書き込みが正常に行われた場合は true、それ以外は false。</returns>
		private bool Save()
		{
			this.TabPage1_Save(this.Current);
			this.TabPage2_Save(this.Current);
			this.TabPage3_Save(this.Current);
			this.TabPage5_Save(this.Current);

			this.Current.ConfigurationToolWindowLocation = this.Location;
			ConfigurationManager.Save();

			return true;
		}

		#endregion


		#region 例外処理

		/// <summary>
		/// ハンドルされなかった例外が発生した際のイベント。
		/// <para>このメソッドが呼ばれると、例外に関するエラーメッセージが表示され、プログラムが終了する。</para>
		/// </summary>
		/// <param name="sender">。</param>
		/// <param name="e">。</param>
		void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			try
			{
				string errorlogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Properties.Settings.Default.ErrorLogFileName;

				MessageBox.Show(
					Properties.Resources.ErrorMsg_Show_ThreadException.Replace("%str1%", e.Exception.Message).Replace("%str2%", errorlogPath),
					Properties.Resources.ErrorMsg_Show_CommonCaption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				File.AppendAllText(errorlogPath, Properties.Resources.ErrorMsg_Log_ThreadException.Replace("%str1%", DateTime.Now.ToString()).Replace("%str2%", e.Exception.ToString()));

				this.Close();
			}
			catch (Exception)
			{
				Application.Exit();
			}
		}

		#endregion


		#region ウィンドウ共通

		/// <summary>
		/// "muphic 起動"ボタンが押された際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void muphicStartButton_Click(object sender, EventArgs e)
		{
			// 設定の適用
			this.Save();

			// ダイアログ結果を Retry に設定し (muphic が起動される)、ウィンドウを閉じる。
			this.DialogResult = DialogResult.Retry;
			this.Close();
		}

		/// <summary>
		/// OK ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void okButton_Click(object sender, EventArgs e)
		{
			// 設定の適用
			this.Save();

			this.ReportSave();

			// ダイアログ結果を OK に設定し、ウィンドウを閉じる
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// キャンセルボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			// メッセージボックスで終了の確認を行う。
			if (!this.ConfirmCancel()) return;

			// ダイアログ結果を Cancel に設定し、ウィンドウを閉じる
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// 適用ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void applyButton_Click(object sender, EventArgs e)
		{
			// 設定の適用
			this.Save();

			this.ReportSave();
		}

		#endregion


		#region メッセージボックス

		/// <summary>
		/// 構成設定の変更をユーザーに報告する。
		/// </summary>
		/// <returns></returns>
		private void ReportSave()
		{
			MessageBox.Show(
				this,
				Properties.Resources.Message_Show_ReportSave,
				Properties.Resources.Message_Show_ReportSave_Caption,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			);
		}


		/// <summary>
		/// 構成設定を変更せず終了するかどうかをユーザに問い合わせる。
		/// </summary>
		private bool ConfirmCancel()
		{
			var result = MessageBox.Show(
				this,
				Properties.Resources.Message_Show_ConfirmCancel,
				Properties.Resources.Message_Show_ConfirmCancel_Caption,
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question
			);

			return result == DialogResult.OK;
		}

		/// <summary>
		/// 初期値に戻す確認ダイアログを表示し、その結果を返す。
		/// </summary>
		/// <returns>ユーザーが OK を選択した場合は true、それ以外は false。</returns>
		private bool ConfirmReset()
		{
			var result = MessageBox.Show(
				this,
				Properties.Resources.Message_Show_ConfirmReset,
				Properties.Resources.Message_Show_ConfirmReset_Caption,
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question
			);

			return result == DialogResult.OK;
		}

		#endregion


		#region 設定項目説明テキスト関連

		/// <summary>
		/// 設定項目説明テキストの、タイトルに使用される太字フォント。
		/// </summary>
		private readonly Font __descriptionBoldFont;
		/// <summary>
		/// 設定項目説明テキストの、タイトルに使用される太字フォントを取得する。
		/// </summary>
		private Font DescriptionBoldFont
		{
			get { return this.__descriptionBoldFont; }
		}


		/// <summary>
		/// 設定項目説明テキストの、タイトルに使用される太字フォント。
		/// </summary>
		private readonly Font __descriptionRegularFont;
		/// <summary>
		/// 設定項目説明テキストの、説明文に使用されるフォントを取得または設定する。
		/// </summary>
		private Font DescriptionRegularFont
		{
			get { return this.__descriptionRegularFont; }
		}

		/// <summary>
		/// 設定項目説明テキストの項目名と説明文を結合した文字列を取得または設定する。
		/// <para>現在表示されている説明テキストと、新たに更新されるテキストが同一であるかどうかを判別する際に使用する。</para>
		/// </summary>
		private string DescriptionText { get; set; }

		/// <summary>
		/// 与えられた項目名と説明文を使用し、設定項目説明テキストを更新する。
		/// </summary>
		/// <param name="title">設定項目名</param>
		/// <param name="message">説明テキスト</param>
		private void UpdateTextBoxDescription(string title, string message)
		{
			if (this.DescriptionText != title + message)
			{
				// 現在の説明テキストの内容を破棄
				this.textBoxDescription.Clear();

				// タイトルを太字フォントで追加
				this.textBoxDescription.SelectionFont = this.DescriptionBoldFont;
				this.textBoxDescription.SelectedText = title + "\r\n";

				// 説明文を通常フォントで追加
				this.textBoxDescription.SelectionFont = this.DescriptionRegularFont;
				this.textBoxDescription.SelectedText = message;

				// 更新の必要性有無の判別用テキスト更新
				this.DescriptionText = title + message;
			}
		}

		/// <summary>
		/// 設定項目説明テキストを、規定の説明文で初期化する。
		/// </summary>
		private void UpdateTextBoxDescriptionDefault()
		{
			this.UpdateTextBoxDescription(
				Properties.Resources.HelpText_ConfWindow_Default_Title,
				Properties.Resources.HelpText_ConfWindow_Default
			);
		}

		#endregion

	}
}
