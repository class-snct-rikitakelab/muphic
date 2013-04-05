using Muphic.Common;

namespace Muphic.ScoreScreenParts.Dialogs
{
	/// <summary>
	/// 楽譜を印刷する際の確認ダイアログ。
	/// </summary>
	public class PrintDialog : Dialog
	{
		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		public readonly ScoreScreen __parent;


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
					case DialogResult.OK:										// はいボタンが押された場合
						this.Parent.ScoreMain.Print();							// 印刷を開始し
						this.Parent.ScreenMode = ScoreScreenMode.ScoreScreen;	// 制御権を楽譜画面に戻す
						break;

					case DialogResult.Cancel:									// いいえボタンが押された場合
						this.Parent.ScreenMode = ScoreScreenMode.ScoreScreen;	// 何もせずに制御権を楽譜画面に戻す
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}


		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		public ScoreScreen Parent
		{
			get { return this.__parent; }
		}


		/// <summary>
		/// 楽譜を印刷する際の確認ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public PrintDialog(ScoreScreen parent)
			: base("PrintDialog_ScoreScreen", DialogButtons.YesNo, DialogIcons.Caution, "IMAGE_SCORESCR_PRINTDIAG_TITLE", "IMAGE_SCORESCR_PRINTDIAG_MSG")
		{
			this.__parent = parent;
			this.DialogResult = DialogResult.None;
		}
	}
}
