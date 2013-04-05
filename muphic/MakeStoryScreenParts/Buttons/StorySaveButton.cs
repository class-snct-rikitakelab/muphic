
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"のこす"ボタンクラス
	/// <para>クリックされると、物語保存のためのダイアログを表示する。</para>
	/// </summary>
	public class StorySaveButton : Common.Button
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
		/// 物語作成画面の"のこす"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる汎用作曲画面。</param>
		public StorySaveButton(MakeStoryScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_SaveBtn, "IMAGE_BUTTON_BOX2_GREEN", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_SaveBtn, "IMAGE_MAKESTORYSCR_SAVEBTN");
		}


		/// <summary>
		/// 物語作成画面の"のこす"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			Tools.DebugTools.ConsolOutputMessage("StorySaveButton -Click", "保存ダイアログ表示", true);
			this.Parent.StorySaveDialog.EnabledBackToTopScreen = false;
			this.Parent.ScreenMode = MakeStoryScreenMode.StorySaveDialog;
		}
	}
}
