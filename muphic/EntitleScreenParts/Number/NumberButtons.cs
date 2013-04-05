
namespace Muphic.EntitleScreenParts.Number
{
	/// <summary>
	/// 数字・記号ボタン群の管理を行う。
	/// </summary>
	public class NumberButtons : Common.Screen
	{
		/// <summary>
		/// 親にあたる題名入力画面
		/// </summary>
		public EntitleScreen Parent { get; private set; }


		/// <summary>
		/// 数字・記号ボタン群管理クラスの新しインスタンスを初期化する。
		/// </summary>
		/// <param name="entitleScreen">親にあたる題名入力画面。</param>
		public NumberButtons(EntitleScreen entitleScreen)
		{
			this.Parent = entitleScreen;

			// 数字・記号ボタンの生成と登録
			NumberButton[,] numberButton = new NumberButton[6, 13];

			// ==============================
			//      部品のインスタンス化
			// ==============================
			numberButton[0, 00] = new NumberButton(this, "１", 0, 0);
			numberButton[0, 01] = new NumberButton(this, "２", 0, 1);
			numberButton[0, 02] = new NumberButton(this, "３", 0, 2);
			numberButton[0, 03] = new NumberButton(this, "４", 0, 3);
			numberButton[0, 04] = new NumberButton(this, "５", 0, 4);
			numberButton[0, 05] = new NumberButton(this, "６", 0, 5);
			numberButton[0, 06] = new NumberButton(this, "７", 0, 6);
			numberButton[0, 07] = new NumberButton(this, "８", 0, 7);
			numberButton[0, 08] = new NumberButton(this, "９", 0, 8);
			numberButton[0, 09] = new NumberButton(this, "０", 0, 9);
			numberButton[2, 00] = new NumberButton(this, "！", 2, 0);
			numberButton[2, 01] = new NumberButton(this, "？", 2, 1);
			numberButton[2, 02] = new NumberButton(this, "・", 2, 2);
			numberButton[2, 03] = new NumberButton(this, "　", 2, 3);
			numberButton[2, 04] = new NumberButton(this, "＋", 2, 4);
			numberButton[2, 05] = new NumberButton(this, "－", 2, 5);
			numberButton[2, 06] = new NumberButton(this, "×", 2, 6);
			numberButton[2, 07] = new NumberButton(this, "÷", 2, 7);
			numberButton[2, 08] = new NumberButton(this, "＝", 2, 8);
			numberButton[2, 09] = new NumberButton(this, "≠", 2, 9);
			numberButton[2, 10] = new NumberButton(this, "＜", 2, 10);
			numberButton[2, 11] = new NumberButton(this, "＞", 2, 11);
			numberButton[2, 12] = new NumberButton(this, "∑", 2, 12);
			numberButton[3, 00] = new NumberButton(this, "％", 3, 0);
			numberButton[3, 01] = new NumberButton(this, "＆", 3, 1);
			numberButton[3, 02] = new NumberButton(this, "￥", 3, 2);
			numberButton[3, 03] = new NumberButton(this, "＄", 3, 3);
			numberButton[3, 04] = new NumberButton(this, "＠", 3, 4);
			numberButton[3, 05] = new NumberButton(this, "＊", 3, 5);
			numberButton[3, 06] = new NumberButton(this, "※", 3, 6);
			numberButton[3, 07] = new NumberButton(this, "～", 3, 7);
			numberButton[3, 08] = new NumberButton(this, "／", 3, 8);
			numberButton[3, 09] = new NumberButton(this, "＼", 3, 9);
			numberButton[3, 10] = new NumberButton(this, "♪", 3, 10);
			numberButton[3, 11] = new NumberButton(this, "＃", 3, 11);
			numberButton[3, 12] = new NumberButton(this, "♭", 3, 12);
			numberButton[4, 00] = new NumberButton(this, "「", 4, 0);
			numberButton[4, 01] = new NumberButton(this, "」", 4, 1);
			numberButton[4, 02] = new NumberButton(this, "（", 4, 2);
			numberButton[4, 03] = new NumberButton(this, "）", 4, 3);
			numberButton[4, 04] = new NumberButton(this, "【", 4, 4);
			numberButton[4, 05] = new NumberButton(this, "】", 4, 5);
			numberButton[4, 06] = new NumberButton(this, "≪", 4, 6);
			numberButton[4, 07] = new NumberButton(this, "≫", 4, 7);
			numberButton[4, 08] = new NumberButton(this, "↑", 4, 8);
			numberButton[4, 09] = new NumberButton(this, "↓", 4, 9);
			numberButton[4, 10] = new NumberButton(this, "←", 4, 10);
			numberButton[4, 11] = new NumberButton(this, "→", 4, 11);
			numberButton[4, 12] = new NumberButton(this, "◎", 4, 12);
			numberButton[5, 00] = new NumberButton(this, "●", 5, 0);
			numberButton[5, 01] = new NumberButton(this, "○", 5, 1);
			numberButton[5, 02] = new NumberButton(this, "▲", 5, 2);
			numberButton[5, 03] = new NumberButton(this, "△", 5, 3);
			numberButton[5, 04] = new NumberButton(this, "▼", 5, 4);
			numberButton[5, 05] = new NumberButton(this, "▽", 5, 5);
			numberButton[5, 06] = new NumberButton(this, "■", 5, 6);
			numberButton[5, 07] = new NumberButton(this, "□", 5, 7);
			numberButton[5, 08] = new NumberButton(this, "★", 5, 8);
			numberButton[5, 09] = new NumberButton(this, "☆", 5, 9);
			numberButton[5, 10] = new NumberButton(this, "◆", 5, 10);
			numberButton[5, 11] = new NumberButton(this, "◇", 5, 11);

			// ==============================
			//      部品のリストへの登録
			// ==============================
			foreach (NumberButton button in numberButton)
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
		/// 数字・記号ボタンの有効/無効の設定を行う。
		/// </summary>
		public void SetEnabled()
		{
			if (this.Parent.Text.Length >= this.Parent.MaxLength)
			{
				// 題名の文字数が最大値以上になっていた場合
				// 全数字・記号ボタンを無効化する
				foreach (NumberButton button in PartsList)
				{
					button.Enabled = false;
				}
			}
			else
			{
				// 題名の文字数が最大値未満だった場合
				// 全数字・記号ボタンを有効にする
				foreach (NumberButton button in PartsList)
				{
					button.Enabled = true;
				}
			}
		}


		/// <summary>
		/// 数字・記号ボタン群とカテゴリラベルテクスチャの描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			Manager.DrawManager.Draw("IMAGE_ENTITLESCR_CATEGORY_NUMBER", Locations.EntitleCategoryLabel);
		}
	}
}
