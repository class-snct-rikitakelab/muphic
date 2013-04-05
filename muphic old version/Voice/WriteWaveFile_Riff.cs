using System;

namespace muphic.tag
{
	/// <summary>
	/// WriteWaveFile_Riff �̊T�v�̐����ł��B
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
			wmf.WriteStrings(wave.aIdentity);							//���ʕ�����"RIFF"�̏�������
			wmf.WriteInteger(wave.bFileSize, 4, true);					//�t�@�C���T�C�Y-8�̏�������
			wmf.WriteStrings(wave.cRiffKind);							//RIFF�̎��"WAVE"�̏�������

			WriteChunk();												//�`�����N�����̏�������
		}

		/// <summary>
		/// �`�����N�����̏�������
		/// </summary>
		private void WriteChunk()
		{
			int ch = 0;
			int bit = 0;
			for(int i=0;i<wave.chunk.Length;i++)
			{
				Chunk chunk = wave.chunk[i];
				if(chunk.aFormat == "fmt ")			//�`�����N��fmt������
				{
					this.WriteChunkHeader(chunk);
					((fmt)chunk).WriteFmt(wmf);		//fmt�`�����N�̏�������
					ch = ((fmt)chunk).dChannel;
					bit = ((fmt)chunk).hBitperSample;
				}
				else if(chunk.aFormat == "data")	//�`�����N��data������
				{
					this.WriteChunkHeader(chunk);
					((data)chunk).WriteData(wmf, ch, bit);	//data�`�����N�̏�������
				}
				else								//���̑��̃`�����N������
				{
					//wmf.Write();					//�������Ȃ�
				}
			}
		}

		/// <summary>
		/// �`�����N�̃w�b�_�̏�������
		/// </summary>
		/// <param name="c">�������ރw�b�_���L�q����Ă���Chunk</param>
		private void WriteChunkHeader(Chunk c)
		{
			wmf.WriteStrings(c.aFormat);				//�`�����N�̃��[�h�̏�������
			wmf.WriteInteger(c.bByte, 4, true);			//�`�����N�T�C�Y�̏�������
		}
	}
}
