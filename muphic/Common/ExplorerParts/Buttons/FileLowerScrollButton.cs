
namespace Muphic.Common.ExplorerParts.Buttons
{
	/// <summary>
	/// エクスプローラ上のファイル選択領域で、ファイルのリストを下へスクロールさせるボタン。
	/// </summary>
	public class FileLowerScrollButton : Button
	{
		/// <summary>
		/// 親にあたるファイル選択領域。
		/// </summary>
		public FileSelectArea Parent { get; private set; }


		/// <summary>
		/// ディレクトリのリストを下へスクロールさせるボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるディレクトリ選択領域。</param>
		public FileLowerScrollButton(FileSelectArea parent)
		{
			this.Parent = parent;

			this.SetBgTexture(this.ToString(), Settings.FileLowerScrollButton, "IMAGE_DIALOG_FILESELECT_UPOFF", "IMAGE_DIALOG_FILESELECT_UPON");
			this.LabelName = "";
		}


		/// <summary>
		/// ファイルのリストを下へスクロールさせるボタンをクリックする。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.LowerScroll();
		}
	}
}
