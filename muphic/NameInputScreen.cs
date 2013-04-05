using Muphic.Common;
using Muphic.Manager;
using Muphic.NameInputScreenParts;
using Muphic.NameInputScreenParts.Buttons;
using Muphic.PlayerWorks;

namespace Muphic
{
	/// <summary>
	/// プレイヤー名入力画面クラス。
	/// </summary>
	public class NameInputScreen : EntitleScreen
	{

		#region フィールドとプロパティ

		/// <summary>
		/// 親にあたるトップ画面。
		/// </summary>
		public TopScreen Parent { get; private set; }

		/// <summary>
		/// 決定ボタン。
		/// </summary>
		public NameDecisionButton NameDecisionButton { get; set; }

		/// <summary>
		/// プレイヤー名が入力されているかどうかを示す値を取得する。
		/// </summary>
		public static bool HasPlayerName
		{
			get
			{
				return !string.IsNullOrEmpty(ConfigurationManager.Current.Player1) || !string.IsNullOrEmpty(ConfigurationManager.Current.Player2);
			}
		}


		#region プレイヤー名

		/// <summary>
		/// プレイヤー1 の名前を取得する。実態は ConfigurationData クラスの Player1 プロパティ。
		/// </summary>
		public string Player1
		{
			get { return ConfigurationManager.Current.Player1; }
			private set { ConfigurationManager.Current.Player1 = value; }
		}

		/// <summary>
		/// プレイヤー2 の名前を取得する。実態は ConfigurationData クラスの Player2 プロパティ。
		/// </summary>
		public string Player2
		{
			get { return ConfigurationManager.Current.Player2; }
			private set { ConfigurationManager.Current.Player2 = value; }
		}

		/// <summary>
		/// 入力中の名前を示す文字列を取得または設定する。
		/// </summary>
		public override string Text
		{
			// EntitleScreen の Text プロパティに被せているので、通常の題名入力と同じように使用できる。
			// base.Text に値を通す必要は無い

			get
			{
				// 現在選択されているプレイヤーに応じて名前を返す
				if (this.InputMode == NameInputMode.Player1) return this.Player1;
				else if (this.InputMode == NameInputMode.Player2) return this.Player2;
				else return "!Unknown Player";
			}
			set
			{
				// 汎用題名入力画面の初期化対策
				if (this.Player1Title == null || this.Player2Title == null) return;

				// 現在選択されているプレイヤーに応じて名前を設定する。
				if (this.InputMode == NameInputMode.Player1)
				{
					if (value.Length > this.Player1Title.MaxLength) value = value.Substring(0, this.Player1Title.MaxLength);
					this.Player1Title.Text = this.Player1 = value;
					Tools.DebugTools.ConsolOutputMessage("プレイヤー1 名前更新", value, true);
				}
				else if (this.InputMode == NameInputMode.Player2)
				{
					if (value.Length > this.Player2Title.MaxLength) value = value.Substring(0, this.Player2Title.MaxLength);
					this.Player2Title.Text = this.Player2 = value;
					Tools.DebugTools.ConsolOutputMessage("プレイヤー2 名前更新", value, true);
				}

				this.SettingEnabledButtons();
			}
		}

		#endregion


		#region プレイヤー性別

		/// <summary>
		/// プレイヤー1 性別選択男児ボタン。
		/// </summary>
		public Player1BoyButton Player1BoyButton { get; private set; }

		/// <summary>
		/// プレイヤー1 性別選択女児ボタン。
		/// </summary>
		public Player1GirlButton Player1GirlButton { get; private set; }

		/// <summary>
		/// プレイヤー2 性別選択男児ボタン。
		/// </summary>
		public Player2BoyButton Player2BoyButton { get; private set; }

		/// <summary>
		/// プレイヤー2 性別選択女児ボタン。
		/// </summary>
		public Player2GirlButton Player2GirlButton { get; private set; }


		/// <summary>
		/// プレイヤー1 の性別を取得する。実態は ConfigurationData クラスの Player1Gender プロパティ。
		/// </summary>
		public AuthorGender Player1Gender
		{
			get { return Author.IntToAuthorGender(ConfigurationManager.Current.Player1Gender); }
			set { ConfigurationManager.Current.Player1Gender = Author.AuthorGenderToInt(value); }
		}

		/// <summary>
		/// プレイヤー2 の性別を取得する。実態は ConfigurationData クラスの Player2Gender プロパティ。
		/// </summary>
		public AuthorGender Player2Gender
		{
			get { return Author.IntToAuthorGender(ConfigurationManager.Current.Player2Gender); }
			set { ConfigurationManager.Current.Player2Gender = Author.AuthorGenderToInt(value); }
		}

		#endregion


		#region プレイヤー選択ボタンと名前表示

