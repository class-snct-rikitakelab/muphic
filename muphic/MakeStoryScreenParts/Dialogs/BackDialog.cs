using Muphic.Common;

namespace Muphic.MakeStoryScreenParts.Dialogs
{
	/// <summary>
	/// 物語作成画面のものがたりおんがくモード終了確認ダイアログクラス。
	/// </summary>
	public class BackDialog : Dialog
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
		/// ものがたりおんがくモード終了確認ダイアログの結果を示す識別子を取得または設定する。
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
						this.Parent.Parent.ScreenMode = TopScreenMode.TopScreen;		// 制御権をトップ画面に戻す
						break;

					case DialogResult.Cancel:											// いいえボタンが押された場合
						this.Parent.StorySaveDialog.EnabledBackToTopScreen = true;		// 保存と同時にものがたりおんがくモードを終了する状態に設定し
						this.Parent.ScreenMode = MakeStoryScreenMode.StorySaveDialog;	// 保存ダイアログ表示
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}


		/// <summary>
		/// ものがたりおんがくモード終了確認ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public BackDialog(MakeStoryScreen parent)
			: base("BackDialog_MakeStoryScreen", DialogButtons.YesNo, DialogIcons.Caution, "IMAGE_MAKESTORYSCR_DIAGTITLE_BACK", "IMAGE_MAKESTORYSCR_DIAGMSG_BACK")
		{
			this.__parent = parent;

			this.DialogResult = DialogResult.None;
		}
	}
}
