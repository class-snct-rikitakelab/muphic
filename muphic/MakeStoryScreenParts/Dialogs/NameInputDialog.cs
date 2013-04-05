using System;
using System.Collections.Generic;

using Muphic.Common;
using Muphic.Manager;
using Muphic.MakeStoryScreenParts.Dialogs.DialogParts;
using Muphic.MakeStoryScreenParts.SubScreens;
using Muphic.PlayerWorks;

namespace Muphic.MakeStoryScreenParts.Dialogs
{
	/// <summary>
	/// [使用しないでくりゃれ。] 物語制作者名入力ダイアログで、現在表示すべき画面を示す識別子を指定する。
	/// </summary>
	public enum NameInputDialogMode
	{
		/// <summary>
		/// ダイアログ。
		/// </summary>
		NameInputDialog,

		/// <summary>
		/// 制作者名入力画面。
		/// </summary>
		NameInputScreen,
	}

	/// <summary>
	/// [使用しないでくりゃれ。] 物語作成画面の制作者名入力ダイアログ。
	/// </summary>
	public class NameInputDialog : Dialog, IDisposable
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
		/// 現在編集中の物語の制作者名のリストを取得する。
		/// </summary>
		public List<Author> Authors
		{
			get { return this.Parent.CurrentStoryData.Authors; }
		}


		#region DialogMode

		/// <summary>
		/// 現在表示すべき画面を示す識別子。
		/// </summary>
		private NameInputDialogMode __nameInputDialogMode;

		/// <summary>
		/// 現在表示すべき画面を示す識別子を取得または設定する。
		/// </summary>
		public NameInputDialogMode DialogMode
		{
			get
			{
				return this.__nameInputDialogMode;
			}
			set
			{
				switch (value)
				{
					case NameInputDialogMode.NameInputDialog:
						// 制作者名をタイトル表示へコピー
						for (int i = 0; i < this.AuthorTexts.Length && i < this.Authors.Count; i++)
						{
							this.AuthorTexts[i].Text = this.Authors[i].Name;
						}
						break;

					case NameInputDialogMode.NameInputScreen:
						break;
				}

				this.__nameInputDialogMode = value;
			}
		}

		#endregion
		

		/// <summary>
		/// 制作者名入力画面。
		/// </summary>
		public NameInputScreen_ NameInputScreen { get; private set; }

		/// <summary>
		/// "なまえ" ボタン群。
		/// </summary>
		public NameInputButton[] NameInputButtons { get; private set; }

		/// <summary>
		/// 制作者名を表示する文字列描画クラス群。
		/// </summary>
		public Title[] AuthorTexts { get; private set; }


		#region DialogResult

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
					case DialogResult.OK:												// "はい"ボタンが押された場合
						this.Parent.ScreenMode = MakeStoryScreenMode.SubmitDialog;		// 提出ダイアログを表示
						this.Close();
						break;

					case DialogResult.Cancel:											// "いいえ"ボタンが押された場合
						this.Parent.ScreenMode = MakeStoryScreenMode.MakeStoryScreen;	// 制御権をものがたりおんがくモードに戻す
						this.Close();
						break;

					case DialogResult.None:
					default:
						break;
				}
			}
		}

		#endregion


		/// <summary>
		/// 制作者名入力ダイアログの新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたる物語作成画面。</param>
		public NameInputDialog(MakeStoryScreen parent)
			: base("MakeStory_NameInputDialog", DialogButtons.Decision, DialogIcons.Caution, "IMAGE_MAKESTORYSCR_DIAGTITLE_NAMEINPUT", "IMAGE_DUMMY")
		{
			this.__parent = parent;

			this.NameInputButtons = new NameInputButton[ConfigurationManager.Current.UserNum];
			this.AuthorTexts = new Title[ConfigurationManager.Current.UserNum];

			for (int i = 0; i < ConfigurationManager.Current.UserNum; i++)
			{
				this.NameInputButtons[i] = new NameInputButton(this, Tools.CommonTools.AddPoints(this.Location, Locations.NameInputButtonLocations[i]), i);
				this.AuthorTexts[i] = new Title(this.ToString() + ".Title" + i.ToString(), Tools.CommonTools.AddPoints(Locations.NameInputTextLocations[i], this.Location), 14);
				this.PartsList.Add(this.NameInputButtons[i]);
				this.PartsList.Add(this.AuthorTexts[i]);
			}

			this.DialogResult = DialogResult.None;
		}


		/// <summary>
		/// 制作者名入力ダイアログを表示する。
		/// </summary>
		public void Show()
		{
			// まず、物語データの制作者名が人数分作成されていなかったら新しく作成する
			for (int i = this.Authors.Count; i < ConfigurationManager.Current.UserNum; i++)
			{
				this.Parent.CurrentStoryData.Authors.Add(new Author());
			}

			// 制作者名入力画面が生成されていなければ生成する
			if (this.NameInputScreen == null) this.NameInputScreen = new NameInputScreen_(this, "");

			// 表示モードをダイアログに
			this.DialogMode = NameInputDialogMode.NameInputDialog;
		}


		/// <summary>
		/// 制作者名入力ダイアログを閉じる。
		/// </summary>
		private void Close()
		{
			this.SafeDispose(this.NameInputScreen);
			this.NameInputScreen = null;
		}


		/// <summary>
		/// 制作者名入力ダイアログを描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			switch (this.DialogMode)
			{
				case NameInputDialogMode.NameInputDialog:
					base.Draw(drawStatus);
					DrawManager.Draw("IMAGE_MAKESTORYSCR_DIAGMSG_NAMEINPUT", this.Location.X + 45, this.Location.Y + 180);
					break;

				case NameInputDialogMode.NameInputScreen:
					this.NameInputScreen.Draw(drawStatus);
					break;
			}
		}

		/// <summary>
		/// 制作者名入力ダイアログがクリックされた際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態。</param>
		public override void Click(MouseStatusArgs mouseStatus)
		{
			switch (this.DialogMode)
			{
				case NameInputDialogMode.NameInputDialog:
					base.Click(mouseStatus);
					break;

				case NameInputDialogMode.NameInputScreen:
					this.NameInputScreen.Click(mouseStatus);
					break;
			}
		}

		/// <summary>
		/// 制作者名入力ダイアログを表示中にマウスが移動した際の処理。
		/// </summary>
		/// <param name="mouseStatus">マウスの状態。</param>
		public override void MouseMove(MouseStatusArgs mouseStatus)
		{
			switch (this.DialogMode)
			{
				case NameInputDialogMode.NameInputDialog:
					base.MouseMove(mouseStatus);
					break;

				case NameInputDialogMode.NameInputScreen:
					this.NameInputScreen.MouseMove(mouseStatus);
					break;
			}
		}


		/// <summary>
		/// このダイアログで利用したリソースを解放する。
		/// </summary>
		public override void Dispose()
		{
			this.Close();
			base.Dispose();
		}

	}
}
