
namespace Muphic.ScoreScreenParts.Buttons
{
	/// <summary>
	/// 楽譜画面でヒツジの楽譜表示を指定するボタン。
	/// </summary>
	public class SheepButton : Muphic.Common.Button
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
		/// ヒツジの楽譜表示ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public SheepButton(ScoreScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Locations.SheepButton, "IMAGE_BUTTON_ELLIPSE_BLUE", "IMAGE_BUTTON_ELLIPSE_ON");
			this.SetLabelTexture(this.ToString(), Locations.SheepButton, "IMAGE_SCORESCR_SHEEPBTN");
		}


		/// <summary>
		/// ヒツジの楽譜表示ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を示す Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScoreMode = ScoreScreenScoreMode.Sheep;
		}
	}
}
