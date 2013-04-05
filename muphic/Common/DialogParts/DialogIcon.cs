
namespace Muphic.Common.DialogParts
{
	/// <summary>
	/// ダイアログアイコン
	/// </summary>
	public class DialogIcon : Parts
	{
		/// <summary>
		/// 親にあたるダイアログ
		/// </summary>
		public Dialog Parent { get; private set; }

		/// <summary>
		/// ダイアログアイコンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="dialog">親にあたるダイアログ。</param>
		/// <param name="iconImage">アイコンテクスチャ。</param>
		public DialogIcon(Dialog dialog, string iconImage)
		{
			this.Parent = dialog;

			Manager.DrawManager.Regist(this.ToString(), Tools.CommonTools.AddPoints(this.Parent.Location, Locations.Icon), iconImage);
		}


		/// <summary>
		/// 現在の System.Object に、ダイアログの親クラスの情報を加えた System.String を返す。
		/// </summary>
		/// <returns>現在の System.Object に、ダイアログの親クラスの情報を加えた System.String。</returns>
		public override string ToString()
		{
			System.Text.StringBuilder str = new System.Text.StringBuilder();
			str.Append(base.ToString());
			str.Append(".");
			str.Append(this.Parent.ParentName);

			return str.ToString();
		}
	}
}
