using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace muphic.ScoreScr
{
	public class ScoreFileReader
	{
		ArrayList AnimalList;
		StreamReader sr = null;

		public ScoreFileReader(ArrayList AnimalList)
		{
			this.AnimalList = AnimalList;
		}
		
		#region ver.SETO
		
		public bool Read(String Name)
		{
			AnimalList.Clear();

			try
			{
				sr = new StreamReader("ScoreData\\" + Name + ".msd");
			}
			catch(FileNotFoundException e)
			{
				MessageBox.Show("�t�@�C����������܂���");
				return false;
			}

			String[] s;
			while((s=ReadCSV()) != null)
			{
				Animal a = new Animal(int.Parse(s[1]), int.Parse(s[2]), s[0]);
				AnimalList.Add(a);
			}
			sr.Close();
			
			return true;
		}
		
		#endregion
		
		#region ver.Gackt ���������g��
		
		public bool ReadMSDFile(string filename)
		{
			// �ガ�����I
			AnimalList.Clear();

			try
			{
				System.Console.WriteLine("muphic�X�R�A�f�[�^�t�@�C�� " + filename + " ��ǂݍ���");
				// �ǂݍ��݃o�b�t�@�ݒ� filename�̓p�X���g���q�������Ă邩�炻�̂܂܂ł���
				sr = new StreamReader(filename);
			}
			catch(FileNotFoundException)
			{
				// ����Ȃ��Ƃ��肦��Ǝv�����ǂ�
				MessageBox.Show("��p�I�t�@�C����������Ȃ�");
				return false;
			}
			
			// �ǂݍ��񂾃f�[�^���i�[����
			string[] data;
			
			// �t�@�C���̍Ō�܂œǂݍ���
			while( (data = this.ReadCSV()) != null )
			{
				// �ǂݍ��񂾃f�[�^��Animal�N���X�`���f�[�^�ɓ����
				// �����̈ʒu(data[1]), �����̉��K(data[2]), �����̎��(data[0])
				Animal a = new Animal(int.Parse(data[1]), int.Parse(data[2]), data[0]);

				// Animal���X�g�ɒǉ�
				AnimalList.Add(a);
			}
			
			// �N���[�Y
			sr.Close();

			return true;

		}
		
		#endregion
		
		/// <summary>
		/// CSV�`����1�s�ǂݍ��ރ��\�b�h
		/// </summary>
		/// <returns></returns>
		private String[] ReadCSV()
		{
			// ��s�ǂݍ���
			String s = sr.ReadLine();
			
			// �ǂݍ��܂Ȃ�������null��Ԃ�
			if(s == null) return null;
			
			// '.'�ŋ�؂��ĕ�����z��ŕԂ�
			return s.Split(new char[] {','});
		}
	}
}