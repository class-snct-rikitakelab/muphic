
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// 汎用作曲画面の"だいめい"ボタンクラス
	/// </summary>
	public class TitleButton : Common.Button
	{
		/// <summary>
		/// 親にあたる作曲画面
		/// </summary>
		public CompositionScreen Parent { get; private set; }


		/// <summary>
		/// "だいめい"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる作曲画面。</param>
		public TitleButton(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.SetBgTexture(this.ToString(), Locations.TitleButton, "IMAGE_BUTTON_BOX2_BLUE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.TitleButton, "IMAGE_COMPOSITIONSCR_TITLEBTN");
		}


		/// <summary>
		/// "だいめい"ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = CompositionScreenMode.EntitleScreen;
		}
	}
}
