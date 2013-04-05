using System.Diagnostics;
using System.IO;

using Muphic.Configuration;
using Muphic.Tools.IO;

namespace Muphic.Manager
{
	/// <summary>
	/// 構成設定管理クラス (シングルトン・継承不可) 
	/// <para>ユーザーによって変更可能な muphic の構成設定の管理を行う。</para>
	/// </summary>
	public sealed class ConfigurationManager : Manager
	{
		/// <summary>
		/// 構成設定管理クラスの静的インスタンス (シングルトンパターン)。
		/// </summary>
		private static ConfigurationManager __instance = new ConfigurationManager();

		/// <summary>
		/// 構成設定管理クラスの静的インスタンス (シングルトンパターン) を取得する。
		/// </summary>
		private static ConfigurationManager Instance
		{
			get { return Muphic.Manager.ConfigurationManager.__instance; }
		}


		#region プロパティ

		/// <summary>
		/// 現在の muphic 構成設定を取得する。
		/// </summary>
		private ConfigurationData _Current { get; set; }

		/// <summary>
		/// 現在の muphic 構成設定を取得する。
		/// </summary>
		public static ConfigurationData Current
		{
			get { return Muphic.Manager.ConfigurationManager.Instance._Current; }
		}


		/// <summary>
		/// ロードしてから変更が加えられていない状態の muphic 構成設定を取得する。
		/// </summary>
		private ConfigurationData _Locked { get; set; }

		/// <summary>
		/// ロードしてから変更が加えられていない状態の muphic 構成設定を取得する。
		/// </summary>
		public static ConfigurationData Locked
		{
			get { return Muphic.Manager.ConfigurationManager.Instance._Locked; }
		}

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// 構成設定管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private ConfigurationManager()
		{
		}
		

		/// <summary>
		/// 構成設定管理クラスの静的インスタンス生成及び使用する構成設定のロードを行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>初期化が正常に終了した場合は true、それ以外は false。</returns>
		private bool _Initialize()
		{
			if (this._IsInitialized) return false;

			// 構成設定ファイルの存在チェック or 書き込み
			// 構成設定ファイルが存在せず、かつここで書き込めなかった場合は起動失敗扱い
			if (!this.SetConfigurationDataFile()) return false;

			this._Current = this._LoadConfigurationData();
			this._Locked = (ConfigurationData)Tools.CommonTools.DeepCopy(this._Current);

			Tools.DebugTools.ConsolOutputMessage("ConfigurationManager -Initialize", "構成設定管理クラス生成完了", false);

			return this._IsInitialized = true;
		}

		#endregion


		#region 設定の読込

