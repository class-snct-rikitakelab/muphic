
namespace Muphic.EntitleScreenParts.Katakana
{
	/// <summary>
	/// 汎用題名入力画面上の"カナ"カテゴリボタン
	/// </summary>
	public class KatakanaCategoryButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }

		/// <summary>
		/// "カナ"カテゴリボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる汎用題名入力画面。</param>
		public KatakanaCategoryButton(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			this.SetBgTexture(this.ToString(), Locations.KatakanaCategoryButton, "IMAGE_BUTTON_BOX2_BLUE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.KatakanaCategoryButton, "IMAGE_ENTITLESCR_KATAKANABTN");
		}


		/// <summary>
		/// "カナ"カテゴリボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.EntitleCategoryMode = EntitleCategoryMode.Katakana;
		}
	}
}
