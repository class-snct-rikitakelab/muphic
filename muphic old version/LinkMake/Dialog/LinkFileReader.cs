using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.LinkMake.Dialog.Select
{
	public class LinkFileReader
	{
		ArrayList AnimalList;
		String Name;
		public int Tempo;
		StreamReader sr = null;

		public LinkFileReader(ArrayList AnimalList)
		{
			this.AnimalList = AnimalList;
		}
		
		
		public bool Read(string filename)
		{
			// �ガ�����I
			AnimalList.Clear();

			try
			{
				System.Console.WriteLine("muphic�X�R�A�f�[�^�t�@�C�� " + filename + " ��ǂݍ���");
				// �ǂݍ��݃o�b�t�@�ݒ� filename�̓p�X���g���q�������Ă邩�炻�̂܂܂ł���
				sr = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException e)
			{
				// ����Ȃ��Ƃ��肦��Ǝv�����ǂ�
				MessageBox.Show("��p�I�t�@�C����������Ȃ�");
				return false;
			}
			
			// �ǂݍ��񂾃f�[�^���i�[����
			string[] data;

			Name = sr.ReadLine();
			sr.ReadLine();//���x���͔�΂�
			Tempo = int.Parse(sr.ReadLine());
			
			// �t�@�C���̍Ō�܂œǂݍ���
			int count = 0;
			while( (data = this.ReadLine()) != null )
			{
				for (int i = 0; i < data.Length-1; i+=2)
				{
					int code = int.Parse(data[i+1]);
					int place = 0;
					switch (data[i].ToCharArray()[0])
					{
						case 'A':
							place = 0;
							break;
						case 'B':
							place = 1;
							break;
						case 'C':
							place = 2;
							break;
						case 'D':
							place = 3;
							break;
						case 'E':
							place = 4;
							break;
						case 'F':
							place = 5;
							break;
						case 'G':
							place = 6;
							break;
						case 'H':
							place = 7;
							break;
						default:
							place = 0;
							break;
					}
				
					Animal a = new Animal(place+count*8, code+1, "Sheep");

					// Animal���X�g�ɒǉ�
					AnimalList.Add(a);
				}
				count++;
			}
			
			// �N���[�Y
			sr.Close();

			return true;

		}

		
		/// <summary>
		/// CSV�`����1�s�ǂݍ��ރ��\�b�h
		/// </summary>
		/// <returns></returns>
		private String[] ReadLine()
		{
			// ��s�ǂݍ���
			String s = sr.ReadLine();
			
			// �ǂݍ��܂Ȃ�������null��Ԃ�
			if(s == null) return null;
			
			// ' '�ŋ�؂��ĕ�����z��ŕԂ�
			return s.Split(new char[] {' '});
		}
	}
}