namespace Muphic.CompositionScreenParts.CompositionMainParts.Buttons
{
	/// <summary>
	/// 作曲画面の楽譜右スクロールボタンクラス
	/// </summary>
	public class RightScrollButton : Muphic.Common.Button
	{
		/// <summary>
		/// 親にあたる作曲部。
		/// </summary>
		public CompositionMain Parent { get; set; }


		/// <summary>
		/// 右スクロールボタンのインスタンス化を行う。
		/// </summary>
		/// <param name="compositionMain">親にあたる作曲部。</param>
		public RightScrollButton(CompositionMain compositionMain)
		{
			this.Parent = compositionMain;

			this.SetBgTexture(this.ToString(), Locations.ScrollRightButton, "IMAGE_BUTTON_ARROW2_R_GREEN", "IMAGE_BUTTON_ARROW2_R_ON");
			this.LabelName = "";
		}


		/// <summary>
		/// 右スクロールボタンがクリックされた場合の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.RightScroll();
		}
	}
}
