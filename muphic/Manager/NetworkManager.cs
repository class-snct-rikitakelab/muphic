using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

using Muphic.Tools;

namespace Muphic.Manager
{
	/// <summary>
	/// ネットワーク管理クラス (シングルトン・継承不可)
	/// <para>ネットワークの接続の管理や、ファイルの移動に関する機能を提供する。</para>
	/// </summary>
	public sealed class NetworkManager : Manager
	{
		/// <summary>
		/// ネットワーク管理クラスの静的インスタンス (シングルトンパターン)。
		/// </summary>
		private static NetworkManager __instance = new NetworkManager();

		/// <summary>
		/// ネットワーク管理クラスの静的インスタンス (シングルトンパターン) を取得する。
		/// </summary>
		private static NetworkManager Instance
		{
			get { return Muphic.Manager.NetworkManager.__instance; }
		}


		#region フィールド / プロパティ

		#region ネットワーク利用

		/// <summary>
		/// ネットワーク接続を利用するかどうかを示す値を取得する。
		/// </summary>
		private static bool EnabledNetwork
		{
			get { return ConfigurationManager.Current.EnabledNetwork; }
		}

		/// <summary>
		/// ネットワークが利用可能かどうかを示す値を取得する。
		/// </summary>
		private bool __canUseNetwork;

		/// <summary>
		/// ネットワークが利用可能かどうかを示す値を取得する。
		/// </summary>
		private bool _CanUseNetwork
		{
			get { return NetworkManager.EnabledNetwork && this.__canUseNetwork && NetworkInterface.GetIsNetworkAvailable(); }
		}

		/// <summary>
		/// ネットワークが利用可能かどうかを示す値を取得する。
		/// </summary>
		public static bool CanUseNetwork
		{
			get { return Muphic.Manager.NetworkManager.Instance._CanUseNetwork; }
		}

		#endregion

		#region IP アドレス

		/// <summary>
		///  muphic が実行されているホストコンピュータの全ての IP アドレス。
		/// </summary>
		private IPAddress[] __hostAddresses;

		/// <summary>
		/// muphic が実行されているホストコンピュータの全ての IP アドレスを取得する。
		/// </summary>
		private IPAddress[] _HostAddresses
		{
			get { return this.__hostAddresses; }
		}

		/// <summary>
		/// muphic が実行されているホストコンピュータの全ての IP アドレスを、カンマ区切りの文字列として取得する。
		/// </summary>
		private string _HostAddressesString
		{
			get
			{
				string addresses = "";

				foreach (IPAddress ip in this._HostAddresses)
				{
					addresses += (string.IsNullOrEmpty(addresses) ? "" : ", ") + ip.ToString();
				}

				return addresses;
			}
		}

		#endregion

		#region 非同期コピー

		/// <summary>
		/// 非同期でファイルのコピーを行うワーカースレッド。
		/// </summary>
		private BackgroundWorker _CopyWorker { get; set; }

		/// <summary>
		/// 非同期でのファイルのコピーの結果 (成功や失敗の原因を示す番号) を取得または設定する。
		/// </summary>
		private int _CopyWorkerResult { get; set; }

		/// <summary>
		/// 非同期でのファイルのコピーの結果 (結果の詳細メッセージ) を取得または設定する。
		/// </summary>
		private string _CopyWorkerResultMessage { get; set; }

		/// <summary>
		/// 非同期でのファイルのコピー操作を行っているかどうかを示す値を取得または設定する。
		/// _CopyWorker の IsBusy と違い、結果の変数の格納など全ての操作を含む。
		/// </summary>
		private bool _IsCopyWorking { get; set; }

		/// <summary>
		/// 非同期でのファイルのコピー操作を行っているかどうかを示す値を取得する。
		/// </summary>
		public static bool IsCopyWorking
		{
			get { return Muphic.Manager.NetworkManager.Instance._IsCopyWorking; }
		}

		/// <summary>
		/// 非同期でのファイルのコピー操作の経過時間を計測するタイマー。
		/// </summary>
		private Stopwatch CopyTimer { get; set; }

		#endregion

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// ネットワーク管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private NetworkManager()
		{
			this.__canUseNetwork = true;

			this._IsCopyWorking = false;
			this._CopyWorkerResult = -1;
			this._CopyWorkerResultMessage = "";

			this._CopyWorker = new BackgroundWorker();
			this._CopyWorker.DoWork += new DoWorkEventHandler(this._CopyWorker_DoWork);
			this._CopyWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this._CopyWorker_RunWorkerCompleted);

			this.CopyTimer = new Stopwatch();
		}