		/// <summary>
		/// プレイヤー1 の名前を表示する領域。
		/// </summary>
		public Title Player1Title { get; private set; }

		/// <summary>
		/// プレイヤー2 の名前を表示する領域。
		/// </summary>
		public Title Player2Title { get; private set; }

		/// <summary>
		/// プレイヤー1 の名前を選択するボタン。
		/// </summary>
		public Player1Button Player1Button { get; set; }

		/// <summary>
		/// プレイヤー2 の名前を選択するボタン。
		/// </summary>
		public Player2Button Player2Button { get; set; }

		#endregion


		#region プレイヤー識別子

		/// <summary>
		/// 入力中のプレイヤーを示す識別子を指定する。
		/// </summary>
		public enum NameInputMode
		{
			/// <summary>
			/// プレイヤー1 の名前を入力中。
			/// </summary>
			Player1,　

			/// <summary>
			/// プレイヤー2 の名前を入力中。
			/// </summary>
			Player2,
		}


		/// <summary>
		/// 入力中のプレイヤーを示す識別子。
		/// </summary>
		private NameInputMode __inputMode;

		/// <summary>
		/// 入力中のプレイヤーを示す識別子を取得または設定する。
		/// </summary>
		public NameInputMode InputMode
		{
			get
			{
				return this.__inputMode;
			}
			set
			{
				this.__inputMode = value;

				if (value == NameInputMode.Player1)
				{
					this.Player1Title.IsBlink = this.Player1Title.Enabled = this.Player1Title.IsJapaneseInput = this.Player1Button.Pressed = true;
					this.Player2Title.IsBlink = this.Player2Title.Enabled = this.Player2Title.IsJapaneseInput = this.Player2Button.Pressed = false;
					Tools.DebugTools.ConsolOutputMessage("Player1 名前入力", this.Player1, true);
				}
				else if (value == NameInputMode.Player2)
				{
					this.Player1Title.IsBlink = this.Player1Title.Enabled = this.Player1Title.IsJapaneseInput = this.Player1Button.Pressed = false;
					this.Player2Title.IsBlink = this.Player2Title.Enabled = this.Player2Title.IsJapaneseInput = this.Player2Button.Pressed = true;
					Tools.DebugTools.ConsolOutputMessage("Player2 名前入力", this.Player2, true);
				}
				else
				{
					this.Player1Title.IsBlink = this.Player1Title.Enabled = this.Player1Title.IsJapaneseInput = this.Player1Button.Pressed = false;
					this.Player2Title.IsBlink = this.Player2Title.Enabled = this.Player2Title.IsJapaneseInput = this.Player2Button.Pressed = false;
					Tools.DebugTools.ConsolOutputMessage("Unknown Player 名前入力", "Error", true);
				}

				this.SettingEnabledButtons();
				this.SetStateOfPlayerGenderSelectButtons();
				JpnLangInputManager.KeyClear();
			}
		}

		#endregion


		/// <summary>
		/// DrawManager への登録番号。クラス解放時に DrawManager に登録したキーとテクスチャを一括削除するため。
		/// </summary>
		private int RegistNum { get; set; }

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// プレイヤー名入力画面クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親に当たるトップ画面。</param>
		public NameInputScreen(TopScreen parent)
			: base(Settings.System.Default.PlayerName_MaxLength, "", "")
		{
			this.Parent = parent;
			this.Initialization();
			this.Title.Visible = false;

			this.InputMode = NameInputMode.Player1;
			this.Player1Title.Text = this.Player1;
			this.Player2Title.Text = this.Player2;
		}


		/// <summary>
		/// プレイヤー名入力画面を構成する各パーツのインスタンス化等を行う。
		/// </summary>
		protected new void Initialization()
		{
			// 登録開始を管理クラスへ通知
			this.RegistNum = DrawManager.BeginRegist(false);

			#region 部品のインスタンス化

			this.NameDecisionButton = new NameDecisionButton(this);
			this.Player1Title = new Title(this.ToString() + ".Player1", Locations.Player1NameArea, Settings.System.Default.PlayerName_MaxLength);
			this.Player2Title = new Title(this.ToString() + ".Player2", Locations.Player2NameArea, Settings.System.Default.PlayerName_MaxLength);
			this.Player1Button = new Player1Button(this);
			this.Player2Button = new Player2Button(this);
			this.Player1BoyButton = new Player1BoyButton(this);
			this.Player1GirlButton = new Player1GirlButton(this);
			this.Player2BoyButton = new Player2BoyButton(this);
			this.Player2GirlButton = new Player2GirlButton(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.NameDecisionButton);
			this.PartsList.Add(this.Player1Title);
			this.PartsList.Add(this.Player2Title);
			this.PartsList.Add(this.Player1Button);
			this.PartsList.Add(this.Player2Button);
			this.PartsList.Add(this.Player1BoyButton);
			this.PartsList.Add(this.Player1GirlButton);
			this.PartsList.Add(this.Player2BoyButton);
			this.PartsList.Add(this.Player2GirlButton);

			#endregion

			// 登録終了を管理クラスへ通知
			DrawManager.EndRegist();
		}

