
namespace Muphic.Common.ExplorerParts.Buttons
{
	/// <summary>
	/// エクスプローラ上の "読込" ボタン。
	/// </summary>
	public class LoadButton : Button
	{
		/// <summary>
		/// 親にあたるエクスプローラ。
		/// </summary>
		public Explorer Parent { get; private set; }

		/// <summary>
		/// エクスプローラ上の "読込" ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるエクスプローラ。</param>
		public LoadButton(Explorer parent)
		{
			this.Parent = parent;

			this.SetBgTexture(this.ToString(), Settings.LoadButtonLocation, "IMAGE_BUTTON_BOX2_ORANGE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.LoadButtonLocation, "IMAGE_EXPLORER_LOADBTN");
		}

		/// <summary>
		///	エクスプローラ上の "読込" ボタンをクリックする。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// 現在の System.Object に、エクスプローラの親の画面の名称を付加した System.String を返す。
		/// </summary>
		/// <returns>現在の System.Object に、エクスプローラの親の画面の名称を付加した System.String。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Parent.ParentName;
		}
	}
}
