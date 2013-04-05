using System;
using System.Collections.Generic;
using System.IO;

using Muphic.Common.ExplorerParts.Buttons;
using Muphic.Manager;
using Muphic.Tools;

namespace Muphic.Common.ExplorerParts
{
	/// <summary>
	/// エクスプローラのファイル選択領域。
	/// </summary>
	public class FileSelectArea : Screen
	{

		#region プロパティ / インデクサ

		/// <summary>
		/// 親にあたるエクスプローラ。
		/// </summary>
		public Explorer Parent { get; private set; }

		/// <summary>
		/// 選択されているファイルを示すインデックス番号を取得または設定する。
		/// </summary>
		public int FocusedIndex { get; private set; }


		#region ファイルのリスト関連

		/// <summary>
		/// 選択中のディレクトリ内にあるファイル名のリストを取得または設定する。
		/// <para>このリストに格納されるファイル名は表示用であり、実際のファイルパスは FilePathList プロパティで取得する。</para>
		/// </summary>
		private string[] FileNameList { get; set; }

		/// <summary>
		/// 選択中のディレクトリ内にあるファイルパスのリストを取得または設定する。
		/// <para>このリストに格納されるパスは読み込み時に利用するものであり、表示するファイル名は FileNameList プロパティで取得する。</para>
		/// </summary>
		private string[] FilePathList { get; set; }

		/// <summary>
		/// 指定したインデックス番号のファイル名を取得する。
		/// </summary>
		/// <param name="index">取得するファイル名のインデックス番号。</param>
		/// <returns>インデックス番号に対応したファイル名。</returns>
		public string this[int index]
		{
			get { return this.FileNameList[index]; }
		}


		/// <summary>
		/// 印刷済みのファイルパスのリストを取得または設定する。
		/// </summary>
		private List<string> PrintedFileList { get; set; }


		/// <summary>
		/// 現在選択中のファイルパスを取得する。何も選択されていない場合は空の文字列。
		/// </summary>
		public string SelectedFilePath
		{
			get
			{
				if (this.FocusedIndex < 0) return "";
				else return this.FilePathList[this.FocusedIndex];
			}
		}

		#endregion


		#region ボタン

		/// <summary>
		/// ファイル選択ボタン。
		/// </summary>
		private FileSelectButton[] SelectButtons { get; set; }

		/// <summary>
		/// 上スクロールボタン。
		/// </summary>
		public FileUpperScrollButton UpperScrollButton { get; private set; }

		/// <summary>
		/// 下スクロールボタン。
		/// </summary>
		public FileLowerScrollButton LowerScrollButton { get; private set; }

		#endregion


		#region スクロールとページ

		/// <summary>
		/// ファイル選択領域リストの 1 行目に表示される、選択中のディレクトリ内にあるファイル名のリストのインデックス番号。
		/// </summary>
		private int __nowPage;

