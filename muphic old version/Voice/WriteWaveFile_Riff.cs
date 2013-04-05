using System;

namespace muphic.tag
{
	/// <summary>
	/// WriteWaveFile_Riff の概要の説明です。
	/// </summary>
	public class WriteWaveFile_Riff
	{
		WriteMediaFile wmf;
		Wave_Riff wave;

		public WriteWaveFile_Riff(String fname, Wave_Riff w)
		{
			wmf = new WriteMediaFile(fname);
			wave = w;
		}
		
		public void CloseWaveFile()
		{
			wmf.CloseMediaFile();
		}

		public void WriteWave()
		{
			wmf.WriteStrings(wave.aIdentity);							//識別文字列"RIFF"の書き込み
			wmf.WriteInteger(wave.bFileSize, 4, true);					//ファイルサイズ-8の書き込み
			wmf.WriteStrings(wave.cRiffKind);							//RIFFの種類"WAVE"の書き込み

			WriteChunk();												//チャンクたちの書き込み
		}

		/// <summary>
		/// チャンクたちの書き込み
		/// </summary>
		private void WriteChunk()
		{
			int ch = 0;
			int bit = 0;
			for(int i=0;i<wave.chunk.Length;i++)
			{
				Chunk chunk = wave.chunk[i];
				if(chunk.aFormat == "fmt ")			//チャンクがfmtだった
				{
					this.WriteChunkHeader(chunk);
					((fmt)chunk).WriteFmt(wmf);		//fmtチャンクの書き込み
					ch = ((fmt)chunk).dChannel;
					bit = ((fmt)chunk).hBitperSample;
				}
				else if(chunk.aFormat == "data")	//チャンクがdataだった
				{
					this.WriteChunkHeader(chunk);
					((data)chunk).WriteData(wmf, ch, bit);	//dataチャンクの書き込み
				}
				else								//その他のチャンクだった
				{
					//wmf.Write();					//何もしない
				}
			}
		}

		/// <summary>
		/// チャンクのヘッダの書き込み
		/// </summary>
		/// <param name="c">書き込むヘッダが記述されているChunk</param>
		private void WriteChunkHeader(Chunk c)
		{
			wmf.WriteStrings(c.aFormat);				//チャンクのモードの書き込み
			wmf.WriteInteger(c.bByte, 4, true);			//チャンクサイズの書き込み
		}
	}
}
