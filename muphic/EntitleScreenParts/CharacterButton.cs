using System.Drawing;
using System.Text;

using Muphic.Manager;

namespace Muphic.EntitleScreenParts
{
	/// <summary>
	/// 汎用題名入力画面上の汎用文字ボタン
	/// </summary>
	public abstract class CharacterButton : Common.Button
	{
		/// <summary>
		/// ボタンに表示する文字を表わす System.String 。
		/// </summary>
		public string Character { get; private set; }

		/// <summary>
		/// ボタンを表示する座標を表わす System.Drawing.Point 。
		/// </summary>
		public Point Location { get; private set; }


		
		/// <summary>
		/// 文字ボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="character">ラベルの文字。</param>
		/// <param name="xNum">ボタンの横位置。</param>
		/// <param name="yNum">ボタンの縦位置。</param>
		protected CharacterButton(string character, int xNum, int yNum)
			: this(character, (double)xNum, (double)yNum)
		{
		}

		/// <summary>
		/// 文字ボタンの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="character">ラベルの文字。</param>
		/// <param name="xNum">ボタンの横位置。</param>
		/// <param name="yNum">ボタンの縦位置。</param>
		protected CharacterButton(string character, double xNum, double yNum)
		{
			this.Character = character;

			// ボタン表示位置の算出
			Point location = Locations.CharButtonBase;
			location.X += (int)(xNum * Locations.CharButtonDiff.Width);
			location.Y += (int)(yNum * Locations.CharButtonDiff.Height);
			this.Location = location;

			// ボタンの背景テクスチャを登録 全ボタン共通
			this.SetBgTexture(this.ToString(), location, "IMAGE_BUTTON_MINIBOX_GREEN", "IMAGE_BUTTON_MINIBOX_ON");

			// ボタンのラベルテクスチャを登録 位置はボタンの中央になるように調整 ラベルはそのボタンの文字
			location.X += (TextureFileManager.GetRectangle("IMAGE_BUTTON_MINIBOX_GREEN").Width - StringManager.StringSize.Width) / 2 + 1;
			location.Y += (TextureFileManager.GetRectangle("IMAGE_BUTTON_MINIBOX_GREEN").Height - StringManager.StringSize.Height) / 2;
			this.SetLabelTexture(this.ToString(), location, character);
		}


		/// <summary>
		/// 現在の System.Object に、ボタンに表示する文字列を付加した System.String を返す。
		/// </summary>
		/// <returns>。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Character;
		}
	}
}
