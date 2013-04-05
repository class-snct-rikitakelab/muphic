using Muphic.Common;

namespace Muphic.TopScreenParts.Buttons
{
	/// <summary>
	/// 名前入力ボタンクラス (トップ画面)
	/// </summary>
	public class NameInputButton : Button
	{
		/// <summary>
		/// 親にあたるトップ画面。
		/// </summary>
		public TopScreen Parent { get; private set; }


		/// <summary>
		/// 名前入力ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="topScreen">親にあたるトップ画面クラス。</param>
		public NameInputButton(TopScreen topScreen)
		{
			this.Parent = topScreen;		// 親スクリーンの設定
			this.CompositionSetting();		// ボタン構成設定
		}

		/// <summary>
		/// テクスチャの登録や座標の設定を行う。
		/// </summary>
		private void CompositionSetting()
		{
			// ==============================
			// ボタンのテクスチャと座標の登録
			// ==============================
			this.SetLabelTexture(this.ToString(), Locations.NameInputScreenButton, "IMAGE_TOPSCR_BUTTON_NAME");
			this.SetBgTexture(this.ToString(), Locations.NameInputScreenButton, "IMAGE_BUTTON_TOP02_BLUE", "IMAGE_BUTTON_TOP02_ON");
		}

		/// <summary>
		/// 名前入力ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = TopScreenMode.NameInputScreen;
		}
	}
}
