using System;

namespace ConfigurationTool
{
	/// <summary>
	/// muphic の動作や機能の状態を保持する構成設定データクラス。
	/// </summary>
	[Serializable]
	public class ConfigurationData
	{

		#region コンストラクタ

		/// <summary>
		/// muphic 構成設定データクラスの新しいインスタンスを初期化する。
		/// </summary>
		public ConfigurationData()
		{
			this.MuphicVersion = this.MuphicVersion_DefaultValue;
			this.MuphicOperationMode = this.MuphicOperationMode_DefaultValue;

			this.AdapterNum = this.AdapterNum_DefaultValue;
			this.Fps = this.Fps_DefaultValue;
			this.IsWindow = this.IsWindow_DefaultValue;
			this.RefreshRate = this.RefreshRate_DefaultValue;
			this.IsLoadTextureFilePreliminarily = this.IsLoadTextureFilePreliminarily_DefaultValue;
			this.IsLogging = this.IsLogging_DefaultValue;
			this.IsLoggingWithFullPath = this.IsLoggingWithFullPath_DefaultValue;
			this.AutoSaveInterval = this.AutoSaveInterval_DefaultValue;
			this.DirectSoundVolume = this.DirectSoundVolume_DefaultValue;
			this.UserNum = this.UserNum_DefaultValue;

			this.EnabledOneScreen = this.EnabledOneScreen_DefaultValue;
			this.EnabledLinkScreen = this.EnabledLinkScreen_DefaultValue;
			this.EnabledStoryScreen = this.EnabledStoryScreen_DefaultValue;
			this.EnabledTutorial = this.EnabledTutorial_DefaultValue;
			this.EnabledVoice = this.EnabledVoice_DefaultValue;
			this.EnabledPrint = this.EnabledPrint_DefaultValue;
			this.EnabledNetwork = this.EnabledNetwork_DefaultValue;
			this.EnabledLimitMode = this.EnabledLimitMode_DefaultValue;
			this.IsProtection = this.IsProtection_DefaultValue;

			this.HarmonyNum = this.HarmonyNum_DefaultValue;
			this.IsUseEighthNote = this.IsUseEighthNote_DefaultValue;
			this.CompositionMaxLine = this.CompositionMaxLine_DefaultValue;
			this.EnabledStoryScoreSave = this.EnabledStoryScoreSave_DefaultValue;
			this.EnabledStoryScoreLoad = this.EnabledStoryScoreLoad_DefaultValue;
			this.PrinterName = this.PrinterName_DefaultValue;
			this.IsReversePrintNumber = this.IsReversePrintNumber_DefaultValue;

			this.LogFileSavePath = this.LogFileSavePath_DefaultValue;
			this.ScoreSaveFolder = this.ScoreSaveFolder_DefaultValue;
			this.StorySaveFolder = this.StorySaveFolder_DefaultValue;
			this.SubmissionPath = this.SubmissionPath_DefaultValue;

			this.WindowLocation = this.WindowLocation_DefaultValue;
			this.ConfigurationToolWindowLocation = this.ConfigurationToolWindowLocation_DefaultValue;

			this.Player1 = this.Player1_DefaultValue;
			this.Player2 = this.Player2_DefaultValue;
			this.Player1Gender = this.Player1Gender_DefaultValue;
			this.Player2Gender = this.Player2Gender_DefaultValue;
			this.EnabledPlayerSave = this.EnabledPlayerSave_DefaultValue;
			this.EnabledPlayerNameInputSafety = this.EnabledPlayerNameInputSafety_DefaultValue;
		}

		#endregion


		#region MuphicVersion

		/// <summary>
		/// この設定が生成された時点での muphic のバージョンを示す値を取得または設定する。
		/// </summary>
		public string MuphicVersion { get; set; }

		/// <summary>
		/// この設定が生成された時点での muphic のバージョンを示す値の初期値を取得する。
		/// </summary>
		public string MuphicVersion_DefaultValue
		{
			get { return "0.0.0.0"; }
		}

