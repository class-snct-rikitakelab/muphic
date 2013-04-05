
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// 汎用作曲画面の"がくふ"ボタンクラス
	/// </summary>
	public class ScoreButton : Common.Button
	{
		/// <summary>
		/// 親にあたる作曲画面
		/// </summary>
		public CompositionScreen Parent { get; private set; }

		/// <summary>
		/// "がくふ"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる作曲画面。</param>
		public ScoreButton(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.SetBgTexture(this.ToString(), Locations.ScoreButton, "IMAGE_BUTTON_BOX3_BLUE", "IMAGE_BUTTON_BOX3_ON");
			this.SetLabelTexture(this.ToString(), Locations.ScoreButton, "IMAGE_COMPOSITIONSCR_SCOREBTN");
		}

		/// <summary>
		/// "がくふ"ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = CompositionScreenMode.ScoreScreen;
		}
	}
}
