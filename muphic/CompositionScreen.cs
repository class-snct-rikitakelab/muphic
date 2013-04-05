using System;

using Muphic.Common;
using Muphic.CompositionScreenParts;
using Muphic.CompositionScreenParts.Buttons;
using Muphic.CompositionScreenParts.Dialogs;
using Muphic.CompositionScreenParts.SubScreens;
using Muphic.PlayerWorks;
using Muphic.Manager;
using Muphic.Tools.IO;

namespace Muphic
{

	/// <summary>
	/// 作曲画面がどのモードで使用されているかを示す識別子を指定する。
	/// </summary>
	public enum CompositionMode
	{
		/// <summary>
		/// ひとりでおんがくモード。
		/// </summary>
		OneScreen,

		/// <summary>
		/// ものがたりおんがくモード。
		/// </summary>
		StoryScreen,

		/// <summary>
		/// 未指定。
		/// </summary>
		Other,
	}


	/// <summary>
	/// 作曲画面で、現在表示すべき画面を示す識別子を指定する。
	/// </summary>
	public enum CompositionScreenMode
	{
		/// <summary>
		/// 作曲画面。
		/// </summary>
		CompositionScreen,

		/// <summary>
		/// 楽譜画面。
		/// </summary>
		ScoreScreen,

		/// <summary>
		/// 題名入力画面。
		/// </summary>
		EntitleScreen,

		/// <summary>
		/// 新規作成ダイアログ。
		/// </summary>
		CreateNewDialog,

		/// <summary>
		/// ファイル保存ダイアログ。
		/// </summary>
		ScoreSaveDialog,

		/// <summary>
		/// ファイル読込ダイアログ。
		/// </summary>
		ScoreLoadDialog,
	}


	/// <summary>
	/// 汎用作曲画面クラス
	/// <para>各部の作曲画面 (ひとりでおんがくモード・ものがたりおんがくモード) は、このクラスを継承して作成する。</para>
	/// </summary>
	/// <remeremarks>
	/// このクラスは抽象クラスであり、親にあたる Parent プロパティや親画面に戻るボタンは定義されていない。
	/// <para>そのため、それらは継承先のクラスで新たに定義する必要がある。</para>
	/// </remeremarks>
	public abstract class CompositionScreen : Screen, IDisposable
	{

		#region フィールドとそのプロパティ

		/// <summary>
		/// 子となる Muphic.Common.Screen。
		/// </summary>
		public Screen SubScreen { get; private set; }


		/// <summary>
		/// 作曲部主クラス。
		/// </summary>
		public CompositionMain CompositionMain { get; private set; }

		/// <summary>
		/// 動物ボタン管理クラス。
		/// </summary>
		public AnimalButtons AnimalButtons { get; private set; }

		/// <summary>
		/// 追尾クラス。
		/// </summary>
		public AnimalHoming AnimalHoming { get; private set; }

		/// <summary>
		/// テンポ管理クラス。
		/// </summary>
		public Tempo Tempo { get; private set; }

		/// <summary>
		/// 楽譜のタイトル。
		/// </summary>
		public Title ScoreTitle { get; private set; }
		//public ScoreTitle ScoreTitle { get; private set; }


		#region ダイアログ

		/// <summary>
		/// 新規作成の確認ダイアログ
		/// </summary>
		public CreateNewDialog CreateNewDialog { get; private set; }

		/// <summary>
		/// 楽譜保存ダイアログ
		/// </summary>
		public ScoreSaveDialog ScoreSaveDialog { get; private set; }

		/// <summary>
		/// 楽譜読込ダイアログ
		/// </summary>
		public ScoreLoadDialog ScoreLoadDialog { get; private set; }

		#endregion


		#region ボタン

		/// <summary>
		/// "はじめから"ボタン
		/// </summary>
		public PlayFirstButton PlayFirstButton { get; private set; }

		/// <summary>
		/// "すすむ"ボタン
		/// </summary>
		public PlayContinueButton PlayContinueButton { get; private set; }

		/// <summary>
		/// "あたらしくつくる"ボタン
		/// </summary>
		public CreateNewButton CreateNewButton { get; private set; }

		/// <summary>
		/// "だいめい"ボタン
		/// </summary>
		public TitleButton TitleButton { get; private set; }

		/// <summary>
		/// "のこす"ボタン
		/// </summary>
		public ScoreSaveButton ScoreSaveButton { get; private set; }

		/// <summary>
		/// "よびだす"ボタン
		/// </summary>
		public ScoreLoadButton ScoreLoadButton { get; private set; }

		/// <summary>
		/// "がくふ"ボタン
		/// </summary>
		public ScoreButton ScoreButton { get; private set; }

		#endregion


		/// <summary>
		/// 現在編集を行っている楽譜データを取得 (または設定) する。
		/// </summary>
		public ScoreData CurrentScoreData { get; private set; }