		/// <summary>
		/// ファイル選択領域リストの 1 行目に表示される、選択中のディレクトリ内にあるファイル名のリストのインデックス番号を取得または設定する。
		/// </summary>
		public int NowPage
		{
			get
			{
				return this.__nowPage;
			}
			private set
			{
				if (this.FileNameList.Length <= Settings.MaxShowFileNum)
				{
					// 選択中のディレクトリ内のファイル数が最大表示数を下回っている場合
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
					else if (value >= this.FileNameList.Length - Settings.MaxShowFileNum)
					{
						this.__nowPage = this.FileNameList.Length - Settings.MaxShowFileNum;
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
		/// エクスプローラ上のファイル選択領域の新しいインスタンスを初期化する。
		/// </summary>
		/// <param name="parent">親にあたるエクスプローラ。</param>
		public FileSelectArea(Explorer parent)
		{
			this.Parent = parent;

			this.Initialization();
		}


		/// <summary>
		/// エクスプローラ上のファイル選択領域を初期化する。
		/// </summary>
		protected override void Initialization()
		{
			// 上限まで選択ボタン生成
			this.SelectButtons = new FileSelectButton[Settings.MaxShowDirectoryNum];
			for (int i = 0; i < this.SelectButtons.Length; i++)
			{
				this.SelectButtons[i] = new FileSelectButton(this, i);
				this.SelectButtons[i].Visible = false;
				this.PartsList.Add(this.SelectButtons[i]);
			}

			// 印刷済みリスト
			this.PrintedFileList = new List<string>();

			// スクロールボタン
			this.UpperScrollButton = new FileUpperScrollButton(this);
			this.LowerScrollButton = new FileLowerScrollButton(this);
			this.PartsList.Add(this.UpperScrollButton);
			this.PartsList.Add(this.LowerScrollButton);

			DrawManager.Regist(this.ToString(), Settings.FileSelectArea.Location, "IMAGE_EXPLORER_FILE_BG");
		}

		#endregion


		#region ファイル一覧更新

		/// <summary>
		/// 選択されたディレクトリ内のファイル一覧を更新する。
		/// </summary>
		public void Update()
		{
			this.Update(this.Parent.DirectorySelectArea.SelectedDirectoryPath);
		}

		/// <summary>
		/// 選択されたディレクトリ内のファイル一覧を更新する。
		/// </summary>
		/// <param name="targetDirectoryPath">対象ディレクトリ名のパス。</param>
		public void Update(string targetDirectoryPath)
		{
			try
			{
				// ファイル一覧を取得
				this.FilePathList = Directory.GetFiles(targetDirectoryPath, "*" + this.Parent.TargetExtension);
			}
			catch (Exception e)
			{
				LogFileManager.WriteLine(e.ToString());
				this.FileNameList = null;
				return;
			}

			// ファイルパスのリストから、ファイル名のリストを生成
			this.FileNameList = new string[this.FilePathList.Length];
			for (int i = 0; i < this.FileNameList.Length; i++)
			{
				this.FileNameList[i] = CommonTools.TrimDisplayString(Path.GetFileNameWithoutExtension(this.FilePathList[i]), Settings.MaxFileNameLength);
			}

			// ファイル名リストの最大数  リストを超える場合はリストの最大数
			int max = this.FileNameList.Length;
			if (max > Settings.MaxShowFileNum) max = Settings.MaxShowFileNum;

			// ファイル名が割り当てられたボタンを可視化
			for (int i = 0; i < max; i++)
			{
				this.SelectButtons[i].Visible = true;
			}

			// ファイル名が割り当てられなかったボタンを不可視化
			for (int i = max; i < Settings.MaxShowFileNum; i++)
			{
				this.SelectButtons[i].Visible = false;
			}

			// リストが空でなければリストの先頭にフォーカス
			if (this.FileNameList.Length > 0) this.Focus(0);
			else this.Focus(-1);

			// 表示を基点位置に戻す
			this.NowPage = 0;

			// ボタン選択状態変更
			this.ChangeSelectedButton();

			// キーボードのフォーカスをファイル選択領域に移す
			this.Parent.NowKeyFocus = Explorer.KeyFocus.FileSelectArea;
		}


		/// <summary>
		/// ファイル一覧の表示をクリアしする。
		/// </summary>
		public void Clear()
		{
			// ファイル名リストあぼーん
			this.FileNameList = new string[0];

			// 選択ボタンあぼーん
			for (int i = 0; i < Settings.MaxShowFileNum; i++)
			{
				this.SelectButtons[i].Visible = false;
			}

			// フォーカスをあぼーん
			this.Focus(-1);

			// 表示を基点位置あぼーん
			this.NowPage = 0;

			// ボタン選択状態をあぼーん
			this.ChangeSelectedButton();

			// キーボードフォーカスをあぼーん
			this.Parent.NowKeyFocus = Explorer.KeyFocus.DirectorySelectArea;
		}

		#endregion


		#region スクロール

		/// <summary>
		/// ファイルのリストを上へスクロールさせる。
		/// </summary>
		public void UpperScroll()
		{
			this.NowPage--;

			this.ChangeSelectedButton();
		}

		/// <summary>
		/// ファイルのリストを下へスクロールさせる。
		/// </summary>
		public void LowerScroll()
		{
			this.NowPage++;

			this.ChangeSelectedButton();
		}

		#endregion


		#region ファイル選択の処理

		/// <summary>
		/// 表示中のファイル一覧のうち、指定したインデックス番号のディレクトリをフォーカスする。
		/// </summary>
		/// <param name="index">リストのインデックス番号。</param>
		public void Focus(int index)
		{
			this.FocusedIndex = index;
			this.ChangeSelectedButton();

			this.Parent.WorkInfoArea.Clear();
		}


		/// <summary>
		/// 現在フォーカスされているファイルを選択する。
		/// </summary>
		public void Select()
		{
			if (this.FocusedIndex >= 0) this.Select(this.FocusedIndex);
		}

		/// <summary>
		/// 表示中のファイル一覧のうちの、指定したインデックス番号のファイルを選択する。
		/// </summary>
		/// <param name="index">リストのインデックス番号。</param>
		public void Select(int index)
		{
			this.Focus(index);

			this.Parent.WorkInfoArea.Show(this.SelectedFilePath);
		}


		/// <summary>
		/// 選択されているファイルのインデックス番号から、ボタンの選択状態を変更する。
		/// </summary>
		private void ChangeSelectedButton()
		{
			// まず全てのボタンの選択状態を解除
			foreach (FileSelectButton fsb in this.SelectButtons) fsb.Pressed = false;

			// ファイルのいずれかが選択されており、かつ表示されているインデックス内だった場合、インデックス番号に対応したボタンを選択する
			if (this.FocusedIndex >= this.NowPage && this.FocusedIndex <= Settings.MaxShowFileNum - 1 + this.NowPage)
			{
				this.SelectButtons[this.FocusedIndex - this.NowPage].Pressed = true;
			}
		}


		/// <summary>
		/// 選択しているファイルを 1 つ上に移動する。
		/// </summary>
		public void UpperSelect()
		{
			// ファイルのリストが Null か空なら何もしない
			if (this.FileNameList == null || this.FileNameList.Length <= 0) return;

			int selectedIndex;

			if (this.FocusedIndex <= 0)
			{
				selectedIndex = this.FileNameList.Length - 1;					// 何も選択されていない状態であれば、リストの最後を選択する
				this.NowPage = selectedIndex - Settings.MaxShowFileNum + 1;		// 表示リスト外を選択した場合、表示リストを下にずらす
			}
			else
			{
				selectedIndex = this.FocusedIndex - 1;					// それ以外の場合、リストの一つ上を選択する。
				if (selectedIndex < this.NowPage) this.NowPage--;		// 表示リスト外を選択した場合、表示リストを下にずらす
			}

			this.Focus(selectedIndex);
		}

		/// <summary>
		/// 選択しているファイルを 1 つ下に移動する。
		/// </summary>
		public void LowerSelect()
		{
			// ファイルのリストが Null か空なら何もしない
			if (this.FileNameList == null || this.FileNameList.Length <= 0) return;

			int selectedIndex;

			if (this.FocusedIndex >= this.FileNameList.Length - 1)
			{
				selectedIndex = 0;										// 何も選択されていない状態であれば、リストの最後を選択する
				if (selectedIndex < this.NowPage) this.NowPage = 0;		// 表示リスト外を選択した場合、表示リストを上にずらす
			}
			else
			{
				selectedIndex = this.FocusedIndex + 1;							// それ以外の場合、リストの一つ上を選択する。
				this.NowPage = selectedIndex - Settings.MaxShowFileNum + 1;		// 表示リスト外を選択した場合、表示リストずらす
			}

			this.Focus(selectedIndex);
		}

		#endregion


		#region 印刷済みリストの処理

		/// <summary>
		/// 現在選択中のファイルを印刷済みリストに追加する。
		/// </summary>
		public void AddPrintedFilePath()
		{
			if (!string.IsNullOrEmpty(this.SelectedFilePath))
			{
				this.AddPrintedFilePath(this.SelectedFilePath);
			}
		}

		/// <summary>
		/// 印刷済みのファイルパスを印刷済みリストに追加する。
		/// </summary>
		/// <param name="filePath">追加するファイルパス。</param>
		public void AddPrintedFilePath(string filePath)
		{
			this.PrintedFileList.Add(filePath);
		}

		/// <summary>
		/// 現在選択中のファイルパスが印刷済みかどうかを確認する。
		/// </summary>
		/// <returns>印刷済みの場合は true、それ以外は false。</returns>
		public bool ContainsPrintedFilePath()
		{
			if (!string.IsNullOrEmpty(this.SelectedFilePath))
			{
				return this.ContainsPrintedFilePath(this.SelectedFilePath);
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 指定したファイルパスが印刷済みかどうかを確認する。
		/// </summary>
		/// <param name="filePath">確認するファイルパス。</param>
		/// <returns>印刷済みの場合は true、それ以外は false。</returns>
		public bool ContainsPrintedFilePath(string filePath)
		{
			return this.PrintedFileList.Contains(filePath);
		}

		#endregion


		#region 描画 / マウス

		/// <summary>
		/// ファイル選択領域を描画する。
		/// </summary>
		/// <param name="drawStatus">描画時の状態データ。</param>
		public override void Draw(DrawStatusArgs drawStatus)
		{
			base.Draw(drawStatus);
		}


		/// <summary>
		/// エクスプローラ上のファイル選択領域からマウスが出た際の処理。
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
