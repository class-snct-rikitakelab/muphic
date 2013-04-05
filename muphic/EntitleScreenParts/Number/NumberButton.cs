using System.Drawing;

namespace Muphic.EntitleScreenParts.Number
{
	/// <summary>
	/// 数字・記号ボタン
	/// </summary>
	public class NumberButton : CharacterButton
	{
		/// <summary>
		/// 親にあたる数字・記号ボタン群管理クラス
		/// </summary>
		public NumberButtons Parent { get; private set; }


		/// <summary>
		/// 数字・記号ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="numberButtons">親にあたる数字・記号ボタン群管理クラス。</param>
		/// <param name="character">ラベルの文字。</param>
		/// <param name="xNum">ボタンの横位置。</param>
		/// <param name="yNum">ボタンの縦位置。</param>
		public NumberButton(NumberButtons numberButtons, string character, int yNum, int xNum)
			: base(character, xNum, yNum)
		{
			this.Parent = numberButtons;
		}


		/// <summary>
		/// 数字・記号ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Parent.Add(this.Character);
		}
	}
}
