using System;
using System.Drawing;
using System.Collections;

namespace muphic.ADV
{
	/// <summary>
	/// PatternData の概要の説明です。
	/// </summary>
	public class PatternData
	{
		public ArrayList UseImage;		// 使用する画像(MainStoryScreenでRegistした文字列), 画像の座標
		public string[] text;			// シナリオ本文
		public string BGM;				// BGM "STOP"なら現在のBGMを停止させる			┐
		public string SE;				// SE 基本的に一回だけ流す						┼これらはファイル名を指定
		public string Voice;			// Voice 一回だけ鳴らして次のパターンで停止		┘
		
		public int Window;				// ウィンドウの表示位置 0:表示しない 1:通常位置に表示 2～:別な位置に表示
		public string assistant;		// ウィンドウ左に表示するアシスタントの動物画像(Registした文字列)
		public bool NextButton;			// メッセージウィンドウの次へボタンを描画するか
		public bool EndButton;			// メッセージウィンドウのおわるボタンを描画するか
		public bool TopScreen;			// チュートリアル実行中に自動的にトップ画面に戻る際に使用
		public bool NextState;			// パターン読み込み後に即ステート進行を行う際に使用
		public Rectangle rect;			// クリックできる領域を部品でなく座標で制限する場合に使用
		public Point MouseClick;		// 特定の座標でクリックさせる場合に使用する座標
		public string ChapterTop;		// 各チャプター開始時のトップ画面を描画する際に使用（トップ画面背景画像のファイル名を格納する）
		public string SPMode;			// 特殊な状態を表す 所謂特殊コマンド使用のための
		public bool DrawString;			// チュートリアル中にトップスクリーン以下の文字列を描画するか
		public Point Fade;				// フェード効果を行うかどうか 欲しいのは回数と時間だがPointクラスで代用
		public ArrayList NBRegist;		// 遅延描画終了後の次へボタン表示直前で画像を読込む際に使用
　		
		public PatternData()
		{
			this.UseImage = new ArrayList();		// とりあえずインスタンス化してみる
			this.BGM = "";							// デフォルトでは空
			this.SE = "";							// デフォルトでは空
			this.Voice = "";						// デフォルトでは空
			this.Window = 1;						// デフォルトでは通常位置に描画させる
			this.assistant = "";					// デフォルトでは空
			this.NextButton = true;					// デフォルトでは描画させる
			this.EndButton = false;					// デフォルトでは描画させない
			this.TopScreen = false;					// デフォルトでは(ry
			this.NextState = false;					// デフォルトでは(ry
			this.rect = new Rectangle(-1,0,0,0);	// デフォルトではXを-1にしておく
			this.MouseClick = new Point(-1,0);		// デフォルトではXを-1にしておく
			this.ChapterTop = "";					// デフォルトでは空
			this.SPMode = "";						// デフォルトでは空
			this.DrawString = true;					// デフォルトでは描画させる設定
			this.Fade = new Point(-1, 0);			// デフォルトで回数を-1にしておく
			this.NBRegist = new ArrayList();
			
			// シナリオのインスタンス化と初期化
			this.text = new string[3];
			for(int i=0; i<this.text.Length; i++) this.text[i] = "";
		}
		
		
		/// <summary>
		/// 与えられたテキストをシナリオ本文に追加するメソッド
		/// </summary>
		/// <param name="text">追加するテキスト</param>
		/// <returns>追加できたかどうか trueなら成功</returns>
		public bool addText(string text)
		{
			for(int i=0; i<this.text.Length; i++)
			{
				if( this.text[i] == "" )
				{
					this.text[i] = text;
					return true;
				}
			}
			return false;
		}
		
		
		/// <summary>
		/// 与えられた画像をリストに追加するメソッド
		/// </summary>
		/// <param name="image">追加する画像名</param>
		/// <param name="x">追加する画像のx座標</param>
		/// <param name="y">追加する画像のy座標</param>
		/// <returns>追加できたかどうか trueなら成功</returns>
		public bool addUseImage(string image, int x, int y)
		{
			// 既に同じ画像名がリストにあれば追加しない
			if( this.UseImage.IndexOf(image) != -1 ) return false;
			
			// 画像名,x座標,y座標の順にリストに登録
			this.UseImage.Add(image);
			this.UseImage.Add(x);
			this.UseImage.Add(y);
			
			return true;
		}
		
		
		public bool addNBRegist(string name, int x, int y, string[] image)
		{
			// 既に同じ登録名があればリストに登録しない
			if( this.NBRegist.IndexOf(name) != -1) return false;
			
			// 登録名、座標、画像パスの順にリストに登録
			this.NBRegist.Add(name);
			this.NBRegist.Add(x);
			this.NBRegist.Add(y);
			this.NBRegist.Add(image);
			
			return true;
		}
	}
}
