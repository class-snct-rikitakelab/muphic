using System;
using System.Collections;

namespace muphic.tag
{
	/// <summary>
	/// ReadWaveFile_Riff の概要の説明です。
	/// </summary>
	public class ReadWaveFile_Riff
	{
		ReadMediaFile rmf;
		Wave_Riff wave;

		public ReadWaveFile_Riff(String fname)
		{
			rmf = new ReadMediaFile(fname);
			wave = new Wave_Riff();
		}

		public void CloseWaveFile()
		{
			rmf.CloseMediaFile();
		}

		public Wave_Riff ReadWave()
		{
			wave.aIdentity = rmf.ReadStrings(4);						//識別文字列?RIFF”の読み込み
			wave.bFileSize = rmf.ReadInteger(4, true);					//ファイルサイズ-8の読み込み
			wave.cRiffKind = rmf.ReadStrings(4);						//RIFFの種類?WAVE"の読み込み
			if(wave.aIdentity != "RIFF" || wave.cRiffKind != "WAVE")
			{
				System.Windows.Forms.MessageBox.Show("これはWAVEファイルではありません");
			}

			ReadChunk();												//チャンクたちの読み込み
			
			return wave;
		}

		/// <summary>
		/// チャンクたちの読み込み
		/// </summary>
		private void ReadChunk()
		{
			int ch = 0;
			int bit = 0;
			ArrayList ChunkList = new ArrayList();

			while(true)
			{
				Chunk chunk = ReadChunkHeader();	//チャンクのヘッダの読み込み
				if(chunk == null)					//ファイルが末尾に達した
				{
					break;
				}
				else if(chunk.aFormat == "fmt ")	//チャンクがfmtだった
				{
					fmt f = new fmt(chunk);
					f.ReadFmt(rmf);					//fmtチャンクの読み込み
					ch = f.dChannel;
					bit = f.hBitperSample;
					ChunkList.Add(f);
				}
				else if(chunk.aFormat == "data")	//チャンクがdataだった
				{
					data d = new data(chunk);
					d.ReadData(rmf, ch, bit);		//dataチャンクの読み込み
					ChunkList.Add(d);
				}
				else								//その他のチャンクだった
				{
					rmf.Read(chunk.bByte);			//空読みして飛ばす
				}
			}

			Chunk[] cs = new Chunk[ChunkList.Count];
			for (int i = 0; i < ChunkList.Count; i++)
			{
				cs[i] = (Chunk)ChunkList[i];
			}
			wave.chunk = cs;
		}

		/// <summary>
		/// チャンクのヘッダの読み込み
		/// </summary>
		/// <returns>読み込まれたヘッダ</returns>
		private Chunk ReadChunkHeader()
		{
			Chunk answer = new Chunk();
			answer.aFormat = rmf.ReadStrings(4);		//チャンクのモードの読み込み
			answer.bByte = rmf.ReadInteger(4, true);	//チャンクサイズの読み込み
			if(answer.aFormat == null || answer.bByte == -1)
			{
				return null;							//ファイルの末尾に達した
			}
			return answer;
		}
	}
}
