
namespace Muphic.ScoreScreenParts.Buttons
{
	/// <summary>
	/// 楽譜を 1 行前にスクロールするボタン。
	/// </summary>
	public class ScrollBackButton : Muphic.Common.Button
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
		/// 楽譜を 1 行前にスクロールするボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる楽譜画面。</param>
		public ScrollBackButton(ScoreScreen parent)
		{
			this.__parent = parent;

			this.SetBgTexture(this.ToString(), Locations.ScrollBackButton, "IMAGE_BUTTON_ARROW2_L_ORANGE", "IMAGE_BUTTON_ARROW2_L_ON");
		}


		/// <summary>
		/// 楽譜を 1 行前にスクロールするボタンを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			Manager.DrawManager.Draw(this.BackgroundName, this.State, this.Alpha, true);
		}


		/// <summary>
		/// 楽譜を 1 行前にスクロールするボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を示す Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.ScoreMain.ScrollBack();
		}
	}
}
