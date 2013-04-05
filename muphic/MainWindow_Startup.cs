using System.IO;
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
		// =======================================
		// muphic の起動時に実行されるコードを記述
		// =======================================


		#region 起動処理 - 多重起動チェック

		/// <summary>
		/// 多重起動の阻止に使用する同期オブジェクト
		/// </summary>
		private static System.Threading.Mutex __mutex;

		/// <summary>
		/// muphic が既に起動されているかをチェックする。
		/// </summary>
		/// <returns>既に起動されていると判断した場合は false、それ以外の場合は true。</returns>
		private static bool CheckMultipleStartUp()
		{
			// 同時に1つのスレッドでしか所有できない同期オブジェクトであるミューテックスを使用する

			// ミューテックスの初期所有権
			bool createdNew;

			// ミューテックスクラスの生成
			// 同じ名前のミューテックスが既に存在している場合、createNewにfalseが格納される
			Muphic.MainWindow.__mutex = new System.Threading.Mutex(true, Muphic.Settings.System.Default.ApplicationMutexName, out createdNew);

			if (createdNew)
			{
				// 初期所有権が得られた場合、初めて起動されると判断する
				DebugTools.ConsolOutputMessage(
					Properties.Resources.Msg_MainWindow_CheckMultipleStartup,
					Properties.Resources.Msg_MainWindow_CheckMultipleStartup_Success,
					false
				);

				return true;
			}
			else
			{
				// 初期所有権が付与されなかった場合は既に起動していると判断する
				DebugTools.ConsolOutputMessage(
					Properties.Resources.Msg_MainWindow_CheckMultipleStartup,
					Properties.Resources.Msg_MainWindow_CheckMultipleStartup_Failure,
					false
				);

				return false;
			}
		}

		#endregion


		#region 起動処理 - 必須ファイルチェック

		/// <summary>
		/// muphic の起動に必要なファイルが存在するかどうかチェックする。
		/// </summary>
		/// <returns>起動に必要なファイルが存在する場合は true、それ以外 (起動できない場合) は false。</returns>
		private static bool CheckEssentialFiles()
		{
			// エラー表示用の文字列　(この文字列が空ならば、エラー無く起動可能であるということになる)
			string errorMsg = "";

			foreach (string file in Settings.ResourceNames.EssentialFiles)
			{
				// ファイル名とそのファイルの説明の文字列を分割する。
				string[] fileInfo = file.Split(new string[] { "|" }, System.StringSplitOptions.RemoveEmptyEntries);

				// ファイルが存在しなければ、エラーメッセージの文字列にファイル名とその説明の文字列を追加する。
				if (!File.Exists(fileInfo[0])) errorMsg += string.Format(" ・{0} ({1})\n", fileInfo[0], fileInfo[1]);
			}

			// エラー文字列が空でなければ、メッセージを表示して修了
			if (!string.IsNullOrEmpty(errorMsg))
			{
				MessageBox.Show(
					CommonTools.GetResourceMessage(Properties.Resources.ErrorMsg_MainWindow_Show_CheckEssentialFiles_Text, errorMsg),
					Properties.Resources.ErrorMsg_MainWindow_Show_CheckEssentialFiles_Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				return false;
			}

			return true;
		}

		#endregion


		#region 起動処理 - 管理クラス生成

		/// <summary>
		/// 各管理クラスの静的インスタンス生成とクラス変数の初期化を行う。
		/// </summary>
		/// <remarks>
		/// 管理クラスは全てシングルトンパターンで記述されているため、ここで全管理クラスのインスタンスを生成する。
		/// <para>また、初期化に失敗した場合はその場で MainScreen のインスタンス生成を中止し、プログラムを終了する。</para>
		/// </remarks>
		/// <returns>正常に初期化が終了した場合は true、それ以外は false。</returns>
		private bool InitializeManager()
		{
			if (!DrawManager.Initialize(this)) return false;			// 描画管理クラス
			if (!SoundManager.Initialize(this)) return false;			// 音声管理クラス
			if (!KeyboardInputManager.Initialize(this)) return false;	// キーボード入力管理クラス
			if (!PrintManager.Initialize()) return false;				// 印刷管理クラス
			if (!NetworkManager.Initialize()) return false;				// ネットワーク管理クラス
			FrameManager.Initialize();									// フレーム管理クラス
			TextureFileManager.Initialize();							// テクスチャファイル管理クラス
			TextureNameManager.Initialize();							// テクスチャ名管理クラス
			RectangleManager.Initialize();								// 矩形管理クラス
			StringManager.Initialize();									// 文字列管理クラス
			JpnLangInputManager.Initialize();							// 日本語入力管理クラス

			DebugTools.ConsolOutputMessage("MainScreen -InitializeManager", "各管理クラス初期化完了", true);
			DebugTools.ConsolOutputMessage(" ", "", true);

			return true;
		}

		#endregion


		#region 起動処理 - テクスチャ読込

		/// <summary>
		/// 指定した統合画像ファイルを全て読み込む。
		/// </summary>
		/// <param name="resourceFileNames">統合画像ファイル名 (セミコロン区切りで複数指定可能)。</param>
		private void LoadTextureFile(string resourceFileNames)
		{
			// セミコロンで分割されたファイル名を、それぞれ先頭と末尾にある空白を削除してリストに追加。
			foreach (string fileName in resourceFileNames.Split(new string[] { ";" }, System.StringSplitOptions.RemoveEmptyEntries))
			{
				if (!string.IsNullOrEmpty(fileName.Trim()) && !DrawManager.ExistsTextureFile(fileName.Trim()))
				{
					DrawManager.LoadTextureFile(fileName.Trim());
				}
			}
		}

		/// <summary>
		/// 指定した統合画像ファイルを全て読み込む。
		/// </summary>
		/// <param name="resourceFileNames">統合画像ファイル名。</param>
		private void LoadTextureFile(string[] resourceFileNames)
		{
			// セミコロンで分割されたファイル名を、それぞれ先頭と末尾にある空白を削除してリストに追加。
			foreach (string fileName in resourceFileNames)
			{
				if (!string.IsNullOrEmpty(fileName.Trim()) && !DrawManager.ExistsTextureFile(fileName.Trim()))
				{
					DrawManager.LoadTextureFile(fileName.Trim());
				}
			}
		}

		#endregion

	}
}
