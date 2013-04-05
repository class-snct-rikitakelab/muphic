using System;
using System.Windows.Forms;

using Muphic.Manager;
using Muphic.Tools;

namespace Muphic
{
	/// <summary>
	/// muphic メインウィンドウ
	/// </summary>
	public partial class MainWindow : Form
	{
		/// <summary>
		/// muphic メインウィンドウの静的インスタンス。
		/// </summary>
		public static MainWindow Instance { get; private set; }


		#region フィールド / プロパティ

		/// <summary>
		/// トップ画面。
		/// </summary>
		private TopScreen TopScreen { get; set; }


		#region システム実行フラグ

		/// <summary>
		/// プログラムが動作中であることを表わす System.Boolean 値。
		/// <para>Running プロパティを使用すること。</para>
		/// </summary>
		private bool __running;

		/// <summary>
		/// プログラムが動作中であることを示す値を取得または設定する。
		/// <para>このプロパティ値が true の間、プログラムのメインループが実行される。</para>
		/// </summary>
		public static bool Running
		{
			get
			{
				return MainWindow.Instance.__running;
			}
			set
			{
				MainWindow.Instance.__running = value;				// プログラム動作の開始と終了に合わせて
				MainWindow.Instance.nitifyIcon.Visible = value;		// タスクトレイアイコンの表示/非表示も切り替える
			}
		}

		#endregion


		#region 動作モード

		/// <summary>
		/// プログラムがデバッグモードであることを表わす System.Boolean 値。
		/// <para>IsDebugMode プロパティを使用すること。</para>
		/// </summary>
		private readonly bool __isDebugMode;

		/// <summary>
		/// デバッグモードかどうかを表す System.Boolean 値を取得する。
		/// </summary>
		public static bool IsDebugMode
		{
			get { return MainWindow.Instance.__isDebugMode; }
		}


		/// <summary>
		/// muphic の動作モードを取得する。
		/// </summary>
		public static MuphicOperationMode MuphicOperationMode
		{
			get { return (MuphicOperationMode)(ConfigurationManager.Locked.MuphicOperationMode); }
		}

		#endregion


		/// <summary>
		/// 1 フレーム描画毎に更新される描画状態データを取得する。
		/// </summary>
		public static DrawStatusArgs DrawStatus { get; private set; }

		#endregion


		#region コンストラクタ (起動処理)

		/// <summary>
		/// muphic メインウィンドウを生成し、起動処理を行う。
		/// </summary>
		private MainWindow()
		{
			try
			{
				#region デバッグモードの判定
#if DEBUG
				this.__isDebugMode = true;
#else
				this.__isDebugMode = false;
#endif
				#endregion

				// メインウィンドウのフォームの設定を行う
				InitializeComponent();

				// 各管理クラスの静的インスタンス生成と初期化
				if (this.InitializeManager())
				{									// 生成と初期化に成功した場合
					this.__running = true;			// メインループ実行フラグを立てる
				}
				else
				{									// 生成と初期化に失敗した場合
					this.__running = false;			// メインループ実行フラグを降ろし
					return;							// プログラム終了
				}

				// システム及びボタン統合画像の読み込み
				this.LoadTextureFile(Settings.ResourceNames.SystemImages);

				// 各画面のファイルを予め読み込み
				if (ConfigurationManager.Current.IsLoadTextureFilePreliminarily)
				{
					// this.LoadTextureFile(Settings.ResourceNames.TopScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.CompositionScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.MakeStoryScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.EntitleScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.PlayStoryScreenImages);
					this.LoadTextureFile(Settings.ResourceNames.ScoreScreenImages);
				}

				// マウス設定
				this.IsClicked = false;					// クリック状態OFF
				this.CursorState = 0;					// カーソルの状態を初期化
				System.Windows.Forms.Cursor.Hide();		// Windows通常のマウスカーソルを隠す
				this.NowMouseLocation = new System.Drawing.Point(this.Width + 20, this.Height + 20);

				// カーソルテクスチャの登録
				DrawManager.Regist("muphicCursor", 0, 0, "IMAGE_SYSTEM_CURSOR_OFF", "IMAGE_SYSTEM_CURSOR_ON");

				// システムトレイアイコンの表示
				this.nitifyIcon.Visible = true;

				MainWindow.DrawStatus = new DrawStatusArgs();

				// NowLoadingダイアログのインスタンス化
				DrawManager.CreateNowLoaing();

				// トップ画面のインスタンス化
				TopScreen = new TopScreen(this);

				//KeyPress += new KeyPressEventHandler(key_press);
				//font = new System.Drawing.Font("MS Gothic", 12);
				//brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
			}
			catch (Exception e)
			{
				// カーソルを表示
				System.Windows.Forms.Cursor.Show();

				// 初期化中に発生した例外は Application.ThreadException イベントが発生しないので、無理矢理キャッチしログに書き込み
				LogFileManager.WriteLineError(e.ToString());

				// メッセージウィンドウの表示
				MessageBox.Show(
					CommonTools.GetResourceMessage(Properties.Resources.ErrorMsg_MainWindow_Show_UnhandledException_Text, e.Message),
					Properties.Resources.ErrorMsg_MainWindow_Show_UnhandledException,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				// メインループは実行せずプログラム終了
				this.__running = false;
			}
		}

		#endregion

	}


	/// <summary>
	/// muphic の動作を表す識別子を指定する。
	/// </summary>
	public enum MuphicOperationMode : int
	{
		/// <summary>
		/// 通常動作
		/// </summary>
		NormalMode = 0,

		/// <summary>
		/// 授業中の動作 (児童用)
		/// </summary>
		StudentMode = 1,

		/// <summary>
		/// 授業中の動作 (講師用)
		/// </summary>
		TeacherMode = 2,
	}
}
