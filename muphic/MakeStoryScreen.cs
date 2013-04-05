using System;

using Muphic.Common;
using Muphic.PlayerWorks;
using Muphic.MakeStoryScreenParts;
using Muphic.MakeStoryScreenParts.Buttons;
using Muphic.MakeStoryScreenParts.Dialogs;
using Muphic.MakeStoryScreenParts.MakeStoryMainParts;
using Muphic.MakeStoryScreenParts.SubScreens;
using Muphic.MakeStoryScreenParts.SubScreens.SubScreenParts;
using Muphic.MakeStoryScreenParts.ThumbnailParts;
using Muphic.Manager;
using Muphic.Settings;

namespace Muphic
{
	// Muphic.PlayStoryScreen と Muphic.MakeStoryScreenParts.SubScreens.PlayStoryScreen が被るので、
	// ここでは後者を MakeStoryPlayScreen として定義し利用することを宣言
	using MakeStoryPlayScreen = Muphic.MakeStoryScreenParts.SubScreens.PlayStoryScreen;


	/// <summary>
	/// ものがたりおんがく画面で、現在表示すべき画面を示す識別子を指定する。
	/// </summary>
	public enum MakeStoryScreenMode
	{
		/// <summary>
		/// 物語作成画面
		/// </summary>
		MakeStoryScreen,

		/// <summary>
		/// 物語再生画面
		/// </summary>
		PlayStoryScreen,

		/// <summary>
		/// 作曲画面
		/// </summary>
		CompositionScreen,

		/// <summary>
		/// 題名入力画面
		/// </summary>
		EntitleScreen,

		/// <summary>
		/// 文章入力画面
		/// </summary>
		SentenceScreen,

		/// <summary>
		/// 新規作成確認ダイアログ
		/// </summary>
		CreateNewDialog,

		/// <summary>
		/// ファイル保存ダイアログ
		/// </summary>
		StorySaveDialog,

		/// <summary>
		/// ファイル読込ダイアログ
		/// </summary>
		StoryLoadDialog,

		/// <summary>
		/// スタンプ全削除確認ダイアログ
		/// </summary>
		AllDeleteDialog,

		/// <summary>
		/// ものがたりおんがく終了確認ダイアログ
		/// </summary>
		BackDialog,

		/// <summary>
		/// 物語提出確認ダイアログ
		/// </summary>
		SubmitDialog,

		///// <summary>
		///// 制作者名入力ダイアログ
		///// </summary>
		// NameInputDialog,

		/// <summary>
		/// 物語作品管理画面。
		/// </summary>
		StoryExplorer,
	}
	

	/// <summary>
	/// ものがたりおんがく画面 (物語作成画面) クラス。
	/// </summary>
	public class MakeStoryScreen : Screen, IDisposable
	{

		#region フィールドとそのプロパティ

		/// <summary>
		/// 物語作成画面の親となるトップ画面クラス。
		/// </summary>
		private readonly TopScreen __parent;

		/// <summary>
		/// 物語作成画面の親となるトップ画面クラス。
		/// </summary>
		public TopScreen Parent
		{
			get { return this.__parent; }
		}

		/// <summary>
		/// 子となる Muphic.Common.Screen。
		/// </summary>
		public Screen SubScreen { get; private set; }


		#region 物語描画

		/// <summary>
		/// スタンプ選択ボタン管理クラス。
		/// </summary>
		public StampSelectArea StampSelectArea { get; private set; }

		/// <summary>
		/// スタンプ追尾クラス。
		/// </summary>
		public StampHoming StampHoming { get; private set; }

		/// <summary>
		/// 物語の絵の管理クラス。
		/// </summary>
		public PictureWindow PictureWindow { get; private set; }

		/// <summary>
		/// サムネイル管理クラス。
		/// </summary>
		public ThumbnailArea ThumbnailArea { get; private set; }

		#endregion


		#region ボタン

		/// <summary>
		/// "はじめにもどる" (トップ画面へ遷移する) ボタン。
		/// </summary>
		public BackButton BackButton { get; private set; }

		/// <summary>
		/// "ものがたりながす" (物語再生画面へ遷移する) ボタン。
		/// </summary>
		public PlayButton PlayButton { get; private set; }

		/// <summary>
		/// "おんがくをつける" (作曲画面へ遷移する) ボタン。
		/// </summary>
		public CompositionButton CompositionButton { get; private set; }

		/// <summary>
		/// 画面両端に配置される 16 個のカテゴリ選択ボタン。
		/// </summary>
		public CategoryButton[] CategoryButtons { get; private set; }

