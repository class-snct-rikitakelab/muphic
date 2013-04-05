
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// 汎用作曲画面の"のこす"ボタンクラス
	/// </summary>
	public class ScoreSaveButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用作曲画面
		/// </summary>
		public CompositionScreen Parent { get; private set; }

		/// <summary>
		/// 汎用作曲画面の"のこす"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる汎用作曲画面。</param>
		public ScoreSaveButton(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.SetBgTexture(this.ToString(), Locations.SaveButton, "IMAGE_BUTTON_BOX2_GREEN", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.SaveButton, "IMAGE_COMPOSITIONSCR_SAVEBTN");
		}


		/// <summary>
		/// 汎用作曲画面の"のこす"ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = CompositionScreenMode.ScoreSaveDialog;
		}
	}
}
