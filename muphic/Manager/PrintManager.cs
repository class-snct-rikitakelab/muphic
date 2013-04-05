using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;

using Muphic.Tools;

namespace Muphic.Manager
{
	/// <summary>
	/// 印刷管理クラス (シングルトン・継承不可) 
	/// <para></para>
	/// </summary>
	public sealed class PrintManager : Manager
	{
		/// <summary>
		/// 印刷管理クラスの静的インスタンス (シングルトンパターン)。
		/// </summary>
		private static readonly PrintManager __instance = new PrintManager();

		/// <summary>
		/// 印刷管理クラスの静的インスタンス (シングルトンパターン) を取得する。
		/// </summary>
		private static PrintManager Instance
		{
			get { return Muphic.Manager.PrintManager.__instance; }
		}


		#region フィールド/プロパティ

		/// <summary>
		/// プリンタへ出力を送信する PrintDocument オブジェクト。
		/// </summary>
		private PrintDocument PrintDocument { get; set; }

		/// <summary>
		/// 印刷するテクスチャの情報を保持するリスト。各テクスチャのリストが、印刷するページ数の分だけ用意される。
		/// </summary>
		private List<List<PrintData>> PrintDataList { get; set; }

		/// <summary>
		/// 印刷で使用するテクスチャを保持するリスト。
		/// </summary>
		private Dictionary<string, Image> TextureList { get; set; }


		/// <summary>
		///  印刷するテクスチャを登録中に、どのページへ登録しているかを示す値。
		/// </summary>
		private int __nowRegistingPage;

		/// <summary>
		/// 印刷するテクスチャを登録中に、どのページへ登録しているかを示す値を取得または設定する。
		/// </summary>
		private int NowRegistingPage
		{
			get
			{
				return this.__nowRegistingPage;
			}
			set
			{
				if (value < 0) value = 0;

				if (value >= this.PrintDataList.Count)					// 対象のページが既に作れたページの範囲外だった場合
				{														// 新たにページを生成する
					while (this.PrintDataList.Count <= value)
					{
						this.PrintDataList.Add(new List<PrintData>());
					}
				}

				this.__nowRegistingPage = value;
			}
		}


		/// <summary>
		/// テクスチャを印刷中に、どのページを印刷しているかを示す値を取得または設定する。
		/// </summary>
		private int NowPrintingPage { get; set; }


		/// <summary>
		/// 現在印刷中のテクスチャ情報リストを取得する。
		/// </summary>
		private List<PrintData> NowPrintData
		{
			get { return this.PrintDataList[this.NowPrintingPage]; }
		}

		#endregion


		#region 印刷するデータ

		/// <summary>
		/// 印刷されるテクスチャの情報を保持する。このクラスのインスタンス 1 つで、1 つのテクスチャの情報となる。
		/// </summary>
		private class PrintData
		{
			/// <summary>
			/// 印刷対象のテクスチャが属する統合画像ファイル名を取得する。
			/// </summary>
			public string FileName { get; private set; }

			/// <summary>
			/// 印刷対象のテクスチャの描画先の座標を取得する。
			/// </summary>
			public Point Location { get; private set; }

			/// <summary>
			/// 印刷対象のテクスチャの拡大・縮小率を取得する。
			/// </summary>
			public float Scaling { get; private set; }

			/// <summary>
			/// 印刷対象のテクスチャの色フィルタを取得する。
			/// </summary>
			public Color Filter { get; private set; }

			/// <summary>
			/// 印刷対象のテクスチャの回転角を取得する。
			/// </summary>
			public float LotationAngle { get; private set; }

			/// <summary>
			/// 印刷対象がテクスチャでなくラインであることを示す値を取得する。
			/// </summary>
			public bool IsLine { get; private set; }

			/// <summary>
			/// 印刷する文字列を取得する。
			/// </summary>
			public string Text { get; private set; }

			public bool IsText
			{
				get { return !string.IsNullOrEmpty(this.Text); }
			}


