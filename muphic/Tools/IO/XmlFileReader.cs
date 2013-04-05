using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using Muphic.CompositionScreenParts;
using Muphic.PlayerWorks;
using Muphic.Manager;
using Muphic.MakeStoryScreenParts;

namespace Muphic.Tools.IO
{
	/// <summary>
	/// XML 読み込みツールクラス (継承不可)
	/// <para>XML ファイルからの読み込みを行う静的メソッドを公開する。</para>
	/// </summary>
	public sealed class XmlFileReader
	{
		/// <summary>
		/// XMLFileReader クラスのインスタンス生成は許可しない。
		/// </summary>
		private XmlFileReader() { }


		#region データ読み込み

		/// <summary>
		/// 指定された型のデータを XML ファイルから読み込む。
		/// </summary>
		/// <typeparam name="ReadType">読み込むデータの型。</typeparam>
		/// <param name="filePath">読み込むデータとなる XML ファイルのパス。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <returns>読み込んだデータ (失敗した場合は その型のデフォルトタイプ)。</returns>
		public static ReadType ReadSaveData<ReadType>(string filePath, bool enabledLogging) where ReadType : new()
		{
			return ReadSaveData<ReadType>(filePath, null, enabledLogging);
		}

		/// <summary>
		/// 指定された型のデータを XML ファイルのバイト配列から読み込む。
		/// </summary>
		/// <typeparam name="ReadType">読み込むデータの型。</typeparam>
		/// <param name="data">読み込むデータとなるバイト配列。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <returns>読み込んだデータ (失敗した場合は その型のデフォルトタイプ)。</returns>
		public static ReadType ReadSaveData<ReadType>(byte[] data, bool enabledLogging) where ReadType : new()
		{
			return ReadSaveData<ReadType>(null, data, enabledLogging);
		}

		/// <summary>
		/// 指定された型のデータを XML ファイルから読み込む。
		/// </summary>
		/// <typeparam name="ReadType">読み込むデータの型。</typeparam>
		/// <param name="filePath">読み込むデータとなる XML ファイルのパス (バイト配列から読み込む場合は null)。</param>
		/// <param name="data">読み込むデータとなるバイト配列 (ファイル名を指定する場合は null)。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <returns>読み込んだデータ (失敗した場合は その型のデフォルトタイプ)。</returns>
		private static ReadType ReadSaveData<ReadType>(string filePath, byte[] data, bool enabledLogging) where ReadType : new()
		{
			if (filePath != null && !File.Exists(filePath))
			{
				if (enabledLogging) LogFileManager.WriteLineError(Properties.Resources.Msg_XmlFileReader_FileNotFound, filePath);
				return default(ReadType);
			}

			XmlSerializer serializer = new XmlSerializer(typeof(ReadType));		// シリアライズ用オブジェクト
			Stream stream = null;												// 読込用ストリーム	
			ReadType readData;													// 読み込んだデータ

			try
			{
				if (filePath != null)											// ファイルパスが指定されていたら
					stream = new FileStream(filePath, FileMode.Open);			//   ファイルストリームを生成し読み込み
				else if (filePath == null && data != null)						// ファイルパスが null かつデータが渡されていた場合
					stream = new MemoryStream(data);							//   メモリストリームにデータを読み込み
				else
					new Exception("コード記述エラー; 逆シリアル化するファイルパス及びデータが未指定");

				readData = (ReadType)serializer.Deserialize(stream);			// 読み込んだストリームから逆シリアル化
				if (enabledLogging)
				{																// ログに書き込み
					LogFileManager.WriteLine(Properties.Resources.Msg_XmlFileReader_Deserialize_Success, filePath == null ? "byte[] len:" + data.Length : filePath);
				}
			}
			catch (Exception exception)
			{
				if (enabledLogging)
				{
					LogFileManager.WriteLineError(Properties.Resources.Msg_XmlFileReader_Deserialize_Failure, filePath);
					LogFileManager.WriteLineError(exception.ToString());
				}
				return default(ReadType);
			}
			finally
			{
				if (stream != null) stream.Close();
			}

			return readData;
		}

		#endregion


		#region ファイル一覧取得

