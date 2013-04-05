using Muphic.Common;
using Muphic.Tools.IO;

namespace Muphic.MakeStoryScreenParts.Dialogs
{
	/// <summary>
	/// 物語作成画面の物語保存ダイアログクラス。
	/// </summary>
	public class StorySaveDialog : Dialog
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
		/// 保存可能かどうかを表わす bool 値。
		/// EnabledSave プロパティを使用すること。
		/// </summary>
		private bool __enabledSave = false;

		/// <summary>
		/// 保存可能かどうかを示す値を取得または設定する。
		/// このプロパティ値が true の間のみ保存可能となる (タイトル未設定時等保存できない状態の場合は false になる)。
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
				this.SetMessage(value ? "IMAGE_MAKESTORYSCR_DIAGMSG_SAVE" : "IMAGE_MAKESTORYSCR_DIAGMSG_NOTSAVE");
			}
		}

		/// <summary>
		/// 物語を保存すると同時にものがたりおんがくを終了するかどうかを取得または設定する。
		/// </summary>
		public bool EnabledBackToTopScreen { get; set; }


		/// <summary>
		/// 物語保存ダイアログの結果を示す識別子を取得または設定する。
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
						// "はい"ボタンが押された場合
						if (this.EnabledSave)					
						{
							// 保存可能状態であれば（はい/いいえ のうち はい が選択された場合）、ファイルに書き込み
							bool result = XmlFileWriter.WriteStoryData(this.Parent.CurrentStoryData);

							if (this.EnabledBackToTopScreen && result)
							{
								// 更に、保存と同時にものがたりおんがくを終了する設定の場合、制御権はトップ画面に戻す
								this.Parent.Parent.ScreenMode = TopScreenMode.TopScreen;
							}
							else
							{
								// 上記設定でなければ、制御権は物語作成画面に戻す
								this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
							}
						}
						else
						{
							// 保存可能状態でなければ（タイトル未設定の場合）、制御権を物語作成画面に戻す
							this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
						}
						break;

					case DialogResult.Cancel:
						// "いいえ"ボタンが押された場合
						this.EnabledBackToTopScreen = false;							// 保存と同時にものがたりおんがくモードを終了する状態を解除し
						this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;	// 制御権をものがたりおんがくモードに戻す
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}


		/// <summary>
		/// 物語作成画面の物語保存ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public StorySaveDialog(MakeStoryScreen parent)
			: base("SaveDialog_MakeStoryScreen", DialogButtons.None, DialogIcons.Caution, "IMAGE_MAKESTORYSCR_DIAGTITLE_SAVE", "IMAGE_MAKESTORYSCR_DIAGMSG_SAVE")
		{
			this.__parent = parent;

			this.DialogResult = DialogResult.None;

			this.EnabledSave = false;
			this.EnabledBackToTopScreen = false;
		}

	}
}
