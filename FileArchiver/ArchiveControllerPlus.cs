using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

using Muphic.Archive;

namespace FileArchiver
{
	/// <summary>
	/// データのアーカイブ化及び展開を行うアーカイバクラス。
	/// </summary>
	public class ArchiveControllerPlus : ArchiveController
	{
		/// <summary>
		/// アーカイバクラスの新しいインスタンスを初期化する。
		/// </summary>
		public ArchiveControllerPlus()
			: base()
		{
			this.ArchivedData = new List<byte[]>();
			this.IsArchivedFlags = new List<bool>();
		}

		/// <summary>
		/// アーカイバクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="filePath">アーカイブを開く場合、アーカイブファイルのパスを指定。</param>
		public ArchiveControllerPlus(string filePath)
			: base(filePath)
		{
			this.ArchivedData = new List<byte[]>();
			this.IsArchivedFlags = new List<bool>();

			// アーカイブを開く場合、同時にデータも全て展開
			byte[][] data = base.ExtractAllData();

			// 展開されたデータを Plus 独自の ArchivedData プロパティに格納
			for (int i = 0; i < data.Length; i++)
			{
				this.ArchivedData.Add(data[i]);
			}
		}


		/// <summary>
		/// アーカイブされたデータ。
		/// <para>新しいデータは、アーカイブ化実行時にバイト配列に変換されこのリストに追加される。</para>
		/// </summary>
		public List<byte[]> ArchivedData { get; set; }


		/// <summary>
		/// ファイルが既にアーカイブ化されていることを示すフラグリスト。
		/// <para>新しいデータは、フォームから追加された時点でこのリストにも false で追加される。</para>
		/// </summary>
		public List<bool> IsArchivedFlags { get; set; }


		/// <summary>
		/// ファイルのアーカイブ化を行う。
		/// </summary>
		/// <param name="savePath">アーカイブ化したファイルの保存先。</param>
		/// <param name="files">アーカイブ化するファイルパスのリスト。</param>
		public void CreateArchive(string savePath, List<string> files)
		{
			FileStream fileStream = null;		// アーカイブ書込用ストリーム
			long byteCounter = 0;				// 合計バイト数計算用変数

			for (int i = 0; i < files.Count; i++)			// アーカイブ対象ファイル全てをチェック
			{												// アーカイブ化されていないファイルがあれば、
				if (!IsArchivedFlags[i])					// バイト配列に変換しアーカイブ化
				{
					fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read);		// i番目のファイルを読み込みモードでストリーム生成
					byte[] data = new byte[fileStream.Length];									// 書込用データ生成

					this.FileInfoList.AddData(					// ファイルリストに i 番目のデータの情報を追加
						Path.GetFileName(files[i]),				// ファイル名はパスを除きファイル名部分のみ
						byteCounter,							// 前のファイルのまでの合計バイト数が開始位置
						data.Length,							// バイト配列の要素数がファイルサイズ
						ArchiveFileTypeTools.IsEncrypteFileType(Path.GetExtension(files[i]))	// 暗号化の有無
					);

					fileStream.Read(data, 0, data.Length);		// i番目のファイルをバイト配列に読み込み
					this.ArchivedData.Add(data);				// 読み込んだバイト配列をアーカイブするデータとして追加

					byteCounter += data.Length;					// 合計バイト数計算 (追加したデータのバイト数加算してくだけ)

					fileStream.Close();							// ファイルを解放
					fileStream.Dispose();
				}
				else
				{
					byteCounter += this.ArchivedData[i].Length;
				}

				this.IsArchivedFlags[i] = true;
			}

			// === ファイル情報リストをシリアライズし、バイト配列として書き込める状態に変換する ===
			MemoryStream memoryStream = new MemoryStream();						// シリアライズ用のメモリストリームを生成
			new BinaryFormatter().Serialize(memoryStream, this.FileInfoList);	// メモリストリームにファイル情報リストをシリアライズ
			memoryStream.Seek(0, SeekOrigin.Begin);								// メモリストリームを先頭にシーク
			byte[] fileListPart = new byte[memoryStream.Length];				// ファイル情報リストの書込用バイト配列を生成
			memoryStream.Read(fileListPart, 0, fileListPart.Length);			// シリアル化されたファイル情報リストをバイト配列に変換

			// === バイト配列に変換されたファイル情報リストのサイズから、ヘッダ部のバイト配列を生成する ===
			byte[] headerPart = Encoding.ASCII.GetBytes(memoryStream.Length.ToString().PadLeft(40, '0'));

			// === アーカイブへの書き込みを行う ===
			fileStream = new FileStream(savePath, FileMode.Create);				// アーカイブファイル書込用ストリーム生成

			for (int i = 0; i < headerPart.Length; i++)							// 1 バイトずつ暗号化しながらヘッダ部を書き込み
				fileStream.WriteByte((byte)(headerPart[i] ^ ArchiveController.GetEncrypteKey(i, headerPart.Length)));

			for (int i = 0; i < fileListPart.Length; i++)						// 1 バイトずつ暗号化しながらファイルリスト部を書き込み
				fileStream.WriteByte((byte)(fileListPart[i] ^ ArchiveController.GetEncrypteKey(i, fileListPart.Length)));

			for (int i = 0; i < this.ArchivedData.Count; i++)					// データ部を全て書き込み
			{
				if (this.FileInfoList.GetFileInfo(i).IsEncrypted)				// i 番目のファイルが暗号化すべきファイルである場合
				{																// 1 バイトずつ XOR 暗号化を行いながら書き込み
					for (int j = 0; j < this.ArchivedData[i].Length; j++)
						fileStream.WriteByte((byte)(this.ArchivedData[i][j] ^ ArchiveController.GetEncrypteKey(j, this.ArchivedData[i].Length)));
				}
				else															// 暗号化を行わないファイルである場合
				{																// 直接書き込み
					fileStream.Write(this.ArchivedData[i], 0, this.ArchivedData[i].Length);
				}
			}

			fileStream.Close();
		}


		/// <summary>
		/// 指定したインデックス番号に対応するアーカイブ済みデータを削除する。
		/// </summary>
		/// <param name="index">削除するデータのインデックス番号。</param>
		public void Delete(int index)
		{
			this.ArchivedData.RemoveAt(index);
			this.FileInfoList.Delete(index);
			this.IsArchivedFlags.RemoveAt(index);
		}


		/// <summary>
		/// アーカイブされたファイルのファイル名一覧を取得する。
		/// </summary>
		public string[] FileList
		{
			get { return this.FileInfoList.GetFileNames(); }
		}


		/// <summary>
		/// アーカイブファイルのサイズを取得する。アーカイブファイルを開いていない場合は -1 となる。
		/// </summary>
		public int ArchiveFileSize
		{
			get
			{
				if (this.ArchiveDataStream == null) return -1;
				else return (int)this.ArchiveDataStream.Length;
			}
		}

	}
}