		/// <summary>
		/// "あたらしくつくる" (物語の新規作成を行う) ボタン。
		/// </summary>
		public CreateNewButton CreateNewButton { get; private set; }

		/// <summary>
		/// "のこす" (物語の保存を行う) ボタン。
		/// </summary>
		public StorySaveButton StorySaveButton { get; private set; }

		/// <summary>
		/// "よびだす" (物語の読込を行う) ボタン。
		/// </summary>
		public StoryLoadButton StoryLoadButton { get; private set; }

		/// <summary>
		/// "いんさつ" (物語の印刷を行う) ボタン。
		/// </summary>
		public PrintButton PrintButton { get; private set; }

		/// <summary>
		/// "だいめい" (題名入力画面へ遷移する) ボタン。
		/// </summary>
		public TitleButton TitleButton { get; private set; }

		/// <summary>
		/// "ぶんしょう" (スライドの文章入力画面へ遷移する) ボタン。
		/// </summary>
		public SentenceButton SentenceButton { get; private set; }

		/// <summary>
		/// "もどす"ボタン (追尾状態をスタンプ削除モードにする) ボタン。
		/// </summary>
		public DeleteButton DeleteButton { get; private set; }

		/// <summary>
		/// "ぜんぶもどす" (スライドのスタンプを全て削除する) ボタン。
		/// </summary>
		public AllDeleteButton AllDeleteButton { get; private set; }

		/// <summary>
		/// "せんせいにだす" (物語を講師用コンピュータに提出する) ボタン。
		/// </summary>
		public SubmitButton SubmitButton { get; private set; }

		#endregion


		#region ダイアログ

		/// <summary>
		/// 新規作成確認ダイアログ。
		/// </summary>
		public CreateNewDialog CreateNewDialog { get; private set; }

		/// <summary>
		/// 物語読込ダイアログ。
		/// </summary>
		public StoryLoadDialog StoryLoadDialog { get; private set; }

		/// <summary>
		/// 物語保存ダイアログ。
		/// </summary>
		public StorySaveDialog StorySaveDialog { get; private set; }

		/// <summary>
		/// スタンプ全削除確認ダイアログ。
		/// </summary>
		public AllDeleteDialog AllDeleteDialog { get; private set; }

		/// <summary>
		/// ものがたりおんがく終了確認ダイアログ。
		/// </summary>
		public BackDialog BackDialog { get; private set; }

		/// <summary>
		/// 物語提出確認ダイアログ。
		/// </summary>
		public SubmitDialog SubmitDialog { get; private set; }

		///// <summary>
		///// 製作者名入力ダイアログ。
		///// </summary>
		// public NameInputDialog NameInputDialog { get; private set; }

		#endregion


		#region 管理

		/// <summary>
		/// ものがたり題名管理クラスを取得する。
		/// </summary>
		public StoryTitle StoryTitle { get; private set; }

		/// <summary>
		/// ものがたり文章管理クラスを取得する。
		/// </summary>
		public StorySentence StorySentence { get; private set; }

		/// <summary>
		/// 現在編集を行っている物語データを取得する。
		/// </summary>
		public StoryData CurrentStoryData { get; private set; }


		/// <summary>
		/// 作曲時の制限モードを取得または設定する。
		/// </summary>
		public CompositionLimitMode LimitMode { get; set; }


		/// <summary>
		/// 成果物管理画面。
		/// </summary>
		public StoryExplorer StoryExplorer { get; set; }

		/// <summary>
		/// muphic が講師用モードで動作しているかどうかを取得する。
		/// </summary>
		public bool IsTeacherMode
		{
			get { return MainWindow.MuphicOperationMode == MuphicOperationMode.TeacherMode; }
		}

		#endregion


		#region 各種データ
		
		/// <summary>
		/// 現在編集を行っている物語のページを表す整数を取得する。
		/// </summary>
		public int CurrentPage
		{
			get
			{
				return this.ThumbnailArea.CurrentPage;
			}
		}

		/// <summary>
		/// 現在編集を行っているスライドを取得する。
		/// </summary>
		public Slide CurrentSlide
		{
			get
			{
				return this.CurrentStoryData.SlideList[this.ThumbnailArea.CurrentPage];
			}
		}

		/// <summary>
		/// 現在のカテゴリモードを取得する。
		/// </summary>
		public CategoryMode CategoryMode
		{
			get
			{
				return this.StampSelectArea.CategoryMode;
			}
		}


		/// <summary>
		/// ものがたりおんがくモードで使用される自動保存ファイルのパスを取得する。
		/// </summary>
		private static string AutoSaveFilePath
		{
			get { return System.IO.Path.Combine(Configuration.ConfigurationData.UserDataFolder, Settings.ResourceNames.StoryScreenAutoSaveFilePath); }
		}


