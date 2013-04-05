using System.Drawing;

namespace Muphic.MakeStoryScreenParts
{
	/// <summary>
	/// 物語の題名の表示を行うクラス。
	/// </summary>
	public class StoryTitle : Common.Screen
	{
		/// <summary>
		/// 親にあたる物語作成画面クラス。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		/// <summary>
		/// 物語の題名を表す文字列を取得する。
		/// </summary>
		public string Title
		{
			get
			{
				return this.Parent.CurrentStoryData.Title;
			}
		}


		/// <summary>
		/// 物語の題名表示クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public StoryTitle(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			Manager.DrawManager.Regist(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_Title, "IMAGE_MAKESTORYSCR_TITLEAREA");
		}


		/// <summary>
		/// 物語の題名の文字列及びその背景テクスチャの描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			Manager.StringManager.Draw(this.Title, Settings.PartsLocation.Default.MakeStoryScr_Title);
		}
	}
}
