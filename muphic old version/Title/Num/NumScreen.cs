using System;
using muphic;
using muphic.Titlemode;

namespace muphic.Titlemode
{
	public class NumScreen : CharScreen
	{
		public TitleScreen parent;
		NumButton[] num;

		public NumScreen(TitleScreen parent)
		{
			this.parent = parent;
			num = new NumButton[46];

			num[0] = new NumButton(this, "�O", 0);
			num[1] = new NumButton(this, "�P", 1);
			num[2] = new NumButton(this, "�Q", 2);
			num[3] = new NumButton(this, "�R", 3);
			num[4] = new NumButton(this, "�S", 4);
			num[5] = new NumButton(this, "�T", 5);
			num[6] = new NumButton(this, "�U", 6);
			num[7] = new NumButton(this, "�V", 7);
			num[8] = new NumButton(this, "�W", 8);
			num[9] = new NumButton(this, "�X", 9);

			num[10] = new NumButton(this, "�{", 10);
			num[11] = new NumButton(this, "�|", 11);
			num[12] = new NumButton(this, "�~", 12);
			num[13] = new NumButton(this, "��", 13);
			num[14] = new NumButton(this, "��", 14);
			num[15] = new NumButton(this, "��", 15);
			num[16] = new NumButton(this, "��", 16);
			num[17] = new NumButton(this, "��", 17);
			num[18] = new NumButton(this, "�H", 18);
			num[19] = new NumButton(this, "�I", 19);
			
			num[20] = new NumButton(this, "��", 20);
			num[21] = new NumButton(this, "��", 21);
			num[22] = new NumButton(this, "��", 22);
			num[23] = new NumButton(this, "��", 23);
			num[24] = new NumButton(this, "��", 24);
			num[25] = new NumButton(this, "��", 25);
			num[26] = new NumButton(this, "��", 26);
			num[27] = new NumButton(this, "��", 27);
			num[28] = new NumButton(this, "��", 28);
			num[29] = new NumButton(this, "��", 29);
			
			num[30] = new NumButton(this, "��", 30);
			num[31] = new NumButton(this, "��", 31);
			num[32] = new NumButton(this, "��", 32);
			num[33] = new NumButton(this, "��", 33);
			num[34] = new NumButton(this, "��", 34);
			num[35] = new NumButton(this, "��", 35);
			num[36] = new NumButton(this, "��", 36);
			num[37] = new NumButton(this, "��", 37);
			num[38] = new NumButton(this, "��", 38);
			num[39] = new NumButton(this, "��", 39);

			num[40] = new NumButton(this, "��", 40);
			num[41] = new NumButton(this, "��", 41);
			num[42] = new NumButton(this, "��", 42);
			num[43] = new NumButton(this, "��", 43);
			num[44] = new NumButton(this, "�`", 44);
			num[45] = new NumButton(this, "�@", 45);


            int Xplace = 212; int Xspace = 61; int Yplace = 224; int Yspace = 54;
			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(num[0].ToString(), Xplace+Xspace*0, Yplace+Yspace*0, "image\\Title\\Num\\0_off.png", "image\\Title\\Num\\0_on.png");
			muphic.DrawManager.Regist(num[1].ToString(), Xplace+Xspace*1, Yplace+Yspace*0, "image\\Title\\Num\\1_off.png", "image\\Title\\Num\\1_on.png");
			muphic.DrawManager.Regist(num[2].ToString(), Xplace+Xspace*2, Yplace+Yspace*0, "image\\Title\\Num\\2_off.png", "image\\Title\\Num\\2_on.png");
			muphic.DrawManager.Regist(num[3].ToString(), Xplace+Xspace*3, Yplace+Yspace*0, "image\\Title\\Num\\3_off.png", "image\\Title\\Num\\3_on.png");
			muphic.DrawManager.Regist(num[4].ToString(), Xplace+Xspace*4, Yplace+Yspace*0, "image\\Title\\Num\\4_off.png", "image\\Title\\Num\\4_on.png");
			muphic.DrawManager.Regist(num[5].ToString(), Xplace+Xspace*5, Yplace+Yspace*0, "image\\Title\\Num\\5_off.png", "image\\Title\\Num\\5_on.png");
			muphic.DrawManager.Regist(num[6].ToString(), Xplace+Xspace*6, Yplace+Yspace*0, "image\\Title\\Num\\6_off.png", "image\\Title\\Num\\6_on.png");
			muphic.DrawManager.Regist(num[7].ToString(), Xplace+Xspace*7, Yplace+Yspace*0, "image\\Title\\Num\\7_off.png", "image\\Title\\Num\\7_on.png");
			muphic.DrawManager.Regist(num[8].ToString(), Xplace+Xspace*8, Yplace+Yspace*0, "image\\Title\\Num\\8_off.png", "image\\Title\\Num\\8_on.png");
			muphic.DrawManager.Regist(num[9].ToString(), Xplace+Xspace*9, Yplace+Yspace*0, "image\\Title\\Num\\9_off.png", "image\\Title\\Num\\9_on.png");

			muphic.DrawManager.Regist(num[10].ToString(), Xplace+Xspace*0, Yplace+Yspace*1, "image\\Title\\Num\\union_off.png", "image\\Title\\Num\\union_on.png");
			muphic.DrawManager.Regist(num[11].ToString(), Xplace+Xspace*1, Yplace+Yspace*1, "image\\Title\\Num\\differ_off.png", "image\\Title\\Num\\differ_on.png");
			muphic.DrawManager.Regist(num[12].ToString(), Xplace+Xspace*2, Yplace+Yspace*1, "image\\Title\\Num\\intersect_off.png", "image\\Title\\Num\\intersect_on.png");
			muphic.DrawManager.Regist(num[13].ToString(), Xplace+Xspace*3, Yplace+Yspace*1, "image\\Title\\Num\\div_off.png", "image\\Title\\Num\\div_on.png");
			muphic.DrawManager.Regist(num[14].ToString(), Xplace+Xspace*4, Yplace+Yspace*1, "image\\Title\\Num\\equal_off.png", "image\\Title\\Num\\equal_on.png");
			muphic.DrawManager.Regist(num[15].ToString(), Xplace+Xspace*5, Yplace+Yspace*1, "image\\Title\\Num\\lessthan_off.png", "image\\Title\\Num\\lessthan_on.png");
			muphic.DrawManager.Regist(num[16].ToString(), Xplace+Xspace*6, Yplace+Yspace*1, "image\\Title\\Num\\morethan_off.png", "image\\Title\\Num\\morethan_on.png");
			muphic.DrawManager.Regist(num[17].ToString(), Xplace+Xspace*7, Yplace+Yspace*1, "image\\Title\\Num\\per_off.png", "image\\Title\\Num\\per_on.png");
			muphic.DrawManager.Regist(num[18].ToString(), Xplace+Xspace*8, Yplace+Yspace*1, "image\\Title\\Num\\ques_off.png", "image\\Title\\Num\\ques_on.png");
			muphic.DrawManager.Regist(num[19].ToString(), Xplace+Xspace*9, Yplace+Yspace*1, "image\\Title\\Num\\exclam_off.png", "image\\Title\\Num\\exclam_on.png");

			muphic.DrawManager.Regist(num[20].ToString(), Xplace+Xspace*0, Yplace+Yspace*2, "image\\Title\\Num\\circ_b_off.png", "image\\Title\\Num\\circ_b_on.png");
			muphic.DrawManager.Regist(num[21].ToString(), Xplace+Xspace*1, Yplace+Yspace*2, "image\\Title\\Num\\circ_off.png", "image\\Title\\Num\\circ_on.png");
			muphic.DrawManager.Regist(num[22].ToString(), Xplace+Xspace*2, Yplace+Yspace*2, "image\\Title\\Num\\tri_b_off.png", "image\\Title\\Num\\tri_b_on.png");
			muphic.DrawManager.Regist(num[23].ToString(), Xplace+Xspace*3, Yplace+Yspace*2, "image\\Title\\Num\\tri_off.png", "image\\Title\\Num\\tri_on.png");
			muphic.DrawManager.Regist(num[24].ToString(), Xplace+Xspace*4, Yplace+Yspace*2, "image\\Title\\Num\\tri_inv_b_off.png", "image\\Title\\Num\\tri_inv_b_on.png");
			muphic.DrawManager.Regist(num[25].ToString(), Xplace+Xspace*5, Yplace+Yspace*2, "image\\Title\\Num\\tri_inv_off.png", "image\\Title\\Num\\tri_inv_on.png");
			muphic.DrawManager.Regist(num[26].ToString(), Xplace+Xspace*6, Yplace+Yspace*2, "image\\Title\\Num\\sqr_b_off.png", "image\\Title\\Num\\sqr_b_on.png");
			muphic.DrawManager.Regist(num[27].ToString(), Xplace+Xspace*7, Yplace+Yspace*2, "image\\Title\\Num\\sqr_off.png", "image\\Title\\Num\\sqr_on.png");
			muphic.DrawManager.Regist(num[28].ToString(), Xplace+Xspace*8, Yplace+Yspace*2, "image\\Title\\Num\\star_b_off.png", "image\\Title\\Num\\star_b_on.png");
			muphic.DrawManager.Regist(num[29].ToString(), Xplace+Xspace*9, Yplace+Yspace*2, "image\\Title\\Num\\star_off.png", "image\\Title\\Num\\star_on.png");
			
			muphic.DrawManager.Regist(num[30].ToString(), Xplace+Xspace*0, Yplace+Yspace*3, "image\\Title\\Num\\diamond_b_off.png", "image\\Title\\Num\\diamond_b_on.png");
			muphic.DrawManager.Regist(num[31].ToString(), Xplace+Xspace*1, Yplace+Yspace*3, "image\\Title\\Num\\diamond_off.png", "image\\Title\\Num\\diamond_on.png");
			muphic.DrawManager.Regist(num[32].ToString(), Xplace+Xspace*2, Yplace+Yspace*3, "image\\Title\\Num\\doublecirc_off.png", "image\\Title\\Num\\doublecirc_on.png");
			muphic.DrawManager.Regist(num[33].ToString(), Xplace+Xspace*3, Yplace+Yspace*3, "image\\Title\\Num\\uparrow_off.png", "image\\Title\\Num\\uparrow_on.png");
			muphic.DrawManager.Regist(num[34].ToString(), Xplace+Xspace*4, Yplace+Yspace*3, "image\\Title\\Num\\leftarrow_off.png", "image\\Title\\Num\\leftarrow_on.png");
			muphic.DrawManager.Regist(num[35].ToString(), Xplace+Xspace*5, Yplace+Yspace*3, "image\\Title\\Num\\rightarrow_off.png", "image\\Title\\Num\\rightarrow_on.png");
			muphic.DrawManager.Regist(num[36].ToString(), Xplace+Xspace*6, Yplace+Yspace*3, "image\\Title\\Num\\downarrow_off.png", "image\\Title\\Num\\downarrow_on.png");
			muphic.DrawManager.Regist(num[37].ToString(), Xplace+Xspace*7, Yplace+Yspace*3, "image\\Title\\Num\\note_off.png", "image\\Title\\Num\\note_on.png");
			muphic.DrawManager.Regist(num[38].ToString(), Xplace+Xspace*8, Yplace+Yspace*3, "image\\Title\\Num\\sharp_off.png", "image\\Title\\Num\\sharp_on.png");
			muphic.DrawManager.Regist(num[39].ToString(), Xplace+Xspace*9, Yplace+Yspace*3, "image\\Title\\Num\\flat_off.png", "image\\Title\\Num\\flat_on.png");

			muphic.DrawManager.Regist(num[40].ToString(), Xplace+Xspace*0, Yplace+Yspace*4, "image\\Title\\Num\\at_off.png", "image\\Title\\Num\\at_on.png");
			muphic.DrawManager.Regist(num[41].ToString(), Xplace+Xspace*1, Yplace+Yspace*4, "image\\Title\\Num\\and_off.png", "image\\Title\\Num\\and_on.png");
			muphic.DrawManager.Regist(num[42].ToString(), Xplace+Xspace*2, Yplace+Yspace*4, "image\\Title\\Num\\ast_off.png", "image\\Title\\Num\\ast_on.png");
			muphic.DrawManager.Regist(num[43].ToString(), Xplace+Xspace*3, Yplace+Yspace*4, "image\\Title\\Num\\rice_off.png", "image\\Title\\Num\\rice_on.png");
			muphic.DrawManager.Regist(num[44].ToString(), Xplace+Xspace*4, Yplace+Yspace*4, "image\\Title\\Num\\wave_off.png", "image\\Title\\Num\\wave_on.png");
			muphic.DrawManager.Regist(num[45].ToString(), Xplace+Xspace*5-5, Yplace+Yspace*4-4, "image\\Title\\Num\\space_off.png", "image\\Title\\Num\\space_on.png");


			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			for(int i = 0;i <= 45;i++)
				BaseList.Add(num[i]);

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