		/// <summary>
		/// この設定が生成された時点での muphic のバージョンを示す値の説明文を取得する。
		/// </summary>
		public string MuphicVersion_Summary
		{
			get { return "この設定が生成された時点での muphic のバージョン。"; }
		}

		#endregion

		#region MuphicOperationMode

		/// <summary>
		/// muphic の動作モードを示す値を取得または設定する。
		/// </summary>
		public int MuphicOperationMode { get; set; }

		/// <summary>
		/// muphic の動作モードを示す値の初期値を取得する。
		/// </summary>
		public int MuphicOperationMode_DefaultValue
		{
			get { return 1; }
		}

		/// <summary>
		/// muphic の動作モードを示す値の説明文を取得する。
		/// </summary>
		public string MuphicOperationMode_Summary
		{
			get { return "muphic の動作モード。"; }
		}

		#endregion


		#region AdapterNum

		/// <summary>
		/// 使用するアダプタの番号を示す値を取得または設定する。
		/// </summary>
		public int AdapterNum { get; set; }

		/// <summary>
		/// 使用するアダプタの番号を示す値の初期値を取得する。
		/// </summary>
		public int AdapterNum_DefaultValue
		{
			get { return 0; }
		}

		/// <summary>
		/// 使用するアダプタの番号を示す値の説明文を取得する。
		/// </summary>
		public string AdapterNum_Summary
		{
			get { return "使用するアダプタの番号。"; }
		}

		#endregion

		#region Fps

		/// <summary>
		/// 1 分間で描画されるフレーム数 (FPS) を示す値を取得または設定する。
		/// </summary>
		public int Fps { get; set; }

		/// <summary>
		/// 1 分間で描画されるフレーム数 (FPS) を示す値の初期値を取得する。
		/// </summary>
		public int Fps_DefaultValue
		{
			get { return 60; }
		}

		/// <summary>
		/// 1 分間で描画されるフレーム数 (FPS) を示す値の説明文を取得する。
		/// </summary>
		public string Fps_Summary
		{
			get { return "1 分間で描画されるフレーム数 (FPS) 。"; }
		}

		#endregion

		#region IsWindow

		/// <summary>
		/// ウィンドウモードで表示するかどうかを示す値を取得または設定する。
		/// <para>ウィンドウモードの場合は true、フルスクリーンモードの場合は false となる。</para>
		/// </summary>
		public bool IsWindow { get; set; }

		/// <summary>
		/// ウィンドウモードで表示するかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool IsWindow_DefaultValue
		{
			get { return false; }
		}

		#endregion

		#region RefreshRate

		/// <summary>
		/// リフレッシュレートを示す値を取得または設定する。
		/// </summary>
		public int RefreshRate { get; set; }

		/// <summary>
		/// リフレッシュレートを示す値の初期値を取得する。
		/// </summary>
		public int RefreshRate_DefaultValue
		{
			get { return 60; }
		}

		/// <summary>
		/// リフレッシュレートを示す値の説明文を取得する。
		/// </summary>
		public string RefreshRate_Summary
		{
			get { return "リフレッシュレート。"; }
		}

		#endregion

		#region IsLoadTextureFilePreliminarily

		/// <summary>
		/// 起動時にテクスチャを予めロードし、画面遷移時間を削減するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool IsLoadTextureFilePreliminarily { get; set; }

		/// <summary>
		/// 起動時にテクスチャを予めロードし、画面遷移時間を削減するかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool IsLoadTextureFilePreliminarily_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// 起動時にテクスチャを予めロードし、画面遷移時間を削減するかどうかを示す値の説明文を取得する。
		/// </summary>
		public string IsLoadTextureFilePreliminarily_Summary
		{
			get { return "起動時にテクスチャを予めロードし、画面遷移時間を削減するかどうか。"; }
		}

		#endregion

		#region IsLogging

		/// <summary>
		/// ログ出力機能を利用するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool IsLogging { get; set; }

