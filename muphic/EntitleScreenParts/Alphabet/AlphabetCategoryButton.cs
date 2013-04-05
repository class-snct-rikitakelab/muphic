
namespace Muphic.EntitleScreenParts.Alphabet
{
	/// <summary>
	/// 汎用題名入力画面上の"ABC"カテゴリボタン
	/// </summary>
	public class AlphabetCategoryButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }

		/// <summary>
		/// "ABC"カテゴリボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる汎用題名入力画面。</param>
		public AlphabetCategoryButton(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			this.SetBgTexture(this.ToString(), Locations.AlphabetCategoryButton, "IMAGE_BUTTON_BOX2_BLUE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.AlphabetCategoryButton, "IMAGE_ENTITLESCR_ALPHABETBTN");
		}


		/// <summary>
		/// "ABC"カテゴリボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.EntitleCategoryMode = EntitleCategoryMode.Alphabet;
		}
	}
}
