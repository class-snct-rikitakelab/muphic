
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"ぶんしょう"ボタンクラス。
	/// <para>クリックされると、文章入力画面へ遷移する。</para>
	/// </summary>
	public class SentenceButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }


		/// <summary>
		/// 物語作成画面の"ぶんしょう"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public SentenceButton(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_SentenceBtn, "IMAGE_BUTTON_BOX2_BLUE", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_SentenceBtn, "IMAGE_MAKESTORYSCR_SENTENCEBTN");
		}


		/// <summary>
		/// 物語作成画面の"ぶんしょう"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = MakeStoryScreenMode.SentenceScreen;
		}

	}
}