		/// <summary>
		/// ログ出力機能を利用するかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool IsLogging_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// ログ出力機能を利用するかどうかを示す値の説明文を取得する。
		/// </summary>
		public string IsLogging_Summary
		{
			get { return "ログ出力機能を利用するかどうか。"; }
		}

		#endregion

		#region IsLoggingWithFullPath

		/// <summary>
		/// ログ出力時に、フルパス表示を有効にするかどうかを示す値を取得または設定する。
		/// </summary>
		public bool IsLoggingWithFullPath { get; set; }

		/// <summary>
		/// ログ出力時に、フルパス表示を有効にするかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool IsLoggingWithFullPath_DefaultValue
		{
			get { return false; }
		}

		/// <summary>
		/// ログ出力時に、フルパス表示を有効にするかどうかを示す値の説明文を取得する。
		/// </summary>
		public string IsLoggingWithFullPath_Summary
		{
			get { return "ログ出力時に、フルパス表示を有効にするかどうか。"; }
		}

		#endregion

		#region AutoSaveInterval

		/// <summary>
		/// 自動保存の間隔 (分単位) を示す値を取得または設定する。
		/// </summary>
		public int AutoSaveInterval { get; set; }

		/// <summary>
		/// 自動保存の間隔 (分単位) を示す値の初期値を取得する。
		/// </summary>
		public int AutoSaveInterval_DefaultValue
		{
			get { return 1; }
		}

		/// <summary>
		/// 自動保存の間隔 (分単位) を示す値の説明文を取得する。
		/// </summary>
		public string AutoSaveInterval_Summary
		{
			get { return "自動保存の間隔 (分単位) 。"; }
		}

		#endregion

		#region DirectSoundVolume

		/// <summary>
		/// サウンドのボリュームを示す値を取得または設定する。
		/// </summary>
		public int DirectSoundVolume { get; set; }

		/// <summary>
		/// サウンドのボリュームを示す値の初期値を取得する。
		/// </summary>
		public int DirectSoundVolume_DefaultValue
		{
			get { return 0; }
		}

		/// <summary>
		/// サウンドのボリュームを示す値の説明文を取得する。
		/// </summary>
		public string DirectSoundVolume_Summary
		{
			get { return "サウンドのボリューム。"; }
		}

		#endregion

		#region UserNum

		/// <summary>
		/// ソフトを利用する人数を示す値を取得または設定する。
		/// </summary>
		public int UserNum { get; set; }

		/// <summary>
		/// ソフトを利用する人数を示す値の初期値を取得する。
		/// </summary>
		public int UserNum_DefaultValue
		{
			get { return 2; }
		}

		/// <summary>
		/// ソフトを利用する人数を示す値の説明文を取得する。
		/// </summary>
		public string UserNum_Summary
		{
			get { return "ソフトを利用する人数。"; }
		}

		#endregion


		#region EnabledOneScreen

		/// <summary>
		/// ひとりでおんがくモードを利用できるかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledOneScreen { get; set; }

		/// <summary>
		/// ひとりでおんがくモードを利用できるかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledOneScreen_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// ひとりでおんがくモードを利用できるかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledOneScreen_Summary
		{
			get { return "ひとりでおんがくモードを利用できるかどうか。"; }
		}

		#endregion

		#region EnabledStoryScreen

		/// <summary>
		/// ものがたりおんがくモードが利用できるかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledStoryScreen { get; set; }

		/// <summary>
		/// ものがたりおんがくモードが利用できるかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledStoryScreen_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// ものがたりおんがくモードが利用できるかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledStoryScreen_Summary
		{
			get { return "ものがたりおんがくモードが利用できるかどうか。"; }
		}

		#endregion

		#region EnabledLinkScreen

		/// <summary>
		/// つなげておんがくモードを利用できるかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledLinkScreen { get; set; }

		/// <summary>
		/// つなげておんがくモードを利用できるかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledLinkScreen_DefaultValue
		{
			get { return false; }
		}

		/// <summary>
		/// つなげておんがくモードを利用できるかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledLinkScreen_Summary
		{
			get { return "つなげておんがくモードを利用できるかどうか。"; }
		}

