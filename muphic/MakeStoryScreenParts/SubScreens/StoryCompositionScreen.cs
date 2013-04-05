using Muphic.CompositionScreenParts;
using Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts;
using Muphic.Manager;

namespace Muphic.MakeStoryScreenParts.SubScreens
{
	/// <summary>
	/// ものがたりおんがくモード作曲画面クラス
	/// </summary>
	public class StoryCompositionScreen : CompositionScreen
	{
		/// <summary>
		/// 親にあたる物語作成画面
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		/// <summary>
		/// "もどる"ボタン
		/// </summary>
		public BackFromCompositionButton BackButton { get; private set; }

		/// <summary>
		/// サムネイル
		/// </summary>
		public StoryCompositionThumbnail StoryCompositionThumbnail { get; private set; }

		/// <summary>
		/// 音階制限選択領域。
		/// </summary>
		public LimitSelectArea LimitSelectArea { get; private set; }

		/// <summary>
		/// DrawManager への登録番号。クラス解放時に DrawManager に登録したキーとテクスチャを一括削除するため。
		/// </summary>
		private int RegistNum { get; set; }


		/// <summary>
		/// ものがたりおんがくモード作曲画面クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public StoryCompositionScreen(MakeStoryScreen parent)
			: base(parent.CurrentSlide.ScoreData, CompositionMode.StoryScreen)
		{
			this.Parent = parent;						// 親となる物語作成画面を設定

			this.Initialization();						// パーツのインスタンス化等

			this.TitleButton.Visible = false;			// 汎用作曲画面を構成するボタンのうち、
			this.ScoreSaveButton.Visible = false;		// ものがたりおんがくモード作曲画面では
			this.ScoreLoadButton.Visible = false;		// 使用しないものを全て非表示にする
		}


		/// <summary>
		/// ものがたりおんがくモード作曲画面を構成する各パーツのインスタンス化等を行う。
		/// </summary>
		public new void Initialization()
		{
			// 登録開始を管理クラスへ通知
			this.RegistNum = DrawManager.BeginRegist(false);

			#region 部品のインスタンス化

			this.BackButton = new BackFromCompositionButton(this);
			this.StoryCompositionThumbnail = new StoryCompositionThumbnail(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.BackButton);
			this.PartsList.Add(this.StoryCompositionThumbnail);

			#endregion

			// 音階制限が有効かを確認し、有効ならインスタンス化して登録
			if (ConfigurationManager.Current.EnabledLimitMode)
			{
				this.LimitSelectArea = new LimitSelectArea(this);
				this.PartsList.Add(this.LimitSelectArea);
			}

			// 登録終了を管理クラスへ通知
			DrawManager.EndRegist();
		}


		/// <summary>
		/// 現在の楽譜データを破棄し、空の楽譜データをセットする。
		/// </summary>
		public override void ClearScoreData()
		{
			base.ClearScoreData();
			this.Parent.CurrentSlide.ScoreData = this.CurrentScoreData;
		}


		/// <summary>
		/// ものがたりおんがくモード作曲画面で使用したリソースを解放し、登録したキーとテクスチャ名を削除する。
		/// </summary>
		public new void Dispose()
		{
			// 汎用作曲画面のリソースを解放
			base.Dispose();

			// 登録されていた使用テクスチャ名を削除
			DrawManager.Delete(this.RegistNum);
		}
	}
}
