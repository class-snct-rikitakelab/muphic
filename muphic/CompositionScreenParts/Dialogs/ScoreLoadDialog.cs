using Muphic.Common;
using Muphic.Tools.IO;

namespace Muphic.CompositionScreenParts.Dialogs
{
	/// <summary>
	/// 楽譜読込ダイアログクラス。
	/// </summary>
	public class ScoreLoadDialog : Dialog
	{
		/// <summary>
		/// 親にあたる汎用作曲画面。
		/// <para>Parent プロパティを使用すること。</para>
		/// </summary>
		private readonly CompositionScreen __parent;

		/// <summary>
		/// 親にあたる汎用作曲画面を取得する。
		/// </summary>
		public CompositionScreen Parent { get { return this.__parent; } }


		/// <summary>
		/// 楽譜読込ダイアログの結果を示す識別子を取得または設定する。
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
					case DialogResult.OK:		// 選択ボタンが押された場合、結果番号に対応するファイル名から楽譜データ読み込み
						this.Parent.SetScoreData(XmlFileReader.ReadScoreData(this.FileNameList[this.SelectArea.Result], true));
						this.Parent.ScreenMode = CompositionScreenMode.CompositionScreen;
						break;

					case DialogResult.Cancel:
						this.Parent.ScreenMode = CompositionScreenMode.CompositionScreen;
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}


		/// <summary>
		/// 楽譜読込ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる汎用作曲画面。</param>
		public ScoreLoadDialog(CompositionScreen parent)
			: base("LoadDialog_CompositionScreen", DialogButtons.FileSelect, DialogIcons.Caution, "IMAGE_COMPOSITIONSCR_DIAGTITLE_LOAD", "IMAGE_COMPOSITIONSCR_DIAGMSG_LOAD")
		{
			this.__parent = parent;

			this.DialogResult = DialogResult.None;
		}
	}
}
