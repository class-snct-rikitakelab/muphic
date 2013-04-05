using System;
using System.Collections;
using System.Drawing;
using muphic.ADV.MsgWindowParts;

namespace muphic.ADV
{
	/// <summary>
	/// ADVパート用のメッセージ表示ウィンドウ
	/// ADVパート用とか言っておきながら実は結構チュートリアル仕様
	/// </summary>
	public class MsgWindow : Screen
	{
		public AdventureMain parent;
			
		public MainWindow mainwindow;
		public NextButton nextbutton;
		public EndButton endbutton;
		public VoiceButton voicebutton;
		
		public string[] Text;		// テキスト本文
		public Point TextP;			// テキストの座標
		public int TextX;			// テキストのx座標
		public int TextY;			// テキストのy座標
		
		public string Assistant;	// ウィンドウ左のアシスタント動物
		public Point AssistantP;	// アシスタント動物の座標(今のところ全部同じになるよう調節済み)
		
		public ArrayList NBRegist;	// 遅延描画終了後に次へボタン表示直前で画像をregistする場合のデータ
		
		// 以下テキストの遅延描画用のフィールド
		public bool DelayDraw;		// 遅延描画を実行中かどうかのフラグ
		public int FrameCount;		// フレーム数
		public int LineCount;		// 表示させる行数
		public int CharCount;		// 表示させる文字数
		const int DelayTime = 7;	// 一文字表示するのに必要なフレーム数
		
		public bool nextbuttonvisible;	// 次へボタンを表示させるかどうか
		public bool endbuttonvisible;	// 終了ボタンを表示させるかどうか
		
		public MsgWindow(AdventureMain adv)
		{
			this.parent = adv;
			this.Assistant = "None";
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			mainwindow = new MainWindow(this);
			nextbutton = new NextButton(this);
			endbutton = new EndButton(this);
			voicebutton = new VoiceButton(this);
			Text = new string[3];
			
			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(mainwindow.ToString(), 0, 0, "image\\TutorialXGA\\MsgWindow\\window_alpha.png");
			muphic.DrawManager.Regist(nextbutton.ToString(), 0, 0, "image\\TutorialXGA\\MsgWindow\\next_off.png", "image\\TutorialXGA\\MsgWindow\\next_on.png");
			muphic.DrawManager.Regist(endbutton.ToString(), 0, 0, "image\\TutorialXGA\\MsgWindow\\end_off.png", "image\\TutorialXGA\\MsgWindow\\end_on.png");
			muphic.DrawManager.Regist(voicebutton.ToString(), 0, 0, "image\\TutorialXGA\\MsgWindow\\voice_off.png", "image\\TutorialXGA\\MsgWindow\\voice_on.png");
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(mainwindow);
			BaseList.Add(nextbutton);
			BaseList.Add(endbutton);
			BaseList.Add(voicebutton);
			
			this.ChangeWindowCoordinate(1);
		}
		
		
		/// <summary>
		/// テキスト本文を得るメソッド
		/// </summary>
		/// <param name="text">テキスト本文</param>
		/// <param name="visible">次へボタンを描画するか</param>
		public void getText(string[] text)
		{
			getText(text, true);
			this.Visible = true;
		}
		
