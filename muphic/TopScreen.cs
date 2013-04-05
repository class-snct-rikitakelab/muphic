using System.IO;

using Muphic.Common;
using Muphic.Manager;
using Muphic.NameInputScreenParts;
using Muphic.TopScreenParts.Buttons;

namespace Muphic
{
	/// <summary>
	/// トップ画面で、現在表示すべき画面を示す列挙型。
	/// </summary>
	public enum TopScreenMode
	{
		/// <summary>
		/// トップ画面。
		/// </summary>
		TopScreen,

		/// <summary>
		/// ひとりでおんがく画面。
		/// </summary>
		OneScreen,

		/// <summary>
		/// つなげておんがく画面。
		/// </summary>
		LinkScreen,

		/// <summary>
		/// ものがたりおんがく画面。
		/// </summary>
		StoryScreen,

		/// <summary>
		/// プレイヤー名入力画面。
		/// </summary>
		NameInputScreen,
	}


	/// <summary>
	/// トップ画面クラス。
	/// </summary>
	public class TopScreen : Screen
	{

		#region フィールドとそのプロパティたち

		/// <summary>
		/// 親にあたる MainScreen クラス。
		/// </summary>
		public MainWindow Parent { get; private set; }


		/// <summary>
		/// 子にあたる Screen クラス。
		/// </summary>
		public Screen SubScreen { get; private set; }


		#region 選択ボタン

		/// <summary>
		/// ひとりで音楽モード 選択ボタン
		/// </summary>
		public OneButton OneButton { get; set; }

		/// <summary>
		/// つなげて音楽モード 選択ボタン
		/// </summary>
		public LinkButton LinkButton { get; set; }

		/// <summary>
		/// ものがたり音楽モード 選択ボタン
		/// </summary>
		public StoryButton StoryButton { get; set; }

		/// <summary>
		/// 名前入力画面 選択ボタン
		/// </summary>
		public NameInputButton NameInputButton { get; set; }

		/// <summary>
		/// muphic終了ボタン
		/// </summary>
		public EndButton EndButton { get; set; }

		#endregion


		/// <summary>
		/// 現在表示すべき画面を表わす TopScreenMode 列挙型。
		/// </summary>
		private TopScreenMode __screenMode = TopScreenMode.TopScreen;

		/// <summary>
		/// 現在表示すべき画面を表わす TopScreenMode 列挙型。
		/// </summary>
		public TopScreenMode ScreenMode
		{
			get
			{
				return this.__screenMode;
			}
			set
			{
				switch (value)
				{
					case TopScreenMode.TopScreen:									// トップ画面が指定された場合
						if (this.__screenMode == TopScreenMode.OneScreen)
						{																// 前の画面がひとりでおんがくモードであれば
							DrawManager.DrawNowLoading();								// NowLoadingテクスチャを表示
							((OneScreen)this.SubScreen).Dispose();						// 子スクリーンを解放
							base.LoadImageFiles();										// トップ画面を読み込み
							LogFileManager.WriteLine(									// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Properties.Resources.Msg_TopScr_EndOneScreen
							);
						}
						if (this.__screenMode == TopScreenMode.StoryScreen)
						{																// 前の画面がものがたりおんがくモードであれば
							DrawManager.DrawNowLoading();								// NowLoadingテクスチャを表示
							((MakeStoryScreen)this.SubScreen).Dispose();				// 子スクリーンを解放
							base.LoadImageFiles();										// トップ画面を読み込み
							LogFileManager.WriteLine(									// ログに書き込み
								Properties.Resources.Msg_Common_ScreenTransition,
								Properties.Resources.Msg_TopScr_EndStoryScreen
							);
						}
						if (this.__screenMode == TopScreenMode.NameInputScreen)
						{
							((NameInputScreen)this.SubScreen).Dispose();
							this.BackupPlayerName();
						}
						this.SubScreen = null;											// 子スクリーンをnull指定し、以後はトップ画面のみ処理を行う
						break;

					case TopScreenMode.OneScreen:									// ひとりでおんがくが指定された場合
						base.UnloadImageFiles();										// トップ画面を解放
						this.SubScreen = new OneScreen(this);							// 子スクリーンにひとりでおんがくクラスを指定 インスタンス生成
						LogFileManager.WriteLine(										// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Properties.Resources.Msg_TopScr_StartOneScreen
						);
						break;

					case TopScreenMode.StoryScreen:									// ものがたりおんがくが指定された場合
						base.UnloadImageFiles();										// トップ画面を解放
						this.SubScreen = new MakeStoryScreen(this);						// 子スクリーンにものがたりおんがくクラスを指定 インスタンス生成
						LogFileManager.WriteLine(										// ログに書き込み
							Properties.Resources.Msg_Common_ScreenTransition,
							Properties.Resources.Msg_TopScr_StartStoryScreen
						);
						break;

					case TopScreenMode.NameInputScreen:
						this.SubScreen = new NameInputScreen(this);
						break;

					default:						// 上記以外の場合はトップ画面指定と同じ扱いとする
						goto case TopScreenMode.TopScreen;
				}

				this.__screenMode = value;
				this.SetButtonsEnabled();
			}
		}

		#endregion


		#region コンストラクタと画面構成設定

		/// <summary>
		/// トップ画面のインスタンス化を行う。
		/// </summary>
		/// <param name="mainscreen">親となるMainScreen。</param>
		public TopScreen(MainWindow mainscreen)
		{
			this.Parent = mainscreen;	// 親スクリーンの設定
			this.Initialization();		// 画面構成設定

			this.LoadPlayerNameBackupFile();
			this.ScreenMode = TopScreenMode.TopScreen;
		}


