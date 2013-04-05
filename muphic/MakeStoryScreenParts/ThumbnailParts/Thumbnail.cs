using System.Drawing;

using Muphic.MakeStoryScreenParts;
using Muphic.MakeStoryScreenParts.MakeStoryMainParts;
using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.MakeStoryScreenParts.ThumbnailParts
{
	/// <summary>
	/// 物語作成画面のサムネイルクラス。
	/// </summary>
	public class Thumbnail : Common.Screen
	{

		#region フィールドとプロパティ

		/// <summary>
		/// 親にあたるサムネイル管理クラス。
		/// </summary>
		public ThumbnailArea Parent { get; private set; }


		/// <summary>
		/// サムネイルの番号を保持する。
		/// <para>ThumbnailNumber プロパティを使用すること。</para>
		/// </summary>
		private readonly int __number;

		/// <summary>
		/// このサムネイルの左から数えた番号を取得する。
		/// </summary>
		public int Number
		{
			get
			{
				return this.__number;
			}
		}

		/// <summary>
		/// このサムネイルが描画を担当するページを取得または設定する。
		/// </summary>
		public int Page { get; private set; }

		/// <summary>
		/// このサムネイルが編集中のページであるかどうかを取得または設定する。
		/// </summary>
		public bool IsCurrentPage { get; private set; }

		/// <summary>
		/// このサムネイルが担当するスライドを取得する。
		/// </summary>
		public Slide Slide
		{
			get
			{
				return this.Parent.Parent.CurrentStoryData.SlideList[this.Page];
			}
		}

		/// <summary>
		/// このサムネイルが編集可能かどうかを取得する。
		/// </summary>
		public bool IsEnabledEdit
		{
			get
			{
				return !(this.Parent.MaxEditedPage + 1 < this.Page);
			}
		}


		#region 描画座標のフィールドとプロパティ

		/// <summary>
		/// このサムネイルの位置（読み取り専用）。
		/// <para>Location プロパティを使用すること。</para>
		/// </summary>
		private readonly Point __location;

		/// <summary>
		/// このサムネイルの位置を取得する。
		/// </summary>
		public Point Location
		{
			get
			{
				return this.__location;
			}
		}


		/// <summary>
		/// スライドの描画基点位置（読み取り専用）。
		/// <para>SlideBaseLocation プロパティを使用すること。</para>
		/// </summary>
		private readonly Point __slideBaseLocation;

		/// <summary>
		/// このサムネイルのスライドの描画基点位置を取得する。
		/// </summary>
		public Point SlideBaseLocation
		{
			get
			{
				return this.__slideBaseLocation;
			}
		}


		/// <summary>
		/// このサムネイルの番号を描画する位置（読み取り専用）。
		/// <para>PagenumberLocation プロパティを使用すること。</para>
		/// </summary>
		private readonly Point __pagenumberLocation;

		/// <summary>
		/// このサムネイルの番号を描画する位置を取得する。
		/// </summary>
		public Point PagenumberLocation
		{
			get
			{
				return this.__pagenumberLocation;
			}
		}


		/// <summary>
		/// このサムネイルが担当するスライドに曲が付いていることを示すアイコンを表示する位置（読み取り専用）。
		/// <para>ComposedIconLocation プロパティを使用すること。</para>
		/// </summary>
		private readonly Point __composedIconLocation;

		/// <summary>
		/// このサムネイルが担当するスライドに曲が付いていることを示すアイコンを表示する位置を取得する。
		/// </summary>
		public Point ComposedIconLocation
		{
			get
			{
				return this.__composedIconLocation;
			}
		}

		#endregion


		/// <summary>
		/// サムネイル上でスライドを描画する際の縮小率。
		/// </summary>
		private const float scalingRate = 0.127F;

		#endregion


		/// <summary>
		/// サムネイルクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="thumbnailArea">親にあたるサムネイル管理クラス。</param>
		/// <param name="number">サムネイルの左から数えた番号。</param>
		public Thumbnail(ThumbnailArea thumbnailArea, int number)
		{
			this.Parent = thumbnailArea;
			this.__number = number;

			// このサムネイルの左から数えた番号に応じた描画位置を取得
			this.__location = (Point)Settings.PartsLocation.Default["MakeStoryScr_Thumb" + number.ToString()];

			// スライドの描画基点位置を、サムネイルの位置から算出し設定
			this.__slideBaseLocation = new Point(this.Location.X + 3, this.Location.Y + 3);

			// サムネイルの横幅中央までの距離を算出し、ページ番号描画位置を設定
			int xOffset = TextureFileManager.GetRectangle("IMAGE_MAKESTORYSCR_THUMBNAIL").Width / 2 - 1;
			this.__pagenumberLocation = new Point(this.Location.X + xOffset, this.Location.Y - 9);

			// 作曲済みを示すアイコンの描画位置を、サムネイルの位置から算出し決定
			this.__composedIconLocation = new Point(this.Location.X + xOffset + 28, this.Location.Y - 9);

			DrawManager.Regist(this.ToString(), this.Location, "IMAGE_MAKESTORYSCR_THUMBNAIL");

			// 数値リテラル直接指定なのは許してくださいごめんなさいごめんなさいごめんなさい
		}


		/// <summary>
		/// サムネイルの描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			DrawManager.Draw(this.ToString(), this.IsEnabledEdit ? (byte)255 : (byte)100);

			// 担当するスライドの描画を行う。
			PictureWindow.DrawSlide(this.Slide, this.SlideBaseLocation, Thumbnail.scalingRate);

			// 編集中のスライドであれば、
			if (this.IsCurrentPage)
			{
				DrawManager.Draw("IMAGE_MAKESTORYSCR_THUMBNAIL_FRAME", this.Location);
			}

			// 担当するスライドに曲が付いていれば、作曲済みであることを示すアイコンを指定位置に描画する
			if (this.Slide.IsComposed) DrawManager.DrawCenter("IMAGE_MAKESTORYSCR_THUMBNAIL_COMPOSED", this.ComposedIconLocation);

			// 担当するスライドのページ番号を描画  編集不可であればやや透過させる
			StringManager.SystemDrawCenter(this.Page + 1, this.PagenumberLocation, this.IsEnabledEdit ? (byte)255 : (byte)100);
		}


		/// <summary>
		/// 編集中のページ番号から、このサムネイルが描画するページ番号を算出する。
		/// </summary>
		/// <returns>このサムネイルが描画するページ番号。</returns>
		public void UpdatePage()
		{
			if (this.Parent.CurrentPage < this.Parent.ThumbnailNum / 2)
			{
				// 編集中のページが 0 もしくは 1 ページ目だった場合
				this.Page = this.Number;
			}
			else if (this.Parent.CurrentPage >= this.Parent.MaxPage - this.Parent.ThumbnailNum / 2)
			{
				// 編集中のページが 最後のページ-1 もしくは 最後のページ だった場合
				this.Page = this.Parent.MaxPage - this.Parent.ThumbnailNum + this.Number;
			}
			else
			{
				// 編集中のページが上記以外（2 ～ 7 ページ目）だった場合
				this.Page = this.Parent.CurrentPage - this.Parent.ThumbnailNum / 2 + this.Number;
			}

			// このサムネイルが担当するページが、編集中のページかどうかを判定する
			this.IsCurrentPage = (this.Page == this.Parent.CurrentPage);
		}


		/// <summary>
		/// サムネイルがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			if (!this.IsEnabledEdit || this.IsCurrentPage) return;

			// 編集ページは、このサムネイルの担当ページになる
			this.Parent.CurrentPage = this.Page;
		}


		/// <summary>
		/// 番号を含むこのサムネイルの System.String を返す。
		/// </summary>
		/// <returns>。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Number.ToString();
		}

	}
}
