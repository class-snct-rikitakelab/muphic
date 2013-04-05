
namespace Muphic.Settings
{
	/// <summary>
	/// muphic で使用するリソース名を取得するための静的なプロパティ及びメソッドを提供する。
	/// </summary>
	public sealed class ResourceNames
	{
		private ResourceNames() { }


		#region 画像ファイル名

		/// <summary>
		/// システム画像として使用する画像ファイル名を取得する。
		/// </summary>
		public static string SystemImages
		{
			get
			{
				string images = "";
				images += @"Images_Common_System.png; ";
				images += @"Images_Common_Dialog.png; ";
				images += @"Images_Buttons.png; ";
				images += ResourceNames.CharacterImages;

				return images;
			}
		}


		/// <summary>
		/// 文字として使用する画像ファイル名を取得する。
		/// </summary>
		public static string CharacterImages
		{
			get
			{
				string images = "";
				images += @"Images_Characters.png; ";

				return images;
			}
		}


		/// <summary>
		/// トップ画面で使用する画像ファイル名を取得する。
		/// </summary>
		public static string TopScreenImages
		{
			get
			{
				string images = "";
				images += @"Images_TopScreen_Backgrounds.png; ";
				images += @"Images_TopScreen_Buttons.png; ";

				return images;
			}
		}


		/// <summary>
		/// 汎用作曲画面で使用する画像ファイル名を取得する。
		/// </summary>
		public static string CompositionScreenImages
		{
			get
			{
				string images = "";
				images += @"ConsolidatedImage_CompositionScreen.png; ";

				return images;
			}
		}


		/// <summary>
		/// 汎用題名入力画面で使用する画像ファイル名を取得する。
		/// </summary>
		public static string EntitleScreenImages
		{
			get
			{
				string images = " ";
				images += @"Images_EntitleScreen_Backgrounds.png; ";
				images += @"Images_EntitleScreen_Buttons.png; ";

				return images;
			}
		}


		/// <summary>
		/// 楽譜画面で使用する画像ファイル名を取得する。
		/// </summary>
		public static string ScoreScreenImages
		{
			get
			{
				string images = "";
				images += @"Images_ScoreScreen_Backgrounds.png; ";
				images += @"Images_ScoreScreen_Buttons.png; ";
				images += @"Images_ScoreScreen_Notes.png; ";
				images += @"Images_ScoreScreen_Dialogs.png; ";
				images += @"Images_Common_Animals.png; ";

				return images;
			}
		}


		/// <summary>
		/// ものがたりおんがくモードで使用する画像ファイル名を取得する。
		/// </summary>
		public static string MakeStoryScreenImages
		{
			get
			{
				string images = "";
				images += @"ConsolidatedImage_MakeStoryScreen_1.png; ";
				images += @"ConsolidatedImage_MakeStoryScreen_2.png; ";
				images += @"Images_MakeStoryScreen_Dialogs.png; ";
				images += @"Images_MakeStoryScreen_Buttons.png; ";

				if (MainWindow.MuphicOperationMode == MuphicOperationMode.TeacherMode)
				{
					images += ResourceNames.ExplorerImages;
				}

				return images;
			}
		}


		/// <summary>
		/// 成果物管理画面で使用する画像ファイル名を取得する。
		/// </summary>
		public static string ExplorerImages
		{
			get
			{
				string images = "";
				images += @"Images_Explorer.png; ";
				images += @"Images_Common_Dialog.png; ";

				return images;
			}
		}


		/// <summary>
		/// 物語再生画面で使用する画像ファイル名を取得する。
		/// </summary>
		public static string PlayStoryScreenImages
		{
			get
			{
				string images = "";
				images += @"ConsolidatedImage_PlayStoryScreen.png; ";

				return images;
			}
		}



		///// <summary>
		///// で使用する画像ファイル名を取得する。
		///// </summary>
		//public static string ScreenImages
		//{
		//    get
		//    {
		//        string images = "";
		//        images += @"";
		//        images += @"";
		//        images += @"";
		//        images += @"";
		//        images += @"";

		//        return images;
		//    }
		//}

		#endregion


		#region 音声ファイル

		/// <summary>
		/// 音声ファイル名を取得する。%str1% を動物名、%str2% を音階に置換して使用する。
		/// </summary>
		public static string SoundFile
		{
			get
			{
				return "Sound_%str1%%str2%.wav";
			}
		}

		#endregion


		#region ログファイル

		/// <summary>
		/// エラーログのファイル名を取得する。
		/// </summary>
		public static string ErrorLogFile
		{
			get
			{
				return "muphic_error.log";
			}
		}

		#endregion


		#region 設定ファイル

		/// <summary>
		/// デフォルトの構成設定ファイル名を取得する。
		/// </summary>
		public static string DefaultConfigurationFile
		{
			get { return "default.settings"; }
		}

		/// <summary>
		/// 講師用モード時のデフォルトの構成設定ファイル名を取得する。
		/// </summary>
		public static string TeacherModeDefaultConfigurationFile
		{
			get { return "default_teacher.settings"; }
		}

		/// <summary>
		/// 児童用モード時のデフォルトの構成設定ファイル名を取得する。
		/// </summary>
		public static string StudentModeDefaultConfigurationFile
		{
			get { return "default_student.settings"; }
		}

		/// <summary>
		/// 通常モード時のデフォルトの構成設定ファイル名を取得する。
		/// </summary>
		public static string NormalModeDefaultConfigurationFile
		{
			get { return "default_normal.settings"; }
		}


		/// <summary>
		/// テクスチャ名リストファイルのファイル名を取得する。
		/// </summary>
		public static string TextureNameListFile
		{
			get { return "ConsolidatedImages.texturelist"; }
		}

		#endregion


		#region アーカイブファイル

		/// <summary>
		/// アーカイブファイルのファイルパスを取得する。
		/// </summary>
		public static string ArchiveFilePath
		{
			get { return @"..\Archive\data01.dat"; }
		}

		#endregion


		#region 自動保存ファイル

		/// <summary>
		/// ひとりでおんがくモードの自動保存ファイルのファイルパスを取得する。
		/// </summary>
		public static string OneScreenAutoSaveFilePath
		{
			get { return @"AutoSave\OneScreen"; }
		}

		/// <summary>
		/// ものがたりおんがくモードの自動保存ファイルのファイルパスを取得する。
		/// </summary>
		public static string StoryScreenAutoSaveFilePath
		{
			get { return @"AutoSave\StoryScreen"; }
		}

		/// <summary>
		/// プレイヤー名の自動保存ファイルのファイルパスを取得する。
		/// </summary>
		public static string PlayerNameAutoSaveFilePath
		{
			get { return @"AutoSave\PlayerName"; }
		}

		#endregion


		#region 起動に必要なファイル

		/// <summary>
		/// 起動時に存在チェックを行うファイルパスと、エラー表示時のファイルの説明のための文字列群を取得する。
		/// 配列の各要素毎に、ファイル名とそのファイルの説明を | で区切った文字列が格納されている。
		/// </summary>
		public static string[] EssentialFiles
		{
			get
			{
				string[] images = new string[] {
					"..\\Archive\\data01.dat|アーカイブファイル", 
					"JpnKanaConversion.dll|必須ライブラリ",
					"JpnKanaConvHelper.dll|必須ライブラリ"
				};

				return images;
			}
		}

		#endregion

	}
}
