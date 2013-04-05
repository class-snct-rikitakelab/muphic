using System.Drawing;

namespace Muphic.MakeStoryScreenParts.Dialogs.DialogParts
{
	/// <summary>
	/// 題名入力ダイアログで、題名入力画面へ遷移する "なまえ" ボタン。
	/// </summary>
	public class NameInputButton : Common.Button
	{
		/// <summary>
		/// 親にあたる題名入力ダイアログ。
		/// </summary>
		public NameInputDialog Parent { get; private set; }

		/// <summary>
		/// このボタンで入力される制作者が何人目かを示す整数。
		/// </summary>
		private int __num;

		/// <summary>
		/// このボタンで入力される制作者が何人目かを示す整数を取得する。
		/// </summary>
		public int Number
		{
			get { return this.__num; }
		}


		/// <summary>
		/// 題名入力画面へ遷移する "なまえ" ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる題名入力ダイアログ。</param>
		/// <param name="location">このボタンを表示する座標。</param>
		/// <param name="number">このボタンで入力される制作者が何人目かを示す 0 から始まる整数。</param>
		public NameInputButton(NameInputDialog parent, Point location, int number)
		{
			this.Parent = parent;
			this.__num = number;

			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_BOX2_GREEN", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), location, "IMAGE_MAKESTORYSCR_NAMEINPUTBTN");
		}


		/// <summary>
		/// "なまえ" ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="mouseStatus"></param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.NameInputScreen.NameDecisionButton.Number = this.Number;
			this.Parent.NameInputScreen.Text = this.Parent.Authors[this.Number].Name;

			this.Parent.DialogMode = NameInputDialogMode.NameInputScreen;
		}


		/// <summary>
		/// 現在の System.Object を表す文字列に、このボタンで入力される制作者が何人目かを示す数値が付加された文字列を返す。
		/// </summary>
		/// <returns>現在の System.Object を表す文字列に、このボタンで入力される制作者が何人目かを示す数値が付加された文字列。</returns>
		public override string ToString()
		{
			return base.ToString() + this.Number.ToString();
		} 
	}
}