		#endregion

		#region EnabledTutorial

		/// <summary>
		/// チュートリアル機能が利用できるかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledTutorial { get; set; }

		/// <summary>
		/// チュートリアル機能が利用できるかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledTutorial_DefaultValue
		{
			get { return false; }
		}

		/// <summary>
		/// チュートリアル機能が利用できるかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledTutorial_Summary
		{
			get { return "チュートリアル機能が利用できるかどうか。"; }
		}

		#endregion

		#region EnabledVoice

		/// <summary>
		/// 音声機能が有効かどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledVoice { get; set; }

		/// <summary>
		/// 音声機能が有効かどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledVoice_DefaultValue
		{
			get { return false; }
		}

		/// <summary>
		/// 音声機能が有効かどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledVoice_Summary
		{
			get { return "音声機能が有効かどうか。"; }
		}

		#endregion

		#region EnabledPrint

		/// <summary>
		/// 印刷機能を利用できるかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledPrint { get; set; }

		/// <summary>
		/// 印刷機能を利用できるかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledPrint_DefaultValue
		{
			get { return false; }
		}

		/// <summary>
		/// 印刷機能を利用できるかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledPrint_Summary
		{
			get { return "印刷機能を利用できるかどうか。"; }
		}

		#endregion

		#region EnabledNetwork

		/// <summary>
		/// ネットワーク機能を利用するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledNetwork { get; set; }

		/// <summary>
		/// ネットワーク機能を利用するかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledNetwork_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// ネットワーク機能を利用するかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledNetwork_Summary
		{
			get { return "ネットワーク機能を利用するかどうか。"; }
		}

		#endregion

		#region EnabledLimitMode

		/// <summary>
		/// 児童モードでの物語作曲画面で、作曲の制限機能を利用するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledLimitMode { get; set; }

		/// <summary>
		/// 児童モードでの物語作曲画面で、作曲の制限機能を利用するかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledLimitMode_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// 児童モードでの物語作曲画面で、作曲の制限機能を利用するかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledLimitMode_Summary
		{
			get { return "児童モードでの物語作曲画面で、作曲の制限機能を利用するかどうか。"; }
		}

		#endregion

		#region IsProtection

		/// <summary>
		/// Shift キーを利用した制限を行うかどうかを示す値を取得または設定する。
		/// </summary>
		public bool IsProtection { get; set; }

		/// <summary>
		/// Shift キーを利用した制限を行うかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool IsProtection_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// Shift キーを利用した制限を行うかどうかを示す値の説明文を取得する。
		/// </summary>
		public string IsProtection_Summary
		{
			get { return "Shift キーを利用した制限を行うかどうか。"; }
		}

		#endregion


		#region HarmonyNum

		/// <summary>
		/// 作曲中に作成可能な和音の音数を示す値を取得または設定する。
		/// </summary>
		public int HarmonyNum { get; set; }

		/// <summary>
		/// 作曲中に作成可能な和音の音数を示す値の初期値を取得する。
		/// </summary>
		public int HarmonyNum_DefaultValue
		{
			get { return 3; }
		}

		/// <summary>
		/// 作曲中に作成可能な和音の音数を示す値の説明文を取得する。
		/// </summary>
		public string HarmonyNum_Summary
		{
			get { return "作曲中に作成可能な和音の音数。"; }
		}

		#endregion

		#region IsUseEighthNote

		/// <summary>
		/// 八分音符を利用するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool IsUseEighthNote { get; set; }

		/// <summary>
		/// 八分音符を利用するかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool IsUseEighthNote_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// 八分音符を利用するかどうかを示す値の説明文を取得する。
		/// </summary>
		public string IsUseEighthNote_Summary
		{
			get { return "八分音符を利用するかどうか。"; }
		}

		#endregion

		#region CompositionMaxLine

		/// <summary>
		/// 作曲可能な小節数を示す値を取得または設定する。
		/// </summary>
		public int CompositionMaxLine { get; set; }