		/// <summary>
		/// ネットワーク管理クラスの初期化を行う。このメソッドはインスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>初期化が正常に終了した場合は true、それ以外は false。</returns>
		private bool _Initialize()
		{
			if (this._IsInitialized) return false;

			// 構成設定でネットワーク機能が無効化されていた場合、初期化を正常終了
			if (!NetworkManager.EnabledNetwork)
			{
				return this._IsInitialized = true;
			}

			try
			{
				// IP アドレスを取得
				this.__hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
			}
			catch (Exception e)
			{
				// IP アドレス取得に失敗した場合、ログに出力し、ネットワークを無効化して正常終了
				LogFileManager.WriteLineError(Properties.Resources.Msg_NetworkMgr_GetHostAddress_Failue);
				LogFileManager.WriteLineError(e.ToString());

				this.__canUseNetwork = false;
				return true;
			}

			// ネットワークの接続状態をチェック
			this._CheckIsNetworkAvailable(true);

			Tools.DebugTools.ConsolOutputMessage("NetworkManager -Initialize", "ネットワーク管理クラス生成完了", false);

			return this._IsInitialized = true;
		}

		#endregion


		#region ネットワーク接続

		/// <summary>
		/// ネットワークが接続可能かどうかを確認する。
		/// </summary>
		/// <param name="isLogging">ネットワークの接続状態をログに出力する場合は true、それ以外は false。</param>
		/// <returns>ネットワークに接続されている場合は true、それ以外は false。</returns>
		private bool _CheckIsNetworkAvailable(bool isLogging)
		{
			if (this._CanUseNetwork)
			{
				LogFileManager.WriteLine(
					Properties.Resources.Msg_NetworkMgr_IsNetworkAvailable,
					CommonTools.GetResourceMessage(Properties.Resources.Msg_NetworkMgr_IsNetworkAvailable_True, this._HostAddressesString)
				);

				return true;
			}
			else
			{
				LogFileManager.WriteLine(
					Properties.Resources.Msg_NetworkMgr_IsNetworkAvailable,
					Properties.Resources.Msg_NetworkMgr_IsNetworkAvailable_False
				);

				return false;
			}
		}

		/// <summary>
		/// ファイルの提出先パスが有効かどうかを確認する。
		/// </summary>
		/// <returns>提出先パスが有効である場合は true、それ以外は false。</returns>
		private bool _CheckIsSubmitAvailable()
		{
			return this._CheckIsSubmitAvailable(ConfigurationManager.Current.SubmissionPath);
		}

		/// <summary>
		/// 指定したファイルの提出先パスが有効かどうかを確認する。
		/// </summary>
		/// <param name="submitPath">確認する提出先パス。</param>
		/// <returns>提出先パスが有効である場合は true、それ以外は false。</returns>
		private bool _CheckIsSubmitAvailable(string submitPath)
		{
			if (string.IsNullOrEmpty(submitPath.Trim())) return false;
			else return Directory.Exists(Path.GetPathRoot(submitPath));
		}

		#endregion


		#region ファイルコピー

		/// <summary>
		/// 指定したファイルの、指定したディレクトリへの非同期コピーを開始する。
		/// </summary>
		/// <param name="srcFilePath">コピー元のファイルパス。</param>
		/// <param name="dstDirectoryPath">コピー先のディレクトリ。</param>
		/// <returns>コピーが開始された場合は true、それ以外は false。</returns>
		private bool _BeginCopyFile(string srcFilePath, string dstDirectoryPath)
		{
			// ネットワークが使用不可だったらコピーしない
			if (!this._CanUseNetwork) return false;

			// コピー先のファイルパスを生成
			string dstFilePath = Path.Combine(dstDirectoryPath, Path.GetFileName(srcFilePath));

			// ファイルコピーを行うことと、コピー元・コピー先のパスをログ表示
			LogFileManager.WriteLine(
				Properties.Resources.Msg_NetworkMgr_FileCopy,
				CommonTools.GetResourceMessage(Properties.Resources.Msg_NetworkMgr_FileCopy_Path, Path.GetFileName(srcFilePath), dstFilePath)
			);

			this.CopyTimer.Reset();

			// コピー結果のプロパティをクリアし、非同期コピー実行フラグを立てる
			this._CopyWorkerResult = -1;
			this._CopyWorkerResultMessage = "";
			this._IsCopyWorking = true;

			// 非同期のコピー操作を開始
			this._CopyWorker.RunWorkerAsync(new string[] { srcFilePath, dstDirectoryPath, dstFilePath });

			return true;
		}

