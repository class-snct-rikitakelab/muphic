
namespace Muphic.TopScreenParts.Buttons
{
	/// <summary>
	/// ひとりでおんがく選択ボタンクラス (トップ画面)
	/// </summary>
	public class OneButton : Common.Button
	{
		/// <summary>
		/// 親にあたるトップ画面クラス
		/// </summary>
		public TopScreen Parent { get; private set; }


		/// <summary>
		/// ひとりでおんがく選択ボタンの初期化を行う。
		/// </summary>
		/// <param name="topScreen">親にあたるトップ画面クラス。</param>
		public OneButton(TopScreen topScreen)
			: base(Manager.ConfigurationManager.Current.EnabledOneScreen)
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
			System.Drawing.Point locaton = Locations.OneScreenButton;
			this.SetLabelTexture(this.ToString(), locaton, "IMAGE_TOPSCR_BUTTON_ONE");
			this.SetBgTexture(this.ToString(), locaton, "IMAGE_BUTTON_TOP01_BLUE", "IMAGE_BUTTON_TOP01_ON");
		}

		/// <summary>
		/// ひとりでおんがく選択ボタンがクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = TopScreenMode.OneScreen;
		}
	}
}
