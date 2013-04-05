using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

using Muphic.Manager;

namespace Muphic.Archive
{
	/// <summary>
	/// アーカイブ化されたデータを扱うクラス。
	/// </summary>
	public class ArchiveController : IDisposable
	{
		/// <summary>
		/// アーカイブ化されたファイルの (主に) 読み込み用のストリーム。
		/// </summary>
		protected Stream ArchiveDataStream { get; set; }

		/// <summary>
		/// アーカイブ化されたファイルの位置やサイズを示すデータ。
		/// </summary>
		protected ArchivedFileList FileInfoList { get; set; }


		/// <summary>
		/// データ部の開始位置を示す整数 (読み取り専用)。
		/// </summary>
		protected readonly long __dataSectionOffset;

		/// <summary>
		/// データ部の開始位置を示す整数を取得する。
		/// </summary>
		protected long DataSectionOffset
		{
			get { return this.__dataSectionOffset; }
		}


		/// <summary>
		/// アーカイブ化されたデータを扱う ArchiveController クラスの新しいインスタンスを初期化する。
		/// </summary>
		protected ArchiveController()
		{
			this.FileInfoList = new ArchivedFileList();
		}
		/// <summary>
		/// アーカイブ化されたデータを扱う ArchiveController クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="archiveFilePath">扱うアーカイブ化されたデータのファイルパス。</param>
		public ArchiveController(string archiveFilePath)
			: this(archiveFilePath, LogFileManager.IsLogging)
		{
		}
		/// <summary>
		/// アーカイブ化されたデータを扱う ArchiveController クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="archiveFilePath">扱うアーカイブ化されたデータのファイルパス。</param>
		/// <param name="enabledLogging">ログに書き込みを行い場合は true、それ以外は false。</param>
		private ArchiveController(string archiveFilePath, bool enabledLogging)
		{
			// ヘッダ部とファイルリスト部を展開 (失敗した場合は例外をスローし強制終了)
			if (!this.ExtractHeader(archiveFilePath, enabledLogging))
			{
				throw new Exception(Muphic.Tools.CommonTools.GetResourceMessage(
					Properties.Resources.Msg_ArchiveController_ArchiveFileOpenFailureException,
					System.IO.Path.GetFileName(archiveFilePath))
				);
			}

			// この時点で、Stream はデータ部の先頭にシークした状態
			// データ部開始位置オフセットを設定
			this.__dataSectionOffset = this.ArchiveDataStream.Position;

			// アーカイブ展開成功  ログに書き込み
			if (enabledLogging)
			{
				LogFileManager.WriteLine(
					Properties.Resources.Msg_ArchiveController_ArchiveFileOpen_Success,
					System.IO.Path.GetFileName(archiveFilePath)
				);
			}
		}
		
		/// <summary>
		/// アーカイブファイルのヘッダ部及びファイルリスト部の展開を行う。
		/// </summary>
		/// <param name="archiveFilePath">展開するアーカイブファイルのパス。</param>
		/// <returns>展開に成功した場合は true、それ以外は false。</returns>
		protected bool ExtractHeader(string archiveFilePath)
		{
			return this.ExtractHeader(archiveFilePath, LogFileManager.IsLogging);
		}
		/// <summary>
		/// アーカイブファイルのヘッダ部及びファイルリスト部の展開を行う。
		/// </summary>
		/// <param name="archiveFilePath">展開するアーカイブファイルのパス。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <returns>展開に成功した場合は true、それ以外は false。</returns>
		private bool ExtractHeader(string archiveFilePath, bool enabledLogging)
		{
			if (!File.Exists(archiveFilePath))		// 指定されたアーカイブファイルの存在チェック
			{										// 存在しなければログ吐いて終了
				if (enabledLogging)
				{
					LogFileManager.WriteLineError(
						Properties.Resources.Msg_ArchiveController_ArchiveFile_NotFound,
						archiveFilePath
					);
				}
				return false;
			}

			try
			{	
				// ファイルパスからアーカイブデータを展開
				this.ArchiveDataStream = new FileStream(archiveFilePath, FileMode.Open, FileAccess.Read);

				// アーカイブからヘッダ部の読み取り  ヘッダ部のサイズは設定情報から取得 (たぶん40バイトだと思うよ)
				byte[] headerPart = new byte[Muphic.Settings.System.Default.ArchiveHeaderBytes];
				//this.ArchiveDataStream.Read(header, 0, header.Length); // 暗号化無しの場合こちら

				// 1 バイトずつ複合化しながらストリームからヘッダ部を読み込み
				for (int i = 0; i < headerPart.Length; i++)
					headerPart[i] = (byte)(this.ArchiveDataStream.ReadByte() ^ ArchiveController.GetEncrypteKey(i, headerPart.Length));

				// 読み取ったヘッダ部から、ファイルリスト部のサイズを取得
				int fileListSize = int.Parse(Encoding.ASCII.GetString(headerPart));

				// アーカイブからファイルリスト部の読み取り
				byte[] fileListPart = new byte[fileListSize];
				// this.ArchiveDataStream.Read(fileListPart, 0, fileListSize); // 暗号化無しの場合こちら

				// 1 バイトずつ複合化しながらストリームからファイルリスト部を読み込み
				for (int i = 0; i < fileListPart.Length; i++)
					fileListPart[i] = (byte)(this.ArchiveDataStream.ReadByte() ^ ArchiveController.GetEncrypteKey(i, fileListPart.Length));

				// ファイルリスト部をバイト配列からクラスへ逆シリアル化
				MemoryStream memoryStream = new MemoryStream(fileListPart);
				this.FileInfoList = (ArchivedFileList)(new BinaryFormatter().Deserialize(memoryStream));
			}
			catch (Exception exception)
			{
				if (enabledLogging)
				{									// 何らかの例外が発生したらログ吐いて終了
					LogFileManager.WriteLineError(Properties.Resources.Msg_ArchiveController_ArchiveFileOpen_Failure, archiveFilePath);
					LogFileManager.WriteLineError(exception.ToString());
				}
				return false;
			}

			return true;
		}