		/// <summary>
		/// 与えられた拡張子と一致するセーブデータのファイル名一覧を取得する。
		/// </summary>
		/// <param name="directoryPath">取得するディレクトリのパス。</param>
		/// <param name="extension">一覧に含めるセーブデータの拡張子。</param>
		/// <returns>セーブデータのファイル名一覧。</returns>
		private static string[] GetSaveDataList(string directoryPath, string extension)
		{
			string[] fileList = null;
			List<string> result = new List<string>();

			try
			{
				if (Directory.Exists(directoryPath))
				{													// 取得先のディレクトリが存在すれば
					fileList = Directory.GetFiles(directoryPath);	// ディレクトリ内の全てのファイル名を取得
				}
				else
				{													// 取得先のディレクトリが存在しなければ
					return result.ToArray();						// 空のリストを返す
				}
			}
			catch (Exception exception)
			{
				LogFileManager.WriteLineError(Properties.Resources.Msg_XmlFileReader_GetFileList_Failure, directoryPath);
				LogFileManager.WriteLineError(exception.ToString());
				return result.ToArray();
			}

			// 楽譜データの条件に一致するファイル名のみ抽出
			foreach (string fileName in fileList)
			{
				// 拡張子によるチェック
				if (Path.GetExtension(fileName) == extension)
				{
					result.Add(fileName);
				}
			}

			return result.ToArray();
		}

		#endregion


		#region 楽譜データ読み込み

		/// <summary>
		/// XML ファイルから楽譜データを読み込む。
		/// </summary>
		/// <param name="filePath">読み込む楽譜データとなる XML ファイルのパス。</param>
		/// <returns>読み込んだ楽譜データ (失敗した場合は楽譜データ)。</returns>
		public static ScoreData ReadScoreData(string filePath)
		{
			return XmlFileReader.ReadSaveData<ScoreData>(filePath, null, true);
		}

		/// <summary>
		/// XML ファイルから楽譜データを読み込む。
		/// </summary>
		/// <param name="filePath">読み込む楽譜データとなる XML ファイルのパス。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <returns>読み込んだ楽譜データ (失敗した場合は空の楽譜データ)。</returns>
		public static ScoreData ReadScoreData(string filePath, bool enabledLogging)
		{
			return XmlFileReader.ReadSaveData<ScoreData>(filePath, null, true);
		}


		/// <summary>
		/// 楽譜データのファイル名一覧を取得する。
		/// </summary>
		/// <returns>。</returns>
		public static string[] GetScoreDataList()
		{
			return GetSaveDataList(ConfigurationManager.Current.ScoreSaveFolder, Settings.System.Default.FileExtension_ScoreData);
		}

		/// <summary>
		/// 楽譜データのファイル名一覧を取得する。
		/// </summary>
		/// <param name="directoryPath">取得するディレクトリのパス。</param>
		/// <returns>楽譜データのファイル名一覧。</returns>
		public static string[] GetScoreDataList(string directoryPath)
		{
			return GetSaveDataList(directoryPath, Settings.System.Default.FileExtension_ScoreData);
		}

		#endregion


		#region 物語データ読み込み

		/// <summary>
		/// XML ファイルから物語データを読み込む。
		/// </summary>
		/// <param name="filePath">読み込む物語データとなる XML ファイルのパス。</param>
		/// <returns>読み込んだ物語データ (失敗した場合は空の物語データ)。</returns>
		public static StoryData ReadStoryData(string filePath)
		{
			return XmlFileReader.ReadSaveData<StoryData>(filePath, null, true);
		}

		/// <summary>
		/// XML ファイルから物語データを読み込む。
		/// </summary>
		/// <param name="filePath">読み込む物語データとなる XML ファイルのパス。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <returns>読み込んだ物語データ (失敗した場合は空の物語データ)。</returns>
		public static StoryData ReadStoryData(string filePath, bool enabledLogging)
		{
			return XmlFileReader.ReadSaveData<StoryData>(filePath, null, enabledLogging);
		}


		/// <summary>
		/// 物語データのファイル名一覧を取得する。
		/// </summary>
		/// <returns>物語データのファイル名一覧。</returns>
		public static string[] GetStoryDataList()
		{
			return GetSaveDataList(ConfigurationManager.Current.StorySaveFolder, Settings.System.Default.FileExtension_StoryData);
		}

		/// <summary>
		/// 物語データのファイル名一覧を取得する。
		/// </summary>
		/// <param name="directoryPath">取得するディレクトリのパス。</param>
		/// <returns>物語データのファイル名一覧。</returns>
		public static string[] GetStoryDataList(string directoryPath)
		{
			return GetSaveDataList(directoryPath, Settings.System.Default.FileExtension_StoryData);
		}

		#endregion

	}
}
