
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// 作曲画面の"はじめから"ボタンクラス
	/// </summary>
	public class PlayFirstButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用作曲画面クラス
		/// </summary>
		public CompositionScreen Parent { get; private set; }


		/// <summary>
		/// ボタンの有効性を表わす bool 値。
		/// プロパティ "Enabled" を使用すること。
		/// </summary>
		private bool __pressed;

		/// <summary>
		/// ボタンが押されていることを表わす bool 値。
		/// このプロパティが true の時は "とまる" ボタンになり、false の時は "はじめから" ボタンになる。
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
		/// 作曲画面の"はじめから"ボタンの新しいインスタンスの初期化を行います。
		/// </summary>
		/// <param name="compositionScreen">親にあたる汎用作曲画面クラス。</param>
		public PlayFirstButton(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.SetBgTexture(this.ToString(), Locations.PlayButton, "IMAGE_BUTTON_PLAY_RED", "IMAGE_BUTTON_PLAY_ON", "IMAGE_BUTTON_PLAY_GREEN");
			this.SetLabelTexture(this.ToString(), Locations.PlayButton, "IMAGE_COMPOSITIONSCR_PLAYBTN", "IMAGE_COMPOSITIONSCR_STOPBTN");
		}


		/// <summary>
		/// 作曲画面の"はじめから"ボタンが押された際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Parent.CompositionMain.AnimalScore.AnimalList.Count == 0) return;

			if (this.Parent.CompositionMain.IsPlaying)
			{
				// 再生中の場合
				this.Parent.CompositionMain.PlayStop();				// 再生を停止
			}
			else
			{
				// 非再生中の場合
				this.Parent.CompositionMain.PlayStart();			// 再生開始
				this.Parent.PlayContinueButton.Enabled = false;		// すすむボタンを無効にする
				this.Pressed = true;								// ボタンON
			}
		}


		/// <summary>
		/// "はじめから"ボタンにセットする。
		/// </summary>
		private void SetPlayButton()
		{
			Manager.DrawManager.Delete(this.LabelName);
			this.SetLabelTexture(this.ToString(), Locations.PlayButton, "IMAGE_COMPOSITIONSCR_PLAYBTN");
		}


		/// <summary>
		/// "とまる"ボタンにセットする。
		/// </summary>
		private void SetStopButton()
		{
			Manager.DrawManager.Delete(this.LabelName);
			this.SetLabelTexture(this.ToString(), Locations.PlayButton, "IMAGE_COMPOSITIONSCR_STOPBTN");
		}
	}
}
