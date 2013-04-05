using System.Drawing;
using Muphic.Common;
using Muphic.Common.DialogParts;
using Muphic.Manager;
using Muphic.Tools.IO;

namespace Muphic.MakeStoryScreenParts.Dialogs
{
	/// <summary>
	/// 物語作成画面の提出確認ダイアログクラス。
	/// </summary>
	public class SubmitDialog : Dialog
	{
		/// <summary>
		/// 親にあたる物語作成画面。
		/// <para>Parent プロパティを使用すること。</para>
		/// </summary>
		private readonly MakeStoryScreen __parent;

		/// <summary>
		/// 親にあたる物語作成画面を取得する。
		/// </summary>
		public MakeStoryScreen Parent
		{
			get { return this.__parent; }
		}


		/// <summary>
		/// 提出可能かどうかを表わす bool 値。
		/// EnabledSave プロパティを使用すること。
		/// </summary>
		private bool __enabledSubmit = false;

		/// <summary>
		/// 提出可能かどうかを示す値を取得または設定する。
		/// このプロパティ値が true の間のみ提出可能となる (タイトル未設定時等提出できない状態の場合は false になる)。
		/// </summary>
		public bool EnabledSubmit
		{
			get
			{
				return this.__enabledSubmit;
			}
			set
			{
				this.__enabledSubmit = value;
				this.SetButtons(value ? DialogButtons.YesNo : DialogButtons.Yes);
				this.SetMessage(value ? "IMAGE_MAKESTORYSCR_DIAGMSG_SUBMIT" : "IMAGE_MAKESTORYSCR_DIAGMSG_NOTSAVE");
			}
		}


		/// <summary>
		/// ユーザーにより提出ボタンが押され、ファイルの非同期コピーの完了待ち状態であるかどうかを示す値。
		/// </summary>
		private bool __isWaitingSubmit;

		/// <summary>
		/// ユーザーにより提出ボタンが押され、ファイルの非同期コピーの完了待ち状態であるかどうかを示す値を取得または設定する。
		/// </summary>
		private bool IsWaitingSubmit
		{
			get
			{
				return this.__isWaitingSubmit;
			}
			set
			{
				this.__isWaitingSubmit = value;

				if (value)
				{
					this.SetButtons(DialogButtons.None);
					this.SetMessage("IMAGE_MAKESTORYSCR_DIAGMSG_SUBMIT_NOW");
					this.WaitingAnimation.Start();
				}
			}
		}


		/// <summary>
		/// 作品の提出に失敗した場合で、ユーザーの承諾待ちであるかどうかを示す値。
		/// </summary>
		private bool __isFailureSubmit;

		/// <summary>
		/// 作品の提出に失敗した場合で、ユーザーの承諾待ちであるかどうかを示す値を取得または設定する。
		/// </summary>
		private bool IsFailureSubmit
		{
			get
			{
				return this.__isFailureSubmit;
			}
			set
			{
				this.__isFailureSubmit = value;

				if (value)
				{
					this.SetButtons(DialogButtons.Yes);
					this.SetMessage("IMAGE_MAKESTORYSCR_DIAGMSG_SUBMIT_FAIL");
				}
			}
		}


		/// <summary>
		/// 作品の提出に失敗した場合で、ユーザーの承諾待ちであるかどうかを示す値。
		/// </summary>
		private bool __isSuccessSubmit;

		/// <summary>
		/// 作品の提出に失敗した場合で、ユーザーの承諾待ちであるかどうかを示す値を取得または設定する。
		/// </summary>
		private bool IsSuccessSubmit
		{
			get
			{
				return this.__isSuccessSubmit;
			}
			set
			{
				this.__isSuccessSubmit = value;

				if (value)
				{
					this.SetButtons(DialogButtons.Yes);
					this.SetMessage("IMAGE_MAKESTORYSCR_DIAGMSG_SUBMIT_FAIL");
				}
			}
		}


		/// <summary>
		/// 待機中のアニメーション表示
		/// </summary>
		private WaitingAnimation WaitingAnimation { get; set; }

