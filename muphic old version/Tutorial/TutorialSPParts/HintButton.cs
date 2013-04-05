using System;
using System.IO;
using System.Text;
using muphic.Common;

namespace muphic.Tutorial.TutorialSPParts
{
	/// <summary>
	/// HintButton の概要の説明です。
	/// </summary>
	public class HintButton : Base
	{
		public TutorialMain parent;
		
		public string directoryname;
		public string[] filenames;
		public muphic.ADV.PatternData pattern;
		public string voicefile;

		public int hintstate;
		
		public HintButton(TutorialMain tm, string directoryname)
		{
			this.parent = tm;
			this.directoryname = directoryname;
			hintstate = -1;
			
			// ヒントファイル一覧を得る
			filenames = TutorialTools.getFileNames(directoryname, ".hnt");
		}
		
		
		/// <summary>
		/// ステート進行メソッド ヒントエディション
		/// </summary>
		public void NextState()
		{
			// ステート進行
			this.hintstate++;
			
			// ただし、ステートがヒントファイルのファイル数を超えていたら-1に戻す
			if(hintstate >= this.filenames.Length) this.hintstate = 0;
			
			// ヒントファイル読み込み
			this.pattern = this.ReadHintFile(this.filenames[hintstate]);
			
			// メッセージウィンドウにテキストを渡す
			this.parent.msgwindow.getText(this.pattern.text);
			
			// メッセージウィンドウ可視化
			this.parent.msgwindow.ChangeWindowCoordinate(1);
			
			// Voiceの処理
			// まず再生中のを停止し、新しいファイル名をセットしたりする
			this.parent.SetVoice(this.pattern.Voice);
		}
		
		/// <summary>
		/// Voiceの再生を開始するメソッド
		/// Voiceのファイル名はクラスフィールドから取得
		/// </summary>
		/// <returns>再生開始できたかどうか trueなら成功</returns>
		public bool PlayVoice()
		{
			if(!this.parent.isPlayVoice) return false;
			
			// パスも含めたファイル名の生成
			string filename = this.directoryname +  "\\" + this.voicefile;
			
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
			if(!this.parent.isPlayVoice) return false;
			
			// パスも含めたファイル名の生成
			string filename = this.directoryname + "\\" + this.voicefile;
			
			if( File.Exists(filename) )
			{
				// ファイルの存在チェックを行い、存在していたら再生停止
				VoiceManager.Stop();
				System.Console.WriteLine("Voice " + filename + " 停止");
				return true;
			}
			return false;
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// クリックされたらとりあえずステート進行
			this.NextState();
		}
		
		
		/// <summary>
		/// チュートリアルパターンファイル読み込みのメソッドを使ってヒントファイルを読む
		/// </summary>
		/// <returns></returns>
		public muphic.ADV.PatternData ReadHintFile(string filename)
		{
			return TutorialTools.ReadPatternFile(filename);
		}
		
		#region こっちは廃止
		/*
		public string[] ReadHintFile()
		{
			System.Console.WriteLine("TutorialHintFile " + filenames[hintstate] + " 読み込み");
			StreamReader reader = new StreamReader(filenames[hintstate++], Encoding.GetEncoding("Shift_JIS"));
			string[] hint = new string[3];
			string str;
			int num=0;
			
			// とりあえず初期化
			for(int i=0; i<hint.Length; i++) hint[i] = "";
			
			// ファイル末尾まで読み込み
			while( (str = reader.ReadLine()) != null ) hint[num++] = str;
			
			reader.Close();

			if(hintstate >= this.filenames.Length) this.hintstate = 0;
			
			return hint;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			this.parent.msgwindow.getText(this.ReadHintFile());
			this.parent.msgwindow.ChangeWindowCoordinate(1);
		}
		*/
		#endregion
		
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
