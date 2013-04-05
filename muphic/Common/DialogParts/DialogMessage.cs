
namespace Muphic.Common.DialogParts
{
	/// <summary>
	/// ダイアログメッセージ
	/// </summary>
	public class DialogMessage : Parts
	{
		/// <summary>
		/// 親にあたるダイアログ
		/// </summary>
		public Dialog Parent { get; private set; }

		/// <summary>
		/// ダイアログメッセージの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="dialog">親にあたるダイアログ。</param>
		/// <param name="messageImage">メッセージテクスチャ。</param>
		public DialogMessage(Dialog dialog, string messageImage)
		{
			this.Parent = dialog;

			Manager.DrawManager.Regist(this.ToString(), Tools.CommonTools.AddPoints(this.Parent.Location, Locations.Message), messageImage);
		}


		/// <summary>
		/// 現在の System.Object に、ダイアログの親クラスの情報を加えた System.String を返す。
		/// </summary>
		/// <returns>。</returns>
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
