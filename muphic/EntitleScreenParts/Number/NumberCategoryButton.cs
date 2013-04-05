
namespace Muphic.EntitleScreenParts.Number
{
	/// <summary>
	/// 汎用題名入力画面上の"１・☆"カテゴリボタン
	/// </summary>
	public class NumberCategoryButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }

		/// <summary>
		/// "１・☆"カテゴリボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる汎用題名入力画面。</param>
		public NumberCategoryButton(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			this.SetBgTexture(this.ToString(), Locations.NumberCategoryButton, "IMAGE_BUTTON_BOX2_BLUE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.NumberCategoryButton, "IMAGE_ENTITLESCR_NUMBERBTN");
		}


		/// <summary>
		/// "１・☆"カテゴリボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.EntitleCategoryMode = EntitleCategoryMode.Number;
		}
	}
}