		/// <summary>
		/// この作曲画面が muphic のモードで使用されているかを示す。
		/// </summary>
		private readonly CompositionMode __compositionMode;

		/// <summary>
		/// この作曲画面が muphic のモードで使用されているかを示す識別子を取得する。
		/// </summary>
		public CompositionMode CompositionMode
		{
			get { return this.__compositionMode; }
		}

		///// <summary>
		///// DrawManager への登録番号。
		///// <para>クラス解放時に DrawManager に登録したキーとテクスチャを一括削除する際に使用する。</para>
		///// </summary>
		//private int RegistNum { get; set; }


		/// <summary>
		/// 表示する画面を表わす CompositionScreenMode 列挙型。
		/// </summary>
		private CompositionScreenMode __screenMode;

		/// <summary>
		/// ScreenMode 列挙型により、作曲画面が表示すべき画面を指定する。
		/// </summary>
		public CompositionScreenMode ScreenMode
		{
			get
			{
				return this.__screenMode;
			}
			set
			{
				switch (value)
				{
					case CompositionScreenMode.CompositionScreen:								// 作曲画面が選択された場合
						if (this.__screenMode == CompositionScreenMode.EntitleScreen)
						{																		// 前の画面が題名入力画面であれば
							((ScoreEntitleScreen)this.SubScreen).Dispose();						// 題名入力画面の解放
							LogFileManager.WriteLine(											// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Tools.CommonTools.GetResourceMessage(Properties.Resources.Msg_CompositionScr_EndEntitle, this.ScoreTitle.Text)
							);
						}
						else if (this.__screenMode == CompositionScreenMode.ScoreScreen)
						{																		// 前の画面が楽譜画面であれば
							((ScoreScreen)this.SubScreen).Dispose();							// 楽譜画面の解放
							LogFileManager.WriteLine(											// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Tools.CommonTools.GetResourceMessage(Properties.Resources.Msg_CompositionScr_EndEntitle, this.ScoreTitle.Text)
							);
						}
						this.SubScreen = null;													// 子スクリーンnull
						break;

					case CompositionScreenMode.ScoreScreen:										// 楽譜画面が選択された場合
						this.SubScreen = new ScoreScreen(this);									// 楽譜画面を生成
						LogFileManager.WriteLine(												// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Properties.Resources.Msg_CompositionScr_StartShowScore
						);
						break;

					case CompositionScreenMode.EntitleScreen:									// 題名入力画面が選択された場合
						this.SubScreen = new ScoreEntitleScreen(this, this.ScoreTitle.Text);	// 題名入力画面を生成(頻度から考えて作曲画面のテクスチャ解放は必要ないと思われる)
						LogFileManager.WriteLine(												// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Properties.Resources.Msg_CompositionScr_StartEntitle
						);
						break;

					case CompositionScreenMode.CreateNewDialog:
						break;

					case CompositionScreenMode.ScoreLoadDialog:									// 読込ダイアログ表示時は、ファイル名一覧の更新を行う
						this.ScoreLoadDialog.SelectArea.Initialization(XmlFileReader.GetScoreDataList());
						break;

					case CompositionScreenMode.ScoreSaveDialog:									// 保存ダイアログ表示時は、タイトルの有無に合わせたボタンの設定を行う。
						this.ScoreSaveDialog.EnabledSave = (string.IsNullOrEmpty(this.CurrentScoreData.ScoreName)) ? false : true;
						break;

					default:
						goto case CompositionScreenMode.CompositionScreen;
				}

				this.__screenMode = value;
			}
		}

		#endregion


		#region コンストラクタ

		/// <summary>
		/// 汎用作曲画面の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="mode">この作曲画面が muphic のどのモードで使用されているかを示す識別子。</param>
		protected CompositionScreen(CompositionMode mode)
			: this(new ScoreData(), mode)
		{
		}

		/// <summary>
		/// 汎用作曲画面の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="defaultData">初期状態でセットする楽譜データ。</param>
		/// <param name="mode">この作曲画面が muphic のどのモードで使用されているかを示す識別子。</param>
		protected CompositionScreen(ScoreData defaultData, CompositionMode mode)
		{
			// 部品のインスタンス化など
			this.Initialization();

			this.ScreenMode = CompositionScreenMode.CompositionScreen;

			this.SetScoreData(defaultData);

			this.__compositionMode = mode;
		}


