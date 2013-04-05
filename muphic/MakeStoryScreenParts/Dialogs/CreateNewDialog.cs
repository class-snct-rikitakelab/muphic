using Muphic.Common;

namespace Muphic.MakeStoryScreenParts.Dialogs
{
	/// <summary>
	/// 物語作成画面の新規作成確認ダイアログクラス。
	/// </summary>
	public class CreateNewDialog : Dialog
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// <para>Parent プロパティを使用すること。</para>
		/// </summary>
		private readonly MakeStoryScreen __parent;

		/// <summary>
		/// 親にあたる物語作成画面を取得する。
		/// </summary>
		public MakeStoryScreen Parent
		{
			get { return this.__parent; }
		}


		/// <summary>
		/// 新規作成ダイアログの結果を示す識別子を取得または設定する。
		/// </summary>
		public override DialogResult DialogResult
		{
			get
			{
				return base.DialogResult;
			}
			set
			{
				base.DialogResult = value;

				switch (value)
				{
					case DialogResult.OK:												// はいボタンが押された場合
						this.Parent.ClearStoryData();									// 空の物語データをセットし
						this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;	// 制御権を物語作成画面に戻す
						break;

					case DialogResult.Cancel:											// いいえボタンが押された場合
						this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;	// 何もせずに制御権を物語作成画面に戻す
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}


		/// <summary>
		/// 物語作成画面の新規作成確認ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public CreateNewDialog(MakeStoryScreen parent)
			: base("CreateNewDialog_MakeStoryScreen", DialogButtons.YesNo, DialogIcons.Caution, "IMAGE_MAKESTORYSCR_DIAGTITLE_CREATE", "IMAGE_MAKESTORYSCR_DIAGMSG_CREATE")
		{
			this.__parent = parent;

			this.DialogResult = DialogResult.None;
		}
	}
}
