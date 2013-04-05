using System;
using muphic;
using muphic.Titlemode;

namespace muphic.Titlemode
{
	public class AlphaScreen : CharScreen
	{
		public TitleScreen parent;
        AlphaButton[] alpha;

		public AlphaScreen(TitleScreen parent)
		{
			this.parent = parent;
			alpha = new AlphaButton[52];

			alpha[0] = new AlphaButton(this, "�`", 0);
			alpha[1] = new AlphaButton(this, "�a", 1);
			alpha[2] = new AlphaButton(this, "�b", 2);
			alpha[3] = new AlphaButton(this, "�c", 3);
			alpha[4] = new AlphaButton(this, "�d", 4);
			alpha[5] = new AlphaButton(this, "�e", 5);
			alpha[6] = new AlphaButton(this, "�f", 6);
			alpha[7] = new AlphaButton(this, "�g", 7);
			alpha[8] = new AlphaButton(this, "�h", 8);
			alpha[9] = new AlphaButton(this, "�i", 9);
			alpha[10] = new AlphaButton(this, "�j", 10);
			alpha[11] = new AlphaButton(this, "�k", 11);
			alpha[12] = new AlphaButton(this, "�l", 12);
			alpha[13] = new AlphaButton(this, "�m", 13);
			alpha[14] = new AlphaButton(this, "�n", 14);
			alpha[15] = new AlphaButton(this, "�o", 15);
			alpha[16] = new AlphaButton(this, "�p", 16);
			alpha[17] = new AlphaButton(this, "�q", 17);
			alpha[18] = new AlphaButton(this, "�r", 18);
			alpha[19] = new AlphaButton(this, "�s", 19);
			alpha[20] = new AlphaButton(this, "�t", 20);
			alpha[21] = new AlphaButton(this, "�u", 21);
			alpha[22] = new AlphaButton(this, "�v", 22);
			alpha[23] = new AlphaButton(this, "�w", 23);
			alpha[24] = new AlphaButton(this, "�x", 24);
			alpha[25] = new AlphaButton(this, "�y", 25);

			alpha[26] = new AlphaButton(this, "��", 26);
			alpha[27] = new AlphaButton(this, "��", 27);
			alpha[28] = new AlphaButton(this, "��", 28);
			alpha[29] = new AlphaButton(this, "��", 29);
			alpha[30] = new AlphaButton(this, "��", 30);
			alpha[31] = new AlphaButton(this, "��", 31);
			alpha[32] = new AlphaButton(this, "��", 32);
			alpha[33] = new AlphaButton(this, "��", 33);
			alpha[34] = new AlphaButton(this, "��", 34);
			alpha[35] = new AlphaButton(this, "��", 35);
			alpha[36] = new AlphaButton(this, "��", 36);
			alpha[37] = new AlphaButton(this, "��", 37);
			alpha[38] = new AlphaButton(this, "��", 38);
			alpha[39] = new AlphaButton(this, "��", 39);
			alpha[40] = new AlphaButton(this, "��", 40);
			alpha[41] = new AlphaButton(this, "��", 41);
			alpha[42] = new AlphaButton(this, "��", 42);
			alpha[43] = new AlphaButton(this, "��", 43);
			alpha[44] = new AlphaButton(this, "��", 44);
			alpha[45] = new AlphaButton(this, "��", 45);
			alpha[46] = new AlphaButton(this, "��", 46);
			alpha[47] = new AlphaButton(this, "��", 47);
			alpha[48] = new AlphaButton(this, "��", 48);
			alpha[49] = new AlphaButton(this, "��", 49);
			alpha[50] = new AlphaButton(this, "��", 50);
			alpha[51] = new AlphaButton(this, "��", 51);

            int Xplace = 212; int Xspace = 61; int Yplace = 224; int Yspace = 54;
			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(alpha[0].ToString(), Xplace+Xspace*0, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\A_off.png", "image\\Title\\Alpha\\AlphaLarge\\A_on.png");
			muphic.DrawManager.Regist(alpha[1].ToString(), Xplace+Xspace*1, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\B_off.png", "image\\Title\\Alpha\\AlphaLarge\\B_on.png");
			muphic.DrawManager.Regist(alpha[2].ToString(), Xplace+Xspace*2, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\C_off.png", "image\\Title\\Alpha\\AlphaLarge\\C_on.png");
			muphic.DrawManager.Regist(alpha[3].ToString(), Xplace+Xspace*3, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\D_off.png", "image\\Title\\Alpha\\AlphaLarge\\D_on.png");
			muphic.DrawManager.Regist(alpha[4].ToString(), Xplace+Xspace*4, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\E_off.png", "image\\Title\\Alpha\\AlphaLarge\\E_on.png");
			muphic.DrawManager.Regist(alpha[5].ToString(), Xplace+Xspace*5, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\F_off.png", "image\\Title\\Alpha\\AlphaLarge\\F_on.png");
			muphic.DrawManager.Regist(alpha[6].ToString(), Xplace+Xspace*6, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\G_off.png", "image\\Title\\Alpha\\AlphaLarge\\G_on.png");
			muphic.DrawManager.Regist(alpha[7].ToString(), Xplace+Xspace*7, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\H_off.png", "image\\Title\\Alpha\\AlphaLarge\\H_on.png");
			muphic.DrawManager.Regist(alpha[8].ToString(), Xplace+Xspace*8, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\I_off.png", "image\\Title\\Alpha\\AlphaLarge\\I_on.png");
			muphic.DrawManager.Regist(alpha[9].ToString(), Xplace+Xspace*9, Yplace+Yspace*0, "image\\Title\\Alpha\\AlphaLarge\\J_off.png", "image\\Title\\Alpha\\AlphaLarge\\J_on.png");

			muphic.DrawManager.Regist(alpha[10].ToString(), Xplace+Xspace*0, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\K_off.png", "image\\Title\\Alpha\\AlphaLarge\\K_on.png");
			muphic.DrawManager.Regist(alpha[11].ToString(), Xplace+Xspace*1, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\L_off.png", "image\\Title\\Alpha\\AlphaLarge\\L_on.png");
			muphic.DrawManager.Regist(alpha[12].ToString(), Xplace+Xspace*2, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\M_off.png", "image\\Title\\Alpha\\AlphaLarge\\M_on.png");
			muphic.DrawManager.Regist(alpha[13].ToString(), Xplace+Xspace*3, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\N_off.png", "image\\Title\\Alpha\\AlphaLarge\\N_on.png");
			muphic.DrawManager.Regist(alpha[14].ToString(), Xplace+Xspace*4, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\O_off.png", "image\\Title\\Alpha\\AlphaLarge\\O_on.png");
			muphic.DrawManager.Regist(alpha[15].ToString(), Xplace+Xspace*5, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\P_off.png", "image\\Title\\Alpha\\AlphaLarge\\P_on.png");
			muphic.DrawManager.Regist(alpha[16].ToString(), Xplace+Xspace*6, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\Q_off.png", "image\\Title\\Alpha\\AlphaLarge\\Q_on.png");
			muphic.DrawManager.Regist(alpha[17].ToString(), Xplace+Xspace*7, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\R_off.png", "image\\Title\\Alpha\\AlphaLarge\\R_on.png");
			muphic.DrawManager.Regist(alpha[18].ToString(), Xplace+Xspace*8, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\S_off.png", "image\\Title\\Alpha\\AlphaLarge\\S_on.png");
			muphic.DrawManager.Regist(alpha[19].ToString(), Xplace+Xspace*9, Yplace+Yspace*1, "image\\Title\\Alpha\\AlphaLarge\\T_off.png", "image\\Title\\Alpha\\AlphaLarge\\T_on.png");

			muphic.DrawManager.Regist(alpha[20].ToString(), Xplace+Xspace*0, Yplace+Yspace*2, "image\\Title\\Alpha\\AlphaLarge\\U_off.png", "image\\Title\\Alpha\\AlphaLarge\\U_on.png");
			muphic.DrawManager.Regist(alpha[21].ToString(), Xplace+Xspace*1, Yplace+Yspace*2, "image\\Title\\Alpha\\AlphaLarge\\V_off.png", "image\\Title\\Alpha\\AlphaLarge\\V_on.png");
			muphic.DrawManager.Regist(alpha[22].ToString(), Xplace+Xspace*2, Yplace+Yspace*2, "image\\Title\\Alpha\\AlphaLarge\\W_off.png", "image\\Title\\Alpha\\AlphaLarge\\W_on.png");
			muphic.DrawManager.Regist(alpha[23].ToString(), Xplace+Xspace*3, Yplace+Yspace*2, "image\\Title\\Alpha\\AlphaLarge\\X_off.png", "image\\Title\\Alpha\\AlphaLarge\\X_on.png");
			muphic.DrawManager.Regist(alpha[24].ToString(), Xplace+Xspace*4, Yplace+Yspace*2, "image\\Title\\Alpha\\AlphaLarge\\Y_off.png", "image\\Title\\Alpha\\AlphaLarge\\Y_on.png");
			muphic.DrawManager.Regist(alpha[25].ToString(), Xplace+Xspace*5, Yplace+Yspace*2, "image\\Title\\Alpha\\AlphaLarge\\Z_off.png", "image\\Title\\Alpha\\AlphaLarge\\Z_on.png");

			muphic.DrawManager.Regist(alpha[26].ToString(), Xplace+Xspace*0, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\A_off.png", "image\\Title\\Alpha\\AlphaSmall\\A_on.png");
			muphic.DrawManager.Regist(alpha[27].ToString(), Xplace+Xspace*1, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\B_off.png", "image\\Title\\Alpha\\AlphaSmall\\B_on.png");
			muphic.DrawManager.Regist(alpha[28].ToString(), Xplace+Xspace*2, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\C_off.png", "image\\Title\\Alpha\\AlphaSmall\\C_on.png");
			muphic.DrawManager.Regist(alpha[29].ToString(), Xplace+Xspace*3, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\D_off.png", "image\\Title\\Alpha\\AlphaSmall\\D_on.png");
			muphic.DrawManager.Regist(alpha[30].ToString(), Xplace+Xspace*4, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\E_off.png", "image\\Title\\Alpha\\AlphaSmall\\E_on.png");
			muphic.DrawManager.Regist(alpha[31].ToString(), Xplace+Xspace*5, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\F_off.png", "image\\Title\\Alpha\\AlphaSmall\\F_on.png");
			muphic.DrawManager.Regist(alpha[32].ToString(), Xplace+Xspace*6, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\G_off.png", "image\\Title\\Alpha\\AlphaSmall\\G_on.png");
			muphic.DrawManager.Regist(alpha[33].ToString(), Xplace+Xspace*7, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\H_off.png", "image\\Title\\Alpha\\AlphaSmall\\H_on.png");
			muphic.DrawManager.Regist(alpha[34].ToString(), Xplace+Xspace*8, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\I_off.png", "image\\Title\\Alpha\\AlphaSmall\\I_on.png");
			muphic.DrawManager.Regist(alpha[35].ToString(), Xplace+Xspace*9, Yplace+Yspace*3, "image\\Title\\Alpha\\AlphaSmall\\J_off.png", "image\\Title\\Alpha\\AlphaSmall\\J_on.png");

			muphic.DrawManager.Regist(alpha[36].ToString(), Xplace+Xspace*0, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\K_off.png", "image\\Title\\Alpha\\AlphaSmall\\K_on.png");
			muphic.DrawManager.Regist(alpha[37].ToString(), Xplace+Xspace*1, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\L_off.png", "image\\Title\\Alpha\\AlphaSmall\\L_on.png");
			muphic.DrawManager.Regist(alpha[38].ToString(), Xplace+Xspace*2, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\M_off.png", "image\\Title\\Alpha\\AlphaSmall\\M_on.png");
			muphic.DrawManager.Regist(alpha[39].ToString(), Xplace+Xspace*3, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\N_off.png", "image\\Title\\Alpha\\AlphaSmall\\N_on.png");
			muphic.DrawManager.Regist(alpha[40].ToString(), Xplace+Xspace*4, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\O_off.png", "image\\Title\\Alpha\\AlphaSmall\\O_on.png");
			muphic.DrawManager.Regist(alpha[41].ToString(), Xplace+Xspace*5, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\P_off.png", "image\\Title\\Alpha\\AlphaSmall\\P_on.png");
			muphic.DrawManager.Regist(alpha[42].ToString(), Xplace+Xspace*6, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\Q_off.png", "image\\Title\\Alpha\\AlphaSmall\\Q_on.png");
			muphic.DrawManager.Regist(alpha[43].ToString(), Xplace+Xspace*7, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\R_off.png", "image\\Title\\Alpha\\AlphaSmall\\R_on.png");
			muphic.DrawManager.Regist(alpha[44].ToString(), Xplace+Xspace*8, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\S_off.png", "image\\Title\\Alpha\\AlphaSmall\\S_on.png");
			muphic.DrawManager.Regist(alpha[45].ToString(), Xplace+Xspace*9, Yplace+Yspace*4, "image\\Title\\Alpha\\AlphaSmall\\T_off.png", "image\\Title\\Alpha\\AlphaSmall\\T_on.png");

			muphic.DrawManager.Regist(alpha[46].ToString(), Xplace+Xspace*0, Yplace+Yspace*5, "image\\Title\\Alpha\\AlphaSmall\\U_off.png", "image\\Title\\Alpha\\AlphaSmall\\U_on.png");
			muphic.DrawManager.Regist(alpha[47].ToString(), Xplace+Xspace*1, Yplace+Yspace*5, "image\\Title\\Alpha\\AlphaSmall\\V_off.png", "image\\Title\\Alpha\\AlphaSmall\\V_on.png");
			muphic.DrawManager.Regist(alpha[48].ToString(), Xplace+Xspace*2, Yplace+Yspace*5, "image\\Title\\Alpha\\AlphaSmall\\W_off.png", "image\\Title\\Alpha\\AlphaSmall\\W_on.png");
			muphic.DrawManager.Regist(alpha[49].ToString(), Xplace+Xspace*3, Yplace+Yspace*5, "image\\Title\\Alpha\\AlphaSmall\\X_off.png", "image\\Title\\Alpha\\AlphaSmall\\X_on.png");
			muphic.DrawManager.Regist(alpha[50].ToString(), Xplace+Xspace*4, Yplace+Yspace*5, "image\\Title\\Alpha\\AlphaSmall\\Y_off.png", "image\\Title\\Alpha\\AlphaSmall\\Y_on.png");
			muphic.DrawManager.Regist(alpha[51].ToString(), Xplace+Xspace*5, Yplace+Yspace*5, "image\\Title\\Alpha\\AlphaSmall\\Z_off.png", "image\\Title\\Alpha\\AlphaSmall\\Z_on.png");


			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			for(int i = 0;i < 52;i++)
				BaseList.Add(alpha[i]);

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