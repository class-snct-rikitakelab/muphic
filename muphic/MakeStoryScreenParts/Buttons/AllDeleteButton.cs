
namespace Muphic.MakeStoryScreenParts.Buttons
{
	/// <summary>
	/// 物語作成画面の"ぜんぶもどす"ボタンクラス。
	/// <para>クリックされると、全てのスタンプを削除する為の確認ダイアログを表示する。</para>
	/// </summary>
	public class AllDeleteButton : Common.Button
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
		/// 物語作成画面の"ぜんぶもどす"ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public AllDeleteButton(MakeStoryScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_AllDeleteBtn, "IMAGE_BUTTON_BOX2_YELLOW", "IMAGE_BUTTON_BOX2_ON");
			this.SetLabelTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_AllDeleteBtn, "IMAGE_MAKESTORYSCR_ALLDELETEBTN");
		}


		/// <summary>
		/// 物語作成画面の"ぜんぶもどす"ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			Tools.DebugTools.ConsolOutputMessage("AllDeleteButton -Click", "スタンプ全削除確認ダイアログ表示", true);
			this.Parent.ScreenMode = MakeStoryScreenMode.AllDeleteDialog;
		}

	}
}
