
namespace Muphic.Common.ExplorerParts.Buttons
{
	/// <summary>
	/// エクスプローラ上のディレクトリ選択領域で、ディレクトリのリストを下へスクロールさせるボタン。
	/// </summary>
	public class DirectoryLowerScrollButton : Button
	{
		/// <summary>
		/// 親にあたるディレクトリ選択領域。
		/// </summary>
		public DirectorySelectArea Parent { get; private set; }


		/// <summary>
		/// ディレクトリのリストを下へスクロールさせるボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるディレクトリ選択領域。</param>
		public DirectoryLowerScrollButton(DirectorySelectArea parent)
		{
			this.Parent = parent;

			this.SetBgTexture(this.ToString(), Settings.DirectoryLowerScrollButton, "IMAGE_DIALOG_FILESELECT_DNOFF", "IMAGE_DIALOG_FILESELECT_DNON");
			this.LabelName = "";
		}


		/// <summary>
		/// ディレクトリのリストを下へスクロールさせるボタンをクリックする。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.LowerScroll();
		}
	}
}