		/// <summary>
		/// 作曲可能な小節数を示す値の初期値を取得する。
		/// </summary>
		public int CompositionMaxLine_DefaultValue
		{
			get { return 8; }
		}

		/// <summary>
		/// 作曲可能な小節数を示す値の説明文を取得する。
		/// </summary>
		public string CompositionMaxLine_Summary
		{
			get { return "作曲可能な小節数。"; }
		}

		#endregion

		#region EnabledStoryScoreSave

		/// <summary>
		/// ものがたりおんがくモードの作曲画面で、ファイルの保存機能が利用できるかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledStoryScoreSave { get; set; }

		/// <summary>
		/// ものがたりおんがくモードの作曲画面で、ファイルの保存機能が利用できるかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledStoryScoreSave_DefaultValue
		{
			get { return false; }
		}

		/// <summary>
		/// ものがたりおんがくモードの作曲画面で、ファイルの保存機能が利用できるかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledStoryScoreSave_Summary
		{
			get { return "ものがたりおんがくモードの作曲画面で、ファイルの保存機能が利用できるかどうか。"; }
		}

		#endregion

		#region EnabledStoryScoreLoad

		/// <summary>
		/// ものがたりおんがくモードの作曲画面で、ファイルの読込機能が利用できるかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledStoryScoreLoad { get; set; }

		/// <summary>
		/// ものがたりおんがくモードの作曲画面で、ファイルの読込機能が利用できるかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledStoryScoreLoad_DefaultValue
		{
			get { return false; }
		}

		/// <summary>
		/// ものがたりおんがくモードの作曲画面で、ファイルの読込機能が利用できるかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledStoryScoreLoad_Summary
		{
			get { return "ものがたりおんがくモードの作曲画面で、ファイルの読込機能が利用できるかどうか。"; }
		}

		#endregion

		#region PrinterName

		/// <summary>
		/// 印刷に使用するプリンタ名を示す値を取得または設定する。
		/// </summary>
		public string PrinterName { get; set; }

		/// <summary>
		/// 印刷に使用するプリンタ名を示す値の初期値を取得する。
		/// </summary>
		public string PrinterName_DefaultValue
		{
			get { return ""; }
		}

		/// <summary>
		/// 印刷に使用するプリンタ名を示す値の説明文を取得する。
		/// </summary>
		public string PrinterName_Summary
		{
			get { return "印刷に使用するプリンタ名。"; }
		}

		#endregion

		#region IsReversePrintNumber

		/// <summary>
		/// 印刷時に、最後のページから順に印刷するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool IsReversePrintNumber { get; set; }

		/// <summary>
		/// 印刷時に、最後のページから順に印刷するかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool IsReversePrintNumber_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// 印刷時に、最後のページから順に印刷するかどうかを示す値の説明文を取得する。
		/// </summary>
		public string IsReversePrintNumber_Summary
		{
			get { return "印刷時に、最後のページから順に印刷するかどうか。"; }
		}

		#endregion


		#region LogFileSavePath

		/// <summary>
		/// ログファイルの保存先を示す値を取得または設定する。
		/// </summary>
		public string LogFileSavePath { get; set; }

		/// <summary>
		/// ログファイルの保存先を示す値の初期値を取得する。
		/// </summary>
		public string LogFileSavePath_DefaultValue
		{
			get { return ConfigurationData.UserDataFolder + "\\muphic.log"; }
		}

		/// <summary>
		/// ログファイルの保存先を示す値の説明文を取得する。
		/// </summary>
		public string LogFileSavePath_Summary
		{
			get { return "ログファイルの保存先。"; }
		}

		#endregion

		#region ScoreSaveFolder

		/// <summary>
		/// 曲の保存先を示す値を取得または設定する。
		/// </summary>
		public string ScoreSaveFolder { get; set; }

		/// <summary>
		/// 曲の保存先を示す値の初期値を取得する。
		/// </summary>
		public string ScoreSaveFolder_DefaultValue
		{
			get { return ConfigurationData.UserDataFolder + "\\ScoreData"; }
		}

