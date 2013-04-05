
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"せんせいにだす"ボタンクラス
	/// <para>クリックされると、物語提出のためのダイアログを表示する。</para>
	/// </summary>
	public class SubmitButton : Common.Button
	{
		/// <summary>
		/// 親にあたる汎用作曲画面。
		/// </summary>
		private readonly MakeStoryScreen __parent;

		/// <summary>
		/// 親にあたる汎用作曲画面を取得する。
		/// </summary>
		public MakeStoryScreen Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// 物語作成画面の"せんせいにだす"ボタンの可視性を示す値を取得または設定する。
		/// ただし、構成設定で muphic が授業 (児童) モードでの動作となっていない限り常に false となる。
		/// </summary>
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = (MainWindow.MuphicOperationMode == MuphicOperationMode.StudentMode) && value;
			}
		}

		/// <summary>
		/// 物語作成画面の"せんせいにだす"ボタンの可視性を示す値を取得または設定する。
		/// ただし、ネットワーク機能が無効になっている場合は常に false となる。
		/// </summary>
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = Manager.NetworkManager.CanUseNetwork && value;
			}
		}


		/// <summary>
		/// 物語作成画面の"せんせいにだす"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる汎用作曲画面。</param>
		public SubmitButton(MakeStoryScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_PrintBtn, "IMAGE_BUTTON_BOX2_GREEN", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_PrintBtn, "IMAGE_MAKESTORYSCR_SUBMITBTN");
		}


		/// <summary>
		/// 物語作成画面の"せんせいにだす"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			Tools.DebugTools.ConsolOutputMessage("SubmitButton -Click", "提出確認ダイアログ表示", true);
			this.Parent.ScreenMode = MakeStoryScreenMode.SubmitDialog;
		}
	}
}
 