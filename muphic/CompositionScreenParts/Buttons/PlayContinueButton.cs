
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// 作曲画面の"すすむ"ボタンクラス
	/// </summary>
	public class PlayContinueButton : Common.Button
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
		/// このプロパティが true の時は "とまる" ボタンになり、false の時は "すすむ" ボタンになる。
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
		/// 作曲画面の"すすむ"ボタンの新しいインスタンスの初期化を行います。
		/// </summary>
		/// <param name="compositionScreen">親にあたる汎用作曲画面クラス。</param>
		public PlayContinueButton(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.SetBgTexture(this.ToString(), Locations.PlayContinueButton, "IMAGE_BUTTON_PLAY_RED", "IMAGE_BUTTON_PLAY_ON", "IMAGE_BUTTON_PLAY_GREEN");
			this.SetLabelTexture(this.ToString(), Locations.PlayContinueButton, "IMAGE_COMPOSITIONSCR_PLAYCONTINUEBTN");
		}


		/// <summary>
		/// 作曲画面の"すすむ"ボタンが押された際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			// 最後の動物が現在表示している楽譜より左にある場合は再生しない
			if (!(this.Parent.CompositionMain.AnimalScore.AnimalList.Count > 0 &&
				this.Parent.CompositionMain.AnimalScore[this.Parent.CompositionMain.AnimalScore.AnimalList.Count - 1].Place >= this.Parent.CompositionMain.DefaultPlace - 1))
			{
				return;
			}

			if (this.Parent.CompositionMain.IsPlaying)
			{
				// 再生中の場合
				this.Parent.CompositionMain.PlayStop();				// 再生を停止
			}
			else
			{
				// 非再生中の場合
				this.Parent.CompositionMain.PlayStart(false);		// 途中から再生開始
				this.Parent.PlayFirstButton.Enabled = false;		// はじめからボタンを無効にする
				this.Pressed = true;								// ボタンON
			}
		}


		/// <summary>
		/// "すすむ"ボタンにセットする。
		/// </summary>
		private void SetPlayButton()
		{
			Manager.DrawManager.Delete(this.LabelName);
			this.SetLabelTexture(this.ToString(), Locations.PlayContinueButton, "IMAGE_COMPOSITIONSCR_PLAYCONTINUEBTN");
		}


		/// <summary>
		/// "とまる"ボタンにセットする。
		/// </summary>
		private void SetStopButton()
		{
			Manager.DrawManager.Delete(this.LabelName);
			this.SetLabelTexture(this.ToString(), Locations.PlayContinueButton, "IMAGE_COMPOSITIONSCR_STOPBTN");
		}
	}
}
