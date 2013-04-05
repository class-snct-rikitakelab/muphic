using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace muphic.LinkMake.Dialog.Save
{
	/// <summary>
	/// SaveButton �̊T�v�̐����ł��B
	/// </summary>
	public class SaveButton : Base
	{
		public LinkSaveDialog parent;

		public SaveButton(LinkSaveDialog dialog)
		{
			this.parent = dialog;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �薼����Ȃ牽�����Ȃ�
			if (this.parent.titlename == null ) return;

			/////////////��������㏑���@�\�p����////////////////////
			bool flag = false;

			for (int i = 0; i < parent.parent.dfList.Index.Count; i++)
			{
				DataIndex di = (DataIndex)parent.parent.dfList.Index[i];
				if (di.Num < 10) continue;
				if (parent.titlename.Equals(di.Title))	//�㏑������
				{
					parent.parent.filenum = di.Num;
					parent.parent.title = di.Title;
					DataIndex di_new = new DataIndex();
					di_new.Num = di.Num;
					di_new.Title = di.Title;
					di_new.Level = parent.num;
					
					if (parent.parent.dfList.Index.Count-1 == i)
					{
						parent.parent.dfList.Index.RemoveAt(i);
						parent.parent.dfList.Index.Add(di_new);
						
						
					}
					else
					{
						parent.parent.dfList.Index.RemoveAt(i);
						parent.parent.dfList.Index.Insert(i, di_new);
						
						
					}
					flag = true;
					break;
				}

			}

			if (!flag)
			{
				//�㏑���Ȃ�
				{
					DataIndex di = (DataIndex)parent.parent.dfList.Index[parent.parent.dfList.Index.Count-1];
					parent.parent.filenum = di.Num+1;
					parent.parent.title = parent.titlename;
					
					DataIndex di_new = new DataIndex();
					di_new.Num = di.Num+1;
					di_new.Title = parent.titlename;
					di_new.Level = parent.num;

					if (di_new.Num < 10) di_new.Num = 10;

					parent.parent.dfList.Index.Add(di_new);
					
				}
			}

			////Index��������
			String filename = "LinkData\\QuesPatt.mdi";
			StreamWriter sw = null;
			try
			{
				System.Console.WriteLine("muphicMDI�t�@�C�� �֏����o��");
				// �������݃o�b�t�@�̐ݒ�
				sw = new StreamWriter(filename, false, Encoding.GetEncoding("Shift_JIS"));
			}
			catch(FileNotFoundException e)
			{
				// �t�@�C���I�[�v���Ɏ��s�����ꍇ �������ݎ��s
				MessageBox.Show("������������ق��������������I");
				//return false;
			}

			for (int i = 0; i < parent.parent.dfList.Index.Count; i++)
			{
				String buf = "";
				DataIndex diw = (DataIndex)parent.parent.dfList.Index[i];

				buf = diw.Num + " " + diw.Title + " " + diw.Level;
				sw.WriteLine(buf);
			}
			
			sw.Close();

			///////////////////////////////////////////////////

			// �t�@�C���������݃N���X���C���X�^���X�����Ă݂�
			LinkFileWriter sfw = new LinkFileWriter(this.parent.parent.score.Animals.AnimalList, parent.parent.tempo_l.TempoMode, parent.titlename, parent.parent.filenum, parent.num);//parent.level);
			
			// ���ۂɏ������� �������A�������݂Ɏ��s�����炻�̂܂�
			if (!sfw.Write()) return;

			// �߂�{�^�������������Ƃɂ��āA�_�C�A���O�����
			parent.back.Click(System.Drawing.Point.Empty);
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

	}
}