		/// <summary>
		/// 汎用作曲画面を構成する各パーツのインスタンス化等行う。
		/// </summary>
		protected override void Initialization()
		{
			// 登録開始
			base.Initialization(Settings.ResourceNames.CompositionScreenImages);

			#region 統合画像の読み込み

			this.LoadImageFiles();

			#endregion

			#region 部品のインスタンス化

			this.CompositionMain = new CompositionMain(this);
			this.AnimalButtons = new AnimalButtons(this);
			this.AnimalHoming = new AnimalHoming(this);
			this.Tempo = new Tempo(this);
			this.PlayFirstButton = new PlayFirstButton(this);
			this.PlayContinueButton = new PlayContinueButton(this);
			this.CreateNewButton = new CreateNewButton(this);
			this.TitleButton = new TitleButton(this);
			this.ScoreSaveButton = new ScoreSaveButton(this);
			this.ScoreLoadButton = new ScoreLoadButton(this);
			this.ScoreButton = new ScoreButton(this);
			this.CreateNewDialog = new CreateNewDialog(this);
			this.ScoreSaveDialog = new ScoreSaveDialog(this);
			this.ScoreLoadDialog = new ScoreLoadDialog(this);
			this.ScoreTitle = new Title(this.ToString(), Locations.Title, Settings.System.Default.Composition_MaxTitleLength);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.CompositionMain);
			this.PartsList.Add(this.AnimalButtons);
			this.PartsList.Add(this.Tempo);
			this.PartsList.Add(this.PlayFirstButton);
			this.PartsList.Add(this.PlayContinueButton);
			this.PartsList.Add(this.ScoreTitle);
			this.PartsList.Add(this.CreateNewButton);
			this.PartsList.Add(this.TitleButton);
			this.PartsList.Add(this.ScoreSaveButton);
			this.PartsList.Add(this.ScoreLoadButton);
			this.PartsList.Add(this.ScoreButton);

			#endregion

			#region テクスチャと座標の登録

			DrawManager.Regist(this.ToString(), Locations.CompositionScreenArea.Location, "IMAGE_COMPOSITIONSCR_BG");

			#endregion

			// 登録終了
			DrawManager.EndRegist();
		}

		#endregion


		#region 楽譜データ操作

		/// <summary>
		/// 現在の楽譜データを破棄し、空の楽譜データをセットする。
		/// </summary>
		public virtual void ClearScoreData()
		{
			ScoreData newData = new ScoreData();

			if (this.CompositionMode == CompositionMode.StoryScreen)
			{
				// ものがたりおんがくモード時は、曲名はクリアせずそのまま使用する
				newData.ScoreName = this.CurrentScoreData.ScoreName;
			}

			this.SetScoreData(newData);
		}

		/// <summary>
		/// 現在の楽譜データを破棄し、与えられた楽譜データをセットする。
		/// </summary>
		/// <param name="setData">セットする楽譜データ。</param>
		public virtual void SetScoreData(ScoreData setData)
		{
			this.CurrentScoreData = setData;

			Tools.DebugTools.ConsolOutputMessage(
				"CompositionScreen -SetStoryData",
				string.Format("楽譜データセット -- \"{0}\"", string.IsNullOrEmpty(setData.ScoreName) ? "(新規作成)" : setData.ScoreName),
				true
			);

			this.CompositionMain.IsPlaying = false;		// 停止状態にする
			this.CompositionMain.Scroll(0.0F);			// 表示位置を楽譜の先頭に戻す
			this.ScoreTitle.Text = setData.ScoreName;	// 楽譜名を表示
		}

		#endregion


		#region 描画