		/// <summary>
		/// 物語が最後に保存されてから、物語に変更が加えられたかどうかを取得する。
		/// </summary>
		public bool IsChanged
		{
			get
			{
				return true;
			}
		}

		#endregion


		#region 表示画面の設定

		/// <summary>
		/// 表示するべき画面を表す MakeStoryScreenMode 列挙型。
		/// <para>こちらではなく ScreenMode プロパティを使用すること。</para>
		/// </summary>
		private MakeStoryScreenMode __screenMode;

		/// <summary>
		/// 表示するべき画面を表す MakeStoryScreenMode 列挙型を取得または設定する。
		/// </summary>
		public MakeStoryScreenMode ScreenMode
		{
			get
			{
				return this.__screenMode;
			}
			set
			{
				switch (value)
				{
					case MakeStoryScreenMode.MakeStoryScreen:											// 物語作成画面が選択された場合
						if (this.__screenMode == MakeStoryScreenMode.CompositionScreen)
						{																					// 前の画面が物語作曲画面であれば
							((StoryCompositionScreen)this.SubScreen).Dispose();								// 物語作曲画面の解放
							LogFileManager.WriteLine(														// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Tools.CommonTools.GetResourceMessage(Properties.Resources.Msg_MakeStoryScr_EndComposition, this.CurrentPage.ToString())
							);
						}

						else if (this.__screenMode == MakeStoryScreenMode.EntitleScreen)
						{																					// 前の画面が物語名入力画面であれば
							((StoryEntitleScreen)this.SubScreen).Dispose();									// 物語名入力画面の解放
							LogFileManager.WriteLine(														// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Tools.CommonTools.GetResourceMessage(Properties.Resources.Msg_MakeStoryScr_EndEntitle, this.CurrentStoryData.Title)
							);
						}

						else if (this.__screenMode == MakeStoryScreenMode.SentenceScreen)
						{																					// 前の画面が物語文入力画面であれば
							((SentenceInputScreen)this.SubScreen).Dispose();								// 物語文入力画面の解放
							LogFileManager.WriteLine(														// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Tools.CommonTools.GetResourceMessage(Properties.Resources.Msg_MakeStoryScr_EndInputSentence, this.CurrentPage.ToString(), this.CurrentSlide.Sentence.ToString())
							);
						}

						else if (this.__screenMode == MakeStoryScreenMode.PlayStoryScreen)
						{																					// 前の画面が物語再生画面であれば
							((MakeStoryPlayScreen)this.SubScreen).Dispose();								// 物語再生画面の解放
							LogFileManager.WriteLine(														// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Properties.Resources.Msg_MakeStoryScr_EndPlayStory
							);
						}

						else if (this.__screenMode == MakeStoryScreenMode.StoryExplorer)
						{																					// 前の画面が作品管理画面であれば
							//((StoryExplorer)this.SubScreen).Dispose();										// 作品管理画面の解放
							LogFileManager.WriteLine(														// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Properties.Resources.Msg_MakeStoryScr_CloseStoryExplorer
							);
						}

						this.SubScreen = null;																// 子スクリーンはnull
						break;


					case MakeStoryScreenMode.CompositionScreen:											// ものがたりおんがくモード作曲画面が選択された場合
						this.SubScreen = new StoryCompositionScreen(this);									// 作曲画面を生成
						LogFileManager.WriteLine(															// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Tools.CommonTools.GetResourceMessage(Properties.Resources.Msg_MakeStoryScr_StartComposition, this.CurrentPage.ToString())
						);
						break;

					case MakeStoryScreenMode.EntitleScreen:												// 物語名入力画面が選択された場合
						this.SubScreen = new StoryEntitleScreen(this, this.StoryTitle.Title);				// 題名入力画面を生成 (頻度から考えて物語作成画面のテクスチャ解放は必要ないと思われる) 
						LogFileManager.WriteLine(															// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Properties.Resources.Msg_MakeStoryScr_StartEntitle
						);
						break;

					case MakeStoryScreenMode.SentenceScreen:											// 物語文入力画面が選択された場合
						this.SubScreen = new SentenceInputScreen(this, this.StorySentence.Sentence);		// 題名入力画面を生成 (頻度から考えて物語作成画面のテクスチャ解放は必要ないと思われる) 
						LogFileManager.WriteLine(															// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Tools.CommonTools.GetResourceMessage(Properties.Resources.Msg_MakeStoryScr_StartInputSentence, this.CurrentPage.ToString())
						);
						break;

					case MakeStoryScreenMode.PlayStoryScreen:											// 物語再生画面が選択された場合
						this.SubScreen = new MakeStoryPlayScreen(this);										// 物語再生画面を生成
						LogFileManager.WriteLine(															// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Properties.Resources.Msg_MakeStoryScr_StartPlayStory
						);
						break;

					case MakeStoryScreenMode.StoryExplorer:												// 作品管理画面が選択された場合
						//this.SubScreen = new StoryExplorer(this);											// 作品管理画面を生成
						this.StoryExplorer.Update();
						LogFileManager.WriteLine(															// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Properties.Resources.Msg_MakeStoryScr_StartPlayStory
						);
						break;

					case MakeStoryScreenMode.CreateNewDialog:
						break;

					case MakeStoryScreenMode.StoryLoadDialog:											// 読込ダイアログが選択された場合
						this.StoryLoadDialog.SelectArea.Initialization(Tools.IO.XmlFileReader.GetStoryDataList());	// ファイル名一覧を更新
						break;

					case MakeStoryScreenMode.StorySaveDialog:											// 保存ダイアログが選択された場合
						this.StorySaveDialog.EnabledSave = !string.IsNullOrEmpty(this.CurrentStoryData.Title);		// 題名の有無によってダイアログの状態を変更
						break;

					case MakeStoryScreenMode.SubmitDialog:												// 物語提出確認ダイアログが選択された場合
						this.SubmitDialog.EnabledSubmit = !string.IsNullOrEmpty(this.CurrentStoryData.Title);		// 題名の有無によってダイアログの状態を変更
						break;

					//case MakeStoryScreenMode.NameInputDialog:
					//    this.NameInputDialog.Show();
					//    break;

					case MakeStoryScreenMode.AllDeleteDialog:
						break;

					case MakeStoryScreenMode.BackDialog:
						break;

					default:
						goto case MakeStoryScreenMode.MakeStoryScreen;
				}

				this.__screenMode = value;
			}
		}