		#endregion 


		#region プレイヤー性別関連メソッド群


		/// <summary>
		/// 入力中のプレイヤーや性別に応じ、プレイヤー性別選択ボタンの状態を設定する。
		/// </summary>
		public void SetStateOfPlayerGenderSelectButtons()
		{
			if (this.InputMode == NameInputMode.Player1)
			{
				// 入力モードがプレイヤー1 の場合

				// プレイヤー1 のボタンを表示する
				this.Player1BoyButton.Visible = this.Player1GirlButton.Visible = true;
				this.Player1BoyButton.Enabled = this.Player1GirlButton.Enabled = true;
				this.Player1BoyButton.Pressed = this.Player1GirlButton.Pressed = false;

				// プレイヤー1 の性別が決まっていれば、その性別のボタンは state=2 で表示する
				if (this.Player1Gender == AuthorGender.Boy) this.Player1BoyButton.Pressed = true;
				else if (this.Player1Gender == AuthorGender.Girl) this.Player1GirlButton.Pressed = true;

				// プレイヤー2 のボタンは非表示にする
				this.Player2BoyButton.Visible = this.Player2GirlButton.Visible = false;

				// ただし、プレイヤー2 の性別が決まっていれば、その性別のボタンは無効状態で表示する
				if (this.Player2Gender == AuthorGender.Boy)
				{
					this.Player2BoyButton.Visible = true;
					this.Player2BoyButton.Enabled = false;
					this.Player2BoyButton.Pressed = true;
				}
				else if (this.Player2Gender == AuthorGender.Girl)
				{
					this.Player2GirlButton.Visible = true;
					this.Player2GirlButton.Enabled = false;
					this.Player2GirlButton.Pressed = true;
				}
			}
			else if (this.InputMode == NameInputMode.Player2)
			{
				// 入力モードがプレイヤー2 の場合

				// プレイヤー2 のボタンを表示する
				this.Player2BoyButton.Visible = this.Player2GirlButton.Visible = true;
				this.Player2BoyButton.Enabled = this.Player2GirlButton.Enabled = true;
				this.Player2BoyButton.Pressed = this.Player2GirlButton.Pressed = false;

				// プレイヤー2 の性別が決まっていれば、その性別のボタンは state=2 で表示する
				if (this.Player2Gender == AuthorGender.Boy) this.Player2BoyButton.Pressed = true;
				else if (this.Player2Gender == AuthorGender.Girl) this.Player2GirlButton.Pressed = true;

				// プレイヤー1 のボタンは非表示にする
				this.Player1BoyButton.Visible = this.Player1GirlButton.Visible = false;

				// ただし、プレイヤー2 の性別が決まっていれば、その性別のボタンは無効状態で表示する
				if (this.Player1Gender == AuthorGender.Boy)
				{
					this.Player1BoyButton.Visible = true;
					this.Player1BoyButton.Enabled = false;
					this.Player1BoyButton.Pressed = true;
				}
				else if (this.Player1Gender == AuthorGender.Girl)
				{
					this.Player1GirlButton.Visible = true;
					this.Player1GirlButton.Enabled = false;
					this.Player1GirlButton.Pressed = true;
				}
			}
			else
			{
				this.Player1BoyButton.Visible = this.Player1GirlButton.Visible = false;
				this.Player2BoyButton.Visible = this.Player2GirlButton.Visible = false;
			}
		}

		#endregion


		#region 描画

		/// <summary>
		/// プレイヤー名入力画面の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			// "キミの名前を入力してね"的な画像を描画する
			//DrawManager.Draw("IMAGE_ENTITLESCR_PLAYERINPUTTITLE", Locations.PlayerInputTitle);
		}

		#endregion


		#region キー操作

		/// <summary>
		/// 学習者名入力画面上でキー入力を行う。
		/// </summary>
		/// <param name="keyStatus"></param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			base.KeyDown(keyStatus);

			// タブキーで入力者切り替え
			if (keyStatus.KeyCode == Microsoft.DirectX.DirectInput.Key.Tab)
			{
				switch (this.InputMode)
				{
					case NameInputMode.Player1:
						this.InputMode = NameInputMode.Player2;
						break;
					case NameInputMode.Player2:
						this.InputMode = NameInputMode.Player1;
						break;
				}
			}
		}


		#endregion


		#region 解放

		/// <summary>
		/// プレイヤー名入力画面で使用したリソースを開放し、登録したキーとテクスチャ名を削除する。
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();

			DrawManager.Delete(this.RegistNum);
		}

		#endregion
	}
}
