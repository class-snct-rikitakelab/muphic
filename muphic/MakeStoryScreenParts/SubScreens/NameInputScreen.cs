using Muphic.MakeStoryScreenParts.Dialogs;
using Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts;

namespace Muphic.MakeStoryScreenParts.SubScreens
{
	/// <summary>
	/// 物語の制作者を名入力する画面。
	/// </summary>
	public class NameInputScreen_ : EntitleScreen
	{
		/// <summary>
		/// 親にあたる名前入力ダイアログ。
		/// </summary>
		public NameInputDialog Parent { get; private set; }

		/// <summary>
		/// 制作者名を決定する "けってい" ボタン。
		/// </summary>
		public NameDecisionButton NameDecisionButton { get; private set; }


		/// <summary>
		/// DrawManager への登録番号。クラス解放時に DrawManager に登録したキーとテクスチャを一括削除するため。
		/// </summary>
		private int RegistNum { get; set; }


		/// <summary>
		/// 物語制作者名入力画面の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる名前入力ダイアログ。</param>
		/// <param name="defaultTitle">初期状態で表示する名前。</param>
		public NameInputScreen_(NameInputDialog parent, string defaultTitle)
			: base(14, defaultTitle, Muphic.Properties.Resources.EntitleExplain_PlayerName)
		{
			this.Parent = parent;
			this.Initialization();
		}


		/// <summary>
		///物語制作者名入力画面を構成する各パーツのインスタンス化等を行う。
		/// </summary>
		protected new void Initialization()
		{
			// 登録開始を管理クラスへ通知
			this.RegistNum = Manager.DrawManager.BeginRegist(false);

			#region 部品のインスタンス化

			this.NameDecisionButton = new NameDecisionButton(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.NameDecisionButton);

			#endregion

			// 登録終了を管理クラスへ通知
			Manager.DrawManager.EndRegist();
		}


		/// <summary>
		/// 物語制作者名入力画面で使用したリソースを解放し、登録したキーとテクスチャ名を削除する。
		/// </summary>
		public new void Dispose()
		{
			base.Dispose();

			Manager.DrawManager.Delete(this.RegistNum);
		}

	}
}