		/// <summary>
		/// 曲の保存先を示す値の説明文を取得する。
		/// </summary>
		public string ScoreSaveFolder_Summary
		{
			get { return "曲の保存先。"; }
		}

		#endregion

		#region StorySaveFolder

		/// <summary>
		/// 物語の保存先を示す値を取得または設定する。
		/// </summary>
		public string StorySaveFolder { get; set; }

		/// <summary>
		/// 物語の保存先を示す値の初期値を取得する。
		/// </summary>
		public string StorySaveFolder_DefaultValue
		{
			get { return ConfigurationData.UserDataFolder + "\\StoryData"; }
		}

		/// <summary>
		/// 物語の保存先を示す値の説明文を取得する。
		/// </summary>
		public string StorySaveFolder_Summary
		{
			get { return "物語の保存先。"; }
		}

		#endregion

		#region SubmissionPath

		/// <summary>
		/// 作品の提出先パスを示す値を取得または設定する。
		/// </summary>
		public string SubmissionPath { get; set; }

		/// <summary>
		/// 作品の提出先パスを示す値の初期値を取得する。
		/// </summary>
		public string SubmissionPath_DefaultValue
		{
			get { return System.IO.Path.Combine(@"\\Muphic-PC\ShareDocuments\muphic", System.Net.Dns.GetHostName()); }
		}

		/// <summary>
		/// 作品の提出先パスを示す値の説明文を取得する。
		/// </summary>
		public string SubmissionPath_Summary
		{
			get { return "作品の提出先パス。"; }
		}

		#endregion


		#region WindowLocation

		/// <summary>
		/// muphic のウィンドウ位置を示す値を取得または設定する。
		/// </summary>
		public System.Drawing.Point WindowLocation { get; set; }

		/// <summary>
		/// muphic のウィンドウ位置を示す値の初期値を取得する。
		/// </summary>
		public System.Drawing.Point WindowLocation_DefaultValue
		{
			get { return new System.Drawing.Point(10, 10); }
		}

		/// <summary>
		/// muphic のウィンドウ位置を示す値の説明文を取得する。
		/// </summary>
		public string WindowLocation_Summary
		{
			get { return "muphic のウィンドウ位置。"; }
		}

		#endregion

		#region ConfigurationToolWindowLocation

		/// <summary>
		/// 構成設定ツールのウィンドウ位置を示す値を取得または設定する。
		/// </summary>
		public System.Drawing.Point ConfigurationToolWindowLocation { get; set; }

		/// <summary>
		/// 構成設定ツールのウィンドウ位置を示す値の初期値を取得する。
		/// </summary>
		public System.Drawing.Point ConfigurationToolWindowLocation_DefaultValue
		{
			get { return System.Drawing.Point.Empty; }
		}

		/// <summary>
		/// 構成設定ツールのウィンドウ位置を示す値の説明文を取得する。
		/// </summary>
		public string ConfigurationToolWindowLocation_Summary
		{
			get { return "構成設定ツールのウィンドウ位置。"; }
		}

		#endregion


		#region Player1

		/// <summary>
		/// muphic 使用者 (1人目) を示す値を取得または設定する。
		/// </summary>
		public string Player1 { get; set; }

		/// <summary>
		/// muphic 使用者 (1人目) を示す値の初期値を取得する。
		/// </summary>
		public string Player1_DefaultValue
		{
			get { return ""; }
		}

		/// <summary>
		/// muphic 使用者 (1人目) を示す値の説明文を取得する。
		/// </summary>
		public string Player1_Summary
		{
			get { return "muphic 使用者 (1人目) 。"; }
		}

		#endregion

		#region Player2

		/// <summary>
		/// muphic 使用者 (2人目) を示す値を取得または設定する。
		/// </summary>
		public string Player2 { get; set; }

		/// <summary>
		/// muphic 使用者 (2人目) を示す値の初期値を取得する。
		/// </summary>
		public string Player2_DefaultValue
		{
			get { return ""; }
		}

