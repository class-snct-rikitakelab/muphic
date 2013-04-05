using System;
using System.IO;
using System.Text;
using System.Collections;
using muphic.Common;

namespace muphic.ADV
{
	public enum ADVParentScreen { TutorialScreen }
	
	/// <summary>
	/// 汎用アドベンチャーパート（読むだけ）
	/// チュートリアルでも使うので、というかそっちがメインなので、チュートリアル実行に都合のいいように作られ(ry
	/// でも独立して使うことだってできる。その為に汎用化したんだよ。使うかわからんけど。
	/// チュートリアル以外で使うときは、列挙型ADVParentScreenとAdventureEndメソッドとDrawImageメソッドあたりに何かを書き加えればいいかもしれない。
	/// </summary>
	public class AdventureMain : Screen
	{
		public Screen parent;
		public ADVParentScreen parentscreen;
		public MsgWindow msgwindow;

		public string DirectoryName;	// ストーリーに使用するデータが格納されたディレクトリ
		public string[] PatternFiles;	// パターンファイル一覧
		public string[] Chapters;		// チャプター一覧（フォルダ名）
		public string[] ImageFiles;		// 画像ファイル一覧
		public PatternData pattern;		// パターンファイルからの情報
		public string BGMFile;			// 再生中のBGMのファイル名 再生中で無い場合は空にしとく
		public string VoiceFile;		// 再生中の音声のファイル名 再生中で無い場合は空にしとく
		public bool isPlayVoice;		// 音声を再生するかどうかのフラグ
		
		public int ADVChapter;			// チャプターの進行を表す
		public int ADVState;			// ストーリーの進行を表す。
		
		public const string PatternFileDirectory = "\\PatternFiles";		// パターンファイルのディレクトリ
		public const string ImageFileDirectory   = "\\ImageFiles";		// 画像のディレクトリ
		public const string BGMFileDirectory     = "\\BGMFiles";			// BGMファイルのディレクトリ
		public const string VoiceFileDirectory   = "\\VoiceFiles";		// 音声ファイルのディレクトリ
		public const string SaveFileDirectory    = "\\SaveFiles";		// 何らかのセーブデータのディレクトリ
		public const string ControlFileDirectory = "\\ControlFiles";		// 制御ファイルのディレクトリ
		
