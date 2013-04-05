
namespace Muphic.EntitleScreenParts.Alphabet
{
	/// <summary>
	/// アルファベットボタン群の管理を行う。
	/// </summary>
	public class AlphabetButtons : Common.Screen
	{
		/// <summary>
		/// 親にあたる題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }


		/// <summary>
		/// アルファベットボタン群管理クラスの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる題名入力画面。</param>
		public AlphabetButtons(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			// アルファベットボタンの生成と登録
			AlphabetButton[,] alphabetButton = new AlphabetButton[6, 13];

			// ==============================
			//      部品のインスタンス化
			// ==============================
			alphabetButton[0, 00] = new AlphabetButton(this, "Ａ", 0, 0);
			alphabetButton[0, 01] = new AlphabetButton(this, "Ｂ", 0, 1);
			alphabetButton[0, 02] = new AlphabetButton(this, "Ｃ", 0, 2);
			alphabetButton[0, 03] = new AlphabetButton(this, "Ｄ", 0, 3);
			alphabetButton[0, 04] = new AlphabetButton(this, "Ｅ", 0, 4);
			alphabetButton[0, 05] = new AlphabetButton(this, "Ｆ", 0, 5);
			alphabetButton[0, 06] = new AlphabetButton(this, "Ｇ", 0, 6);
			alphabetButton[0, 07] = new AlphabetButton(this, "Ｈ", 0, 7);
			alphabetButton[0, 08] = new AlphabetButton(this, "Ｉ", 0, 8);
			alphabetButton[0, 09] = new AlphabetButton(this, "Ｊ", 0, 9);
			alphabetButton[0, 10] = new AlphabetButton(this, "Ｋ", 0, 10);
			alphabetButton[0, 11] = new AlphabetButton(this, "Ｌ", 0, 11);
			alphabetButton[0, 12] = new AlphabetButton(this, "Ｍ", 0, 12);
			alphabetButton[1, 00] = new AlphabetButton(this, "Ｎ", 1, 0);
			alphabetButton[1, 01] = new AlphabetButton(this, "Ｏ", 1, 1);
			alphabetButton[1, 02] = new AlphabetButton(this, "Ｐ", 1, 2);
			alphabetButton[1, 03] = new AlphabetButton(this, "Ｑ", 1, 3);
			alphabetButton[1, 04] = new AlphabetButton(this, "Ｒ", 1, 4);
			alphabetButton[1, 05] = new AlphabetButton(this, "Ｓ", 1, 5);
			alphabetButton[1, 06] = new AlphabetButton(this, "Ｔ", 1, 6);
			alphabetButton[1, 07] = new AlphabetButton(this, "Ｕ", 1, 7);
			alphabetButton[1, 08] = new AlphabetButton(this, "Ｖ", 1, 8);
			alphabetButton[1, 09] = new AlphabetButton(this, "Ｗ", 1, 9);
			alphabetButton[1, 10] = new AlphabetButton(this, "Ｘ", 1, 10);
			alphabetButton[1, 11] = new AlphabetButton(this, "Ｙ", 1, 11);
			alphabetButton[1, 12] = new AlphabetButton(this, "Ｚ", 1, 12);
			alphabetButton[3, 00] = new AlphabetButton(this, "ａ", 3, 0);
			alphabetButton[3, 01] = new AlphabetButton(this, "ｂ", 3, 1);
			alphabetButton[3, 02] = new AlphabetButton(this, "ｃ", 3, 2);
			alphabetButton[3, 03] = new AlphabetButton(this, "ｄ", 3, 3);
			alphabetButton[3, 04] = new AlphabetButton(this, "ｅ", 3, 4);
			alphabetButton[3, 05] = new AlphabetButton(this, "ｆ", 3, 5);
			alphabetButton[3, 06] = new AlphabetButton(this, "ｇ", 3, 6);
			alphabetButton[3, 07] = new AlphabetButton(this, "ｈ", 3, 7);
			alphabetButton[3, 08] = new AlphabetButton(this, "ｉ", 3, 8);
			alphabetButton[3, 09] = new AlphabetButton(this, "ｊ", 3, 9);
			alphabetButton[3, 10] = new AlphabetButton(this, "ｋ", 3, 10);
			alphabetButton[3, 11] = new AlphabetButton(this, "ｌ", 3, 11);
			alphabetButton[3, 12] = new AlphabetButton(this, "ｍ", 3, 12);
			alphabetButton[4, 00] = new AlphabetButton(this, "ｎ", 4, 0);
			alphabetButton[4, 01] = new AlphabetButton(this, "ｏ", 4, 1);
			alphabetButton[4, 02] = new AlphabetButton(this, "ｐ", 4, 2);
			alphabetButton[4, 03] = new AlphabetButton(this, "ｑ", 4, 3);
			alphabetButton[4, 04] = new AlphabetButton(this, "ｒ", 4, 4);
			alphabetButton[4, 05] = new AlphabetButton(this, "ｓ", 4, 5);
			alphabetButton[4, 06] = new AlphabetButton(this, "ｔ", 4, 6);
			alphabetButton[4, 07] = new AlphabetButton(this, "ｕ", 4, 7);
			alphabetButton[4, 08] = new AlphabetButton(this, "ｖ", 4, 8);
			alphabetButton[4, 09] = new AlphabetButton(this, "ｗ", 4, 9);
			alphabetButton[4, 10] = new AlphabetButton(this, "ｘ", 4, 10);
			alphabetButton[4, 11] = new AlphabetButton(this, "ｙ", 4, 11);
			alphabetButton[4, 12] = new AlphabetButton(this, "ｚ", 4, 12);

			// ==============================
			//      部品のリストへの登録
			// ==============================
			foreach (AlphabetButton button in alphabetButton)
			{
				// nullであれば無視し、nullでなければ登録する
				if (button == null) continue;
				else this.PartsList.Add(button);
			}

			// ==============================
			//     テクスチャと座標の登録
			// ==============================
			Manager.DrawManager.Regist(this.ToString(), Locations.CharArea.Location, "IMAGE_ENTITLESCR_CHAEAREA");
		}


		/// <summary>
		/// アルファベットボタンの有効/無効の設定を行う。
		/// </summary>
		public void SetEnabled()
		{
			if (this.Parent.Text.Length >= this.Parent.MaxLength)
			{
				// 題名の文字数が最大値以上になっていた場合
				// 全アルファベットボタンを無効化する
				foreach (AlphabetButton button in PartsList)
				{
					button.Enabled = false;
				}
			}
			else
			{
				// 題名の文字数が最大値未満だった場合
				// 全アルファベットボタンを有効にする
				foreach (AlphabetButton button in PartsList)
				{
					button.Enabled = true;
				}
			}
		}


		/// <summary>
		/// アルファベットボタン群とカテゴリラベルテクスチャの描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			Manager.DrawManager.Draw("IMAGE_ENTITLESCR_CATEGORY_ALPHABET", Locations.EntitleCategoryLabel);
		}
	}
}
