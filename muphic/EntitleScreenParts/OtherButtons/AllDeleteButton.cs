
namespace Muphic.EntitleScreenParts.OtherButtons
{
	/// <summary>
	/// 題名入力画面上の"ぜんぶけす"ボタン。
	/// </summary>
	public class AllDeleteButton : Common.Button
	{
		/// <summary>
		/// 親にあたる題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }

		/// <summary>
		/// "ぜんぶけす"ボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる題名入力画面。</param>
		public AllDeleteButton(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			this.SetBgTexture(this.ToString(), Locations.AllDeleteButton, "IMAGE_BUTTON_BOX1_YELLOW", "IMAGE_BUTTON_BOX1_ON");
			this.SetLabelTexture(this.ToString(), Locations.AllDeleteButton, "IMAGE_ENTITLESCR_ALLDELLBTN");
		}


		/// <summary>
		/// "ぜんぶけす"ボタンがクリックされた際の処理。
		/// 題名を空にする。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.AllDelete();
			Manager.JpnLangInputManager.KeyClear();
		}
	}
}
