
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// テンポダウンボタンクラス
	/// </summary>
	public class TempoDownButton : Common.Button
{
		/// <summary>
		/// 親にあたるテンポ管理クラス
		/// </summary>
		public Tempo Parent { get; private set; }


		/// <summary>
		/// テンポダウンボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="tempo">。</param>
		public TempoDownButton(Tempo tempo)
		{
			this.Parent = tempo;

			this.SetBgTexture(this.ToString(), Locations.TempoRightButton, "IMAGE_BUTTON_ARROW2_R_ORANGE", "IMAGE_BUTTON_ARROW2_R_ON");
			this.LabelName = "";
		}

		/// <summary>
		/// テンポダウンボタンがクリックされた場合の処理
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.TempoMode--;
		}
	}
}
