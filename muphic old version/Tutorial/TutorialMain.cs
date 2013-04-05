using System;
using System.IO;
using System.Text;
using System.Drawing;
using muphic.Common;
using muphic.ADV;
using muphic.Tutorial.TutorialSPParts;

namespace muphic.Tutorial
{
	/// <summary>
	/// チュートリアルのメインクラス ADV用のクラスを継承
	/// </summary>
	public class TutorialMain : AdventureMain
	{
		public TopScreen topscreen;
		public new TutorialScreen parent;
		
		public HintButton hintbutton;
		public CheckButton checkbutton;
		public CompleteButton completebutton;
		
		public TutorialEndDialog edialog;	// 終了確認ダイアログ
		public bool isEndDialog;			// 終了確認ダイアログ表示するか否か
		
		public TutorialStart tutorialstart;	// チャプター毎のトップ画面
		public bool isTutorialStart;		// チャプター毎のトップ画面を表示するか否か
		
		public bool isHintButton;			// ヒントボタンが出ているかどうか
		
		public CursorControl cursorcontrol;
		
		public const string SaveFileName = "TutorialSaveData.sav";
		
		private int Emergency;		// 緊急時用
		
		public TutorialMain(TutorialScreen tscreen, string DirectoryName, bool continueflag) : base((Screen)tscreen, DirectoryName, ADVParentScreen.TutorialScreen)
		{
			this.parent = tscreen;
			this.isEndDialog = false;
			this.isHintButton = false;
			this.Emergency = 0;
			
			// 続きから開始する場合はセーブデータの読み込み
			if(continueflag) this.ADVChapter = TutorialTools.ReadSaveFile(this.DirectoryName + SaveFileDirectory + "\\" + SaveFileName, false) - 1;
			
			// チャプター一覧を得る
			this.Chapters = TutorialTools.getDirectoryNames(DirectoryName + PatternFileDirectory, "TutorialChapter");
			
			// 途中終了用のダイアログ
			edialog = new TutorialEndDialog(this);
			
			// チュートリアル用のトップスクリーン
			topscreen = new TopScreen(this);
			
			// カーソル制御
			cursorcontrol = new CursorControl(this);
			
			// ボタン指示用矢印の登録
			DrawManager.Regist("TutorialSupport_arrow_left" , 0, 0, "image\\TutorialXGA\\Arrow\\arrow_left1.png" , "image\\TutorialXGA\\Arrow\\arrow_left2.png" );
			DrawManager.Regist("TutorialSupport_arrow_right", 0, 0, "image\\TutorialXGA\\Arrow\\arrow_right1.png", "image\\TutorialXGA\\Arrow\\arrow_right2.png");
			
			// クリック指示用枠の登録
			DrawManager.Regist("TutorialSupport_frame_animal"    , 0, 0, "image\\TutorialXGA\\Frame\\frame_animal1.png"    , "image\\TutorialXGA\\Frame\\frame_animal2.png"    );
			DrawManager.Regist("TutorialSupport_frame_link"      , 0, 0, "image\\TutorialXGA\\Frame\\frame_link1.png"      , "image\\TutorialXGA\\Frame\\frame_link2.png"      );
			DrawManager.Regist("TutorialSupport_frame_one"       , 0, 0, "image\\TutorialXGA\\Frame\\frame_one1.png"       , "image\\TutorialXGA\\Frame\\frame_one2.png"       );
			DrawManager.Regist("TutorialSupport_frame_story_back", 0, 0, "image\\TutorialXGA\\Frame\\frame_story_back1.png", "image\\TutorialXGA\\Frame\\frame_story_back2.png");
			DrawManager.Regist("TutorialSupport_frame_story_body", 0, 0, "image\\TutorialXGA\\Frame\\frame_story_body1.png", "image\\TutorialXGA\\Frame\\frame_story_body2.png");
		}
		
		
		/// <summary>
		/// チャプターを次に進めるメソッド
		/// </summary>
		public new void NextChapter()
		{
			// チャプターを一つ進める
			this.ADVChapter++;
			
			// パターンファイル一覧を得る
			this.PatternFiles = TutorialTools.getFileNames(this.Chapters[this.ADVChapter], ".pat");
			
			// ステートをリセットし、最初のステートへ進行
			this.ADVState = -1;
			this.NextState();
		}
		
		
		/// <summary>
		/// 次に進めるメソッド
		/// </summary>
		public new void NextState()
		{
			// ステートがチャプターのパターン数を超えた場合
			if(this.ADVState+1 >= this.PatternFiles.Length)
			{
				if(this.ADVChapter+1 >= this.Chapters.Length)
				{
					// もし全てのチャプターを流し終えたら終了
					this.TutorialEnd(false);
					return;
				}
				else
				{
					// 次のチャプターへ進む
					this.NextChapter();
					return;
				}
			}
			
			// フラグと待機状態を解除し、トリガーリストと許可リストをクリア
			this.initTutorialStatus();
			
			// 継承元ADVクラスのステート進行メソッドを実行
			base.NextState();
			
			// チュートリアル進行のためクリックを許可するパーツをリストに追加
			this.addPermissionList();
			
			// マウスクリック指定がされていた場合は実行
			if( this.pattern.MouseClick.X != -1 ) this.MouseClick(this.pattern.MouseClick);
			
			// チャプター毎のトップ画面を描画する設定になっていた場合はトップ画面をインスタンス化
			if( this.pattern.ChapterTop != "" )
			{
				tutorialstart = new TutorialStart(this.parent, this.pattern.ChapterTop);
				this.isTutorialStart = true;
			}
			else
			{
				this.isTutorialStart = false;
			}
			
			// フェード効果を出す場合はそうする
			if(this.pattern.Fade.X != -1)
			{
				// 回数が0に指定されていたらこちらで決めた回数で行う
				if(this.pattern.Fade.X == 0) this.Fade();
				else this.Fade(this.pattern.Fade.X, this.pattern.Fade.Y);
			}
			
			// 特殊コマンドが設定されていれば発動
			if(this.pattern.SPMode != "") this.SPCommand(this.pattern.SPMode);
			
			// 自動的にmuphicトップ画面に戻るよう設定されていた場合はそうする
			if( pattern.TopScreen ) this.BackTopScreen();
			
			// パターンファイル読み込み後、即ステート進行するよう設定されていた場合
			// (システム関連のみのパターンだった際に使用)
			// このコードは念のためNextStateメソッドの最後に記述する
			if( this.pattern.NextState )
			{
				this.NextState();
				return;
			}
		}
		
		
		public new void StopVoice()
		{
			base.StopVoice();
			
			if(this.isHintButton) this.hintbutton.StopVoice();
		}
		
		
		/// <summary>
		/// チュートリアルの状態の簡易初期化
		/// </summary>
		public void initTutorialStatus()
		{
			TutorialStatus.setDisableNextState();
			TutorialStatus.setDisableNextStateStandBy();
			TutorialStatus.initTriggerPartsList();
			TutorialStatus.initPermissionPartsList();
			TutorialStatus.setDisableClickControl();
		}
		