		#endregion


		#endregion


		#region コンストラクタ/初期化

		/// <summary>
		/// ものがたりおんがく画面クラスの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">この画面の親となるトップ画面。</param>
		public MakeStoryScreen(TopScreen parent)
		{
			// 親のトップ画面を設定
			this.__parent = parent;

			// パーツのインスタンス化等
			this.Initialization();

			// 自動保存イベントの登録
			FrameManager.AutoSaveEvent += new Muphic.Manager.AutoSaveEventHandler(AutoSaveEvent);

			// 動作モードによるボタンの表示/非表示の設定
			if (Muphic.MainWindow.MuphicOperationMode == MuphicOperationMode.StudentMode)
			{
				// 児童用モードの場合、提出ボタンを有効にし、印刷ボタンを無効にする
				this.SubmitButton.Visible = true;
				this.PrintButton.Visible = false;

				// 児童用モードで制限機能が有効ならば、デフォルトで音階と八分を制限する
				if(ConfigurationManager.Current.EnabledLimitMode)
					this.LimitMode = CompositionLimitMode.LimitCode579AndCrotchet;
			}
			else
			{
				// 児童用モードでなければ、提出ボタンを無効にし、印刷ボタンを有効にする。
				this.SubmitButton.Visible = false;
				this.PrintButton.Visible = true;

				// 児童用モードでなければ、音階制限は無効にする
				this.LimitMode = CompositionLimitMode.None;
			}

			// 物語の初期状態の設定
			if (System.IO.File.Exists(MakeStoryScreen.AutoSaveFilePath))
			{
				// 自動保存データが存在した場合は読み込む
				this.SetStoryData(Muphic.Tools.IO.XmlFileReader.ReadStoryData(MakeStoryScreen.AutoSaveFilePath));
			}
			else
			{
				// 自動保存データが存在しない場合は、物語を新規作成する
				this.ClearStoryData();
			}
		}


