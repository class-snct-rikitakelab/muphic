
namespace Muphic.Common.DialogParts.Buttons
{
	/// <summary>
	/// ファイル選択ダイアログ上の下スクロールボタン
	/// </summary>
	public class LowerScrollButton : Common.Button
	{
		/// <summary>
		/// 親にあたるダイアログ
		/// </summary>
		public Dialog Parent { get; private set; }

		/// <summary>
		/// 下スクロールボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="dialog">親にあたるダイアログ。</param>
		public LowerScrollButton(Dialog dialog)
		{
			this.Parent = dialog;

			this.SetBgTexture(this.ToString(), Tools.CommonTools.AddPoints(this.Parent.Location, Locations.ScrollDownButton), "IMAGE_DIALOG_FILESELECT_DNOFF", "IMAGE_DIALOG_FILESELECT_DNON");
			this.LabelName = "";
		}


		/// <summary>
		/// 上スクロールボタンがクリックされた場合の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.LowerScroll();
		}

		/// <summary>
		/// 下へスクロールする。
		/// </summary>
		public void LowerScroll()
		{
			this.Parent.SelectArea.NowPage++;
		}


		/// <summary>
		/// 現在の System.Object に、親ダイアログの名前を付加した System.String を返す。
		/// </summary>
		/// <returns>現在のファイル選択領域に親ダイアログの名前を付加した System.String。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Parent.ParentName;
		}
	}
}
