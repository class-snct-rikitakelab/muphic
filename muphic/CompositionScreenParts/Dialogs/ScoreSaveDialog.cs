using Muphic.Common;
using Muphic.PlayerWorks;
using Muphic.Tools.IO;

namespace Muphic.CompositionScreenParts.Dialogs
{
	/// <summary>
	/// 楽譜保存ダイアログクラス。
	/// </summary>
	public class ScoreSaveDialog : Dialog
	{
		/// <summary>
		/// 親にあたる汎用作曲画面。
		/// </summary>
		private readonly CompositionScreen __parent;

		/// <summary>
		/// 親にあたる汎用作曲画面を取得する。
		/// </summary>
		public CompositionScreen Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// 保存可能かどうかを表わす bool 値。
		/// プロパティ "EnabledSave" を使用すること。
		/// </summary>
		private bool __enabledSave = false;

		/// <summary>
		/// 保存可能かどうかを表わす bool 値。
		/// このプロパティ値が true の間のみ保存可能となる（タイトル未設定時等保存できない状態の場合は false になる）。
		/// </summary>
		public bool EnabledSave
		{
			get
			{
				return this.__enabledSave;
			}
			set
			{
				this.__enabledSave = value;
				this.SetButtons(value ? DialogButtons.YesNo : DialogButtons.Yes);
				this.SetMessage(value ? "IMAGE_COMPOSITIONSCR_DIAGMSG_SAVE" : "IMAGE_COMPOSITIONSCR_DIAGMSG_NOTSAVE");
			}
		}


		/// <summary>
		/// 楽譜保存ダイアログの結果を示す識別子。
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
					case DialogResult.OK:
						if (this.EnabledSave)
						{
							XmlFileWriter.WriteScoreData(new ScoreData(this.Parent.ScoreTitle.Text, this.Parent.CompositionMain.AnimalScore.AnimalList, this.Parent.Tempo.TempoMode));
						}

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
		/// 楽譜保存ダイアログの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる汎用作曲画面。</param>
		public ScoreSaveDialog(CompositionScreen compositionScreen)
			: base("SaveDialog_CompositionScreen", DialogButtons.None, DialogIcons.Caution, "IMAGE_COMPOSITIONSCR_DIAGTITLE_SAVE", "IMAGE_COMPOSITIONSCR_DIAGMSG_SAVE")
		{
			this.__parent = compositionScreen;

			this.DialogResult = DialogResult.None;
			this.EnabledSave = false;
		}

	}
}