		/// <summary>
		/// チュートリアル進行のためクリックを許可するパーツを追加する
		/// </summary>
		private void addPermissionList()
		{			
			// 次へボタンは無条件でトリガーパーツとする
			TutorialStatus.addTriggerPartsList("muphic.ADV.MsgWindowParts.NextButton");
			
			// やめるボタンと音声切り替えボタンは許可する
			TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.EndButton");
			TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.VoiceButton");
			
			// 終了確認ダイアログのボタンも許可する
			TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialEndDialogParts.YesButton");
			TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialEndDialogParts.NoButton");
		}
		
		
		/// <summary>
		/// ななななななななんという特殊コマンド
		/// </summary>
		public void SPCommand(string SPMode)
		{
			StoryScreen story;
			
			switch(SPMode)
			{
				case "PT02_Link27_1":
					// つなげて音楽パターンファイル27
					
					// つなげて音楽の問題を自動選択する
					new muphic.Link.Dialog.Select.SelectDialog(1, (LinkScreen)topscreen.Screen);
					break;
					
				case "PT02_Link30":
					// つなげて音楽パターンファイル30
					
					// まず特殊コマンドモードをONにする
					TutorialStatus.setEnableIsSPMode("PT02_Link30");
					
					// ヒントボタンをインスタンス化＆登録等
					hintbutton = new HintButton(this, DirectoryName + ControlFileDirectory + "\\" + TutorialStatus.getisSPMode() );
					DrawManager.Regist(hintbutton.ToString(), 540, 30, "image\\TutorialXGA\\FreedomButtons\\hint_off.png", "image\\TutorialXGA\\FreedomButtons\\hint_on.png");
					BaseList.Add(hintbutton);
					TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialSPParts.HintButton");
					this.isHintButton = true;
					
					// トリガーパーツリストをクリアする（単純なボタン操作によるステート進行は無いため）
					TutorialStatus.initTriggerPartsList();
					
					// 次へボタンを許可リストへ追加
					TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.NextButton");
					
					// 曲名バーを消す
					((LinkScreen)this.topscreen.Screen).titlebar.Visible = false;
					break;
					
				case "PT02_Link30_True":
					// つなげて音楽パターンファイル30 答え合わせで正解だった場合
					
					// メッセージウィンドウに正解のを出す
					this.msgwindow.getText(new string[] {"やった！これで　きょくは　かんせいだ！！"});
					this.msgwindow.ChangeWindowCoordinate(1);
					
					// 自由操作を終了
					TutorialStatus.setDisableIsSPMode();	// まず特殊コマンドモードをOFFにする
					hintbutton.Visible = false;				// ヒントボタンを消す
					this.isHintButton = false;
					
					// クリック許可リスト系をクリアして云々
					this.initTutorialStatus();
					this.addPermissionList();
					
					break;
					
				case "PT02_Link30_False":
					// つなげて音楽パターンファイル30 答え合わせで不正解だった場合
					
					// メッセージウィンドウに不正解のを出す
					this.msgwindow.getText(new string[] {"ざんねん！　もういちどがんばろう！"});
					this.msgwindow.ChangeWindowCoordinate(1);
					
					// 処理これだけでいいんかーいなー？ ぐれんかーいなー？				
					break;
					
				case "PT03_Story28":
					// ものがたり音楽パターンファイル28
					
					// 作曲画面 楽譜のクリア
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					this.ClearStoryScore(story);
					break;
					
				case "PT03_Story30":
					// ものがたり音楽パターンファイル30

					// 特殊コマンドモードをONにする
					TutorialStatus.setEnableIsSPMode("PT03_Story30");
					break;
					
				case "PT03_Story36":
					// ものがたり音楽パターンファイル32
					
					// まず特殊コマンドモードをONにする
					TutorialStatus.setEnableIsSPMode("PT03_Story32");
					
					// がんばる
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					String oldAnimalName = story.score.Animals[0].AnimalName;			//4匹すべて同じ名前なので，どれからとっても関係ねぇ
					this.ClearStoryScore(story);
					
					// こちらで新たに動物を登録
					story.score.Animals.Insert(0,8,oldAnimalName, false);
					story.score.Animals.Insert(4,8,oldAnimalName, false);
					story.score.Animals.Insert(5,8,oldAnimalName, false);
					break;
					
				case "PT03_Story40":
					// ものがたり音楽パターンファイル38
					
					// まず特殊コマンドモードをONにする
					TutorialStatus.setEnableIsSPMode("PT03_Story38");
					
					// がんばる
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					String oldAnimalName2 = story.score.Animals[0].AnimalName;			//3匹すべて同じ名前なので，どれからとっても関係ねぇ
					this.ClearStoryScore(story);
					
					// こちらで新たに動物を登録
					story.score.Animals.Insert(0,8,oldAnimalName2, false);
					story.score.Animals.Insert(4,7,oldAnimalName2, false);
					story.score.Animals.Insert(5,6,oldAnimalName2, false);
					break;
					
				case "PT03_Story46":
					// ものがたり音楽パターンファイル46
					
					// まず特殊コマンドモードをONにする
					TutorialStatus.setEnableIsSPMode("PT03_Story46");
					
					// がんばる
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					this.ClearStoryScore(story);
					
					// 予め用意したものがたりスライドを読み込み、サムネイルに画像を表示させてしまうのである  うわー
					story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					muphic.MakeStory.StoryFileReader reader = new muphic.MakeStory.StoryFileReader(story.parent.PictureStory);
					reader.Read("..\\TutorialData\\TutorialMain\\ControlFiles\\PT03_Story46\\ａｋｕｍｏｎｄａｉ");
					
					break;
					
				case "PT03_Story50":
					// ものがたり音楽パターンファイル50
					
					// テラマンダム
					//story = (StoryScreen)((MakeStoryScreen)topscreen.Screen).Screen;
					//this.ClearStoryScore(story);
					
					
					// まず特殊コマンドモードをONにする
					TutorialStatus.setEnableIsSPMode("PT03_Story50");
					
					// チェックボタンをインスタンス化＆登録等
					checkbutton = new CheckButton(this);
					DrawManager.Regist(checkbutton.ToString(), 540, 30, "image\\TutorialXGA\\FreedomButtons\\check_off.png", "image\\TutorialXGA\\FreedomButtons\\check_on.png");
					BaseList.Add(checkbutton);
					TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialSPParts.CheckButton");
					
					// メッセージウィンドウを非表示
					this.msgwindow.ChangeWindowCoordinate(0);
					
					// トリガーパーツリストをクリアする（単純なボタン操作によるステート進行は無いため）
					TutorialStatus.initTriggerPartsList();
					
					// 次へボタンを許可リストへ追加
					TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.NextButton");
					
					break;
					
				case "PT03_Story50_True":
					// ものがたり音楽パターンファイル50 正しかった場合
					
					// 正解のメッセージ
					//this.msgwindow.getText(new string[] {"まだまだ、かなしそう　じゃないかも。", "どうぶつは　ちゃんとよっつ　つかってね。", "もういちど　がんばってみよう！"});
					
					// 自由操作を終了
					TutorialStatus.setDisableIsSPMode();	// まず特殊コマンドモードをOFFにする
					checkbutton.Visible = false;			// チェックボタンを消す
					
					// クリック許可リスト系をクリアして云々
					this.initTutorialStatus();
					this.addPermissionList();
					
					break;
					
				case "PT03_Story50_False":
					// ものがたり音楽パターンファイル50 正しくなかった場合
					
					break;
					
				case "PT04_One02":
					// ひとりで音楽パターンファイル02
					
					// まず特殊コマンドモードをONにする
					TutorialStatus.setEnableIsSPMode("PT04_One02");
					
					// 完成ボタンのインスタンス化＆登録等
					completebutton = new CompleteButton(this);
					DrawManager.Regist(completebutton.ToString(), 540, 30, "image\\TutorialXGA\\FreedomButtons\\comp_off.png", "image\\TutorialXGA\\FreedomButtons\\comp_on.png");
					BaseList.Add(completebutton);
					TutorialStatus.addPermissionPartsList("muphic.Tutorial.TutorialSPParts.CompleteButton");
					
					// ひとりで音楽画面 タイトル表示関連・ファイル入出力関連は不要なので消す
					((OneScreen)this.topscreen.Screen).titlebutton.Visible = false;
					((OneScreen)this.topscreen.Screen).titleform.Visible = false;
					((OneScreen)this.topscreen.Screen).savebutton.Visible = false;
					((OneScreen)this.topscreen.Screen).selectbutton.Visible = false;
					
					// メッセージウィンドウを非表示
					this.msgwindow.ChangeWindowCoordinate(0);
					
					// トリガーパーツリストをクリアする（単純なボタン操作によるステート進行は無いため）
					TutorialStatus.initTriggerPartsList();
					
					// 次へボタンを許可リストへ追加
					TutorialStatus.addPermissionPartsList("muphic.ADV.MsgWindowParts.NextButton");
					
					break;
					
				case "PT04_One02_True":
					// ひとりで音楽パターンファイル02 正解の場合
					
					// 自由操作を終了
					TutorialStatus.setDisableIsSPMode();	// まず特殊コマンドモードをOFFにする
					completebutton.Visible = false;			// チェックボタンを消す
					
					// クリック許可リスト系をクリアして云々
					this.initTutorialStatus();
					this.addPermissionList();
					
					break;
					
				case "PT04_One02_False":
					// ひとりで音楽パターンファイル02 不正解の場合
					
					break;
					
				default:
					System.Console.WriteLine("こちらで想定していない特殊コマンドを使おうとしたようだ");
					System.Console.WriteLine("ヽ(`Д´)ﾉｼﾗﾈｰﾖ!");
					break;
			}
		}
		
		
		public void ClearStoryScore(StoryScreen story)
		{
			// ものがたり音楽の動物ボタン群を何も選択されていない状態にする
			//story.stories.NowClick = muphic.Story.StoryButtonsClickMode.None;
			story.stories.AllClear();
					
			// スクロールバーの位置を初期位置に戻す
			story.score.ChangeScroll(0);			
			
			// 動物を全て削除
			story.score.Animals.AllDelete();
		}
		
		
		/// <summary>
		/// MainScreenのクリックメソッドを呼んでくれる
		/// </summary>
		/// <param name="point"></param>
		public void MouseClick(Point point)
		{
			System.Windows.Forms.MouseEventArgs e = new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.None, 1, point.X, point.Y, 0);
			this.parent.parent.parent.OnMouseDownPublic(e);
			this.parent.parent.parent.OnMouseUpPublic(e);
		}
		
		
		/// <summary>
		/// フェード効果のメソッド
		/// こちらはチュートリアル用に数値を予め設定してみた
		/// </summary>
		public void Fade()
		{
			this.Fade(4, 6);
		}
		