			/// <summary>
			/// テクスチャの印刷データの新しいインスタンスを初期化する。
			/// </summary>
			/// <param name="fileName">印刷対象のテクスチャが属する統合画像ファイル名。</param>
			/// <param name="scaling">印刷対象のテクスチャを描画する描画先のサイズ。</param>
			/// <param name="angle">印刷対象のテクスチャの回転角</param>
			/// <param name="location">印刷対象のテクスチャの描画先の座標。</param>
			/// <param name="filter">印刷対象のテクスチャの色フィルタ。</param>
			public PrintData(string fileName, float scaling, float angle, Point location, Color filter)
				: this()
			{
				this.FileName = fileName;
				this.Scaling = scaling;
				this.LotationAngle = angle;
				this.Location = location;
				this.Filter = filter;
			}

			/// <summary>
			/// 文字列の印刷データの新しいインスタンスを初期化する。
			/// </summary>
			/// <param name="text">印刷対象の文字列。</param>
			/// <param name="location">印刷対象の文字列の描画先の座標。</param>
			/// <param name="size">印刷対象のフォントの大きさ。</param>
			public PrintData(string text, Point location, float size)
				: this()
			{
				this.Text = text;
				this.Location = location;
				this.LotationAngle = size;
			}


			/// <summary>
			/// 印刷データの新しいインスタンスを初期化する。
			/// </summary>
			private PrintData()
				: this("", 1.0F, 0.0F, new Point(), new Color(), false, "")
			{
			}

			/// <summary>
			/// 印刷データの新しいインスタンスを初期化する。
			/// </summary>
			/// <param name="fileName">印刷対象のテクスチャが属する統合画像ファイル名。</param>
			/// <param name="scaling">印刷対象のテクスチャを描画する描画先のサイズ。</param>
			/// <param name="angle">印刷対象のテクスチャの回転角</param>
			/// <param name="location">印刷対象のテクスチャもしくは文字列の描画先の座標。</param>
			/// <param name="filter">印刷対象のテクスチャの色フィルタ。または、印刷対象のラインもしくは文字列の色。</param>
			/// <param name="isLine">印刷対象がラインである場合は true、それ以外は false。</param>
			/// <param name="text">印刷対象の文字列。</param>
			private PrintData(string fileName, float scaling, float angle, Point location, Color filter, bool isLine, string text)
			{
				this.FileName = fileName;
				this.Scaling = scaling;
				this.LotationAngle = angle;
				this.Location = location;
				this.Filter = filter;
				this.IsLine = isLine;
				this.Text = text;
			}
		}

		#endregion


		#region コンストラクタ/初期化

		/// <summary>
		/// 印刷管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		private PrintManager()
		{
			this.PrintDataList = new List<List<PrintData>>();
			this.TextureList = new Dictionary<string, Image>();
			this.NowRegistingPage = this.NowPrintingPage = 0;
		}


		/// <summary>
		/// 描画管理クラスの静的インスタンス生成及び使用する描画デバイス等の初期化を行う。
		/// インスタンス生成後に１度しか実行できない。
		/// </summary>
		/// <returns>初期化が正常に終了した場合は true、それ以外は false。</returns>
		private bool _Initialize()
		{
			if (this._IsInitialized) return false;

			this.PrintDocument = new PrintDocument();

			// 印刷と印刷完了のイベントを登録
			this.PrintDocument.PrintPage += new PrintPageEventHandler(this._PrintDocument_PrintPage);
			this.PrintDocument.EndPrint += new PrintEventHandler(this._PrintDocument_EndPrint);

			// 印刷中... のウィンドウを表示しない
			this.PrintDocument.PrintController = new StandardPrintController();

			// プリンタの一覧を列挙し、その中に構成設定ファイルのプリンタ名と同じプリンタ名を発見したら
			// そのプリンタを印刷に使用するプリンタとして選択
			// 発見できなかったら自動的に通常使うプリンタで印刷される (ハズ)
			foreach (string printerName in PrinterSettings.InstalledPrinters)
			{
				if (ConfigurationManager.Current.PrinterName == printerName)
				{
					this.PrintDocument.PrinterSettings.PrinterName = printerName;
					break;
				}
			}

			DebugTools.ConsolOutputMessage("PrintManager -Initialize", "印刷管理クラス生成完了", true);

			return this._IsInitialized = true;
		}

