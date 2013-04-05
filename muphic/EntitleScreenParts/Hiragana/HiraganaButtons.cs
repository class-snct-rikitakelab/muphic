
namespace Muphic.EntitleScreenParts.Hiragana
{
	/// <summary>
	/// 平仮名ボタン群の管理を行う。
	/// </summary>
	public class HiraganaButtons : Common.Screen
	{
		/// <summary>
		/// 親にあたる題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }


		/// <summary>
		/// 平仮名ボタン群管理クラスの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる題名入力画面。</param>
		public HiraganaButtons(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			// 平仮名ボタンの生成と登録
			HiraganaButton[,] hiraganaButton = new HiraganaButton[7, 13];

			// ==============================
			//      部品のインスタンス化
			// ==============================
			hiraganaButton[0, 00] = new HiraganaButton(this, "ゃ", 0, 0);
			hiraganaButton[0, 01] = new HiraganaButton(this, "ぁ", 0, 1);
			hiraganaButton[0, 03] = new HiraganaButton(this, "わ", 0, 3);
			hiraganaButton[0, 04] = new HiraganaButton(this, "ら", 0, 4);
			hiraganaButton[0, 05] = new HiraganaButton(this, "や", 0, 5);
			hiraganaButton[0, 06] = new HiraganaButton(this, "ま", 0, 6);
			hiraganaButton[0, 07] = new HiraganaButton(this, "は", 0, 7);
			hiraganaButton[0, 08] = new HiraganaButton(this, "な", 0, 8);
			hiraganaButton[0, 09] = new HiraganaButton(this, "た", 0, 9);
			hiraganaButton[0, 10] = new HiraganaButton(this, "さ", 0, 10);
			hiraganaButton[0, 11] = new HiraganaButton(this, "か", 0, 11);
			hiraganaButton[0, 12] = new HiraganaButton(this, "あ", 0, 12);
			hiraganaButton[1, 00] = new HiraganaButton(this, "ゅ", 1, 0);
			hiraganaButton[1, 01] = new HiraganaButton(this, "ぃ", 1, 1);
			hiraganaButton[1, 03] = new HiraganaButton(this, "を", 1, 3);
			hiraganaButton[1, 04] = new HiraganaButton(this, "り", 1, 4);
			hiraganaButton[1, 06] = new HiraganaButton(this, "み", 1, 6);
			hiraganaButton[1, 07] = new HiraganaButton(this, "ひ", 1, 7);
			hiraganaButton[1, 08] = new HiraganaButton(this, "に", 1, 8);
			hiraganaButton[1, 09] = new HiraganaButton(this, "ち", 1, 9);
			hiraganaButton[1, 10] = new HiraganaButton(this, "し", 1, 10);
			hiraganaButton[1, 11] = new HiraganaButton(this, "き", 1, 11);
			hiraganaButton[1, 12] = new HiraganaButton(this, "い", 1, 12);
			hiraganaButton[2, 00] = new HiraganaButton(this, "ょ", 2, 0);
			hiraganaButton[2, 01] = new HiraganaButton(this, "ぅ", 2, 1);
			hiraganaButton[2, 03] = new HiraganaButton(this, "ん", 2, 3);
			hiraganaButton[2, 04] = new HiraganaButton(this, "る", 2, 4);
			hiraganaButton[2, 05] = new HiraganaButton(this, "ゆ", 2, 5);
			hiraganaButton[2, 06] = new HiraganaButton(this, "む", 2, 6);
			hiraganaButton[2, 07] = new HiraganaButton(this, "ふ", 2, 7);
			hiraganaButton[2, 08] = new HiraganaButton(this, "ぬ", 2, 8);
			hiraganaButton[2, 09] = new HiraganaButton(this, "つ", 2, 9);
			hiraganaButton[2, 10] = new HiraganaButton(this, "す", 2, 10);
			hiraganaButton[2, 11] = new HiraganaButton(this, "く", 2, 11);
			hiraganaButton[2, 12] = new HiraganaButton(this, "う", 2, 12);
			hiraganaButton[3, 00] = new HiraganaButton(this, "っ", 3, 0);
			hiraganaButton[3, 01] = new HiraganaButton(this, "ぇ", 3, 1);
			hiraganaButton[3, 04] = new HiraganaButton(this, "れ", 3, 4);
			hiraganaButton[3, 06] = new HiraganaButton(this, "め", 3, 6);
			hiraganaButton[3, 07] = new HiraganaButton(this, "へ", 3, 7);
			hiraganaButton[3, 08] = new HiraganaButton(this, "ね", 3, 8);
			hiraganaButton[3, 09] = new HiraganaButton(this, "て", 3, 9);
			hiraganaButton[3, 10] = new HiraganaButton(this, "せ", 3, 10);
			hiraganaButton[3, 11] = new HiraganaButton(this, "け", 3, 11);
			hiraganaButton[3, 12] = new HiraganaButton(this, "え", 3, 12);
			hiraganaButton[4, 00] = new HiraganaButton(this, "ゎ", 4, 0);
			hiraganaButton[4, 01] = new HiraganaButton(this, "ぉ", 4, 1);
			hiraganaButton[4, 04] = new HiraganaButton(this, "ろ", 4, 4);
			hiraganaButton[4, 05] = new HiraganaButton(this, "よ", 4, 5);
			hiraganaButton[4, 06] = new HiraganaButton(this, "も", 4, 6);
			hiraganaButton[4, 07] = new HiraganaButton(this, "ほ", 4, 7);
			hiraganaButton[4, 08] = new HiraganaButton(this, "の", 4, 8);
			hiraganaButton[4, 09] = new HiraganaButton(this, "と", 4, 9);
			hiraganaButton[4, 10] = new HiraganaButton(this, "そ", 4, 10);
			hiraganaButton[4, 11] = new HiraganaButton(this, "こ", 4, 11);
			hiraganaButton[4, 12] = new HiraganaButton(this, "お", 4, 12);
			hiraganaButton[5, 00] = new HiraganaButton(this, "゛", 5.4, 0);
			hiraganaButton[5, 01] = new HiraganaButton(this, "゜", 5.4, 1);
			hiraganaButton[6, 09] = new HiraganaButton(this, "　", 5.4, 9);
			hiraganaButton[6, 10] = new HiraganaButton(this, "ー", 5.4, 10);
			hiraganaButton[6, 11] = new HiraganaButton(this, "、", 5.4, 11);
			hiraganaButton[6, 12] = new HiraganaButton(this, "。", 5.4, 12);


			// ==============================
			//      部品のリストへの登録
			// ==============================
			foreach (HiraganaButton button in hiraganaButton)
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
		/// 平仮名ボタンの有効/無効の設定を行う。
		/// </summary>
		public void SetEnabled()
		{
			if (this.Parent.Text.Length >= this.Parent.MaxLength)
			{
				// 題名の文字数が最大値以上になっていた場合
				// 全平仮名ボタンを無効化する
				foreach (HiraganaButton button in PartsList)
				{
					button.Enabled = false;
				}
			}
			else
			{
				// 題名の文字数が最大値未満だった場合
				// 全平仮名ボタンを有効にする
				foreach (HiraganaButton button in PartsList)
				{
					button.Enabled = true;
				}
			}

			if (this.Parent.Text.Length > 0)
			{
				// 題名の文字数が1文字以上あった場合
				char check;

				// 末尾の文字に濁点を付けられるかをチェックし、付けられるなら濁点ボタンを有効にする
				check = HiraganaButtons.Dakuon(this.Parent.Text[this.Parent.Text.Length - 1]);
				if (check == ' ') ((HiraganaButton)this.PartsList[this.PartsList.Count - 6]).Enabled = false;
				else ((HiraganaButton)this.PartsList[this.PartsList.Count - 2]).Enabled = true;

				// 末尾の文字に半濁点を付けられるかをチェックし、付けられるなら濁半点ボタンを有効にする
				check = HiraganaButtons.Handakuon(this.Parent.Text[this.Parent.Text.Length - 1]);
				if (check == ' ') ((HiraganaButton)this.PartsList[this.PartsList.Count - 5]).Enabled = false;
				else ((HiraganaButton)this.PartsList[this.PartsList.Count - 1]).Enabled = true;
			}
			else
			{
				// 題名の文字数が0文字だった場合、問答無用で濁点・半濁点ボタン無効
				((HiraganaButton)this.PartsList[this.PartsList.Count - 6]).Enabled = false;
				((HiraganaButton)this.PartsList[this.PartsList.Count - 5]).Enabled = false;
			}
		}


		/// <summary>
		/// 平仮名ボタン群とカテゴリラベルテクスチャの描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			Manager.DrawManager.Draw("IMAGE_ENTITLESCR_CATEGORY_HIRAGANA", Locations.EntitleCategoryLabel);
		}


