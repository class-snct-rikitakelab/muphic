
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// 作曲画面の"あたらしくつくる"ボタン
	/// </summary>
	public class CreateNewButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用作曲画面
		/// </summary>
		public CompositionScreen Parent { get; private set; }

		/// <summary>
		/// 作曲画面の"あたらしくつくる"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる汎用作曲画面。</param>
		public CreateNewButton(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.SetBgTexture(this.ToString(), Locations.CreateButton, "IMAGE_BUTTON_BOX2_YELLOW", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.CreateButton, "IMAGE_COMPOSITIONSCR_CREATEBTN");
		}

		/// <summary>
		/// 作曲画面の"あたらしくつくる"ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = CompositionScreenMode.CreateNewDialog;
		}
	}
}
