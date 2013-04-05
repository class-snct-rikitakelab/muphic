using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using Muphic.CompositionScreenParts;
using Muphic.PlayerWorks;
using Muphic.Manager;
using Muphic.MakeStoryScreenParts;

namespace Muphic.Tools.IO
{
	/// <summary>
	/// XML 書き込みツールクラス (継承不可)
	/// <para>XML ファイルへの書き込みを行う静的メソッドを公開する。</para>
	/// </summary>
	public sealed class XmlFileWriter
	{
		/// <summary>
		/// XMLFileWriter クラスのインスタンス生成は許可しない。
		/// </summary>
		private XmlFileWriter() { }


		#region データ書き込み

		/// <summary>
		/// 与えられた型のデータを XML ファイルに書き込む。
		/// </summary>
		/// <typeparam name="WriteType">書き込むデータの型 (ScoreData や StoryData など)。</typeparam>
		/// <param name="saveData">書き込むデータ。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <param name="savePath">保存先のパス。</param>
		/// <returns>正常に書き込まれた場合は true、それ以外は false。</returns>
		public static bool WriteSaveData<WriteType>(WriteType saveData, bool enabledLogging, string savePath) where WriteType : new()
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(savePath));		// ディレクトリ作成
			}
			catch (PathTooLongException exception)
			{
				if (enabledLogging)
				{
					LogFileManager.WriteLineError(Properties.Resources.Msg_XmlFileWriter_CreateDirectoryFailure_PathTooLong, savePath);
					LogFileManager.WriteLineError(exception.ToString());
				}
				return false;
			}
			catch (Exception exception)
			{
				if (enabledLogging)
				{
					LogFileManager.WriteLineError(Properties.Resources.Msg_XmlFileWriter_CreateDirectoryFailure, savePath);
					LogFileManager.WriteLineError(exception.ToString());
				}
				return false;
			}

			XmlSerializer serializer = new XmlSerializer(typeof(WriteType));	// シリアル化用オブジェクト生成
			FileStream stream = null;											// 書き込み用ファイルストリーム

			try
			{
				stream = new FileStream(savePath, FileMode.Create);				// ファイルストリーム生成
				serializer.Serialize(stream, saveData);							// シリアル化して書き込み
				if (enabledLogging)
				{																// ログに書き込み
					LogFileManager.WriteLine(Properties.Resources.Msg_XmlFileWriter_Serialize_Success, savePath);
				}
			}
			catch (InvalidOperationException exception)
			{
				if (enabledLogging)												// シリアル化に失敗した場合
				{																// 例外メッセージと共にログに書き込み
					LogFileManager.WriteLineError(Properties.Resources.Msg_XmlFileWriter_Serialize_Failure, savePath);
					LogFileManager.WriteLineError(exception.ToString());
				}
				return false;
			}
			catch (Exception exception)
			{
				if (enabledLogging)												// 何らかの理由で書き込みに失敗した場合
				{																// 例外メッセージと共にログに書き込み
					LogFileManager.WriteLineError(Properties.Resources.Msg_XmlFileWriter_FileWrite_Failure, savePath);
					LogFileManager.WriteLineError(exception.ToString());
				}
				return false;
			}
			finally
			{
				if (stream != null) stream.Close();								// ストリームを閉じる
			}

			return true;
		}

		#endregion


		#region 楽譜データ書き込み

		/// <summary>
		/// 楽譜データを XML ファイルに書き込む。
		/// </summary>
		/// <remarks>
		/// ログファイルへの書き込みの有無を指定しなかった場合は自動的に書き込みを行う。
		/// <para>また、保存先パスを指定しなかった場合は楽譜データ既定の位置に曲名と同じファイル名で保存される。</para>
		/// </remarks>
		/// <param name="scoreData">書き込む楽譜データ。</param>
		/// <returns>正常に書き込まれた場合は true、それ以外は false。</returns>
		public static bool WriteScoreData(ScoreData scoreData)
		{
			return XmlFileWriter.WriteSaveData<ScoreData>(scoreData, true, XmlFileWriter.CreateScoreDataPath(scoreData.ScoreName));
		}

		/// <summary>
		/// 楽譜データを XML ファイルに書き込む。
		/// </summary>
		/// <remarks>
		/// 保存先パスを指定しなかった場合は楽譜データ既定の位置に曲名と同じファイル名で保存される。
		/// </remarks>
		/// <param name="scoreData">書き込む楽譜データ。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <returns>正常に書き込まれた場合は true、それ以外は false。</returns>
		public static bool WriteScoreData(ScoreData scoreData, bool enabledLogging)
		{
			return XmlFileWriter.WriteSaveData<ScoreData>(scoreData, enabledLogging, XmlFileWriter.CreateScoreDataPath(scoreData.ScoreName));
		}

		/// <summary>
		/// 楽譜データを XML ファイルに書き込む。
		/// </summary>
		/// <param name="scoreData">書き込む楽譜データ。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <param name="savePath">保存先のパス。</param>
		/// <returns>正常に書き込まれた場合は true、それ以外は false。</returns>
		public static bool WriteScoreData(ScoreData scoreData, bool enabledLogging, string savePath)
		{
			return XmlFileWriter.WriteSaveData<ScoreData>(scoreData, enabledLogging, savePath);
		}


		/// <summary>
		/// 書き込む楽譜データの保存先パスを生成する。
		/// </summary>
		/// <param name="scoreName">楽譜名。</param>
		/// <returns>生成された楽譜データの保存先パス。</returns>
		public static string CreateScoreDataPath(string scoreName)
		{
			StringBuilder filePath = new StringBuilder();

			filePath.Append(ConfigurationManager.Current.ScoreSaveFolder);		// ディレクトリ設定
			if (filePath[filePath.Length - 1] != '\\') filePath.Append("\\");	// 末尾に\記号追加
			filePath.Append(scoreName);											// 楽譜名追加
			filePath.Append(Settings.System.Default.FileExtension_ScoreData);	// 拡張子追加

			return filePath.ToString();
		}

		#endregion


		#region 物語データ書き込み

		/// <summary>
		/// 物語データを XML ファイルに書き込む。
		/// </summary>
		/// <remarks>
		/// ログファイルへの書き込みの有無を指定しなかった場合は自動的に書き込みを行う。
		/// <para>また、保存先パスを指定しなかった場合は物語データ既定の位置に物語名と同じファイル名で保存される。</para>
		/// </remarks>
		/// <param name="storyData">書き込む物語データ。</param>
		/// <returns>正常に書き込まれた場合は true、それ以外は false。</returns>
		public static bool WriteStoryData(StoryData storyData)
		{
			return XmlFileWriter.WriteSaveData<StoryData>(storyData, true, XmlFileWriter.CreateStoryDataPath(storyData.Title));
		}

		/// <summary>
		/// 物語データを XML ファイルに書き込む。
		/// </summary>
		/// <remarks>
		/// また、保存先パスを指定しなかった場合は物語データ既定の位置に物語名と同じファイル名で保存される。
		/// </remarks>
		/// <param name="storyData">書き込む物語データ。</param>
		/// <param name="savePath">保存先のパス。</param>
		/// <returns>正常に書き込まれた場合は true、それ以外は false。</returns>
		public static bool WriteStoryData(StoryData storyData, string savePath)
		{
			return XmlFileWriter.WriteSaveData<StoryData>(storyData, true, savePath);
		}

		/// <summary>
		/// 物語データを XML ファイルに書き込む。
		/// </summary>
		/// <param name="storyData">書き込む物語データ。</param>
		/// <param name="enabledLogging">ログファイルに書き込みを行う場合は true、それ以外は false。</param>
		/// <param name="savePath">保存先のパス。</param>
		/// <returns>正常に書き込まれた場合は true、それ以外は false。</returns>
		public static bool WriteStoryData(StoryData storyData, bool enabledLogging, string savePath)
		{
			return XmlFileWriter.WriteSaveData<StoryData>(storyData, enabledLogging, savePath);
		}


		/// <summary>
		/// 書き込む物語データの保存先パスを生成する。
		/// </summary>
		/// <param name="storyName">物語名。</param>
		/// <returns>生成された物語データの保存先パス。</returns>
		public static string CreateStoryDataPath(string storyName)
		{
			StringBuilder filePath = new StringBuilder();

			filePath.Append(ConfigurationManager.Current.StorySaveFolder);		// ディレクトリ設定
			if (filePath[filePath.Length - 1] != '\\') filePath.Append("\\");	// 末尾に\記号追加
			filePath.Append(storyName);											// 楽譜名追加
			filePath.Append(Settings.System.Default.FileExtension_ScoreData);	// 拡張子追加

			return filePath.ToString();
		}

		#endregion
	}
}