		/// <summary>
		/// 指定した文字に濁点が付けられるかをチェックする。
		/// </summary>
		/// <param name='character'>チェックする文字。</param>
		/// <returns>濁点が付けられる場合はその濁音、それ以外は空の文字。</returns>
		public static char Dakuon(char character)
		{
			switch (character)
			{
				case 'か':
					return 'が';
				case 'き':
					return 'ぎ';
				case 'く':
					return 'ぐ';
				case 'け':
					return 'げ';
				case 'こ':
					return 'ご';

				case 'さ':
					return 'ざ';
				case 'し':
					return 'じ';
				case 'す':
					return 'ず';
				case 'せ':
					return 'ぜ';
				case 'そ':
					return 'ぞ';

				case 'た':
					return 'だ';
				case 'ち':
					return 'ぢ';
				case 'つ':
					return 'づ';
				case 'て':
					return 'で';
				case 'と':
					return 'ど';

				case 'は':
					return 'ば';
				case 'ひ':
					return 'び';
				case 'ふ':
					return 'ぶ';
				case 'へ':
					return 'べ';
				case 'ほ':
					return 'ぼ';

				default:
					return ' ';
			}
		}


		/// <summary>
		/// 指定した文字に半濁点が付けられるかをチェックする。
		/// </summary>
		/// <param name="character">チェックする文字。</param>
		/// <returns>半濁点が付けられる場合はその半濁音、それ以外は空の文字。</returns>
		public static char Handakuon(char character)
		{
			switch (character)
			{
				case 'は':
					return 'ぱ';
				case 'ひ':
					return 'ぴ';
				case 'ふ':
					return 'ぷ';
				case 'へ':
					return 'ぺ';
				case 'ほ':
					return 'ぽ';

				default:
					return ' ';
			}
		}
	}
}

