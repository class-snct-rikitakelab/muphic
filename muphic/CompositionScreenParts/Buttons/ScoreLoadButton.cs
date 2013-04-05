
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// 汎用作曲画面の"よびだす"ボタン
	/// </summary>
	public class ScoreLoadButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用作曲画面
		/// </summary>
		public CompositionScreen Parent { get; private set; }

		/// <summary>
		/// 汎用作曲画面の"よびだす"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる汎用作曲画面。</param>
		public ScoreLoadButton(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.SetBgTexture(this.ToString(), Locations.LoadButton, "IMAGE_BUTTON_BOX2_GREEN", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.LoadButton, "IMAGE_COMPOSITIONSCR_LOADBTN");
		}


		/// <summary>
		/// 汎用作曲画面の"よびだす"ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = CompositionScreenMode.ScoreLoadDialog;
		}
	}
}
