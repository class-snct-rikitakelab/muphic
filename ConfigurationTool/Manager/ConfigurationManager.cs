using System;
using System.IO;

using ConfigurationTool.Tools;
using ConfigurationTool.Tools.IO;

namespace ConfigurationTool.Manager
{
	/// <summary>
	/// 構成設定管理クラス (シングルトン・継承不可) 
	/// <para>ユーザーによって変更可能な muphic の構成設定の管理を行う。</para>
	/// </summary>
	public class ConfigurationManager
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
			get { return ConfigurationTool.Manager.ConfigurationManager.__instance; }
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
			get { return ConfigurationTool.Manager.ConfigurationManager.Instance._Current; }
		}

		/// <summary>
		/// 初期化済みであることを示す値を取得または設定する。
		/// </summary>
		private bool IsInitialized { get; set; }

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// 構成設定管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private ConfigurationManager()
		{
			this.IsInitialized = false;
		}
		

		/// <summary>
		/// 構成設定管理クラスの静的インスタンス生成及び使用する描画デバイス等の初期化を行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>初期化が正常に終了した場合は true、それ以外は false。</returns>
		private bool _Initialize()
		{
			if (this.IsInitialized) return false;

			this._Current = this._LoadConfigurationData();

			return this.IsInitialized = true;
		}

		#endregion


		#region 設定の読込

		/// <summary>
		/// 構成設定ファイルを読み込む。
		/// </summary>
		/// <returns>読み込んだ構成設定。</returns>
		private ConfigurationData _LoadConfigurationData()
		{
			// 構成設定ファイルが存在した場合はファイルから読込
			if (File.Exists(ConfigurationData.ConfigurationDataPath))
			{
				return XmlFileReader.ReadSaveData<ConfigurationData>(ConfigurationData.ConfigurationDataPath);
			}

			// アーカイブ内にも存在しなければ、新しくつくる
			else
			{
				return new ConfigurationData();
			}
		}

		#endregion


		#region 設定の書込


		/// <summary>
		/// 構成設定ファイルを書き込む。
		/// </summary>
		/// <param name="data">書き込む構成設定ファイル。</param>
		/// <returns>書込に成功した場合は true、それ以外は false。</returns>
		private bool _SaveConfigurationData()
		{
			return this._SaveConfigurationData(this._Current, ConfigurationData.ConfigurationDataPath);
		}

		/// <summary>
		/// 構成設定ファイルを書き込む。
		/// </summary>
		/// <param name="data">書き込む構成設定ファイル。</param>
		/// <param name="path">書き込み先のパス。</param>
		/// <returns>書き込みに成功した場合は true、それ以外は false。</returns>
		private bool _SaveConfigurationData(ConfigurationData data, string path)
		{
			return XmlFileWriter.WriteSaveData<ConfigurationData>(data, path);
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
			return ConfigurationTool.Manager.ConfigurationManager.Instance._Initialize();
		}

		/// <summary>
		/// 現在の構成設定を、規定の構成設定ファイルに書き込む。
		/// </summary>
		/// <returns>書き込みに成功した場合は true、それ以外は false。</returns>
		public static bool Save()
		{
			return ConfigurationTool.Manager.ConfigurationManager.Instance._SaveConfigurationData();
		}

		#endregion
	}
}