		/// <summary>
		/// フェード効果のメソッド
		/// こちらは引数で回数と時間を決められる
		/// </summary>
		/// <param name="num">フェード効果の回数（→暗→明 の回数）</param>
		/// <param name="time">明→暗の切り替えにかけるフレーム数</param>
		public void Fade(int num, int time)
		{
			//return;
			
			for(int i=0; i<num; i++)
			{
				DrawManager.StartFadeOut(time);
				DrawManager.StartFadeIn(time);
			}
		}
		
		
		/// <summary>
		/// 自動的にトップ画面に戻すメソッド
		/// </summary>
		public void BackTopScreen()
		{
			switch(this.topscreen.ScreenMode)
			{
				case muphic.ScreenMode.OneScreen:
					// 戻るボタンを押したことにする
					((OneScreen)this.topscreen.Screen).back.Click(System.Drawing.Point.Empty);
					break;
				case muphic.ScreenMode.LinkScreen:
					// 答え合わせの結果ダイアログの戻るボタンを押し、つなげて音楽回答画面の戻るボタンを押したことにする
					((Link.Dialog.Answer.AnswerDialog)((LinkScreen)this.topscreen.Screen).Screen).back.Click(System.Drawing.Point.Empty);
					((LinkScreen)this.topscreen.Screen).back.Click(System.Drawing.Point.Empty);
					break;
				case muphic.ScreenMode.StoryScreen:
					((StoryScreen)((MakeStoryScreen)this.topscreen.Screen).Screen).back.Click(Point.Empty);
					((MakeStoryScreen)this.topscreen.Screen).bb.Click(Point.Empty);
					break;
				default:
					break;
			}
		}
		
		
		/// <summary>
		/// 緊急時に一つ前のステートに戻る
		/// なんということでしょう
		/// </summary>
		public void EmergencyCode()
		{
			System.Console.WriteLine("EmergencyCode発動。1つ前のステートに戻る");
			this.ADVState -= 2;
			this.Emergency = 0;
			this.NextState();
		}
		
		
		/// <summary>
		/// 全パターンを実行し終わったら呼び出す
		/// チュートリアルを終了
		/// 引数無しで呼び出した場合は自動的にセーブされる
		/// </summary>
		public void TutorialEnd()
		{
			this.TutorialEnd(true);
		}
		/// <summary>
		/// 全パターンを実行し終わったら呼び出す
		/// チュートリアルを終了
		/// </summary>
		/// <param name="save">セーブを行うかどうか</param>
		public void TutorialEnd(bool save)
		{
			if(save) this.TutorialSave();
			base.AdventureEnd();
		}
		
		
		/// <summary>
		/// チュートリアルのセーブ
		/// </summary>
		public void TutorialSave()
		{
			TutorialTools.WriteSaveFile(this.DirectoryName + SaveFileDirectory + "\\" + SaveFileName, this.ADVChapter, this.Chapters[this.ADVChapter], this.Chapters.Length);
		}
		
		
		
