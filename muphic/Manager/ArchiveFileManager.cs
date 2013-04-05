using System;
using System.Windows.Forms;
using Muphic.Archive;

namespace Muphic.Manager
{
	/// <summary>
	/// アーカイブファイル管理クラス (シングルトン・継承不可) 
	/// <para>アーカイブに格納された</para>
	/// </summary>
	public sealed class ArchiveFileManager : Manager
	{
		/// <summary>
		/// アーカイブファイル管理クラスの静的インスタンス (シングルトンパターン)
		/// </summary>
		private static ArchiveFileManager __instance = new ArchiveFileManager();

		/// <summary>
		/// アーカイブファイル管理クラスの静的インスタンス (シングルトンパターン)
		/// </summary>
		private static ArchiveFileManager Instance
		{
			get { return ArchiveFileManager.__instance; }
		}

		/// <summary>
		/// ファイルアーカイブを扱うコントローラ。
		/// </summary>
		private ArchiveController FileArchive { get; set; }


		/// <summary>
		/// アーカイブファイル管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private ArchiveFileManager()
		{
		}


		#region コンストラクタ / 初期化 / 解放

		/// <summary>
		/// アーカイブファイル管理クラスの静的インスタンス生成及び使用するアーカイブファイルの読み込みを行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>正常に初期化が行われた場合は true、それ以外は false。</returns>
		private bool _Initialize()
		{
			if (this._IsInitialized) return false;

			try
			{
				this.FileArchive = new ArchiveController(Settings.ResourceNames.ArchiveFilePath);
			}
			catch (Exception e)
			{
				LogFileManager.WriteLineError(e.ToString());

				// メッセージウィンドウ表示
				MessageBox.Show(
					Muphic.Tools.CommonTools.GetResourceMessage(
						Properties.Resources.ErrorMsg_ArchiveMgr_Show_FailureArchiveLoad_Text,
						System.IO.Path.GetFileName(Settings.ResourceNames.ArchiveFilePath)),
					Properties.Resources.ErrorMsg_ArchiveMgr_Show_FailureArchiveLoad_Caption,
					MessageBoxButtons.OK, MessageBoxIcon.Error);

				return false;
			}

			Tools.DebugTools.ConsolOutputMessage("ArchiveFileManager -Initialize", "アーカイブファイル管理クラス生成完了", true);

			return this._IsInitialized = true;
		}


		/// <summary>
		/// アーカイブを解放する。
		/// </summary>
		private void _Dispose()
		{
			if (this.FileArchive != null)
			{
				this.FileArchive.Dispose();
				this.FileArchive = null;
			}
		}

		#endregion


		#region アーカイブ操作

		/// <summary>
		/// アーカイブから指定したファイルのデータを取得する。
		/// </summary>
		/// <param name="fileName">取得するファイル。</param>
		/// <returns>取得されたデータ。</returns>
		private byte[] _GetData(string fileName)
		{
			return this.FileArchive.ExtractData(fileName);
		}


		/// <summary>
		/// 指定したファイルがアーカイブ内に存在するかどうかを確認する。
		/// </summary>
		/// <param name="fileName">確認するファイル。</param>
		/// <returns>ファイルがアーカイブ内に存在すれば true、それ以外は false。</returns>
		private bool _Exists(string fileName)
		{
			return this.FileArchive.Exits(fileName);
		}

		#endregion


		#region 外部から呼ばれるメソッド群

		/// <summary>
		/// アーカイブファイル管理クラスの静的インスタンス生成及び使用するアーカイブファイルの読み込みを行う。
		/// インスタンス生成後に１度しか実行できない点に注意。
		/// </summary>
		/// <returns>正常に初期化が行われた場合は true、それ以外は false。</returns>
		public static bool Initialize()
		{
			return Muphic.Manager.ArchiveFileManager.Instance._Initialize();
		}


		/// <summary>
		/// 指定したファイルのデータを取得する。
		/// </summary>
		/// <param name="fileName">取得するファイル。</param>
		/// <returns>取得されたデータ。</returns>
		public static byte[] GetData(string fileName)
		{
			return Muphic.Manager.ArchiveFileManager.Instance._GetData(fileName);
		}


		/// <summary>
		/// 指定したファイルがアーカイブ内に存在するかどうかを確認する。
		/// </summary>
		/// <param name="fileName">確認するファイル。</param>
		/// <returns>ファイルがアーカイブ内に存在すれば true、それ以外は false。</returns>
		public static bool Exists(string fileName)
		{
			return Muphic.Manager.ArchiveFileManager.Instance._Exists(fileName);
		}

		#endregion

	}
}
