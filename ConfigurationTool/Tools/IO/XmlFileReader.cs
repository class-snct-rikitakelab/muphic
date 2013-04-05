using System.IO;
using System.Xml.Serialization;

namespace ConfigurationTool.Tools.IO
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
		/// <param name="filePath">読み込むデータとなる XML ファイルのパス (バイト配列から読み込む場合は null)。</param>
		/// <returns>読み込んだデータ (失敗した場合は その型のデフォルトタイプ)。</returns>
		public static ReadType ReadSaveData<ReadType>(string filePath) where ReadType : new()
		{
			if (!File.Exists(filePath))
			{
				return default(ReadType);
			}

			XmlSerializer serializer = new XmlSerializer(typeof(ReadType));		// シリアライズ用オブジェクト
			Stream stream = null;												// 読込用ストリーム	
			ReadType readData;													// 読み込んだデータ

			try
			{
				stream = new FileStream(filePath, FileMode.Open);				// ファイルストリームを生成し読み込み
				readData = (ReadType)serializer.Deserialize(stream);			// 読み込んだストリームから逆シリアル化
			}
			finally
			{
				if (stream != null) stream.Close();
			}

			return readData;
		}

		#endregion

	}
}
