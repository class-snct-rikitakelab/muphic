using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.One
{
	public class ScoreFileWriter
	{
		ArrayList AnimalList;
		int tempo;
		StreamWriter sw = null;
		
		public const string SaveFileDirectory = "ScoreData\\";		// �f�[�^��ۑ�����f�B���N�g��
		public const string VoiceFileDirectory = "";				// �����t�@�C�����ۑ�����Ă���f�B���N�g�� �ۑ���ł͂Ȃ����Ƃɒ���
		
		// �ۑ�����ۃt�@�C���̏㏑�������邩�ǂ���
		public const bool OverrideFlag = false;
		
		public ScoreFileWriter(ArrayList AnimalList, int tempo)
		{
			this.AnimalList = AnimalList;
			this.tempo = tempo;
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
			// �t�@�C���������� ���͏㏑�����Ȃ�
			string filename = DecideFilename(name, ScoreFileWriter.OverrideFlag, ".msd");
			
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
			
			// �ȉ���������
			
			// �܂��e���|�̏�����������
			sw.WriteLine(tempo);
			
			// �������X�g����������
			for(int i=0; i<AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				WriteCSV(a.ToString(), a.place.ToString(), a.code.ToString());
			}

			// �N���[�Y
			sw.Close();
			
			// �����t�@�C���̕ۑ�
			for(int i=0; i<this.AnimalList.Count; i++)
			{
				// �������X�g���ɉ���������Ή����t�@�C���̕ۑ����s��
				Animal animal = (Animal)this.AnimalList[i];
				if(animal.AnimalName == "Voice")
				{
					this.SaveVoiceFile(name);
					break;
				}
			}
			
			// �������ݐ���
			return true;
		}
		
		
		/// <summary>
		/// �����t�@�C����ۑ����郁�\�b�h
		/// </summary>
		/// <param name="name">�ۑ�����f�B���N�g����</param>
		public void SaveVoiceFile(string name)
		{
			// �����t�@�C����ۑ�����f�B���N�g���������߂�
			// �ۑ��t�@�C�����Ɠ����f�B���N�g�����ɂ��邽�߁A�t�@�C��������̃��\�b�h�𗬗p
			string directoryname = ScoreFileWriter.DecideFilename(name, false, "");
			
			// �����t�@�C���ۑ���̃f�B���N�g�����쐬
			Directory.CreateDirectory(directoryname);
			
			// �t�@�C�����R�s�[
			for(int i=1; i<=8; i++)
			{
				string filename = "Voice" + i.ToString() + ".wav";
				File.Copy(filename, directoryname + "\\" + filename, true);
			}
		}
		
		
		/// <summary>
		/// �^����ꂽ�����񂩂�t�@�C�����𐶐����郁�\�b�h
		/// </summary>
		/// <param name="filename">������</param>
		/// <param name="overrideflag">�㏑�����邩�ǂ���</param>
		/// <returns>�t�@�C����</returns>
		public static string DecideFilename(string name, bool overrideflag, string extention)
		{
			for(int i=0; i<37564; i++)
			{
				// 0���珇�Ԃ�"������_�ԍ�.msd"�����݂��邩�`�F�b�N���Ă���
				string filename = ScoreFileWriter.SaveFileDirectory + name + "_" + i.ToString() + extention;

				// ���݂��Ȃ��t�@�C����������΂���Ɍ���
				if( !overrideflag || !File.Exists(filename) ) return filename;
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