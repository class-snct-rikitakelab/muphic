
namespace Muphic.CompositionScreenParts.SubScreens.SubScreenParts
{
	/// <summary>
	/// 曲名入力画面上の"けってい"ボタン
	/// </summary>
	public class TitleDecisionButton : Common.Button
	{
		/// <summary>
		/// 親にあたる曲名入力画面
		/// </summary>
		public ScoreEntitleScreen Parent { get; private set; }

		/// <summary>
		/// "けってい"ボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる曲名入力画面。</param>
		public TitleDecisionButton(ScoreEntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			this.SetBgTexture(this.ToString(), Muphic.EntitleScreenParts.Locations.DecisionButton, "IMAGE_BUTTON_BACK_BLUE", "IMAGE_BUTTON_BACK_ON");
			this.SetLabelTexture(this.ToString(), Muphic.EntitleScreenParts.Locations.DecisionButton, "IMAGE_ENTITLESCR_DECITIONBTN");
		}

		/// <summary>
		/// "けってい"ボタンが押された際の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			// 曲名の決定
			this.Parent.Parent.CurrentScoreData.ScoreName = this.Parent.Text;
			this.Parent.Parent.ScoreTitle.Text = this.Parent.Text;

			// 画面を作曲画面に戻す
			this.Parent.Parent.ScreenMode = CompositionScreenMode.CompositionScreen;
		}
	}
}