		#endregion


		#region 印刷対象登録

		/// <summary>
		/// 印刷対象のテクスチャを登録する (座標自動取得)。
		/// </summary>
		/// <param name="className">登録するテクスチャのキー (クラス名等), またはテクスチャ名。</param>
		/// <param name="state">そのクラスの状態 (状態によって登録するテクスチャが変わる)。</param>
		/// <param name="isCenter">location が左上でなくテクスチャ中央の座標である場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率 (拡大・縮小を行わない場合は 1.0)。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ (テクスチャの色をそのまま使用する場合は Color.White)。</param>
		private void _RegistTexture(string className, int state, bool isCenter, float scaling, Color filter)
		{
			// クラス名から描画する際の左上座標を得る
			Rectangle r = Muphic.Manager.RectangleManager.Get(className, state);

			// 存在チェック
			if (r.X == -1)
			{
				// キーが存在しない場合、その旨をコンソールに出力して終了
				return;
			}

			// キーが存在し描画する際の左上座標が判明したら、同メソッド(オーバーロード)を呼ぶ
			this._RegistTexture(className, r.Location, state, isCenter, scaling, 0.0F, filter);
		}

		/// <summary>
		/// 印刷対象のテクスチャを登録する (座標指定)。
		/// </summary>
		/// <param name="className">登録するテクスチャのキー (クラス名等), またはテクスチャ名。</param>
		/// <param name="location">描画する座標。</param>
		/// <param name="state">そのクラスの状態 (状態によって登録するテクスチャが変わる)。</param>
		/// <param name="isCenter">location が左上でなくテクスチャ中央の座標である場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率 (拡大・縮小を行わない場合は 1.0)。</param>
		/// <param name="isLotation90">テクスチャを 90 度回転させて登録する場合は true、それ以外は false。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ (テクスチャの色をそのまま使用する場合は Color.White)。</param>
		private void _RegistTexture(string className, Point location, int state, bool isCenter, float scaling, bool isLotation90, Color filter)
		{
			this._RegistTexture(className, location, state, isCenter, scaling, isLotation90 ? (float)(Math.PI / 2) : 0.0F, filter);
		}

		/// <summary>
		/// 印刷対象のテクスチャを登録する (座標指定)。
		/// </summary>
		/// <param name="className">登録するテクスチャのキー (クラス名等), またはテクスチャ名。</param>
		/// <param name="location">登録する座標。</param>
		/// <param name="state">そのクラスの状態 (状態によって登録するテクスチャが変わる)。</param>
		/// <param name="isCenter">location が左上でなくテクスチャ中央の座標である場合は true、それ以外は false。</param>
		/// <param name="scaling">拡大・縮小率 (拡大・縮小を行わない場合は 1.0)。</param>
		/// <param name="lotationAngle">テクスチャを回転させて登録する場合はその回転角 (ラジアン右回転)、回転させない場合は 0。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ (テクスチャの色をそのまま使用する場合は Color.White)。</param>
		private void _RegistTexture(string className, Point location, int state, bool isCenter, float scaling, float lotationAngle, Color filter)
		{
			string textureName = Muphic.Manager.TextureNameManager.Get(className, state);		// クラス名からテクスチャ名を得る

			if (string.IsNullOrEmpty(textureName))							// クラス名からテクスチャ名が得られたかをチェックする
			{
				if (Muphic.Manager.TextureFileManager.Exist(className))		// 得られなかった場合,className自体がテクスチャ名になっている可能性がある
				{
					textureName = className;								// TextureFileManagerに登録されていれば,そのままテクスチャ名とする
				}
				else
				{
					Muphic.Tools.DebugTools.ConsolOutputError("PrintManager -RegistTexture", "テクスチャ印刷登録失敗 (キー: " + className + " は未登録) ");
					return;		// キーが存在しない場合、その旨をコンソールに出力して終了
				}
			}

			// 印刷用のテクスチャのみ統合画像でなく単体の画像なので、テクスチャ名をファイル名に変える
			textureName = "PRINT" + textureName + ".png";

			// そのファイルがアーカイブ内に無ければおかしい
			if (!ArchiveFileManager.Exists(textureName))
			{
				Muphic.Tools.DebugTools.ConsolOutputError("PrintManager -RegistTexture", "テクスチャ印刷登録失敗 (ファイル: " + className + " がアーカイブに存在しない) ");
				return;
			}

			// テクスチャ登録
			this.PrintDataList[this.NowRegistingPage].Add(new PrintData(textureName, scaling, lotationAngle, location, filter));
		}


