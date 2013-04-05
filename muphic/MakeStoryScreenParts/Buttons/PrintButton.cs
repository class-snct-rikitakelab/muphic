
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"いんさつ"ボタンクラス。
	/// <para>クリックされると、印刷の為の確認ダイアログを表示する。</para>
	/// </summary>
	public class PrintButton : Common.Button
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }


		/// <summary>
		/// 印刷ボタンの可視性を示す値を取得または設定する。
		/// ただし、構成設定で muphic が授業 (児童) モードでの動作となっている場合は常に false となる。
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
		/// ボタンの有効性を示す値を取得または設定する。
		/// システムの設定で印刷機能が無効化されている場合は常に false となる。
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
		/// 物語作成画面の"いんさつ"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public PrintButton(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_PrintBtn, "IMAGE_BUTTON_BOX2_GREEN", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_PrintBtn, "IMAGE_MAKESTORYSCR_PRINTBTN");
		}


		/// <summary>
		/// 物語作成画面の"いんさつ"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (MainWindow.MuphicOperationMode == MuphicOperationMode.TeacherMode)
			{
				new Tools.Printer.StoryPrinter(this.Parent.CurrentStoryData).PrintForStudent();
			}
		}

	}
}
