
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"よびだす"ボタンクラス。
	/// <para>クリックされると、物語読込の為のダイアログを表示する。</para>
	/// </summary>
	public class StoryLoadButton : Common.Button
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
		/// 物語作成画面の"よびだす"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる汎用作曲画面。</param>
		public StoryLoadButton(MakeStoryScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_LoadBtn, "IMAGE_BUTTON_BOX2_GREEN", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_LoadBtn, "IMAGE_MAKESTORYSCR_LOADBTN");
		}


		/// <summary>
		/// 物語作成画面の"よびだす"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (Manager.KeyboardInputManager.IsProtection) return;

			if (MainWindow.MuphicOperationMode == MuphicOperationMode.TeacherMode)
			{
				Tools.DebugTools.ConsolOutputMessage("StoryLoadButton -Click", "提出作品管理画面表示", true);
				this.Parent.ScreenMode = MakeStoryScreenMode.StoryExplorer;
			}
			else
			{
				Tools.DebugTools.ConsolOutputMessage("StoryLoadButton -Click", "読込ダイアログ表示", true);
				this.Parent.ScreenMode = MakeStoryScreenMode.StoryLoadDialog;
			}
		}
	}
}
