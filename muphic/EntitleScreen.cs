using Muphic.Common;
using Muphic.EntitleScreenParts;
using Muphic.EntitleScreenParts.Alphabet;
using Muphic.EntitleScreenParts.Hiragana;
using Muphic.EntitleScreenParts.Katakana;
using Muphic.EntitleScreenParts.Number;
using Muphic.EntitleScreenParts.OtherButtons;
using Muphic.Manager;
using Microsoft.DirectX.DirectInput;

namespace Muphic
{
	/// <summary>
	/// 題名入力画面で入力する文字種を示す識別子を指定する。
	/// </summary>
	public enum EntitleCategoryMode
	{
		/// <summary>
		/// アルファベット入力
		/// </summary>
		Alphabet,

		/// <summary>
		/// 平仮名入力
		/// </summary>
		Hiragana,

		/// <summary>
		/// 片仮名入力
		/// </summary>
		Katakana,

		/// <summary>
		/// 数字・記号入力
		/// </summary>
		Number,
	}


	/// <summary>
	/// 汎用題名入力画面クラス
	/// <para>各部での曲名や物語名等の文字入力画面は、このクラスを継承して作成する。</para>
	/// </summary>
	/// <remeremarks>
	/// このクラスは抽象クラスであり、親にあたる Parent プロパティや題名を決定し親画面に戻るボタンは定義されていない。
	/// <para>そのため、それらは継承先のクラスで新たに定義する必要がある。</para>
	/// </remeremarks>
	public abstract class EntitleScreen : Screen, System.IDisposable
	{

		#region フィールドとそのプロパティたち

		#region 題名とその説明

		/// <summary>
		/// 背景と共に題名の文字列が表示される領域。入力された題名はこのクラスが保持する。
		/// </summary>
		protected Title Title { get; set; }

		/// <summary>
		/// 入力される題名の最大文字数を表わす整数。
		/// </summary>
		public int MaxLength { get; private set; }

		/// <summary>
		/// 入力された題名を示す文字列を取得または設定する。
		/// </summary>
		public virtual string Text
		{
			get
			{
				return this.Title.Text;
			}
			set
			{
				this.Title.Text = value;
				Tools.DebugTools.ConsolOutputMessage("題名更新", this.Title.Text, true);
				this.SettingEnabledButtons();
			}
		}

		/// <summary>
		/// 入力する題名の説明文を取得する。
		/// </summary>
		public string ExplainText { get; private set; }

		#endregion


		#region 文字種

		/// <summary>
		/// 入力する文字種を表す EntitleMode 列挙型。
		/// <para>EntitleMode プロパティを使用すること。</para>
		/// </summary>
		private EntitleCategoryMode __entitleCategoryMode;

		/// <summary>
		/// 入力する文字種を表す EntitleMode 列挙型。
		/// </summary>
		public EntitleCategoryMode EntitleCategoryMode
		{
			get
			{
				return this.__entitleCategoryMode;
			}
			set
			{
				this.__entitleCategoryMode = value;

				// 全ボタン群管理クラスを不可視化
				this.AlphabetButtons.Visible = false;
				this.HiraganaButtons.Visible = false;
				this.KatakanaButtons.Visible = false;
				this.NumberButtons.Visible = false;

				// 全カテゴリ選択ボタンを有効化
				this.AlphabetCategoryButton.Enabled = true;
				this.HiraganaCategoryButton.Enabled = true;
				this.KatakanaCategoryButton.Enabled = true;
				this.NumberCategoryButton.Enabled = true;

				// 設定されたカテゴリに応じ、
				// ボタン群管理クラスの可視性とカテゴリ選択ボタンの有効性を設定
				switch (value)
				{
					case EntitleCategoryMode.Alphabet:
						this.AlphabetButtons.Visible = true;
						this.AlphabetCategoryButton.Enabled = false;
						break;

					case EntitleCategoryMode.Hiragana:
						this.HiraganaButtons.Visible = true;
						this.HiraganaCategoryButton.Enabled = false;
						break;

					case EntitleCategoryMode.Katakana:
						this.KatakanaButtons.Visible = true;
						this.KatakanaCategoryButton.Enabled = false;
						break;

					case EntitleCategoryMode.Number:
						this.NumberButtons.Visible = true;
						this.NumberCategoryButton.Enabled = false;
						break;

					default:
						break;
				}

				this.SettingEnabledButtons();
				JpnLangInputManager.KeyClear();
			}
		}