		/// <summary>
		/// 提出確認ダイアログの結果を示す識別子を取得または設定する。
		/// </summary>
		public override DialogResult DialogResult
		{
			get
			{
				return base.DialogResult;
			}
			set
			{
				base.DialogResult = value;

				switch (value)
				{
					case DialogResult.OK:					// "はい"ボタンが押された場合
						if (this.IsFailureSubmit)				// 提出失敗後のユーザーの承認待ちだった場合
						{										// 待ちを解除し
							this.IsFailureSubmit = false;		// 制御権を物語作成画面に戻す
							this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
						}
						else if (this.IsSuccessSubmit)			// 提出成功後のユーザーの認証待ちだった場合
						{										// 待ちを解除し
							this.IsSuccessSubmit = false;		// 制御権を物語作成画面に戻す
							this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
						}
						else if (this.EnabledSubmit)			// 提出可能状態であれば (はい/いいえ のうち はい が選択された場合)、ファイル提出
						{										// まずローカルへ書き込むためのパス生成し (通常の保存と同じ)、ローカルへ保存
							string savePath = XmlFileWriter.CreateStoryDataPath(this.Parent.CurrentStoryData.Title);
							XmlFileWriter.WriteSaveData<Muphic.PlayerWorks.StoryData>(this.Parent.CurrentStoryData, true, savePath);

							if (NetworkManager.BeginCopyFile(savePath, ConfigurationManager.Current.SubmissionPath))
							{									// 非同期コピーを開始する
								this.IsWaitingSubmit = true;	// コピーの完了待ちフラグを立てる
							}
							else								// 非同期コピーが開始できなかった場合
							{									// 制御権を物語作成画面に戻す
								this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
							}
						}
						else									// 提出可能状態でなければ (タイトル未設定の場合)、
						{										// 制御権を物語作成画面に戻す
							this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
						}
						break;

					case DialogResult.Cancel:											// "いいえ"ボタンが押された場合
						this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;	// 制御権をものがたりおんがくモードに戻す
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}


		/// <summary>
		/// 物語作成画面の提出確認ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public SubmitDialog(MakeStoryScreen parent)
			: base("SubmitDialog_MakeStoryScreen", DialogButtons.None, DialogIcons.Caution, "IMAGE_MAKESTORYSCR_DIAGTITLE_SUBMIT", "IMAGE_MAKESTORYSCR_DIAGMSG_SUBMIT")
		{
			this.__parent = parent;

			this.DialogResult = DialogResult.None;

			this.EnabledSubmit = false;
			this.IsFailureSubmit = false;
			this.IsSuccessSubmit = false;
			this.IsWaitingSubmit = false;

			this.WaitingAnimation = new WaitingAnimation(
				Locations.SubmitDialogWaitingAnimationTimeSpan,
				new Point(
					this.Location.X + Locations.SubmitDialogWaitingAnimation.X,
					this.Location.Y + Locations.SubmitDialogWaitingAnimation.Y),
				Locations.SubmitDialogWaitingAnimationNum,
				Locations.SubmitDialogWaitingAnimationLocationSpan
			);
		}


		/// <summary>
		/// 物語作成画面の提出確認ダイアログを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			if (this.IsWaitingSubmit)					// 非同期コピー終了待ちの場合は別処理を行う
			{
				if (!NetworkManager.IsCopyWorking)		// 非同期コピーが完了していた場合
				{										// コピーの結果を取得
					bool result = NetworkManager.EndCopyFile();

					if (result)							// コピーに成功した場合
					{									// 結果をログ出力し、制御権を戻す
						LogFileManager.WriteLine(
							Properties.Resources.Msg_MakeStoryScr_Submit,
							Properties.Resources.Msg_MakeStoryScr_Submit_Success
						);
						this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;
					}
					else								// コピーに失敗した場合
					{									// 結果をログ出力し、承認待ちへ移行
						LogFileManager.WriteLine(
							Properties.Resources.Msg_MakeStoryScr_Submit,
							Properties.Resources.Msg_MakeStoryScr_Submit_Failure
						);
						this.IsFailureSubmit = true;
					}

					this.IsWaitingSubmit = false;		// 非同期コピー終了待ちフラグを降ろす
				}
				else									// 非同期コピーが完了していない場合
				{										// アニメーションを表示する
					this.WaitingAnimation.Draw(drawStatus);
				}
			}
		}

	}
}
