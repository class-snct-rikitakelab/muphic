using System;
using System.Collections;

namespace muphic.tag
{
	/// <summary>
	/// ReadWaveFile_Riff �̊T�v�̐����ł��B
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
			wave.aIdentity = rmf.ReadStrings(4);						//���ʕ�����?RIFF�h�̓ǂݍ���
			wave.bFileSize = rmf.ReadInteger(4, true);					//�t�@�C���T�C�Y-8�̓ǂݍ���
			wave.cRiffKind = rmf.ReadStrings(4);						//RIFF�̎��?WAVE"�̓ǂݍ���
			if(wave.aIdentity != "RIFF" || wave.cRiffKind != "WAVE")
			{
				System.Windows.Forms.MessageBox.Show("�����WAVE�t�@�C���ł͂���܂���");
			}

			ReadChunk();												//�`�����N�����̓ǂݍ���
			
			return wave;
		}

		/// <summary>
		/// �`�����N�����̓ǂݍ���
		/// </summary>
		private void ReadChunk()
		{
			int ch = 0;
			int bit = 0;
			ArrayList ChunkList = new ArrayList();

			while(true)
			{
				Chunk chunk = ReadChunkHeader();	//�`�����N�̃w�b�_�̓ǂݍ���
				if(chunk == null)					//�t�@�C���������ɒB����
				{
					break;
				}
				else if(chunk.aFormat == "fmt ")	//�`�����N��fmt������
				{
					fmt f = new fmt(chunk);
					f.ReadFmt(rmf);					//fmt�`�����N�̓ǂݍ���
					ch = f.dChannel;
					bit = f.hBitperSample;
					ChunkList.Add(f);
				}
				else if(chunk.aFormat == "data")	//�`�����N��data������
				{
					data d = new data(chunk);
					d.ReadData(rmf, ch, bit);		//data�`�����N�̓ǂݍ���
					ChunkList.Add(d);
				}
				else								//���̑��̃`�����N������
				{
					rmf.Read(chunk.bByte);			//��ǂ݂��Ĕ�΂�
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
		/// �`�����N�̃w�b�_�̓ǂݍ���
		/// </summary>
		/// <returns>�ǂݍ��܂ꂽ�w�b�_</returns>
		private Chunk ReadChunkHeader()
		{
			Chunk answer = new Chunk();
			answer.aFormat = rmf.ReadStrings(4);		//�`�����N�̃��[�h�̓ǂݍ���
			answer.bByte = rmf.ReadInteger(4, true);	//�`�����N�T�C�Y�̓ǂݍ���
			if(answer.aFormat == null || answer.bByte == -1)
			{
				return null;							//�t�@�C���̖����ɒB����
			}
			return answer;
		}
	}
}
