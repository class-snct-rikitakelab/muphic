
namespace Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts
{
	/// <summary>
	/// 物語文入力画面から物語作成画面へ戻る"けってい"ボタンクラス。
	/// <para>クリックされると、入力された文章を物語に反映し、物語作成画面へ遷移する。</para>
	/// </summary>
	public class SentenceDecisionButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語名入力画面
		/// </summary>
		public SentenceInputScreen Parent { get; private set; }


		/// <summary>
		/// 物語作成画面へ戻る"けってい"ボタンクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語名入力画面。</param>
		public SentenceDecisionButton(SentenceInputScreen parent)
		{
			this.Parent = parent;

			this.SetBgTexture(this.ToString(), Muphic.EntitleScreenParts.Locations.DecisionButton, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Muphic.EntitleScreenParts.Locations.DecisionButton, "IMAGE_ENTITLESCR_DECITIONBTN");
		}


		/// <summary>
		/// 物語作成画面へ戻る"けってい"ボタンが押された際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			// 文章を物語の編集中のページに反映
			this.Parent.Parent.CurrentSlide.Sentence = this.Parent.Text;

			// 物語作成画面へ画面遷移
			this.Parent.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
		}
	}
}
