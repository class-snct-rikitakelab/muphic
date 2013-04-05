using System;
using System.Drawing;

using Muphic.Common.DialogParts;
using Muphic.Common.DialogParts.Buttons;
using Muphic.Manager;

namespace Muphic.Common
{
	/// <summary>
	/// ダイアログの結果を示す識別子を指定する。
	/// </summary>
	public enum DialogResult
	{
		/// <summary>
		/// 結果が選択されていない状態。
		/// </summary>
		None,

		/// <summary>
		/// ダイアログの結果は OK (通常、"はい" または "けってい" ボタンが選択された場合)。
		/// </summary>
		OK,

		/// <summary>
		/// ダイアログの結果は Cancel (通常、"いいえ" または "もどる" ボタンが選択された場合)。
		/// </summary>
		Cancel,
	}

	/// <summary>
	/// ダイアログに表示するボタンの種類を指定する。
	/// </summary>
	public enum DialogButtons
	{
		/// <summary>
		/// "はい" ボタン。
		/// </summary>
		Yes,

		/// <summary>
		/// "はい" および "いいえ" ボタン。
		/// </summary>
		YesNo,

		/// <summary>
		/// ファイル選択ボックスと "もどる" ボタン。
		/// </summary>
		FileSelect,

		/// <summary>
		/// "けってい" および "もどる" ボタン。
		/// </summary>
		Decision,

		/// <summary>
		/// ボタンを表示しない。
		/// </summary>
		None,

		/// <summary>
		/// 別なボタンを作成する。
		/// </summary>
		Original,
	}

	/// <summary>
	/// ダイアログに表示するアイコンの種類を指定する。
	/// </summary>
	public enum DialogIcons
	{
		/// <summary>
		/// エクスクラメーションマーク。
		/// </summary>
		Caution,
	}


	/// <summary>
	/// 汎用ダイアログクラス
	/// <para>各部のダイアログはこのクラスを継承して作成する。</para>
	/// </summary>
	public class Dialog : Screen
	{
		/// <summary>
		/// "はい" ボタン。
		/// </summary>
		public YesButton YesButton { get; private set; }

		/// <summary>
		/// "いいえ" ボタン。
		/// </summary>
		public NoButton NoButton { get; private set; }

		/// <summary>
		/// "けってい" ボタン。
		/// </summary>
		public DecisionButton DecisionButton { get; private set; }

		/// <summary>
		/// "もどる" ボタン。
		/// </summary>
		public BackButton BackButton { get; private set; }

		/// <summary>
		/// ダイアログのタイトル。
		/// </summary>
		public DialogTitle Title { get; private set; }

		/// <summary>
		/// ダイアログのメッセージ。
		/// </summary>
		public DialogMessage Message { get; private set; }

		/// <summary>
		/// ダイアログのアイコン。
		/// </summary>
		public DialogIcon Icon { get; private set; }

		/// <summary>
		/// ダイアログ上のファイル選択領域。
		/// </summary>
		public SelectArea SelectArea { get; private set; }

		/// <summary>
		/// 選択するファイルのリストを取得または設定する。
		/// </summary>
		public string[] FileNameList { get; set; }

		/// <summary>
		/// ダイアログ上のファイル選択領域での上スクロールボタン。
		/// </summary>
		public UpperScrollButton UpperScrollButton { get; private set; }

		/// <summary>
		/// ダイアログ上のファイル選択領域での下スクロールボタン。
		/// </summary>
		public LowerScrollButton LowerScrollButton { get; private set; }

		/// <summary>
		/// ダイアログの表示位置を取得または設定する。
		/// </summary>
		public Point Location { get; private set; }


		/// <summary>
		/// ダイアログを使用する親クラスの名前を取得または設定する。
		/// </summary>
		public string ParentName { get; private set; }


		/// <summary>
		/// ダイアログの結果を示す識別子を取得または設定する。
		/// </summary>
		public virtual DialogResult DialogResult { get; set; }

		
		/// <summary>
		/// ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parentName">ダイアログの名前を表す文字列 (ダイアログを構成するボタンやパーツを識別するために必要となる)。</param>
		/// <param name="buttons">ダイアログに表示するボタン。</param>
		/// <param name="icon">ダイアログに表示するアイコン。</param>
		/// <param name="titleImage">ダイアログのタイトルテクスチャ。</param>
		/// <param name="messageImage">ダイアログのメッセージテクスチャ。</param>
		public Dialog(string parentName, DialogButtons buttons, DialogIcons icon, string titleImage, string messageImage)
		{
			this.ParentName = parentName;
			this.Initialization(buttons, icon, titleImage, messageImage);
		}
		