		/// <summary>
		/// 題名に句読点を使用できるかどうかを示す値を取得する。
		/// </summary>
		public bool canUseInterpunction { get; private set; }

		#endregion


		#region ボタン

		/// <summary>
		/// "かな"カテゴリボタン
		/// </summary>
		public HiraganaCategoryButton HiraganaCategoryButton { get; private set; }

		/// <summary>
		/// "カナ"カテゴリボタン
		/// </summary>
		public KatakanaCategoryButton KatakanaCategoryButton { get; private set; }

		/// <summary>
		/// "ABC"カテゴリボタン
		/// </summary>
		public AlphabetCategoryButton AlphabetCategoryButton { get; private set; }

		/// <summary>
		/// "１・☆"カテゴリボタン
		/// </summary>
		public NumberCategoryButton NumberCategoryButton { get; private set; }


		/// <summary>
		/// "けす"ボタン
		/// </summary>
		public SingleDeleteButton SingleDeleteButton { get; private set; }

		/// <summary>
		/// "すべてけす"ボタン
		/// </summary>
		public AllDeleteButton AllDeleteButton { get; private set; }

		#endregion


		#region ボタン群管理クラス

		/// <summary>
		/// 平仮名ボタン群管理クラス
		/// </summary>
		public HiraganaButtons HiraganaButtons { get; private set; }

		/// <summary>
		/// 片仮名ボタン群管理クラス
		/// </summary>
		public KatakanaButtons KatakanaButtons { get; private set; }

		/// <summary>
		/// アルファベットボタン群管理クラス
		/// </summary>
		public AlphabetButtons AlphabetButtons { get; private set; }

		/// <summary>
		/// 数字・記号ボタン群管理クラス
		/// </summary>
		public NumberButtons NumberButtons { get; private set; }

		#endregion

		#endregion


		#region コンストラクタ/初期化

		/// <summary>
		/// 汎用題名入力画面を初期化する。
		/// </summary>
		/// <param name="maxLength">題名の最大文字数。</param>
		protected EntitleScreen(int maxLength) : this(maxLength, "", "", false)
		{
		}
		
		/// <summary>
		/// 汎用題名入力画面を初期化する。
		/// </summary>
		/// <param name="maxLength">題名の最大文字数。</param>
		/// <param name="defaultTitle">初期状態で表示する題名。</param>
		/// <param name="explainText">入力する題名の説明文。</param>
		protected EntitleScreen(int maxLength, string defaultTitle, string explainText)
			: this(maxLength, defaultTitle, explainText, false)
		{
		}

