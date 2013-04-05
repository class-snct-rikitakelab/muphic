using System;

using Muphic.Common.ExplorerParts;
using Muphic.Common.ExplorerParts.Buttons;
using Muphic.Manager;

namespace Muphic.Common
{
	/// <summary>
	/// 特定のディレクトリ内のディレクトリ及びファイルを管理するファイル エクスプローラ。
	/// </summary>
	public abstract class Explorer : Screen, IDisposable
	{

		#region フィールド / プロパティ

		/// <summary>
		/// 管理する対象のファイルの種類。
		/// </summary>
		private ExplorerTargetType __targetType;

		/// <summary>
		/// 管理する対象のファイルの種類を取得する。
		/// </summary>
		public ExplorerTargetType TargetType
		{
			get { return this.__targetType; }
		}


		/// <summary>
		/// 対象のディレクトリのパスを取得する。
		/// </summary>
		public string TargetDirectory { get; private set; }

		/// <summary>
		/// 親の画面の名称を取得する。
		/// </summary>
		public string ParentName { get; private set; }

		/// <summary>
		/// 対象のファイルの種類を指定する拡張子を取得または設定する。
		/// </summary>
		public string TargetExtension { get; private set; }


		#region 領域とボタンたち

		/// <summary>
		/// ディレクトリ選択領域。
		/// </summary>
		public DirectorySelectArea DirectorySelectArea { get; private set; }

		/// <summary>
		/// ファイル選択領域。
		/// </summary>
		public FileSelectArea FileSelectArea { get; private set; }

		/// <summary>
		/// 作品情報表示領域。
		/// </summary>
		public WorkInfoArea WorkInfoArea { get; private set; }

		/// <summary>
		/// "読込" ボタン。
		/// </summary>
		public LoadButton LoadButton { get; private set; }

		/// <summary>
		/// "キャンセル" ボタン。
		/// </summary>
		public CancelButton CancelButton { get; private set; }

		/// <summary>
		/// "印刷" ボタン。
		/// </summary>
		public PrintButton PrintButton { get; private set; }

		#endregion


		/// <summary>
		/// ダイアログの結果を示す識別子を取得または設定する。
		/// </summary>
		public virtual DialogResult DialogResult { get; set; }

		/// <summary>
		/// ファイル エクスプローラで最終的に得られたパスを取得する。
		/// </summary>
		public string ResultPath
		{
			get { return this.FileSelectArea.SelectedFilePath; }
		}


		#region キーフォーカス関連

		/// <summary>
		/// キーボード操作の際に、どの部品の操作をするかを示す識別子を指定する。
		/// </summary>
		public enum KeyFocus
		{
			/// <summary>
			/// ディレクトリ選択領域。
			/// </summary>
			DirectorySelectArea,

			/// <summary>
			/// ファイル選択領域。
			/// </summary>
			FileSelectArea,
		}

		/// <summary>
		/// 現在のキーボード操作の対象を示す識別子を取得または設定する。
		/// </summary>
		public KeyFocus NowKeyFocus { get; set; }

		#endregion

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// ファイル エクスプローラの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="targetType">管理する対象のファイルの種類。</param>
		/// <param name="targetDirectory">対象のディレクトリのパス。</param>
		/// <param name="parentName">親の画面の名称。</param>
		/// <param name="targetExtension">対象のファイルの種類を指定する拡張子。</param>
		public Explorer(ExplorerTargetType targetType, string targetDirectory, string parentName, string targetExtension)
		{
			this.__targetType = targetType;
			this.TargetDirectory = targetDirectory;
			this.ParentName = parentName;

			this.Initialization();

			this.NowKeyFocus = KeyFocus.DirectorySelectArea;	// キー操作対象はデフォルトでディレクトリ選択領域
			this.DialogResult = DialogResult.None;				// ダイアログの結果は見選択
			this.DirectorySelectArea.Update();					// ディレクトリのリストの最初の更新
			this.WorkInfoArea.Clear();							// 作品の表示をクリア
		}


		/// <summary>
		/// ファイル エクスプローラの初期化を行う。
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization(Settings.ResourceNames.ExplorerImages);

			#region 統合画像の読み込み

			this.LoadImageFiles();

			#endregion

			#region 部品のインスタンス化

			this.DirectorySelectArea = new DirectorySelectArea(this);
			this.FileSelectArea = new FileSelectArea(this);
			this.WorkInfoArea = new WorkInfoArea(this);
			this.LoadButton = new LoadButton(this);
			this.CancelButton = new CancelButton(this);
			this.PrintButton = new PrintButton(this);

			#endregion

			#region 部品のリストへの登録