#region ボタン群配列

//HiraganaButton[,] hiraganaButton = new HiraganaButton[6, 13];

//hiraganaButton[0, 00] = new HiraganaButton(this, "", 0, 0);
//hiraganaButton[0, 01] = new HiraganaButton(this, "", 0, 1);
//hiraganaButton[0, 02] = new HiraganaButton(this, "", 0, 2);
//hiraganaButton[0, 03] = new HiraganaButton(this, "", 0, 3);
//hiraganaButton[0, 04] = new HiraganaButton(this, "", 0, 4);
//hiraganaButton[0, 05] = new HiraganaButton(this, "", 0, 5);
//hiraganaButton[0, 06] = new HiraganaButton(this, "", 0, 6);
//hiraganaButton[0, 07] = new HiraganaButton(this, "", 0, 7);
//hiraganaButton[0, 08] = new HiraganaButton(this, "", 0, 8);
//hiraganaButton[0, 09] = new HiraganaButton(this, "", 0, 9);
//hiraganaButton[0, 10] = new HiraganaButton(this, "", 0, 10);
//hiraganaButton[0, 11] = new HiraganaButton(this, "", 0, 11);
//hiraganaButton[0, 12] = new HiraganaButton(this, "", 0, 12);
//hiraganaButton[1, 00] = new HiraganaButton(this, "", 1, 0);
//hiraganaButton[1, 01] = new HiraganaButton(this, "", 1, 1);
//hiraganaButton[1, 02] = new HiraganaButton(this, "", 1, 2);
//hiraganaButton[1, 03] = new HiraganaButton(this, "", 1, 3);
//hiraganaButton[1, 04] = new HiraganaButton(this, "", 1, 4);
//hiraganaButton[1, 05] = new HiraganaButton(this, "", 1, 5);
//hiraganaButton[1, 06] = new HiraganaButton(this, "", 1, 6);
//hiraganaButton[1, 07] = new HiraganaButton(this, "", 1, 7);
//hiraganaButton[1, 08] = new HiraganaButton(this, "", 1, 8);
//hiraganaButton[1, 09] = new HiraganaButton(this, "", 1, 9);
//hiraganaButton[1, 10] = new HiraganaButton(this, "", 1, 10);
//hiraganaButton[1, 11] = new HiraganaButton(this, "", 1, 11);
//hiraganaButton[1, 12] = new HiraganaButton(this, "", 1, 12);
//hiraganaButton[2, 00] = new HiraganaButton(this, "", 2, 0);
//hiraganaButton[2, 01] = new HiraganaButton(this, "", 2, 1);
//hiraganaButton[2, 02] = new HiraganaButton(this, "", 2, 2);
//hiraganaButton[2, 03] = new HiraganaButton(this, "", 2, 3);
//hiraganaButton[2, 04] = new HiraganaButton(this, "", 2, 4);
//hiraganaButton[2, 05] = new HiraganaButton(this, "", 2, 5);
//hiraganaButton[2, 06] = new HiraganaButton(this, "", 2, 6);
//hiraganaButton[2, 07] = new HiraganaButton(this, "", 2, 7);
//hiraganaButton[2, 08] = new HiraganaButton(this, "", 2, 8);
//hiraganaButton[2, 09] = new HiraganaButton(this, "", 2, 9);
//hiraganaButton[2, 10] = new HiraganaButton(this, "", 2, 10);
//hiraganaButton[2, 11] = new HiraganaButton(this, "", 2, 11);
//hiraganaButton[2, 12] = new HiraganaButton(this, "", 2, 12);
//hiraganaButton[3, 00] = new HiraganaButton(this, "", 3, 0);
//hiraganaButton[3, 01] = new HiraganaButton(this, "", 3, 1);
//hiraganaButton[3, 02] = new HiraganaButton(this, "", 3, 2);
//hiraganaButton[3, 03] = new HiraganaButton(this, "", 3, 3);
//hiraganaButton[3, 04] = new HiraganaButton(this, "", 3, 4);
//hiraganaButton[3, 05] = new HiraganaButton(this, "", 3, 5);
//hiraganaButton[3, 06] = new HiraganaButton(this, "", 3, 6);
//hiraganaButton[3, 07] = new HiraganaButton(this, "", 3, 7);
//hiraganaButton[3, 08] = new HiraganaButton(this, "", 3, 8);
//hiraganaButton[3, 09] = new HiraganaButton(this, "", 3, 9);
//hiraganaButton[3, 10] = new HiraganaButton(this, "", 3, 10);
//hiraganaButton[3, 11] = new HiraganaButton(this, "", 3, 11);
//hiraganaButton[3, 12] = new HiraganaButton(this, "", 3, 12);
//hiraganaButton[4, 00] = new HiraganaButton(this, "", 4, 0);
//hiraganaButton[4, 01] = new HiraganaButton(this, "", 4, 1);
//hiraganaButton[4, 02] = new HiraganaButton(this, "", 4, 2);
//hiraganaButton[4, 03] = new HiraganaButton(this, "", 4, 3);
//hiraganaButton[4, 04] = new HiraganaButton(this, "", 4, 4);
//hiraganaButton[4, 05] = new HiraganaButton(this, "", 4, 5);
//hiraganaButton[4, 06] = new HiraganaButton(this, "", 4, 6);
//hiraganaButton[4, 07] = new HiraganaButton(this, "", 4, 7);
//hiraganaButton[4, 08] = new HiraganaButton(this, "", 4, 8);
//hiraganaButton[4, 09] = new HiraganaButton(this, "", 4, 9);
//hiraganaButton[4, 10] = new HiraganaButton(this, "", 4, 10);
//hiraganaButton[4, 11] = new HiraganaButton(this, "", 4, 11);
//hiraganaButton[4, 12] = new HiraganaButton(this, "", 4, 12);
//hiraganaButton[5, 00] = new HiraganaButton(this, "", 5, 0);
//hiraganaButton[5, 01] = new HiraganaButton(this, "", 5, 1);
//hiraganaButton[5, 02] = new HiraganaButton(this, "", 5, 2);
//hiraganaButton[5, 03] = new HiraganaButton(this, "", 5, 3);
//hiraganaButton[5, 04] = new HiraganaButton(this, "", 5, 4);
//hiraganaButton[5, 05] = new HiraganaButton(this, "", 5, 5);
//hiraganaButton[5, 06] = new HiraganaButton(this, "", 5, 6);
//hiraganaButton[5, 07] = new HiraganaButton(this, "", 5, 7);
//hiraganaButton[5, 08] = new HiraganaButton(this, "", 5, 8);
//hiraganaButton[5, 09] = new HiraganaButton(this, "", 5, 9);
//hiraganaButton[5, 10] = new HiraganaButton(this, "", 5, 10);
//hiraganaButton[5, 11] = new HiraganaButton(this, "", 5, 11);
//hiraganaButton[5, 12] = new HiraganaButton(this, "", 5, 12);

#endregion
