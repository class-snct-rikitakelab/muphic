
namespace Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts
{
	/// <summary>
	/// ものがたりおんがく作曲画面から物語作成画面へ戻る"もどる"ボタンクラス。
	/// </summary>
	public class BackFromCompositionButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語作成画面
		/// </summary>
		public StoryCompositionScreen Parent { get; private set; }


		/// <summary>
		/// 物語作成画面へ戻る"もどる"ボタンクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="storyCompositionScreen">親にあたる物語作成画面。</param>
		public BackFromCompositionButton(StoryCompositionScreen storyCompositionScreen)
		{
			this.Parent = storyCompositionScreen;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.OneScr_BackBtn, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.OneScr_BackBtn, "IMAGE_MAKESTORYSCR_ENDCOMPOSEBTN");
		}


		/// <summary>
		/// 物語作成画面へ戻る"もどる"ボタンが押された際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			// 画面モードを物語作成画面に戻す
			this.Parent.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;

			this.Parent.Parent.CurrentSlideChanged();
		}
	}
}
