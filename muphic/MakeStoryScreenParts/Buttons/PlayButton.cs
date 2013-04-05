
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面もの"ものがたりながす"ボタンクラス。
	/// <para>クリックされると、物語再生画面へ遷移する。</para>
	/// </summary>
	public class PlayButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		
		/// <summary>
		/// 物語作成画面の"ものがたりながす"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStorysScreen">親にあたる物語作成画面。</param>
		public PlayButton(MakeStoryScreen makeStorysScreen)
		{
			this.Parent = makeStorysScreen;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_PlayBtn, "IMAGE_BUTTON_PLAY_RED", "IMAGE_BUTTON_PLAY_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_PlayBtn, "IMAGE_MAKESTORYSCR_PLAYBTN");
		}


		/// <summary>
		/// 物語作成画面の"ものがたりながす"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = MakeStoryScreenMode.PlayStoryScreen;
		}


		/// <summary>
		/// 現在編集中の物語データから、このボタンの有効/無効の設定を行う。
		/// </summary>
		public void SetEnabled()
		{
			if (this.Parent.CurrentStoryData.MaxEditedPage == -1) this.Enabled = false;
			else this.Enabled = true;
		}

	}
}
