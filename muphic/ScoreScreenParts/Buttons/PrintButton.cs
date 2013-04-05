
namespace Muphic.ScoreScreenParts.Buttons
{
	/// <summary>
	/// 楽譜を印刷するボタン。
	/// </summary>
	public class PrintButton : Muphic.Common.Button
	{
		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		private readonly ScoreScreen __parent;

		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		public ScoreScreen Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// 印刷ボタンの可視性を示す値を取得または設定する。
		/// ただし、構成設定で muphic が授業 (児童) モードでの動作となっている場合は常に無効状態になる。
		/// </summary>
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = (MainWindow.MuphicOperationMode != MuphicOperationMode.StudentMode) && value;
			}
		}

		/// <summary>
		/// 印刷ボタンの有効性を示す値を取得または設定する。
		/// ただし、構成設定で印刷機能が無効化されていた場合は常に無効状態になる。
		/// </summary>
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = Manager.ConfigurationManager.Current.EnabledPrint && value;
			}
		}


		/// <summary>
		/// 楽譜を印刷するボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public PrintButton(ScoreScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Locations.PrintButton, "IMAGE_BUTTON_BOX2_YELLOW", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Locations.PrintButton, "IMAGE_SCORESCR_PRINTBTN");
		}


		/// <summary>
		/// 楽譜を印刷するボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を示す Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScreenMode = ScoreScreenMode.PrintDialog;
		}
	}
}
