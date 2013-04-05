using System;
using System.Collections;
using muphic.LinkMake.Dialog.Save;

namespace muphic.LinkMake.Dialog.Save
{
	/// <summary>
	/// ScoreSaveDialog �̊T�v�̐����ł��B
	/// </summary>
	public class LinkSaveDialog : Screen
	{
		public LinkMakeScreen parent;

		public SaveButton save;
		public BackButton back;
		public CharForm form;
		public TitleButton title;
		public Screen Screen;
		public int level;
		public int num;

		public string titlename;	// �薼

		public LinkSaveDialog(LinkMakeScreen link)
		{
			this.parent = link;
			//this.SetTitleName(null);

			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////

			save = new SaveButton(this);
			back = new BackButton(this);
			title = new TitleButton(this);
			form = new CharForm(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251, "image\\link\\dialog\\save\\dialog_save_bak.png");
			muphic.DrawManager.Regist(save.ToString(), 671, 393, "image\\link\\dialog\\save\\nokosu_off.png", "image\\link\\dialog\\save\\nokosu_on.png");
			muphic.DrawManager.Regist(back.ToString(), 671, 451, "image\\link\\dialog\\back_off.png", "image\\link\\dialog\\back_on.png");
			muphic.DrawManager.Regist(title.ToString(), 338, 402, "image\\link\\dialog\\save\\daimei_off.png", "image\\link\\dialog\\save\\daimei_on.png");
			muphic.DrawManager.Regist(form.ToString(), 342, 456, "image\\link\\dialog\\save\\charform.png");
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(save);
			BaseList.Add(back);
			BaseList.Add(title);
			BaseList.Add(form);

			num = ScoreCalc();

			if (num <= 10) level = 1;
			else if (num <= 20) level = 2;
			else if (num <= 30) level = 3;
			else if (num <= 40) level = 4;
			else level = 5;

			titlename = parent.title;
		}
		
		
		/// <summary>
		/// �薼��^����ꂽ������ɏ��������郁�\�b�h
		/// </summary>
		/// <param name="name"></param>
		public void SetTitleName(string name)
		{
//			this.titlename = name;
//			this.parent.title = name;
		}
		
		public override void Draw()
		{
			if (Screen == null)
			{
				base.Draw ();
			
				// �薼�̕`��
				muphic.DrawManager.DrawString(this.titlename, 360, 470);
				// ��{���x���\���H(Debug)
				//muphic.DrawManager.DrawString("Lv."+level, 360, 440);
			}
			else
			{
				Screen.Draw();
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			if (Screen == null)
			{
				base.MouseMove(p);
			}
			else
			{
				Screen.MouseMove(p);
			}
		}

		public override void Click(System.Drawing.Point p)
		{
			if (Screen == null)
			{
				base.Click(p);
			}
			else
			{
				Screen.Click(p);
			}

		}
		

		/// <summary>
		/// ��{���x���v�Z
		/// </summary>
		private int ScoreCalc()
		{
			Animals animals = parent.score.Animals;
			ArrayList buf = new ArrayList();
			String str = "";
			int maxplace = 0;
			
			//�ő�l�擾
			for(int i=0; i<animals.AnimalList.Count; i++)
			{
				Animal a = (Animal)animals[i];
				if (maxplace < a.place)
				{
					maxplace = a.place;
				}
			}

			int temp = 8;
			int count = 0;

			for (int i = 0; i <= maxplace/8; i++)
			{
				for (int j = 0; j < animals.AnimalList.Count; j++)
				{
					Animal a = (Animal)animals[j];
					if (a.place < temp && a.place > temp-9)
					{
						str = str + (a.place%8) + (a.code-1);
					}
				}
				temp += 8;
				buf.Add(str);
				str = "";
				count++;
			}

			/////////////////
			int score = 0;
			
			//�Ȃ��鏬�ߐ�
			//score += buf.Count;
			switch (buf.Count)
			{
				case 1:
					score += 7;
					break;
				case 2:
					score += 10;
					break;
				case 3:
					score += 20;
					break;
				case 4:
					score += 24;
					break;
				case 5:
					score += 30;
					break;
				case 6:
					score += 34;
					break;
				case 7:
					score += 36;
					break;
				case 8:
					score += 38;
					break;
				case 9:
					score += 40;
					break;
				case 10:
					score += 40;
					break;
				default:
					break;
			}


			//���l�̏��߂�����ꍇ���_
			for (int i = 0; i < buf.Count - 1; i++)
			{
				for (int j = i; j < buf.Count; j++)
				{
					if (((String)buf[i]).Equals((String)buf[j]))
					{
						score -= 2;
					}
				}
			}
		
			//���߂�����̕��ϓ�����
			int aniCount = 0;
			for (int i = 0; i < buf.Count; i++)
			{
				aniCount += ((String)buf[i]).Length/2;
			}
			score += (int)((aniCount/buf.Count)*1.5);
		

			//-----------��������d�v------------
			//�ގ��p�^�[���ł̍ՓT�@����Ȃ��̓_
			String[] result = new String[buf.Count];
			
			for (int i = 0; i < buf.Count; i++)
			{
				String tmp = (String)buf[i];
				int before = 0;
				if (!tmp.Equals("")) before = Int32.Parse(tmp.Substring(1, 1));

				result[i] = "";

				for (int j = 1; j < tmp.Length/2; j++)
				{
					int now = Int32.Parse(tmp.Substring(j*2+1, 1));
					
					if (now < before) result[i] += "p";
					else if (now == before) result[i] += "n";
					else result[i] += "m";

					before = now;
				}
			}

			int compCount = 0;
			for (int i = 0; i < buf.Count-1; i++)
			{
				for (int j = i+1; j < buf.Count; j++)
				{
					if (result[i].Equals(result[j]))
					{
						score += 2;
						compCount++;
					}
				}
			}

			if (compCount < 3) score = score - 3*compCount - 5;


			//���x���͓��o�����_����Ԃ��Ă݂�e�X�g
//			//���x�����o
//			if (score <= 5)
//			{
//				score = 1;
//			}
//			else if (score <= 11)
//			{
//				score = 2;
//			}
//			else if (score <= 17)
//			{
//				score = 3;
//			}
//			else if (score <= 23)
//			{
//				score = 4;
//			}
//			else
//			{
//				score = 5;
//			}

			return score;
		}
	}
}