		/// <summary>
		/// TopScreenの画面構成の設定
		/// 部品のインスタンス化や登録等はここで行う
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization(Settings.ResourceNames.TopScreenImages);

			#region 統合画像の読み込み

			base.LoadImageFiles();

			#endregion

			#region 部品のインスタンス化

			this.OneButton = new OneButton(this);
			this.StoryButton = new StoryButton(this);
			this.NameInputButton = new NameInputButton(this);
			this.EndButton = new EndButton(this);

			#endregion

			#region 部品をリストに登録

			this.PartsList.Add(this.OneButton);
			this.PartsList.Add(this.StoryButton);
			this.PartsList.Add(this.NameInputButton);
			this.PartsList.Add(this.EndButton);

			#endregion

			#region 部品のテクスチャと座標の登録

			DrawManager.Regist(this.ToString(), 0, 0, "IMAGE_TOPSCR_BG");

			#endregion

			DrawManager.EndRegist();
		}

		#endregion


		#region プレイヤー名関連

		/// <summary>
		/// トップ画面の [ひとりでおんがく] ボタン及び [ものがたりおんがく] ボタンの有効性を設定する。
		/// プレイヤー名が未入力かつプレイヤー名未入力時に格画面へ遷移できない設定の場合、2 つのボタンは無効化される。
		/// </summary>
		public void SetButtonsEnabled()
		{
			this.OneButton.Enabled = ConfigurationManager.Locked.EnabledOneScreen &&
				!(ConfigurationManager.Current.EnabledPlayerNameInputSafety && !NameInputScreen.HasPlayerName);
			this.StoryButton.Enabled = ConfigurationManager.Locked.EnabledStoryScreen &&
				!(ConfigurationManager.Current.EnabledPlayerNameInputSafety && !NameInputScreen.HasPlayerName);
		}

		/// <summary>
		/// プレイヤー名のバックアップを行う。
		/// </summary>
		private void BackupPlayerName()
		{
			Muphic.Tools.IO.XmlFileWriter.WriteSaveData<PlayerNameBackup>(
				new PlayerNameBackup(
					ConfigurationManager.Current.Player1,
					ConfigurationManager.Current.Player2,
					ConfigurationManager.Current.Player1Gender,
					ConfigurationManager.Current.Player2Gender),
				true,
				PlayerNameBackup.BackupFilePath
			);
		}

		/// <summary>
		/// プレイヤー名のバックアップファイルが存在すれば、それを削除する。
		/// </summary>
		public void DeletePlayerNameBackupFile()
		{
			if (File.Exists(PlayerNameBackup.BackupFilePath))
			{
				File.Delete(PlayerNameBackup.BackupFilePath);
			}
		}

		/// <summary>
		/// プレイヤー名のバックアップファイルが存在すれば、それをロードする。
		/// </summary>
		public void LoadPlayerNameBackupFile()
		{
			if (File.Exists(PlayerNameBackup.BackupFilePath))
			{
				var playerNameData = Muphic.Tools.IO.XmlFileReader.ReadSaveData<PlayerNameBackup>(PlayerNameBackup.BackupFilePath, true);
				
				ConfigurationManager.Current.Player1 = playerNameData.Player1;
				ConfigurationManager.Current.Player2 = playerNameData.Player2;
				ConfigurationManager.Current.Player1Gender = playerNameData.Player1Gender;
				ConfigurationManager.Current.Player2Gender = playerNameData.Player2Gender;
			}
		}

		#endregion


		#region 各動作記述メソッド群

		// 基本的に ScreenMode が TopScreen であれば自身の動作のみ実行し、
		// ScreenMode がそれ以外であればその動作の処理を行う

		/// <summary>
		/// トップ画面の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			if (this.ScreenMode == TopScreenMode.TopScreen) base.Draw(drawStatus);
			else this.SubScreen.Draw(drawStatus);
		}

		/// <summary>
		/// トップ画面がクリックされた際に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			if (this.ScreenMode == TopScreenMode.TopScreen) base.Click(mouseStatus);
			else this.SubScreen.Click(mouseStatus);
		}

		/// <summary>
		/// トップ画面上でマウスが動いた際に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void MouseMove(MouseStatusArgs mouseStatus)
		{
			if (this.ScreenMode == TopScreenMode.TopScreen) base.MouseMove(mouseStatus);
			else this.SubScreen.MouseMove(mouseStatus);
		}

		/// <summary>
		/// トップ画面上でドラッグが開始された際に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void DragBegin(MouseStatusArgs mouseStatus)
		{
			if (this.ScreenMode == TopScreenMode.TopScreen) base.DragBegin(mouseStatus);
			else this.SubScreen.DragBegin(mouseStatus);
		}

		/// <summary>
		/// トップ画面上でドラッグが終了した際に呼ばれる。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態を表わす Muphic.MouseStatusArgs クラス。</param>
		public override void DragEnd(MouseStatusArgs mouseStatus)
		{
			if (this.ScreenMode == TopScreenMode.TopScreen) base.DragEnd(mouseStatus);
			else this.SubScreen.DragEnd(mouseStatus);
		}

		/// <summary>
		/// キーボードの何らかのキーが押された際の処理。
		/// </summary>
		/// <param name="keyStatus">キーボードの状態を示す Muphic.KeyboardStatusArgs クラス。</param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			if (this.ScreenMode == TopScreenMode.TopScreen) base.KeyDown(keyStatus);
			else this.SubScreen.KeyDown(keyStatus);
		}

		#endregion

	}
}
