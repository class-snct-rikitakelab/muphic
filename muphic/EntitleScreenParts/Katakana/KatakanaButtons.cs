
namespace Muphic.EntitleScreenParts.Katakana
{
	/// <summary>
	/// 片仮名ボタン群の管理を行う。
	/// </summary>
	public class KatakanaButtons : Common.Screen
	{
		/// <summary>
		/// 親にあたる題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }


		/// <summary>
		/// 片仮名ボタン群管理クラスの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる題名入力画面。</param>
		public KatakanaButtons(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			// 片仮名ボタンの生成と登録
			KatakanaButton[,] katakanaButton = new KatakanaButton[7, 13];

			// ==============================
			//      部品のインスタンス化
			// ==============================
			katakanaButton[0, 00] = new KatakanaButton(this, "ャ", 0, 0);
			katakanaButton[0, 01] = new KatakanaButton(this, "ァ", 0, 1);
			katakanaButton[0, 03] = new KatakanaButton(this, "ワ", 0, 3);
			katakanaButton[0, 04] = new KatakanaButton(this, "ラ", 0, 4);
			katakanaButton[0, 05] = new KatakanaButton(this, "ヤ", 0, 5);
			katakanaButton[0, 06] = new KatakanaButton(this, "マ", 0, 6);
			katakanaButton[0, 07] = new KatakanaButton(this, "ハ", 0, 7);
			katakanaButton[0, 08] = new KatakanaButton(this, "ナ", 0, 8);
			katakanaButton[0, 09] = new KatakanaButton(this, "タ", 0, 9);
			katakanaButton[0, 10] = new KatakanaButton(this, "サ", 0, 10);
			katakanaButton[0, 11] = new KatakanaButton(this, "カ", 0, 11);
			katakanaButton[0, 12] = new KatakanaButton(this, "ア", 0, 12);
			katakanaButton[1, 00] = new KatakanaButton(this, "ュ", 1, 0);
			katakanaButton[1, 01] = new KatakanaButton(this, "ィ", 1, 1);
			katakanaButton[1, 03] = new KatakanaButton(this, "ヲ", 1, 3);
			katakanaButton[1, 04] = new KatakanaButton(this, "リ", 1, 4);
			katakanaButton[1, 06] = new KatakanaButton(this, "ミ", 1, 6);
			katakanaButton[1, 07] = new KatakanaButton(this, "ヒ", 1, 7);
			katakanaButton[1, 08] = new KatakanaButton(this, "ニ", 1, 8);
			katakanaButton[1, 09] = new KatakanaButton(this, "チ", 1, 9);
			katakanaButton[1, 10] = new KatakanaButton(this, "シ", 1, 10);
			katakanaButton[1, 11] = new KatakanaButton(this, "キ", 1, 11);
			katakanaButton[1, 12] = new KatakanaButton(this, "イ", 1, 12);
			katakanaButton[2, 00] = new KatakanaButton(this, "ョ", 2, 0);
			katakanaButton[2, 01] = new KatakanaButton(this, "ゥ", 2, 1);
			katakanaButton[2, 03] = new KatakanaButton(this, "ン", 2, 3);
			katakanaButton[2, 04] = new KatakanaButton(this, "ル", 2, 4);
			katakanaButton[2, 05] = new KatakanaButton(this, "ユ", 2, 5);
			katakanaButton[2, 06] = new KatakanaButton(this, "ム", 2, 6);
			katakanaButton[2, 07] = new KatakanaButton(this, "フ", 2, 7);
			katakanaButton[2, 08] = new KatakanaButton(this, "ヌ", 2, 8);
			katakanaButton[2, 09] = new KatakanaButton(this, "ツ", 2, 9);
			katakanaButton[2, 10] = new KatakanaButton(this, "ス", 2, 10);
			katakanaButton[2, 11] = new KatakanaButton(this, "ク", 2, 11);
			katakanaButton[2, 12] = new KatakanaButton(this, "ウ", 2, 12);
			katakanaButton[3, 00] = new KatakanaButton(this, "ッ", 3, 0);
			katakanaButton[3, 01] = new KatakanaButton(this, "ェ", 3, 1);
			katakanaButton[3, 04] = new KatakanaButton(this, "レ", 3, 4);
			katakanaButton[3, 06] = new KatakanaButton(this, "メ", 3, 6);
			katakanaButton[3, 07] = new KatakanaButton(this, "ヘ", 3, 7);
			katakanaButton[3, 08] = new KatakanaButton(this, "ネ", 3, 8);
			katakanaButton[3, 09] = new KatakanaButton(this, "テ", 3, 9);
			katakanaButton[3, 10] = new KatakanaButton(this, "セ", 3, 10);
			katakanaButton[3, 11] = new KatakanaButton(this, "ケ", 3, 11);
			katakanaButton[3, 12] = new KatakanaButton(this, "エ", 3, 12);
			katakanaButton[4, 00] = new KatakanaButton(this, "ヮ", 4, 0);
			katakanaButton[4, 01] = new KatakanaButton(this, "ォ", 4, 1);
			katakanaButton[4, 04] = new KatakanaButton(this, "ロ", 4, 4);
			katakanaButton[4, 05] = new KatakanaButton(this, "ヨ", 4, 5);
			katakanaButton[4, 06] = new KatakanaButton(this, "モ", 4, 6);
			katakanaButton[4, 07] = new KatakanaButton(this, "ホ", 4, 7);
			katakanaButton[4, 08] = new KatakanaButton(this, "ノ", 4, 8);
			katakanaButton[4, 09] = new KatakanaButton(this, "ト", 4, 9);
			katakanaButton[4, 10] = new KatakanaButton(this, "ソ", 4, 10);
			katakanaButton[4, 11] = new KatakanaButton(this, "コ", 4, 11);
			katakanaButton[4, 12] = new KatakanaButton(this, "オ", 4, 12);
			katakanaButton[5, 00] = new KatakanaButton(this, "゛", 5.4, 0);
			katakanaButton[5, 01] = new KatakanaButton(this, "゜", 5.4, 1);
			katakanaButton[6, 09] = new KatakanaButton(this, "　", 5.4, 9);
			katakanaButton[6, 10] = new KatakanaButton(this, "ー", 5.4, 10);
			katakanaButton[6, 11] = new KatakanaButton(this, "、", 5.4, 11);
			katakanaButton[6, 12] = new KatakanaButton(this, "。", 5.4, 12);

			// ==============================
			//      部品のリストへの登録
			// ==============================
			foreach (KatakanaButton button in katakanaButton)
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
		/// 片仮名ボタンの有効/無効の設定を行う。
		/// </summary>
		public void SetEnabled()
		{
			if (this.Parent.Text.Length >= this.Parent.MaxLength)
			{
				// 題名の文字数が最大値以上になっていた場合
				// 全片仮名ボタンを無効化する
				foreach (KatakanaButton button in PartsList)
				{
					button.Enabled = false;
				}
			}
			else
			{
				// 題名の文字数が最大値未満だった場合
				// 全片仮名ボタンを有効にする
				foreach (KatakanaButton button in PartsList)
				{
					button.Enabled = true;
				}
			}

			if (this.Parent.Text.Length > 0)
			{
				// 題名の文字数が1文字以上あった場合
				char check;

				// 末尾の文字に濁点を付けられるかをチェックし、付けられるなら濁点ボタンを有効にする
				check = KatakanaButtons.Dakuon(this.Parent.Text[this.Parent.Text.Length - 1]);
				if (check == ' ') ((KatakanaButton)this.PartsList[this.PartsList.Count - 6]).Enabled = false;
				else ((KatakanaButton)this.PartsList[this.PartsList.Count - 6]).Enabled = true;

				// 末尾の文字に半濁点を付けられるかをチェックし、付けられるなら濁半点ボタンを有効にする
				check = KatakanaButtons.Handakuon(this.Parent.Text[this.Parent.Text.Length - 1]);
				if (check == ' ') ((KatakanaButton)this.PartsList[this.PartsList.Count - 5]).Enabled = false;
				else ((KatakanaButton)this.PartsList[this.PartsList.Count - 5]).Enabled = true;
			}
			else
			{
				// 題名の文字数が0文字だった場合、問答無用で濁点・半濁点ボタン無効
				((KatakanaButton)this.PartsList[this.PartsList.Count - 6]).Enabled = false;
				((KatakanaButton)this.PartsList[this.PartsList.Count - 5]).Enabled = false;
			}
		}


		/// <summary>
		/// カタカナボタン群とカテゴリラベルテクスチャの描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			Manager.DrawManager.Draw("IMAGE_ENTITLESCR_CATEGORY_KATAKANA", Locations.EntitleCategoryLabel);
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
				case 'カ':
					return 'ガ';
				case 'キ':
					return 'ギ';
				case 'ク':
					return 'グ';
				case 'ケ':
					return 'ゲ';
				case 'コ':
					return 'ゴ';

				case 'サ':
					return 'ザ';
				case 'シ':
					return 'ジ';
				case 'ス':
					return 'ズ';
				case 'セ':
					return 'ゼ';
				case 'ソ':
					return 'ゾ';

				case 'タ':
					return 'ダ';
				case 'チ':
					return 'ヂ';
				case 'ツ':
					return 'ヅ';
				case 'テ':
					return 'デ';
				case 'ト':
					return 'ド';

				case 'ハ':
					return 'バ';
				case 'ヒ':
					return 'ビ';
				case 'フ':
					return 'ブ';
				case 'ヘ':
					return 'ベ';
				case 'ホ':
					return 'ボ';

				case 'ウ':
					return 'ヴ';

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
				case 'ハ':
					return 'パ';
				case 'ヒ':
					return 'ピ';
				case 'フ':
					return 'プ';
				case 'ヘ':
					return 'ペ';
				case 'ホ':
					return 'ポ';

				default:
					return ' ';
			}
		}
	}

}
