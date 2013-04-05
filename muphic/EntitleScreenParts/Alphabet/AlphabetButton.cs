using System.Drawing;

namespace Muphic.EntitleScreenParts.Alphabet
{
	/// <summary>
	/// アルファベットボタン
	/// </summary>
	public class AlphabetButton : CharacterButton
	{
		/// <summary>
		/// 親にあたるアルファベットボタン群管理クラス
		/// </summary>
		public AlphabetButtons Parent { get; private set; }


		/// <summary>
		/// アルファベットボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="alphabetButtons">親にあたるアルファベットボタン群管理クラス。</param>
		/// <param name="character">ラベルの文字。</param>
		/// <param name="xNum">ボタンの横位置。</param>
		/// <param name="yNum">ボタンの縦位置。</param>
		public AlphabetButton(AlphabetButtons alphabetButtons, string character, int yNum, int xNum)
			: base(character, xNum, yNum)
		{
			this.Parent = alphabetButtons;
		}


		/// <summary>
		/// アルファベットボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			this.Parent.Parent.Add(this.Character);
		}
	}
}