		/// <summary>
		/// 汎用題名入力画面を初期化する。
		/// </summary>
		/// <param name="maxLength">題名の最大文字数。</param>
		/// <param name="defaultTitle">初期状態で表示する題名。</param>
		/// <param name="explainText">入力する題名の説明文。</param>
		/// <param name="canUseInterpunction">句読点の使用を許可する場合は true、それ以外は false。</param>
		protected EntitleScreen(int maxLength, string defaultTitle, string explainText, bool canUseInterpunction)
		{
			this.MaxLength = maxLength;
			this.Initialization();

			this.Title.IsBlink = true;
			this.Title.IsJapaneseInput = true;
			this.ExplainText = explainText;
			this.canUseInterpunction = canUseInterpunction;
			this.Text = defaultTitle;									// 初期状態で表示する題名を設定
			this.EntitleCategoryMode = EntitleCategoryMode.Hiragana;	// 初期状態で表示するカテゴリは平仮名

			JpnLangInputManager.KeyClear();
		}

		
		/// <summary>
		/// 楽譜画面を構成する各部品のインスタンス化などを行う。
		/// </summary>
		protected override void Initialization()
		{
			// 登録開始
			base.Initialization(Settings.ResourceNames.EntitleScreenImages);

			#region 統合画像の読み込み

			this.LoadImageFiles();

			#endregion

			#region 部品のインスタンス化

			this.HiraganaButtons = new HiraganaButtons(this);
			this.KatakanaButtons = new KatakanaButtons(this);
			this.AlphabetButtons = new AlphabetButtons(this);
			this.NumberButtons = new NumberButtons(this);
			this.HiraganaCategoryButton = new HiraganaCategoryButton(this);
			this.KatakanaCategoryButton = new KatakanaCategoryButton(this);
			this.AlphabetCategoryButton = new AlphabetCategoryButton(this);
			this.NumberCategoryButton = new NumberCategoryButton(this);
			this.Title = new Title(this.ToString(), Locations.Title, this.MaxLength);
			this.SingleDeleteButton = new SingleDeleteButton(this);
			this.AllDeleteButton = new AllDeleteButton(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.HiraganaButtons);
			this.PartsList.Add(this.KatakanaButtons);
			this.PartsList.Add(this.AlphabetButtons);
			this.PartsList.Add(this.NumberButtons);
			this.PartsList.Add(this.HiraganaCategoryButton);
			this.PartsList.Add(this.KatakanaCategoryButton);
			this.PartsList.Add(this.AlphabetCategoryButton);
			this.PartsList.Add(this.NumberCategoryButton);
			this.PartsList.Add(this.Title);
			this.PartsList.Add(this.SingleDeleteButton);
			this.PartsList.Add(this.AllDeleteButton);

			#endregion

			#region テクスチャと座標の登録

			Manager.DrawManager.Regist(this.ToString(), Locations.EntitleScreenArea.Location, "IMAGE_ENTITLESCR_BG");

			#endregion

			// 登録終了
			DrawManager.EndRegist();
		}


		#endregion


		#region 題名操作メソッド群

		/// <summary>
		/// 題名に文字を追加する。
		/// </summary>
		/// <param name="character">追加する文字。</param>
		public void Add(string character)
		{
			this.Text += character;
		}

