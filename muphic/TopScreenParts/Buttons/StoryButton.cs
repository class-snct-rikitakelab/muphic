
namespace Muphic.TopScreenParts.Buttons
{
	/// <summary>
	/// ものがたりおんがく選択ボタンクラス (トップ画面)
	/// </summary>
	public class StoryButton : Common.Button
	{
		/// <summary>
		/// 親にあたるトップ画面クラス
		/// </summary>
		public TopScreen Parent { get; private set; }


		/// <summary>
		/// ものがたりおんがく選択ボタンの初期化を行う。
		/// </summary>
		/// <param name="topScreen">親にあたるトップ画面クラス。</param>
		public StoryButton(TopScreen topScreen)
			: base(Manager.ConfigurationManager.Current.EnabledStoryScreen)
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
			System.Drawing.Point location = Locations.StoryScreenButton;
			this.SetLabelTexture(this.ToString(), location, "IMAGE_TOPSCR_BUTTON_STORY");
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_TOP01_BLUE", "IMAGE_BUTTON_TOP01_ON");
		}

		/// <summary>
		/// ものがたりおんがく選択ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = TopScreenMode.StoryScreen;
		}
	}
}