		/// <summary>
		/// 印刷対象の文字列を登録する。
		/// </summary>
		/// <param name="text">印刷対象の文字列。</param>
		/// <param name="location">印刷対象の文字列の描画先の座標。</param>
		/// <param name="size">印刷対象のフォントの大きさ。</param>
		private void _RegistString(string text, Point location, float size)
		{
			this.PrintDataList[this.NowRegistingPage].Add(new PrintData(text, location, size));
		}

		#endregion


		#region ページ操作

		/// <summary>
		/// 登録先のページを変更する。
		/// </summary>
		/// <param name="page">登録先のページ。</param>
		public void _ChangePage(int page)
		{
			this.NowRegistingPage = page;
		}

		/// <summary>
		/// 登録先のページを次のページに変更する。
		/// </summary>
		public void _ChangePageNext()
		{
			this.NowRegistingPage++;
		}

		/// <summary>
		/// 登録先のページを前のページに変更する。
		/// </summary>
		public void _ChangePagePrev()
		{
			this.NowRegistingPage--;
		}

		#endregion


		#region 印刷

		/// <summary>
		/// 全てのページの印刷を開始する。
		/// </summary>
		private void _PrintStart()
		{
			this._PrintStart(0);
		}

		/// <summary>
		/// 指定したページからの印刷を開始する。
		/// </summary>
		/// <param name="startPage"></param>
		private void _PrintStart(int startPage)
		{
			DrawManager.DrawNowLoading();

			this.NowPrintingPage = startPage;

			this.PrintDocument.DefaultPageSettings.Landscape = true;
			this.PrintDocument.PrinterSettings.PrintToFile = true;

			LogFileManager.WriteLine(
				Properties.Resources.Msg_PrintMgr_PrintStart,
				CommonTools.GetResourceMessage(
					Properties.Resources.Msg_PrintMgr_PrintStart_PrinterName,
					this.PrintDocument.PrinterSettings.PrinterName)
			);

			this.PrintDocument.Print();
		}

		/// <summary>
		/// ページ単位での印刷を行う。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			LogFileManager.WriteLine(
				Properties.Resources.Msg_PrintMgr_Printing,
				CommonTools.GetResourceMessage(
					Properties.Resources.Msg_PrintMgr_Printing_Page,
					(this.NowPrintingPage + 1).ToString(),
					this.PrintDataList.Count.ToString())
			);