		/// <summary>
		/// 非同期コピーを完了し、その結果を取得する。
		/// </summary>
		/// <param name="isLogging">結果のログを出力する場合は true、それ以外は false。</param>
		/// <returns>コピーに成功した場合は true、それ以外は false。</returns>
		private bool _EndCopyFile(bool isLogging)
		{
			bool result = false;
			string resultMsg = "";

			switch (this._CopyWorkerResult)
			{
				case 0:		// 正常終了した場合
					resultMsg = Properties.Resources.Msg_NetworkMgr_FileCopyResult_Success;
					result = true;
					goto default;

				case 1:		// コピー先ディレクトリが設定されていなかった場合
					resultMsg = Properties.Resources.Msg_NetworkMgr_FileCopyResult_Failure_DstEmpty;
					result = false;
					goto default;

				case 2:		// コピー元ファイルが存在しなかった場合
					resultMsg = Properties.Resources.Msg_NetworkMgr_FileCopyResult_Failure_SrcFileNotFound;
					result = false;
					goto default;

				case 3:		// コピー先のルートディレクトリが存在しなかった場合
					resultMsg = Properties.Resources.Msg_NetworkMgr_FileCopyResult_Failure_DstRootNotFound;
					result = false;
					goto default;

				case 10:	// 何らかの例外が発生した場合 (この場合のみ例外内容もログ出力)
					LogFileManager.WriteLineError(
						Properties.Resources.Msg_NetworkMgr_FileCopyResult,
						Properties.Resources.Msg_NetworkMgr_FileCopyResult_Failure_Exception
					);
					LogFileManager.WriteLineError(this._CopyWorkerResultMessage);
					return false;

				default:	// 例外発生時以外はここでメッセージを出力し結果を返す
					if (isLogging) LogFileManager.WriteLine(Properties.Resources.Msg_NetworkMgr_FileCopyResult, resultMsg);
					return result;
			}
		}


		#region BackgroundWorker による非同期コピー

		/// <summary>
		/// 非同期でのファイルのコピーを行う。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _CopyWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Cancel = false;
			this.CopyTimer.Start();

			string srcFilePath = ((string[])e.Argument)[0];
			string dstDirectoryPath = ((string[])e.Argument)[1];
			string dstFilePath = ((string[])e.Argument)[2];

			// コピー先ディレクトリが設定されているかを確認
			if (string.IsNullOrEmpty(dstDirectoryPath))
			{
				e.Result = new object[] { 1, "" };
				return;
			}

			// コピー元ファイルが存在するかを確認
			if (!File.Exists(srcFilePath))
			{
				e.Result = new object[] { 2, "" };
				return;
			}

			// コピー先のルートディレクトリが存在するかを確認
			if (!Directory.Exists(Path.GetPathRoot(dstFilePath)))
			{
				e.Result = new object[] { 3, "" };
				return;
			}

			try
			{
				// ディレクトリを生成し、ファイルをコピー
				Directory.CreateDirectory(dstDirectoryPath);
				File.Copy(srcFilePath, dstFilePath, true);
			}
			catch (Exception exception)
			{
				e.Result = new object[] { 10, exception.ToString() };
				return;
			}

			// ここまで来れば成功と見なす
			e.Result = new object[] { 0, "" };
		}


		/// <summary>
		/// ファイルの非同期コピーが終了した際に実行される。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _CopyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.CopyTimer.Stop();

			// 非同期コピーの結果をプロパティに格納
			this._CopyWorkerResult = (int)(((object[])e.Result)[0]);
			this._CopyWorkerResultMessage = (string)(((object[])e.Result)[1]);

			// 実行フラグを降ろす
			this._IsCopyWorking = false;
		}

		#endregion

		#endregion


		#region 外部から呼ばれるメソッド群

		#region Init

		/// <summary>
		/// ネットワーク管理クラスの初期化を行う。このメソッドはインスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>初期化が正常に終了した場合は true、それ以外は false。</returns>
		public static bool Initialize()
		{
			return Muphic.Manager.NetworkManager.Instance._Initialize();
		}

		#endregion

		#region Check

		/// <summary>
		/// ファイルの提出先パスが有効かどうかを確認する。
		/// </summary>
		/// <returns>提出先パスが有効である場合は true、それ以外は false。</returns>
		public static bool CheckIsSubmitAvailable()
		{
			return Muphic.Manager.NetworkManager.Instance._CheckIsSubmitAvailable();
		}

		#endregion

		#region Copy

		/// <summary>
		/// 指定したファイルの、指定したディレクトリへの非同期コピーを開始する。
		/// </summary>
		/// <param name="srcFilePath">コピー元のファイルパス。</param>
		/// <param name="dstDirectoryPath">コピー先のディレクトリ。</param>
		/// <returns>コピーが開始された場合は true、それ以外は false。</returns>
		public static bool BeginCopyFile(string srcFilePath, string dstDirectoryPath)
		{
			return Muphic.Manager.NetworkManager.Instance._BeginCopyFile(srcFilePath, dstDirectoryPath);
		}

		/// <summary>
		/// 非同期コピーを終了し、その結果を取得する。
		/// </summary>
		/// <returns>コピーに成功した場合は true、それ以外は false。</returns>
		public static bool EndCopyFile()
		{
			return Muphic.Manager.NetworkManager.Instance._EndCopyFile(true);
		}

		#endregion

		#endregion

	}
}
