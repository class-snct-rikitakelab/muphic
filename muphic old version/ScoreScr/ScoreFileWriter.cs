using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.ScoreScr
{
	public class ScoreFileWriter
	{
		ArrayList AnimalList;
		StreamWriter sw = null;
		public ScoreFileWriter(ArrayList AnimalList)
		{
			this.AnimalList = AnimalList;
		}
		
		#region ver.SETO
		
		public bool Write(String Name)
		{
			try
			{
				sw = new StreamWriter("StoryData\\" + Name + ".txt");
			}
			catch(FileNotFoundException e)
			{
				MessageBox.Show("�t�@�C����������܂���");
				return false;
			}

			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				WriteCSV(a.ToString(), a.place.ToString(), a.code.ToString());
			}
			sw.Close();
			return true;
		}
		
		#endregion
		
		#region ver.Gackt ���������g��
		
		/// <summary>
		/// �t�@�C���������݃��\�b�h
		/// </summary>
		/// <param name="name">�t�@�C�����ɂȂ镶����</param>
		/// <returns>�������ݐ������ۂ�</returns>
		public bool WriteMSDFile(string name)
		{
			// �t�@�C����������
			string filename = DecideFilename(name);

			// �t�@�C�����擾�Ɏ��s�����ꍇ �������ݎ��s
			if(filename == null) return false;
			
			try
			{
				System.Console.WriteLine("muphic�X�R�A�f�[�^�t�@�C�� " + filename + " �֏����o��");
				// �������݃o�b�t�@�̐ݒ�
				sw = new StreamWriter(filename, false, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException)
			{
				// �t�@�C���I�[�v���Ɏ��s�����ꍇ �������ݎ��s
				MessageBox.Show("��p�I�t�@�C�����J���Ȃ�");
				return false;
			}
			
			// ��������
			for(int i=0; i<AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				WriteCSV(a.ToString(), a.place.ToString(), a.code.ToString());
			}

			// �N���[�Y
			sw.Close();

			// �������ݐ���
			return true;
		}
		
		
		/// <summary>
		/// �^����ꂽ�����񂩂�t�@�C�����𐶐����郁�\�b�h
		/// </summary>
		/// <param name="filename">������</param>
		/// <returns>�t�@�C����</returns>
		public static string DecideFilename(string name)
		{
			for(int i=0; i<37564; i++)
			{
				// 0���珇�Ԃ�"������_�ԍ�.msd"�����݂��邩�`�F�b�N���Ă���
				string filename = "ScoreData\\" + name + "_" + i.ToString() + ".msd";

				// ���݂��Ȃ��t�@�C����������΂���Ɍ���
				if( !File.Exists(filename) ) return filename;
			}

			return null;
		}
		
		#endregion
		
		private void WriteCSV(String s1, String s2)
		{
			WriteCSV(new String[] {s1, s2});
		}

		private void WriteCSV(String s1, String s2, String s3)
		{
			WriteCSV(new String[] {s1, s2, s3});
		}

		private void WriteCSV(String[] strs)
		{
			String s = "";
			for(int i=0;i<strs.Length;i++)
			{
				if(s != "")
				{
					s += ",";
				}
				s += strs[i];
			}
			sw.WriteLine(s);
		}
	}
}