		/// <summary>
		/// 題名の末尾１文字を置き換える。
		/// </summary>
		/// <param name="character">置き換えにる文字。</param>
		/// <returns>置き換えに成功した場合は true、それ以外は false。</returns>
		public bool Replace(string character)
		{
			if (this.Text.Length > 0)
			{
				this.Text = this.Text.Remove(this.Text.Length - 1) + character;
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 題名の末尾１文字を削除する。
		/// </summary>
		/// <returns>削除に成功した場合は true、それ以外は false。</returns>
		public bool SingleDelete()
		{
			// 未確定ローマ字から削除できる場合は削除し、成功扱いで終了
			if (JpnLangInputManager.KeyDelete()) return true;

			if (this.Text.Length > 0)
			{
				this.Text = this.Text.Remove(this.Text.Length - 1);
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 題名を空にする。
		/// </summary>
		public void AllDelete()
		{
			this.Text = "";
		}

		#endregion


		#region ボタン操作メソッド

		/// <summary>
		/// 題名の文字数等に応じて、各種ボタンの有効/無効の切り替えを行う。
		/// </summary>
		public void SettingEnabledButtons()
		{
			// 入力カテゴリに応じて文字ボタンの有効/無効の切り替え
			// 各ボタン群管理クラスに任せる
			switch (this.EntitleCategoryMode)
			{
				case EntitleCategoryMode.Alphabet:
					this.AlphabetButtons.SetEnabled();
					break;

				case EntitleCategoryMode.Hiragana:
					this.HiraganaButtons.SetEnabled();
					break;

				case EntitleCategoryMode.Katakana:
					this.KatakanaButtons.SetEnabled();
					break;

				case EntitleCategoryMode.Number:
					this.NumberButtons.SetEnabled();
					break;

				default:
					break;
			}

			// 文字数に応じた他のボタンの有効/無効の切り替え
			if (this.Text.Length == 0)
			{
				// 0文字であれば削除ボタン無効
				this.SingleDeleteButton.Enabled = false;
				this.AllDeleteButton.Enabled = false;
			}
			else
			{
				// 1文字以上あれば削除ボタン有効
				this.SingleDeleteButton.Enabled = true;
				this.AllDeleteButton.Enabled = true;
			}
		}


		#endregion


		#region 描画

		/// <summary>
		/// 汎用題名入力画面を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			StringManager.Draw(this.ExplainText, Locations.ExplainText);
		}

		#endregion


		#region キー入力

		/// <summary>
		/// 汎用題名入力画面上でキー入力する。
		/// </summary>
		/// <param name="keyStatus"></param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			base.KeyDown(keyStatus);

			#region Apps キーが押されたら、入力文字種をスイッチする (半ば隠し機能のようなもの)

			if (keyStatus.KeyCode == Key.Apps)
			{
				switch (this.EntitleCategoryMode)
				{
					case EntitleCategoryMode.Hiragana:
						this.EntitleCategoryMode = EntitleCategoryMode.Katakana;
						return;
					case EntitleCategoryMode.Katakana:
						this.EntitleCategoryMode = EntitleCategoryMode.Alphabet;
						return;
					case EntitleCategoryMode.Alphabet:
						this.EntitleCategoryMode = EntitleCategoryMode.Number;
						return;
					case EntitleCategoryMode.Number:
						this.EntitleCategoryMode = EntitleCategoryMode.Hiragana;
						return;
				}
			}

			#endregion

			#region 入力文字種がひらがなもしくはカタカナだった場合、キーボードから日本語入力処理

			if (this.EntitleCategoryMode == EntitleCategoryMode.Hiragana || this.EntitleCategoryMode == EntitleCategoryMode.Katakana)
			{
				// かなモードの判定 (予定)

				if(keyStatus.KeyCode == Key.Return || keyStatus.KeyCode == Key.NumPadEnter)
				{
					// Enter キーだった場合、未確定ローマ字を確定させる
					this.Text += JpnLangInputManager.NowKeys;
					JpnLangInputManager.KeyClear();
				}
				else if (keyStatus.KeyCode == Key.BackSpace)
				{
					// Backspace キーだった場合、未確定ローマ字から1字削除。削除できなければ、確定題名から1文字削除
					if (!JpnLangInputManager.KeyDelete() && this.Text.Length >= 1) this.Text = this.Text.Remove(this.Text.Length - 1);
				}
				else if(this.Text.Length < this.MaxLength)
				{
					// 上記以外で最大文字数に達してなければ、キー入力を受け付け
					// 入力文字種がカタカナであれば、カタカナに変換して題名に追加
					string temp = JpnLangInputManager.KeyInput(keyStatus.KeyCode, keyStatus.Shift);
					this.Text += (this.EntitleCategoryMode == EntitleCategoryMode.Hiragana) ? temp : JpnLangInputManager.HiraganaToKatakana(temp);
				}
			}

			#endregion

			#region 入力文字種がアルファベットだった場合、キーボードからアルファベット入力処理

			else if (this.EntitleCategoryMode == EntitleCategoryMode.Alphabet || this.EntitleCategoryMode == EntitleCategoryMode.Number)
			{
				if (keyStatus.KeyCode == Key.BackSpace)
				{
					// BackspaceKey だった場合、題名から1文字削除
					if (this.Text.Length >= 1) this.Text = this.Text.Remove(this.Text.Length - 1);
				}
				else
				{
					// BackspaceKey でなければ、キー入力を受け付け
					// まずアルファベットに該当するかをチェックし、該当しなければ記号に該当するかをチェック
					// 最終的に題名の末尾に確定文字列として追加される
					string add = Tools.CommonTools.KeyToAlphabet(keyStatus.KeyCode, keyStatus.Shift);
					if (string.IsNullOrEmpty(add)) add = Tools.CommonTools.KeyToSymbol(keyStatus.KeyCode, keyStatus.Shift, false);
					this.Text += add;
				}
			}

			#endregion
		}

		#endregion


		#region 解放

		/// <summary>
		/// 作曲画面で使用した統合画像を解放する。
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
		}

		#endregion

	}
}
