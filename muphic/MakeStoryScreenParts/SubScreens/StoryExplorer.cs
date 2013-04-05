using Muphic.Common;

namespace Muphic.MakeStoryScreenParts.SubScreens
{
	/// <summary>
	/// 提出された物語作品の管理画面。
	/// </summary>
	public class StoryExplorer : Explorer
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }


		/// <summary>
		/// 管理画面の結果を示す識別子を取得または設定する。
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
						this.Parent.SetStoryData(Muphic.Tools.IO.XmlFileReader.ReadStoryData(this.ResultPath));
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
		/// 提出された物語作品の管理画面の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public StoryExplorer(MakeStoryScreen parent)
			: base(ExplorerTargetType.StoryData, Manager.ConfigurationManager.Current.StorySaveFolder, "MakeStoryScreen", Settings.System.Default.FileExtension_StoryData)
		{
			this.Parent = parent;
		}
	}
}