		/// <summary>
		/// 与えられたファイル名のデータをアーカイブ内から展開する。
		/// </summary>
		/// <param name="fileName">展開するデータのファイル名。</param>
		/// <returns>展開に成功した場合はそのデータ、それ以外の場合は byte[] のデフォルト値。</returns>
		public byte[] ExtractData(string fileName)
		{
			// ファイル名に該当するデータのアーカイブ情報を取得し、展開
			return this.ExtractData(this.FileInfoList.GetFileInfo(fileName));
		}


		/// <summary>
		/// 与えられたアーカイブ情報に該当するデータをアーカイブ内から展開する。
		/// </summary>
		/// <param name="info">展開するデータのアーカイブ情報。</param>
		/// <returns>展開に成功した場合はそのデータ、それ以外の場合は byte[] のデフォルト値。</returns>
		private byte[] ExtractData(ArchivedFileInfo info)
		{
			// 該当するデータが存在しなかった場合、デフォルト値のバイト配列を返戻
			if (info.Equals(ArchivedFileInfo.Default)) return default(byte[]);

			// ストリームを当該位置 (データ部開始位置＋該当データ開始位置) にシーク
			this.ArchiveDataStream.Seek(this.DataSectionOffset + info.StartPosition, SeekOrigin.Begin);

			// 取得用のバイト配列データを生成
			byte[] data = new byte[info.DataSize];

			if (info.IsEncrypted)	// 暗号化されている場合
			{						// 1 バイトずつ読み込み、それぞれを XOR で複合化
				for (int i = 0; i < data.Length; i++)
				{
					data[i] = (byte)(this.ArchiveDataStream.ReadByte() ^ ArchiveController.GetEncrypteKey(i, data.Length));
				}
			}
			else					// 暗号化されていない場合
			{						// ストリームからファイルサイズ分全てバイト配列に読み込み、格納
				this.ArchiveDataStream.Read(data, 0, (int)info.DataSize);
			}

			return data;
		}


		/// <summary>
		/// アーカイブの暗号化及び複合化に必要なキーを得る。
		/// </summary>
		/// <param name="keyNumber">キー生成の係数 (通常はループ変数を指定する)。</param>
		/// <param name="fileSize">暗号化するファイルのサイズ</param>
		/// <returns>生成された暗号化キー。</returns>
		protected static byte GetEncrypteKey(int keyNumber, int fileSize)
		{
			return (byte)((14252 * keyNumber) + (fileSize / 2));
		}


		/// <summary>
		/// アーカイブ内の全てのデータを展開する。
		/// </summary>
		/// <returns>展開されたデータ。</returns>
		public virtual byte[][] ExtractAllData()
		{
			// ストリームを当該位置 (データ部開始位置) にシーク
			this.ArchiveDataStream.Seek(this.DataSectionOffset, SeekOrigin.Begin);

			// アーカイブ内の全てのデータを格納する変数を生成
			byte[][] data = new byte[this.FileInfoList.Count][];

			for (int i = 0; i < data.Length; i++)
			{
				// インデックス番号に応じたデータの容量を取得し、ストリームからデータを取得
				data[i] = this.ExtractData(this.FileInfoList.GetFileInfo(i));
			}

			return data;
		}


		/// <summary>
		/// 指定したファイルがアーカイブ内に存在するかどうかを確認する。
		/// </summary>
		/// <param name="fileName">確認するファイル。</param>
		/// <returns>ファイルがアーカイブ内に存在する場合は true、それ以外は false。</returns>
		public bool Exits(string fileName)
		{
			return this.FileInfoList.Exits(fileName);
		}



		/// <summary>
		/// 開いているストリームがあれば解放する。
		/// </summary>
		public void Dispose()
		{
			if(this.ArchiveDataStream != null) this.ArchiveDataStream.Close();
		}
	}
}
