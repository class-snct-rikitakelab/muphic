using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

//�T�J����̒����܂����[
namespace muphic.LinkMake.Dialog
{
	public class LinkFileWriter
	{
		ArrayList AnimalList;
		int Tempo;
		int Level;
		int Filenum;
		String Name;
		StreamWriter sw = null;

		public LinkFileWriter(ArrayList AnimalList, int tempo, String name, int filenum, int level)
		{
			this.AnimalList = AnimalList;
			this.Tempo = tempo;
			this.Name = name;
			this.Filenum = filenum;
			this.Level = level;
		}
		
		/// <summary>
		/// �t�@�C���������݃��\�b�h
		/// </summary>
		/// <param name="name">�t�@�C�����ɂȂ镶����</param>
		/// <returns>�������ݐ������ۂ�</returns>
		public bool Write()
		{
			// �t�@�C����������
			string filename = DecideFilename(Filenum);
			String[] code = {"A","B","C","D","E","F","G","H"};

			// �t�@�C�����擾�Ɏ��s�����ꍇ �������ݎ��s
			if(filename == null) return false;
			
			try
			{
				System.Console.WriteLine("muphicMDL�t�@�C�� " + filename + " �֏����o��");
				// �������݃o�b�t�@�̐ݒ�
				sw = new StreamWriter(filename, false, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException e)
			{
				// �t�@�C���I�[�v���Ɏ��s�����ꍇ �������ݎ��s
				MessageBox.Show("��p�I�t�@�C�����J���Ȃ�");
				return false;
			}
			
			int maxplace = 0;
			String str = "";
			// ��������
			sw.WriteLine(Name);
			sw.WriteLine(Level);
			sw.WriteLine(Tempo);

			//�ő�l�擾
			for(int i=0; i<AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				if (maxplace < a.place)
				{
					maxplace = a.place;
				}
			}
			
			int temp = 8;
			//1���߂��Ƃɏ�������
			for (int i = 0; i <= maxplace/8; i++)
			{
				for (int j = 0; j < AnimalList.Count; j++)
				{
					Animal a = (Animal)AnimalList[j];
					if (a.place < temp && a.place > temp-9)
					{
						str += code[a.place%8] + " " + (a.code-1) + " ";
					}
				}
				temp += 8;
				sw.WriteLine(str);
				str = "";
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
		public static string DecideFilename(int filenum)
		{
			if (filenum < 10)
			{
				for(int i=10; i<37564; i++)
				{
					// 0���珇�Ԃ�"������_�ԍ�.msd"�����݂��邩�`�F�b�N���Ă���
					string filename = "LinkData\\QuesPatt_" + i.ToString() + ".mdl";

					// ���݂��Ȃ��t�@�C����������΂���Ɍ���
					if( !File.Exists(filename) ) return filename;
				}
			}
			else
			{
				string filename = "LinkData\\QuesPatt_" + filenum.ToString() + ".mdl";
				return filename;
			}

			return null;
		}
		

	}
}