			// 対象ページの全てのラインとテクスチャを印刷する。
			for (int i = 0; i < this.NowPrintData.Count; i++)
			{
				if (this.NowPrintData[i].IsText)
				{
					Font font = new Font("ＭＳ ゴシック", this.NowPrintData[i].LotationAngle, FontStyle.Regular);

					e.Graphics.DrawString(this.NowPrintData[i].Text, font, Brushes.Black, this.NowPrintData[i].Location);
				}
				//else if (this.NowPrintData[i].IsLine)
				//{
				//    Point[] positions = new Point[5];
				//    positions[0] = new Point(this.NowPrintData[i].SrcRectangle.Left, this.NowPrintData[i].SrcRectangle.Top);
				//    positions[1] = new Point(this.NowPrintData[i].SrcRectangle.Left, this.NowPrintData[i].SrcRectangle.Bottom);
				//    positions[2] = new Point(this.NowPrintData[i].SrcRectangle.Right, this.NowPrintData[i].SrcRectangle.Bottom);
				//    positions[3] = new Point(this.NowPrintData[i].SrcRectangle.Right, this.NowPrintData[i].SrcRectangle.Top);
				//    positions[4] = new Point(this.NowPrintData[i].SrcRectangle.Left, this.NowPrintData[i].SrcRectangle.Top);

				//    //e.Graphics.DrawLines(new Pen(this.NowPrintData[i].Filter, this.NowPrintData[i].DistSize.Width), positions);
				//}
				else
				{
					PrintData target = this.NowPrintData[i];

					ColorMatrix colorMatrix = new ColorMatrix();
					colorMatrix.Matrix00 = target.Filter.R / 255F;
					colorMatrix.Matrix11 = target.Filter.G / 255F;
					colorMatrix.Matrix22 = target.Filter.B / 255F;
					colorMatrix.Matrix33 = target.Filter.A / 255F;
					colorMatrix.Matrix44 = 1;

					ImageAttributes imageAttributes = new ImageAttributes();
					imageAttributes.SetColorMatrix(colorMatrix);

					Image textureData;		// 印刷に使用する統合画像
					textureData = Bitmap.FromStream(new MemoryStream(ArchiveFileManager.GetData(target.FileName)));

					#region 統合画像の場合はテクスチャのリストに追加して多重読み込みを防ぐけど、統合画像での印刷が廃止された今は使っていない
					// 印刷しようとするテクスチャがリストに含まれているかをチェック
					// 含まれていた場合はそのテクスチャを取得
					//if (!this.TextureList.TryGetValue(target.FileName, out textureData))
					//{
					//    // 取得できなかった場合は、アーカイブからファイルを展開しリストに追加
					//    textureData = Bitmap.FromStream(new MemoryStream(ArchiveFileManager.GetData(target.FileName)));
					//    this.TextureList.Add(target.FileName, textureData);
					//}
					#endregion

					// 印刷ページにテクスチャを実際に描画
					e.Graphics.DrawImage(
						textureData,
						new RectangleF(
							target.Location.X * target.Scaling, 
							target.Location.Y * target.Scaling, 
							textureData.Width * target.Scaling, 
							textureData.Height * target.Scaling), 
						new Rectangle(new Point(0, 0), textureData.Size), 
						GraphicsUnit.Pixel
					);

					textureData.Dispose();
				}
			}

