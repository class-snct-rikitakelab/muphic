using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace muphic
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class MainScreen : System.Windows.Forms.Form
	{
		public int FrameCount;											//今何フレーム目かをカウントする(0～59)
		private TopScreen top;
		public System.Windows.Forms.Timer FrameCounter;
		private System.ComponentModel.IContainer components;
		public bool isClicked = false;									//今ドラッグ中かどうかを判定する
		private Point beginPoint;
		private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;										//ドラッグ開始時の座標
		public Point nowPoint;

		Point pMouseMove;
		public muphic.Common.FadeTimer FadeTimer;
		int CursorState = 0;
		
		static public MainScreen form;

		public MainScreen()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			FileNameManager fm = new FileNameManager();					//ファイル名管理クラスのインスタンス化
			DrawManager dm = new DrawManager(this);						//描画管理クラスのインスタンス化
			SoundManager sm = new SoundManager(this);					//SE管理クラスのインスタンス化
			VoiceManager vm = new VoiceManager(axWindowsMediaPlayer1);	//音声管理クラスのインスタンス化
			PointManager pm = new PointManager();						//座標管理クラスのインスタンス化
			PrintManager prm = new PrintManager();						//印刷管理クラスのインスタンス化
			top = new TopScreen(this);									//TopScreenのインスタンス化

			DrawManager.Regist("ArrowR1", 0, 0, "image\\TutorialXGA\\Arrow\\arrow_right1.png");
			DrawManager.Regist("ArrowR2", 0, 0, "image\\TutorialXGA\\Arrow\\arrow_right2.png");
			DrawManager.Regist("ArrowL1", 0, 0, "image\\TutorialXGA\\Arrow\\arrow_left1.png");
			DrawManager.Regist("ArrowL2", 0, 0, "image\\TutorialXGA\\Arrow\\arrow_left2.png");
			strs = new String[] {"ArrowR1", "ArrowR2", "ArrowL1", "ArrowL2"};

			//マウスカーソル
			DrawManager.Regist("MuphicCursor", 0, 0, "image\\top\\cursor_off.png", "image\\top\\cursor_on.png");
			System.Windows.Forms.Cursor.Hide();

			Application.ApplicationExit += new EventHandler(ApplicationExited);
			
			this.FrameCounter.Start();									//フレームカウントのスタート
		}

		String[] strs;
		int now_strs=0;

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
			this.FrameCounter = new System.Windows.Forms.Timer(this.components);
			this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
			((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
			this.SuspendLayout();
			// 
			// FrameCounter
			// 
			this.FrameCounter.Interval = 12;
			this.FrameCounter.Tick += new System.EventHandler(this.Timer1_Tick);
			// 
			// axWindowsMediaPlayer1
			// 
			this.axWindowsMediaPlayer1.Enabled = true;
			this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(1025, 769);
			this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
			this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
			this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(10, 10);
			this.axWindowsMediaPlayer1.TabIndex = 0;
			this.axWindowsMediaPlayer1.Visible = false;
			// 
			// MainScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.axWindowsMediaPlayer1);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainScreen";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "muphic";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainScreen_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// フレームごとに描画するメソッド
		/// </summary>
		private void Render()
		{
			//DrawManager.Change(new Rectangle(0, 0, 1024, 768), new Rectangle(256, 192, 512, 384));
			muphic.DrawManager.Begin(true);									//描画開始を管理側に通知する
			top.Draw();														//実際に描画する
			//muphic.DrawManager.DrawString(pMouseMove.ToString(), 0, 0);
			//DrawManager.Draw(strs[this.now_strs], pMouseMove.X, pMouseMove.Y);
			//muphic.DrawManager.DrawString(this.Location.ToString(), 0, 30);

			//muphic.DrawManager.DrawCursor(nowPoint);
			DrawManager.SetCursor("MuphicCursor", new Point(pMouseMove.X-17, pMouseMove.Y-15), CursorState);//マウスカーソルの描画
			muphic.DrawManager.End();										//描画終了を管理側に通知する
		}

		/// <summary>
		/// FrameCounterが1/60秒をカウントするたびに呼ばれるメソッド
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void FrameCounter_Tick(object sender, System.EventArgs e)
//		{
//			this.Render();
//			FrameCount++;
//			if(FrameCount == 60)
//			{
//				FrameCount = 0;
//			}
//		}
		
		
		/// <summary>
		/// 別クラスから強制的にマウスクリックを行わせるため
		/// </summary>
		/// <param name="point"></param>
		public void OnMouseUpPublic(MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}
		
		/// <summary>
		/// 別クラスから強制的にマウスクリックを行わせるため
		/// </summary>
		/// <param name="e"></param>
		public void OnMouseDownPublic(MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}
		
		
		
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			top.MouseMove(new Point(e.X, e.Y));
			pMouseMove = new Point(e.X, e.Y);
			this.nowPoint = new Point(pMouseMove.X, pMouseMove.Y);
			//muphic.DrawManager.SetCursorPos(nowPoint);
			//ドラッグ関係処理
			if(isClicked == true)
			{
				top.Drag(this.beginPoint, new Point(e.X, e.Y));
			}
		}
		
		
		String a = "asdf";
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			//ドラッグ関係処理
			this.beginPoint = new Point(e.X, e.Y);
			this.isClicked = true;
			top.DragBegin(this.beginPoint);
			CursorState = 1;									//マウスをクリック状態にする
			//DrawManager.FadeOutEvent += new FadeEventHandler(top.Draw);
			//DrawManager.StartFadeOut(30);
			//a = a.Insert(a.Length , "　");
			//System.Diagnostics.Debug.WriteLine(a + "end");
			//muphic.Common.CommonTools.StringCenter("るるる", 15);
			//VoiceManager.Play("oonuma.mp3");
		}

		public MouseEventArgs e;

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			//ドラッグ関係処理
			this.beginPoint = Point.Empty;
			this.isClicked = false;
			top.DragEnd(this.beginPoint, new Point(e.X, e.Y));
			this.e = e;
			top.Click(new Point(e.X, e.Y));
			if(e.Button == MouseButtons.Right)
			{
				this.now_strs++;
				if(now_strs == strs.Length)
				{
					now_strs = 0;
				}
			}
			CursorState = 0;									//マウスをクリックしてない状態にする
			
			//DrawManager.FadeInEvent += new FadeEventHandler(top.Draw);
			//DrawManager.StartFadeIn(30);
			//VoiceManager.Stop();

			this.isShiftKey = false;
		}

		// シフトキーフラグ Client動作時の戻るボタン等で使用
		private bool isShiftKey = false;
		public bool getIsShiftKey() { return this.isShiftKey; }

		// フタエノキワミ、アッー！
		Keys[] Command = {Keys.K, Keys.I, Keys.W, Keys.A, Keys.M, Keys.I};
		bool isKiwami = false;
		int CommandCounter = 0;
		
		protected override void OnKeyDown(KeyEventArgs e)
		{
			System.Console.WriteLine("キー入力: " + e.KeyCode.ToString());

			// シフトキーが押されたら次のクリックまでシフトキーフラグON
			if (e.KeyCode == Keys.ShiftKey) this.isShiftKey = true;

			if (muphic.Common.CommonSettings.getClientMode() && !(e.KeyCode == Keys.ShiftKey))
			{
				// 児童/生徒による操作の場合、キーボードは無効にする
				System.Console.WriteLine("Client動作のため、キーボード操作はブロックされます。");
				return;
			}
			
			base.OnKeyDown (e);
			if(e.Control)
			{
				if(e.KeyCode == Command[CommandCounter])
				{
					System.Diagnostics.Debug.Write("push " + Command[CommandCounter]);
					CommandCounter++;
				}
				else
				{
					CommandCounter = 0;
				}
				if(CommandCounter == 6)
				{
					System.Diagnostics.Debug.Write("ok");
					CommandKiwami();
					isKiwami = true;
					CommandCounter = 0;
				}
			}
			else
			{
				CommandCounter = 0;
			}

			if(e.KeyCode == Keys.Escape)
			{
				Application.Exit();
			}
			/*else if(e.KeyCode == Keys.P)
			{
				muphic.PrintManager.ChangePage(1);
				ArrayList a = ((OneScreen)top.Screen).BaseList;
				for(int i=0;i<a.Count;i++)
				{
					muphic.PrintManager.Regist(a[i].ToString());
				}
				muphic.PrintManager.ChangePage(2);
				for(int i=0;i<top.BaseList.Count;i++)
				{
					muphic.PrintManager.Regist(top.BaseList[i].ToString());
				}
//				muphic.PrintManager.Regist("Bird", 0, 100);
//				muphic.PrintManager.Regist("Cat", 100, 100);
//				muphic.PrintManager.Regist("Dog", 200, 100);
//				muphic.PrintManager.Regist("Pig", 300, 100);
//				muphic.PrintManager.Regist("Rabbit", 400, 100);
//				muphic.PrintManager.Regist("Sheep", 500, 100);
//				muphic.PrintManager.Regist("Voice", 600, 100);
				muphic.PrintManager.Print(true);
			}*/
		}
		
		
		public void setCursorPos(Point p)
		{
			Cursor.Position = p;
		}
		
		public Point getCursorPos()
		{
			return Cursor.Position;
		}

		private void CommandKiwami()
		{
			for(int i=1;i<=8;i++)				//既存音声ファイルのコピー
			{
				SoundManager.Delete("Voice" + i + ".wav");
				System.IO.File.Copy("Voice" + i + ".wav", "CommandData\\Kiwami\\temp" + i + ".wav", true);
			}
			for(int i=1;i<=8;i++)				//フタエノキワミ、アッー！のコピー
			{
				System.IO.File.Copy("CommandData\\Kiwami\\Voice" + i + ".wav", "Voice" + i + ".wav", true);
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed (e);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing (e);
		}

		private void ApplicationExited(object sender, EventArgs e)
		{
			if(isKiwami)
			{
				for(int i=1;i<=8;i++)
				{
					System.IO.File.Copy("CommandData\\Kiwami\\temp" + i + ".wav", "Voice" + i + ".wav", true);
				}
			}
		}
		
//
//		protected override void OnPaint(PaintEventArgs e)
//		{
//			base.OnPaint (e);
//			Graphics g = e.Graphics;
//
//			Bitmap bird = new Bitmap("image\\one\\button\\animal\\bird\\bird.png");
//			Bitmap cat = new Bitmap("image\\one\\button\\animal\\cat\\cat.png");
//			Bitmap dog = new Bitmap("image\\one\\button\\animal\\dog\\dog.png");
//			Bitmap pig = new Bitmap("image\\one\\button\\animal\\pig\\pig.png");
//			Bitmap rabbit = new Bitmap("image\\one\\button\\animal\\rabbit\\rabbit.png");
//			Bitmap sheep = new Bitmap("image\\one\\button\\animal\\sheep\\sheep.png");
//			Bitmap voice = new Bitmap("image\\one\\button\\animal\\voice\\voice.png");
//
//			g.DrawImage(bird, 0, 100);
//			g.DrawImage(cat, 100, 100);
//			g.DrawImage(dog, 200, 100);
//			g.DrawImage(pig, 300, 100);
//			g.DrawImage(rabbit, 400, 100);
//			g.DrawImage(sheep, 500, 100);
//			g.DrawImage(voice, 600, 100);
//			g.DrawImage(rabbit, new Rectangle(700, 100, rabbit.Width, rabbit.Height), 0, 0, rabbit.Width, rabbit.Height, GraphicsUnit.Pixel);
//		}
int i = 0;
		private void Timer1_Tick(object sender, System.EventArgs e)
		{
			muphic.MainScreen.form.FrameCount++;
			if(muphic.MainScreen.form.FrameCount == 60)
			{
				i++;
				//System.Diagnostics.Debug.WriteLine(i);
				muphic.MainScreen.form.FrameCount = 0;
			}
				
			muphic.Common.AutoSave.Count();	// 自動セーブ用カウンタ
			try
			{
				//更新
				muphic.MainScreen.form.Render();
			}
			catch (Microsoft.DirectX.Direct3D.DeviceLostException)
			{
				//System.Console.WriteLine("NULLPO");
				//OnDeviceLostException();
				//muphic.DrawManager.OnDeviceLost();
			}
			//Application.DoEvents();

		}


		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// 設定ファイルのリード
			muphic.Common.CommonSettings.ReadCommonSettings();
			
			//更新
			muphic.MainScreen.form = new MainScreen();
			muphic.MainScreen.form.Show();
			Application.Run();
			muphic.MainScreen.form.FrameCounter.Start();
/*
			//Application.Run(new MainScreen());
			while(muphic.MainScreen.form.Created)
			{

				muphic.MainScreen.form.FrameCount++;
				if(muphic.MainScreen.form.FrameCount == 60)
				{
					muphic.MainScreen.form.FrameCount = 0;
				}
				
				muphic.Common.AutoSave.Count();	// 自動セーブ用カウンタ
				try
				{
					//更新
					muphic.MainScreen.form.Render();
				}
				catch (Microsoft.DirectX.Direct3D.DeviceLostException)
				{
					//System.Console.WriteLine("NULLPO");
					//OnDeviceLostException();
					//muphic.DrawManager.OnDeviceLost();
				}
				Application.DoEvents();

			}*/
		}

		private void MainScreen_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}
	}
}
