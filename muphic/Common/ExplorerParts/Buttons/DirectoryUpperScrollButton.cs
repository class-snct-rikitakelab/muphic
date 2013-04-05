
namespace Muphic.Common.ExplorerParts.Buttons
{
	/// <summary>
	/// エクスプローラ上のディレクトリ選択領域で、ディレクトリのリストを上へスクロールさせるボタン。
	/// </summary>
	public class DirectoryUpperScrollButton : Button
	{
		/// <summary>
		/// 親にあたるディレクトリ選択領域。
		/// </summary>
		public DirectorySelectArea Parent { get; private set; }


		/// <summary>
		/// ディレクトリのリストを上へスクロールさせるボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるディレクトリ選択領域。</param>
		public DirectoryUpperScrollButton(DirectorySelectArea parent)
		{
			this.Parent = parent;

			this.SetBgTexture(this.ToString(), Settings.DirectoryUpperScrollButton, "IMAGE_DIALOG_FILESELECT_UPOFF", "IMAGE_DIALOG_FILESELECT_UPON");
			this.LabelName = "";
		}


		/// <summary>
		/// ディレクトリのリストを上へスクロールさせるボタンをクリックする。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.UpperScroll();
		}
	}
}