		/// <summary>
		/// muphic 使用者 (2人目) を示す値の説明文を取得する。
		/// </summary>
		public string Player2_Summary
		{
			get { return "muphic 使用者 (2人目) 。"; }
		}

		#endregion

		#region Player1Gender

		/// <summary>
		/// プレイヤー1 の性別を示す値を取得または設定する。
		/// </summary>
		public int Player1Gender { get; set; }

		/// <summary>
		/// プレイヤー1 の性別を示す値の初期値を取得する。
		/// </summary>
		public int Player1Gender_DefaultValue
		{
			get { return 0; }
		}

		/// <summary>
		/// プレイヤー1 の性別を示す値の説明文を取得する。
		/// </summary>
		public string Player1Gender_Summary
		{
			get { return "プレイヤー1 の性別。"; }
		}

		#endregion

		#region Player2Gender

		/// <summary>
		/// プレイヤー2 の性別を示す値を取得または設定する。
		/// </summary>
		public int Player2Gender { get; set; }

		/// <summary>
		/// プレイヤー2 の性別を示す値の初期値を取得する。
		/// </summary>
		public int Player2Gender_DefaultValue
		{
			get { return 0; }
		}

		/// <summary>
		/// プレイヤー2 の性別を示す値の説明文を取得する。
		/// </summary>
		public string Player2Gender_Summary
		{
			get { return "プレイヤー2 の性別。"; }
		}

		#endregion

		#region EnabledPlayerSave

		/// <summary>
		/// muphic 使用者を保存するかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledPlayerSave { get; set; }

		/// <summary>
		/// muphic 使用者を保存するかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledPlayerSave_DefaultValue
		{
			get { return false; }
		}

		/// <summary>
		/// muphic 使用者を保存するかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledPlayerSave_Summary
		{
			get { return "muphic 使用者を保存するかどうか。"; }
		}

		#endregion

		#region EnabledPlayerNameInputSafety

		/// <summary>
		/// 児童用モードで、プレイヤー名が未入力の時に各画面に遷移するのを防ぐかどうかを示す値を取得または設定する。
		/// </summary>
		public bool EnabledPlayerNameInputSafety { get; set; }

		/// <summary>
		/// 児童用モードで、プレイヤー名が未入力の時に各画面に遷移するのを防ぐかどうかを示す値の初期値を取得する。
		/// </summary>
		public bool EnabledPlayerNameInputSafety_DefaultValue
		{
			get { return true; }
		}

		/// <summary>
		/// 児童用モードで、プレイヤー名が未入力の時に各画面に遷移するのを防ぐかどうかを示す値の説明文を取得する。
		/// </summary>
		public string EnabledPlayerNameInputSafety_Summary
		{
			get { return "児童用モードで、プレイヤー名が未入力の時に各画面に遷移するのを防ぐかどうか。"; }
		}

		#endregion


		#region 保存されないプロパティ

		/// <summary>
		/// ユーザー毎の設定が保存されるフォルダ (ユーザーフォルダ) のパスを取得する。
		/// 通常、このフォルダに構成設定やログ、作品のデータが保存される。
		/// </summary>
		public static string UserDataFolder
		{
			get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SNCT\\muphic"; }
		}

		/// <summary>
		/// この構成設定データが保存されるパスを取得する。
		/// </summary>
		public static string ConfigurationDataPath
		{
			get { return ConfigurationData.UserDataFolder + "\\" + "muphic.settings"; }
		}

		#endregion


		#region テンプレート

		///// <summary>
		///// ●●●を示す値。
		///// </summary>
		//private ▼▼▼ __setting;

		///// <summary>
		///// ●●●を示す値を取得または設定する。
		///// <para></para>
		///// </summary>
		//public ▼▼▼ ■■■
		//{
		//    get { return this.__setting; }
		//    set { if (this.EnabledDataSet) this.__setting = value; }
		//}

		///// <summary>
		///// ●●●を示す値の初期値を取得する。
		///// </summary>
		//public ▼▼▼ DefaultValue_■■■
		//{
		//    get { return ▲▲▲; }
		//}

		#endregion

	}
}
