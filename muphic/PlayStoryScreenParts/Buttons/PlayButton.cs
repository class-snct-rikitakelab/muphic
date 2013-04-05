
namespace Muphic.PlayStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語再生画面の"はじまりはじまり"ボタンクラス。
	/// <para>停止中にクリックされると物語の再生を開始し、再生中にクリックされると物語の再生を停止する。</para>
	/// </summary>
	public class PlayButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語再生画面。
		/// </summary>
		private readonly PlayStoryScreen __parent;

		/// <summary>
		/// 親にあたる物語再生画面。
		/// </summary>
		public PlayStoryScreen Parent
		{
			get { return this.__parent; }
		}


		/// <summary>
		/// ボタンの有効性を表わすブール値。
		/// Enabled プロパティを使用すること。
		/// </summary>
		private bool __pressed;

		/// <summary>
		/// ボタンが押されていることを表わすブール値。
		/// このプロパティが true の時は "とめる" ボタンになり、false の時は "はじまりはじまり" ボタンになる。
		/// </summary>
		public new bool Pressed
		{
			get
			{
				return this.__pressed;
			}
			set
			{
				base.Pressed = value;
				this.__pressed = value;

				if (value) this.SetStopButton();
				else this.SetPlayButton();
			}
		}

		/// <summary>
		/// 物語再生画面の"はじまりはじまり"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">このボタンの親となる物語再生画面。</param>
		public PlayButton(PlayStoryScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.PlayStoryScr_PlayBtn, "IMAGE_BUTTON_PLAY_RED", "IMAGE_BUTTON_PLAY_ON", "IMAGE_BUTTON_PLAY_GREEN");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.PlayStoryScr_PlayBtn, "IMAGE_PLAYSTORYSCR_PLAYSTARTBTN");
		}


		/// <summary>
		/// 物語再生画面の"はじまりはじまり"ボタンがクリックされると実行される。
		/// </summary>
		/// <param name="mouseStatus">クリック時の状態データ。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Pressed)
			{
				// 押下状態(再生中)であれば停止
				this.Parent.PlayStop();
			}
			else
			{
				// 非押下状態(停止中)であれば再生開始
				this.Parent.PlayStart();
			}
		}


		/// <summary>
		/// "はじまりは"ボタンにセットする。
		/// </summary>
		private void SetPlayButton()
		{
			Manager.DrawManager.Delete(this.LabelName);
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.PlayStoryScr_PlayBtn, "IMAGE_PLAYSTORYSCR_PLAYSTARTBTN");
		}

		/// <summary>
		/// "とめる"ボタンにセットする。
		/// </summary>
		private void SetStopButton()
		{
			Manager.DrawManager.Delete(this.LabelName);
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.PlayStoryScr_PlayBtn, "IMAGE_PLAYSTORYSCR_PLAYSTOPBTN");
		}

	}
}
