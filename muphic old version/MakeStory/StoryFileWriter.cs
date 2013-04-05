using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace muphic.MakeStory
{
	public class StoryFileWriter
	{
		PictStory PictureStory;
		StreamWriter sw = null;
		public const string SaveFileDirectory = "StoryData\\";		// �f�[�^��ۑ�����f�B���N�g��

		public StoryFileWriter(PictStory PictureStory)
		{
			this.PictureStory = PictureStory;
		}

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

			WriteTitle(PictureStory.Title);						//�^�C�g���̕`��
			for(int i=0;i<10;i++)
			{
				Slide slide = PictureStory.Slide[i];
				WriteCSV(i.ToString(), "Slide");				//i���ڂ̕��i�̕ۑ��J�n
				WriteCSV("haikei", PictureStory.Slide[i].haikei.ToString());//�w�i�̕ۑ�
				WriteCSV("tempo", PictureStory.Slide[i].tempo.ToString());	//�e���|�̕ۑ�
				WriteCSV("sentence", PictureStory.Slide[i].Sentence);	//���͂̕ۑ�
				for(int j=0;j<slide.ObjList.Count;j++)
				{
					Obj o = (Obj)slide.ObjList[j];
					WriteCSV(o.ToString(), o.X.ToString(), o.Y.ToString());
				}
				WriteCSV(i.ToString(), "Animal");
				for(int j=0;j<slide.AnimalList.Count;j++)
				{
					Animal a = (Animal)slide.AnimalList[j];
					WriteCSV(a.ToString(), a.place.ToString(), a.code.ToString());
				}
			}
			sw.Close();

			// �����t�@�C���̕ۑ�
			for(int i=0; i<10; i++)
			{
				Slide slide = PictureStory.Slide[i];
				for(int j=0;j<slide.AnimalList.Count;j++)
				{
					// �������X�g���ɉ���������Ή����t�@�C���̕ۑ����s��
					Animal animal = (Animal)slide.AnimalList[j];
					if(animal.AnimalName == "Voice")
					{
						this.SaveVoiceFile(Name);
						break;
					}
				}
			}	
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
			//string directoryname = StoryFileWriter.DecideFilename(name, false, "");
			string directoryname = "StoryData\\" + name;
			
			if(!Directory.Exists(directoryname))
			{
				// �����t�@�C���ۑ���̃f�B���N�g�����쐬
				Directory.CreateDirectory(directoryname);
			}
			
			// �t�@�C�����R�s�[
			for(int i=1; i<=8; i++)
			{
				string filename = "Voice" + i.ToString() + ".wav";
				File.Copy(filename, directoryname + "\\" + filename,  true);
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
				string filename = StoryFileWriter.SaveFileDirectory + name + "_" + i.ToString() + extention;

				// ���݂��Ȃ��t�@�C����������΂���Ɍ���
				if( !overrideflag || !File.Exists(filename) ) return filename;
			}

			return null;
		}

		private void WriteTitle(String title)
		{
			String s = "Title:" + title;
			sw.WriteLine(s);
		}

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