		/// <summary>
		/// 物語作成画面クラスを構成する各部パーツのインスタンス化やテクスチャ登録を行う。
		/// </summary>
		protected override void Initialization()
		{
			// 登録開始
			base.Initialization(ResourceNames.MakeStoryScreenImages);

			#region 統合画像の読み込み

			this.LoadImageFiles();

			#endregion

			#region 部品のインスタンス化

			this.StoryTitle = new StoryTitle(this);
			this.StorySentence = new StorySentence(this);
			this.StampSelectArea = new StampSelectArea(this);
			this.StampHoming = new StampHoming(this);
			this.PictureWindow = new PictureWindow(this);
			this.ThumbnailArea = new ThumbnailArea(this);
			this.BackButton = new BackButton(this);
			this.PlayButton = new PlayButton(this);
			this.CompositionButton = new CompositionButton(this);
			this.CreateNewButton = new CreateNewButton(this);
			this.StorySaveButton = new StorySaveButton(this);
			this.StoryLoadButton = new StoryLoadButton(this);
			this.PrintButton = new PrintButton(this);
			this.TitleButton = new TitleButton(this);
			this.SentenceButton = new SentenceButton(this);
			this.SubmitButton = new SubmitButton(this);
			this.DeleteButton = new DeleteButton(this);
			this.AllDeleteButton = new AllDeleteButton(this);
			this.CreateNewDialog = new CreateNewDialog(this);
			this.StoryLoadDialog = new StoryLoadDialog(this);
			this.StorySaveDialog = new StorySaveDialog(this);
			this.AllDeleteDialog = new AllDeleteDialog(this);
			this.SubmitDialog = new SubmitDialog(this);
			this.BackDialog = new BackDialog(this);
			// this.NameInputDialog = new NameInputDialog(this);

			#endregion

			#region 部品のリストへの登録
			
			this.PartsList.Add(this.StoryTitle);
			this.PartsList.Add(this.StorySentence);
			this.PartsList.Add(this.StampSelectArea);
			this.PartsList.Add(this.PictureWindow);
			this.PartsList.Add(this.ThumbnailArea);
			this.PartsList.Add(this.BackButton);
			this.PartsList.Add(this.PlayButton);
			this.PartsList.Add(this.CompositionButton);
			this.PartsList.Add(this.CreateNewButton);
			this.PartsList.Add(this.StorySaveButton);
			this.PartsList.Add(this.StoryLoadButton);
			this.PartsList.Add(this.PrintButton);
			this.PartsList.Add(this.TitleButton);
			this.PartsList.Add(this.SentenceButton);
			this.PartsList.Add(this.SubmitButton);
			this.PartsList.Add(this.DeleteButton);
			this.PartsList.Add(this.AllDeleteButton);

			#endregion

			#region カテゴリ選択ボタンのインスタンス化と登録

			this.CategoryButtons = new CategoryButton[16];		// カテゴリボタン16個
			string[] labelNames = new string[] {				// テクスチャのラベル名16種
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_BG1",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_BG2",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_BG3",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_BG4",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_IM1",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_IM2",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_IM3",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_IM4",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_HM1",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_HM2",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_HM3",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_HM4",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_AM1",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_AM2",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_AM3",
				"IMAGE_MAKESTORYSCR_CATEGORYBTN_AM4"
			};

			for(int i=0; i<this.CategoryButtons.Length; i++)	// 0～15のカテゴリ選択ボタンをそれぞれインスタンス化し
			{													// 部品リストに登録
				this.CategoryButtons[i] = new CategoryButton(
					this, (CategoryMode)i,
					(System.Drawing.Point)Settings.PartsLocation.Default["MakeStoryScr_CategoryBtn" + i.ToString()],
					labelNames[i]
				);
				this.PartsList.Add(this.CategoryButtons[i]);
			}

			#endregion

			#region テクスチャと座標の登録
			
			DrawManager.Regist(this.ToString(), Settings.PartsLocation.Default.MakeStoryScr.Location, "IMAGE_MAKESTORYSCR_BG");

			#endregion

			// 登録終了
			DrawManager.EndRegist();

			// 成果物管理画面も予め生成
			if (this.IsTeacherMode) this.StoryExplorer = new StoryExplorer(this);
		}

		#endregion


		#region 描画

		/// <summary>
		/// 物語作成画面を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			switch (this.ScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:		// 物語作成画面指定
					base.Draw(drawStatus);						// 物語作成画面の描画
					this.StampHoming.DrawTarget(drawStatus);	// 追尾対象を描画
					break;

				case MakeStoryScreenMode.PlayStoryScreen:		// 物語再生画面指定
				case MakeStoryScreenMode.EntitleScreen:			// 題名入力画面指定
				case MakeStoryScreenMode.SentenceScreen:		// 文章入力画面指定
				case MakeStoryScreenMode.CompositionScreen:		// 作曲画面指定
					this.SubScreen.Draw(drawStatus);			// それぞれサブ画面の描画
					break;

				case MakeStoryScreenMode.CreateNewDialog:		// 新規作成ダイアログ指定
					base.Draw(drawStatus);						// 物語作成画面を描画
					this.CreateNewDialog.Draw(drawStatus);		// 新規作成ダイアログを描画
					break;

				case MakeStoryScreenMode.StorySaveDialog:		// 保存ダイアログ指定
					base.Draw(drawStatus);						// 物語作成画面を描画
					this.StorySaveDialog.Draw(drawStatus);		// 物語保存ダイアログを描画
					break;