			this.PartsList.Add(this.DirectorySelectArea);
			this.PartsList.Add(this.FileSelectArea);
			this.PartsList.Add(this.WorkInfoArea);
			this.PartsList.Add(this.LoadButton);
			this.PartsList.Add(this.CancelButton);
			this.PartsList.Add(this.PrintButton);

			#endregion

			#region テクスチャと座標の登録

			DrawManager.Regist(this.ToString(), Muphic.Common.ExplorerParts.Settings.ExplorerArea.Location, "IMAGE_EXPLORER_BG");

			#endregion

			// 登録終了
			DrawManager.EndRegist();
		}

		#endregion


		#region キーボード

		/// <summary>
		/// ディレクトリ選択領域でキーボード操作が行われた際の処理。
		/// </summary>
		/// <param name="keyStatus">キーボードの状態。</param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			base.KeyDown(keyStatus);

			if (this.NowKeyFocus == KeyFocus.DirectorySelectArea)
			{
				// キー操作対象がディレクトリ選択領域の場合

				switch (keyStatus.KeyCode)
				{
					case Microsoft.DirectX.DirectInput.Key.Up:
						this.DirectorySelectArea.UpperSelect();
						break;

					case Microsoft.DirectX.DirectInput.Key.Down:
						this.DirectorySelectArea.LowerSelect();
						break;

					case Microsoft.DirectX.DirectInput.Key.Right:	// 右キー押下
						this.DirectorySelectArea.Select();			// 選択中のディレクトリがあればそのディレクトリを選択
						break;

					case Microsoft.DirectX.DirectInput.Key.F5:
						this.DirectorySelectArea.Update();
						break;

					case Microsoft.DirectX.DirectInput.Key.Return:	// Enter キーが押された場合
					case Microsoft.DirectX.DirectInput.Key.NumPadEnter:
						this.DirectorySelectArea.Select();			// 選択中のディレクトリがあればそのディレクトリを選択
						break;

					default:
						break;
				}
			}
			else if (this.NowKeyFocus == KeyFocus.FileSelectArea)
			{
				// キー操作対象がファイル選択領域の場合

				switch (keyStatus.KeyCode)
				{
					case Microsoft.DirectX.DirectInput.Key.Up:
						this.FileSelectArea.UpperSelect();
						break;

					case Microsoft.DirectX.DirectInput.Key.Down:
						this.FileSelectArea.LowerSelect();
						break;

					case Microsoft.DirectX.DirectInput.Key.Left:			// 左キー押下
						this.NowKeyFocus = KeyFocus.DirectorySelectArea;	// ディレクトリ選択領域にフォーカスを戻し
						this.FileSelectArea.Focus(-1);						// ファイル選択のフォーカスを解除
						break;

					case Microsoft.DirectX.DirectInput.Key.Right:			// 右キー押下
						if (this.FileSelectArea.FocusedIndex >= 0)			// 選択中のファイルがあれば
						{													// そのファイルを選択する
							this.FileSelectArea.Select();
						}
						break;

					case Microsoft.DirectX.DirectInput.Key.F5:
						this.NowKeyFocus = KeyFocus.DirectorySelectArea;
						this.DirectorySelectArea.Update();
						break;

					case Microsoft.DirectX.DirectInput.Key.Return:			// Enter キーが押された場合
					case Microsoft.DirectX.DirectInput.Key.NumPadEnter:
						if (this.LoadButton.Enabled) this.DialogResult = DialogResult.OK;
						else this.FileSelectArea.Select();					// ファイルが選択された状態ならそのファイルを読み込んで終了
						break;												// それ以外の場合はフォーカスされているファイルを選択

					default:
						break;
				}
			}
		}

		#endregion


		#region 表示の更新

		/// <summary>
		/// 表示を更新する。
		/// </summary>
		public void Update()
		{
			this.DirectorySelectArea.Update();		// ディレクトリ一覧を取得
			this.FileSelectArea.Clear();			// ファイル一覧を更新
			this.WorkInfoArea.Clear();				// 作品の表示をクリア
		}

		#endregion


		#region 解放 / その他

		/// <summary>
		/// ファイル エクスプローラで使用したリソースを解放する。
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
		}


		/// <summary>
		/// 現在の System.Object に、エクスプローラの親の画面の名称を付加した System.String を返す。
		/// </summary>
		/// <returns>現在の System.Object に、エクスプローラの親の画面の名称を付加した System.String。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.ParentName;
		}

		#endregion

	}


	/// <summary>
	/// ファイル エクスプローラで管理するファイルの種類の識別子を指定する。
	/// </summary>
	public enum ExplorerTargetType
	{
		/// <summary>
		/// 物語データ。
		/// </summary>
		StoryData,
	}
}
