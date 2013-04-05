
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"だいめい"ボタンクラス。
	/// <para>クリックされると、題名入力画面へ遷移する。</para>
	/// </summary>
	public class TitleButton : Common.Button
	{
		/// <summary>
		/// 親にあた物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }


		/// <summary>
		/// 物語作成画面の"だいめい"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public TitleButton(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_TitleBtn, "IMAGE_BUTTON_BOX2_BLUE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_TitleBtn, "IMAGE_MAKESTORYSCR_TITLEBTN");
		}


		/// <summary>
		/// 物語作成画面の"だいめい"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = MakeStoryScreenMode.EntitleScreen;
		}
	}
}
