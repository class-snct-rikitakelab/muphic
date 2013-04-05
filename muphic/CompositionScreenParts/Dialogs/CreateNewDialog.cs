using Muphic.Common;

namespace Muphic.CompositionScreenParts.Dialogs
{
	/// <summary>
	/// 曲の新規作成ダイアログ
	/// </summary>
	public class CreateNewDialog : Dialog
	{
		/// <summary>
		/// 親にあたる汎用作曲画面。
		/// </summary>
		public CompositionScreen Parent { get; private set; }


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
					case DialogResult.OK:													// はいボタンが押された場合
						this.Parent.ClearScoreData();										// 各要素の初期化を行い
						this.Parent.ScreenMode = CompositionScreenMode.CompositionScreen;	// 制御権を作曲画面に戻す
						break;

					case DialogResult.Cancel:												// いいえボタンが押された場合
						this.Parent.ScreenMode = CompositionScreenMode.CompositionScreen;	// 何もせずに制御権を作曲画面に戻す
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}


		/// <summary>
		/// 曲の新規作成ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる汎用作曲画面。</param>
		public CreateNewDialog(CompositionScreen parent)
			: base("CreateNewDialog_CompositionScreen", DialogButtons.YesNo, DialogIcons.Caution, "IMAGE_COMPOSITIONSCR_DIAGTITLE_CREATE", "IMAGE_COMPOSITIONSCR_DIAGMSG_CREATE")
		{
			this.Parent = parent;

			this.DialogResult = DialogResult.None;
		}
	}
}
