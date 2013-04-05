using System;
using System.IO;
using System.Windows.Forms;

namespace muphic.tag
{
	/// <summary>
	/// ReadMediaFile �̊T�v�̐����ł��B
	/// </summary>
	public class ReadMediaFile
	{
		public String FileName;
		private FileStream fs;
		public ReadMediaFile(String fname)
		{
			this.FileName = fname;
			try
			{
				fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
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
		/// ���f�B�A�t�@�C������w�肳�ꂽ�������ǂݍ���String�^�ŕԂ�
		/// </summary>
		/// <param name="num">�ǂݍ��ޕ�����</param>
		/// <returns>�ǂݍ��񂾂��̂�String�^�ɕϊ���������</returns>
		public String ReadStrings(int num)
		{
			byte[] bytes = this.Read(num);
			if(bytes == null)
			{
				return null;				//�t�@�C���̖����ɒB����
			}
			return this.BytestoString(bytes);
		}

		/// <summary>
		/// ���f�B�A�t�@�C������w�肳�ꂽ�������ǂݍ���int�^�ŕԂ�
		/// </summary>
		/// <param name="num">�ǂݍ��ރo�C�g��(int�̐�����4�o�C�g�����E)</param>
		/// <param name="isByteOrder">�o�C�g�I�[�_(�ŉ��ʃo�C�g���ŏ��̃A�h���X�ɂ��邩�ǂ���)�������t���O(WAVE�ɕK�v)</param>
		/// <returns></returns>
		public int ReadInteger(int num, bool isByteOrder)
		{
			int answer = 0;
			byte[] bytes = this.Read(num);
			if(bytes == null)
			{
				return -1;						//�t�@�C���̖����ɒB����
			}
			if(isByteOrder)						//�����o�C�g�I�[�_�̏ꍇ�A����Ȕz���
			{									//�߂��Ȃ��Ƃ����Ȃ�(�t�ɂ���)
				bytes = this.ByteOrder(bytes);
			}
			for(int i=0;i<bytes.Length;i++)
			{
				//System.Diagnostics.Debug.WriteLine(((int)bytes[i]).ToString(), i.ToString());
				answer += (int)bytes[i] << (bytes.Length-i-1) * 8;
			}
			return answer;
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
		/// byte�^�̔z���String�^�ɕϊ����郁�\�b�h
		/// </summary>
		/// <param name="chars">�ϊ�����byte�^�̔z��</param>
		/// <returns>�ϊ����ꂽString�^</returns>
		private String BytestoString(byte[] bytes)
		{
			String s = "";
			for(int i=0;i<bytes.Length;i++)
			{
				s += ((char)bytes[i]).ToString();
			}
			return s;
		}

		/// <summary>
		/// ���f�B�A�t�@�C������char�^�̔z��Ŏw�肳�ꂽ�o�C�g�������ǂݍ��ފ�{���\�b�h
		/// </summary>
		/// <param name="num">�ǂݍ��ރo�C�g��</param>
		/// <returns></returns>
		public byte[] Read(int num)
		{
			byte[] bytes = new byte[num];
			int size = fs.Read(bytes, 0, num);
			if(size == 0)
			{
				return null;
			}
			return bytes;
		}
	}
}