		public new void Draw()
		{
			topscreen.Draw();		// チュートリアル用画面の描画
			base.Draw();
			
			if(this.isEndDialog)
			{
				this.edialog.Draw();
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.Draw();
			}
		}

		public new void Click(System.Drawing.Point p)
		{
			if(this.isEndDialog)
			{
				// 終了確認ダイアログが出ている場合はそちらのクリック処理
				this.edialog.Click(p);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.Click(p);
			}
			else
			{
				// メッセージウィンドウの下の画面にクリックが伝わってしまうアレの苦肉の解決策
				// メッセージウィンドウの可視性を予め確認しておく
				bool MsgWindowVisible = this.msgwindow.Visible;
				
				base.Click (p);
				
				// クリック制限がされていた場合、以降の処理は行わず
				if( TutorialStatus.getClickControl()) return;
				
				// 緊急時に左上を3回クリックすると一つ前に戻る
				if( this.inRect(p, new Rectangle(0, 0, 50, 50))) this.Emergency++;
				if( this.Emergency >= 3 )
				{
					this.EmergencyCode();
					return;
				}
				
				// クリックできる範囲を座標で制限している場合はクリックされた座標が許可されている範囲かをチェック
				if( this.pattern.rect.X != -1 && !this.inRect(p, this.pattern.rect) )
				{
					// 許可範囲外だった場合はtopscreen以下のクリックはさせない	
				}
				else
				{
					if( !(MsgWindowVisible && this.msgwindow.nextbutton.Visible)) topscreen.Click(p);
				}
				
				// 動作待機中でなく、ステート進行フラグが立っていたら
				if( TutorialStatus.getNextStateFlag() && !TutorialStatus.getNextStateStandBy())
				{
					// フラグを解除して次のステートに進む
					TutorialStatus.setDisableNextState();
					this.NextState();
				}
			}
		}
		
		public new void MouseMove(System.Drawing.Point p)
		{
			if(this.isEndDialog)
			{
				this.edialog.MouseMove(p);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.MouseMove(p);
			}
			else
			{
				base.MouseMove (p);
				topscreen.MouseMove(p);
			}
		}
		
		public new void DragBegin(Point begin)
		{
			if(this.isEndDialog)
			{
				this.edialog.DragBegin (begin);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.DragBegin (begin);
			}
			else
			{
				base.DragBegin (begin);
				topscreen.DragBegin (begin);
			}
		}
		
		public new void Drag(Point begin, Point p)
		{
			if(this.isEndDialog)
			{
				this.edialog.Drag (begin, p);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.Drag (begin, p);
			}
			else
			{
				base.Drag (begin, p);
				topscreen.Drag (begin, p);
			}
		}
		
		public new void DragEnd(Point begin, Point p)
		{
			if(this.isEndDialog)
			{
				this.edialog.DragEnd (begin, p);
			}
			else if(this.isTutorialStart)
			{
				this.tutorialstart.DragEnd (begin, p);
			}
			else
			{
				base.DragEnd (begin, p);
				topscreen.DragEnd (begin, p);
			}
		}
	}
}
