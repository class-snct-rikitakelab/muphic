
namespace Muphic.Common.ExplorerParts.Buttons
{
	/// <summary>
	/// エクスプローラ上のディレクトリ選択領域の "更新" ボタン。
	/// </summary>
	public class ReloadButton : Button
	{
		/// <summary>
		/// 親にあたるディレクトリ選択領域。
		/// </summary>
		public DirectorySelectArea Parent { get; private set; }

		/// <summary>
		/// エクスプローラ上のディレクトリ選択領域の "更新" ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるディレクトリ選択領域。</param>
		public ReloadButton(DirectorySelectArea parent)
		{
			this.Parent = parent;

			this.SetBgTexture(this.ToString(), Settings.ReloadButtonLocation, "IMAGE_BUTTON_BOX2_BLUE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.ReloadButtonLocation, "IMAGE_EXPLORER_RELOADBTN");
		}


		/// <summary>
		/// エクスプローラ上のディレクトリ選択領域の "更新" ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Update();
		}


		/// <summary>
		/// 現在の System.Object に、エクスプローラの親の画面の名称を付加した System.String を返す。
		/// </summary>
		/// <returns>現在の System.Object に、エクスプローラの親の画面の名称を付加した System.String。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Parent.Parent.ParentName;
		}
	}
}
