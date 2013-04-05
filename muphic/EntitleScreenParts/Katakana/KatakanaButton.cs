using System.Drawing;

namespace Muphic.EntitleScreenParts.Katakana
{
	/// <summary>
	/// 平仮名ボタン
	/// </summary>
	public class KatakanaButton : CharacterButton
	{
		/// <summary>
		/// 親にあたる平仮名ボタン群管理クラス
		/// </summary>
		public KatakanaButtons Parent { get; private set; }

		
		/// <summary>
		/// 平仮名ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="katakanaButtons">親にあたる平仮名ボタン群管理クラス。</param>
		/// <param name="character">ラベルの文字。</param>
		/// <param name="xNum">ボタンの横位置。</param>
		/// <param name="yNum">ボタンの縦位置。</param>
		public KatakanaButton(KatakanaButtons katakanaButtons, string character, int yNum, int xNum)
			: this(katakanaButtons, character, (double)yNum, (double)xNum)
		{
		}

		/// <summary>
		/// 平仮名ボタンの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="katakanaButtons">親にあたる平仮名ボタン群管理クラス。</param>
		/// <param name="character">ラベルの文字。</param>
		/// <param name="xNum">ボタンの横位置。</param>
		/// <param name="yNum">ボタンの縦位置。</param>
		public KatakanaButton(KatakanaButtons katakanaButtons, string character, double yNum, double xNum)
			: base(character, xNum, yNum)
		{
			this.Parent = katakanaButtons;
		}


		/// <summary>
		/// 平仮名ボタンがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			base.Click(mouseStatus);

			if (this.Character == "゛")
			{
				// 濁点ボタンが押されたら、最後の１文字を濁音に換える
				this.Parent.Parent.Replace(KatakanaButtons.Dakuon(this.Parent.Parent.Text[this.Parent.Parent.Text.Length - 1]).ToString());
			}
			else if (this.Character == "゜")
			{
				// 半濁点ボタンが押されたら、最後の１文字を半濁音に換える
				this.Parent.Parent.Replace(KatakanaButtons.Handakuon(this.Parent.Parent.Text[this.Parent.Parent.Text.Length - 1]).ToString());
			}
			else
			{
				this.Parent.Parent.Add(this.Character);
			}

			this.Parent.SetEnabled();
		}
	}
}
