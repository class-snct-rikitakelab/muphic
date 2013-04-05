using Muphic.Common;
using Muphic.Tools.IO;

namespace Muphic.MakeStoryScreenParts.Dialogs
{
	/// <summary>
	/// 物語作成画面の物語読込ダイアログクラス。
	/// </summary>
	public  class StoryLoadDialog : Dialog
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
		/// 物語読込ダイアログの結果を示す識別子を取得または設定する。
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
					case DialogResult.OK:		// 選択ボタンが押された場合、結果番号に対応するファイル名から物語データ読み込み
						this.Parent.SetStoryData(XmlFileReader.ReadStoryData(this.FileNameList[this.SelectArea.Result]));
						this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
						break;

					case DialogResult.Cancel:
						this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}


		/// <summary>
		/// 物語作成画面の物語読込ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public StoryLoadDialog(MakeStoryScreen parent)
			: base("LoadDialog_MakeStoryScreen", DialogButtons.FileSelect, DialogIcons.Caution, "IMAGE_MAKESTORYSCR_DIAGTITLE_LOAD", "IMAGE_MAKESTORYSCR_DIAGMSG_LOAD")
		{
			this.__parent = parent;

			this.DialogResult = DialogResult.None;
		}

	}
}