		/// <summary>
		/// 作曲画面を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			switch (this.ScreenMode)
			{
				case CompositionScreenMode.CompositionScreen:
					base.Draw(drawStatus);										// 作曲画面を描画
					if (this.AnimalHoming.HomingMode == HomingMode.Animal)		// 追尾対象が動物だった場合
					{															// 追尾位置に追尾対象の動物を描画
						Manager.DrawManager.DrawCenter(this.AnimalHoming.ToString(), this.AnimalHoming.AnimalPoint, this.AnimalHoming.State);
					}
					else if (this.AnimalHoming.HomingMode == HomingMode.Line)	// 追尾ではなくラインの描画設定だった場合
					{															// 追尾動物の代わりに動物を選択していることを表すラインを描画
						if (this.AnimalHoming.State == 8)						// ただし、削除追尾だった場合はラインと同時に追尾対象の削除テクスチャを描画する
						{														// 
							Manager.DrawManager.DrawCenter(this.AnimalHoming.ToString(), this.AnimalHoming.AnimalPoint, 9);
						}
						System.Drawing.Rectangle lineRect = RectangleManager.Get(this.AnimalHoming.ToString(), 8);
						System.Drawing.Rectangle lineArea = new System.Drawing.Rectangle(Tools.CommonTools.CenterToOnreft(this.AnimalHoming.AnimalPoint, lineRect.Size), lineRect.Size);
						Manager.DrawManager.DrawLine(lineArea);
					}
					break;

				case CompositionScreenMode.ScoreScreen:
				case CompositionScreenMode.EntitleScreen:
					this.SubScreen.Draw(drawStatus);
					break;

				case CompositionScreenMode.CreateNewDialog:
					base.Draw(drawStatus);
					this.CreateNewDialog.Draw(drawStatus);
					break;

				case CompositionScreenMode.ScoreSaveDialog:
					base.Draw(drawStatus);
					this.ScoreSaveDialog.Draw(drawStatus);
					break;

				case CompositionScreenMode.ScoreLoadDialog:
					base.Draw(drawStatus);
					this.ScoreLoadDialog.Draw(drawStatus);
					break;

				default:
					goto case CompositionScreenMode.CompositionScreen;
			}
		}

		#endregion


		#region マウス動作

		/// <summary>
		/// 作曲画面上でマウスがクリックされた際の処理を行う。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case CompositionScreenMode.CompositionScreen:
					base.Click(mouseStatus);
					break;

				case CompositionScreenMode.ScoreScreen:
				case CompositionScreenMode.EntitleScreen:
					this.SubScreen.Click(mouseStatus);
					break;

				case CompositionScreenMode.CreateNewDialog:
					this.CreateNewDialog.Click(mouseStatus);
					break;

				case CompositionScreenMode.ScoreSaveDialog:
					this.ScoreSaveDialog.Click(mouseStatus);
					break;

				case CompositionScreenMode.ScoreLoadDialog:
					this.ScoreLoadDialog.Click(mouseStatus);
					break;

				default:
					goto case CompositionScreenMode.CompositionScreen;
			}
		}


		/// <summary>
		/// 作曲画面上でマウスが動いた場合の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void MouseMove(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case CompositionScreenMode.CompositionScreen:
					base.MouseMove(mouseStatus);
					this.AnimalHoming.MouseMove(mouseStatus);
					break;

				case CompositionScreenMode.ScoreScreen:
				case CompositionScreenMode.EntitleScreen:
					this.SubScreen.MouseMove(mouseStatus);
					break;

				case CompositionScreenMode.CreateNewDialog:
					this.CreateNewDialog.MouseMove(mouseStatus);
					break;

				case CompositionScreenMode.ScoreSaveDialog:
					this.ScoreSaveDialog.MouseMove(mouseStatus);
					break;

				case CompositionScreenMode.ScoreLoadDialog:
					this.ScoreLoadDialog.MouseMove(mouseStatus);
					break;

				default:
					goto case CompositionScreenMode.CompositionScreen;
			}
		}

		#endregion


		#region ドラッグ

		/// <summary>
		/// 作曲画面上でドラッグを開始する。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void DragBegin(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case CompositionScreenMode.CompositionScreen:
					if (!this.CompositionMain.IsPlaying) base.DragBegin(mouseStatus);
					break;

				case CompositionScreenMode.ScoreScreen:
				case CompositionScreenMode.EntitleScreen:
					this.SubScreen.DragBegin(mouseStatus);
					break;

				default:
					break;
			}
		}


		/// <summary>
		/// 作曲画面上でのドラッグを終了する。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void DragEnd(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case CompositionScreenMode.CompositionScreen:
					if (!this.CompositionMain.IsPlaying) base.DragEnd(mouseStatus);
					break;

				case CompositionScreenMode.ScoreScreen:
				case CompositionScreenMode.EntitleScreen:
					this.SubScreen.DragEnd(mouseStatus);
					break;

				default:
					// ギガァァァァアアアア
					// ドリルゥゥゥゥゥゥゥウウウウ
					break;
			}
		}

		#endregion


		#region キーボード

		/// <summary>
		/// キーボードの何らかのキーが押された際の処理。
		/// </summary>
		/// <param name="keyStatus">キーボードの状態を示す Muphic.KeyboardStatusArgs クラス。</param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			switch (this.ScreenMode)
			{
				case CompositionScreenMode.CompositionScreen:
					this.CompositionMain.KeyDown(keyStatus);
					break;

				case CompositionScreenMode.ScoreScreen:
				case CompositionScreenMode.EntitleScreen:
					this.SubScreen.KeyDown(keyStatus);
					break;

				case CompositionScreenMode.CreateNewDialog:
					this.CreateNewDialog.KeyDown(keyStatus);
					break;

				case CompositionScreenMode.ScoreSaveDialog:
					this.ScoreSaveDialog.KeyDown(keyStatus);
					break;

				case CompositionScreenMode.ScoreLoadDialog:
					this.ScoreLoadDialog.KeyDown(keyStatus);
					break;

				default:
					goto case CompositionScreenMode.CompositionScreen;
			}
			base.KeyDown(keyStatus);
		}

		#endregion


		#region 解放

		/// <summary>
		/// 作曲画面登録したテクスチャ名を、描画管理クラスから削除する。
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
		}

		#endregion

	}
}
