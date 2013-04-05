using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// Score の概要の説明です。
	/// </summary>
	public class Score : Base
	{
		public string sname;			// 音符
		public int[] code = new int[4];	// 音階 配列
		public double length;			// 音の長さ（八分とか四分とか
		public int  tie;				// タイかどうか 1:タイ(開始) 2:タイ(終了)
		public int chord;				// 和音情報(音符の数)

		public Score()
		{
			// とりあえず音階は-1で初期化
			for(int i=0; i<4; i++) this.code[i] = -1;
			// 生成時の音の長さは8分にしておく 必要なら後で直す
			this.length = 0.5;
			this.tie = 0;
			this.chord = 1;
		}

		// 音階を追加する
		// １番目から順に見ていき、空いていれば(-1だったら)そこに追加
		public int AddCode(int code)
		{
			if(this.code[0] < 0) this.code[0] = code;
			else if(this.code[1] < 0) this.code[1] = code;
			else if(this.code[2] < 0) this.code[2] = code;
			else return 0;
			return 1;
		}


		public override string ToString()
		{
			return this.sname;
		}
	}
}
