
namespace Muphic.ScoreScreenParts.Buttons
{
	/// <summary>
	/// 楽譜画面から汎用作曲画面へ戻るボタン。
	/// </summary>
	public class BackButton : Muphic.Common.Button
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
		/// 楽譜画面から汎用作曲画面へ戻るボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public BackButton(ScoreScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Locations.BackButton, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Locations.BackButton, "IMAGE_SCORESCR_BACKBTN");
		}


		/// <summary>
		/// 楽譜画面から汎用作曲画面へ戻るボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を示す Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Parent.ScreenMode = CompositionScreenMode.CompositionScreen;
		}
	}
}
