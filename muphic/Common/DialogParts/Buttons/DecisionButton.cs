using System.Drawing;

namespace Muphic.Common.DialogParts.Buttons
{
	/// <summary>
	/// ダイアログ上の "けってい" ボタンクラス。
	/// </summary>
	public class DecisionButton : Button
	{
		/// <summary>
		/// 親にあたるダイアログ。
		/// </summary>
		public Dialog Parent { get; private set; }


		/// <summary>
		/// "もどる" ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="dialog">親にあたるダイアログ。</param>
		/// <param name="location">描画する際の左上座標。</param>
		public DecisionButton(Dialog dialog, Point location)
		{
			this.Parent = dialog;

			this.SetBgTexture(this.ToString(), Tools.CommonTools.AddPoints(this.Parent.Location, location), "IMAGE_BUTTON_BOX3_BLUE", "IMAGE_BUTTON_BOX3_ON");
			this.SetLabelTexture(this.ToString(), Tools.CommonTools.AddPoints(this.Parent.Location, location), "IMAGE_DIALOG_BUTTON_DECISION");
		}


		/// <summary>
		/// "もどる" ボタンがクリックされた時に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.DialogResult = DialogResult.OK;
		}


		/// <summary>
		/// 現在の System.Object に、親ダイアログの名前を付加した System.String を返す。
		/// </summary>
		/// <returns>現在のファイル選択領域に親ダイアログの名前を付加した System.String。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Parent.ParentName;
		}
	}
}
