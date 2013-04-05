
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"あたらしくつくる"ボタンクラス。
	/// <para>クリックされると、現在の物語を破棄し新規作成を行う為の確認ダイアログを表示する。</para>
	/// </summary>
	public class CreateNewButton : Muphic.Common.Button
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
		/// 物語作成画面の"あたらしくつくる"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public CreateNewButton(MakeStoryScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Muphic.Settings.PartsLocation.Default.MakeStoryScr_CreateNewBtn, "IMAGE_BUTTON_BOX2_YELLOW", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Muphic.Settings.PartsLocation.Default.MakeStoryScr_CreateNewBtn, "IMAGE_MAKESTORYSCR_CREATEBTN");
		}


		/// <summary>
		/// 物語作成画面の"あたらしくつくる"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (Manager.KeyboardInputManager.IsProtection) return;

			Tools.DebugTools.ConsolOutputMessage("CreateNew -Click", "新規作成ダイアログ表示", true);
			this.Parent.ScreenMode = MakeStoryScreenMode.CreateNewDialog;
		}
	}
}
