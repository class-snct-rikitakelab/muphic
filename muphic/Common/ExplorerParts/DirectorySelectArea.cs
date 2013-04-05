using System;
using System.IO;

using Muphic.Common.ExplorerParts.Buttons;
using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.Common.ExplorerParts
{
	/// <summary>
	/// エクスプローラのディレクトリ選択領域。
	/// </summary>
	public class DirectorySelectArea : Screen
	{

		#region プロパティ / インデクサ

		/// <summary>
		/// 親にあたるエクスプローラ。
		/// </summary>
		public Explorer Parent { get; private set; }

		/// <summary>
		/// 選択されているディレクトリを示すインデックス番号を取得または設定する。
		/// </summary>
		public int FocusedIndex { get; private set; }


		#region ディレクトリのリスト関連

		/// <summary>
		/// 対象のディレクトリ内にある、ディレクトリ名の先頭にシャープ ("#") があるものを除いたディレクトリ数を取得する。
		/// </summary>
		public int DirectoryNum { get; private set; }

		/// <summary>
		/// 対象のディレクトリ内にあるディレクトリ名のリストを取得または設定する。
		/// <para>このリストに格納されるディレクトリ名は表示用であり、実際のディレクトリパスは DirectoryPathList プロパティで取得する。</para>
		/// </summary>
		private string[] DirectoryNameList { get; set; }

		/// <summary>
		/// 対象のディレクトリ内にあるディレクトリパスのリストを取得または設定する。
		/// <para>このリストに格納されるパスは読み込み時に利用するものであり、表示するディレクトリ名は DirectoryNameList プロパティで取得する。</para>
		/// </summary>
		private string[] DirectoryPathList { get; set; }

		/// <summary>
		/// 指定したインデックス番号のディレクトリ名を取得する。
		/// </summary>
		/// <param name="index">取得するディレクトリ名のインデックス番号。</param>
		/// <returns>インデックス番号に対応したディレクトリ名。</returns>
		public string this[int index]
		{
			get { return this.DirectoryNameList[index]; }
		}

		/// <summary>
		/// 現在選択中のディレクトリパスを取得する。何も選択されていない場合は空の文字列。
		/// </summary>
		public string SelectedDirectoryPath
		{
			get
			{
				if (this.FocusedIndex < 0) return "";
				else return this.DirectoryPathList[this.FocusedIndex];
			}
		}

		#endregion


		#region ボタン

		/// <summary>
		/// ディレクトリ選択ボタン。
		/// </summary>
		private DirectorySelectButton[] SelectButtons { get; set; }

		/// <summary>
		/// 上スクロールボタン。
		/// </summary>
		public DirectoryUpperScrollButton UpperScrollButton { get; private set; }

		/// <summary>
		/// 下スクロールボタン。
		/// </summary>
		public DirectoryLowerScrollButton LowerScrollButton { get; private set; }

		/// <summary>
		/// 更新ボタン。
		/// </summary>
		public ReloadButton ReloadButton { get; private set; }

		#endregion


		#region スクロールとページ

		/// <summary>
		/// ディレクトリ選択領域リストの 1 行目に表示される、対象ディレクトリ内にあるディレクトリ名のリストのインデックス番号。
		/// </summary>
		private int __nowPage;

		/// <summary>
		/// ディレクトリ選択領域リストの 1 行目に表示される、対象ディレクトリ内にあるディレクトリ名のリストのインデックス番号を取得または設定する。
		/// </summary>
		public int NowPage
		{
			get
			{
				return this.__nowPage;
			}
			private set
			{
				if (this.DirectoryNameList.Length <= Settings.MaxShowDirectoryNum)
				{
					// 対象ディレクトリ内のディレクトリ数が最大表示数を下回っている場合
					// リスト 1 行目のインデックス番号は 0 固定で、スクロールバーを非表示
					this.__nowPage = 0;
					this.LowerScrollButton.Visible = false;
					this.UpperScrollButton.Visible = false;
				}
				else
				{
					this.LowerScrollButton.Visible = true;
					this.UpperScrollButton.Visible = true;

					if (value <= 0)
					{
						this.__nowPage = 0;
						this.LowerScrollButton.Enabled = true;
						this.UpperScrollButton.Enabled = false;
					}
					else if (value >= this.DirectoryNameList.Length - Settings.MaxShowDirectoryNum)
					{
						this.__nowPage = this.DirectoryNameList.Length - Settings.MaxShowDirectoryNum;
						this.LowerScrollButton.Enabled = false;
						this.UpperScrollButton.Enabled = true;
					}
					else
					{
						this.__nowPage = value;
						this.LowerScrollButton.Enabled = true;
						this.UpperScrollButton.Enabled = true;
					}
				}
			}
		}

		#endregion

		#endregion


		#region コンストラクタ / 初期化

		/// <summary>
		/// エクスプローラ上のディレクトリ選択領域の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるエクスプローラ。</param>
		public DirectorySelectArea(Explorer parent)
		{
			this.Parent = parent;

			this.Initialization();
		}


		/// <summary>
		/// エクスプローラ上のディレクトリ選択領域を初期化する。
		/// </summary>
		protected override void Initialization()
		{
			// 上限まで選択ボタン生成
			this.SelectButtons = new DirectorySelectButton[Settings.MaxShowDirectoryNum];
			for (int i = 0; i < this.SelectButtons.Length; i++)
			{
				this.SelectButtons[i] = new DirectorySelectButton(this, i);
				this.SelectButtons[i].Visible = false;
				this.PartsList.Add(this.SelectButtons[i]);
			}

			// 更新ボタン
			this.ReloadButton = new ReloadButton(this);
			this.PartsList.Add(this.ReloadButton);

			// スクロールボタン
			this.UpperScrollButton = new DirectoryUpperScrollButton(this);
			this.LowerScrollButton = new DirectoryLowerScrollButton(this);
			this.PartsList.Add(this.UpperScrollButton);
			this.PartsList.Add(this.LowerScrollButton);

			DrawManager.Regist(this.ToString(), Settings.DirectorySelectArea.Location, "IMAGE_EXPLORER_DIR_BG");
		}

		#endregion


		#region ディレクトリ一覧更新

		/// <summary>
		/// 規定のエクスプローラ対象ディレクトリ内のディレクトリ一覧を更新する。
		/// </summary>
		public void Update()
		{
			this.Update(this.Parent.TargetDirectory);
		}

		/// <summary>
		/// 指定したエクスプローラ対象ディレクトリ内のディレクトリ一覧を更新する。
		/// </summary>
		/// <param name="targetDirectoryPath">対象ディレクトリ名のパス。</param>
		public void Update(string targetDirectoryPath)
		{
			// ディレクトリパスの初期化
			// getDirectoriesTemp は、ディレクトリ一覧を一時的に入れておく変数
			// 実際のディレクトリ一覧は、getDirectoriesTemp に対象ディレクトリそのものを含めたものになる
			string[] getDirectoriesTemp = this.DirectoryPathList = new string[0];

			try
			{
				// ディレクトリ一覧を取得し一時変数に入れとく
				getDirectoriesTemp = Directory.GetDirectories(targetDirectoryPath);
			}
			catch (Exception e)
			{
				LogFileManager.WriteLine(e.ToString());
				this.DirectoryNameList = null;
				return;
			}

			// 対象ディレクトリそのものを加えた実際のディレクトリ一覧をつくる
			this.DirectoryPathList = new string[getDirectoriesTemp.Length + 1];
			if (this.DirectoryPathList.Length > 0)
			{
				this.DirectoryPathList[0] = targetDirectoryPath;
				for (int i = 1; i < getDirectoriesTemp.Length + 1; i++) this.DirectoryPathList[i] = getDirectoriesTemp[i - 1];
			}

			// ディレクトリパスのリストから、ディレクトリ名のリストを生成
			this.DirectoryNameList = new string[this.DirectoryPathList.Length];
			this.DirectoryNameList[0] = CommonTools.TrimDisplayString(Settings.TargetDirectoryDefaultName, Settings.MaxDirectoryNameLength);
			for (int i = 1; i < this.DirectoryNameList.Length; i++)
			{
				this.DirectoryNameList[i] = CommonTools.TrimDisplayString(Path.GetFileName(this.DirectoryPathList[i]), Settings.MaxDirectoryNameLength);
			}
			
			// ディレクトリ名の最大数  リストを超える場合はリストの最大数
			int max = this.DirectoryNameList.Length;
			if (max > Settings.MaxShowDirectoryNum) max = Settings.MaxShowDirectoryNum;

			// ディレクトリ名が割り当てられたボタンを可視化
			for (int i = 0; i < max; i++)
			{
				this.SelectButtons[i].Visible = true;
			}

			// ディレクトリ名が割り当てられなかったボタンを不可視化
			for (int i = max; i < Settings.MaxShowDirectoryNum; i++)
			{
				this.SelectButtons[i].Visible = false;
			}

			// 表示を基点位置に戻す
			this.NowPage = 0;

			// リストの選択状態を解除
			this.Focus(-1);

			// ディレクトリ数更新
			this.SetDirectoryNum();

			// キーボードのフォーカスをディレクトリ選択領域に移す
			this.Parent.NowKeyFocus = Explorer.KeyFocus.DirectorySelectArea;
		}


		/// <summary>
		/// ディレクトリ数をカウントし、表示を更新する。
		/// </summary>
		public void SetDirectoryNum()
		{
			// ディレクトリ数を 0 で初期化
			this.DirectoryNum = 0;

			// 除外する条件にマッチしないディレクトリ名の数をカウント
			foreach (string directoryName in this.DirectoryNameList)
			{
				if (!(directoryName.Length > 0 && directoryName[0] == Settings.DeselectionKeyChar)) this.DirectoryNum++;
			}
		}

		#endregion


		#region スクロール

		/// <summary>
		/// ディレクトリのリストを上へスクロールさせる。
		/// </summary>
		public void UpperScroll()
		{
			this.NowPage--;

			this.ChangeFocusedButton();
		}

		/// <summary>
		/// ディレクトリのリストを下へスクロールさせる。
		/// </summary>
		public void LowerScroll()
		{
			this.NowPage++;

			this.ChangeFocusedButton();
		}

		#endregion


		#region ディレクトリ選択の処理

		/// <summary>
		/// 表示中のディレクトリ一覧のうち、指定したインデックス番号のディレクトリをフォーカスする。
		/// </summary>
		/// <param name="index">リストのインデックス番号。</param>
		public void Focus(int index)
		{
			this.FocusedIndex = index;
			this.ChangeFocusedButton();

			this.Parent.FileSelectArea.Focus(-1);
		}


		/// <summary>
		/// 現在フォーカスされているディレクトリを選択する。
		/// </summary>
		public void Select()
		{
			if (this.FocusedIndex >= 0) this.Select(this.FocusedIndex);
		}

		/// <summary>
		/// 表示中のディレクトリ一覧のうち、指定したインデックス番号のディレクトリを選択する。
		/// </summary>
		/// <param name="index">リストのインデックス番号。</param>
		public void Select(int index)
		{
			this.Focus(index);
			this.Parent.FileSelectArea.Update();
		}


		/// <summary>
		/// 選択されているディレクトリのインデックス番号から、ボタンの選択状態を変更する。
		/// </summary>
		private void ChangeFocusedButton()
		{
			// まず全てのボタンの選択状態を解除
			foreach (DirectorySelectButton dsb in this.SelectButtons) dsb.Pressed = false;

			// ディレクトリのいずれかが選択されており、かつ表示されているインデックス内だった場合、インデックス番号に対応したボタンを選択する
			if (this.FocusedIndex >= this.NowPage && this.FocusedIndex <= Settings.MaxShowDirectoryNum - 1 + this.NowPage)
			{
				this.SelectButtons[this.FocusedIndex - this.NowPage].Pressed = true;
			}
		}


		/// <summary>
		/// 選択しているディレクトリを 1 つ上に移動する。
		/// </summary>
		public void UpperSelect()
		{
			// ディレクトリのリストが Null か空なら何もしない
			if (this.DirectoryNameList == null || this.DirectoryNameList.Length <= 0) return;
			
			int selectedIndex;

			if (this.FocusedIndex <= 0)
			{
				selectedIndex = this.DirectoryNameList.Length - 1;					// 何も選択されていない状態であれば、リストの最後を選択する
				this.NowPage = selectedIndex - Settings.MaxShowDirectoryNum + 1;	// 表示リスト外を選択した場合、表示リストを下にずらす
			}
			else
			{
				selectedIndex = this.FocusedIndex - 1;					// それ以外の場合、リストの一つ上を選択する。
				if (selectedIndex < this.NowPage) this.NowPage--;		// 表示リスト外を選択した場合、表示リストを下にずらす
			}

			this.Focus(selectedIndex);
		}

		/// <summary>
		/// 選択しているディレクトリを 1 つ下に移動する。
		/// </summary>
		public void LowerSelect()
		{
			// ディレクトリのリストが Null か空なら何もしない
			if (this.DirectoryNameList == null || this.DirectoryNameList.Length <= 0) return;

			int selectedIndex;

			if (this.FocusedIndex >= this.DirectoryNameList.Length - 1)
			{
				selectedIndex = 0;										// 何も選択されていない状態であれば、リストの最後を選択する
				if (selectedIndex < this.NowPage) this.NowPage = 0;		// 表示リスト外を選択した場合、表示リストを上にずらす
			}
			else
			{
				selectedIndex = this.FocusedIndex + 1;								// それ以外の場合、リストの一つ上を選択する。
				this.NowPage = selectedIndex - Settings.MaxShowDirectoryNum + 1;	// 表示リスト外を選択した場合、表示リストずらす
			}

			this.Focus(selectedIndex);
		}

		#endregion


		#region 描画 / マウス

		/// <summary>
		/// ディレクトリ選択領域を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);

			// ディレクトリ数の表示
			StringManager.SystemDraw(this.DirectoryNum, Settings.DirectoryNumLocation);
		}


		/// <summary>
		/// エクスプローラ上のディレクトリ選択領域からマウスが出た際の処理。
		/// </summary>
		public override void MouseLeave()
		{
			base.MouseLeave();

			// 全ての部品の MouseLeave メソッドを呼ぶ
			foreach (Parts parts in this.PartsList) parts.MouseLeave();
		}

		#endregion

	}
}
