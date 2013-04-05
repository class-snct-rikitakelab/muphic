using System;

namespace muphic.tag
{

	/// <summary>
	/// Chunk �̊T�v�̐����ł��B
	/// </summary>
	public class Chunk
	{
		public String aFormat;	//�t�H�[�}�b�g�̒�`�Bfmt�`�����N��data�`�����N�������肷��(fact��LIST�`�����N�Ƃ����̂�����炵�����A����͖�������)
		public int bByte;		//�`�����N�̃o�C�g��										16��10 00 00 00

		public Chunk()
		{
		}
	}

	public class fmt : Chunk
	{
		public int cFormatID;			//�t�H�[�}�b�gID											 1��01 00
		public int dChannel;			//�`�����l����(���m����)							�X�e���I 2��02 00
		public int eSamplingRate;		//�T���v�����O���[�g								   44100Hz��44 AC 00 00
		public int fDataSpeed;			//�f�[�^���x(Byte/Sec) 44.1kHz,16bit�X�e���I�Ȃ�  44100*2*2=176400��10 B1 02 00 (0x2B110)
		public int gBlockSize;			//�u���b�N�T�C�Y(Byte/Sample*�`�����l����)16bit�X�e���I�Ȃ�  2*2=4��04 00
		public int hBitperSample;		//�T���v������̃r�b�g��(bit/Sample)Wave�ł�8��16			16��10 00
		public int iExtensionSize;		//�g�������̃T�C�Y(FormatID = 1�Ȃ瑶�݂��Ȃ�)
		public byte[] Extention;		//�g������

		public fmt()
		{
		}

		public fmt(Chunk c)
		{
			this.aFormat = c.aFormat;
			this.bByte = c.bByte;
		}

		/// <summary>
		/// fmt�`�����N�̓ǂݍ���
		/// </summary>
		/// <param name="rmf">ReadMediaFile</param>
		/// <returns>�����������ǂ���</returns></returns>
		public bool ReadFmt(ReadMediaFile rmf)
		{
			this.cFormatID = rmf.ReadInteger(2, true);			//�t�H�[�}�b�gID�ǂݍ���
			this.dChannel = rmf.ReadInteger(2, true);			//�`�����l�����ǂݍ���
			this.eSamplingRate = rmf.ReadInteger(4, true);		//�T���v�����O���[�g�ǂݍ���
			this.fDataSpeed = rmf.ReadInteger(4, true);			//�f�[�^���x�ǂݍ���
			this.gBlockSize = rmf.ReadInteger(2, true);			//�u���b�N�T�C�Y�ǂݍ���
			this.hBitperSample = rmf.ReadInteger(2, true);		//�T���v������̃r�b�g���ǂݍ���
			if(this.cFormatID != 1)
			{													//FormatID��1�łȂ����
				this.iExtensionSize = rmf.ReadInteger(4, true);	//�g�������̃T�C�Y�̓ǂݍ���
				//�g�������̓ǂݍ��݂�����
				System.Windows.Forms.MessageBox.Show("���݊g�������̓ǂݍ��݂͎�������Ă��܂���");
			}
			return true;
		}

		/// <summary>
		/// fmt�`�����N�̏�������
		/// </summary>
		/// <param name="wmf">WriteMediaFile</param>
		public void WriteFmt(WriteMediaFile wmf)
		{
			wmf.WriteInteger(this.cFormatID, 2, true);			//�t�H�[�}�b�gID��������
			wmf.WriteInteger(this.dChannel, 2, true);			//�`�����l������������
			wmf.WriteInteger(this.eSamplingRate, 4, true);		//�T���v�����O���[�g��������
			wmf.WriteInteger(this.fDataSpeed, 4, true);			//�f�[�^���x��������
			wmf.WriteInteger(this.gBlockSize, 2, true);			//�u���b�N�T�C�Y��������
			wmf.WriteInteger(this.hBitperSample, 2, true);		//�T���v������̃r�b�g����������
			if(this.cFormatID != 1)
			{													//FormatID��1�łȂ����
				wmf.WriteInteger(this.iExtensionSize, 4, true);	//�g�������̃T�C�Y�̏�������
				//�g�������̏������݂�����
				System.Windows.Forms.MessageBox.Show("���݊g�������̏������݂͎�������Ă��܂���");
			}
		}
	}

	public class data : Chunk
	{
		public byte[][] WaveData;			//�f�[�^(�X�e���I�Ȃ�0��L�A1��R�����ꂼ�����)

		public data()
		{
		}

		public data(Chunk c)
		{
			this.aFormat = c.aFormat;
			this.bByte = c.bByte;
		}

		/// <summary>
		/// data�`�����N�̓ǂݍ���
		/// </summary>
		/// <param name="rmf">ReadMediaFile</param>
		/// <param name="ch">�`�����l���B�X�e���I�Ȃ�2</param>
		/// <param name="bit">8�r�b�g��16�r�b�g�̂ǂ�����</param>
		/// <returns>�����������ǂ���</returns>
		public bool ReadData(ReadMediaFile rmf, int ch, int bit)
		{
			int b = bit/8;					//�o�C�g�\���ɒ����Ă���
			this.WaveData = new byte[ch][];	//�`�����l���̕������z������
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
		/// data�`�����N�̏�������
		/// </summary>
		/// <param name="wmf">WriteMediaFile</param>
		/// <param name="ch">�`�����l���B�X�e���I�Ȃ�2</param>
		/// <param name="bit">8�r�b�g��16�r�b�g�̂ǂ�����</param>
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
