
namespace Muphic.EntitleScreenParts.OtherButtons
{
	/// <summary>
	/// 題名入力画面上の"けす"ボタン。
	/// </summary>
	public class SingleDeleteButton : Common.Button
	{
		/// <summary>
		/// 親にあたる題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }
		

		/// <summary>
		/// "ぜんぶけす"ボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる題名入力画面。</param>
		public SingleDeleteButton(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			this.SetBgTexture(this.ToString(), Locations.SingleDeleteButton, "IMAGE_BUTTON_BOX1_YELLOW", "IMAGE_BUTTON_BOX1_ON");
			this.SetLabelTexture(this.ToString(), Locations.SingleDeleteButton, "IMAGE_ENTITLESCR_SINGLEDELBTN");
		}


		/// <summary>
		/// "けす"ボタンがクリックされた際の処理。
		/// 題名の末尾１文字を削除する。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.SingleDelete();
		}
	}
}
