using System;

namespace muphic.Titlemode
{
	/// <summary>
	/// HiraButton �̊T�v�̐����ł��B
	/// </summary>
	public class HiraButton : Base
	{
		public HiraScreen parent;
		public int num;
		public string ch;

		public HiraButton(HiraScreen parent,string c,int i)
		{
			this.parent = parent;
			ch = c;
			num = i;
		}

		private void del()
		{
			if((parent.parent.Text != null)&&(parent.parent.Text.Length > 0))
				parent.parent.Text = parent.parent.Text.Remove(parent.parent.Text.Length-1,1);
		}
		private int check()
		{
			if(this.ch.Equals("�J"))
			{
				//����
				//�΂Ђ炪��
				if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}//�΃J�^�J�i
				else if(parent.parent.Text.EndsWith("�J"))
				{
					this.del();
					parent.parent.Text += "�K";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�L"))
				{
					this.del();
					parent.parent.Text += "�M";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�N"))
				{
					this.del();
					parent.parent.Text += "�O";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�P"))
				{
					this.del();
					parent.parent.Text += "�Q";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�R"))
				{
					this.del();
					parent.parent.Text += "�S";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�T"))
				{
					this.del();
					parent.parent.Text += "�U";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�V"))
				{
					this.del();
					parent.parent.Text += "�W";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�X"))
				{
					this.del();
					parent.parent.Text += "�Y";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�Z"))
				{
					this.del();
					parent.parent.Text += "�[";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�\"))
				{
					this.del();
					parent.parent.Text += "�]";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�^"))
				{
					this.del();
					parent.parent.Text += "�_";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�`"))
				{
					this.del();
					parent.parent.Text += "�a";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�c"))
				{
					this.del();
					parent.parent.Text += "�d";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�e"))
				{
					this.del();
					parent.parent.Text += "�f";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�g"))
				{
					this.del();
					parent.parent.Text += "�h";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�n"))
				{
					this.del();
					parent.parent.Text += "�o";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�q"))
				{
					this.del();
					parent.parent.Text += "�r";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�t"))
				{
					this.del();
					parent.parent.Text += "�u";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�w"))
				{
					this.del();
					parent.parent.Text += "�x";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�z"))
				{
					this.del();
					parent.parent.Text += "�{";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�E"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else
				{
					return 1;
				}
			}
			if(this.ch.Equals("�K"))
			{
				//������
				//�΂Ђ炪��
				if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("��"))
				{
					this.del();
					parent.parent.Text += "��";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�n"))
				{
					this.del();
					parent.parent.Text += "�p";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�q"))
				{
					this.del();
					parent.parent.Text += "�s";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�t"))
				{
					this.del();
					parent.parent.Text += "�v";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�w"))
				{
					this.del();
					parent.parent.Text += "�y";
					return 1;
				}
				else if(parent.parent.Text.EndsWith("�z"))
				{
					this.del();
					parent.parent.Text += "�|";
					return 1;
				}
				else
				{
					return 1;
				}
			}
			return 0;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			int i = check();
			if(i == 0)
			{
				if ((parent.parent.Text == null) || (parent.parent.Text.Length < parent.parent.maxlength))
					parent.parent.Text += ch;
			}
			System.Diagnostics.Debug.WriteLine(true,parent.parent.Text);
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}

		public override string ToString()
		{
			return "HiraButton" + this.num;					//�͋Z
		}
	}
}