			// 次ページへ移動  リストの最大ページを超えたら印刷を終了し、それ以外は印刷を続行
			e.HasMorePages = ++this.NowPrintingPage < this.PrintDataList.Count;
		}


		/// <summary>
		/// 印刷が完了した際に実行される。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _PrintDocument_EndPrint(object sender, PrintEventArgs e)
		{
			// 印刷完了をログ表示
			LogFileManager.WriteLine(Properties.Resources.Msg_PrintMgr_PrintEnd);

			// 印刷対象のテクスチャ情報を全てクリア
			foreach (List<PrintData> pdl in this.PrintDataList) pdl.Clear();
			this.PrintDataList.Clear();

			// 読み込んだ統合画像を全て解放しリストをクリア
			foreach (KeyValuePair<string, Image> textureData in this.TextureList) textureData.Value.Dispose();
			this.TextureList.Clear();

			// ページ数をリセット
			this.NowPrintingPage = this.NowRegistingPage = 0;
		}

		#endregion


		#region 解放

		/// <summary>
		/// デバイスやテクスチャの解放を行う。
		/// </summary>
		private void _Dispose()
		{
		}

		#endregion


		#region 外部から呼び出されるメソッド群

		#region Init/Dispose

		/// <summary>
		/// 印刷管理クラスの静的インスタンス生成及び使用するプリンタオブジェクト等の初期化を行う。
		/// インスタンス生成後に１度しか実行できない点に注意。
		/// </summary>
		public static bool Initialize()
		{
			return Muphic.Manager.PrintManager.Instance._Initialize();
		}

		/// <summary>
		/// 印刷管理クラスで使用されているアンマネージリソースを解放する。
		/// </summary>
		public static void Dispose()
		{
			Muphic.Manager.PrintManager.Instance._Dispose();
		}

		#endregion

		#region Regist

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標は Manager 側で検索 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		public static void Regist(String className)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, 0, false, 1.0F, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標は Manager 側で検索 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void Regist(String className, float scaling)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, 0, false, scaling, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標は Manager 側で検索 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Regist(String className, byte alpha)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, 0, false, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state 指定 / 座標は Manager 側で検索 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		public static void Regist(String className, int state)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, state, false, 1.0F, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state 指定 / 座標は Manager 側で検索 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Regist(String className, int state, byte alpha)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, state, false, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="keyName">登録するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		public static void Regist(String keyName, int xLocation, int yLocation)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="keyName">登録するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Regist(String keyName, int xLocation, int yLocation, byte alpha)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state 指定 / 座標指定 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		public static void Regist(String className, int xLocation, int yLocation, int state)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), state, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過無し / 拡大・縮小無し / 90 度回転指定 / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		/// <param name="isLotation90">テクスチャを 90 度右回転させて登録する場合は true、それ以外は false。</param>
		public static void Regist(String className, int xLocation, int yLocation, bool isLotation90)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), 0, false, 1.0F, true, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過無し / 拡大・縮小率指定 / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void Regist(String className, int xLocation, int yLocation, float scaling)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), 0, false, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 拡大・縮小無し / 回転無し / フィルタ指定)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Regist(String className, int xLocation, int yLocation, Color filter)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), 0, false, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 拡大・縮小率指定 / 回転無し / フィルタ指定)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Regist(String className, int xLocation, int yLocation, float scaling, Color filter)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), 0, false, scaling, 0.0F, filter);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state 指定 / 座標指定 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Regist(String className, int xLocation, int yLocation, int state, byte alpha)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), state, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 拡大・縮小率指定 / 回転角指定 / フィルタ指定)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		/// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="lotationAngle">テクスチャの回転角 (ラジアン右回転)。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Regist(String className, int xLocation, int yLocation, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), state, false, scaling, lotationAngle, filter);
		}


		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="keyName">登録するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		public static void Regist(String keyName, Point location)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, location, 0, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過無し / 拡大・縮小率指定 / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="keyName">登録するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void Regist(String keyName, Point location, float scaling)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, location, 0, false, scaling, 0.0F, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="keyName">登録するキー (クラス名),またはテクスチャ名。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Regist(String keyName, Point location, byte alpha)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, location, 0, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state 指定 / 座標指定 / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		public static void Regist(String className, Point location, int state)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, state, false, 1.0F, 0.0F, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過無し / 拡大・縮小無し / 90 度回転指定 / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="isLotation90">テクスチャを 90 度右回転させて登録する場合は true、それ以外は false。</param>
		public static void Regist(String className, Point location, bool isLotation90)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, 0, false, 1.0F, true, Color.White);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 拡大・縮小無し / 回転無し / フィルタ指定)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Regist(String className, Point location, Color filter)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, 0, false, 1.0F, 0.0F, filter);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 拡大・縮小率指定 / 回転無し / フィルタ指定)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="keyName">登録するキー (クラス名), またはテクスチャ名。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Regist(String keyName, Point location, float scaling, Color filter)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, location, 0, false, scaling, 0.0F, filter);
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state 指定 / 座標指定 / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		public static void Regist(String className, Point location, int state, byte alpha)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, state, false, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 透過度指定 / 拡大・縮小率指定 / 回転無し / フィルタ無し)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		public static void Regist(String className, Point location, byte alpha, float scaling)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, 0, false, scaling, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		}

		/// <summary>
		/// 印刷するテクスチャを登録する (state = 0 / 座標指定 / 拡大・縮小率指定 / 回転角指定 / フィルタ指定)。
		/// DrawManager の Draw メソッドと同様に使用する。
		/// </summary>
		/// <param name="className">登録するキー (クラス名)。</param>
		/// <param name="location">登録するテクスチャの左上座標。</param>
		/// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		/// <param name="scaling">拡大・縮小率。</param>
		/// <param name="lotationAngle">テクスチャの回転角 (ラジアン右回転)。</param>
		/// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		public static void Regist(String className, Point location, int state, float scaling, float lotationAngle, Color filter)
		{
			Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, state, false, scaling, lotationAngle, filter);
		}

		#endregion

		#region RegistCenter

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標は Manager 側で検索 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		//public static void RegistCenter(String className)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, 0, true, 1.0F, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標は Manager 側で検索 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		//public static void RegistCenter(String className, byte alpha)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, 0, true, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state 指定 / 座標は Manager 側で検索 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		//public static void RegistCenter(String className, int state)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, state, true, 1.0F, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state 指定 / 座標は Manager 側で検索 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		///// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		//public static void RegistCenter(String className, int state, byte alpha)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, state, true, 1.0F, Color.FromArgb(alpha, 255, 255, 255));
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="keyName">登録するキー (クラス名), またはテクスチャ名。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		//public static void RegistCenter(String keyName, int xLocation, int yLocation)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="keyName">登録するキー (クラス名), またはテクスチャ名。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		///// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		//public static void RegistCenter(String keyName, int xLocation, int yLocation, byte alpha)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state 指定 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		///// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		//public static void RegistCenter(String className, int xLocation, int yLocation, int state)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), state, true, 1.0F, 0.0F, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 90 度回転指定 / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		///// <param name="isLotation90">テクスチャを 90 度右回転させて登録する場合は true、それ以外は false。</param>
		//public static void RegistCenter(String className, int xLocation, int yLocation, bool isLotation90)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), 0, true, 1.0F, true, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小率指定 / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		///// <param name="scaling">拡大・縮小率。</param>
		//public static void RegistCenter(String className, int xLocation, int yLocation, float scaling)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), 0, true, scaling, 0.0F, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 拡大・縮小無し / 回転無し / フィルタ指定)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		///// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		//public static void RegistCenter(String className, int xLocation, int yLocation, Color filter)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), 0, true, 1.0F, 0.0F, filter);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 拡大・縮小率指定 / 回転無し / フィルタ指定)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		///// <param name="scaling">拡大・縮小率。</param>
		///// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		//public static void RegistCenter(String className, int xLocation, int yLocation, float scaling, Color filter)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), 0, true, scaling, 0.0F, filter);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state 指定 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		///// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		///// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		//public static void RegistCenter(String className, int xLocation, int yLocation, int state, byte alpha)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), state, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 拡大・縮小率指定 / 回転角指定 / フィルタ指定)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="xLocation">登録するテクスチャの左上ｘ座標。</param>
		///// <param name="yLocation">登録するテクスチャの左上ｙ座標。</param>
		///// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		///// <param name="scaling">拡大・縮小率。</param>
		///// <param name="lotationAngle">テクスチャの回転角 (ラジアン右回転)。</param>
		///// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		//public static void RegistCenter(String className, int xLocation, int yLocation, int state, float scaling, float lotationAngle, Color filter)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, new Point(xLocation, yLocation), state, true, scaling, lotationAngle, filter);
		//}


		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="keyName">登録するキー (クラス名),またはテクスチャ名。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		//public static void RegistCenter(String keyName, Point location)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, location, 0, true, 1.0F, 0.0F, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小率指定 / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="keyName">登録するキー (クラス名),またはテクスチャ名。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="scaling">拡大・縮小率。</param>
		//public static void RegistCenter(String keyName, Point location, float scaling)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, location, 0, true, scaling, 0.0F, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="keyName">登録するキー (クラス名),またはテクスチャ名。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		//public static void RegistCenter(String keyName, Point location, byte alpha)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, location, 0, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state 指定 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		//public static void RegistCenter(String className, Point location, int state)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, state, true, 1.0F, 0.0F, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過無し / 拡大・縮小無し / 90 度回転指定 / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="isLotation90">テクスチャを 90 度右回転させて登録する場合は true、それ以外は false。</param>
		//public static void RegistCenter(String className, Point location, bool isLotation90)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, 0, true, 1.0F, true, Color.White);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 拡大・縮小無し / 回転無し / フィルタ指定)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		//public static void RegistCenter(String className, Point location, Color filter)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, 0, true, 1.0F, 0.0F, filter);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 拡大・縮小率指定 / 回転無し / フィルタ指定)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="keyName">登録するキー (クラス名), またはテクスチャ名。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="scaling">拡大・縮小率。</param>
		///// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		//public static void RegistCenter(String keyName, Point location, float scaling, Color filter)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(keyName, location, 0, true, scaling, 0.0F, filter);
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state 指定 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小無し / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		///// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		//public static void RegistCenter(String className, Point location, int state, byte alpha)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, state, true, 1.0F, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 透過度指定 / 拡大・縮小率指定 / 回転無し / フィルタ無し)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="alpha">テクスチャの透過度 (0～255)。</param>
		///// <param name="scaling">拡大・縮小率。</param>
		//public static void RegistCenter(String className, Point location, byte alpha, float scaling)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, 0, true, scaling, 0.0F, Color.FromArgb(alpha, 255, 255, 255));
		//}

		///// <summary>
		///// 印刷するテクスチャを登録する (state = 0 / 座標指定 (中心座標) / 拡大・縮小率指定 / 回転角指定 / フィルタ指定)。
		///// DrawManager の Draw メソッドと同様に使用する。
		///// </summary>
		///// <param name="className">登録するキー (クラス名)。</param>
		///// <param name="location">登録するテクスチャの左上座標。</param>
		///// <param name="state">現在のクラスの状態 (これにより登録するテクスチャが変わる)。</param>
		///// <param name="scaling">拡大・縮小率。</param>
		///// <param name="lotationAngle">テクスチャの回転角 (ラジアン右回転)。</param>
		///// <param name="filter">印刷の際にテクスチャと掛け合わせる、透過情報を含む色フィルタ。</param>
		//public static void RegistCenter(String className, Point location, int state, float scaling, float lotationAngle, Color filter)
		//{
		//    Muphic.Manager.PrintManager.Instance._RegistTexture(className, location, state, true, scaling, lotationAngle, filter);
		//}

		#endregion

		#region RegistString

		/// <summary>
		/// 印刷対象の文字列を登録する。
		/// </summary>
		/// <param name="text">印刷対象の文字列。</param>
		/// <param name="location">印刷対象の文字列の描画先の座標。</param>
		/// <param name="size">印刷対象のフォントの大きさ。</param>
		public static void RegistString(string text, Point location, float size)
		{
			Muphic.Manager.PrintManager.Instance._RegistString(text, location, size);
		}

		#endregion

		#region Print

		/// <summary>
		/// 現在登録されているテクスチャを 1 ページ目からすべて印刷し、登録されている全てのテクスチャがクリアする。
		/// </summary>
		public static void Print()
		{
			Muphic.Manager.PrintManager.Instance._PrintStart();
		}

		/// <summary>
		/// 現在登録されているテクスチャを指定したページからすべて印刷し、登録されている全てのテクスチャがクリアする。
		/// </summary>
		/// <param name="startPage"></param>
		public static void Print(int startPage)
		{
			Muphic.Manager.PrintManager.Instance._PrintStart(startPage);
		}

		#endregion

		#region Page

		/// <summary>
		/// 印刷対象のテクスチャを登録するページを、次のページへ移動する。
		/// </summary>
		public static void NextPage()
		{
			Muphic.Manager.PrintManager.Instance._ChangePageNext();
		}

		/// <summary>
		/// 印刷対象のテクスチャを登録するページを、前のページへ移動する。
		/// </summary>
		public static void PreviewPage()
		{
			Muphic.Manager.PrintManager.Instance._ChangePagePrev();
		}

		/// <summary>
		/// 印刷対象のテクスチャを登録するページを、指定したページへ移動する。
		/// </summary>
		/// <param name="page">登録するページ。</param>
		public static void ChangePage(int page)
		{
			Muphic.Manager.PrintManager.Instance._ChangePage(page);
		}

		#endregion

		#endregion

	}
}
