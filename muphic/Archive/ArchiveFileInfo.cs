using System;
using System.Collections.Generic;

namespace Muphic.Archive
{
	/// <summary>
	/// アーカイブファイル内に含まれるファイルのリストを保持するクラス。
	/// </summary>
	[Serializable]
	public class ArchivedFileList
	{
		/// <summary>
		/// 
		/// </summary>
		public ArchivedFileList()
		{
			this.FileNames = new List<string>();
			this.FileInfo = new List<ArchivedFileInfo>();
		}

		/// <summary>
		/// 格納するファイル名を示す文字列のリストを取得または設定する。
		/// </summary>
		private List<string> FileNames { get; set; }

		/// <summary>
		/// 格納するファイルの位置やサイズを示すデータのリストを取得または設定する。
		/// </summary>
		private List<ArchivedFileInfo> FileInfo { get; set; }


		/// <summary>
		/// アーカイブ内のファイル数を取得する。
		/// </summary>
		public int Count
		{
			get { return this.FileNames.Count; }
		}


		/// <summary>
		/// データの追加を行う。
		/// </summary>
		/// <param name="fileName">追加するデータのファイル名。</param>
		/// <param name="startPosition">追加するデータのアーカイブ内の開始位置。</param>
		/// <param name="dataSize">追加するデータのサイズ。</param>
		/// <param name="isEncrypted">追加するデータが暗号化されている場合は true、暗号化されていない場合は false。</param>
		public void AddData(string fileName, long startPosition, int dataSize, bool isEncrypted)
		{
			this.FileNames.Add(fileName);
			this.FileInfo.Add(new ArchivedFileInfo(startPosition, dataSize, isEncrypted));
		}


		/// <summary>
		/// 指定したファイル名に対応するファイル情報 (アーカイブ内の位置とサイズ) を返す。
		/// </summary>
		/// <param name="fileName">ファイル情報を得るデータのファイル名。</param>
		/// <returns>ファイル情報 (ファイル名リストに存在しない場合、デフォルト値)。</returns>
		public ArchivedFileInfo GetFileInfo(string fileName)
		{
			// ファイル名リストから、該当するファイル名のインデックス番号を取得
			int index = this.FileNames.IndexOf(fileName);

			// index が -1 (ファイル名が見つからない) 場合、デフォルト値を返戻
			if (index < 0) return ArchivedFileInfo.Default;

			// ファイルがアーカイブ内に存在すれば、ファイル情報を返戻
			else return this.FileInfo[index];
		}


		/// <summary>
		/// 指定したインデックス番号に対応するファイル情報 (アーカイブ内の位置とサイズ) を返す。
		/// </summary>
		/// <param name="index">ファイル情報を得るデータのインデックス番号。</param>
		/// <returns>ファイル情報 (ファイル名リストに存在しない場合、デフォルト値)。</returns>
		public ArchivedFileInfo GetFileInfo(int index)
		{
			// index が範囲外の場合、デフォルト値を返戻
			if (index < 0 || index >= this.FileNames.Count) return ArchivedFileInfo.Default;

			// ファイルがアーカイブ内に存在すれば、ファイル情報を返戻
			else return this.FileInfo[index];
		}


		/// <summary>
		/// アーカイブ内のファイル名一覧を取得する。
		/// </summary>
		/// <returns>ファイル名一覧。</returns>
		public string[] GetFileNames()
		{
			return this.FileNames.ToArray();
		}


		/// <summary>
		/// 指定したファイルがアーカイブ内に存在するかどうかを確認する。
		/// </summary>
		/// <param name="fileName">確認するファイル。</param>
		/// <returns>ファイルがアーカイブ内に存在する場合は true、それ以外は false。</returns>
		public bool Exits(string fileName)
		{
			return this.FileNames.Contains(fileName);
		}


		/// <summary>
		/// 指定したインデックス番号に対応するアーカイブ内の読込済みデータを削除する。
		/// </summary>
		/// <param name="index">削除するデータのインデックス番号。</param>
		public bool Delete(int index)
		{
			// index が範囲外の場合はfalse
			if (index < 0 || index >= this.FileNames.Count) return false;

			this.FileNames.RemoveAt(index);
			this.FileInfo.RemoveAt(index);

			return true;
		}
	}


	/// <summary>
	/// アーカイブファイル内のそれぞれのファイルの位置やサイズを保持するアーカイブ情報データ。
	/// </summary>
	[Serializable]
	public struct ArchivedFileInfo
	{
		/// <summary>
		/// アーカイブ内のファイル位置やサイズを保持する ArchivedFileInfo 構造体の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="startPosition">ファイルの開始位置バイト数を示す整数。</param>
		/// <param name="dataSize">ファイルのバイトサイズを示す整数。</param>
		/// <param name="isEncrypted">暗号化されている場合は true、それ以外は false。</param>
		public ArchivedFileInfo(long startPosition, int dataSize, bool isEncrypted)
		{
			__startPosition = startPosition;
			__dataSize = dataSize;
			__isEncrypted = isEncrypted;
		}


		#region 開始位置

		/// <summary>
		/// ファイルの開始位置バイト数。
		/// </summary>
		private long __startPosition;

		/// <summary>
		/// ファイルの開始位置バイト数を取得する。
		/// </summary>
		public long StartPosition
		{
			get { return this.__startPosition; }
		}

		#endregion

		#region サイズ

		/// <summary>
		/// ファイルのバイトサイズ。
		/// </summary>
		private int __dataSize;

		/// <summary>
		/// ファイルのバイトサイズを取得する。
		/// </summary>
		public int DataSize
		{
			get { return this.__dataSize; }
		}

		#endregion

		#region 暗号化

		/// <summary>
		/// 暗号化されているかどうかを示すフラグ。
		/// </summary>
		private bool __isEncrypted;

		/// <summary>
		/// 暗号化されているかどうかを示す値を取得する。
		/// </summary>
		public bool IsEncrypted
		{
			get { return this.__isEncrypted; }
		}

		#endregion


		/// <summary>
		/// ArchivedFileInfo 構造体のデフォルト値 (情報を保持していない状態) を取得する。
		/// </summary>
		public static ArchivedFileInfo Default
		{
			get { return new ArchivedFileInfo(-1, -1, false); }
		}


		/// <summary>
		/// 与えられた ArchiveInfo データが、このインスタンスと等価であるかどうかを判定する。
		/// </summary>
		/// <param name="data">比較するデータ。</param>
		/// <returns>等価である場合は true、それ以外は false。</returns>
		public bool Equals(ArchivedFileInfo data)
		{
			return (this.StartPosition == data.StartPosition && this.DataSize == data.DataSize);
		}
	}
}
