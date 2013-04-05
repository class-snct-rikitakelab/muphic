using System.Drawing;

namespace Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts
{
	/// <summary>
	/// ものがたりおんがくモード作曲画面右上のサムネイルクラス。
	/// </summary>
	public class StoryCompositionThumbnail : Common.Screen
	{
		/// <summary>
		/// 親にあたる作曲画面
		/// </summary>
		public StoryCompositionScreen Parent { get; private set; }


		/// <summary>
		/// サムネイルとフレームの描画位置（読み取り専用）。
		/// <para>Locations プロパティを使用すること。</para>
		/// </summary>
		private readonly Point[] __locations;

		/// <summary>
		/// フレームの描画位置を取得する。
		/// </summary>
		public Point[] Locations
		{
			get
			{
				return this.__locations;
			}
		}

		/// <summary>
		/// サムネイルとスライドの描画位置を取得する。
		/// </summary>
		public Point Location
		{
			get
			{
				return this.__locations[0];
			}
		}


		/// <summary>
		/// サムネイル上でスライドを描画する際の縮小率。
		/// </summary>
		private const float scalingRate = 0.365F;


		/// <summary>
		/// サムネイルクラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="storyCompositionScreen">親にあたる作曲画面。</param>
		public StoryCompositionThumbnail(StoryCompositionScreen storyCompositionScreen)
		{
			this.Parent = storyCompositionScreen;
			this.__locations = new Point[4];

			// サムネイルとフレーム（上）の描画位置を設定
			this.__locations[0] = Muphic.CompositionScreenParts.Locations.Thumbnail;

			// フレーム（下）の描画位置を算出
			Size frameTop = Manager.TextureFileManager.GetRectangle("IMAGE_MAKESTORYSCR_PICTURE_FRAME_TOP").Size;
			this.__locations[1] = new Point(
				this.__locations[0].X,
				this.__locations[0].Y + (int)((frameTop.Height + Manager.TextureFileManager.GetRectangle("IMAGE_MAKESTORYSCR_PICTURE_FRAME_LEFT").Height) * scalingRate)
			);

			// フレーム（左）の描画位置を算出
			this.__locations[2] = new Point(
				this.__locations[0].X,
				this.__locations[0].Y + (int)(frameTop.Height * scalingRate)
			);

			// フレーム（右）の描画位置を算出
			this.__locations[3] = new Point(
				this.__locations[0].X + (int)((frameTop.Width - Manager.TextureFileManager.GetRectangle("IMAGE_MAKESTORYSCR_PICTURE_FRAME_RIGHT").Width) * scalingRate),
				this.__locations[2].Y
			);

			Manager.DrawManager.Regist(this.ToString(), this.Location, "IMAGE_MAKESTORYSCR_COMPOSITION_THUMB");
		}


		/// <summary>
		/// サムネイルと編集ページのスライドを縮小描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			// スライドを縮小して描画
			PictureWindow.DrawSlide(this.Parent.Parent.CurrentSlide, this.Location, scalingRate);

			// 枠の描画
			Manager.DrawManager.Draw("IMAGE_MAKESTORYSCR_PICTURE_FRAME_TOP", this.Locations[0], scalingRate);
			Manager.DrawManager.Draw("IMAGE_MAKESTORYSCR_PICTURE_FRAME_BTM", this.Locations[1], scalingRate);
			Manager.DrawManager.Draw("IMAGE_MAKESTORYSCR_PICTURE_FRAME_LEFT", this.Locations[2], scalingRate);
			Manager.DrawManager.Draw("IMAGE_MAKESTORYSCR_PICTURE_FRAME_RIGHT", this.Locations[3], scalingRate);
		}
	}
}