				case MakeStoryScreenMode.StoryLoadDialog:		// 読込ダイアログ指定
					base.Draw(drawStatus);						// 物語作成画面を描画
					this.StoryLoadDialog.Draw(drawStatus);		// 物語読込ダイアログを描画
					break;

				case MakeStoryScreenMode.AllDeleteDialog:		// スタンプ全削除確認ダイアログ指定
					base.Draw(drawStatus);						// 物語作成画面を描画
					this.AllDeleteDialog.Draw(drawStatus);		// スタンプ全削除確認ダイアログを描画
					break;

				case MakeStoryScreenMode.SubmitDialog:			// 物語提出確認ダイアログ指定
					base.Draw(drawStatus);						// 物語作成画面を描画
					this.SubmitDialog.Draw(drawStatus);			// 物語提出確認ダイアログを描画
					break;

				//case MakeStoryScreenMode.NameInputDialog:
				//    base.Draw(drawStatus);
				//    this.NameInputDialog.Draw(drawStatus);
				//    break;

				case MakeStoryScreenMode.BackDialog:			// ものがたりおんがく終了確認ダイアログ指定
					base.Draw(drawStatus);						// 物語作成画面を描画
					this.BackDialog.Draw(drawStatus);			// ものがたりおんがく終了確認ダイアログを描画
					break;

				case MakeStoryScreenMode.StoryExplorer:			// 提出作品管理画面指定
					this.StoryExplorer.Draw(drawStatus);
					break;

