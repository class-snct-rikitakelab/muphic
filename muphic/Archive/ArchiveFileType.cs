
namespace Muphic.Archive
{
	/// <summary>
	/// アーカイバが認識できるファイルの種類を示す識別子を指定する。
	/// </summary>
	public enum ArchiveFileType
	{
		/// <summary>
		/// テクスチャ
		/// </summary>
		Image,

		/// <summary>
		/// サウンド
		/// </summary>
		Sound,

		/// <summary>
		/// テキスト
		/// </summary>
		Text,

		/// <summary>
		/// テクスチャ名リスト
		/// </summary>
		TextureNameList,

		/// <summary>
		/// その他 (認識できない)
		/// </summary>
		Other,
	}



	/// <summary>
	/// アーカイバが認識できるファイルの種類に関する静的メソッドを公開する。
	/// </summary>
	public static class ArchiveFileTypeTools
	{
		/// <summary>
		/// 拡張子から、アーカイバが認識できるファイルの種類を判別する。
		/// </summary>
		/// <param name="extension">判別する拡張子。</param>
		/// <returns>判別されたファイルの種類。</returns>
		public static ArchiveFileType GetFileType(string extension)
		{
			switch (extension)
			{
				case ".jpg":
				case ".bmp":
				case ".png":
				case ".gif":
					return ArchiveFileType.Image;

				case ".wav":
					return ArchiveFileType.Sound;

				case ".txt":
					return ArchiveFileType.Text;

				case ".texturelist":
					return ArchiveFileType.TextureNameList;

				default:
					return ArchiveFileType.Other;
			}
		}


		/// <summary>
		/// 与えられたファイルの種類に対応する文字列を返す。
		/// </summary>
		/// <param name="fileType">ファイルの種類。</param>
		/// <returns>ファイルの種類に対応する文字列。</returns>
		public static string GetFileType(ArchiveFileType fileType)
		{
			switch (fileType)
			{
				case ArchiveFileType.Image:
					return "テクスチャ";

				case ArchiveFileType.Sound:
					return "音声";

				case ArchiveFileType.Text:
					return "テキスト";

				case ArchiveFileType.TextureNameList:
					return "テクスチャ名リスト";

				default:
					return "不明";
			}
		}


		/// <summary>
		/// 指定したファイルが暗号化すべき種類かどうかを確認する。
		/// </summary>
		/// <param name="fileType">確認するファイルの種類。</param>
		/// <returns>暗号化すべき種類である場合は true、それ以外は false。</returns>
		public static bool IsEncrypteFileType(ArchiveFileType fileType)
		{
			switch (fileType)
			{
				case ArchiveFileType.Text:
				case ArchiveFileType.TextureNameList:
					return true;

				default:
					return false;
			}
		}


		/// <summary>
		/// 指定したファイルが暗号化すべき種類かどうかを確認する。
		/// </summary>
		/// <param name="extension">確認するファイルの拡張子。</param>
		/// <returns>暗号化すべき種類である場合は true、それ以外は false。</returns>
		public static bool IsEncrypteFileType(string extension)
		{
			return ArchiveFileTypeTools.IsEncrypteFileType(ArchiveFileTypeTools.GetFileType(extension));
		}
	}
}
