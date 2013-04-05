using Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts;
using Muphic.Manager;

namespace Muphic.MakeStoryScreenParts.SubScreens
{
	/// <summary>
	/// 物語名入力画面
	/// </summary>
	public class StoryEntitleScreen : EntitleScreen
	{
		/// <summary>
		/// 親にあたる物語作成画面
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		/// <summary>
		/// "けってい"ボタン
		/// </summary>
		public TitleDecisionButton TitleDecisionButton { get; private set; }

		/// <summary>
		/// DrawManager への登録番号。クラス解放時に DrawManager に登録したキーとテクスチャを一括削除するため。
		/// </summary>
		private int RegistNum { get; set; }

		
		/// <summary>
		/// 物語名入力画面の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		/// <param name="defaultTitle">初期状態で表示する題名。</param>
		public StoryEntitleScreen(MakeStoryScreen makeStoryScreen, string defaultTitle)
			: base(Settings.System.Default.StoryMake_MaxTitleLength, defaultTitle, Muphic.Properties.Resources.EntitleExplain_StoryTitle)
		{
			this.Parent = makeStoryScreen;		// 親にあたる物語作成画面を設定
			this.Initialization();				// パーツのインスタンス化等を行う
		}


		/// <summary>
		/// 物語名入力画面を構成する各パーツのインスタンス化等を行う。
		/// </summary>
		protected new void Initialization()
		{
			// 登録開始を管理クラスへ通知
			this.RegistNum = DrawManager.BeginRegist(false);

			#region 部品のインスタンス化

			this.TitleDecisionButton = new TitleDecisionButton(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.TitleDecisionButton);

			#endregion

			// 登録終了を管理クラスへ通知
			DrawManager.EndRegist();
		}


		/// <summary>
		/// 題名入力画面で使用したリソースを解放し、登録したキーとテクスチャ名を削除する。
		/// </summary>
		public new void Dispose()
		{
			base.Dispose();

			Manager.DrawManager.Delete(this.RegistNum);
		}
	}
}
