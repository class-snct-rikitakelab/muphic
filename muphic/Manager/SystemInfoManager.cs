using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;

namespace Muphic.Manager
{
	using DxManager = Microsoft.DirectX.Direct3D.Manager;

	/// <summary>
	/// システム情報管理クラス (シングルトン・継承不可) 
	/// <para>muphic 本体及び muphic が実行されている環境のシステム情報を保持する。</para>
	/// </summary>
	public sealed class SystemInfoManager : Manager
	{
		/// <summary>
		/// システム情報管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static SystemInfoManager __instance = new SystemInfoManager();

		/// <summary>
		/// システム情報管理クラスの静的インスタンス (シングルトンパターン) 
		/// </summary>
		private static SystemInfoManager Instance
		{
			get { return SystemInfoManager.__instance; }
		}


		#region コンストラクタと初期化メソッド

		/// <summary>
		/// システム情報管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private SystemInfoManager()
		{
			this.__windowsName = "Unknown Windows";
			this.__platform = "Unknown Platform";
			this.__windowsVersion = "Unknown Version";
			this.__buildNumber = "Unknown BuildNumber";
			this.__servicePack = "Unknown ServicePack";
		}


		/// <summary>
		/// システム情報管理クラスの静的インスタンスを生成し、システム情報を取得する。
		/// </summary>
		/// <returns></returns>
		private bool _Initialize()
		{
			if (this._IsInitialized) return false;		// 初期化済みの場合は初期化を行わない


			LogFileManager.WriteLine(					// ログファイルに使用可能最大物理メモリ量を書き込み
				Properties.Resources.Msg_SysInfoMgr_GetTotalMemorySize,
				SystemInfoManager.TotalPhysicalMemory
			);
			LogFileManager.WriteLine(					// ログファイルに使用可能空き物理メモリ量を書き込み
				Properties.Resources.Msg_SysInfoMgr_GetAvailableMemorySize,
				SystemInfoManager.AvailablePhysicalMemory
			);


			try
			{
				string[] wininfo = this._GetWindowsInfo();	// Windows バージョンの判別

				this.__windowsName = wininfo[0];			// OS名
				this.__platform = wininfo[1];				// プラットフォーム
				this.__windowsVersion = wininfo[2];			// バージョン
				this.__buildNumber = wininfo[3];			// ビルド番号
				this.__servicePack = wininfo[4];			// サービスパック
			}
			catch (Exception e)								// 何らかのエラーが発生した場合は
			{												// ログへ書き込んで続行
				// ログファイルへ書き込み
				LogFileManager.WriteLineError(e.ToString());
			}

			LogFileManager.WriteLine(					// ログファイルに Windows バージョンの書き込み
				Properties.Resources.Msg_SysInfoMgr_GetWindowsInfo,
				SystemInfoManager.WindowsInfo
			);


			try
			{
				//this._GetAdapterInfo();					// アダプタと表示先ディスプレイの設定を取得
			}
			catch (Exception e)							// このとき例外が発生した場合は DirectX が使用できないと判断
			{											// プログラム終了
				// ログファイルへ書き込み
				LogFileManager.WriteLineError(e.ToString());

				// メッセージウィンドウ表示
				MessageBox.Show(
					Properties.Resources.ErrorMsg_MainWindow_Show_CheckDirectX_Text,
					Properties.Resources.ErrorMsg_MainWindow_Show_CheckDirectX_Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				return false;
			}

			LogFileManager.WriteLine(					// ログファイルにアダプタと表示先ディスプレイの設定を書き込み
				Properties.Resources.Msg_SysInfoMgr_GetAdapterInfo,
				SystemInfoManager.GraphicAdapterName + "(" + SystemInfoManager.CurrentDisplaySize + ")"
			);

			Tools.DebugTools.ConsolOutputMessage("SystemInfoManager -Initialize", "システム情報管理クラス生成完了", true);

			return this._IsInitialized = true;			// 初期化後に初期化済みフラグを立てる
		}

		/// <summary>
		/// システム情報管理クラスの静的インスタンスを生成し、システム情報を取得する。
		/// インスタンス生成後に１度しか実行できない点に注意。
		/// </summary>
		/// <returns>。</returns>
		public static bool Initialize()
		{
			return Muphic.Manager.SystemInfoManager.Instance._Initialize();
		}

		#endregion


		#region private フィールド

		/// <summary>
		/// muphic が動作している OS 名を保持する。
		/// </summary>
		private string __windowsName;

		/// <summary>
		/// muphic が動作している OS のプラットフォームの情報を保持する。
		/// </summary>
		private string __platform;

		/// <summary>
		///  muphic が動作している OS のバージョンを保持する。
		/// </summary>
		private string __windowsVersion;

		/// <summary>
		/// muphic が動作している OS のビルド番号を保持する。
		/// </summary>
		private string __buildNumber;

		/// <summary>
		/// muphic が動作している OS のサービスパックの情報を保持する。
		/// </summary>
		private string __servicePack;


		/// <summary>
		/// muphic を表示するアダプタ名を保持する。
		/// </summary>
		private string __adapterName;

		/// <summary>
		/// muphic 表示先のディスプレイの横幅を保持する。
		/// </summary>
		private int __displaySizeWidth;

		/// <summary>
		/// muphic 表示先のディスプレイの縦幅を保持する。
		/// </summary>
		private int __displaySizeHeight;

		#endregion


		#region static プロパティ

		/// <summary>
		/// muphic のバージョンを取得する。
		/// </summary>
		public static string MuphicVersion
		{
			get
			{
				return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
			}
		}


		/// <summary>
		/// muphic が動作している OS 名を取得する。
		/// </summary>
		public static string WindowsName
		{
			get { return SystemInfoManager.Instance.__windowsName; }
		}

		/// <summary>
		/// muphic が動作している OS のプラットフォームの情報を取得する。
		/// </summary>
		public static string Platform
		{
			get { return SystemInfoManager.Instance.__platform; }
		}

		/// <summary>
		/// muphic が動作している OS のバージョンを取得する。
		/// </summary>
		public static string WindowsVersion
		{
			get { return SystemInfoManager.Instance.__windowsVersion; }
		}

		/// <summary>
		/// muphic が動作している OS のビルド番号を取得する。
		/// </summary>
		public static string BuildNumber
		{
			get { return SystemInfoManager.Instance.__buildNumber; }
		}

		/// <summary>
		/// muphic が動作している OS のサービスパックの情報を取得する。
		/// </summary>
		public static string ServicePack
		{
			get { return SystemInfoManager.Instance.__servicePack; }
		}

		/// <summary>
		/// muphic が動作している OS の一連の情報を取得する。
		/// </summary>
		public static string WindowsInfo
		{
			get
			{
				return
					String.Format("{0} ({1} {2} {3}) {4}",
						SystemInfoManager.Instance.__windowsName,
						SystemInfoManager.Instance.__platform,
						SystemInfoManager.Instance.__windowsVersion,
						SystemInfoManager.Instance.__buildNumber,
						SystemInfoManager.Instance.__servicePack
					);
			}
		}


		/// <summary>
		/// muphic 表示先のディスプレイアダプタ (ビデオカード) 名を取得する。
		/// </summary>
		public static string GraphicAdapterName
		{
			get { return SystemInfoManager.Instance.__adapterName; }
		}

		/// <summary>
		/// muphic 表示先のディスプレイの現在の解像度を取得する。
		/// </summary>
		public static string CurrentDisplaySize
		{
			get { return SystemInfoManager.Instance.__displaySizeWidth.ToString() + " x " + SystemInfoManager.Instance.__displaySizeHeight.ToString(); }
		}


		/// <summary>
		/// muphic が動作している OS で使用可能な最大メモリサイズ (MB) を取得する。
		/// </summary>
		public static string TotalPhysicalMemory
		{
			get
			{
				return String.Format("{0} MB", new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024 / 1024);
			}
		}

		/// <summary>
		/// muphic が動作している OS で使用可能な空きメモリサイズ (MB) を取得する。
		/// </summary>
		public static string AvailablePhysicalMemory
		{
			get
			{
				return String.Format("{0} MB", new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1024 / 1024);
			}
		}

		/// <summary>
		/// muphic が使用しているメモリサイズ (MB) を取得する。
		/// </summary>
		public static string UsedPhysicalMemory
		{
			get
			{
				return String.Format("{0} MB", Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024);
				//return String.Format("{0} MB", GC.GetTotalMemory(false) / 1024 / 1024);
			}
		}

		#endregion



		#region Windows OS バージョン判別に必要な定義

		/// <summary>
		/// GetVersionEx 関数を使うための定義
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct OSVERSIONINFOEX
		{
			public int dwOSVersionInfoSize;
			public int dwMajorVersion;
			public int dwMinorVersion;
			public int dwBuildNumber;
			public int dwPlatformId;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string szCSDVersion;
			public short wServicePackMajor;
			public short wServicePackMinor;
			public short wSuiteMask;
			public byte wProductType;
			public byte wReserved;
		}

		/// <summary>
		/// Win32 API
		/// <para>Windows OS の詳しいバージョンを取得する。</para>
		/// </summary>
		/// <param name="o">。</param>
		/// <returns>。</returns>
		[DllImport("kernel32.dll", EntryPoint = "GetVersionExA")]
		private static extern int GetVersionEx(ref OSVERSIONINFOEX o);

		#region OSVERSIONINFOEX 構造体サイズ

		/// <summary>
		/// OSVERSIONINFOEX 構造体サイズ
		/// </summary>
		private const short OSVERSIONINFOEX_SIZE = 156;

		/// <summary>
		/// OSVERSIONINFOEX 構造体サイズ
		/// </summary>
		private const short OSVERSIONINFO_SIZE = 148;

		#endregion

		#region dwPlatformId 定義値

		/// <summary>
		/// dwPlatformId 定義値
		/// </summary>
		private const byte VER_PLATFORM_WIN32s = 0;

		/// <summary>
		/// dwPlatformId 定義値
		/// </summary>
		private const byte VER_PLATFORM_WIN32_WINDOWS = 1;

		/// <summary>
		/// dwPlatformId 定義値
		/// </summary>
		private const byte VER_PLATFORM_WIN32_NT = 2;

		#endregion

		#region wSuiteMask 定義値

		/// <summary>
		/// Windows XP Home Edition
		/// </summary>
		private const short VER_SUITE_PERSONAL = 0x0200;

		/// <summary>
		/// Windows 2000 Datacenter Server, or Windows Server 2003, Datacenter Edition
		/// </summary>
		private const short VER_SUITE_DATACENTER = 0x0080;

		/// <summary>
		/// Windows NT 4.0 Enterprise Edition or Windows 2000 Advanced Server, or Windows Server 2003, Enterprise Edition
		/// </summary>
		private const short VER_SUITE_ENTERPRISE = 0x0002;

		/// <summary>
		/// Windows Server 2003, Web Edition
		/// </summary>
		private const short VER_SUITE_BLADE = 0x0400;

		#endregion


		/// <summary>
		/// Win32 API
		/// <para>Windows Vista のエディションを取得する。</para>
		/// </summary>
		/// <param name="dwOSMajorVersion">。</param>
		/// <param name="dwOSMinorVersion">。</param>
		/// <param name="dwSpMajorVersion">。</param>
		/// <param name="dwSpMinorVersion">。</param>
		/// <param name="pdwReturnedProductType">。</param>
		/// <returns>。</returns>
		[DllImport("Kernel32.dll")]
		private static extern bool GetProductInfo(
		  int dwOSMajorVersion,
		  int dwOSMinorVersion,
		  int dwSpMajorVersion,
		  int dwSpMinorVersion,
		  out uint pdwReturnedProductType);

		#region pdwReturnedProductType 定義値 (Windows Vista もしくは Windows Server 2008 の各エディション定義値) 

		/// <summary>
		/// 0x00000006, Windows Vista Business Edition
		/// </summary>
		private const uint PRODUCT_BUSINESS = 0x00000006;

		/// <summary>
		/// 0x00000004, Windows Vista Enterprise Edition
		/// </summary>
		private const uint PRODUCT_ENTERPRISE = 0x00000004;

		/// <summary>
		/// 0x00000002, Windows Vista Home Basic Edition
		/// </summary>
		private const uint PRODUCT_HOME_BASIC = 0x00000002;

		/// <summary>
		/// 0x00000003, Windows Vista Home Premium Edition
		/// </summary>
		private const uint PRODUCT_HOME_PREMIUM = 0x00000003;

		/// <summary>
		/// 0x00000001, Windows Vista Ultimate Edition
		/// </summary>
		private const uint PRODUCT_ULTIMATE = 0x00000001;

		/// <summary>
		/// 0x00000000, Windows Vista An unknown product
		/// </summary>
		private const uint PRODUCT_UNDEFINED = 0x00000000;

		/// <summary>
		/// 0x00000012, Windows HPC Server 2008
		/// </summary>
		private const uint PRODUCT_CLUSTER_SERVER = 0x00000012;

		/// <summary>
		/// 0x00000008, Windows Server 2008 Datacenter
		/// </summary>
		private const uint PRODUCT_DATACENTER_SERVER = 0x00000008;

		/// <summary>
		/// 0x0000000C, Windows Server 2008 Datacenter (core installation)
		/// </summary>
		private const uint PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C;

		/// <summary>
		/// 0x00000027, Windows Server 2008 Datacenter without Hyper-V (core installation)
		/// </summary>
		private const uint PRODUCT_DATACENTER_SERVER_CORE_V = 0x00000027;

		/// <summary>
		/// 0x00000025, Windows Server 2008 Datacenter without Hyper-V
		/// </summary>
		private const uint PRODUCT_DATACENTER_SERVER_V = 0x00000025;

		/// <summary>
		/// 0x0000000A, Windows Server 2008 Enterprise
		/// </summary>
		private const uint PRODUCT_ENTERPRISE_SERVER = 0x0000000A;

		/// <summary>
		/// 0x0000000E, Windows Server 2008 Enterprise (core installation)
		/// </summary>
		private const uint PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E;

		/// <summary>
		/// 0x00000029, Windows Server 2008 Enterprise without Hyper-V (core installation)
		/// </summary>
		private const uint PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029;

		/// <summary>
		/// 0x0000000F, Windows Server 2008 Enterprise for Itanium-based Systems
		/// </summary>
		private const uint PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F;

		/// <summary>
		/// 0x00000026, Windows Server 2008 Enterprise without Hyper-V
		/// </summary>
		private const uint PRODUCT_ENTERPRISE_SERVER_V = 0x00000026;

		/// <summary>
		/// 0x0000001E, Windows Essential Business Server 2008 Management Server
		/// </summary>
		private const uint PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E;

		/// <summary>
		/// 0x00000020, Windows Essential Business Server 2008 Messaging Server
		/// </summary>
		private const uint PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020;

		/// <summary>
		/// 0x0000001F, Windows Essential Business Server 2008 Security Server
		/// </summary>
		private const uint PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F;

		/// <summary>
		/// 0x00000018, Windows Server 2008 for Windows Essential Server Solutions
		/// </summary>
		private const uint PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018;

		/// <summary>
		/// 0x00000023, Windows Server 2008 without Hyper-V for Windows Essential Server Solutions
		/// </summary>
		private const uint PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023;

		/// <summary>
		/// 0x00000009, Windows Small Business Server 2008
		/// </summary>
		private const uint PRODUCT_SMALLBUSINESS_SERVER = 0x00000009;

		/// <summary>
		/// 0x00000007, Windows Server 2008 Standard
		/// </summary>
		private const uint PRODUCT_STANDARD_SERVER = 0x00000007;

		/// <summary>
		/// 0x0000000D, Windows Server 2008 Standard (core installation)
		/// </summary>
		private const uint PRODUCT_STANDARD_SERVER_CORE = 0x0000000D;

		/// <summary>
		/// 0x00000028, Windows Server 2008 Standard without Hyper-V (core installation)
		/// </summary>
		private const uint PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028;

		/// <summary>
		/// 0x00000024, Windows Server 2008 Standard without Hyper-V
		/// </summary>
		private const uint PRODUCT_STANDARD_SERVER_V = 0x00000024;

		/// <summary>
		/// 0x0000000B, Windows Vista Startar
		/// </summary>
		private const uint PRODUCT_STARTER = 0x0000000B;

		/// <summary>
		/// 0x00000011, Windows Web Server 2008
		/// </summary>
		private const uint PRODUCT_WEB_SERVER = 0x00000011;

		/// <summary>
		/// 0x0000001D, Windows Web Server 2008  (core installation)
		/// </summary>
		private const uint PRODUCT_WEB_SERVER_CORE = 0x0000001D;

		#endregion

		#endregion


		#region Windows OS バージョン判別

		/// <summary>
		/// Win32 API の GetVersionEx 関数及び GetProductInfo 関数を利用して、muphic が動作している Windows OS のバージョンを判別する。
		/// </summary>
		/// <remarks>
		/// WMI の Win32_OperatingSystem クラスを使用すればかなり楽に OS 情報を取得できるが、
		/// OS 名に ® やら ™ やらシステムドライブのパーティションが含まれていて使いづらかったり、
		/// そもそも Windows 2000 より前の OS では別途インストールが必要だったりと面倒なため、Win32 API を使用。
		/// </remarks>
		/// <returns>Windows OS の５つの情報を含む配列 (0:OS名  1:プラットフォーム  2:バージョン  3:ビルド番号  4:サービスパック) 。</returns>
		private string[] _GetWindowsInfo()
		{
			// GetVersionEx関数によりOSの情報を取得
			OSVERSIONINFOEX osInfo = new OSVERSIONINFOEX();
			osInfo.dwOSVersionInfoSize = OSVERSIONINFOEX_SIZE;
			int bVersionEx = GetVersionEx(ref osInfo);
			if (bVersionEx == 0)
			{
				osInfo.dwOSVersionInfoSize = OSVERSIONINFO_SIZE;
				GetVersionEx(ref osInfo);
			}

			string windowsName = "Unknown Windows";				 // Windows名

			switch (osInfo.dwPlatformId)
			{
				case VER_PLATFORM_WIN32_WINDOWS:				 // Windows 9x系
					if (osInfo.dwMajorVersion == 4)
					{
						switch (osInfo.dwMinorVersion)
						{
							case 0:								// Windows 95
								windowsName = "Windows 95";		// .NET Framework がサポートされていないため
								break;							// 実際にはあり得ないパターン
							case 10:
								if ((osInfo.szCSDVersion.Length <= 0) ||
								   (osInfo.szCSDVersion.Replace(" ", "") != "A"))
								{
									windowsName = "Windows 98";
								}
								else
								{
									windowsName = "Windows 98 SE";
								}
								break;
							case 90:
								windowsName = "Windows Me";
								break;
						}
					}
					break;

				case VER_PLATFORM_WIN32_NT:						// Windows NT系
					if (osInfo.dwMajorVersion == 4)				// Windows NT 4.0
					{											// .NET Framework がサポートされていないため
						windowsName = "Windows NT 4.0";			// 実際にはあり得ないパターン
					}											// よって、詳しい判別処理は省略

					else if (osInfo.dwMajorVersion == 5)
					{
						switch (osInfo.dwMinorVersion)
						{
							case 0:
								windowsName = "Windows 2000";

								if ((osInfo.wSuiteMask & VER_SUITE_DATACENTER)
									  == VER_SUITE_DATACENTER)
								{
									windowsName += " Datacenter Server";
								}
								else if ((osInfo.wSuiteMask & VER_SUITE_ENTERPRISE)
										   == VER_SUITE_ENTERPRISE)
								{
									windowsName += " Advanced Server";
								}
								else
								{
									windowsName += " Server";
								}
								break;

							case 1:
								windowsName = "Windows XP";

								if ((osInfo.wSuiteMask & VER_SUITE_PERSONAL)
									  == VER_SUITE_PERSONAL)
								{
									windowsName += " Home Edition";
								}
								else
								{
									windowsName += " Professional";
								}
								break;

							case 2:
								windowsName = "Windows Server 2003";

								if ((osInfo.wSuiteMask & VER_SUITE_DATACENTER)
									  == VER_SUITE_DATACENTER)
								{
									windowsName += " Datacenter";
								}
								else if ((osInfo.wSuiteMask & VER_SUITE_ENTERPRISE)
										   == VER_SUITE_ENTERPRISE)
								{
									windowsName += " Enterprise";
								}
								else if (osInfo.wSuiteMask == VER_SUITE_BLADE)
								{
									windowsName += " Web";
								}
								else
								{
									windowsName += " Standard";
								}
								break;
						}
					}

					else if (osInfo.dwMajorVersion == 6)
					{
						switch (osInfo.dwMinorVersion)
						{
							case 0:								
								windowsName = "Windows Vista";
								break;
							case 1:
								windowsName = "Windows 7";
								break;
							default:
								windowsName = "Unknown Windows";
								break;
						}

						// Windows Vista 以降の場合GetVersionEx関数だけでは完全にエディションを判別できないため
						// GetProductInfo関数を使用してエディションを判別

						uint edition = PRODUCT_UNDEFINED;
						if (GetProductInfo(
								osInfo.dwMajorVersion,
								osInfo.dwMinorVersion,
								osInfo.wServicePackMajor,
								osInfo.wServicePackMinor,
								out edition))
						{
							switch (edition)
							{
								case PRODUCT_ENTERPRISE:
									windowsName += " Enterprise";
									break;
								case PRODUCT_ULTIMATE:
									windowsName += " Ultimate";
									break;
								case PRODUCT_BUSINESS:
									windowsName += " Business";
									break;
								case PRODUCT_HOME_PREMIUM:
									windowsName += " Home Premium";
									break;
								case PRODUCT_HOME_BASIC:
									windowsName += " Home Basic";
									break;
								case PRODUCT_STARTER:
									windowsName += " Startar";
									break;
								case PRODUCT_CLUSTER_SERVER:
									windowsName = "Windows HPC Server 2008";
									break;
								case PRODUCT_DATACENTER_SERVER:
									windowsName = "Windows Server 2008 Datacenter";
									break;
								case PRODUCT_DATACENTER_SERVER_CORE:
									windowsName = " Windows Server 2008 Datacenter (core installation)";
									break;
								case PRODUCT_DATACENTER_SERVER_CORE_V:
									windowsName = "Windows Server 2008 Datacenter without Hyper-V (core installation)";
									break;
								case PRODUCT_DATACENTER_SERVER_V:
									windowsName = "Windows Server 2008 Datacenter without Hyper-V";
									break;
								case PRODUCT_ENTERPRISE_SERVER:
									windowsName = "Windows Server 2008 Enterprise";
									break;
								case PRODUCT_ENTERPRISE_SERVER_CORE:
									windowsName = "Windows Server 2008 Enterprise (core installation)";
									break;
								case PRODUCT_ENTERPRISE_SERVER_CORE_V:
									windowsName = "Windows Server 2008 Enterprise without Hyper-V (core installation)";
									break;
								case PRODUCT_ENTERPRISE_SERVER_IA64:
									windowsName = "Windows Server 2008 Enterprise for Itanium-based Systems";
									break;
								case PRODUCT_ENTERPRISE_SERVER_V:
									windowsName = "Windows Server 2008 Enterprise without Hyper-V";
									break;
								case PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT:
									windowsName = "Windows Essential Business Server 2008 Management Server";
									break;
								case PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING:
									windowsName = "Windows Essential Business Server 2008 Messaging Server";
									break;
								case PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY:
									windowsName = "Windows Essential Business Server 2008 Security Server";
									break;
								case PRODUCT_SERVER_FOR_SMALLBUSINESS:
									windowsName = "Windows Server 2008 for Windows Essential Server Solutions";
									break;
								case PRODUCT_SERVER_FOR_SMALLBUSINESS_V:
									windowsName = "Windows Server 2008 without Hyper-V for Windows Essential Server Solutions";
									break;
								case PRODUCT_SMALLBUSINESS_SERVER:
									windowsName = "Windows Small Business Server 2008";
									break;
								case PRODUCT_STANDARD_SERVER:
									windowsName = "Windows Server 2008 Standard";
									break;
								case PRODUCT_STANDARD_SERVER_CORE:
									windowsName = "Windows Server 2008 Standard (core installation)";
									break;
								case PRODUCT_STANDARD_SERVER_CORE_V:
									windowsName = "Windows Server 2008 Standard without Hyper-V (core installation)";
									break;
								case PRODUCT_STANDARD_SERVER_V:
									windowsName = "Windows Server 2008 Standard without Hyper-V";
									break;
								case PRODUCT_WEB_SERVER:
									windowsName = "Windows Web Server 2008";
									break;
								case PRODUCT_WEB_SERVER_CORE:
									windowsName = "Windows Web Server 2008  (core installation)";
									break;
								default:
									windowsName += " Unknown";
									break;
							}
						}
					}
					break;
			}

			string strPlatform = "Unknown Platform";

			if (osInfo.dwPlatformId == VER_PLATFORM_WIN32_WINDOWS)
			{
				strPlatform = "Win32Windows";
			}
			else if (osInfo.dwPlatformId == VER_PLATFORM_WIN32_NT)
			{
				strPlatform = "Win32NT";
			}

			#region コンソール出力
			//// システム情報を出力
			//Console.WriteLine(
			//  "{0} (Platform {1} Version {2}.{3} Build {4}) {5}",
			//  windowsName,
			//  strPlatform,
			//  osInfo.dwMajorVersion,
			//  osInfo.dwMinorVersion,
			//  osInfo.dwBuildNumber,
			//  osInfo.szCSDVersion);
			#endregion


			return new String[]{
				windowsName,
				strPlatform,
				"Version " + osInfo.dwMajorVersion + "." + osInfo.dwMinorVersion,
				"Build " + osInfo.dwBuildNumber,
				osInfo.szCSDVersion
			};
		}

		#endregion



		#region グラフィックアダプタ判別

		/// <summary>
		/// muphic が動作しているコンピュータ上のグラフィックアダプタを判別する。
		/// <para>同時に、DirectX が使用可能かどうかのチェックも行う。</para>
		/// </summary>
		private void _GetAdapterInfo()
		{
			// muphic を表示するディスプレイアダプタの番号
			int checkAdapter = 0;

			// 表示先のディスプレイ番号が０以外に設定されていた場合、アダプタ数の妥当性をチェックした上でその値に設定
			if (ConfigurationManager.Current.AdapterNum < DxManager.Adapters.Count)
			{
				checkAdapter = ConfigurationManager.Current.AdapterNum;
			}

			// muphic表示先 (チェック対象) のアダプタの情報を取得する。
			AdapterInformation ainfo = DxManager.Adapters[checkAdapter];

			// アダプタ名を取得
			this.__adapterName = ainfo.Information.Description;

			// アダプタの現在の解像度を取得する。
			this.__displaySizeWidth = ainfo.CurrentDisplayMode.Width;
			this.__displaySizeHeight = ainfo.CurrentDisplayMode.Height;
		}

		#endregion
	}

}
