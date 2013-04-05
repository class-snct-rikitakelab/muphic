using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TextureListMaker
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// BackgounrdWorker を利用し、指定されたソース画像ファイルのリストからテクスチャ名リストの生成を開始する。
		/// </summary>
		/// <param name="fileList"></param>
		private void StartBackgroundWorker(List<string> fileList)
		{
			// バックグランドでのファイル読み込み操作が終了していなければ、終了するまで待機
			while (this.backgroundWorker.IsBusy) System.Threading.Thread.Sleep(1000);

			// バックグラウンドでのファイル読み込み操作開始
			this.listBox_TextureList.BeginUpdate();
			this.backgroundWorker.RunWorkerAsync(fileList);
		}
	
		/// <summary>
		/// BackgounrdWorker を利用し、指定されたソース画像ファイルのリストからテクスチャ名リストの生成を開始する。
		/// </summary>
		/// <param name="fileList"></param>
		private void StartBackgroundWorker(string[] files)
		{
			List<string> fileList = new List<string>();

			for (int i = 0; i < files.Length; i++)
			{
				fileList.Add(files[i]);
			}

			this.StartBackgroundWorker(fileList);
		}


		/// <summary>
		/// テクスチャ名リストを生成する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			e.Cancel = true;
			string errorMessegeList = "";

			foreach (string textFileName in (List<string>)(e.Argument))
			{
				string str;						// 作業用文字列
				var r = new Rectangle();		// 作業用四角形領域 (前の行の画像の位置・サイズを記憶するため) 
				StreamReader reader;			// 読み込みバッファ
				int line_num = 0;				// なんとなく行数をカウントしてみる

				try								// 読み込みバッファを設定
				{
					reader = new StreamReader(textFileName, Encoding.GetEncoding("Shift_JIS"));
				}
				catch (Exception)				// 読み込みに失敗したらfalseを返す
				{
					continue;
				}

				while ((str = reader.ReadLine()) != null)			// 行末まで1行ごと読み込み
				{
					string[] temp;				// [0]:ソース画像ファイル名  [1]:テクスチャ名  [2]:ソース画像ファイル内の位置とサイズ
					string textureFileName;		// テクスチャ名
					string sourceFileName;		// ソース画像ファイル名
					line_num++;					// 行数計算用

					str = Muphic.Tools.CommonTools.RemoveStr(str, "//");	// 先ずは、コメント部分の削除

					temp = str.Split(new char[] { '\t' }, System.StringSplitOptions.RemoveEmptyEntries);		// 文字列を ソース画像ファイル名・テクスチャ名・位置とサイズ に分割

					if (temp.Length == 0)						// 分割して得た文字列の数が 0 だった場合
					{													// 空行と判断し読み飛ばす
						continue;										// 次の行へ
					}
					else if (temp.Length == 2)					// 分割して得た文字列の数が 2 つだった場合
					{												// 位置とサイズについて、分割された 2 番目の文字列から Rectangle を生成
						r = this.StringToRectangle(temp[1], r);		// 分割された 1 番目の文字列からテクスチャ名を取得
						textureFileName = temp[0];					// (ソース画像ファイル名の記述が無いので) 読み込んだテキストファイル名からソース画像ファイル名を生成
						sourceFileName = Path.ChangeExtension(Path.GetFileName(textFileName), ".png");
					}
					else if (temp.Length == 3)					// 分割して得た文字列の数が 3 つだった場合
					{
						r = this.StringToRectangle(temp[2], r);		// 位置とサイズについて、分割された 3 番目の文字列から Rectangle を生成
						textureFileName = temp[1];					// 分割された 2 番目の文字列からテクスチャ名を取得
						sourceFileName = temp[0];					// 分割された 1 番目の文字列からソース画像ファイル名を取得
					}
					else										// 分割して得た文字列の数が上記以外だった場合
					{												// 不正なフォーマットとして処理
						errorMessegeList += "\"" + Path.GetFileName(textFileName) + "\" " + (line_num + 1) + "行目 (不正フォーマット)\r\n";
						continue;									// エラーメッセージを追加し次の行へ
					}

					if ((r.Width == -1) && (r.Height == -1))	// 不正なフォーマットにより文字列から Rectangle が生成できなかった場合
					{												// 全ての要素が -1 の Rectangle が得られる
						errorMessegeList += "\"" + Path.GetFileName(textFileName) + "\" " + (line_num + 1) + "行目 (不正フォーマット)\r\n";
						continue;									// エラーメッセージを追加し次の行へ
					}

					if (this.RegistedTextureNameList.Contains(temp[1]))		// テクスチャ名が既に登録されている場合
					{														// エラーメッセージを追加し次の行へ
						errorMessegeList += "\"" + Path.GetFileName(textFileName) + "\" " + (line_num + 1) + "行目 (テクスチャ名重複)\r\n";
						continue;
					}

					((BackgroundWorker)sender).ReportProgress(0, new MainWindow.TextureListItem(textureFileName, sourceFileName, r));
				}
			}

			e.Cancel = false;
			e.Result = errorMessegeList;

			((BackgroundWorker)sender).ReportProgress(100);
		}

		/// <summary>
		/// 文字列表現の矩形を Rectangle 型に変換する。
		/// </summary>
		/// <param name="rectStr">変換する文字列。ソース画像ファイル内での位置とサイズを表す。</param>
		/// <param name="r">変換する文字列に "+" や "-" が含まれていた場合の基になる Rectangle 型。</param>
		/// <returns>文字列から変換された位置とサイズ。</returns>
		private Rectangle StringToRectangle(string sourceStr, Rectangle r)
		{
			string[] rectangleStr = sourceStr.Split(new char[] { '\t', ' ', ',' }, System.StringSplitOptions.RemoveEmptyEntries);

			if (rectangleStr.Length != 4 && rectangleStr.Length != 2)		// 位置とサイズの文字列をカンマで分割し、
			{																// 文字列の数が 4 か 2 でなかった場合は不正なフォーマットとして弾く
				return new Rectangle(-1, -1, -1, -1);
			}

			if (rectangleStr[0].IndexOf('+') == 0) r.X += int.Parse(rectangleStr[0].Substring(1));			// Rectangle の X の先頭文字が '+' だった場合、前の行の X に足し合わせる
			else if (rectangleStr[0].IndexOf('-') == 0) r.Y -= int.Parse(rectangleStr[0].Substring(1));		// Rectangle の X の先頭文字が '-' だった場合、前の行の X から引く
			else r.X = int.Parse(rectangleStr[0]);															// それ以外であれば、Xにそのまま値を入れる

			if (rectangleStr[1].IndexOf('+') == 0) r.Y += int.Parse(rectangleStr[1].Substring(1));			// Rectangle の Y の先頭文字が '+' だった場合、前の行の Y に足し合わせる
			else if (rectangleStr[1].IndexOf('-') == 0) r.Y -= int.Parse(rectangleStr[1].Substring(1));		// Rectangle の Y の先頭文字が '-' だった場合、前の行の Y から引く
			else r.Y = int.Parse(rectangleStr[1]);															// それ以外であれば、 Y にそのまま値を入れる

			if (rectangleStr.Length == 4)					// Rectangle の文字列数が 4 だった場合は横幅と縦幅も設定する
			{												// Rectangle の文字列数が 2 だった場合は前の行の横幅と縦幅をそのまま使用
				r.Width = int.Parse(rectangleStr[2]);		// 横幅設定
				r.Height = int.Parse(rectangleStr[3]);		// 縦幅設定
			}

			return r;
		}


		/// <summary>
		/// テクスチャ名リストが更新される際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 100)	// 報告されるタスク進捗状況が 100 になったら
			{									// 現在のソース画像ファイルリストからのテクスチャ名リスト生成完了
				this.FileList.Clear();			// ソース画像ファイルリストを初期化
			}
			else								// 上記以外のタスク進捗状況であれば
			{									// テクスチャ名リストにデータを追加
				this.listBox_TextureList.Items.Add((MainWindow.TextureListItem)(e.UserState));
			}
		}


		/// <summary>
		/// テクスチャ名リスト生成が完了した際の処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			this.listBox_TextureList.EndUpdate();
			this.listBox_TextureList.Update();

			if (!String.IsNullOrEmpty((string)(e.Result)))
			{
				MessageBox.Show(this,
					(string)(e.Result) + (e.Cancelled ? "\r\n読み込み中断" : "\r\n上記を除き読み込み完了"),
					"読み込みエラー: " + (((string)(e.Result)).Split(new string[] { "\r\n" }, StringSplitOptions.None).Length - 1).ToString(),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}

			if (this.listBox_TextureList.Items.Count > 0)
			{
				this.button_CreateTextureList.Enabled = true;
				this.mainMenuItem_File_Save.Enabled = true;
			}
		}

	}
}
