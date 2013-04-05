using System.IO;
using System.Xml.Serialization;

namespace ConfigurationTool.Tools.IO
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
		/// <typeparam name="WriteType">書き込むデータの型。</typeparam>
		/// <param name="saveData">書き込むデータ。</param>
		/// <param name="savePath">保存先のパス。</param>
		/// <returns>正常に書き込まれた場合は true、それ以外は false。</returns>
		public static bool WriteSaveData<WriteType>(WriteType saveData, string savePath) where WriteType : new()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(savePath));			// ディレクトリ作成

			XmlSerializer serializer = new XmlSerializer(typeof(WriteType));	// シリアル化用オブジェクト生成
			FileStream stream = null;											// 書き込み用ファイルストリーム

			try
			{
				stream = new FileStream(savePath, FileMode.Create);				// ファイルストリーム生成
				serializer.Serialize(stream, saveData);							// シリアル化して書き込み
			}
			finally
			{
				if (stream != null) stream.Close();								// ストリームを閉じる
			}

			return true;
		}

		#endregion
	}
}
