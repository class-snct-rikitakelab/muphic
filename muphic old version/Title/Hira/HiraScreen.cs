using System;
using muphic;
using muphic.Titlemode;

namespace muphic.Titlemode
{
	public class HiraScreen : CharScreen
	{
		public TitleScreen parent;
		HiraButton[] hira;

		public HiraScreen(TitleScreen parent)
		{
			this.parent = parent;
			hira = new HiraButton[59];

			hira[0] = new HiraButton(this, "��", 0);
			hira[1] = new HiraButton(this, "��", 1);
			hira[2] = new HiraButton(this, "��", 2);
			hira[3] = new HiraButton(this, "��", 3);
			hira[4] = new HiraButton(this, "��", 4);

			hira[5] = new HiraButton(this, "��", 5);
			hira[6] = new HiraButton(this, "��", 6);
			hira[7] = new HiraButton(this, "��", 7);
			hira[8] = new HiraButton(this, "��", 8);
			hira[9] = new HiraButton(this, "��", 9);
			
			hira[10] = new HiraButton(this, "��", 10);
			hira[11] = new HiraButton(this, "��", 11);
			hira[12] = new HiraButton(this, "��", 12);
			hira[13] = new HiraButton(this, "��", 13);
			hira[14] = new HiraButton(this, "��", 14);
			
			hira[15] = new HiraButton(this, "��", 15);
			hira[16] = new HiraButton(this, "��", 16);
			hira[17] = new HiraButton(this, "��", 17);
			hira[18] = new HiraButton(this, "��", 18);
			hira[19] = new HiraButton(this, "��", 19);

			hira[20] = new HiraButton(this, "��", 20);
			hira[21] = new HiraButton(this, "��", 21);
			hira[22] = new HiraButton(this, "��", 22);
			hira[23] = new HiraButton(this, "��", 23);
			hira[24] = new HiraButton(this, "��", 24);

			hira[25] = new HiraButton(this, "��", 25);
			hira[26] = new HiraButton(this, "��", 26);
			hira[27] = new HiraButton(this, "��", 27);
			hira[28] = new HiraButton(this, "��", 28);
			hira[29] = new HiraButton(this, "��", 29);

			hira[30] = new HiraButton(this, "��", 30);
			hira[31] = new HiraButton(this, "��", 31);
			hira[32] = new HiraButton(this, "��", 32);
			hira[33] = new HiraButton(this, "��", 33);
			hira[34] = new HiraButton(this, "��", 34);

			hira[35] = new HiraButton(this, "��", 35);
			hira[36] = new HiraButton(this, "��", 36);
			hira[37] = new HiraButton(this, "��", 37);

			hira[38] = new HiraButton(this, "��", 38);
			hira[39] = new HiraButton(this, "��", 39);
			hira[40] = new HiraButton(this, "��", 40);
			hira[41] = new HiraButton(this, "��", 41);
			hira[42] = new HiraButton(this, "��", 42);

			hira[43] = new HiraButton(this, "��", 43);
			hira[44] = new HiraButton(this, "��", 44);
			hira[45] = new HiraButton(this, "��", 45);

			hira[46] = new HiraButton(this, "��", 46);
			hira[47] = new HiraButton(this, "��", 47);
			hira[48] = new HiraButton(this, "��", 48);
			hira[49] = new HiraButton(this, "��", 49);
			hira[50] = new HiraButton(this, "��", 50);

			hira[51] = new HiraButton(this, "��", 51);
			hira[52] = new HiraButton(this, "��", 52);
			hira[53] = new HiraButton(this, "��", 53);

			hira[54] = new HiraButton(this, "��", 54);

			hira[55] = new HiraButton(this, "��", 55);

			hira[56] = new HiraButton(this, "�J", 56);
			hira[57] = new HiraButton(this, "�K", 57);
			hira[58] = new HiraButton(this, "�[", 58);

			int Xplace = 125; int Xspace = 61; int Yplace = 223; int Yspace = 54;
			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			//���s
			muphic.DrawManager.Regist(hira[0].ToString(), Xplace+Xspace*12, Yplace+Yspace*0, "image\\Title\\Hira\\a_off.png", "image\\Title\\Hira\\a_on.png");
			muphic.DrawManager.Regist(hira[1].ToString(), Xplace+Xspace*12, Yplace+Yspace*1, "image\\Title\\Hira\\i_off.png", "image\\Title\\Hira\\i_on.png");
			muphic.DrawManager.Regist(hira[2].ToString(), Xplace+Xspace*12, Yplace+Yspace*2, "image\\Title\\Hira\\u_off.png", "image\\Title\\Hira\\u_on.png");
			muphic.DrawManager.Regist(hira[3].ToString(), Xplace+Xspace*12, Yplace+Yspace*3, "image\\Title\\Hira\\e_off.png", "image\\Title\\Hira\\e_on.png");
			muphic.DrawManager.Regist(hira[4].ToString(), Xplace+Xspace*12, Yplace+Yspace*4, "image\\Title\\Hira\\o_off.png", "image\\Title\\Hira\\o_on.png");
			//���s
			muphic.DrawManager.Regist(hira[5].ToString(), Xplace+Xspace*11, Yplace+Yspace*0, "image\\Title\\Hira\\ka_off.png", "image\\Title\\Hira\\ka_on.png");
			muphic.DrawManager.Regist(hira[6].ToString(), Xplace+Xspace*11, Yplace+Yspace*1, "image\\Title\\Hira\\ki_off.png", "image\\Title\\Hira\\ki_on.png");
			muphic.DrawManager.Regist(hira[7].ToString(), Xplace+Xspace*11, Yplace+Yspace*2, "image\\Title\\Hira\\ku_off.png", "image\\Title\\Hira\\ku_on.png");
			muphic.DrawManager.Regist(hira[8].ToString(), Xplace+Xspace*11, Yplace+Yspace*3, "image\\Title\\Hira\\ke_off.png", "image\\Title\\Hira\\ke_on.png");
			muphic.DrawManager.Regist(hira[9].ToString(), Xplace+Xspace*11, Yplace+Yspace*4, "image\\Title\\Hira\\ko_off.png", "image\\Title\\Hira\\ko_on.png");
			//���s
			muphic.DrawManager.Regist(hira[10].ToString(), Xplace+Xspace*10, Yplace+Yspace*0, "image\\Title\\Hira\\sa_off.png", "image\\Title\\Hira\\sa_on.png");
			muphic.DrawManager.Regist(hira[11].ToString(), Xplace+Xspace*10, Yplace+Yspace*1, "image\\Title\\Hira\\si_off.png", "image\\Title\\Hira\\si_on.png");
			muphic.DrawManager.Regist(hira[12].ToString(), Xplace+Xspace*10, Yplace+Yspace*2, "image\\Title\\Hira\\su_off.png", "image\\Title\\Hira\\su_on.png");
			muphic.DrawManager.Regist(hira[13].ToString(), Xplace+Xspace*10, Yplace+Yspace*3, "image\\Title\\Hira\\se_off.png", "image\\Title\\Hira\\se_on.png");
			muphic.DrawManager.Regist(hira[14].ToString(), Xplace+Xspace*10, Yplace+Yspace*4, "image\\Title\\Hira\\so_off.png", "image\\Title\\Hira\\so_on.png");
			//���s
			muphic.DrawManager.Regist(hira[15].ToString(), Xplace+Xspace*9, Yplace+Yspace*0, "image\\Title\\Hira\\ta_off.png", "image\\Title\\Hira\\ta_on.png");
			muphic.DrawManager.Regist(hira[16].ToString(), Xplace+Xspace*9, Yplace+Yspace*1, "image\\Title\\Hira\\ti_off.png", "image\\Title\\Hira\\ti_on.png");
			muphic.DrawManager.Regist(hira[17].ToString(), Xplace+Xspace*9, Yplace+Yspace*2, "image\\Title\\Hira\\tu_off.png", "image\\Title\\Hira\\tu_on.png");
			muphic.DrawManager.Regist(hira[18].ToString(), Xplace+Xspace*9, Yplace+Yspace*3, "image\\Title\\Hira\\te_off.png", "image\\Title\\Hira\\te_on.png");
			muphic.DrawManager.Regist(hira[19].ToString(), Xplace+Xspace*9, Yplace+Yspace*4, "image\\Title\\Hira\\to_off.png", "image\\Title\\Hira\\to_on.png");
			//�ȍs
			muphic.DrawManager.Regist(hira[20].ToString(), Xplace+Xspace*8, Yplace+Yspace*0, "image\\Title\\Hira\\na_off.png", "image\\Title\\Hira\\na_on.png");
			muphic.DrawManager.Regist(hira[21].ToString(), Xplace+Xspace*8, Yplace+Yspace*1, "image\\Title\\Hira\\ni_off.png", "image\\Title\\Hira\\ni_on.png");
			muphic.DrawManager.Regist(hira[22].ToString(), Xplace+Xspace*8, Yplace+Yspace*2, "image\\Title\\Hira\\nu_off.png", "image\\Title\\Hira\\nu_on.png");
			muphic.DrawManager.Regist(hira[23].ToString(), Xplace+Xspace*8, Yplace+Yspace*3, "image\\Title\\Hira\\ne_off.png", "image\\Title\\Hira\\ne_on.png");
			muphic.DrawManager.Regist(hira[24].ToString(), Xplace+Xspace*8, Yplace+Yspace*4, "image\\Title\\Hira\\no_off.png", "image\\Title\\Hira\\no_on.png");
			//�͍s
			muphic.DrawManager.Regist(hira[25].ToString(), Xplace+Xspace*7, Yplace+Yspace*0, "image\\Title\\Hira\\ha_off.png", "image\\Title\\Hira\\ha_on.png");
			muphic.DrawManager.Regist(hira[26].ToString(), Xplace+Xspace*7, Yplace+Yspace*1, "image\\Title\\Hira\\hi_off.png", "image\\Title\\Hira\\hi_on.png");
			muphic.DrawManager.Regist(hira[27].ToString(), Xplace+Xspace*7, Yplace+Yspace*2, "image\\Title\\Hira\\hu_off.png", "image\\Title\\Hira\\hu_on.png");
			muphic.DrawManager.Regist(hira[28].ToString(), Xplace+Xspace*7, Yplace+Yspace*3, "image\\Title\\Hira\\he_off.png", "image\\Title\\Hira\\he_on.png");
			muphic.DrawManager.Regist(hira[29].ToString(), Xplace+Xspace*7, Yplace+Yspace*4, "image\\Title\\Hira\\ho_off.png", "image\\Title\\Hira\\ho_on.png");
			//�܍s
			muphic.DrawManager.Regist(hira[30].ToString(), Xplace+Xspace*6, Yplace+Yspace*0, "image\\Title\\Hira\\ma_off.png", "image\\Title\\Hira\\ma_on.png");
			muphic.DrawManager.Regist(hira[31].ToString(), Xplace+Xspace*6, Yplace+Yspace*1, "image\\Title\\Hira\\mi_off.png", "image\\Title\\Hira\\mi_on.png");
			muphic.DrawManager.Regist(hira[32].ToString(), Xplace+Xspace*6, Yplace+Yspace*2, "image\\Title\\Hira\\mu_off.png", "image\\Title\\Hira\\mu_on.png");
			muphic.DrawManager.Regist(hira[33].ToString(), Xplace+Xspace*6, Yplace+Yspace*3, "image\\Title\\Hira\\me_off.png", "image\\Title\\Hira\\me_on.png");
			muphic.DrawManager.Regist(hira[34].ToString(), Xplace+Xspace*6, Yplace+Yspace*4, "image\\Title\\Hira\\mo_off.png", "image\\Title\\Hira\\mo_on.png");
			//��s
			muphic.DrawManager.Regist(hira[35].ToString(), Xplace+Xspace*5, Yplace+Yspace*0, "image\\Title\\Hira\\ya_off.png", "image\\Title\\Hira\\ya_on.png");
			muphic.DrawManager.Regist(hira[36].ToString(), Xplace+Xspace*5, Yplace+Yspace*2, "image\\Title\\Hira\\yu_off.png", "image\\Title\\Hira\\yu_on.png");
			muphic.DrawManager.Regist(hira[37].ToString(), Xplace+Xspace*5, Yplace+Yspace*4, "image\\Title\\Hira\\yo_off.png", "image\\Title\\Hira\\yo_on.png");
			//��s
			muphic.DrawManager.Regist(hira[38].ToString(), Xplace+Xspace*4, Yplace+Yspace*0, "image\\Title\\Hira\\ra_off.png", "image\\Title\\Hira\\ra_on.png");
			muphic.DrawManager.Regist(hira[39].ToString(), Xplace+Xspace*4, Yplace+Yspace*1, "image\\Title\\Hira\\ri_off.png", "image\\Title\\Hira\\ri_on.png");
			muphic.DrawManager.Regist(hira[40].ToString(), Xplace+Xspace*4, Yplace+Yspace*2, "image\\Title\\Hira\\ru_off.png", "image\\Title\\Hira\\ru_on.png");
			muphic.DrawManager.Regist(hira[41].ToString(), Xplace+Xspace*4, Yplace+Yspace*3, "image\\Title\\Hira\\re_off.png", "image\\Title\\Hira\\re_on.png");
			muphic.DrawManager.Regist(hira[42].ToString(), Xplace+Xspace*4, Yplace+Yspace*4, "image\\Title\\Hira\\ro_off.png", "image\\Title\\Hira\\ro_on.png");
			//�����
			muphic.DrawManager.Regist(hira[43].ToString(), Xplace+Xspace*3, Yplace+Yspace*0, "image\\Title\\Hira\\wa_off.png", "image\\Title\\Hira\\wa_on.png");
			muphic.DrawManager.Regist(hira[44].ToString(), Xplace+Xspace*3, Yplace+Yspace*2, "image\\Title\\Hira\\wo_off.png", "image\\Title\\Hira\\wo_on.png");
			muphic.DrawManager.Regist(hira[45].ToString(), Xplace+Xspace*3, Yplace+Yspace*4, "image\\Title\\Hira\\n_off.png", "image\\Title\\Hira\\n_on.png");
			//���s
			muphic.DrawManager.Regist(hira[46].ToString(), Xplace+Xspace*2, Yplace+Yspace*0, "image\\Title\\Hira\\xa_off.png", "image\\Title\\Hira\\xa_on.png");
			muphic.DrawManager.Regist(hira[47].ToString(), Xplace+Xspace*2, Yplace+Yspace*1, "image\\Title\\Hira\\xi_off.png", "image\\Title\\Hira\\xi_on.png");
			muphic.DrawManager.Regist(hira[48].ToString(), Xplace+Xspace*2, Yplace+Yspace*2, "image\\Title\\Hira\\xu_off.png", "image\\Title\\Hira\\xu_on.png");
			muphic.DrawManager.Regist(hira[49].ToString(), Xplace+Xspace*2, Yplace+Yspace*3, "image\\Title\\Hira\\xe_off.png", "image\\Title\\Hira\\xe_on.png");
			muphic.DrawManager.Regist(hira[50].ToString(), Xplace+Xspace*2, Yplace+Yspace*4, "image\\Title\\Hira\\xo_off.png", "image\\Title\\Hira\\xo_on.png");
			//��s
			muphic.DrawManager.Regist(hira[51].ToString(), Xplace+Xspace*1, Yplace+Yspace*0, "image\\Title\\Hira\\xya_off.png", "image\\Title\\Hira\\xya_on.png");
			muphic.DrawManager.Regist(hira[52].ToString(), Xplace+Xspace*1, Yplace+Yspace*2, "image\\Title\\Hira\\xyu_off.png", "image\\Title\\Hira\\xyu_on.png");
			muphic.DrawManager.Regist(hira[53].ToString(), Xplace+Xspace*1, Yplace+Yspace*4, "image\\Title\\Hira\\xyo_off.png", "image\\Title\\Hira\\xyo_on.png");
			//����J�K�[
			muphic.DrawManager.Regist(hira[54].ToString(), Xplace+Xspace*0, Yplace+Yspace*0, "image\\Title\\Hira\\xtu_off.png", "image\\Title\\Hira\\xtu_on.png");
			muphic.DrawManager.Regist(hira[55].ToString(), Xplace+Xspace*0, Yplace+Yspace*1, "image\\Title\\Hira\\xwa_off.png", "image\\Title\\Hira\\xwa_on.png");
			muphic.DrawManager.Regist(hira[56].ToString(), Xplace+Xspace*0, Yplace+Yspace*2, "image\\Title\\Hira\\dakuten_off.png", "image\\Title\\Hira\\dakuten_on.png");
			muphic.DrawManager.Regist(hira[57].ToString(), Xplace+Xspace*0, Yplace+Yspace*3, "image\\Title\\Hira\\handakuten_off.png", "image\\Title\\Hira\\handakuten_on.png");
			muphic.DrawManager.Regist(hira[58].ToString(), Xplace+Xspace*0, Yplace+Yspace*4, "image\\Title\\Hira\\-_off.png", "image\\Title\\Hira\\-_on.png");
			

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			for(int i = 0;i < 59;i++)
				BaseList.Add(hira[i]);

		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}

		public override void Draw()
		{
			base.Draw ();
		}

	}
}