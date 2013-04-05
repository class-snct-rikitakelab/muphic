
namespace Muphic.Settings
{
	/// <summary>
	/// muphic の動作を決定するシステム設定を取得するための静的なプロパティ及びメソッドを提供する。
	/// </summary>
	public sealed class SystemSettings
	{
		private SystemSettings() { }

		
		/// <summary>
		/// 作品データ保存時の XML 名前空間。
		/// </summary>
		public const string XmlNameSpace = "http://muphic.sendai-nct.ac.jp";

		///// <summary>
		///// 作品データ保存時の XML 名前空間を取得する。
		///// </summary>
		//public static string XmlNameSpace
		//{
		//    get
		//    {
		//        return "http://muphic.sendai-nct.ac.jp";
		//    }
		//}

	}
}
