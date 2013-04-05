namespace Muphic.CompositionScreenParts.CompositionMainParts.Buttons
{
	/// <summary>
	/// 作曲画面の楽譜左スクロールボタンクラス
	/// </summary>
	public class LeftScrollButton : Muphic.Common.Button
	{
		/// <summary>
		/// 親にあたる作曲部。
		/// </summary>
		public CompositionMain Parent { get; set; }


		/// <summary>
		/// 左スクロールボタンのインスタンス化を行う。
		/// </summary>
		/// <param name="compositionMain">親にあたる作曲部。</param>
		public LeftScrollButton(CompositionMain compositionMain)
		{
			this.Parent = compositionMain;

			this.SetBgTexture(this.ToString(), Locations.ScrollLeftButton, "IMAGE_BUTTON_ARROW2_L_GREEN", "IMAGE_BUTTON_ARROW2_L_ON");
			this.LabelName = "";
		}


		/// <summary>
		/// 左スクロールボタンがクリックされた場合の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.LeftScroll();
		}
	}
}
