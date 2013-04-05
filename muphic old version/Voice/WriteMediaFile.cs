using System;
using System.IO;
using System.Windows.Forms;

namespace muphic.tag
{
	/// <summary>
	/// WriteMediaFile �̊T�v�̐����ł��B
	/// </summary>
	public class WriteMediaFile
	{
		public String FileName;
		private FileStream fs;

		public WriteMediaFile(String fname)
		{
			this.FileName = fname;
			try
			{
				fs = new FileStream(fname, FileMode.Create);
			}
			catch(IOException e)
			{
				MessageBox.Show("�t�@�C��" + fname + "��������܂���");
			}
		}

		public void CloseMediaFile()
		{
			fs.Close();
		}

		/// <summary>
		/// String�^�ŗ^����ꂽ���̂����f�B�A�t�@�C���ɏ�������
		/// </summary>
		/// <param name="num">�������ޕ�����</param>
		public void WriteStrings(String s)
		{
			byte[] bytes = this.StringtoByte(s);
			this.Write(bytes);
		}

		/// <summary>
		/// int�^�̂��̂��w�肳�ꂽ�o�C�g���������f�B�A�t�@�C���ɏ�������
		/// </summary>
		/// <param name="a">�������ޑΏۂ̐���</param>
		/// <param name="num">�������ރo�C�g��(int�̐�����4�o�C�g�����E)</param>
		/// <param name="isByteOrder">�o�C�g�I�[�_(�ŉ��ʃo�C�g���ŏ��̃A�h���X�ɂ��邩�ǂ���)�������t���O(WAVE�ɕK�v)</param>
		public void WriteInteger(int a, int num, bool isByteOrder)
		{
			byte[] bytes = new byte[num];
			for(int i=0;i<num;i++)
			{
				bytes[bytes.Length-i-1] = (byte)(a & 0xFF);		//�ŉ��ʃo�C�g��bytes�ɑ������
				a = a >> 8;										//�ŉ��ʃo�C�g����������
			}
			bytes = this.ByteOrder(bytes);
			this.Write(bytes);
		}

		/// <summary>
		/// �o�C�g�I�[�_�����ɖ߂�(�������̓o�C�g�I�[�_�ɂ���)���\�b�h
		/// </summary>
		/// <param name="Oldchars">����byte�^�z��</param>
		/// <returns>�������byte�^�z��</returns>
		public byte[] ByteOrder(byte[] Oldbytes)
		{
			byte[] Newbytes = new byte[Oldbytes.Length];
			for(int i=0;i<Oldbytes.Length;i++)
			{
				Newbytes[i] = Oldbytes[Oldbytes.Length-i-1];
			}
			return Newbytes;
		}


		/// <summary>
		/// String�^��byte�^�̔z��ɂɕϊ����郁�\�b�h
		/// </summary>
		/// <param name="chars">�ϊ�����byte�^�̔z��</param>
		/// <returns>�ϊ����ꂽString�^</returns>
		private byte[] StringtoByte(String s)
		{
			byte[] bytes = new byte[s.Length];
			for(int i=0;i<s.Length;i++)
			{
				bytes[i] = (byte)s[i];
			}
			return bytes;
		}

		/// <summary>
		/// ���f�B�A�t�@�C���Ɏw�肵���o�C�g�������������ރ��\�b�h
		/// </summary>
		/// <param name="bytes">�������ނ���</param>
		public void Write(byte[] bytes)
		{
			fs.Write(bytes, 0, bytes.Length);
		}
	}
}
