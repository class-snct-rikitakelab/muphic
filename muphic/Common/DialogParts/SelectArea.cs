using Muphic.Common.DialogParts.Buttons;

namespace Muphic.Common.DialogParts
{
	/// <summary>
	/// ダイアログのファイル選択領域
	/// </summary>
	public class SelectArea : Screen
	{
		/// <summary>
		/// 親にあたるダイアログ。
		/// </summary>
		public Dialog Parent { get; private set; }

		/// <summary>
		/// 選択されたファイルを表わす Dialog.FileNameList 内のインデックス番号を取得または設定する。。
		/// </summary>
		public int Result { get; set; }

		/// <summary>
		/// ファイル選択ボタン。
		/// </summary>
		public SelectButton[] SelectButtons { get; set; }

		/// <summary>
		/// ファイルリストの1行目のファイルの番号を表わす整数。
		/// <para>NowPage プロパティを使用すること。</para>
		/// </summary>
		private int __nowPage;

		/// <summary>
		/// ファイルリストの1行目のファイルの番号を表わす整数。
		/// </summary>
		public int NowPage
		{
			get
			{
				return this.__nowPage;
			}
			set
			{
				if (this.Parent.FileNameList.Length <= 4)
				{
					// 選択対象のファイル数が4未満だった場合は0固定、スクロールボタン非表示
					this.__nowPage = 0;
					this.Parent.UpperScrollButton.Visible = false;
					this.Parent.LowerScrollButton.Visible = false;
				}
				else
				{
					if (value <= 0)
					{
						this.__nowPage = 0;
						this.Parent.UpperScrollButton.Visible = false;
						this.Parent.LowerScrollButton.Visible = true;
					}
					else if (value >= this.Parent.FileNameList.Length - 4)
					{
						this.__nowPage = this.Parent.FileNameList.Length - 4;
						this.Parent.UpperScrollButton.Visible = true;
						this.Parent.LowerScrollButton.Visible = false;
					}
					else
					{
						this.__nowPage = value;
						this.Parent.UpperScrollButton.Visible = true;
						this.Parent.LowerScrollButton.Visible = true;
					}
				}
			}
		}


		/// <summary>
		/// ファイル選択領域の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="dialog">親にあたるダイアログ。</param>
		public SelectArea(Dialog dialog)
		{
			this.Parent = dialog;

			this.Result = 0;

			// ファイル選択ボタンをインスタンス化
			this.SelectButtons = new SelectButton[4];
			for (int i = 0; i < SelectButtons.Length; i++)
			{
				this.SelectButtons[i] = new SelectButton(this, i);
				this.PartsList.Add(this.SelectButtons[i]);
			}

			Manager.DrawManager.Regist(this.ToString(), Tools.CommonTools.AddPoints(this.Parent.Location, Locations.SelectArea), "IMAGE_DIALOG_FILESELECT_BG");
		}


		/// <summary>
		/// ファイル選択領域の描画を行う。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);
		}


		/// <summary>
		/// ダイアログ表示前に実行する。
		/// </summary>
		public void Initialization(string[] fileList)
		{
			this.Parent.FileNameList = fileList;
			this.NowPage = 0;

			// 選択対象のファイル数が4未満だった場合はファイル数に合わせ選択ボタンを非表示にする
			for (int i = 0; i < 4; i++)
			{
				this.SelectButtons[i].Visible = (this.Parent.FileNameList.Length > i);
				this.SelectButtons[i].State = 0;
			}
		}


		/// <summary>
		/// 現在の System.Object に、親ダイアログの名前を付加した System.String を返す。
		/// </summary>
		/// <returns>現在のファイル選択領域に親ダイアログの名前を付加した System.String。</returns>
		public override string ToString()
		{
			return base.ToString() + "." + this.Parent.ParentName;
		}
	}
}
