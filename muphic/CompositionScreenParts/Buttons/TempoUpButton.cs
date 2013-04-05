
namespace Muphic.CompositionScreenParts.Buttons
{
	/// <summary>
	/// テンポアップボタンクラス
	/// </summary>
	public class TempoUpButton : Common.Button
	{
		/// <summary>
		/// 親にあたるテンポ管理クラス
		/// </summary>
		public Tempo Parent { get; private set; }


		/// <summary>
		/// テンポアップボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="tempo">。</param>
		public TempoUpButton(Tempo tempo)
		{
			this.Parent = tempo;

			this.SetBgTexture(this.ToString(), Locations.TempoLeftButton, "IMAGE_BUTTON_ARROW2_L_ORANGE", "IMAGE_BUTTON_ARROW2_L_ON");
			this.LabelName = "";
		}

		/// <summary>
		/// テンポアップボ
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		/// </summary>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.TempoMode++;
		}
	}
}
