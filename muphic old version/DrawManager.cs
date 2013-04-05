using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace muphic
{
	#region Ver.1
	/*
	/// <summary>
	/// DrawManager の概要の説明です。
	/// </summary>
	public class DrawManager
	{
		private static DrawManager drawManager;
		private Hashtable TextureTable;
		private Device device;
		private Sprite sprite = null;

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			muphic.DrawManager.drawManager = this;
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.Windowed = true;								//ウィンドウモードの設定(ウィンドウモード)
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)

	#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//デバイスタイプで失敗していたらこれでなる
					device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//頂点描画先設定で失敗していたらこれでなる
						device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキー(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture[] texture = new Texture[FileName.Length];

			if(TextureTable.Contains(ClassName))
			{
				return;																		//既に登録されていたら終了
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture[i] = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);	//テクスチャのインスタンス化
				if(i == 0)
				{
					muphic.PointManager.Set(ClassName, p, bitmap.Size);						//座標データの登録(最初の画像の座標だけ)
				}
			}

			TextureTable.Add(ClassName, texture);
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName);				//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!TextureTable.ContainsKey(ClassName))						//クラスが登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			Texture[] texture = (Texture[])TextureTable[ClassName];
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			sprite.Draw2D(texture[state], center, 0, location, Color.FromArgb(255, 255, 255));
		}

		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		public void BeginDevice()
		{
			device.Clear(ClearFlags.Target, Color.White, 0, 0);				//画面のクリア
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
		}

		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		static public void Begin()
		{
			muphic.DrawManager.drawManager.BeginDevice();
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}
	}*/
	#endregion

	#region Ver.2
	/*
	/// <summary>
	/// DrawManager version2 ハッシュテーブルを2つにして同じテクスチャを重複呼び出ししないようにした。
	/// </summary>
	public class DrawManager
	{
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//ファイル名とテクスチャを関連付けている
		private Hashtable FileNameTable;						//クラスメイとファイル名の配列を関連付けている
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//この中には、それぞれ描画待ちの文字列たちが、(文字列、x、y、Color)の順に入っている

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			FileNameTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// フォントデータの構造体を作成
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();

			// 構造体に必要なデータをセット
			fd.Height = 24;
			fd.FaceName = "ＭＳ ゴシック";
			try
			{
				// フォントを作成
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// 例外発生
				MessageBox.Show("文字列描画エラー");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.Windowed = true;								//ウィンドウモードの設定(ウィンドウモード)
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)

	#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//頂点描画先設定で失敗していたらこれでなる
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//デバイスタイプで失敗していたらこれでなる
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキー(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(FileNameTable.Contains(ClassName))
			{
				return;																		//既に登録されていたら終了
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//テクスチャのインスタンス化
				rs[i] = new Rectangle(p, bitmap.Size);										//座標を入れとく
				if(!TextureTable.Contains(FileName[i]))										//テクスチャが既に登録されていなければ
				{
					TextureTable.Add(FileName[i], texture);									//TextureTableに格納
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//座標データの登録
			FileNameTable.Add(ClassName, FileName);											//FileNameTableに格納
		}

		public void DeleteTexture(String ClassName)
		{
			String[] filename = (String[])FileNameTable[ClassName];
			FileNameTable.Remove(ClassName);												//削除
			muphic.PointManager.Delete(ClassName);											//対応している座標も削除
			for(int i=0;i<filename.Length;i++)
			{
				if(!FileNameTable.Contains(filename[i]))									//もし他に同じファイルを参照している
				{																			//クラスがなかったら
					TextureTable.Remove(filename[i]);										//該当するテクスチャも削除
				}
			}
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!FileNameTable.ContainsKey(ClassName))						//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			String fname = (String)((String[])FileNameTable[ClassName])[state];//クラス名とstateから該当するテクスチャのファイル名を取得
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
		}

		public void AddText(String str, int x, int y, Color color)
		{
			//文字列はspriteが終ってから描画しないといけないので、spriteが終るまで一時TextListにためておく
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextListからのデータの取り出し
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//文字列の描画
			}
			TextList.Clear();												//ため込んでいた文字列をすべて削除
		}

		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		public void BeginDevice()
		{
			device.Clear(ClearFlags.Target, Color.White, 0, 0);				//画面のクリア
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//スプライトが終了したので、文字列を描画す
		}

		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		static public void Begin()
		{
			muphic.DrawManager.drawManager.BeginDevice();
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}*/
	#endregion
	
	#region Ver.3
	/// <summary>
	/// DrawManager version2 ハッシュテーブルを2つにして同じテクスチャを重複呼び出ししないようにした。
	/// DrawManager version3 DrawString機能を追加。詳細はDrawTextメソッドを参照。
	/// </summary>
	/*
	public class DrawManager
	{
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//ファイル名とテクスチャを関連付けている
		private Hashtable FileNameTable;						//クラスメイとファイル名の配列を関連付けている
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//この中には、それぞれ描画待ちの文字列たちが、(文字列、x、y、Color)の順に入っている

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			FileNameTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// フォントデータの構造体を作成
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();

			// 構造体に必要なデータをセット
			fd.Height = 24;
			fd.FaceName = "ＭＳ ゴシック";
			try
			{
				// フォントを作成
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// 例外発生
				MessageBox.Show("文字列描画エラー");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.Windowed = true;								//ウィンドウモードの設定(ウィンドウモード)
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)

	#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//頂点描画先設定で失敗していたらこれでなる
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//デバイスタイプで失敗していたらこれでなる
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキー(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(FileNameTable.Contains(ClassName))
			{
				return;																		//既に登録されていたら終了
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//テクスチャのインスタンス化
				rs[i] = new Rectangle(p, bitmap.Size);										//座標を入れとく
				if(!TextureTable.Contains(FileName[i]))										//テクスチャが既に登録されていなければ
				{
					TextureTable.Add(FileName[i], texture);									//TextureTableに格納
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//座標データの登録
			FileNameTable.Add(ClassName, FileName);											//FileNameTableに格納
		}

		public void DeleteTexture(String ClassName)
		{
			String[] filename = (String[])FileNameTable[ClassName];
			FileNameTable.Remove(ClassName);												//削除
			muphic.PointManager.Delete(ClassName);											//対応している座標も削除
			for(int i=0;i<filename.Length;i++)
			{
				if(!FileNameTable.Contains(filename[i]))									//もし他に同じファイルを参照している
				{																			//クラスがなかったら
					TextureTable.Remove(filename[i]);										//該当するテクスチャも削除
				}
			}
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!FileNameTable.ContainsKey(ClassName))						//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			String fname = (String)((String[])FileNameTable[ClassName])[state];//クラス名とstateから該当するテクスチャのファイル名を取得
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは、1倍固定バージョン
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
			//こっちは倍率を変えることができるバージョン
			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width, ra.Height), center, 0, new Point(ra.X, ra.Y), Color.FromArgb(255, 255, 255));

		}

		public void AddText(String str, int x, int y, Color color)
		{
			//文字列はspriteが終ってから描画しないといけないので、spriteが終るまで一時TextListにためておく
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextListからのデータの取り出し
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//文字列の描画
			}
			TextList.Clear();												//ため込んでいた文字列をすべて削除
		}

		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		public void BeginDevice()
		{
			device.Clear(ClearFlags.Target, Color.White, 0, 0);				//画面のクリア
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//スプライトが終了したので、文字列を描画す
		}

		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">冗長引数</param>
		/// <param name="y">冗長引数</param>
		/// <param name="FileName">冗長引数</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		static public void Begin()
		{
			muphic.DrawManager.drawManager.BeginDevice();
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}*/
	#endregion

	#region Ver.4
	/*
	/// <summary>
	/// DrawManager version2 ハッシュテーブルを2つにして同じテクスチャを重複呼び出ししないようにした。
	/// DrawManager version3 DrawString機能を追加。詳細はDrawTextメソッドを参照。
	/// DrawManager version4 isWindowモードを設定することによりウィンドウモードかフルスクリーンモードかを選択可能
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//ウィンドウモードかどうか(falseでフルスクリーンモード)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//ファイル名とテクスチャを関連付けている
		private Hashtable FileNameTable;						//クラスメイとファイル名の配列を関連付けている
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//この中には、それぞれ描画待ちの文字列たちが、(文字列、x、y、Color)の順に入っている

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			FileNameTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// フォントデータの構造体を作成
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();

			// 構造体に必要なデータをセット
			fd.Height = 24;
			fd.FaceName = "ＭＳ ゴシック";
			try
			{
				// フォントを作成
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// 例外発生
				MessageBox.Show("文字列描画エラー");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)
			if(isWindow)											//ウィンドウモードの場合の設定
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//ウィンドウモードの設定(trueなのでウィンドウモード)
			}
			else
			{														//フルスクリーンモードの場合の設定
				form.Size = new Size(1024, 768);
				pParameters.Windowed = false;							//ウィンドウモードの設定(falseなのでフルスクリーンモード)
				pParameters.EnableAutoDepthStencil = true;				// 深度ステンシルバッファの設定
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// 自動深度ステンシル サーフェイスのフォーマットの設定

				// 使用できるディスプレイモードを検索し、目的のモードを探す
				bool flag = false;

				// ディプレイモードを列挙し、サイズが「1024×768」かつ
				// リフレッシュレートが「60」のモードを探す
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// 条件に見合えば使用する
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// 見つかったことを示すフラグを立てる
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// 目的のモードがなければそのまま終了
					MessageBox.Show("指定したディプレイモードは見つかりませんでした。",
						"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


	#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//頂点描画先設定で失敗していたらこれでなる
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//デバイスタイプで失敗していたらこれでなる
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキーc(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(FileNameTable.Contains(ClassName))
			{
				return;																		//既に登録されていたら終了
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//テクスチャのインスタンス化
				rs[i] = new Rectangle(p, bitmap.Size);										//座標を入れとく
				if(!TextureTable.Contains(FileName[i]))										//テクスチャが既に登録されていなければ
				{
					TextureTable.Add(FileName[i], texture);									//TextureTableに格納
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//座標データの登録
			FileNameTable.Add(ClassName, FileName);											//FileNameTableに格納
		}

		/// <summary>
		/// テクスチャをデリートすることによってメモリの残り残量を増やすメソッド
		/// </summary>
		/// <param name="ClassName">消すクラスの名前</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = (String[])FileNameTable[ClassName];
			FileNameTable.Remove(ClassName);												//削除
			muphic.PointManager.Delete(ClassName);											//対応している座標も削除
			for(int i=0;i<filename.Length;i++)
			{
				if(!FileNameTable.Contains(filename[i]))									//もし他に同じファイルを参照している
				{																			//クラスがなかったら
					TextureTable.Remove(filename[i]);										//該当するテクスチャも削除
				}
			}
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!FileNameTable.ContainsKey(ClassName))						//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			String fname = (String)((String[])FileNameTable[ClassName])[state];//クラス名とstateから該当するテクスチャのファイル名を取得
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは、1倍固定バージョン
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
			//こっちは倍率を変えることができるバージョン
			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width, ra.Height), center, 0, new Point(ra.X, ra.Y), Color.FromArgb(255, 255, 255));

		}

		public void AddText(String str, int x, int y, Color color)
		{
			//文字列はspriteが終ってから描画しないといけないので、spriteが終るまで一時TextListにためておく
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextListからのデータの取り出し
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//文字列の描画
			}
			TextList.Clear();												//ため込んでいた文字列をすべて削除
		}

		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		public void BeginDevice()
		{
			device.Clear(ClearFlags.Target, Color.White, 0, 0);				//画面のクリア
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//スプライトが終了したので、文字列を描画す
		}

		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">冗長引数</param>
		/// <param name="y">冗長引数</param>
		/// <param name="FileName">冗長引数</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		static public void Begin()
		{
			muphic.DrawManager.drawManager.BeginDevice();
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}*/
	#endregion

	#region Ver.5
	/*
	/// <summary>
	/// DrawManager version2 ハッシュテーブルを2つにして同じテクスチャを重複呼び出ししないようにした。
	/// DrawManager version3 DrawString機能を追加。詳細はDrawTextメソッドを参照。
	/// DrawManager version4 isWindowモードを設定することによりウィンドウモードかフルスクリーンモードかを選択可能
	/// DrawManager version5 BeginRegistメソッドを呼ぶと、NowLoadingの画面が表示されるようになる
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//ウィンドウモードかどうか(falseでフルスクリーンモード)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//ファイル名とテクスチャを関連付けている
		private Hashtable FileNameTable;						//クラスメイとファイル名の配列を関連付けている
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//この中には、それぞれ描画待ちの文字列たちが、(文字列、x、y、Color)の順に入っている

		private bool isNowLoading;								//NowLoadingの画面を表示するべきかどうか
		private int NumRegistTextureMax;						//NowLoadingの画面の間に登録すべきテクスチャ数
		private int NumRegistTexture;							//現在読み込んだテクスチャの数

		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			FileNameTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// フォントデータの構造体を作成
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();


			// 構造体に必要なデータをセット
			fd.Height = 25;
			//fd.FaceName = "ＭＳ ゴシック";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// フォントを作成
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// 例外発生
				MessageBox.Show("文字列描画エラー");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)
			if(isWindow)											//ウィンドウモードの場合の設定
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//ウィンドウモードの設定(trueなのでウィンドウモード)
			}
			else
			{														//フルスクリーンモードの場合の設定
				form.Size = new Size(1024, 768);
				pParameters.Windowed = false;							//ウィンドウモードの設定(falseなのでフルスクリーンモード)
				pParameters.EnableAutoDepthStencil = true;				// 深度ステンシルバッファの設定
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// 自動深度ステンシル サーフェイスのフォーマットの設定

				// 使用できるディスプレイモードを検索し、目的のモードを探す
				bool flag = false;

				// ディプレイモードを列挙し、サイズが「1024×768」かつ
				// リフレッシュレートが「60」のモードを探す
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// 条件に見合えば使用する
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// 見つかったことを示すフラグを立てる
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// 目的のモードがなければそのまま終了
					MessageBox.Show("指定したディプレイモードは見つかりませんでした。",
						"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


	#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//頂点描画先設定で失敗していたらこれでなる
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//デバイスタイプで失敗していたらこれでなる
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		/// <summary>
		/// これからテクスチャの登録作業を始めることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、EndRegistTextureを呼び出すまでNowLoading画面が表示される
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				// 画像を分離する必要が無い件について
				DrawTexture("Nowloading_bak", 0, false);	// 背景濃いめ
				//DrawTexture("Nowloading", 0, false);
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}
		}

		/// <summary>
		/// NowLoading画面を描画するメソッド
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("ロード中..." + this.NumRegistTexture, 0, 100);
				return;
			}
			float percent = this.NumRegistTexture / (float)this.NumRegistTextureMax;		//現在の読み込み状況を0～1(パーセントのようなもの)の範囲に直したもの
			Rectangle BarRect = new Rectangle(269, 494, 484, 30);							//これがバーの四角形

			int NowBarWidth = (int)(BarRect.Width * percent);								//現在の読み込み状況をバーの幅に直したもの
			for(int i=0;i<NowBarWidth;i++)
			{
				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			}
		}

		/// <summary>
		/// これでテクスチャの登録作業を終えることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、NowLoading画面の表示が終わって、普通の描画に切り替わる
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキーc(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(FileNameTable.Contains(ClassName))
			{
				return;																		//既に登録されていたら終了
			}
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//テクスチャのインスタンス化
				rs[i] = new Rectangle(p, bitmap.Size);										//座標を入れとく
				if(!TextureTable.Contains(FileName[i]))										//テクスチャが既に登録されていなければ
				{
					TextureTable.Add(FileName[i], texture);									//TextureTableに格納
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//座標データの登録
			FileNameTable.Add(ClassName, FileName);											//FileNameTableに格納

			if(this.isNowLoading)															//もし、NowLoading画面を描画しないといけないなら
			{
				this.NumRegistTexture += FileName.Length;
				if(this.NumRegistTextureMax == 0)
				{
					DrawManager.Begin(true);												//テスト用のNowLoadingの場合は画面をクリアする
				}
				else
				{
					DrawManager.Begin(false);												//本番用のNowLoadingの場合は画面をクリアしない
				}
				this.DrawNowLoading();
				DrawManager.End();
			}
		}

		/// <summary>
		/// テクスチャをデリートすることによってメモリの残り残量を増やすメソッド
		/// </summary>
		/// <param name="ClassName">消すクラスの名前</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = (String[])FileNameTable[ClassName];
			FileNameTable.Remove(ClassName);												//削除
			muphic.PointManager.Delete(ClassName);											//対応している座標も削除
			for(int i=0;i<filename.Length;i++)
			{
				if(!FileNameTable.Contains(filename[i]))									//もし他に同じファイルを参照している
				{																			//クラスがなかったら
					TextureTable.Remove(filename[i]);										//該当するテクスチャも削除
				}
			}
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			if(!FileNameTable.ContainsKey(ClassName))						//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			String fname = (String)((String[])FileNameTable[ClassName])[state];//クラス名とstateから該当するテクスチャのファイル名を取得
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは、1倍固定バージョン
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
			//こっちは倍率を変えることができるバージョン
			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width, ra.Height), center, 0, new Point(ra.X, ra.Y), Color.FromArgb(255, 255, 255));

		}

		public void AddText(String str, int x, int y, Color color)
		{
			//文字列はspriteが終ってから描画しないといけないので、spriteが終るまで一時TextListにためておく
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextListからのデータの取り出し
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//文字列の描画
			}
			TextList.Clear();												//ため込んでいた文字列をすべて削除
		}

		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//画面のクリア
			}
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//スプライトが終了したので、文字列を描画す
		}

		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによって仮NowLoading画面の描画が開始する(テクスチャ読み込みの総数を調べるときに有効)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が開始する
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// テクスチャの登録を終了することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が終了する
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">冗長引数</param>
		/// <param name="y">冗長引数</param>
		/// <param name="FileName">冗長引数</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}
	*/
	#endregion
	
	#region Ver.6
	/*
	/// <summary>
	/// DrawManager version2 ハッシュテーブルを2つにして同じテクスチャを重複呼び出ししないようにした。
	/// DrawManager version3 DrawString機能を追加。詳細はDrawTextメソッドを参照。
	/// DrawManager version4 isWindowモードを設定することによりウィンドウモードかフルスクリーンモードかを選択可能
	/// DrawManager version5 BeginRegistメソッドを呼ぶと、NowLoadingの画面が表示されるようになる
	/// DrawManager version6 FileNameTableの管理をFileNameManagerに任せた(印刷機能実現のため) あと、DeleteTextureの問題発覚(他のテクスチャが使っているかどうか調べることができない)
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//ウィンドウモードかどうか(falseでフルスクリーンモード)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//ファイル名とテクスチャを関連付けている
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//この中には、それぞれ描画待ちの文字列たちが、(文字列、x、y、Color)の順に入っている

		private bool isNowLoading;								//NowLoadingの画面を表示するべきかどうか
		private int NumRegistTextureMax;						//NowLoadingの画面の間に登録すべきテクスチャ数
		private int NumRegistTexture;							//現在読み込んだテクスチャの数

		private static Rectangle BarRect = new Rectangle(268, 494, 482, 30);	// NowLoading バーの四角形
		private int BarWidth;
		
		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// フォントデータの構造体を作成
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
			
			// 構造体に必要なデータをセット
			fd.Height = 24;
			//fd.FaceName = "ＭＳ ゴシック";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// フォントを作成
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// 例外発生
				MessageBox.Show("文字列描画エラー");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)
			if(isWindow)											//ウィンドウモードの場合の設定
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//ウィンドウモードの設定(trueなのでウィンドウモード)
			}
			else
			{														//フルスクリーンモードの場合の設定
				form.Size = new Size(1024, 768);
				pParameters.Windowed = false;							//ウィンドウモードの設定(falseなのでフルスクリーンモード)
				pParameters.EnableAutoDepthStencil = true;				// 深度ステンシルバッファの設定
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// 自動深度ステンシル サーフェイスのフォーマットの設定

				// 使用できるディスプレイモードを検索し、目的のモードを探す
				bool flag = false;

				// ディプレイモードを列挙し、サイズが「1024×768」かつ
				// リフレッシュレートが「60」のモードを探す
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// 条件に見合えば使用する
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// 見つかったことを示すフラグを立てる
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// 目的のモードがなければそのまま終了
					MessageBox.Show("指定したディプレイモードは見つかりませんでした。",
						"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


	#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//頂点描画先設定で失敗していたらこれでなる
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//デバイスタイプで失敗していたらこれでなる
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
	#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		/// <summary>
		/// これからテクスチャの登録作業を始めることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、EndRegistTextureを呼び出すまでNowLoading画面が表示される
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			this.BarWidth = 0;
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				DrawTexture("Nowloading_bak", 0, false);	// 背景濃いめ
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}
		}
		
		/// <summary>
		/// NowLoading画面を描画するメソッド
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("ロード中..." + this.NumRegistTexture, 0, 100);
				return;
			}
			
//			float percent = this.NumRegistTexture / (float)this.NumRegistTextureMax;		//現在の読み込み状況を0～1(パーセントのようなもの)の範囲に直したもの
//			Rectangle BarRect = new Rectangle(268, 494, 482, 30);							//これがバーの四角形
//
//			int NowBarWidth = (int)(BarRect.Width * percent);								//現在の読み込み状況をバーの幅に直したもの
//			for(int i=0;i<NowBarWidth;i++)
//			{
//				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
//			}
			
			int NowBarWidth = (int)(BarRect.Width * (this.NumRegistTexture / (float)this.NumRegistTextureMax));
			
			for(int i=this.BarWidth;i<NowBarWidth;i++)
			{
				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			}
			
			this.BarWidth = NowBarWidth;
		}

		/// <summary>
		/// これでテクスチャの登録作業を終えることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、NowLoading画面の表示が終わって、普通の描画に切り替わる
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキーc(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(muphic.FileNameManager.Regist(ClassName, FileName) == false)					//FileNameManagerにファイル名を登録
			{
				//return;																		//既に登録されていたら終了しない(PrintManagerで
			}																				//FileNameManagerに登録した可能性が残されているため)
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//テクスチャのインスタンス化
				rs[i] = new Rectangle(p, bitmap.Size);										//座標を入れとく
				if(!TextureTable.Contains(FileName[i]))										//テクスチャが既に登録されていなければ
				{
					TextureTable.Add(FileName[i], texture);									//TextureTableに格納
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//座標データの登録

			if(this.isNowLoading)															//もし、NowLoading画面を描画しないといけないなら
			{
				this.NumRegistTexture += FileName.Length;
				if(this.NumRegistTextureMax == 0)
				{
					DrawManager.Begin(true);												//テスト用のNowLoadingの場合は画面をクリアする
				}
				else
				{
					DrawManager.Begin(false);												//本番用のNowLoadingの場合は画面をクリアしない
				}
				this.DrawNowLoading();
				DrawManager.End();
			}
		}

		/// <summary>
		/// テクスチャをデリートすることによってメモリの残り残量を増やすメソッド
		/// </summary>
		/// <param name="ClassName">消すクラスの名前</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = muphic.FileNameManager.GetFileNames(ClassName);
			muphic.FileNameManager.Delete(ClassName);										//削除
			muphic.PointManager.Delete(ClassName);											//対応している座標も削除
			for(int i=0;i<filename.Length;i++)
			{
//				if(!FileNameTable.Contains(filename[i]))									//もし他に同じファイルを参照している
//				{																			//クラスがなかったら
//					TextureTable.Remove(filename[i]);										//該当するテクスチャも削除
//				}										//ここがおかしい
			}
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
			Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは、1倍固定バージョン
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
			//こっちは倍率を変えることができるバージョン
			//Rectangle ra = muphic.PointManager.Get(ClassName, state);
			//sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new Size(ra.Width/2, ra.Height/2), center, 0, location, Color.FromArgb(255, 255, 255));
		}
		
		public void AddText(String str, int x, int y, Color color)
		{
			//文字列はspriteが終ってから描画しないといけないので、spriteが終るまで一時TextListにためておく
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextListからのデータの取り出し
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//文字列の描画
			}
			TextList.Clear();												//ため込んでいた文字列をすべて削除
		}

		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//画面のクリア
			}
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//スプライトが終了したので、文字列を描画す
		}

		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによって仮NowLoading画面の描画が開始する(テクスチャ読み込みの総数を調べるときに有効)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が開始する
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// テクスチャの登録を終了することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が終了する
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">冗長引数</param>
		/// <param name="y">冗長引数</param>
		/// <param name="FileName">冗長引数</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}*/
	#endregion

	#region Ver.7.1
	/*
	/// <summary>
	/// DrawManager version2 ハッシュテーブルを2つにして同じテクスチャを重複呼び出ししないようにした。
	/// DrawManager version3 DrawString機能を追加。詳細はDrawTextメソッドを参照。
	/// DrawManager version4 isWindowモードを設定することによりウィンドウモードかフルスクリーンモードかを選択可能
	/// DrawManager version5 BeginRegistメソッドを呼ぶと、NowLoadingの画面が表示されるようになる
	/// DrawManager version6 FileNameTableの管理をFileNameManagerに任せた(印刷機能実現のため) あと、DeleteTextureの問題発覚(他のテクスチャが使っているかどうか調べることができない)[
	/// DrawManager version7 物語音楽で使うサムネイル機能(拡大・縮小表示機能)追加
	/// DrawManager version7.1 フルスクリーンモードのとき、タイトルバーを消すように直した
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//ウィンドウモードかどうか(falseでフルスクリーンモード)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//ファイル名とテクスチャを関連付けている
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//この中には、それぞれ描画待ちの文字列たちが、(文字列、x、y、Color)の順に入っている

		private bool isNowLoading;								//NowLoadingの画面を表示するべきかどうか
		private int NumRegistTextureMax;						//NowLoadingの画面の間に登録すべきテクスチャ数
		private int NumRegistTexture;							//現在読み込んだテクスチャの数

		private static Rectangle BarRect = new Rectangle(268, 494, 482, 30);	// NowLoading バーの四角形
		private int BarWidth;

		//拡大・縮小関係変数
		Rectangle src;											//仮想ウィンドウの四角形
		Rectangle dest;											//実際にウィンドウに描画する際の四角形
		
		public DrawManager(Form form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// フォントデータの構造体を作成
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
			
			// 構造体に必要なデータをセット
			fd.Height = 24;
			//fd.FaceName = "ＭＳ ゴシック";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// フォントを作成
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// 例外発生
				MessageBox.Show("文字列描画エラー");
				return;
			}
		}

		private void InitDevice(Form form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)
			if(isWindow)											//ウィンドウモードの場合の設定
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//ウィンドウモードの設定(trueなのでウィンドウモード)
			}
			else
			{														//フルスクリーンモードの場合の設定
				form.Size = new Size(1024, 768);
				form.ControlBox = false;							//タイトルバー消去
				form.Text = "";										//タイトルバーを消したい気持ちを汲んでくれないMicrosoft
				pParameters.Windowed = false;							//ウィンドウモードの設定(falseなのでフルスクリーンモード)
				pParameters.EnableAutoDepthStencil = true;				// 深度ステンシルバッファの設定
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// 自動深度ステンシル サーフェイスのフォーマットの設定

				// 使用できるディスプレイモードを検索し、目的のモードを探す
				bool flag = false;

				// ディプレイモードを列挙し、サイズが「1024×768」かつ
				// リフレッシュレートが「60」のモードを探す
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// 条件に見合えば使用する
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// 見つかったことを示すフラグを立てる
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// 目的のモードがなければそのまま終了
					MessageBox.Show("指定したディプレイモードは見つかりませんでした。",
						"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


			#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//頂点描画先設定で失敗していたらこれでなる
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//デバイスタイプで失敗していたらこれでなる
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
			#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		/// <summary>
		/// これからテクスチャの登録作業を始めることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、EndRegistTextureを呼び出すまでNowLoading画面が表示される
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			this.BarWidth = 0;
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				DrawTexture("Nowloading_bak", 0, false);	// 背景濃いめ
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}
		}
		
		/// <summary>
		/// NowLoading画面を描画するメソッド
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("ロード中..." + this.NumRegistTexture, 0, 100);
				return;
			}
			
			//			float percent = this.NumRegistTexture / (float)this.NumRegistTextureMax;		//現在の読み込み状況を0～1(パーセントのようなもの)の範囲に直したもの
			//			Rectangle BarRect = new Rectangle(268, 494, 482, 30);							//これがバーの四角形
			//
			//			int NowBarWidth = (int)(BarRect.Width * percent);								//現在の読み込み状況をバーの幅に直したもの
			//			for(int i=0;i<NowBarWidth;i++)
			//			{
			//				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			//			}
			
			int NowBarWidth = (int)(BarRect.Width * (this.NumRegistTexture / (float)this.NumRegistTextureMax));
			
			for(int i=this.BarWidth;i<NowBarWidth;i++)
			{
				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			}
			
			this.BarWidth = NowBarWidth;
		}

		/// <summary>
		/// これでテクスチャの登録作業を終えることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、NowLoading画面の表示が終わって、普通の描画に切り替わる
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキーc(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(muphic.FileNameManager.Regist(ClassName, FileName) == false)					//FileNameManagerにファイル名を登録
			{
				//return;																		//既に登録されていたら終了しない(PrintManagerで
			}																				//FileNameManagerに登録した可能性が残されているため)
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//テクスチャのインスタンス化
				rs[i] = new Rectangle(p, bitmap.Size);										//座標を入れとく
				if(!TextureTable.Contains(FileName[i]))										//テクスチャが既に登録されていなければ
				{
					TextureTable.Add(FileName[i], texture);									//TextureTableに格納
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//座標データの登録

			if(this.isNowLoading)															//もし、NowLoading画面を描画しないといけないなら
			{
				this.NumRegistTexture += FileName.Length;
				if(this.NumRegistTextureMax == 0)
				{
					DrawManager.Begin(true);												//テスト用のNowLoadingの場合は画面をクリアする
				}
				else
				{
					DrawManager.Begin(false);												//本番用のNowLoadingの場合は画面をクリアしない
				}
				this.DrawNowLoading();
				DrawManager.End();
			}
		}

		/// <summary>
		/// テクスチャをデリートすることによってメモリの残り残量を増やすメソッド
		/// </summary>
		/// <param name="ClassName">消すクラスの名前</param>
		public void DeleteTexture(String ClassName)
		{
//			String[] filename = muphic.FileNameManager.GetFileNames(ClassName);
//			muphic.FileNameManager.Delete(ClassName);										//削除
//			muphic.PointManager.Delete(ClassName);											//対応している座標も削除
//			for(int i=0;i<filename.Length;i++)
//			{
//				if(!FileNameTable.Contains(filename[i]))									//もし他に同じファイルを参照している
//				{																			//クラスがなかったら
//					TextureTable.Remove(filename[i]);										//該当するテクスチャも削除
//				}									//ここがおかしい
//			}
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは、1倍固定バージョン
			sprite.Draw2D(texture, center, 0, location, Color.FromArgb(255, 255, 255));
		}

		///////////////////////////////////////////////////////////////////////
		//拡大・縮小描画関係
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 拡大・縮小の際、仮に用いるディスプレイと、実際に描画する領域を変更する
		/// </summary>
		/// <param name="srcRect">座標を指定する際の描画領域(菊さんで言うWindowの領域)</param>
		/// <param name="destRect">実際に描画する領域(菊さんで言うサムネイル領域)</param>
		public void ChangeWindowSize(Rectangle srcRect, Rectangle destRect)
		{
			this.src = srcRect;
			this.dest = destRect;
		}

		/// <summary>
		/// 拡大・縮小処理を施したテクスチャを描画する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="state"></param>
		/// <param name="isCenter"></param>
		public void DrawDivTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawDivTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 拡大・縮小処理を施したテクスチャを描画する
		/// </summary>
		/// <param name="ClassName">クラスのキー</param>
		/// <param name="location">src側における座標(拡大・縮小処理前座標)</param>
		/// <param name="state">状態</param>
		/// <param name="isCenter">真ん中にするかどうか</param>
		public void DrawDivTexture(String ClassName, Point location, int state, bool isCenter)
		{
			float divX = (float)dest.Width / src.Width;
			float divY = (float)dest.Height / src.Height;
			PointF newLocation = new Point(0, 0);
			newLocation.X = location.X - src.X;
			newLocation.X = newLocation.X * divX;
			newLocation.X = newLocation.X + dest.X;
			newLocation.Y = location.Y - src.Y;
			newLocation.Y = newLocation.Y * divY;
			newLocation.Y = newLocation.Y + dest.Y;
			DrawTexture(ClassName, divX, divY, newLocation, state, isCenter);
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="divX">描画するものの横の倍率</param>
		/// <param name="divY">描画するものの縦の倍率</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		private void DrawTexture(String ClassName, float divX, float divY, PointF location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			PointF center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width * divX / 2;
				center.Y = r.Height * divX / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは倍率を変えることができるバージョン
			Rectangle ra = muphic.PointManager.Get(ClassName, state);
			sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new SizeF(ra.Width*divX, ra.Height*divY), center, 0, location, Color.FromArgb(255, 255, 255));
		}
		
		public void AddText(String str, int x, int y, Color color)
		{
			//文字列はspriteが終ってから描画しないといけないので、spriteが終るまで一時TextListにためておく
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextListからのデータの取り出し
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, color);						//文字列の描画
			}
			TextList.Clear();												//ため込んでいた文字列をすべて削除
		}

		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//画面のクリア
			}
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//スプライトが終了したので、文字列を描画す
		}

		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによって仮NowLoading画面の描画が開始する(テクスチャ読み込みの総数を調べるときに有効)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が開始する
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// テクスチャの登録を終了することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が終了する
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		#region Regist

		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		#endregion

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">冗長引数</param>
		/// <param name="y">冗長引数</param>
		/// <param name="FileName">冗長引数</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		#region Draw

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		#region DrawDiv

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawDiv(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDiv(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawDiv(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDiv(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawDivCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDivCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawDivCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDivCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		/// <summary>
		/// 拡大・縮小の際の倍率を決定するメソッド
		/// </summary>
		/// <param name="srcRect">描画する座標を登録する仮想ウィンドウの四角形(登録四角形)</param>
		/// <param name="destRect">実際にウィンドウに描画する際に描画される四角形(描画四角形)</param>
		static public void Change(Rectangle srcRect, Rectangle destRect)
		{
			muphic.DrawManager.drawManager.ChangeWindowSize(srcRect, destRect);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}
	}
	*/
	#endregion

	#region Ver.8.2
	/*	
	public delegate void FadeEventHandler();
	/// <summary>
	/// DrawManager version2 ハッシュテーブルを2つにして同じテクスチャを重複呼び出ししないようにした。
	/// DrawManager version3 DrawString機能を追加。詳細はDrawTextメソッドを参照。
	/// DrawManager version4 isWindowモードを設定することによりウィンドウモードかフルスクリーンモードかを選択可能
	/// DrawManager version5 BeginRegistメソッドを呼ぶと、NowLoadingの画面が表示されるようになる
	/// DrawManager version6 FileNameTableの管理をFileNameManagerに任せた(印刷機能実現のため) あと、DeleteTextureの問題発覚(他のテクスチャが使っているかどうか調べることができない)[
	/// DrawManager version7 物語音楽で使うサムネイル機能(拡大・縮小表示機能)追加
	/// DrawManager version7.1 フルスクリーンモードのとき、タイトルバーを消すように直した
	/// DrawManager version8 フェードイン、フェードアウト機能を追加
	/// DrawManager version8.1 マウスカーソルがNowLoading中に表示されている点を修正
	/// DrawManager version8.2 縮小描画の際の問題を修正(DrawDivのDraw2Dメソッドが変更されている)
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = true;							//ウィンドウモードかどうか(falseでフルスクリーンモード)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//ファイル名とテクスチャを関連付けている
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//この中には、それぞれ描画待ちの文字列たちが、(文字列、x、y、Color)の順に入っている

		private bool isNowLoading;								//NowLoadingの画面を表示するべきかどうか
		private int NumRegistTextureMax;						//NowLoadingの画面の間に登録すべきテクスチャ数
		private int NumRegistTexture;							//現在読み込んだテクスチャの数

		private static Rectangle BarRect = new Rectangle(268, 494, 482, 30);	// NowLoading バーの四角形
		private int BarWidth;

		//拡大・縮小関係変数
		Rectangle src;											//仮想ウィンドウの四角形
		Rectangle dest;											//実際にウィンドウに描画する際の四角形

		//フェードイン・フェードアウト用変数
		int alpha = 255;										//透過度をあらわす変数(255で普通に描画)
		int fade;												//フェード度をあらわす変数(255で普通に描画)
		Color FadeColor = Color.Black;							//フェードインの始め、フェードアウトの最後の色
		Color FadeColor2 = Color.White;							//フェードインの最後、フェードアウトの始めの色
		Color FilterColor = Color.White;						//現在テクスチャを描画する際に用いるフィルタカラー
		public static event FadeEventHandler FadeInEvent;		//フェードインの際に呼ぶDrawメソッドを登録するイベント
		public static event FadeEventHandler FadeOutEvent;		//フェードアウトの際に呼ぶDrawメソッドを登録するイベント
		Timer FadeTimer;										//フェードイン・フェードアウトの際のフレームカウンター
		int NowFadeFrameCount;									//フェード処理の際の現在のフレーム数
		int FadeFrameCount;										//フェード処理をするフレーム数

		//マウスカーソルの描画関係
		String MouseClassName;
		Point MousePoint;
		int MouseState;
		bool isDrawMouseCursor = true;

		public DrawManager(MainScreen form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// フォントデータの構造体を作成
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
			
			// 構造体に必要なデータをセット
			fd.Height = 24;
			//fd.FaceName = "ＭＳ ゴシック";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// フォントを作成
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// 例外発生
				MessageBox.Show("文字列描画エラー");
				return;
			}

			//FadeTimer = new Timer(new System.ComponentModel.Container());
			//FadeTimer.Interval = 16;
			FadeTimer = form.FadeTimer;
		}

		private void InitDevice(MainScreen form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)
			if(isWindow)											//ウィンドウモードの場合の設定
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//ウィンドウモードの設定(trueなのでウィンドウモード)
			}
			else
			{														//フルスクリーンモードの場合の設定
				form.Size = new Size(1024, 768);
				form.ControlBox = false;							//タイトルバー消去
				form.Text = "";										//タイトルバーを消したい気持ちを汲んでくれないMicrosoft
				pParameters.Windowed = false;							//ウィンドウモードの設定(falseなのでフルスクリーンモード)
				pParameters.EnableAutoDepthStencil = true;				// 深度ステンシルバッファの設定
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// 自動深度ステンシル サーフェイスのフォーマットの設定

				// 使用できるディスプレイモードを検索し、目的のモードを探す
				bool flag = false;

				// ディプレイモードを列挙し、サイズが「1024×768」かつ
				// リフレッシュレートが「60」のモードを探す
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// 条件に見合えば使用する
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// 見つかったことを示すフラグを立てる
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// 目的のモードがなければそのまま終了
					MessageBox.Show("指定したディプレイモードは見つかりませんでした。",
						"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


			#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//頂点描画先設定で失敗していたらこれでなる
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//デバイスタイプで失敗していたらこれでなる
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
			#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		#region テクスチャ登録・NowLoading関係
		///////////////////////////////////////////////////////////////////////
		//テクスチャ登録、削除・NowLoading関係
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// これからテクスチャの登録作業を始めることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、EndRegistTextureを呼び出すまでNowLoading画面が表示される
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			this.BarWidth = 0;
			this.isDrawMouseCursor = false;					//NowLoading中はマウスカーソルを表示しない
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				DrawTexture("Nowloading_bak", 0, false);	// 背景濃いめ
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}
		}
		
		/// <summary>
		/// NowLoading画面を描画するメソッド
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("ロード中..." + this.NumRegistTexture, 0, 100);
				return;
			}
			
			//			float percent = this.NumRegistTexture / (float)this.NumRegistTextureMax;		//現在の読み込み状況を0～1(パーセントのようなもの)の範囲に直したもの
			//			Rectangle BarRect = new Rectangle(268, 494, 482, 30);							//これがバーの四角形
			//
			//			int NowBarWidth = (int)(BarRect.Width * percent);								//現在の読み込み状況をバーの幅に直したもの
			//			for(int i=0;i<NowBarWidth;i++)
			//			{
			//				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			//			}
			
			int NowBarWidth = (int)(BarRect.Width * (this.NumRegistTexture / (float)this.NumRegistTextureMax));
			
			for(int i=this.BarWidth;i<NowBarWidth;i++)
			{
				DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
			}
			
			this.BarWidth = NowBarWidth;
		}

		/// <summary>
		/// これでテクスチャの登録作業を終えることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、NowLoading画面の表示が終わって、普通の描画に切り替わる
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			this.isDrawMouseCursor = true;				//NowLoadingが終わったので、マウスカーソルを描画する
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキーc(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(muphic.FileNameManager.Regist(ClassName, FileName) == false)					//FileNameManagerにファイル名を登録
			{
				//return;																		//既に登録されていたら終了しない(PrintManagerで
			}																				//FileNameManagerに登録した可能性が残されているため)
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//テクスチャのインスタンス化
				rs[i] = new Rectangle(p, bitmap.Size);										//座標を入れとく
				if(!TextureTable.Contains(FileName[i]))										//テクスチャが既に登録されていなければ
				{
					TextureTable.Add(FileName[i], texture);									//TextureTableに格納
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//座標データの登録

			if(this.isNowLoading)															//もし、NowLoading画面を描画しないといけないなら
			{
				this.NumRegistTexture += FileName.Length;
				if(this.NumRegistTextureMax == 0)
				{
					DrawManager.Begin(true);												//テスト用のNowLoadingの場合は画面をクリアする
				}
				else
				{
					DrawManager.Begin(false);												//本番用のNowLoadingの場合は画面をクリアしない
				}
				this.DrawNowLoading();
				DrawManager.End();
			}
		}

		/// <summary>
		/// テクスチャをデリートすることによってメモリの残り残量を増やすメソッド
		/// </summary>
		/// <param name="ClassName">消すクラスの名前</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = muphic.FileNameManager.GetFileNames(ClassName);
			muphic.FileNameManager.Delete(ClassName);										//削除
			muphic.PointManager.Delete(ClassName);											//対応している座標も削除
			for(int i=0;i<filename.Length;i++)
			{
//				if(!FileNameTable.Contains(filename[i]))									//もし他に同じファイルを参照している
//				{																			//クラスがなかったら
//					TextureTable.Remove(filename[i]);										//該当するテクスチャも削除
//				}										//ここがおかしい
			}
		}
		#endregion

		#region テクスチャ描画関係
		///////////////////////////////////////////////////////////////////////
		//テクスチャ描画関係
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは、1倍固定バージョン
			sprite.Draw2D(texture, center, 0, location,  Color.FromArgb(alpha, FilterColor.R, FilterColor.G, FilterColor.B));
		}
		#endregion

		#region 拡大・縮小描画関係
		///////////////////////////////////////////////////////////////////////
		//拡大・縮小描画関係
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 拡大・縮小の際、仮に用いるディスプレイと、実際に描画する領域を変更する
		/// </summary>
		/// <param name="srcRect">座標を指定する際の描画領域(菊さんで言うWindowの領域)</param>
		/// <param name="destRect">実際に描画する領域(菊さんで言うサムネイル領域)</param>
		public void ChangeWindowSize(Rectangle srcRect, Rectangle destRect)
		{
			this.src = srcRect;
			this.dest = destRect;
		}

		/// <summary>
		/// 拡大・縮小処理を施したテクスチャを描画する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="state"></param>
		/// <param name="isCenter"></param>
		public void DrawDivTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawDivTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 拡大・縮小処理を施したテクスチャを描画する
		/// </summary>
		/// <param name="ClassName">クラスのキー</param>
		/// <param name="location">src側における座標(拡大・縮小処理前座標)</param>
		/// <param name="state">状態</param>
		/// <param name="isCenter">真ん中にするかどうか</param>
		public void DrawDivTexture(String ClassName, Point location, int state, bool isCenter)
		{
			float divX = (float)dest.Width / src.Width;
			float divY = (float)dest.Height / src.Height;
			PointF newLocation = new Point(0, 0);
			newLocation.X = location.X - src.X;
			newLocation.X = newLocation.X * divX;
			newLocation.X = newLocation.X + dest.X;
			newLocation.Y = location.Y - src.Y;
			newLocation.Y = newLocation.Y * divY;
			newLocation.Y = newLocation.Y + dest.Y;
			DrawTexture(ClassName, divX, divY, newLocation, state, isCenter);
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="divX">描画するものの横の倍率</param>
		/// <param name="divY">描画するものの縦の倍率</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		private void DrawTexture(String ClassName, float divX, float divY, PointF location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			PointF center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width * divX / 2;
				center.Y = r.Height * divY / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは倍率を変えることができるバージョン
			Rectangle ra = muphic.PointManager.Get(ClassName, state);
			sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new SizeF(ra.Width*divX, ra.Height*divY), PointF.Empty, 0, new PointF(location.X-center.X, location.Y-center.Y),  Color.FromArgb(alpha, FilterColor));
		}
		#endregion

		#region 文字列描画関係
		///////////////////////////////////////////////////////////////////////
		//文字列描画関係
		///////////////////////////////////////////////////////////////////////
		public void AddText(String str, int x, int y, Color color)
		{
			//文字列はspriteが終ってから描画しないといけないので、spriteが終るまで一時TextListにためておく
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextListからのデータの取り出し
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, Color.FromArgb(alpha, color));						//文字列の描画
			}
			TextList.Clear();												//ため込んでいた文字列をすべて削除
		}
		#endregion

		#region フェードイン・フェードアウト関係
		///////////////////////////////////////////////////////////////////////
		//フェードイン・フェードアウト関係
		///////////////////////////////////////////////////////////////////////
		public void FadeIn(int FrameCount)
		{
			FadeFrameCount = FrameCount;
			NowFadeFrameCount = 0;
			
			FadeTimer.Tick += new EventHandler(FadeInRender);
			FadeTimer.Enabled = true;
		}

		private void FadeInRender(object sender, System.EventArgs e)
		{																	//現在のフェードの色の決定
			float PerOnce = (float)NowFadeFrameCount / FadeFrameCount;		//現在のフレームカウント終了の割合(0～1)
			int r = (int)((float)FadeColor2.R * PerOnce + (float)FadeColor.R * (1-PerOnce));
			int g = (int)((float)FadeColor2.G * PerOnce + (float)FadeColor.G * (1-PerOnce));
			int b = (int)((float)FadeColor2.B * PerOnce + (float)FadeColor.B * (1-PerOnce));
			this.FilterColor = Color.FromArgb(r, g, b);						//色の決定
				
			DrawManager.Begin(false);
			muphic.DrawManager.FadeInEvent();								//元画面の描画
			DrawManager.End();

			NowFadeFrameCount++;											//フレームカウントをインクリメント
			
			if(NowFadeFrameCount >= FadeFrameCount)							//フレームカウントが終了したら
			{
				FadeTimer.Enabled = false;
				FilterColor = Color.White;
				FadeTimer.Tick -= new EventHandler(FadeInRender);
			}
		}

		public void FadeOut(int FrameCount)
		{
			FadeFrameCount = FrameCount;
			NowFadeFrameCount = 0;
			//this.FilterColor = Color.White;
			
			FadeTimer.Tick += new EventHandler(FadeOutRender);
			FadeTimer.Enabled = true;
			//while(FadeTimer.Enabled);										//描画が終わるまで待機
		}

		private void FadeOutRender(object sender, System.EventArgs e)
		{																	//現在のフェードの色の決定
			float PerOnce = (float)NowFadeFrameCount / FadeFrameCount;		//現在のフレームカウント終了の割合(0～1)
			int r = (int)((float)FadeColor.R * PerOnce + (float)FadeColor2.R * (1-PerOnce));
			int g = (int)((float)FadeColor.G * PerOnce + (float)FadeColor2.G * (1-PerOnce));
			int b = (int)((float)FadeColor.B * PerOnce + (float)FadeColor2.B * (1-PerOnce));
			this.FilterColor = Color.FromArgb(r, g, b);						//色の決定
				
			DrawManager.Begin(false);
			muphic.DrawManager.FadeOutEvent();								//元画面の描画
			DrawManager.End();

			NowFadeFrameCount++;											//フレームカウントをインクリメント
			
			if(NowFadeFrameCount >= FadeFrameCount)							//フレームカウントが終了したら
			{
				FadeTimer.Enabled = false;
				//FilterColor = Color.White;
				FadeTimer.Tick -= new EventHandler(FadeOutRender);
			}
		}

		#endregion

		#region フェードイベント送信用メソッド
		///////////////////////////////////////////////////////////////////////
		//フェードイベント送信用メソッド
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// フェードインの際描画するDrawメソッドを呼び出す
		/// </summary>
		/// <param name="e"></param>
		private void OnFadeIn()
		{
			if(FadeInEvent != null) FadeInEvent();
		}

		/// <summary>
		/// フェードアウトの際描画するDrawメソッドを呼び出す
		/// </summary>
		private void OnFadeOut()
		{
			if(FadeOutEvent != null) FadeOutEvent();
		}

		#endregion

		#region デバイス関係
		///////////////////////////////////////////////////////////////////////
		//デバイス関係
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//画面のクリア
			}
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//スプライトが終了したので、文字列を描画す
			sprite.Begin(SpriteFlags.AlphaBlend);
			if(isDrawMouseCursor)
			{
				DrawManager.Draw(MouseClassName, MousePoint.X, MousePoint.Y, MouseState);
			}
			sprite.End();
		}

		#endregion

		#region 外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによって仮NowLoading画面の描画が開始する(テクスチャ読み込みの総数を調べるときに有効)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が開始する
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// テクスチャの登録を終了することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が終了する
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		#region Regist

		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		#endregion

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">冗長引数</param>
		/// <param name="y">冗長引数</param>
		/// <param name="FileName">冗長引数</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		#region Draw

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		#region DrawDiv

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawDiv(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDiv(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawDiv(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDiv(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawDivCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDivCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawDivCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDivCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		/// <summary>
		/// 拡大・縮小の際の倍率を決定するメソッド
		/// </summary>
		/// <param name="srcRect">描画する座標を登録する仮想ウィンドウの四角形(登録四角形)</param>
		/// <param name="destRect">実際にウィンドウに描画する際に描画される四角形(描画四角形)</param>
		static public void Change(Rectangle srcRect, Rectangle destRect)
		{
			muphic.DrawManager.drawManager.ChangeWindowSize(srcRect, destRect);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}

		/// <summary>
		/// フェードインを始めるメソッド(1秒間)
		/// </summary>
		static public void StartFadeIn()
		{
			muphic.DrawManager.drawManager.FadeIn(60);
		}

		/// <summary>
		/// フェードインとはじめるメソッド(フレーム数で指定)
		/// </summary>
		/// <param name="FrameCount">フェードインをするフレーム数</param>
		static public void StartFadeIn(int FrameCount)
		{
			muphic.DrawManager.drawManager.FadeIn(FrameCount);
		}

		/// <summary>
		/// フェードアウトを始めるメソッド(1秒間)
		/// </summary>
		static public void StartFadeOut()
		{
			muphic.DrawManager.drawManager.FadeOut(60);
		}

		/// <summary>
		/// フェードアウトを始めるメソッド(フレーム数で指定)
		/// </summary>
		/// <param name="FrameCount">フェードアウトをするフレーム数</param>
		static public void StartFadeOut(int FrameCount)
		{
			muphic.DrawManager.drawManager.FadeOut(FrameCount);
		}

		/// <summary>
		/// カーソルのデータを設定するメソッド
		/// </summary>
		/// <param name="ClassName">カーソルの登録名</param>
		/// <param name="p">マウスの座標</param>
		/// <param name="state">マウスの状態</param>
		static public void SetCursor(String ClassName, Point p, int state)
		{
			muphic.DrawManager.drawManager.MousePoint = p;
			muphic.DrawManager.drawManager.MouseState = state;
			muphic.DrawManager.drawManager.MouseClassName = ClassName;
		}

		#endregion
	}*/
	#endregion
	
	#region Ver.9
		
	public delegate void FadeEventHandler();
	/// <summary>
	/// DrawManager version2 ハッシュテーブルを2つにして同じテクスチャを重複呼び出ししないようにした。
	/// DrawManager version3 DrawString機能を追加。詳細はDrawTextメソッドを参照。
	/// DrawManager version4 isWindowモードを設定することによりウィンドウモードかフルスクリーンモードかを選択可能
	/// DrawManager version5 BeginRegistメソッドを呼ぶと、NowLoadingの画面が表示されるようになる
	/// DrawManager version6 FileNameTableの管理をFileNameManagerに任せた(印刷機能実現のため) あと、DeleteTextureの問題発覚(他のテクスチャが使っているかどうか調べることができない)[
	/// DrawManager version7 物語音楽で使うサムネイル機能(拡大・縮小表示機能)追加
	/// DrawManager version7.1 フルスクリーンモードのとき、タイトルバーを消すように直した
	/// DrawManager version8 フェードイン、フェードアウト機能を追加
	/// DrawManager version8.1 マウスカーソルがNowLoading中に表示されている点を修正
	/// DrawManager version8.2 縮小描画の際の問題を修正(DrawDivのDraw2Dメソッドが変更されている)
	/// DrawManager version9 NowLoadingの高速化
	/// DrawManager version9.1 フェードイン・フェードアウトについて、終了したらメソッドから出る仕様に変更
	/// </summary>
	public class DrawManager
	{
		private bool isWindow = muphic.Common.CommonSettings.getIsWindow();		//ウィンドウモードかどうか(falseでフルスクリーンモード)
		private static DrawManager drawManager;
		private Hashtable TextureTable;							//ファイル名とテクスチャを関連付けている
		private Device device;
		private Sprite sprite = null;
		private Microsoft.DirectX.Direct3D.Font font;
		private ArrayList TextList;								//この中には、それぞれ描画待ちの文字列たちが、(文字列、x、y、Color)の順に入っている

		//NowLoading用変数
		private bool isNowLoading;								//NowLoadingの画面を表示するべきかどうか
		private int NumRegistTextureMax;						//NowLoadingの画面の間に登録すべきテクスチャ数
		private int NumRegistTexture;							//現在読み込んだテクスチャの数
		private int NowDrawingPercent;							//現在画面に描画されている割合(%)
		private int DrawingPercentOnce=5;						//1度に画面に描画する割合

		private static Rectangle BarRect = new Rectangle(268, 494, 482, 30);	// NowLoading バーの四角形
		private int BarWidth;

		private bool flagNowLoading;

		//拡大・縮小関係変数
		Rectangle src;											//仮想ウィンドウの四角形
		Rectangle dest;											//実際にウィンドウに描画する際の四角形

		//フェードイン・フェードアウト用変数
		int alpha = 255;										//透過度をあらわす変数(255で普通に描画)
		int fade;												//フェード度をあらわす変数(255で普通に描画)
		Color FadeColor = Color.Black;							//フェードインの始め、フェードアウトの最後の色
		Color FadeColor2 = Color.White;							//フェードインの最後、フェードアウトの始めの色
		Color FilterColor = Color.White;						//現在テクスチャを描画する際に用いるフィルタカラー
		public static event FadeEventHandler FadeInEvent;		//フェードインの際に呼ぶDrawメソッドを登録するイベント
		public static event FadeEventHandler FadeOutEvent;		//フェードアウトの際に呼ぶDrawメソッドを登録するイベント
		Timer FadeTimer;										//フェードイン・フェードアウトの際のフレームカウンター
		int NowFadeFrameCount;									//フェード処理の際の現在のフレーム数
		int FadeFrameCount;										//フェード処理をするフレーム数

		//マウスカーソルの描画関係
		String MouseClassName;
		Point MousePoint;
		int MouseState;
		bool isDrawMouseCursor = true;

		public DrawManager(MainScreen form)
		{
			InitDevice(form);
			TextureTable = new Hashtable();
			TextList = new ArrayList();
			muphic.DrawManager.drawManager = this;

			// フォントデータの構造体を作成
			Microsoft.DirectX.Direct3D.FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
			
			// 構造体に必要なデータをセット
			fd.Height = 24;
			//fd.FaceName = "ＭＳ ゴシック";
			fd.FaceName = "MeiryoKe_Gothic";
			fd.Quality = FontQuality.ClearTypeNatural;
			try
			{
				// フォントを作成
				font = new Microsoft.DirectX.Direct3D.Font(device, fd);
			}   
			catch (Exception e)
			{
				// 例外発生
				MessageBox.Show("文字列描画エラー");
				return;
			}

			//FadeTimer = new Timer(new System.ComponentModel.Container());
			//FadeTimer.Interval = 16;
			FadeTimer = form.FadeTimer;
		}

		private void InitDevice(MainScreen form)
		{
			PresentParameters pParameters = new PresentParameters();	//パラメータ設定クラスのインスタンス化
			pParameters.SwapEffect = SwapEffect.Discard;				//スワップの設定(Discard:最も効率的な方法をディスプレイ側で決定する)
			if(isWindow)											//ウィンドウモードの場合の設定
			{
				form.ClientSize = new Size(1024, 768);
				pParameters.Windowed = true;							//ウィンドウモードの設定(trueなのでウィンドウモード)
			}
			else
			{														//フルスクリーンモードの場合の設定
				form.Size = new Size(1024, 768);
				form.ControlBox = false;							//タイトルバー消去
				form.Text = "";										//タイトルバーを消したい気持ちを汲んでくれないMicrosoft
				pParameters.Windowed = false;							//ウィンドウモードの設定(falseなのでフルスクリーンモード)
				pParameters.EnableAutoDepthStencil = true;				// 深度ステンシルバッファの設定
				pParameters.AutoDepthStencilFormat = DepthFormat.D16;	// 自動深度ステンシル サーフェイスのフォーマットの設定

				// 使用できるディスプレイモードを検索し、目的のモードを探す
				bool flag = false;

				// ディプレイモードを列挙し、サイズが「1024×768」かつ
				// リフレッシュレートが「60」のモードを探す
				foreach (DisplayMode i in Manager.Adapters[0].SupportedDisplayModes)
				{
					if (i.Width == 1024 && i.Height == 768 && i.RefreshRate == 60)
					{
						// 条件に見合えば使用する
						pParameters.BackBufferWidth = 1024;
						pParameters.BackBufferHeight = 768;
						pParameters.BackBufferFormat = i.Format;
						pParameters.FullScreenRefreshRateInHz = 60;
						// 見つかったことを示すフラグを立てる
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					// 目的のモードがなければそのまま終了
					MessageBox.Show("指定したディプレイモードは見つかりませんでした。",
						"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}


			#region デバイス生成作業
			//デバイス生成
			try
			{
				device = new Device(0, DeviceType.Hardware, form, CreateFlags.HardwareVertexProcessing, pParameters);
			}
			catch(Exception e)
			{	
				try
				{
					//頂点描画先設定で失敗していたらこれでなる
					device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, pParameters);
				}
				catch(Exception f)
				{
					try
					{
						//デバイスタイプで失敗していたらこれでなる
						device = new Device(0, DeviceType.Reference, form, CreateFlags.HardwareVertexProcessing, pParameters);
					}
					catch(Exception g)
					{
						try
						{
							//両方失敗していたらこれでなる
							device = new Device(0, DeviceType.Reference, form, CreateFlags.SoftwareVertexProcessing, pParameters);
						}
						catch(Exception h)
						{
							//それでもだめだったらどうしようもない
							MessageBox.Show("無理");
							Application.Exit();
						}
					}
				}
			}
			#endregion

			sprite = new Sprite(device);								//Spriteオブジェクトのインスタンス化
		}

		#region テクスチャ登録・NowLoading関係
		///////////////////////////////////////////////////////////////////////
		//テクスチャ登録、削除・NowLoading関係
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// これからテクスチャの登録作業を始めることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、EndRegistTextureを呼び出すまでNowLoading画面が表示される
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		public void BeginRegistTexture(int MaxRegistTex)
		{
			this.NowDrawingPercent = 0;
			this.NumRegistTexture = 0;
			this.NumRegistTextureMax = MaxRegistTex;
			this.isNowLoading = true;
			this.BarWidth = 0;
			this.isDrawMouseCursor = false;					//NowLoading中はマウスカーソルを表示しない
			
			if(this.NumRegistTextureMax != 0)
			{
				DrawManager.Begin(false);
				DrawTexture("Nowloading_bak", 0, false);	// 背景濃いめ
				DrawTexture("Nowloading_all", 0, false);
				DrawManager.End();
			}

			flagNowLoading = true;
		}
		
		/// <summary>
		/// NowLoading画面を描画するメソッド
		/// </summary>
		public void DrawNowLoading()
		{
			if(this.NumRegistTextureMax == 0)
			{
				DrawString("ロード中..." + this.NumRegistTexture, 0, 100);
				return;
			}
			
			int NowPercent = (int)((float)NumRegistTexture / NumRegistTextureMax * 100);	//現在の読み込み完了(%)
			if(this.NowDrawingPercent + this.DrawingPercentOnce <= NowPercent)				//現在、前に描画したものからもう1区切り分だけ読み込みが終わったら
			{
				int NowBarWidth = (int)((float)BarRect.Width * NowDrawingPercent / 100);	//今まで描画していた幅
				int NewBarWidth = (int)((float)BarRect.Width * (NowDrawingPercent + DrawingPercentOnce) / 100);//今回新しく付け足した後の幅
				//int NewBarWidth = (int)(BarRect.Width * (this.NumRegistTexture / (float)this.NumRegistTextureMax));
				DrawManager.Begin(false);
				for(int i=0;i<NewBarWidth;i++)									//その間でfor文をまわす
				{
					DrawTexture("1px", new Point(BarRect.X+i, BarRect.Y), 0, false);
				}

				if (flagNowLoading)
				{
					DrawTexture("Nowloading_bak", 0, false);
					DrawTexture("Nowloading_all", 0, false);
					flagNowLoading = false;

				}
				DrawManager.End();
				this.NowDrawingPercent += DrawingPercentOnce;
			}	
		}

		/// <summary>
		/// これでテクスチャの登録作業を終えることをDrawManagerに教えるメソッド。
		/// ちなみにこれを呼び出すと、NowLoading画面の表示が終わって、普通の描画に切り替わる
		/// </summary>
		public void EndRegistTexture()
		{
			this.isNowLoading = false;
			this.isDrawMouseCursor = true;				//NowLoadingが終わったので、マウスカーソルを描画する
			System.Console.WriteLine("NumRegistTexture = " + this.NumRegistTexture);
		}

		/// <summary>
		/// テクスチャを実際に登録するクラス、同時に座標と幅・高さの登録も行う
		/// </summary>
		/// <param name="ClassName">ハッシュに登録するキーc(クラス名)</param>
		/// <param name="p">画像を表示する座標(固定画像の場合、動的画像の場合はとりあえず(0,0)でいいと思う)</param>
		/// <param name="FileName">テクスチャにする画像ファイルの名前(複数可)</param>
		public void RegistTexture(String ClassName, Point p, String[] FileName)
		{
			Bitmap bitmap;
			Texture texture;
			Rectangle[] rs = new Rectangle[FileName.Length];

			if(muphic.FileNameManager.Regist(ClassName, FileName) == false)					//FileNameManagerにファイル名を登録
			{
				//return;																		//既に登録されていたら終了しない(PrintManagerで
			}																				//FileNameManagerに登録した可能性が残されているため)
			for(int i=0;i<FileName.Length;i++)
			{
				bitmap = new Bitmap(FileName[i]);											//PNGファイルの読み込み
				texture = Texture.FromBitmap(device, bitmap, Usage.None, Pool.Managed);		//テクスチャのインスタンス化
				rs[i] = new Rectangle(p, bitmap.Size);										//座標を入れとく
				if(!TextureTable.Contains(FileName[i]))										//テクスチャが既に登録されていなければ
				{
					TextureTable.Add(FileName[i], texture);									//TextureTableに格納
				}
			}
			muphic.PointManager.Set(ClassName, rs);											//座標データの登録

			if(this.isNowLoading)															//もし、NowLoading画面を描画しないといけないなら
			{
				this.NumRegistTexture += FileName.Length;
//				if(this.NumRegistTextureMax == 0)
//				{
//					DrawManager.Begin(true);												//テスト用のNowLoadingの場合は画面をクリアする
//				}
//				else
//				{
//					DrawManager.Begin(false);												//本番用のNowLoadingの場合は画面をクリアしない
//				}
				this.DrawNowLoading();
				
//				DrawManager.End();
			}
		}

		/// <summary>
		/// テクスチャをデリートすることによってメモリの残り残量を増やすメソッド
		/// </summary>
		/// <param name="ClassName">消すクラスの名前</param>
		public void DeleteTexture(String ClassName)
		{
			String[] filename = muphic.FileNameManager.GetFileNames(ClassName);
			muphic.FileNameManager.Delete(ClassName);										//削除
			muphic.PointManager.Delete(ClassName);											//対応している座標も削除
			for(int i=0;i<filename.Length;i++)
			{
//				if(!FileNameTable.Contains(filename[i]))									//もし他に同じファイルを参照している
//				{																			//クラスがなかったら
//					TextureTable.Remove(filename[i]);										//該当するテクスチャも削除
//				}										//ここがおかしい
			}
		}
		#endregion

		#region テクスチャ描画関係
		///////////////////////////////////////////////////////////////////////
		//テクスチャ描画関係
		///////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を検索してくれる)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">PointManagerに入っている座標を画像の中央にするか、画像の左上にするか</param>
		public void DrawTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);			//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		public void DrawTexture(String ClassName, Point location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			Point center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width / 2;
				center.Y = r.Height / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは、1倍固定バージョン
			sprite.Draw2D(texture, center, 0, location,  Color.FromArgb(alpha, FilterColor.R, FilterColor.G, FilterColor.B));
		}
		#endregion

		#region 拡大・縮小描画関係
		///////////////////////////////////////////////////////////////////////
		//拡大・縮小描画関係
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 拡大・縮小の際、仮に用いるディスプレイと、実際に描画する領域を変更する
		/// </summary>
		/// <param name="srcRect">座標を指定する際の描画領域(菊さんで言うWindowの領域)</param>
		/// <param name="destRect">実際に描画する領域(菊さんで言うサムネイル領域)</param>
		public void ChangeWindowSize(Rectangle srcRect, Rectangle destRect)
		{
			this.src = srcRect;
			this.dest = destRect;
		}

		/// <summary>
		/// 拡大・縮小処理を施したテクスチャを描画する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="state"></param>
		/// <param name="isCenter"></param>
		public void DrawDivTexture(String ClassName, int state, bool isCenter)
		{
			Rectangle r = muphic.PointManager.Get(ClassName, state);		//ClassNameをキーに座標データを検索
			if(r == Rectangle.Empty)										//クラスが登録されていない場合
			{
				return;														//必殺"何もできない"
			}
			DrawDivTexture(ClassName, r.Location, state, isCenter);			//オーバーロードのもう片方を呼ぶ
		}

		/// <summary>
		/// 拡大・縮小処理を施したテクスチャを描画する
		/// </summary>
		/// <param name="ClassName">クラスのキー</param>
		/// <param name="location">src側における座標(拡大・縮小処理前座標)</param>
		/// <param name="state">状態</param>
		/// <param name="isCenter">真ん中にするかどうか</param>
		public void DrawDivTexture(String ClassName, Point location, int state, bool isCenter)
		{
			float divX = (float)dest.Width / src.Width;
			float divY = (float)dest.Height / src.Height;
			PointF newLocation = new Point(0, 0);
			newLocation.X = location.X - src.X;
			newLocation.X = newLocation.X * divX;
			newLocation.X = newLocation.X + dest.X;
			newLocation.Y = location.Y - src.Y;
			newLocation.Y = newLocation.Y * divY;
			newLocation.Y = newLocation.Y + dest.Y;
			DrawTexture(ClassName, divX, divY, newLocation, state, isCenter);
		}

		/// <summary>
		/// 実際にテクスチャを画面に描画する(自分で座標を指定する)
		/// </summary>
		/// <param name="ClassName">描画するテクスチャのキー</param>
		/// <param name="divX">描画するものの横の倍率</param>
		/// <param name="divY">描画するものの縦の倍率</param>
		/// <param name="location">描画する座標</param>
		/// <param name="state">そのクラスの状態(状態によって描画するテクスチャを変える)</param>
		/// <param name="isCenter">locationの座標を画面の中央にするか、画面の左上にするか</param>
		private void DrawTexture(String ClassName, float divX, float divY, PointF location, int state, bool isCenter)
		{
			String fname = muphic.FileNameManager.GetFileName(ClassName, state);
			if(fname == null)												//ファイル名が登録されていない場合
			{
				return;														//必殺"何もしない"
			}
			Texture texture = (Texture)TextureTable[fname];					//ファイル名からテクスチャを取得
			PointF center = new Point(0, 0);
			if(isCenter)													//真ん中で表示する場合
			{
				Rectangle r = muphic.PointManager.Get(ClassName, state);
				center.X = r.Width * divX / 2;
				center.Y = r.Height * divY / 2;									//これでちょうど画像の真ん中がセンターになる
			}
			//こっちは倍率を変えることができるバージョン
			Rectangle ra = muphic.PointManager.Get(ClassName, state);
			sprite.Draw2D(texture, new Rectangle(0, 0, ra.Width, ra.Height), new SizeF(ra.Width*divX, ra.Height*divY), PointF.Empty, 0, new PointF(location.X-center.X, location.Y-center.Y),  Color.FromArgb(alpha, FilterColor));
		}
		#endregion

		#region 文字列描画関係
		///////////////////////////////////////////////////////////////////////
		//文字列描画関係
		///////////////////////////////////////////////////////////////////////
		public void AddText(String str, int x, int y, Color color)
		{
			//文字列はspriteが終ってから描画しないといけないので、spriteが終るまで一時TextListにためておく
			TextList.Add(str);
			TextList.Add(x);
			TextList.Add(y);
			TextList.Add(color);
		}

		public void DrawText()
		{
			for(int i=0;i<TextList.Count/4;i++)
			{
				String str = (String)TextList[i*4];							//TextListからのデータの取り出し
				int x = (int)TextList[i*4+1];
				int y = (int)TextList[i*4+2];
				Color color = (Color)TextList[i*4+3];

				font.DrawText(null, str, x, y, Color.FromArgb(alpha, color));						//文字列の描画
			}
			TextList.Clear();												//ため込んでいた文字列をすべて削除
		}
		#endregion

		#region フェードイン・フェードアウト関係
		///////////////////////////////////////////////////////////////////////
		//フェードイン・フェードアウト関係
		///////////////////////////////////////////////////////////////////////
		public void FadeIn(int FrameCount)
		{
			FadeFrameCount = FrameCount;
			NowFadeFrameCount = 0;
			
			FadeTimer.Tick += new EventHandler(FadeInRender);
			FadeTimer.Enabled = true;
			while(FadeTimer.Enabled)
			{
				Application.DoEvents();
			}										//描画が終わるまで待機
		}

		private void FadeInRender(object sender, System.EventArgs e)
		{																	//現在のフェードの色の決定
			float PerOnce = (float)NowFadeFrameCount / FadeFrameCount;		//現在のフレームカウント終了の割合(0～1)
			int r = (int)((float)FadeColor2.R * PerOnce + (float)FadeColor.R * (1-PerOnce));
			int g = (int)((float)FadeColor2.G * PerOnce + (float)FadeColor.G * (1-PerOnce));
			int b = (int)((float)FadeColor2.B * PerOnce + (float)FadeColor.B * (1-PerOnce));
			this.FilterColor = Color.FromArgb(r, g, b);						//色の決定
				
			DrawManager.Begin(false);
			muphic.DrawManager.FadeInEvent();								//元画面の描画
			DrawManager.End();

			NowFadeFrameCount++;											//フレームカウントをインクリメント
			
			if(NowFadeFrameCount >= FadeFrameCount)							//フレームカウントが終了したら
			{
				FadeTimer.Enabled = false;
				FilterColor = Color.White;
				FadeTimer.Tick -= new EventHandler(FadeInRender);
			}
		}

		public void FadeOut(int FrameCount)
		{
			FadeFrameCount = FrameCount;
			NowFadeFrameCount = 0;
			//this.FilterColor = Color.White;
			
			FadeTimer.Tick += new EventHandler(FadeOutRender);
			FadeTimer.Enabled = true;
			while(FadeTimer.Enabled)
			{
				Application.DoEvents();
			}										//描画が終わるまで待機
		}

		private void FadeOutRender(object sender, System.EventArgs e)
		{																	//現在のフェードの色の決定
			float PerOnce = (float)NowFadeFrameCount / FadeFrameCount;		//現在のフレームカウント終了の割合(0～1)
			int r = (int)((float)FadeColor.R * PerOnce + (float)FadeColor2.R * (1-PerOnce));
			int g = (int)((float)FadeColor.G * PerOnce + (float)FadeColor2.G * (1-PerOnce));
			int b = (int)((float)FadeColor.B * PerOnce + (float)FadeColor2.B * (1-PerOnce));
			this.FilterColor = Color.FromArgb(r, g, b);						//色の決定
				
			DrawManager.Begin(false);
			muphic.DrawManager.FadeOutEvent();								//元画面の描画
			DrawManager.End();

			NowFadeFrameCount++;											//フレームカウントをインクリメント
			
			if(NowFadeFrameCount >= FadeFrameCount)							//フレームカウントが終了したら
			{
				FadeTimer.Enabled = false;
				//FilterColor = Color.White;
				FadeTimer.Tick -= new EventHandler(FadeOutRender);
			}
		}

		#endregion

		#region フェードイベント送信用メソッド
		///////////////////////////////////////////////////////////////////////
		//フェードイベント送信用メソッド
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// フェードインの際描画するDrawメソッドを呼び出す
		/// </summary>
		/// <param name="e"></param>
		private void OnFadeIn()
		{
			if(FadeInEvent != null) FadeInEvent();
		}

		/// <summary>
		/// フェードアウトの際描画するDrawメソッドを呼び出す
		/// </summary>
		private void OnFadeOut()
		{
			if(FadeOutEvent != null) FadeOutEvent();
		}

		#endregion

		#region デバイス関係
		///////////////////////////////////////////////////////////////////////
		//デバイス関係
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// デバイスの描画開始メソッドを呼ぶだけ
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		public void BeginDevice(bool isClear)
		{
			if(isClear)
			{
				device.Clear(ClearFlags.Target, Color.White, 0, 0);			//画面のクリア
			}
			device.BeginScene();											//描画開始
		}

		/// <summary>
		/// デバイスの描画終了メソッドを呼ぶだけ
		/// </summary>
		public void EndDevice()
		{
			device.EndScene();												//描画終了
			device.Present();												//サーフェイスとバッファと交換する
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		public void BeginSprite()
		{
			sprite.Begin(SpriteFlags.AlphaBlend);
		}

		/// <summary>
		/// スプライト描画を終えるときに呼ぶ必要があるメソッド
		/// </summary>
		public void EndSprite()
		{
			sprite.End();
			DrawText();														//スプライトが終了したので、文字列を描画す
			sprite.Begin(SpriteFlags.AlphaBlend);
			if(isDrawMouseCursor)
			{
				DrawManager.Draw(MouseClassName, MousePoint.X, MousePoint.Y, MouseState);
			}
			sprite.End();
		}

		#endregion

		#region 外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		//外から呼ばれるメソッドとそのオーバーロードたち
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによって仮NowLoading画面の描画が開始する(テクスチャ読み込みの総数を調べるときに有効)
		/// </summary>
		static public void BeginRegist()
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(0);
		}

		/// <summary>
		/// テクスチャの登録を開始することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が開始する
		/// </summary>
		/// <param name="MaxRegistTex"></param>
		static public void BeginRegist(int MaxRegistTex)
		{
			muphic.DrawManager.drawManager.BeginRegistTexture(MaxRegistTex);
		}

		/// <summary>
		/// テクスチャの登録を終了することをDrawManagerに伝えるメソッド。
		/// これを呼ぶことによってNowLoading画面の描画が終了する
		/// </summary>
		static public void EndRegist()
		{
			muphic.DrawManager.drawManager.EndRegistTexture();
		}

		#region Regist

		/// <summary>
		/// テクスチャを登録する(1つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[1] {FileName});
		}

		/// <summary>
		/// テクスチャを登録する(2つだけ)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName1">登録する1つ目の画像ファイル名</param>
		/// <param name="FileName2">登録する2つ目の画像ファイル名</param>
		static public void Regist(String ClassName, int x, int y, String FileName1, String FileName2)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), new String[2] {FileName1, FileName2});
		}

		/// <summary>
		/// テクスチャを登録する(複数)
		/// </summary>
		/// <param name="ClassName">登録するキー(クラス名)</param>
		/// <param name="x">画像を表示するときのx座標の値</param>
		/// <param name="y">画像を表示するときのy座標の値</param>
		/// <param name="FileName">登録する画像ファイル名の配列</param>
		static public void Regist(String ClassName, int x, int y, String[] FileName)
		{
			muphic.DrawManager.drawManager.RegistTexture(ClassName, new Point(x, y), FileName);
		}

		#endregion

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		static public void Delete(String ClassName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		/// <summary>
		/// テクスチャを削除する
		/// </summary>
		/// <param name="ClassName"></param>
		/// <param name="x">冗長引数</param>
		/// <param name="y">冗長引数</param>
		/// <param name="FileName">冗長引数</param>
		static public void Delete(String ClassName, int x, int y, String FileName)
		{
			muphic.DrawManager.drawManager.DeleteTexture(ClassName);
		}

		#region Draw

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void Draw(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void Draw(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void Draw(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		#region DrawDiv

		/// <summary>
		/// テクスチャを描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawDiv(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, false);
		}

		/// <summary>
		/// テクスチャを描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDiv(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, false);
		}

		/// <summary>
		/// テクスチャを描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawDiv(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, false);
		}

		/// <summary>
		/// テクスチャを描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDiv(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, false);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0,座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		static public void DrawDivCenter(String ClassName)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, 0, true);
		}

		/// <summary>
		/// テクスチャを座標を中心として描画する(座標はこちら側で検索)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDivCenter(String ClassName, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, state, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する(state=0)
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		static public void DrawDivCenter(String ClassName, int x, int y)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), 0, true);
		}

		
		/// <summary>
		/// テクスチャを座標を中心として描画する
		/// </summary>
		/// <param name="ClassName">探索するキー</param>
		/// <param name="x">テクスチャを描画するx座標</param>
		/// <param name="y">テクスチャを描画するy座標</param>
		/// <param name="state">現在のクラスの状態(これによって描画する画像が変わる)</param>
		static public void DrawDivCenter(String ClassName, int x, int y, int state)
		{
			muphic.DrawManager.drawManager.DrawDivTexture(ClassName, new Point(x, y), state, true);
		}

		#endregion

		/// <summary>
		/// 拡大・縮小の際の倍率を決定するメソッド
		/// </summary>
		/// <param name="srcRect">描画する座標を登録する仮想ウィンドウの四角形(登録四角形)</param>
		/// <param name="destRect">実際にウィンドウに描画する際に描画される四角形(描画四角形)</param>
		static public void Change(Rectangle srcRect, Rectangle destRect)
		{
			muphic.DrawManager.drawManager.ChangeWindowSize(srcRect, destRect);
		}

		/// <summary>
		/// スプライト描画を始める時に呼ぶ必要があるメソッド
		/// </summary>
		/// <param name="isClear">画面をクリアするかどうか</param>
		static public void Begin(bool isClear)
		{
			muphic.DrawManager.drawManager.BeginDevice(isClear);
			muphic.DrawManager.drawManager.BeginSprite();
		}

		/// <summary>
		/// スプライト描画を終える時に呼ぶ必要があるメソッド
		/// </summary>
		static public void End()
		{
			muphic.DrawManager.drawManager.EndSprite();
			muphic.DrawManager.drawManager.EndDevice();
		}

		/// <summary>
		/// 文字列を描画するメソッド
		/// </summary>
		/// <param name="str"></param>
		static public void DrawString(String str, int x, int y)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, Color.Black);
		}

		static public void DrawString(String str, int x, int y, System.Drawing.Color color)
		{
			muphic.DrawManager.drawManager.AddText(str, x, y, color);
		}

		/// <summary>
		/// フェードインを始めるメソッド(1秒間)
		/// </summary>
		static public void StartFadeIn()
		{
			muphic.DrawManager.drawManager.FadeIn(60);
		}

		/// <summary>
		/// フェードインとはじめるメソッド(フレーム数で指定)
		/// </summary>
		/// <param name="FrameCount">フェードインをするフレーム数</param>
		static public void StartFadeIn(int FrameCount)
		{
			muphic.DrawManager.drawManager.FadeIn(FrameCount);
		}

		/// <summary>
		/// フェードアウトを始めるメソッド(1秒間)
		/// </summary>
		static public void StartFadeOut()
		{
			muphic.DrawManager.drawManager.FadeOut(60);
		}

		/// <summary>
		/// フェードアウトを始めるメソッド(フレーム数で指定)
		/// </summary>
		/// <param name="FrameCount">フェードアウトをするフレーム数</param>
		static public void StartFadeOut(int FrameCount)
		{
			muphic.DrawManager.drawManager.FadeOut(FrameCount);
		}

		/// <summary>
		/// カーソルのデータを設定するメソッド
		/// </summary>
		/// <param name="ClassName">カーソルの登録名</param>
		/// <param name="p">マウスの座標</param>
		/// <param name="state">マウスの状態</param>
		static public void SetCursor(String ClassName, Point p, int state)
		{
			muphic.DrawManager.drawManager.MousePoint = p;
			muphic.DrawManager.drawManager.MouseState = state;
			muphic.DrawManager.drawManager.MouseClassName = ClassName;
		}

		#endregion
	}
	#endregion
}
