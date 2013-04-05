
namespace Muphic.EntitleScreenParts.Hiragana
{
	/// <summary>
	/// 汎用題名入力画面上の"かな"カテゴリボタン
	/// </summary>
	public class HiraganaCategoryButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }

		/// <summary>
		/// "かな"カテゴリボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる汎用題名入力画面。</param>
		public HiraganaCategoryButton(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			this.SetBgTexture(this.ToString(), Locations.HiraganaCategoryButton, "IMAGE_BUTTON_BOX2_BLUE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.HiraganaCategoryButton, "IMAGE_ENTITLESCR_HIRAGANABTN");
		}


		/// <summary>
		/// "かな"カテゴリボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.EntitleCategoryMode = EntitleCategoryMode.Hiragana;
		}
	}
}
