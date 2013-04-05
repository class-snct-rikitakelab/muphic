
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"おんがくをつける"ボタンクラス。
	/// <para>クリックされると、現在のスライドの作曲画面へ遷移する。</para>
	/// </summary>
	public class CompositionButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }


		/// <summary>
		/// 物語作成画面の"おんがくをつける"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public CompositionButton(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_CompositionBtn, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_CompositionBtn, "IMAGE_MAKESTORYSCR_COMPOSITIONBTN");
		}


		/// <summary>
		/// 物語作成画面の"おんがくをつける"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			// 画面モードを作曲画面にする
			this.Parent.ScreenMode = MakeStoryScreenMode.CompositionScreen;
		}

	}
}