		/// <summary>
		/// 各パーツのインスタンス化及び初期化を行う。
		/// </summary>
		protected void Initialization(DialogButtons buttons, DialogIcons icon, string titleImage, string messageImage)
		{
			base.Initialization();

			this.DialogResult = DialogResult.None;					// ダイアログの結果を初期化

			// 画面の中央座標を算出
			Point centerPoint = new Point(Settings.System.Default.WindowSize_Width / 2, Settings.System.Default.WindowSize_Height / 2);

			// ダイアログの左上座標を算出(中央座標とダイアログのサイズから計算)
			this.Location = Tools.CommonTools.CenterToOnreft(centerPoint, TextureFileManager.GetRectangle("IMAGE_DIALOG").Size);

			// ダイアログテクスチャを登録
			DrawManager.Regist(this.ToString(), this.Location, "IMAGE_DIALOG");

			this.Title = new DialogTitle(this, titleImage);			// タイトルのインスタンス化
			this.PartsList.Add(this.Title);

			this.SetMessage(messageImage);	// メッセージの設定

			this.SetButtons(buttons);		// ボタンの設定

			switch (icon)					// 選択されたアイコンを登録する
			{
				case DialogIcons.Caution:	// ！
					this.Icon = new DialogIcon(this, "IMAGE_DIALOG_ICON_CAUTION");
					this.PartsList.Add(this.Icon);
					break;

				default:
					break;
			}
		}


		/// <summary>
		/// ダイアログのメッセージテクスチャの設定を行う。
		/// </summary>
		/// <param name="messageImage">メッセージテクスチャ名。</param>
		protected void SetMessage(string messageImage)
		{
			if (this.Message != null)
			{
				DrawManager.Delete(this.Message.ToString());
				this.PartsList.Remove(this.Message);
			}
			this.Message = new DialogMessage(this, messageImage);
			this.PartsList.Add(this.Message);
		}


		/// <summary>
		/// DialogButtons 列挙型に応じたボタンの生成を行う。
		/// <para>はい/いいえボタンのみ、既に生成したボタンを破棄し再生成を行うことができる。</para>
		/// </summary>
		/// <param name="buttons">生成するボタン。</param>
		protected void SetButtons(DialogButtons buttons)
		{
			// まず登録されている既存のボタンを削除する

			if (this.YesButton != null)
			{
				this.YesButton.Dispose();
				this.PartsList.Remove(this.YesButton);
			}
			if (this.NoButton != null)
			{
				this.NoButton.Dispose();
				this.PartsList.Remove(this.NoButton);
			}

			switch (buttons)					// 選択されたボタンのみインスタンス化を行う
			{
				case DialogButtons.Yes:			// はいボタンのみ
					this.YesButton = new YesButton(this, Locations.YesButtonCenter);
					this.PartsList.Add(this.YesButton);
					break;

				case DialogButtons.YesNo:		// はい・いいえボタン
					this.YesButton = new YesButton(this, Locations.YesButton);
					this.NoButton = new NoButton(this, Locations.NoButton);
					this.PartsList.Add(this.YesButton);
					this.PartsList.Add(this.NoButton);
					break;

				case DialogButtons.FileSelect:	// ファイル選択
					this.UpperScrollButton = new UpperScrollButton(this);
					this.LowerScrollButton = new LowerScrollButton(this);
					this.SelectArea = new SelectArea(this);
					this.BackButton = new BackButton(this, Locations.BackButton);
					this.PartsList.Add(this.SelectArea);
					this.PartsList.Add(this.BackButton);
					this.PartsList.Add(this.UpperScrollButton);
					this.PartsList.Add(this.LowerScrollButton);
					break;

				case DialogButtons.Decision:
					this.DecisionButton = new DecisionButton(this, Locations.DecisionButton);
					this.BackButton = new BackButton(this, Locations.BackButton);
					this.PartsList.Add(this.DecisionButton);
					this.PartsList.Add(this.BackButton);
					break;

				case DialogButtons.Original:
				case DialogButtons.None:
				default:
					break;
			}
		}


		/// <summary>
		/// キーボードの何らかのキーが押された際の処理。
		/// </summary>
		/// <param name="keyStatus">キーボードの状態。</param>
		public override void KeyDown(KeyboardStatusArgs keyStatus)
		{
			if (keyStatus.KeyCode == Microsoft.DirectX.DirectInput.Key.Y || keyStatus.KeyCode == Microsoft.DirectX.DirectInput.Key.Return)
			{
				// Y か Enter が押されたら OK と同じ
				if (this.YesButton != null || this.DecisionButton != null) this.DialogResult = DialogResult.OK;
			}
			else if (keyStatus.KeyCode == Microsoft.DirectX.DirectInput.Key.N)
			{
				// N が押されたら Cancel と同じ
				if (this.BackButton != null || this.NoButton != null) this.DialogResult = DialogResult.Cancel;
			}
			else if (keyStatus.KeyCode == Microsoft.DirectX.DirectInput.Key.Up)
			{
				// ↑が押されたら、上スクロールと同じ
				if (this.FileNameList != null) this.UpperScrollButton.UpperScroll();
			}
			else if (keyStatus.KeyCode == Microsoft.DirectX.DirectInput.Key.Down)
			{
				// ↓が押されたら、下スクロールと同じ
				if (this.FileNameList != null) this.LowerScrollButton.LowerScroll();
			}
		}


		/// <summary>
		/// ダイアログの描画を行う。
		/// </summary>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			if (!drawStatus.ShowDialog)		// 既にダイアログが描画されている場合を除き
			{								// まず黒背景を半透明で描画
				DrawManager.Draw("IMAGE_COMMON_BACKGROUND", 0, 0, Color.FromArgb(100, 0, 0, 0));
				drawStatus.ShowDialog = true;
			}

			base.Draw(drawStatus);			// その他ダイアログ部品の描画
		}

	}
}
