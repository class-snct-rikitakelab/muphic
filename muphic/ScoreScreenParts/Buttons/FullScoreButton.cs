
namespace Muphic.ScoreScreenParts.Buttons
{
	/// <summary>
	/// 楽譜画面でフルスコア表示を指定するボタン。
	/// </summary>
	public class FullScoreButton : Muphic.Common.Button
	{
		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		private readonly ScoreScreen __parent;

		/// <summary>
		/// 親にあたる楽譜画面。
		/// </summary>
		public ScoreScreen Parent
		{
			get { return this.__parent; }
		}


		/// <summary>
		/// フルスコア表示ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public FullScoreButton(ScoreScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Locations.FullScoreButton, "IMAGE_BUTTON_ELLIPSE_BLUE", "IMAGE_BUTTON_ELLIPSE_ON");
			this.SetLabelTexture(this.ToString(), Locations.FullScoreButton, "IMAGE_SCORESCR_FULLSCOREBTN");
		}


		/// <summary>
		/// フルスコア表示ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を示す Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScoreMode = ScoreScreenScoreMode.FullScore;
		}
	}
}