				default:
					goto case MakeStoryScreenMode.MakeStoryScreen;
			}
		}

		#endregion


		#region マウス動作

		/// <summary>
		/// 物語作成画面がクリックされた際の処理
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:	// 物語作成画面指定
					base.Click(mouseStatus);
					break;

				case MakeStoryScreenMode.PlayStoryScreen:	// 物語再生画面指定
				case MakeStoryScreenMode.EntitleScreen:		// 題名入力画面指定
				case MakeStoryScreenMode.SentenceScreen:	// 文章入力画面指定
				case MakeStoryScreenMode.CompositionScreen:	// 作曲画面指定
					this.SubScreen.Click(mouseStatus);
					break;

				case MakeStoryScreenMode.CreateNewDialog:	// 新規作成ダイアログ指定
					this.CreateNewDialog.Click(mouseStatus);
					break;

				case MakeStoryScreenMode.StorySaveDialog:	// 保存ダイアログ指定
					this.StorySaveDialog.Click(mouseStatus);
					break;

				case MakeStoryScreenMode.StoryLoadDialog:	// 読込ダイアログ指定
					this.StoryLoadDialog.Click(mouseStatus);
					break;

				case MakeStoryScreenMode.AllDeleteDialog:	// スタンプ全削除確認ダイアログ指定
					this.AllDeleteDialog.Click(mouseStatus);
					break;

				case MakeStoryScreenMode.SubmitDialog:		// 物語提出確認ダイアログ指定
					this.SubmitDialog.Click(mouseStatus);
					break;

				//case MakeStoryScreenMode.NameInputDialog:
				//    this.NameInputDialog.Click(mouseStatus);
				//    break;
					
				case MakeStoryScreenMode.BackDialog:		// ものがたりおんがく終了確認ダイアログ指定
					this.BackDialog.Click(mouseStatus);
					break;

				case MakeStoryScreenMode.StoryExplorer:		// 提出作品管理画面指定
					this.StoryExplorer.Click(mouseStatus);
					break;

				default:
					goto case MakeStoryScreenMode.MakeStoryScreen;
			}
		}


		/// <summary>
		/// 物語作成画面上でマウスが動いた場合の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void MouseMove(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:	// 物語作成画面指定
					base.MouseMove(mouseStatus);
					this.StampHoming.MouseMove(mouseStatus);
					break;

				case MakeStoryScreenMode.PlayStoryScreen:	// 物語再生画面指定
				case MakeStoryScreenMode.EntitleScreen:		// 題名入力画面指定
				case MakeStoryScreenMode.SentenceScreen:	// 文章入力画面指定
				case MakeStoryScreenMode.CompositionScreen:	// 作曲画面指定
					this.SubScreen.MouseMove(mouseStatus);
					break;

				case MakeStoryScreenMode.CreateNewDialog:	// 新規作成ダイアログ指定
					this.CreateNewDialog.MouseMove(mouseStatus);
					break;

				case MakeStoryScreenMode.StorySaveDialog:	// 保存ダイアログ指定
					this.StorySaveDialog.MouseMove(mouseStatus);
					break;

				case MakeStoryScreenMode.StoryLoadDialog:	// 読込ダイアログ指定
					this.StoryLoadDialog.MouseMove(mouseStatus);
					break;

				case MakeStoryScreenMode.AllDeleteDialog:	// スタンプ全削除確認ダイアログ指定
					this.AllDeleteDialog.MouseMove(mouseStatus);
					break;

				case MakeStoryScreenMode.SubmitDialog:		// 物語提出確認ダイアログ指定
					this.SubmitDialog.MouseMove(mouseStatus);
					break;

				//case MakeStoryScreenMode.NameInputDialog:	// 制作者名入力ダイアログ指定
				//    this.NameInputDialog.MouseMove(mouseStatus);
				//    break;

				case MakeStoryScreenMode.BackDialog:		// ものがたりおんがく終了確認ダイアログ指定
					this.BackDialog.MouseMove(mouseStatus);
					break;

				case MakeStoryScreenMode.StoryExplorer:		// 提出作品管理画面指定
					this.StoryExplorer.MouseMove(mouseStatus);
					break;

				default:
					goto case MakeStoryScreenMode.MakeStoryScreen;
			}
		}


		#endregion


		#region ドラッグ

		/// <summary>
		/// 物語作成画面上でドラッグが開始された際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void DragBegin(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:	// 物語作成画面指定
					base.DragBegin(mouseStatus);
					break;

				case MakeStoryScreenMode.PlayStoryScreen:	// 物語再生画面指定
				case MakeStoryScreenMode.EntitleScreen:		// 題名入力画面指定
				case MakeStoryScreenMode.SentenceScreen:	// 文章入力画面指定
				case MakeStoryScreenMode.CompositionScreen:	// 作曲画面指定
					this.SubScreen.DragBegin(mouseStatus);
					break;

				case MakeStoryScreenMode.StoryExplorer:		// 提出作品管理画面指定
					this.StoryExplorer.DragBegin(mouseStatus);
					break;

				default:
					break;
			}
		}


		/// <summary>
		/// 物語作成画面上でドラッグが終了した際の処理。
		/// </summary>
		/// <param name="mouseStatus">クリック時のマウスの状態。</param>
		public override void DragEnd(MouseStatusArgs mouseStatus)
		{
			switch (this.ScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:	// 物語作成画面指定
					base.DragEnd(mouseStatus);
					break;

				case MakeStoryScreenMode.PlayStoryScreen:	// 物語再生画面指定
				case MakeStoryScreenMode.EntitleScreen:		// 題名入力画面指定
				case MakeStoryScreenMode.SentenceScreen:	// 文章入力画面指定
				case MakeStoryScreenMode.CompositionScreen:	// 作曲画面指定
					this.SubScreen.DragEnd(mouseStatus);
					break;

				case MakeStoryScreenMode.StoryExplorer:		// 提出作品管理画面指定
					this.StoryExplorer.DragEnd(mouseStatus);
					break;

				default:
					break;
			}
		}

		#endregion


		#region キーボード

		/// <summary>
		/// キーボードの何らかのキーが押された際の処理。
		/// </summary>
		/// <param name="keyStatus">キーボードの状態。</param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			switch (this.ScreenMode)
			{
				case MakeStoryScreenMode.MakeStoryScreen:	// 物語作成画面指定
					base.KeyDown(keyStatus);
					break;

				case MakeStoryScreenMode.PlayStoryScreen:	// 物語再生画面指定
				case MakeStoryScreenMode.EntitleScreen:		// 題名入力画面指定
				case MakeStoryScreenMode.SentenceScreen:	// 文章入力画面指定
				case MakeStoryScreenMode.CompositionScreen:	// 作曲画面指定
					this.SubScreen.KeyDown(keyStatus);
					break;

				case MakeStoryScreenMode.CreateNewDialog:	// 新規作成ダイアログ指定
					this.CreateNewDialog.KeyDown(keyStatus);
					break;

				case MakeStoryScreenMode.StorySaveDialog:	// 保存ダイアログ指定
					this.StorySaveDialog.KeyDown(keyStatus);
					break;

				case MakeStoryScreenMode.StoryLoadDialog:	// 読込ダイアログ指定
					this.StoryLoadDialog.KeyDown(keyStatus);
					break;

				case MakeStoryScreenMode.AllDeleteDialog:	// スタンプ全削除確認ダイアログ指定
					this.AllDeleteDialog.KeyDown(keyStatus);
					break;

				case MakeStoryScreenMode.SubmitDialog:		// 物語提出確認ダイアログ指定
					this.SubmitDialog.KeyDown(keyStatus);
					break;

				//case MakeStoryScreenMode.NameInputDialog:
				//    this.NameInputDialog.KeyDown(keyStatus);
				//    break;

				case MakeStoryScreenMode.BackDialog:		// ものがたりおんがく終了確認ダイアログ指定
					this.BackDialog.KeyDown(keyStatus);
					break;

				case MakeStoryScreenMode.StoryExplorer:		// 提出作品管理画面指定
					this.StoryExplorer.KeyDown(keyStatus);
					break;

				default:
					goto case MakeStoryScreenMode.MakeStoryScreen;
			}
		}

		#endregion


		#region 解放

		/// <summary>
		/// ものがたりおんがく画面で使用したリソースを解放し、登録したキーとテクスチャ名を削除する。
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();

			// 自動保存イベントの登録を削除
			FrameManager.AutoSaveEvent -= new Muphic.Manager.AutoSaveEventHandler(this.AutoSaveEvent);

			// 正常な画面遷移なので、自動保存データを削除する
			if (System.IO.File.Exists(MakeStoryScreen.AutoSaveFilePath))
			{
				try
				{
					System.IO.File.Delete(MakeStoryScreen.AutoSaveFilePath);
				}
				catch (System.Exception e)
				{
					LogFileManager.WriteLineError(Properties.Resources.Msg_MakeStoryScr_AutoSaveFileDelete_Failure, MakeStoryScreen.AutoSaveFilePath);
					LogFileManager.WriteLineError(e.ToString());
				}
			}

			this.SafeDispose(this.SubScreen);
			this.SafeDispose(this.StoryExplorer);
			// this.SafeDispose(this.NameInputDialog);
		}

		#endregion


		#region 物語データ操作

		/// <summary>
		/// 現在の物語データを破棄し、空の物語データをセットする。
		/// </summary>
		public void ClearStoryData()
		{
			this.SetStoryData(new StoryData());

			// プレイヤー1 の情報を物語データにセット
			if (!string.IsNullOrEmpty(ConfigurationManager.Current.Player1))
			{
				this.CurrentStoryData.Authors.Add(Author.CurrentPlayer1Data);
			}

			// プレイヤー1 の情報を物語データにセット
			if (!string.IsNullOrEmpty(ConfigurationManager.Current.Player2))
			{
				this.CurrentStoryData.Authors.Add(Author.CurrentPlayer2Data);
			}
		}

		/// <summary>
		/// 現在の物語データを破棄し、与えられた物語データをセットする。
		/// </summary>
		/// <param name="setData">セットする物語データ。</param>
		public void SetStoryData(StoryData setData)
		{
			// 編集スライドをセット
			this.CurrentStoryData = setData;

			Tools.DebugTools.ConsolOutputMessage(
				"MakeStoryScreen -SetStoryData",
				string.Format("物語データセット -- \"{0}\"", string.IsNullOrEmpty(setData.Title) ? "(新規作成)" : setData.Title),
				true
			);

			// 編集スライドは0に移動
			this.ThumbnailArea.CurrentPage = 0;

			// 全てのスタンプに画面上の座標とテクスチャ名を設定する
			Tools.MakeStoryTools.SetStampImageName(this.CurrentStoryData);

			this.CurrentSlideChanged();
		}


		/// <summary>
		/// 現在の絵のスタンプと背景をクリアし、初期状態に戻す。
		/// </summary>
		public void ClearCurrentPicture()
		{
			this.CurrentSlide.StampList.Clear();						// スタンプをすべて削除
			this.CurrentSlide.BackgroundPlace = BackgroundPlace.None;	// 背景の場所をクリア
			this.CurrentSlide.BackgroundSky = BackgroundSky.None;		// 背景の場所をクリア
			this.CurrentSlideChanged();
		}


		/// <summary>
		/// 現在編集中のスライドに何らかの変更が為された、または為される可能性のある処理の後に実行するべき処理。
		/// </summary>
		public void CurrentSlideChanged()
		{
			this.ThumbnailArea.SetNextButtonEnabled();	// スライド移動ボタンの設定
			this.PlayButton.SetEnabled();				// 再生ボタンの設定
		}

		#endregion


		#region 自動保存

		/// <summary>
		/// 物語データの自動保存を行う。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AutoSaveEvent(object sender, Muphic.Manager.AutoSaveEventArgs e)
		{
			LogFileManager.WriteLine(
				Properties.Resources.Msg_MakeStoryScr_AutoSave,
				string.IsNullOrEmpty(this.CurrentStoryData.Title) ? Properties.Resources.Msg_MakeStoryScr_NonTitle : this.CurrentStoryData.Title
			);
			Muphic.Tools.IO.XmlFileWriter.WriteStoryData(this.CurrentStoryData, MakeStoryScreen.AutoSaveFilePath);
		}

		#endregion

	}
}