		/// <summary>
		/// テキスト本文を得るメソッド
		/// </summary>
		/// <param name="text">テキスト本文</param>
		/// <param name="visible">trueなら遅延描画を開始する</param>
		public void getText(string[] text, bool visible)
		{
			this.Text = text;
			this.trimText();					// 文字列の末尾の空白を削除
			if(visible) this.StartDelayDraw();	// 新たな文字列で遅延描画を開始
		}
		
		
		/// <summary>
		/// 次へボタンを描画するかどうかを得るメソッド
		/// </summary>
		/// <param name="visible">次へボタンを描画するならtrue</param>
		public void getNextButtonVisible(bool visible)
		{
			this.nextbuttonvisible = visible;
		}
		
		
		/// <summary>
		/// テキスト本文の行ごとの文末の空白を削除する
		/// </summary>
		public void trimText()
		{
			for(int i=0; i<this.Text.Length; i++)
			{
				this.Text[i] = this.Text[i].TrimEnd();
			}
		}
		
		
		/// <summary>
		/// メッセージウィンドウの下にクリックを透過させるかどうか
		/// </summary>
		/// <returns>trueなら下の画面のクリック</returns>
		public bool ClickThrough()
		{
			System.Console.WriteLine(this.Visible);
			System.Console.WriteLine(this.nextbutton.Visible);
			System.Console.WriteLine(!(this.Visible & this.nextbutton.Visible));
			return !(this.Visible & this.nextbutton.Visible);
		}
		
		
		/// <summary>
		/// アシスタント動物をセットする
		/// </summary>
		/// <param name="assistant"></param>
		public void setAssistant(string assistant)
		{
			this.Assistant = assistant;
		}
		
		
		/// <summary>
		/// ウィンドウの座標を変更するメソッド
		/// modeフィールドの値によって変化する
		/// </summary>
		/// <param name="mode">モード(1が通常の座標)</param>
		public void ChangeWindowCoordinate(int mode)
		{
			int x=0, y=0;
			
			// モードによって座標を変える
			switch(mode)
			{
				case 0:
					this.Visible = false;
					break;
				case 1:
					// 通常の位置
					x = 45;
					y = 571;
					goto default;
				case 2:
					// 一番上の位置
					x = 45;
					y = 6;
					goto default;
				case 3:
					// 真ん中
					x = 45;
					y = 232;
					// 看板の文字消さないといけない
					muphic.Common.TutorialStatus.setDisableDrawString();
					goto default;
				default:
					this.Visible = true;
					break;
			}
			
			// 登録されている座標を変更する
			PointManager.Set(mainwindow.ToString(),  new Rectangle[] {new Rectangle(x,     y,     937, 157), new Rectangle(x,     y,     937, 157)} );
			PointManager.Set(nextbutton.ToString(),  new Rectangle[] {new Rectangle(x+815, y+91,  101, 54),  new Rectangle(x+815, y+91,  101, 54 )} );
			PointManager.Set(endbutton.ToString(),   new Rectangle[] {new Rectangle(x+815, y+28,  101, 54),  new Rectangle(x+815, y+28,  101, 54 )} );
			PointManager.Set(voicebutton.ToString(), new Rectangle[] {new Rectangle(x+125, y+104, 54,  52),  new Rectangle(x+125, y+104, 54,  52 )} );
			
			// テキストの表示座標も変更する
			this.TextP = new Point(x+170-7+40, y+39-6);
			
			// アシスタント動物の表示座標も変更する
			this.AssistantP = new Point(x-7, y-6);
		}
		
		
		/// <summary>
		/// テキストの遅延描画を開始するメソッド
		/// </summary>
		public void StartDelayDraw()
		{
			// 各フィールドの初期化を行う
			this.LineCount = 1;
			this.CharCount = 0;
			this.FrameCount = 0;
			
			// クリック操作を禁止する
			muphic.Common.TutorialStatus.setEnableClickControl();
			
			// 次へボタンを非表示にする
			this.nextbutton.Visible = false;
			
			// 遅延描画開始
			this.DelayDraw = true;
			
			// ついでにVoice再生
			//((TutorialScreen)this.parent.parent).tutorialmain.PlayVoice();
		}
		
		
		/// <summary>
		/// テキストの遅延描画を終了するメソッド
		/// </summary>
		public void EndDelayDraw()
		{
			// 遅延描画終了
			this.DelayDraw = false;
			
			// 次へボタンを出す前に画像をRegistする設定になっていれば行う
			if(this.NBRegist.Count != 0)
			{
				for(int i=0; i<this.NBRegist.Count/4; i++)
				{
					DrawManager.Regist((string)this.NBRegist[i*4], (int)this.NBRegist[i*4+1], (int)this.NBRegist[i*4+2], (string[])this.NBRegist[i*4+3]);
				}

				// クリアしてしまおう
				this.NBRegist.Clear();
			}
			
			// クリック操作を許可する
			muphic.Common.TutorialStatus.setDisableClickControl();
			
			// 次へボタンが表示される設定になっている場合は表示させる
			if(this.nextbuttonvisible) this.nextbutton.Visible = true;
		}
		
		
		public override void Draw()
		{
			base.Draw ();
			
			// アシスタント動物の描画 "None"だった場合やファイル名テーブルに登録名が無かった場合は飛ばす
			if(this.Assistant != "None" && FileNameManager.GetFileExist(this.Assistant) && this.Visible)
			{
				DrawManager.Draw(this.Assistant, AssistantP.X, AssistantP.Y);
			}
			
			// テキスト本文の描画
			if(this.Visible)
			{
				// テキストの遅延描画を実行中かどうかで処理がかわる
				if(!this.DelayDraw)
				{
					// 遅延描画を実行中でない場合は普通に描画
					
					for(int i=0; i<this.Text.Length; i++)
					{
						DrawManager.DrawString(this.Text[i], this.TextP.X, this.TextP.Y + i*36);
					}
				}
				else
				{
					// 遅延描画を実行中だった場合
					
					int i=0;
					for(; i<this.Text.Length; i++)
					{
						// まず、行カウントを超えた行数は表示しない
						if(this.LineCount <= i) break;
						
						// 文字カウントが行の文字数を超えてなければ、文字カウント以降の文字列を削除
						string temp = this.Text[i];
						if( this.LineCount-1 == i && this.CharCount < this.Text[i].Length )
						{
							temp = temp.Remove(this.CharCount, this.Text[i].Length-this.CharCount);
						}
						
						// 文字列の描画
						DrawManager.DrawString(temp, this.TextP.X, this.TextP.Y + i*36);
					}
					
					this.FrameCount++;
					
					if(this.FrameCount == DelayTime)
					{
						this.CharCount++;
						this.FrameCount = 0;
					}
					
					// 行と文字の最後までカウントが終了したら、遅延描画を終わらせる
					if( (this.LineCount >= this.Text.Length) && (this.CharCount > this.Text[this.Text.Length-1].Length) )
					{
						this.EndDelayDraw();
					}
					
					// 文字カウントが行の文字数に達していたら
					if(this.CharCount > this.Text[i-1].Length)
					{
						// 行カウントを増やし、文字カウントをリセット
						this.LineCount++;
						this.CharCount = 0;
					}
				}
			}
		}
		
		public override void Click(Point p)
		{
			base.Click (p);
		}

		public override void MouseMove(Point p)
		{
			base.MouseMove (p);
		}
		
	}
}