		/// <summary>
		/// 規定のパスに構成設定ファイルが存在するかを確認し、存在しない場合は設定ファイルを書き込む。
		/// </summary>
		/// <returns>規定のパスに構成設定ファイルが (新たに書き込まれた場合を含め) 存在する場合は true、それ以外は false。</returns>
		private bool SetConfigurationDataFile()
		{
			// 既に構成設定ファイルが存在していた場合は正常終了
			if (File.Exists(ConfigurationData.ConfigurationDataPath)) return true;

			// ファイルが存在していなかった場合、初回起動としてウィンドウを表示
			using (var flw = new Muphic.SubForms.FirstLaunchWindow())
			{
				flw.ShowDialog();

				#region [キャンセル] ボタンが押された場合

				// 構成設定ファイルが存在しないので false 
				if (flw.DialogResult == System.Windows.Forms.DialogResult.Cancel) return false;

				#endregion

				#region [muphic 起動] ボタンが押された場合

				if (flw.DialogResult == System.Windows.Forms.DialogResult.OK)
				{
					// 動作モードに応じたデフォルト構成設定ファイルのファイル名を取得
					string fileName = this.GetDefaultConfigurationFileName(flw.SelectedMode);

					// 規定の構成設定ファイルパスに書き込み
					if (ArchiveFileManager.Exists(fileName))
					{
						XmlFileWriter.WriteSaveData<ConfigurationData>(
							XmlFileReader.ReadSaveData<ConfigurationData>(ArchiveFileManager.GetData(fileName), false),
							false,
							ConfigurationData.ConfigurationDataPath
						);

						return true;
					}
					else
					{					// ただし、デフォルト構成設定ファイルがアーカイブ内に無ければ
						return false;	// 構成設定ファイルが存在しないことになるので false
					}
				}

				#endregion

				#region [muphic 詳細設定] ボタンが押された場合

				if (flw.DialogResult == System.Windows.Forms.DialogResult.Retry)
				{
					// 同じディレクトリに [muphic 動作設定.exe] が存在するかを確認
					if (File.Exists(Settings.System.Default.MuphicConfigFileName))
					{
						// [muphic 動作設定.exe] が存在する場合、バージョン情報を取得
						var versionInfo = FileVersionInfo.GetVersionInfo(Settings.System.Default.MuphicConfigFileName);

						// プロダクト名から正規の [muphic 動作設定.exe] かどうかを判断し、正規のものであれば、動作設定ツールを起動する
						if (versionInfo.ProductName == Settings.System.Default.MuphicConfigProductName)
						{
							Process configuration = new Process();
							configuration.StartInfo = new ProcessStartInfo(Settings.System.Default.MuphicConfigFileName, "-firstLaunch");
							configuration.Start();
							configuration.WaitForExit();
						}
					}

					// 最後に、もう一度構成設定ファイルが存在するかを確認し、その結果を返す
					return File.Exists(ConfigurationData.ConfigurationDataPath);
				}

				#endregion
			}

			return false;
		}


		/// <summary>
		/// 構成設定ファイルを読み込む。
		/// </summary>
		private ConfigurationData _LoadConfigurationData()
		{
			// 構成設定ファイルが存在した場合はファイルから読込
			if (File.Exists(ConfigurationData.ConfigurationDataPath))
			{
				return XmlFileReader.ReadSaveData<ConfigurationData>(ConfigurationData.ConfigurationDataPath, false);
			}

			// ファイルが存在しなければ、アーカイブから初期設定を展開し読込
			else if (ArchiveFileManager.Exists(Settings.ResourceNames.DefaultConfigurationFile))
			{
				return XmlFileReader.ReadSaveData<ConfigurationData>(ArchiveFileManager.GetData(Settings.ResourceNames.DefaultConfigurationFile), false);
			}

			// アーカイブ内にも存在しなければ、新しくつくる
			else
			{
			    return new ConfigurationData();
			}
		}


		/// <summary>
		/// 指定した動作モードの、デフォルト構成設定ファイルのファイル名を返す。
		/// </summary>
		/// <param name="operationMode">muphic 動作モード。</param>
		/// <returns>デフォルト構成設定ファイルのファイル名。</returns>
		private string GetDefaultConfigurationFileName(MuphicOperationMode operationMode)
		{
			switch (operationMode)
			{
				case MuphicOperationMode.TeacherMode:
					return Settings.ResourceNames.TeacherModeDefaultConfigurationFile;

				case MuphicOperationMode.StudentMode:
					return Settings.ResourceNames.StudentModeDefaultConfigurationFile;

				case MuphicOperationMode.NormalMode:
					return Settings.ResourceNames.NormalModeDefaultConfigurationFile;

				default:
					return Settings.ResourceNames.DefaultConfigurationFile;
			}
		}


		#endregion


		#region 外部から呼ばれるメソッド群

		/// <summary>
		/// 構成設定管理クラスの静的インスタンス生成及び構成設定の読込を行う。
		/// インスタンス生成後に１度しか実行できない点に注意。
		/// </summary>
		/// <returns>初期化が正常に行われた場合は true、それ以外は false。</returns>
		public static bool Initialize()
		{
			return Muphic.Manager.ConfigurationManager.Instance._Initialize();
		}

		#endregion
	}
}
