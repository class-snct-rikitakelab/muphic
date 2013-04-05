
namespace Muphic.NameInputScreenParts.Buttons
{
	/// <summary>
	/// プレイヤー名入力画面からトップ画面へ戻る"けってい"ボタンクラス。
	/// </summary>
	public class NameDecisionButton : Common.Button
	{
		/// <summary>
		/// 親にあたるプレイヤー名入力画面。
		/// </summary>
		public NameInputScreen Parent { get; private set; }


		/// <summary>
		/// トップ画面へ戻る"けってい"ボタンクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるプレイヤー名入力画面。</param>
		public NameDecisionButton(NameInputScreen parent)
		{
			this.Parent = parent;

			this.SetBgTexture(this.ToString(), Muphic.EntitleScreenParts.Locations.DecisionButton, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Muphic.EntitleScreenParts.Locations.DecisionButton, "IMAGE_ENTITLESCR_DECITIONBTN");
		}


		/// <summary>
		/// トップ画面へ戻る"けってい"ボタンが押された際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Parent.ScreenMode = TopScreenMode.TopScreen;
		}
	}
}
