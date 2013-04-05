using System;

namespace muphic.tag
{

	/// <summary>
	/// Chunk の概要の説明です。
	/// </summary>
	public class Chunk
	{
		public String aFormat;	//フォーマットの定義。fmtチャンクかdataチャンクかを決定する(factやLISTチャンクというのもあるらしいが、それは無視する)
		public int bByte;		//チャンクのバイト数										16→10 00 00 00

		public Chunk()
		{
		}
	}

	public class fmt : Chunk
	{
		public int cFormatID;			//フォーマットID											 1→01 00
		public int dChannel;			//チャンネル数(モノラル)							ステレオ 2→02 00
		public int eSamplingRate;		//サンプリングレート								   44100Hz→44 AC 00 00
		public int fDataSpeed;			//データ速度(Byte/Sec) 44.1kHz,16bitステレオなら  44100*2*2=176400→10 B1 02 00 (0x2B110)
		public int gBlockSize;			//ブロックサイズ(Byte/Sample*チャンネル数)16bitステレオなら  2*2=4→04 00
		public int hBitperSample;		//サンプル当りのビット数(bit/Sample)Waveでは8か16			16→10 00
		public int iExtensionSize;		//拡張部分のサイズ(FormatID = 1なら存在しない)
		public byte[] Extention;		//拡張部分

		public fmt()
		{
		}

		public fmt(Chunk c)
		{
			this.aFormat = c.aFormat;
			this.bByte = c.bByte;
		}

		/// <summary>
		/// fmtチャンクの読み込み
		/// </summary>
		/// <param name="rmf">ReadMediaFile</param>
		/// <returns>成功したかどうか</returns></returns>
		public bool ReadFmt(ReadMediaFile rmf)
		{
			this.cFormatID = rmf.ReadInteger(2, true);			//フォーマットID読み込み
			this.dChannel = rmf.ReadInteger(2, true);			//チャンネル数読み込み
			this.eSamplingRate = rmf.ReadInteger(4, true);		//サンプリングレート読み込み
			this.fDataSpeed = rmf.ReadInteger(4, true);			//データ速度読み込み
			this.gBlockSize = rmf.ReadInteger(2, true);			//ブロックサイズ読み込み
			this.hBitperSample = rmf.ReadInteger(2, true);		//サンプル当りのビット数読み込み
			if(this.cFormatID != 1)
			{													//FormatIDが1でなければ
				this.iExtensionSize = rmf.ReadInteger(4, true);	//拡張部分のサイズの読み込み
				//拡張部分の読み込みも書く
				System.Windows.Forms.MessageBox.Show("現在拡張部分の読み込みは実装されていません");
			}
			return true;
		}

		/// <summary>
		/// fmtチャンクの書き込み
		/// </summary>
		/// <param name="wmf">WriteMediaFile</param>
		public void WriteFmt(WriteMediaFile wmf)
		{
			wmf.WriteInteger(this.cFormatID, 2, true);			//フォーマットID書き込み
			wmf.WriteInteger(this.dChannel, 2, true);			//チャンネル数書き込み
			wmf.WriteInteger(this.eSamplingRate, 4, true);		//サンプリングレート書き込み
			wmf.WriteInteger(this.fDataSpeed, 4, true);			//データ速度書き込み
			wmf.WriteInteger(this.gBlockSize, 2, true);			//ブロックサイズ書き込み
			wmf.WriteInteger(this.hBitperSample, 2, true);		//サンプル当りのビット数書き込み
			if(this.cFormatID != 1)
			{													//FormatIDが1でなければ
				wmf.WriteInteger(this.iExtensionSize, 4, true);	//拡張部分のサイズの書き込み
				//拡張部分の書き込みも書く
				System.Windows.Forms.MessageBox.Show("現在拡張部分の書き込みは実装されていません");
			}
		}
	}

	public class data : Chunk
	{
		public byte[][] WaveData;			//データ(ステレオなら0にL、1にRがそれぞれ入る)

		public data()
		{
		}

		public data(Chunk c)
		{
			this.aFormat = c.aFormat;
			this.bByte = c.bByte;
		}

		/// <summary>
		/// dataチャンクの読み込み
		/// </summary>
		/// <param name="rmf">ReadMediaFile</param>
		/// <param name="ch">チャンネル。ステレオなら2</param>
		/// <param name="bit">8ビットか16ビットのどっちか</param>
		/// <returns>成功したかどうか</returns>
		public bool ReadData(ReadMediaFile rmf, int ch, int bit)
		{
			int b = bit/8;					//バイト表示に直しておく
			this.WaveData = new byte[ch][];	//チャンネルの分だけ配列を作る
			for(int i=0;i<ch;i++)
			{
				this.WaveData[i] = new byte[this.bByte/ch];
			}
			for(int i=0;i<this.bByte/(ch*b);i++)
			{
				for(int j=0;j<ch;j++)
				{
					for(int k=0;k<b;k++)
					{
						this.WaveData[j][i*b + k] = rmf.Read(1)[0];
					}
				}
			}
			return true;
		}

		/// <summary>
		/// dataチャンクの書き込み
		/// </summary>
		/// <param name="wmf">WriteMediaFile</param>
		/// <param name="ch">チャンネル。ステレオなら2</param>
		/// <param name="bit">8ビットか16ビットのどっちか</param>
		public bool WriteData(WriteMediaFile wmf, int ch, int bit)
		{
			int b = bit/8;
			for(int i=0;i<this.bByte/(ch*b);i++)
			{
				for(int j=0;j<ch;j++)
				{
					for(int k=0;k<b;k++)
					{
						byte[] by = {this.WaveData[j][i*b + k]};
						wmf.Write(by);
					}
				}
			}
			return true;
		}
	}
}