		public AdventureMain(Screen screen, string DirectoryName, muphic.ADV.ADVParentScreen parentscreen)
		{
			this.parent = screen;
			this.DirectoryName = DirectoryName;
			this.parentscreen = parentscreen;
			this.ADVChapter = -1;
			this.BGMFile = "";
			this.VoiceFile = "";
			this.isPlayVoice = true;
			
			// メッセージウィンドウのインスタンス化
			this.msgwindow = new MsgWindow(this);
			
			// チャプター一覧を得る
			this.Chapters = TutorialTools.getDirectoryNames(DirectoryName + PatternFileDirectory, "ADVChapter");
			
			// 画像ファイル一覧を得る
			// this.ImageFiles = TutorialTools.getFileNames(DirectoryName + ImageFileDirectory);
		}
		
		
		/// <summary>
		/// チャプターを次に進めるメソッド
		/// </summary>
		public void NextChapter()
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
		/// スライドを次に進めるメソッド
		/// </summary>
		public void NextState()
		{
			// 一つ進める
			this.ADVState++;
			
			// ステートがチャプターのパターン数を超えた場合
			if( !TutorialStatus.getIsTutorial() && this.ADVState >= this.PatternFiles.Length )
			{
				if(this.ADVChapter >= this.Chapters.Length)
				{
					// もし全てのチャプターを流し終えたら終了
					this.AdventureEnd();
					return;
				}
				else
				{
					// 次のチャプターへ進む
					this.NextChapter();
				}
			}
			
			// 次のパターンファイルを読み込む
			this.pattern = TutorialTools.ReadPatternFile( this.PatternFiles[this.ADVState] );
			
			// 文字列の描画を許可するかどうかの設定 
			if( this.pattern.DrawString ) TutorialStatus.setEnableDrawString();
			else TutorialStatus.setDisableDrawString();
			
			// メッセージウィンドウに関して
			if( this.pattern.Window != 0 )
			{
				// メッセージウィンドウが表示される設定であれば
				this.msgwindow.Visible = true;
				this.msgwindow.ChangeWindowCoordinate(this.pattern.Window);	// 位置の設定
				
				// メッセージウィンドウに次のスライドのテキストを渡す
				this.msgwindow.getText(this.pattern.text);
				
				// メッセージウィンドウの次へボタンの描画の変更 遅延描画の関係でvisible変えるだけではダメなので
				this.msgwindow.getNextButtonVisible(this.pattern.NextButton);
				
				// メッセージウィンドウの次へボタンを表示する直前に画像をregistする設定になっている場合
				this.msgwindow.NBRegist = this.pattern.NBRegist;
				
				// メッセージウィンドウのアシスタント動物の変更
				// パターンファイルに何も記述されていなかった場合はそのままにしておく
				if(this.pattern.assistant != "") this.msgwindow.setAssistant(this.pattern.assistant);
			}
			else
			{
				// メッセージウィンドウが表示されない設定だったら
				this.msgwindow.Visible = false;
			}
			
			// BGMの処理
			if( (this.pattern.BGM != "") && (this.pattern.BGM != this.BGMFile) )
			{
				// パターンファイルのBGM項に現在再生中のファイル名とは別の何らかのデータがあったら
				
				// まずBGMを停止しファイル名を空にする
				this.StopBGM();
				this.BGMFile = "";
				
				// "STOP"でなければ、新しいファイルを再生する
				if(this.pattern.BGM != "STOP")
				{
					this.BGMFile = this.pattern.BGM;
					this.PlayBGM(); //うるさいので止めとく
				}
			}
			
			// Voiceの処理
			// まず再生中のを停止し、新しいファイル名をセットしたりする
			//this.StopVoice();
			//this.VoiceFile = this.pattern.Voice;
			//this.PlayVoice();
			this.SetVoice(this.pattern.Voice);
			
			
			// パターンファイル読み込み後、即ステート進行するよう設定されていた場合
			// (システム関連のみのパターンだった際に使用)
			// このコードは念のためNextStateメソッドの最後に記述する
			if( this.pattern.NextState )
			{
				// チュートリアル実行中の場合はそちらの方で呼び出すため、チュートリアル実行中でない場合のみここでステート進行
				if( !TutorialStatus.getIsTutorial() ) this.NextState();
				return;
			}
		}
		
		
		/// <summary>
		/// BGMの再生を開始するメソッド
		/// BGMのファイル名はクラスフィールドから取得
		/// </summary>
		/// <returns>再生開始できたかどうか trueなら成功</returns>
		public bool PlayBGM()
		{
			// パスも含めたファイル名の生成
			string filename = DirectoryName + BGMFileDirectory + "\\" + this.BGMFile;
			
			if( File.Exists(filename) )
			{
				// ファイルの存在チェックを行い、存在していたら再生開始
				SoundManager.Play(filename);
				System.Console.WriteLine("BGM " + filename + " 再生");
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// BGMの再生を停止するメソッド
		/// BGMのファイル名はクラスフィールドから取得
		/// </summary>
		/// <returns>再生停止できたかどうか trueなら成功</returns>
		public bool StopBGM()
		{
			// パスも含めたファイル名の生成
			string filename = DirectoryName + BGMFileDirectory + "\\" + this.BGMFile;
			
			if( File.Exists(filename) )
			{
				// ファイルの存在チェックを行い、存在していたら再生停止
				SoundManager.Stop(filename);
				System.Console.WriteLine("BGM " + filename + " 停止");
				return true;
			}
			return false;
		}
		
		
		/// <summary>
		/// Voiceの再生を開始するメソッド
		/// Voiceのファイル名はクラスフィールドから取得
		/// </summary>
		/// <returns>再生開始できたかどうか trueなら成功</returns>
		public bool PlayVoice()
		{
			if(!this.isPlayVoice) return false;
			
			// パスも含めたファイル名の生成
			string filename = DirectoryName + VoiceFileDirectory + "\\" + this.VoiceFile;
			
			if( File.Exists(filename) )
			{
				// ファイルの存在チェックを行い、存在していたら再生開始
				VoiceManager.Play(filename);
				System.Console.WriteLine("Voice " + filename + " 再生");
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Voiceの再生を停止するメソッド
		/// Voiceのファイル名はクラスフィールドから取得
		/// </summary>
		/// <returns>再生停止できたかどうか trueなら成功</returns>
		public bool StopVoice()
		{
			if(!this.isPlayVoice) return false;
			
			// パスも含めたファイル名の生成
			string filename = DirectoryName + VoiceFileDirectory + "\\" + this.VoiceFile;
			
			if( File.Exists(filename) )
			{
				// ファイルの存在チェックを行い、存在していたら再生停止
				VoiceManager.Stop();
				System.Console.WriteLine("Voice " + filename + " 停止");
				return true;
			}
			return false;
		}
		
		
		public void SetVoice(string filename)
		{
			this.StopVoice();
			this.VoiceFile = filename;
			if(this.isPlayVoice) this.PlayVoice();
		}
		
		
		/// <summary>
		/// 全スライドを流し終えたら呼び出す
		/// アドベンチャーパートの終了
		/// </summary>
		public void AdventureEnd()
		{
			// とりあえずBGM止める
			this.StopBGM();
			this.StopVoice();
			
			// 親のスクリーンが何かによって処理を変える
			switch(this.parentscreen)
			{
				case muphic.ADV.ADVParentScreen.TutorialScreen:
					// チュートリアルだった場合、チュートリアル終了メソッドを呼ぶ
					((TutorialScreen)this.parent).EndTutorial();
					break;
				default:
					break;
			}
		}
		
		/// <summary>
		/// ScreenクラスのDrawメソッドを呼ぶ
		/// </summary>
		public void ScreenDraw()
		{
			base.Draw();
		}
		
		/// <summary>
		/// 登録された画像を描画
		/// </summary>
		protected void ImageDraw()
		{
			for(int i=0; i<this.pattern.UseImage.Count/3; i++)
			{
				// ArrayListから使用する登録画像名とその座標を取り出す
				// ただし、登録名が存在しなければ飛ばす
				string image = (string)this.pattern.UseImage[i*3];
				if( !FileNameManager.GetFileExist(image)) continue;
				
				int    x     = (int)this.pattern.UseImage[i*3+1];
				int    y     = (int)this.pattern.UseImage[i*3+2];
				int    state = FileNameManager.GetFileNames(image).Length;
				
				
				if( state == 1 || image.IndexOf("Support") == -1)
				{
					// stateが1つしかない場合は、普通に描画
					DrawManager.Draw(image, x, y);
				}
				else
				{
					// stateが複数ある場合は、1秒間にそれらを入れ替えながら描画する
					
					// ただし、テキストの遅延描画実行中はチュートリアル補助画像は表示させない
					if( ((TutorialScreen)this.parent).tutorialmain.msgwindow.DelayDraw && ((int)image.IndexOf("TutorialSupport_") != -1) ) continue;
					
					// MainScreenのFrameCountを利用するため、親のクラスによって参照の仕方が変わる
					int framecount = 0;
					switch(this.parentscreen)
					{
						case muphic.ADV.ADVParentScreen.TutorialScreen:
							framecount = ((TutorialScreen)this.parent).parent.parent.FrameCount;
							break;
						default:
							break;
					}
					
					// framecountを使用しstateを定期的に変更しながら描画
					DrawManager.Draw(image, x, y, (int)System.Math.Ceiling((double)(framecount+1)/(60/state)) - 1 );
				}
			}
		}
		
		
		/// <summary>
		/// 描画メソッド
		/// </summary>
		public override void Draw()
		{
			base.Draw ();
			
			// 登録された画像を描画
			ImageDraw();
			
			// メッセージウィンドウの描画
			this.msgwindow.Draw();
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			this.msgwindow.Click(p);
		}
		
		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
			this.msgwindow.MouseMove(p);
		}
	}
}
