using System.Drawing;
using Muphic.Manager;

namespace Muphic.MakeStoryScreenParts.ThumbnailParts
{
	/// <summary>
	/// 物語作成画面のサムネイル管理クラス。
	/// </summary>
	public class ThumbnailArea : Common.Screen
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// </summary>
		public MakeStoryScreen Parent { get; private set; }

		/// <summary>
		/// 前のスライドを表示するボタン。
		/// </summary>
		public PrevSlideButton PrevSlideButton { get; private set; }

		/// <summary>
		/// 次のスライドを表示するボタン。
		/// </summary>
		public NextSlideButton NextSlideButton { get; private set; }

		/// <summary>
		/// サムネイル
		/// </summary>
		public Thumbnail[] ThumbnailList { get; private set; }


		/// <summary>
		/// 表示するサムネイルの個数。
		/// <para>ThumbnailNum プロパティを使用すること。</para>
		/// </summary>
		private readonly int __thumbnailNum = 5;

		/// <summary>
		/// 表示するサムネイルの個数を取得する。
		/// </summary>
		public int ThumbnailNum
		{
			get
			{
				return this.__thumbnailNum;
			}
		}


		/// <summary>
		/// 現在編集中のページ番号。
		/// <para>CurrentPage プロパティを使用すること。</para>
		/// </summary>
		private int __currentPage;

		/// <summary>
		/// 現在編集中のページ番号を取得または設定する。
		/// </summary>
		public int CurrentPage
		{
			get
			{
				return this.__currentPage;
			}
			set
			{
				if (value <= 0)								// 設定値が 0 以下だった場合
				{
					this.__currentPage = 0;						// ページ番号を 0 に設定
					this.PrevSlideButton.Enabled = false;		// ページ戻しボタンを無効化
					this.NextSlideButton.Enabled = true;		// ページ送りボタンを有効化
				}
				else if (value >= this.MaxPage - 1)		// 設定値が最大値以上だった場合
				{
					this.__currentPage = this.MaxPage - 1;	// ページ番号を最大値に設定
					this.PrevSlideButton.Enabled = true;		// ページ戻しボタンを有効化
					this.NextSlideButton.Enabled = false;		// ページ送りボタンを無効化
				}
				else										// 設定値が上記以外だった場合
				{
					this.__currentPage = value;					// ページ番号を設定
					this.PrevSlideButton.Enabled = true;		// ページ戻しボタンを有効化
					this.NextSlideButton.Enabled = true;		// ページ送りボタンを有効化
				}

				foreach (Thumbnail thumb in this.ThumbnailList)
				{
					thumb.UpdatePage();						// 各サムネイルの担当ページ番号を更新
				}

				Tools.DebugTools.ConsolOutputMessage(
					"ThumbnailArea -CurrentPage",
					string.Format("編集ページ切替 {0}/{1}", this.__currentPage, this.MaxPage - 1),
					true
				);

				// === 以下、編集ページ切り替えに伴うボタン等の設定 ===

				// ページ送りボタンの有効/無効の設定
				this.SetNextButtonEnabled();

				// 両サイドのカテゴリボタンをクリア（カテゴリ無選択状態にする）
				this.Parent.StampSelectArea.CategoryMode = Muphic.MakeStoryScreenParts.Buttons.CategoryMode.None;

			}
		}


		/// <summary>
		/// 物語の最大ページ数を取得する。
		/// </summary>
		public int MaxPage
		{
			get
			{
				return this.Parent.CurrentStoryData.MaxPage;
			}
		}

		/// <summary>
		/// 編集されているページのうち、最後のページ番号を取得する。編集されているページが１つも無い場合、-1 となる。
		/// </summary>
		public int MaxEditedPage
		{
			get
			{
				return this.Parent.CurrentStoryData.MaxEditedPage;
			}
		}


		/// <summary>
		/// サムネイル管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="makeStoryScreen">親にあたる物語作成画面。</param>
		public ThumbnailArea(MakeStoryScreen makeStoryScreen)
		{
			this.Parent = makeStoryScreen;

			// ページ送り/戻しボタンを生成し、部品リストに追加
			this.NextSlideButton = new NextSlideButton(this);
			this.PrevSlideButton = new PrevSlideButton(this);
			this.PartsList.Add(this.NextSlideButton);
			this.PartsList.Add(this.PrevSlideButton);

			// サムネイルを生成し、部品リストに追加
			this.ThumbnailList = new Thumbnail[this.ThumbnailNum];
			for (int i = 0; i < this.ThumbnailList.Length; i++)
			{
				this.ThumbnailList[i] = new Thumbnail(this, i);
				this.PartsList.Add(this.ThumbnailList[i]);
			}

			Manager.DrawManager.Regist(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_ThumbnailArea.Location, "IMAGE_MAKESTORYSCR_THUMBNAILAREA");
		}


		/// <summary>
		/// サムネイル領域の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.DrawParts(drawStatus);
		}


		/// <summary>
		/// ページ送りボタンの有効/無効を設定する。
		/// </summary>
		/// <remarks>
		/// 編集中のスライドが空であれば、次のスライドには進めないようにするため、ページ送りボタンを無効化する。
		/// </remarks>
		public void SetNextButtonEnabled()
		{
			// 最大ページ数に達していたら無効
			if (this.CurrentPage >= this.MaxPage - 1) this.NextSlideButton.Enabled = false;

			// そうでなければ、編集中のスライドとそれ以降が空なら無効、そうでないなら有効
			else this.NextSlideButton.Enabled = !(this.MaxEditedPage < this.CurrentPage);
		}

	}
}
