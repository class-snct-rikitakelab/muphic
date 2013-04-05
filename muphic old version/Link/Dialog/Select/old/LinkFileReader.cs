//using System;
//using System.Collections;
//using System.IO;
//using System.Text;
//using System.Windows.Forms;
//using System.Drawing;
//
//
//namespace muphic.Link.Dialog.Select
//{
//	public class LinkFileReader
//	{
//		ArrayList AnimalList;
//		public String Name;
//		public int Tempo;
//		StreamReader sr = null;
//		public Point[][] pattern = new Point[10][];
//
//		public LinkFileReader(ArrayList AnimalList)
//		{
//			this.AnimalList = AnimalList;
//		}
//		
//		
//		public bool Read(string filename)
//		{
//			// �ガ�����I
//			AnimalList.Clear();
//
//			try
//			{
//				System.Console.WriteLine("muphic�X�R�A�f�[�^�t�@�C�� " + filename + " ��ǂݍ���");
//				// �ǂݍ��݃o�b�t�@�ݒ� filename�̓p�X���g���q�������Ă邩�炻�̂܂܂ł���
//				sr = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
//			}
//			catch(FileNotFoundException)
//			{
//				// ����Ȃ��Ƃ��肦��Ǝv�����ǂ�
//				MessageBox.Show("��p�I�t�@�C����������Ȃ�");
//				return false;
//			}
//			
//			// �ǂݍ��񂾃f�[�^���i�[����
//			string[] data;
//
//			Name = sr.ReadLine();
//			Tempo = int.Parse(sr.ReadLine());
//			
//
//			// �t�@�C���̍Ō�܂œǂݍ���
//			int count = 0;
//			while( (data = this.ReadLine()) != null )
//			{
//				for (int i = 0; i < data.Length-1; i+=2)
//				{
//					int code = int.Parse(data[i+1]);
//					int place = 0;
//					switch (data[i].ToCharArray()[0])
//					{
//						case 'A':
//							place = 0;
//							break;
//						case 'B':
//							place = 1;
//							break;
//						case 'C':
//							place = 2;
//							break;
//						case 'D':
//							place = 3;
//							break;
//						case 'E':
//							place = 4;
//							break;
//						case 'F':
//							place = 5;
//							break;
//						case 'G':
//							place = 6;
//							break;
//						case 'H':
//							place = 7;
//							break;
//						default:
//							place = 0;
//							break;
//					}
//				
//					Animal a = new Animal(place+count*8, code+1, "Sheep");
//
//					// Animal���X�g�ɒǉ�
//					AnimalList.Add(a);
//				}
//				
//				//���ԂȂ��̂œˊтō��܂�
//				
//				pattern[count] = new Point[data.Length/2];
//				for (int i = 0; i < data.Length/2; i++)
//				{
//					switch (data[i*2].ToCharArray(0, 1)[0])
//					{
//						case 'A':
//							pattern[count][i].X = -3;
//							break;
//						case 'B':
//							pattern[count][i].X = -2;
//							break;
//						case 'C':
//							pattern[count][i].X = -1;
//							break;
//						case 'D':
//							pattern[count][i].X = 0;
//							break;
//						case 'E':
//							pattern[count][i].X = 1;
//							break;
//						case 'F':
//							pattern[count][i].X = 2;
//							break;
//						case 'G':
//							pattern[count][i].X = 3;
//							break;
//						case 'H':
//							pattern[count][i].X = 4;
//							break;
//						default:
//							pattern[count][i].X = 0;
//							break;
//					}
//
//					switch (data[i*2+1].ToCharArray(0, 1)[0])
//					{
//						case '0':
//							pattern[count][i].Y = 0; //25 + 181;
//							break;
//						case '1':
//							pattern[count][i].Y = 1; //75 + 181;
//							break;
//						case '2':
//							pattern[count][i].Y = 2; //125 + 181;
//							break;
//						case '3':
//							pattern[count][i].Y = 3; //175 + 181;
//							break;
//						case '4':
//							pattern[count][i].Y = 4; //225 + 181;
//							break;
//						case '5':
//							pattern[count][i].Y = 5; //275 + 181;
//							break;
//						case '6':
//							pattern[count][i].Y = 6; //325 + 181;
//							break;
//						case '7':
//							pattern[count][i].Y = 7; //375 + 181;
//							break;
//						default:
//							pattern[count][i].Y = 4; //225 + 181;
//							break;
//					}
//				}
//
//				count++;
//			}
//			
//			// �N���[�Y
//			sr.Close();
//
//			//pattern�̏d���`�F�b�N�A�_�~�[�𓚂̐�����
//			Random rand = new System.Random(); //���������փN���X�C���X�^���X��
//
////			//���l�p�^�[�������݂���@���@���S�Ƀ����_���ȃp�^�[���𐶐����ĕЕ���u��������
////			for (int i = 0; i < count-1; i++)
////			{
////				for (int j = i+1; j < count; j++)
////				{
////					if (pattern[i].Length == pattern[j].Length)
////					{
////						bool flag_k = true;
////						for (int k = 0; k < pattern[i].Length; k++)
////						{
////							bool flag_l = false;
////							for (int l = 0; l < pattern[j].Length; l++)
////							{
////								if (pattern[i][k].X == pattern[j][l].X && pattern[i][k].Y == pattern[j][l].Y)
////								{
////									flag_l = true;
////								}
////							}
////							if (!flag_l)
////							{
////								flag_k = false;
////								break;
////							}
////						}
////
////						if (flag_k)
////						{
////							int pat_num = rand.Next(2, 6);
////							pattern[j] = new Point[pat_num];
////							for (int k = 0; k < pat_num; k++)
////							{
////								pattern[j][k].X = rand.Next(7) - 3;
////								pattern[j][k].Y = rand.Next(7);
////							}
////						}
////					}
////				}
////			}
//
//			//�󂢂Ă���ꏊ������΂����ɗގ��p�^�[�����l�ߍ���
//			//���@�����@�����瓯���p�^�[�����o�Ă���\������
//			//�܂������_���p�^�[���𐶐����Ȃ��ꍇ������̂Ŕ��X���炢�Ő�������
//			for (int i = count; i < 10; i++)
//			{
//
//				if (rand.Next(4) > -1)
//				{
//					int temp = rand.Next(count);
//					//pattern[temp].CopyTo(pattern[i], 0);
//					pattern[i] = (Point[])pattern[temp].Clone();
//					int ch_ud = rand.Next(2);
//					int ch_lr = rand.Next(2);
//					int ch_all = rand.Next(2);
//					int xp, xm, yp, ym;
//					xp = xm = yp = ym = 1;
//
//					for (int j = 0; j < pattern[temp].Length; j++)
//					{
//						#region old
////						if (ch_ud != 0) 
////						{
////							pattern[i][j].Y++;
////							if (pattern[i][j].Y > 7) pattern[i][j].Y = 0;
////						}
////						else 
////						{
////							pattern[i][j].Y--;
////							if (pattern[i][j].Y < 0) pattern[i][j].Y = 7;
////						}
////
////						if (ch_lr != 0) 
////						{
////							pattern[i][j].X++;
////							if (pattern[i][j].X > 4) pattern[i][j].X = -3;
////						}
////						else 
////						{
////							pattern[i][j].X--;
////							if (pattern[i][j].X < -3) pattern[i][j].X = 4;
////						}
//						#endregion
//						switch (ch_all)
//						{
//							case 0:
//								pattern[i][j].Y += yp;
//								if (pattern[i][j].Y > 7) pattern[i][j].Y = 0 + (yp-1);
//								break;
//							case 1:
//								pattern[i][j].Y -= ym;
//								if (pattern[i][j].Y < 0) pattern[i][j].Y = 7 - (ym-1);
//								break;
//							default:
//								break;
//						}
//					}
//
//					switch (ch_all)
//					{
//						case 0:
//							yp++;
//							break;
//						case 1:
//							ym++;
//							break;
//						default:
//							break;
//					}
//				}
//				else
//				{
//					int pat_num = rand.Next(2, 6);
//					pattern[i] = new Point[pat_num];
//					for (int k = 0; k < pat_num; k++)
//					{
//						pattern[i][k].X = rand.Next(7) - 3;
//						pattern[i][k].Y = rand.Next(7);
//					}
//				}
//			}
//
//			//���l�p�^�[�������݂���@���@���S�Ƀ����_���ȃp�^�[���𐶐����ĕЕ���u��������
//			for (int i = 0; i < 10-1; i++)
//			{
//				for (int j = i+1; j < 10; j++)
//				{
//					if (pattern[i].Length == pattern[j].Length)
//					{
//						bool flag_k = true;
//						for (int k = 0; k < pattern[i].Length; k++)
//						{
//							bool flag_l = false;
//							for (int l = 0; l < pattern[j].Length; l++)
//							{
//								if (pattern[i][k].X == pattern[j][l].X && pattern[i][k].Y == pattern[j][l].Y)
//								{
//									flag_l = true;
//								}
//							}
//							if (!flag_l)
//							{
//								flag_k = false;
//								break;
//							}
//						}
//
//						if (flag_k)
//						{
//							int pat_num = rand.Next(2, 6);
//							pattern[j] = new Point[pat_num];
//							for (int k = 0; k < pat_num; k++)
//							{
//								pattern[j][k].X = rand.Next(7) - 3;
//								pattern[j][k].Y = rand.Next(7);
//							}
//						}
//					}
//				}
//			}
//
//			if (!muphic.Common.TutorialStatus.getIsTutorial()) //���בւ��B�������`���[�g���A�����s���̏ꍇ�A�I�����p�^�[�����Œ�
//			{
//				//�p�^�[���̕��בւ��@15�񂮂炢�Ł[
//				for (int i = 0; i < 15; i++)
//				{
//					int n = rand.Next(10);
//					int m = rand.Next(10);
//					Point[] pat_temp;
//					if (n != m)
//					{
//						pat_temp = (Point[])pattern[n].Clone();
//						pattern[n] = (Point[])pattern[m].Clone();
//						pattern[m] = (Point[])pat_temp.Clone();
//					}
//				}
//			}
//			else
//			{
//				//1�Ԃ�2�Ԃ̂ݓ���ւ��A��͕��u
//				Point[] pat_temp;
//				pat_temp = (Point[])pattern[0].Clone();
//				pattern[0] = (Point[])pattern[1].Clone();
//				pattern[1] = (Point[])pat_temp.Clone();
//			}
//
//			return true;
//
//		}
//
//		
//		/// <summary>
//		/// CSV�`����1�s�ǂݍ��ރ��\�b�h
//		/// </summary>
//		/// <returns></returns>
//		private String[] ReadLine()
//		{
//			// ��s�ǂݍ���
//			String s = sr.ReadLine();
//			
//			// �ǂݍ��܂Ȃ�������null��Ԃ�
//			if(s == null) return null;
//			
//			// ' '�ŋ�؂��ĕ�����z��ŕԂ�
//			return s.Split(new char[] {' '});
//		}
//	}
//}