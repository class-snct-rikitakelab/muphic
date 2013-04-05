using Muphic.CompositionScreenParts.Buttons;
using Muphic.Manager;

namespace Muphic.CompositionScreenParts
{
	/// <summary>
	/// 曲のテンポを管理するクラス。
	/// </summary>
	public class Tempo : Common.Screen
	{
		/// <summary>
		/// 親にあたる作曲画面クラス。
		/// </summary>
		public CompositionScreen Parent { get; private set; }


		/// <summary>
		/// 現在のテンポを表わす。
		/// </summary>
		public int TempoMode
		{
			get
			{
				return this.Parent.CurrentScoreData.Tempo;
			}
			set
			{
				if (value <= 0)
				{																			// テンポが0以下に設定された場合
					this.Parent.CurrentScoreData.Tempo = 0;									// テンポを0(最小)に設定
				}
				else if (value >= this.TempoTextureNames.Length - 1)
				{																			// テンポが表示テクスチャ数以上に設定された場合
					this.Parent.CurrentScoreData.Tempo = this.TempoTextureNames.Length - 1;	// テンポを表示テクスチャ数-1(最大)に設定
				}
				else
				{																			// 上記以外の場合
					this.Parent.CurrentScoreData.Tempo = value;								// テンポをそのまま設定
				}

				this.SettingVisibleTempoButtons();											// テンポに応じて操作ボタンの可視/不可視を切り替え
			}
		}

		/// <summary>
		/// テンポ表示テクスチャ
		/// </summary>
		public string[] TempoTextureNames { get; private set; }


		/// <summary>
		/// テンポアップ(右側)ボタン
		/// </summary>
		public TempoUpButton TempoUpButton { get; private set; }

		/// <summary>
		/// テンポダウン(左側)ボタン
		/// </summary>
		public TempoDownButton TempoDownButton { get; private set; }


		/// <summary>
		/// テンポの初期値を表す整数を取得する。通常は"ふつう"を表す 2 となる。
		/// </summary>
		public static int Default
		{
			get { return 2; }
		}


		/// <summary>
		/// テンポ管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="compositionScreen">親にあたる作曲画面。</param>
		public Tempo(CompositionScreen compositionScreen)
		{
			this.Parent = compositionScreen;

			this.Initialization();
		}


		/// <summary>
		/// 各構成部品のインスタンス化等を行う。
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();

			// テンポ表示テクスチャ名の登録
			this.TempoTextureNames = new string[] {
				"IMAGE_COMPOSITIONSCR_TEMPO_1",
				"IMAGE_COMPOSITIONSCR_TEMPO_2",
				"IMAGE_COMPOSITIONSCR_TEMPO_3",
				"IMAGE_COMPOSITIONSCR_TEMPO_4",
				"IMAGE_COMPOSITIONSCR_TEMPO_5"
			};

			// テンポ領域の登録
			System.Drawing.Point location = Locations.TempoArea.Location;
			DrawManager.Regist(this.ToString(), location, "IMAGE_COMPOSITIONSCR_TEMPO");

			// テンポ表示の登録
			location = Locations.TempoBg;
			DrawManager.Regist(this.ToString() + ".BG", location, "IMAGE_COMPOSITIONSCR_TEMPO_BG");
			DrawManager.Regist(this.ToString() + ".Label", location, TempoTextureNames);

			// テンポボタンのインスタンス化と登録
			this.TempoUpButton = new TempoUpButton(this);
			this.TempoDownButton = new TempoDownButton(this);
			this.PartsList.Add(this.TempoUpButton);
			this.PartsList.Add(this.TempoDownButton);
		}


		/// <summary>
		/// 現在のテンポ値の状態に応じ、テンポ操作ボタンの可視/不可視を切り替える。
		/// </summary>
		public void SettingVisibleTempoButtons()
		{
			this.SettingVisibleTempoButtons(true);
		}
		/// <summary>
		/// 現在のテンポ値の状態に応じ、テンポ操作ボタンの可視/不可視を切り替える。
		/// </summary>
		/// <param name="enabled">可視状態にする場合はtrue、不可視状態にする場合はfalse。</param>
		public void SettingVisibleTempoButtons(bool enabled)
		{
			if (!enabled)
			{												// 不可視に設定する場合は
				this.TempoUpButton.Visible = false;			// テンポアップボタン
				this.TempoDownButton.Visible = false;		// テンポダウンボタン 共に不可視化
			}
			else if (this.TempoMode == 0)
			{												// テンポが最低値ならば
				this.TempoDownButton.Visible = false;		// テンポダウンボタンを不可視化
				this.TempoUpButton.Visible = true;			// テンポアップボタンは可視化
			}
			else if (this.TempoMode == this.TempoTextureNames.Length - 1)
			{												// テンポが最大値ならば
				this.TempoUpButton.Visible = false;			// テンポアップボタンを不可視化
				this.TempoDownButton.Visible = true;		// テンポダウンボタンは可視化
			}
			else
			{												// 上記のいずれもマッチしない場合
				this.TempoUpButton.Visible = true;			// テンポアップボタン
				this.TempoDownButton.Visible = true;		// テンポダウンボタン 共に可視化
			}
		}


		/// <summary>
		/// テンポ表示の描画を行う
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.DrawParts(drawStatus);

			DrawManager.Draw(this.ToString() + ".BG", 0);					// 背景を描画
			DrawManager.Draw(this.ToString() + ".Label", this.TempoMode);	// TempoModeに対応したラベルを描画
		}


		/// <summary>
		/// マウスがテンポ領域から出た際に実行される。
		/// </summary>
		/// <remarks>
		/// ボタンから直接領域外に出ると、各ボタンの MouseLeave メソッドが呼ばれないことがあるために必要となる処置。
		/// </remarks>
		public override void MouseLeave()
		{
			base.MouseLeave();

			this.TempoUpButton.MouseLeave();
			this.TempoDownButton.MouseLeave();
		}
	}
}
