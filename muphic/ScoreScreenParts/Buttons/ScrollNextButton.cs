
namespace Muphic.ScoreScreenParts.Buttons
{
	/// <summary>
	/// 楽譜を 1 行次にスクロールするボタン。
	/// </summary>
	public class ScrollNextButton : Muphic.Common.Button
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
		/// 楽譜を 1 行次にスクロールするボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public ScrollNextButton(ScoreScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Locations.ScrollNextButton, "IMAGE_BUTTON_ARROW2_R_ORANGE", "IMAGE_BUTTON_ARROW2_R_ON");
		}


		/// <summary>
		/// 楽譜を 1 行次にスクロールするボタンを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			Manager.DrawManager.Draw(this.BackgroundName, this.State, this.Alpha, true);
		}


		/// <summary>
		/// 楽譜を 1 行次にスクロールするボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を示す Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScoreMain.ScrollNext();
		}
	}
}
