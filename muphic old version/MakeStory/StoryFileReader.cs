using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace muphic.MakeStory
{
	public class StoryFileReader
	{
		PictStory PictureStory;
		StreamReader sr = null;
		public StoryFileReader(PictStory PictureStory)
		{
			this.PictureStory = PictureStory;
		}

		public bool Read(String Name)
		{
			PictureStory.Init();							//PictureStory�̏�����
			try
			{
				sr = new StreamReader("StoryData\\" + Name + ".txt");
			}
			catch(FileNotFoundException e)
			{
				MessageBox.Show("�t�@�C����������܂���");
				return false;
			}

			String[] s;
			int NowPage=-1;
			bool NowMode=false;			//true=���i, false=�y��
			while((s=ReadCSV()) != null)
			{
				if(s.Length == 1)
				{
					PictureStory.Title = GetTitle(s[0]);
				}
				if(s.Length == 2)	//2�Ȃ�C�y�[�W���̎w��ƃ��[�h�̎w��D
				{
					NowPage = int.Parse(s[0]);
					NowMode = (s[1]=="Slide") ? true : false;
					if(NowMode)											//����Slide�Ȃ�C���̂��ƂɁC�w�i�ƃe���|�̐ݒ肪�i�[����Ă���
					{
						s = ReadCSV();
						Slide slide = PictureStory.Slide[NowPage];
						slide.haikei = new Obj( 182, 267, s[1]);			//�w�i�̓ǂݍ���
						s = ReadCSV();
						slide.tempo = int.Parse(s[1]);					//�e���|�̓ǂݍ���
						s = ReadCSV();
						if(s[0] != "sentence")
						{
							MessageBox.Show("�t�@�C���f�[�^�������ł�");
						}
						slide.Sentence = s[1];

						PictureStory.Slide[NowPage] = slide;
					}
				}
				else if(s.Length == 3)
				{
					Slide slide = PictureStory.Slide[NowPage];
					if(NowMode)							//���i�̒ǉ�
					{
						Obj o = new Obj(int.Parse(s[1]), int.Parse(s[2]), s[0]);
						//o.ObjInit(int.Parse(s[1]), int.Parse(s[2]), s[0]);
						slide.ObjList.Add(o);
					}
					else								//�����̒ǉ�
					{
						Animal a = new Animal(int.Parse(s[1]), int.Parse(s[2]), s[0]);
						slide.AnimalList.Add(a);
					}
					PictureStory.Slide[NowPage] = slide;
				}
			}
			sr.Close();

			// �����t�@�C���̓ǂݍ���
			this.LoadVoiceFile(Name);
			
			return true;
		}

		public void LoadVoiceFile(string name)
		{
			// �ǂݍ��݃t�@�C�������i�[���ꂽ�f�B���N�g�����𓾂�
			string directoryname = "StoryData";
			
			// �ǂݍ��݃t�@�C��������g���q�𔲂���������𓾂� ���ꂪ�����t�@�C�����ۑ�����Ă���f�B���N�g�����ɂȂ�
			string voicedirectoryname = Path.GetFileNameWithoutExtension(directoryname + "\\" + name);
			
			// �����t�@�C�����ۑ�����Ă���p�X�t���f�B���N�g�����𐶐�
			voicedirectoryname = directoryname + "\\" + voicedirectoryname;
			
			// �f�B���N�g�������݂��Ȃ���Ή����t�@�C���̓��[�h���Ȃ�
			if( !Directory.Exists(voicedirectoryname) ) return;
			
			// �����t�@�C�����R�s�[
			for(int i=1; i<=8; i++)
			{
				string filename = "Voice" + i.ToString() + ".wav";
				SoundManager.Delete(filename);
				File.Copy(voicedirectoryname + "\\" + filename, filename, true);
			}
		}

		/// <summary>
		/// Title:�^�C�g����
		/// �ƂȂ��Ă���s����^�C�g������������菜�����\�b�h
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private String GetTitle(String s)
		{
			char[] separater = {':'};
			return s.Split(separater)[1];
		}

		private String[] ReadCSV()
		{
			String s = sr.ReadLine();
			char[] separater = {','};
			if(s == null)
			{
				return null;
			}
			return s.Split(separater);
		}
	}
}