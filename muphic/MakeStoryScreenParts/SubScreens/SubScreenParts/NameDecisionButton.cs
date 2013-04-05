using Muphic.MakeStoryScreenParts.Dialogs;

namespace Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts
{
	/// <summary>
	/// 制作者名入力画面から制作者名入力ダイアログへ戻る "けってい" ボタンクラス。
	/// クリックされると、入力された名前を物語の制作者名として繁栄し、元のダイアログへ遷移する。
	/// </summary>
	public class NameDecisionButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語名入力画面
		/// </summary>
		public NameInputScreen_ Parent { get; private set; }

		/// <summary>
		/// このボタンで入力される制作者が何人目かを示す整数を取得する。
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// 物語作成画面へ戻る"けってい"ボタンクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語名入力画面。</param>
		public NameDecisionButton(NameInputScreen_ parent)
		{
			this.Parent = parent;
			this.Number = 0;

			this.SetBgTexture(this.ToString(), Muphic.EntitleScreenParts.Locations.DecisionButton, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Muphic.EntitleScreenParts.Locations.DecisionButton, "IMAGE_ENTITLESCR_DECITIONBTN");
		}


		/// <summary>
		/// 物語作成画面へ戻る"けってい"ボタンが押された際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Parent.Authors[this.Number].Name = this.Parent.Text;
			this.Parent.Parent.DialogMode = NameInputDialogMode.NameInputDialog;
		}
	}
}
