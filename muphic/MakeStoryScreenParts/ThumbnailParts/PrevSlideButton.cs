
namespace Muphic.MakeStoryScreenParts.ThumbnailParts
{
	/// <summary>
	/// 物語作成画面のサムネイル領域のページ戻しボタン。
	/// <para>クリックされると、編集ページを１つ前のスライドへ移動する。</para>
	/// </summary>
	public class PrevSlideButton : Common.Button
	{
		/// <summary>
		/// 親にあたるサムネイル管理クラス。
		/// </summary>
		public ThumbnailArea Parent { get; private set; }


		/// <summary>
		/// ページ戻しボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="thumbnailArea">。</param>
		public PrevSlideButton(ThumbnailArea thumbnailArea)
		{
			this.Parent = thumbnailArea;

			this.SetBgTexture(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr_ThumbLeft, "IMAGE_BUTTON_ARROW1_L_YELLOW", "IMAGE_BUTTON_ARROW1_L_ON");
		}


		/// <summary>
		/// ページ戻しボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.CurrentPage--;
		}
	}
}
