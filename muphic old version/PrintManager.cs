using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace muphic
{
	#region ver.1.0.0
	/*
	/// <summary>
	/// PrintManager の概要の説明です。
	/// </summary>
	public class PrintManager
	{
		int NowRegistingNum = -1;										//(登録中に限り)今現在PointQueueのどの要素を登録ししてるかを調べる変数
		int NowPrintingNum = 0;											//(印刷中に限り)今現在PointQueueのどの要素を印刷しているかを調べる変数(PrintPageで使用)
		private static PrintManager printManager;
		private PrintDocument pd;
		private Hashtable BitmapTable;									//ビットマップファイルを格納しているようなもの(キーはファイル名)
		private ArrayList[] PointQueue;									//印刷するときの座標を格納している待ち行列(キーはファイル名)3枚目に印刷するものは、要素2に入れればよい

		public PrintManager()
		{
			BitmapTable = new Hashtable();
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(PrintPage);
			pd.EndPrint += new PrintEventHandler(EndPrint);
			muphic.PrintManager.printManager = this;
		}

		/// <summary>
		/// ビットマップファイルを印刷できる状態にして待機させる(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">登録するビットマップファイルのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void RegistBitmap(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			RegistBitmap(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// ビットマップファイルを印刷できる状態にして待機させる(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">登録するビットマップファイルのキー</param>
		/// <param name="location">登録する座標</param>
		/// <param name="state">そのクラスの状態(状態によって登録するを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void RegistBitmap(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);	//登録するファイルのファイル名を取得
			if(fname == null)														//ファイル名が登録されていない場合
			{
				return;																//必殺"何もしない"
			}

			if(BitmapTable.ContainsKey(fname) == false)
			{
				Bitmap b = new Bitmap(fname);										//ファイル名をもとにBitmapクラスをインスタンス化
				BitmapTable.Add(fname, b);											//BitmapTableに追加
			}
			if(isCenter)															//真ん中の場合の修正
			{
				Bitmap b = (Bitmap)BitmapTable[fname];
				location.X -= b.Width / 2;
				location.Y -= b.Height / 2;
			}
			PointQueue[NowRegistingNum].Add(fname);
			PointQueue[NowRegistingNum].Add(location);
			
//			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
//			if(fname == null)												//ファイル名が登録されていない場合
//			{
//				return;														//必殺"何もしない"
//			}
//			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
//			Point center = new Point(0, 0);
//			if(isCenter)													//真ん中で表示する場合
//			{
//				Rectangle r = muphic.PointManager.Get(ClassName, state);
//				center.X = r.Width / 2;
//				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
//			}
//			//こっちは、1倍固定バージョン
//			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
//			//こっちは倍率を変えることができるバージョン
//			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
//			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width, ra.Height), center, 0, new Point(ra.X, ra.Y), Color.FromArgb(255, 255, 255));

		}

		/// <summary>
		/// PointQueueに登録する対象の要素を変更する
		/// </summary>
		/// <param name="NewNum"></param>
		public void ChangeRegistingPage(int NewNum)
		{
			if(NowRegistingNum >= NewNum)								//もし、新しいページがまでの許容範囲内なら
			{
				this.NowPrintingNum = NewNum;							//変更だけして
				return;													//終了
			}
			this.NowRegistingNum = NewNum;								//もし、許容範囲外なら
			ArrayList[] als = new ArrayList[NewNum+1];					//新しく作らなければならない
			int i;

			for(i=0;i<PointQueue.Length;i++)							//古い許容範囲の分は
			{															//普通にコピー
				als[i] = PointQueue[i];
			}
			for(i=i;i<als.Length;i++)									//古い許容範囲外の分は
			{															//新しくインスタンス化
				als[i] = new ArrayList();
			}
			PointQueue = als;											//新しく作ったものをPointQueueに代入
		}

		/// <summary>
		/// ページの印刷を開始するメソッド
		/// </summary>
		public void BeginPrint()
		{
			pd.DefaultPageSettings.Landscape = true;
			//pd.PrinterSettings.LandscapeAngle = 90;
			pd.Print();
		}

		Rectangle RealField;														//印刷用のフィールドで、比を4:3に修正したもの
		SizeF div;																	//今までのフィールドと新しいフィールドのサイズ比

		/// <summary>
		/// ChangeFieldで使う今までのフィールドと新しいフィールドのサイズ比を決定するメソッド
		/// </summary>
		/// <param name="Field">印刷用のフィールド</param>
		private void DecideDiv(Rectangle Field)
		{
			if(Field.Width/4 > Field.Height/3)										//縦の方が比が小さいので
			{																		//縦に合わせる
				Size RealSize = new Size(Field.Height/3*4, Field.Height);
				RealField = new Rectangle(Field.X+(Field.Width-RealSize.Width)/2, Field.Y, RealSize.Width, RealSize.Height);
			}
			else if(Field.Width/4 < Field.Height/3)									//横の方が比が小さいので
			{																		//横に合わせる
				Size RealSize = new Size(Field.Width, Field.Width/4*3);
				RealField = new Rectangle(Field.X, Field.Y+(Field.Height-RealSize.Height)/2, RealSize.Width, RealSize.Height);
			}
			else
			{
				RealField = Field;
			}
			div = new SizeF(RealField.Width / 1024, RealField.Height / 768);	//今までのフィールドと新しいフィールドのサイズ比
		}

		/// <summary>
		/// 今までの1024×768のフィールドから印刷用のフィールドへと切り替えるメソッド
		/// </summary>
		/// <param name="src">切り替える対象の四角形</param>
		/// <param name="Field">印刷用のフィールド</param>
		/// <returns></returns>
		private void ChangeField(ref Rectangle src)
		{
//			Rectangle answer = new Rectangle(0, 0, 0, 0);
//			answer.Width = (int)(src.Width / div.Width);
//			answer.Height = (int)(src.Height / div.Height);
//			answer.X = (int)(src.X / div.Width + RealField.X);
//			answer.Y = (int)(src.Y / div.Height + RealField.Y);
//			return answer;
			src.Width = (int)(src.Width / div.Width);
			src.Height = (int)(src.Height / div.Height);
			src.X = (int)(src.X / div.Width + RealField.X);
			src.Y = (int)(src.Y / div.Height + RealField.Y);
		}

		/// <summary>
		/// 実際に印刷をするメソッド。印刷するページの分だけこれが呼ばれる。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(this.NowPrintingNum == 0)																//初めてなら
			{
				this.DecideDiv(e.MarginBounds);															//フィールドの比を決定させる
			}
			Graphics g = e.Graphics;
			for(int i=0;i<PointQueue[this.NowPrintingNum].Count/2;i++)
			{
				String fname = (String)PointQueue[NowPrintingNum][i*2];									//PointQueueからファイル名を取り出す
				Point location = (Point)PointQueue[NowPrintingNum][i*2+1];								//PointQueueから座標を取り出す
				Bitmap b = (Bitmap)BitmapTable[fname];													//ファイル名をもとにBitmapクラスを取り出す
				Rectangle src = new Rectangle(0, 0, b.Width, b.Height);									//ビットマップファイルの幅・高さを指定する
				//src = this.ChangeField(src);															//印刷用に座標を変換する
				Rectangle dest = new Rectangle(location.X, location.Y, b.Width, b.Height);				//貼り付け先の座標を指定する
				g.DrawImage(b, dest, src, GraphicsUnit.Pixel);											//貼り付け
			}
			this.NowPrintingNum++;
			if(NowPrintingNum == PointQueue.Length)														//もし、すべて印刷し終えたなら
			{
				e.HasMorePages = false;																	//印刷終了
			}
			else
			{
				e.HasMorePages = true;
			}System.Diagnostics.Debug.WriteLine(e.PageBounds, "Page");
			System.Diagnostics.Debug.WriteLine(e.MarginBounds, "Margin");
		}

		/// <summary>
		/// 実際に印刷が開始されたときにおこるイベント。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void StartPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//this.NowPrintingNum = 0;												//ページの初期化
		}

		/// <summary>
		/// 実際の印刷が終了されたときにおこるイベント。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BitmapTable.Clear();
			for(int i=0;i<PointQueue.Length;i++)
			{
				PointQueue[i].Clear();
			}
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			this.NowPrintingNum = 0;												//ページの初期化
			this.NowRegistingNum = -1;												//登録対象ページの方も初期化
		}

//		public static void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//		{
//		}

		
		/// <summary>
		/// 登録する対象のページを変更する
		/// </summary>
		/// <param name="NewPage"></param>
		static public void ChangePage(int NewPage)
		{
			muphic.PrintManager.printManager.ChangeRegistingPage(NewPage-1);			//ここでの変数はページ数で、あっちで呼ばれるのは要素数なことに注意
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Regist(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Regist(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Regist(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Regist(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void RegistCenter(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, true);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void RegistCenter(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, true);
		}

		
		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void RegistCenter(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void RegistCenter(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// ページの印刷を開始する
		/// </summary>
		public static void Print()
		{
			muphic.PrintManager.printManager.BeginPrint();
		}
	}
	*/
	#endregion
	
	#region ver.2.0.0 文字列印刷機能付加
	/*
	/// <summary>
	/// PrintManager の概要の説明です。
	/// </summary>
	public class PrintManager
	{
		int NowRegistingNum = -1;										//(登録中に限り)今現在PointQueueのどの要素を登録ししてるかを調べる変数
		int NowPrintingNum = 0;											//(印刷中に限り)今現在PointQueueのどの要素を印刷しているかを調べる変数(PrintPageで使用)
		private static PrintManager printManager;
		private PrintDocument pd;
		private Hashtable BitmapTable;									//ビットマップファイルを格納しているようなもの(キーはファイル名)
		private ArrayList[] PointQueue;									//印刷するときの座標を格納している待ち行列(キーはファイル名)3枚目に印刷するものは、要素2に入れればよい
		
		private ArrayList[] TextList;									// 文字列データ 文字列・x座標・y座標・色・大きさ の順
		private const string fontname = "MeiryoKe_Gothic";				// 使用するフォント

		public PrintManager()
		{
			BitmapTable = new Hashtable();
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			TextList = new ArrayList[1];
			TextList[0] = new ArrayList();
			pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(PrintPage);
			pd.EndPrint += new PrintEventHandler(EndPrint);
			muphic.PrintManager.printManager = this;
		}
		

		/// <summary>
		/// ビットマップファイルを印刷できる状態にして待機させる(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">登録するビットマップファイルのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void RegistBitmap(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			RegistBitmap(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// ビットマップファイルを印刷できる状態にして待機させる(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">登録するビットマップファイルのキー</param>
		/// <param name="location">登録する座標</param>
		/// <param name="state">そのクラスの状態(状態によって登録するを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void RegistBitmap(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);	//登録するファイルのファイル名を取得
			if(fname == null)														//ファイル名が登録されていない場合
			{
				return;																//必殺"何もしない"
			}

			if(BitmapTable.ContainsKey(fname) == false)
			{
				Bitmap b = new Bitmap(fname);										//ファイル名をもとにBitmapクラスをインスタンス化
				BitmapTable.Add(fname, b);											//BitmapTableに追加
			}
			if(isCenter)															//真ん中の場合の修正
			{
				Bitmap b = (Bitmap)BitmapTable[fname];
				location.X -= b.Width / 2;
				location.Y -= b.Height / 2;
			}
			PointQueue[NowRegistingNum].Add(fname);
			PointQueue[NowRegistingNum].Add(location);
		}

		/// <summary>
		/// PointQueueに登録する対象の要素を変更する
		/// </summary>
		/// <param name="NewNum"></param>
		public void ChangeRegistingPage(int NewNum)
		{
			if(NowRegistingNum >= NewNum)								//もし、新しいページがまでの許容範囲内なら
			{
				this.NowPrintingNum = NewNum;							//変更だけして
				return;													//終了
			}
			this.NowRegistingNum = NewNum;								//もし、許容範囲外なら
			ArrayList[] als = new ArrayList[NewNum+1];					//新しく作らなければならない
			ArrayList[] alst= new ArrayList[NewNum+1];
			int i;

			for(i=0;i<PointQueue.Length;i++)							//古い許容範囲の分は
			{															//普通にコピー
				als[i] = PointQueue[i];
				alst[i]= TextList[i];
			}
			for(i=i;i<als.Length;i++)									//古い許容範囲外の分は
			{															//新しくインスタンス化
				als[i] = new ArrayList();
				alst[i]= new ArrayList();
			}
			PointQueue = als;											//新しく作ったものをPointQueueに代入
			TextList = alst;
		}

		/// <summary>
		/// ページの印刷を開始するメソッド
		/// </summary>
		public void BeginPrint()
		{
			pd.DefaultPageSettings.Landscape = true;
			//pd.PrinterSettings.LandscapeAngle = 90;
			pd.Print();
		}

		Rectangle RealField;														//印刷用のフィールドで、比を4:3に修正したもの
		SizeF div;																	//今までのフィールドと新しいフィールドのサイズ比

		/// <summary>
		/// ChangeFieldで使う今までのフィールドと新しいフィールドのサイズ比を決定するメソッド
		/// </summary>
		/// <param name="Field">印刷用のフィールド</param>
		private void DecideDiv(Rectangle Field)
		{
			if(Field.Width/4 > Field.Height/3)										//縦の方が比が小さいので
			{																		//縦に合わせる
				Size RealSize = new Size(Field.Height/3*4, Field.Height);
				RealField = new Rectangle(Field.X+(Field.Width-RealSize.Width)/2, Field.Y, RealSize.Width, RealSize.Height);
			}
			else if(Field.Width/4 < Field.Height/3)									//横の方が比が小さいので
			{																		//横に合わせる
				Size RealSize = new Size(Field.Width, Field.Width/4*3);
				RealField = new Rectangle(Field.X, Field.Y+(Field.Height-RealSize.Height)/2, RealSize.Width, RealSize.Height);
			}
			else
			{
				RealField = Field;
			}
			div = new SizeF(RealField.Width / 1024, RealField.Height / 768);	//今までのフィールドと新しいフィールドのサイズ比
		}

		/// <summary>
		/// 今までの1024×768のフィールドから印刷用のフィールドへと切り替えるメソッド
		/// </summary>
		/// <param name="src">切り替える対象の四角形</param>
		/// <param name="Field">印刷用のフィールド</param>
		/// <returns></returns>
		private void ChangeField(ref Rectangle src)
		{
			//			Rectangle answer = new Rectangle(0, 0, 0, 0);
			//			answer.Width = (int)(src.Width / div.Width);
			//			answer.Height = (int)(src.Height / div.Height);
			//			answer.X = (int)(src.X / div.Width + RealField.X);
			//			answer.Y = (int)(src.Y / div.Height + RealField.Y);
			//			return answer;
			src.Width = (int)(src.Width / div.Width);
			src.Height = (int)(src.Height / div.Height);
			src.X = (int)(src.X / div.Width + RealField.X);
			src.Y = (int)(src.Y / div.Height + RealField.Y);
		}

		/// <summary>
		/// 実際に印刷をするメソッド。印刷するページの分だけこれが呼ばれる。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(this.NowPrintingNum == 0)																//初めてなら
			{
				this.DecideDiv(e.MarginBounds);															//フィールドの比を決定させる
			}
			Graphics g = e.Graphics;
			for(int i=0;i<PointQueue[this.NowPrintingNum].Count/2;i++)
			{
				String fname = (String)PointQueue[NowPrintingNum][i*2];									//PointQueueからファイル名を取り出す
				Point location = (Point)PointQueue[NowPrintingNum][i*2+1];								//PointQueueから座標を取り出す
				Bitmap b = (Bitmap)BitmapTable[fname];													//ファイル名をもとにBitmapクラスを取り出す
				Rectangle src = new Rectangle(0, 0, b.Width, b.Height);									//ビットマップファイルの幅・高さを指定する
				//src = this.ChangeField(src);															//印刷用に座標を変換する
				Rectangle dest = new Rectangle(location.X, location.Y, b.Width, b.Height);				//貼り付け先の座標を指定する
				g.DrawImage(b, dest, src, GraphicsUnit.Pixel);											//貼り付け
			}
			
			// テキストも印刷
			for(int i=0; i<this.TextList[this.NowPrintingNum].Count/5; i++)
			{
				// 各データの取り出し
				string str = (string)this.TextList[this.NowPrintingNum][i*5];
				int x = (int)this.TextList[this.NowPrintingNum][i*5+1];
				int y = (int)this.TextList[this.NowPrintingNum][i*5+2];
				Brush color = (Brush)this.TextList[this.NowPrintingNum][i*5+3];
				int size = (int)this.TextList[this.NowPrintingNum][i*5+4];
				
				// フォント生成
				System.Drawing.Font font = new System.Drawing.Font(fontname, size);
				
				// 貼り付け
				g.DrawString(str, font, color, (float)x, (float)y, new StringFormat());
			}
			// テキストリストのクリア
			TextList[this.NowPrintingNum].Clear();
			
			this.NowPrintingNum++;
			if(NowPrintingNum == PointQueue.Length)														//もし、すべて印刷し終えたなら
			{
				e.HasMorePages = false;																	//印刷終了
			}
			else
			{
				e.HasMorePages = true;
			}System.Diagnostics.Debug.WriteLine(e.PageBounds, "Page");
			System.Diagnostics.Debug.WriteLine(e.MarginBounds, "Margin");
		}

		/// <summary>
		/// 実際に印刷が開始されたときにおこるイベント。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void StartPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//this.NowPrintingNum = 0;												//ページの初期化
		}

		/// <summary>
		/// 実際の印刷が終了されたときにおこるイベント。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BitmapTable.Clear();
			for(int i=0;i<PointQueue.Length;i++)
			{
				PointQueue[i].Clear();
			}
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			this.NowPrintingNum = 0;												//ページの初期化
			this.NowRegistingNum = -1;												//登録対象ページの方も初期化
		}
		
		/// <summary>
		/// 印刷するテキストをリストへ登録する
		/// </summary>
		/// <param name="str"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		public void AddText(String str, int x, int y, System.Drawing.Brush color, int size)
		{
			TextList[this.NowRegistingNum].Add(str);
			TextList[this.NowRegistingNum].Add(x);
			TextList[this.NowRegistingNum].Add(y);
			TextList[this.NowRegistingNum].Add(color);
			TextList[this.NowRegistingNum].Add(size);
		}
		
		
		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// 登録する対象のページを変更する
		/// </summary>
		/// <param name="NewPage"></param>
		static public void ChangePage(int NewPage)
		{
			muphic.PrintManager.printManager.ChangeRegistingPage(NewPage-1);			//ここでの変数はページ数で、あっちで呼ばれるのは要素数なことに注意
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Regist(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Regist(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Regist(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Regist(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void RegistCenter(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, true);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void RegistCenter(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, true);
		}

		
		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void RegistCenter(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void RegistCenter(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// ページの印刷を開始する
		/// </summary>
		public static void Print()
		{
			muphic.PrintManager.printManager.BeginPrint();
		}
		
		/// <summary>
		/// 印刷する文字列を登録するメソッド
		/// </summary>
		/// <param name="str">印刷する文字列</param>
		/// <param name="x">印刷する文字列の左上x座標</param>
		/// <param name="y">印刷する文字列の左上y座標</param>
		static public void RegistString(String str, int x, int y)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, System.Drawing.Brushes.Black, 20);
		}
		
		/// <summary>
		/// 印刷する文字列を登録するメソッド
		/// </summary>
		/// <param name="str">印刷する文字列</param>
		/// <param name="x">印刷する文字列の左上x座標</param>
		/// <param name="y">印刷する文字列の左上y座標</param>
		/// <param name="color">印刷する文字列の色</param>
		static public void RegistString(String str, int x, int y, System.Drawing.Brush color)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, color, 20);
		}
		
		/// <summary>
		/// 印刷する文字列を登録するメソッド
		/// </summary>
		/// <param name="str">印刷する文字列</param>
		/// <param name="x">印刷する文字列の左上x座標</param>
		/// <param name="y">印刷する文字列の左上y座標</param>
		/// <param name="size">印刷する文字列の大きさ(指定しなければ20)</param>
		static public void RegistString(String str, int x, int y, int size)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, System.Drawing.Brushes.Black, size);
		}
		
		/// <summary>
		/// 印刷する文字列を登録するメソッド
		/// </summary>
		/// <param name="str">印刷する文字列</param>
		/// <param name="x">印刷する文字列の左上x座標</param>
		/// <param name="y">印刷する文字列の左上y座標</param>
		/// <param name="color">印刷する文字列の色</param>
		/// <param name="size">印刷する文字列の大きさ(指定しなければ20)</param>
		static public void RegistString(String str, int x, int y, System.Drawing.Brush color, int size)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, color, size);
		}
	}
	*/
	#endregion
	
	#region ver.3.0.0 印刷範囲変更機能追加
	
	/// <summary>
	/// PrintManager の概要の説明です。
	/// </summary>
	public class PrintManager
	{
		int NowRegistingNum = -1;										//(登録中に限り)今現在PointQueueのどの要素を登録ししてるかを調べる変数
		int NowPrintingNum = 0;											//(印刷中に限り)今現在PointQueueのどの要素を印刷しているかを調べる変数(PrintPageで使用)
		private static PrintManager printManager;
		private PrintDocument pd;
		private Hashtable BitmapTable;									//ビットマップファイルを格納しているようなもの(キーはファイル名)
		private ArrayList[] PointQueue;									//印刷するときの座標を格納している待ち行列(キーはファイル名)3枚目に印刷するものは、要素2に入れればよい
		
		Rectangle RealField;											//印刷用のフィールドで、比を4:3に修正したもの
		Rectangle VirtualField;											//今までのフィールド、印刷を指定する仮想領域

		private ArrayList[] TextList;									// 文字列データ 文字列・x座標・y座標・色・大きさ の順
		private const string fontname = "MeiryoKe_Gothic";				// 使用するフォント
		
		public bool isExpand;											//用紙の大きさに合わせて拡大するかどうかのフラグ
		public bool isNotUseMarginBounds;								//プリンタに設定されている余白を使用しないかどうか
		public PrintManager()
		{
			BitmapTable = new Hashtable();
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			TextList = new ArrayList[1];
			TextList[0] = new ArrayList();
			pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(PrintPage);
			pd.EndPrint += new PrintEventHandler(EndPrint);
			pd.PrintController = new System.Drawing.Printing.StandardPrintController();
			muphic.PrintManager.printManager = this;
		}
		
		/// <summary>
		/// ビットマップファイルを印刷できる状態にして待機させる(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">登録するビットマップファイルのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void RegistBitmap(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			RegistBitmap(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// ビットマップファイルを印刷できる状態にして待機させる(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">登録するビットマップファイルのキー</param>
		/// <param name="location">登録する座標</param>
		/// <param name="state">そのクラスの状態(状態によって登録するを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void RegistBitmap(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);	//登録するファイルのファイル名を取得
			if(fname == null)														//ファイル名が登録されていない場合
			{
				return;																//必殺"何もしない"
			}

			if(BitmapTable.ContainsKey(fname) == false)
			{
				Bitmap b = new Bitmap(fname);										//ファイル名をもとにBitmapクラスをインスタンス化
				BitmapTable.Add(fname, b);											//BitmapTableに追加
			}
			if(isCenter)															//真ん中の場合の修正
			{
				Bitmap b = (Bitmap)BitmapTable[fname];
				location.X -= b.Width / 2;
				location.Y -= b.Height / 2;
			}
			PointQueue[NowRegistingNum].Add(fname);
			PointQueue[NowRegistingNum].Add(location);
		}
		
		/// <summary>
		/// PointQueueに登録する対象の要素を変更する
		/// </summary>
		/// <param name="NewNum"></param>
		public void ChangeRegistingPage(int NewNum)
		{
			if(NowRegistingNum >= NewNum)								//もし、新しいページがまでの許容範囲内なら
			{
				this.NowPrintingNum = NewNum;							//変更だけして
				return;													//終了
			}
			this.NowRegistingNum = NewNum;								//もし、許容範囲外なら
			ArrayList[] als = new ArrayList[NewNum+1];					//新しく作らなければならない
			ArrayList[] alst= new ArrayList[NewNum+1];
			int i;

			for(i=0;i<PointQueue.Length;i++)							//古い許容範囲の分は
			{															//普通にコピー
				als[i] = PointQueue[i];
				alst[i]= TextList[i];
			}
			for(i=i;i<als.Length;i++)									//古い許容範囲外の分は
			{															//新しくインスタンス化
				als[i] = new ArrayList();
				alst[i]= new ArrayList();
			}
			PointQueue = als;											//新しく作ったものをPointQueueに代入
			TextList = alst;
		}

		/// <summary>
		/// ChangeFieldで使う今までのフィールドと新しいフィールドのサイズ比を決定するメソッド
		/// </summary>
		/// <param name="Field">印刷用のフィールド</param>
		private void DecideDiv(Rectangle Field)
		{
			RectangleF RealF;
			if(Field.Width/VirtualField.Width > Field.Height/VirtualField.Height)	//縦の方が比が小さいので
			{																		//縦に合わせる
				SizeF RealSize = new SizeF((float)Field.Height/VirtualField.Height*VirtualField.Width, Field.Height);
				RealF = new RectangleF(Field.X+(Field.Width-RealSize.Width)/2, Field.Y, RealSize.Width, RealSize.Height);
			}
			else if(Field.Width/4 < Field.Height/3)									//横の方が比が小さいので
			{																		//横に合わせる
				SizeF RealSize = new SizeF(Field.Width, (float)Field.Width/VirtualField.Width*VirtualField.Height);
				RealF = new RectangleF(Field.X, Field.Y+(Field.Height-RealSize.Height)/2, RealSize.Width, RealSize.Height);
			}
			else
			{
				RealF = Field;
			}
			RealField = new Rectangle((int)RealF.X, (int)RealF.Y, (int)RealF.Width, (int)RealF.Height);
		}

		/// <summary>
		/// 今までの仮想フィールドから印刷用のフィールドへと切り替えるメソッド
		/// </summary>
		/// <param name="src">切り替える対象の四角形</param>
		/// <param name="Field">印刷用のフィールド</param>
		/// <returns></returns>
		private Rectangle ChangeField(Rectangle r)
		{
			float divX = (float)RealField.Width / VirtualField.Width;
			float divY = (float)RealField.Height / VirtualField.Height;
			r.X = r.X - VirtualField.X;
			r.X = (int)(r.X * divX);
			r.Width = (int)(r.Width * divX);
			r.X = r.X + RealField.X;
			r.Y = r.Y - VirtualField.Y;
			r.Y = (int)(r.Y * divY);
			r.Height = (int)(r.Height * divY);
			r.Y = r.Y + RealField.Y;
			//			src.Width = (int)(src.Width / div.Width);
			//			src.Height = (int)(src.Height / div.Height);
			//			src.X = (int)(src.X / div.Width + RealField.X);
			//			src.Y = (int)(src.Y / div.Height + RealField.Y);
			return r;
		}


		/// <summary>
		/// ページの印刷を開始するメソッド
		/// </summary>
		/// <param name="VirtualField"></param>
		/// <param name="isExpand">印刷を用紙に合わせて拡大するかどうか</param>
		public void BeginPrint(Rectangle VirtualField, bool isExpand)
		{
			this.VirtualField = VirtualField;
			pd.DefaultPageSettings.Landscape = true;
			//pd.PrinterSettings.LandscapeAngle = 90;
			this.isExpand = isExpand;
			this.isNotUseMarginBounds = false;
			pd.PrinterSettings.PrintToFile = true;
			pd.Print();
		}

		/// <summary>
		/// ページの印刷を開始するメソッド
		/// </summary>
		/// <param name="VirtualField"></param>
		/// <param name="isExpand">印刷を用紙に合わせて拡大するかどうか</param>
		/// <param name="isNotUseMarginBounds">余白を間に入れないかどうか</param>
		public void BeginPrint(Rectangle VirtualField, bool isExpand, bool isNotUseMarginBounds)
		{
			this.VirtualField = VirtualField;
			pd.DefaultPageSettings.Landscape = true;
			//pd.PrinterSettings.LandscapeAngle = 90;
			this.isExpand = isExpand;
			this.isNotUseMarginBounds = isNotUseMarginBounds;
			pd.PrinterSettings.PrintToFile = true;
			pd.Print();
		}

		/// <summary>
		/// 実際に印刷をするメソッド。印刷するページの分だけこれが呼ばれる。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(this.NowPrintingNum == 0)																//初めてなら
			{
				if(this.isExpand)
				{
					if(this.isNotUseMarginBounds)
					{
						this.DecideDiv(new Rectangle(e.PageBounds.X+20, e.PageBounds.Y+20, e.PageBounds.Width-40, e.PageBounds.Height-40));
					}
					else
					{
						this.DecideDiv(e.MarginBounds);								//フィールドの比を決定させる
					}
				}
				else
				{
					this.DecideDiv(new Rectangle((e.PageBounds.Width-VirtualField.Width)/2, (e.PageBounds.Height-VirtualField.Height)/2,
						VirtualField.Width, VirtualField.Height));										//デフォルトのフィールドのまま、真ん中に印刷させる
				}
			}
			Graphics g = e.Graphics;
			String fname;
			Point location;
			Bitmap b;
			Rectangle src, dest;
			for(int i=0;i<PointQueue[this.NowPrintingNum].Count/2;i++)
			{
				fname = (String)PointQueue[NowPrintingNum][i*2];									//PointQueueからファイル名を取り出す
				location = (Point)PointQueue[NowPrintingNum][i*2+1];								//PointQueueから座標を取り出す
				b = (Bitmap)BitmapTable[fname];														//ファイル名をもとにBitmapクラスを取り出す
				src = new Rectangle(0, 0, b.Width, b.Height);								//ビットマップファイルの幅・高さを指定する
				dest = new Rectangle(location.X, location.Y, b.Width, b.Height);				//貼り付け先の座標を指定する
				dest = this.ChangeField(dest);																//印刷用に座標を変換する
				g.DrawImage(b, dest, src, GraphicsUnit.Pixel);											//貼り付け
			}
			
			// テキストも印刷
			for(int i=0; i<this.TextList[this.NowPrintingNum].Count/5; i++)
			{
				// 各データの取り出し
				string str = (string)this.TextList[this.NowPrintingNum][i*5];
				int x = (int)this.TextList[this.NowPrintingNum][i*5+1];
				int y = (int)this.TextList[this.NowPrintingNum][i*5+2];
				Rectangle r = new Rectangle(x, y, 0, 0);
				r = this.ChangeField(r);
				Brush color = (Brush)this.TextList[this.NowPrintingNum][i*5+3];
				int size = (int)this.TextList[this.NowPrintingNum][i*5+4];
				
				// フォント生成
				System.Drawing.Font font = new System.Drawing.Font(fontname, size);
				
				// 貼り付け
				g.DrawString(str, font, color, (float)r.X, (float)r.Y, new StringFormat());
			}
			// テキストリストのクリア
			TextList[this.NowPrintingNum].Clear();
			
			this.NowPrintingNum++;
			if(NowPrintingNum == PointQueue.Length)														//もし、すべて印刷し終えたなら
			{
				e.HasMorePages = false;																	//印刷終了
			}
			else
			{
				e.HasMorePages = true;
			}System.Diagnostics.Debug.WriteLine(e.PageBounds, "Page");
			System.Diagnostics.Debug.WriteLine(e.MarginBounds, "Margin");
		}

		/// <summary>
		/// 実際に印刷が開始されたときにおこるイベント。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void StartPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//this.NowPrintingNum = 0;												//ページの初期化
		}

		/// <summary>
		/// 実際の印刷が終了されたときにおこるイベント。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			BitmapTable.Clear();
			for(int i=0;i<PointQueue.Length;i++)
			{
				PointQueue[i].Clear();
			}
			PointQueue = new ArrayList[1];
			PointQueue[0] = new ArrayList();
			this.NowPrintingNum = 0;												//ページの初期化
			this.NowRegistingNum = -1;												//登録対象ページの方も初期化
		}
		
		/// <summary>
		/// 印刷するテキストをリストへ登録する
		/// </summary>
		/// <param name="str"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		public void AddText(String str, int x, int y, System.Drawing.Brush color, int size)
		{
			TextList[this.NowRegistingNum].Add(str);
			TextList[this.NowRegistingNum].Add(x);
			TextList[this.NowRegistingNum].Add(y);
			TextList[this.NowRegistingNum].Add(color);
			TextList[this.NowRegistingNum].Add(size);
		}
		
		
		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// 登録する対象のページを変更する
		/// </summary>
		/// <param name="NewPage"></param>
		static public void ChangePage(int NewPage)
		{
			muphic.PrintManager.printManager.ChangeRegistingPage(NewPage-1);		//ここでの変数はページ数で、あっちで呼ばれるのは要素数なことに注意
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Regist(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Regist(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Regist(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Regist(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void RegistCenter(String ClassName)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, 0, true);
		}

		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void RegistCenter(String ClassName, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, state, true);
		}

		
		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心,state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void RegistCenter(String ClassName, int x, int y)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// どこにビットマップファイルを印刷するのかを登録する(座標は中心)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void RegistCenter(String ClassName, int x, int y, int state)
		{
			muphic.PrintManager.printManager.RegistBitmap(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// ページの印刷を開始する(デフォルトなので領域は1024×768)
		/// </summary>
		/// <param name="isExpand">印刷を用紙にあわせて拡大するかどうか</param>
		public static void Print(bool isExpand)
		{
			muphic.PrintManager.printManager.BeginPrint(new Rectangle(0, 0, 1024, 768), isExpand);
		}

		/// <summary>
		/// ページの印刷を開始する
		/// </summary>
		/// <param name="VirtualField">いままでどの領域で座標を指定してきたか</param>
		/// <param name="isExpand">印刷を用紙にあわせて拡大するかどうか</param>
		public static void Print(Rectangle VirtualField, bool isExpand)
		{
			muphic.PrintManager.printManager.BeginPrint(VirtualField, isExpand);
		}

		/// <summary>
		/// ページの印刷を開始する
		/// </summary>
		/// <param name="VirtualField">いままでどの領域で座標を指定してきたか</param>
		/// <param name="isExpand">印刷を用紙に合わせて拡大するかどうか</param>
		/// <param name="isNotUseMarginBounds">デフォルトの隙間を使わないかどうか(falseなら全領域を使う)</param>
		public static void Print(Rectangle VirtualField, bool isExpand, bool isNotUseMarginBounds)
		{
			muphic.PrintManager.printManager.BeginPrint(VirtualField, isExpand, isNotUseMarginBounds);
		}
		
		/// <summary>
		/// 印刷する文字列を登録するメソッド
		/// </summary>
		/// <param name="str">印刷する文字列</param>
		/// <param name="x">印刷する文字列の左上x座標</param>
		/// <param name="y">印刷する文字列の左上y座標</param>
		static public void RegistString(String str, int x, int y)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, System.Drawing.Brushes.Black, 20);
		}
		
		/// <summary>
		/// 印刷する文字列を登録するメソッド
		/// </summary>
		/// <param name="str">印刷する文字列</param>
		/// <param name="x">印刷する文字列の左上x座標</param>
		/// <param name="y">印刷する文字列の左上y座標</param>
		/// <param name="color">印刷する文字列の色</param>
		static public void RegistString(String str, int x, int y, System.Drawing.Brush color)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, color, 20);
		}
		
		/// <summary>
		/// 印刷する文字列を登録するメソッド
		/// </summary>
		/// <param name="str">印刷する文字列</param>
		/// <param name="x">印刷する文字列の左上x座標</param>
		/// <param name="y">印刷する文字列の左上y座標</param>
		/// <param name="size">印刷する文字列の大きさ(指定しなければ20)</param>
		static public void RegistString(String str, int x, int y, int size)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, System.Drawing.Brushes.Black, size);
		}
		
		/// <summary>
		/// 印刷する文字列を登録するメソッド
		/// </summary>
		/// <param name="str">印刷する文字列</param>
		/// <param name="x">印刷する文字列の左上x座標</param>
		/// <param name="y">印刷する文字列の左上y座標</param>
		/// <param name="color">印刷する文字列の色</param>
		/// <param name="size">印刷する文字列の大きさ(指定しなければ20)</param>
		static public void RegistString(String str, int x, int y, System.Drawing.Brush color, int size)
		{
			muphic.PrintManager.printManager.AddText(str, x, y, color, size);
		}
	}
	
	#endregion
}
