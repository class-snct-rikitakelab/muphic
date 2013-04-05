using Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts;
using Muphic.Manager;

namespace Muphic.MakeStoryScreenParts.SubScreens
{
	/// <summary>
	/// 物語作成画面で作成した物語を再生する物語再生画面クラス (Muphic.PlayStoryScreen を継承している)。
	/// </summary>
	public class PlayStoryScreen : Muphic.PlayStoryScreen, System.IDisposable
	{
		/// <summary>
		/// 物語再生画面の親となる物語作成画面。
		/// </summary>
		private readonly MakeStoryScreen __parent;

		/// <summary>
		/// 物語再生画面の親となる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// "えをつくる" (物語作成画面へ画面遷移する) ボタン。
		/// </summary>
		public BackFromPlayStoryButton BackButton { get; private set; }


		/// <summary>
		/// DrawManager への登録番号。
		/// <para>クラス解放時に DrawManager に登録したキーとテクスチャを一括削除する際に使用する。</para>
		/// </summary>
		private int RegistNum { get; set; }


		/// <summary>
		/// 物語再生画面クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">この画面の親となる物語再生画面。</param>
		public PlayStoryScreen(MakeStoryScreen parent)
			: base(parent.CurrentStoryData)
		{
			this.__parent = parent;
			this.Initialization();
		}

		/// <summary>
		/// 物語再生画面を構成する各パーツのインスタンス化等を行う。
		/// </summary>
		protected new void Initialization()
		{
			// 登録開始を管理クラスへ通知
			this.RegistNum = DrawManager.BeginRegist(false);

			#region 部品のインスタンス化

			this.BackButton = new BackFromPlayStoryButton(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.BackButton);

			#endregion

			// 登録終了を管理クラスへ通知
			DrawManager.EndRegist();
		}


		/// <summary>
		/// 物語再生画面で登録したテクスチャ名を、描画管理クラスから削除する。
		/// </summary>
		public new void Dispose()
		{
			base.Dispose();
			DrawManager.Delete(this.RegistNum);
		}

	}
}
