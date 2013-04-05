using System;
using muphic;
using muphic.Titlemode;

namespace muphic.Titlemode
{
	public class KataScreen : CharScreen
	{
		public TitleScreen parent;
		KataButton[] kata;

		public KataScreen(TitleScreen parent)
		{
			this.parent = parent;
			kata = new KataButton[59];

			kata[0] = new KataButton(this, "ア", 0);
			kata[1] = new KataButton(this, "イ", 1);
			kata[2] = new KataButton(this, "ウ", 2);
			kata[3] = new KataButton(this, "エ", 3);
			kata[4] = new KataButton(this, "オ", 4);

			kata[5] = new KataButton(this, "カ", 5);
			kata[6] = new KataButton(this, "キ", 6);
			kata[7] = new KataButton(this, "ク", 7);
			kata[8] = new KataButton(this, "ケ", 8);
			kata[9] = new KataButton(this, "コ", 9);
			
			kata[10] = new KataButton(this, "サ", 10);
			kata[11] = new KataButton(this, "シ", 11);
			kata[12] = new KataButton(this, "ス", 12);
			kata[13] = new KataButton(this, "セ", 13);
			kata[14] = new KataButton(this, "ソ", 14);
			
			kata[15] = new KataButton(this, "タ", 15);
			kata[16] = new KataButton(this, "チ", 16);
			kata[17] = new KataButton(this, "ツ", 17);
			kata[18] = new KataButton(this, "テ", 18);
			kata[19] = new KataButton(this, "ト", 19);

			kata[20] = new KataButton(this, "ナ", 20);
			kata[21] = new KataButton(this, "ニ", 21);
			kata[22] = new KataButton(this, "ヌ", 22);
			kata[23] = new KataButton(this, "ネ", 23);
			kata[24] = new KataButton(this, "ノ", 24);

			kata[25] = new KataButton(this, "ハ", 25);
			kata[26] = new KataButton(this, "ヒ", 26);
			kata[27] = new KataButton(this, "フ", 27);
			kata[28] = new KataButton(this, "ヘ", 28);
			kata[29] = new KataButton(this, "ホ", 29);

			kata[30] = new KataButton(this, "マ", 30);
			kata[31] = new KataButton(this, "ミ", 31);
			kata[32] = new KataButton(this, "ム", 32);
			kata[33] = new KataButton(this, "メ", 33);
			kata[34] = new KataButton(this, "モ", 34);

			kata[35] = new KataButton(this, "ヤ", 35);
			kata[36] = new KataButton(this, "ユ", 36);
			kata[37] = new KataButton(this, "ヨ", 37);

			kata[38] = new KataButton(this, "ラ", 38);
			kata[39] = new KataButton(this, "リ", 39);
			kata[40] = new KataButton(this, "ル", 40);
			kata[41] = new KataButton(this, "レ", 41);
			kata[42] = new KataButton(this, "ロ", 42);

			kata[43] = new KataButton(this, "ワ", 43);
			kata[44] = new KataButton(this, "ヲ", 44);
			kata[45] = new KataButton(this, "ン", 45);

			kata[46] = new KataButton(this, "ァ", 46);
			kata[47] = new KataButton(this, "ィ", 47);
			kata[48] = new KataButton(this, "ゥ", 48);
			kata[49] = new KataButton(this, "ェ", 49);
			kata[50] = new KataButton(this, "ォ", 50);

			kata[51] = new KataButton(this, "ャ", 51);
			kata[52] = new KataButton(this, "ュ", 52);
			kata[53] = new KataButton(this, "ョ", 53);

			kata[54] = new KataButton(this, "ッ", 54);

			kata[55] = new KataButton(this, "ヮ", 55);

			kata[56] = new KataButton(this, "゛", 56);
			kata[57] = new KataButton(this, "゜", 57);
			kata[58] = new KataButton(this, "ー", 58);

			int Xplace = 125; int Xspace = 61; int Yplace = 223; int Yspace = 54;
			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			//あ行
			muphic.DrawManager.Regist(kata[0].ToString(), Xplace+Xspace*12, Yplace+Yspace*0, "image\\Title\\Kata\\a_off.png", "image\\Title\\Kata\\a_on.png");
			muphic.DrawManager.Regist(kata[1].ToString(), Xplace+Xspace*12, Yplace+Yspace*1, "image\\Title\\Kata\\i_off.png", "image\\Title\\Kata\\i_on.png");
			muphic.DrawManager.Regist(kata[2].ToString(), Xplace+Xspace*12, Yplace+Yspace*2, "image\\Title\\Kata\\u_off.png", "image\\Title\\Kata\\u_on.png");
			muphic.DrawManager.Regist(kata[3].ToString(), Xplace+Xspace*12, Yplace+Yspace*3, "image\\Title\\Kata\\e_off.png", "image\\Title\\Kata\\e_on.png");
			muphic.DrawManager.Regist(kata[4].ToString(), Xplace+Xspace*12, Yplace+Yspace*4, "image\\Title\\Kata\\o_off.png", "image\\Title\\Kata\\o_on.png");
			//か行
			muphic.DrawManager.Regist(kata[5].ToString(), Xplace+Xspace*11, Yplace+Yspace*0, "image\\Title\\Kata\\ka_off.png", "image\\Title\\Kata\\ka_on.png");
			muphic.DrawManager.Regist(kata[6].ToString(), Xplace+Xspace*11, Yplace+Yspace*1, "image\\Title\\Kata\\ki_off.png", "image\\Title\\Kata\\ki_on.png");
			muphic.DrawManager.Regist(kata[7].ToString(), Xplace+Xspace*11, Yplace+Yspace*2, "image\\Title\\Kata\\ku_off.png", "image\\Title\\Kata\\ku_on.png");
			muphic.DrawManager.Regist(kata[8].ToString(), Xplace+Xspace*11, Yplace+Yspace*3, "image\\Title\\Kata\\ke_off.png", "image\\Title\\Kata\\ke_on.png");
			muphic.DrawManager.Regist(kata[9].ToString(), Xplace+Xspace*11, Yplace+Yspace*4, "image\\Title\\Kata\\ko_off.png", "image\\Title\\Kata\\ko_on.png");
			//さ行
			muphic.DrawManager.Regist(kata[10].ToString(), Xplace+Xspace*10, Yplace+Yspace*0, "image\\Title\\Kata\\sa_off.png", "image\\Title\\Kata\\sa_on.png");
			muphic.DrawManager.Regist(kata[11].ToString(), Xplace+Xspace*10, Yplace+Yspace*1, "image\\Title\\Kata\\si_off.png", "image\\Title\\Kata\\si_on.png");
			muphic.DrawManager.Regist(kata[12].ToString(), Xplace+Xspace*10, Yplace+Yspace*2, "image\\Title\\Kata\\su_off.png", "image\\Title\\Kata\\su_on.png");
			muphic.DrawManager.Regist(kata[13].ToString(), Xplace+Xspace*10, Yplace+Yspace*3, "image\\Title\\Kata\\se_off.png", "image\\Title\\Kata\\se_on.png");
			muphic.DrawManager.Regist(kata[14].ToString(), Xplace+Xspace*10, Yplace+Yspace*4, "image\\Title\\Kata\\so_off.png", "image\\Title\\Kata\\so_on.png");
			//た行
			muphic.DrawManager.Regist(kata[15].ToString(), Xplace+Xspace*9, Yplace+Yspace*0, "image\\Title\\Kata\\ta_off.png", "image\\Title\\Kata\\ta_on.png");
			muphic.DrawManager.Regist(kata[16].ToString(), Xplace+Xspace*9, Yplace+Yspace*1, "image\\Title\\Kata\\ti_off.png", "image\\Title\\Kata\\ti_on.png");
			muphic.DrawManager.Regist(kata[17].ToString(), Xplace+Xspace*9, Yplace+Yspace*2, "image\\Title\\Kata\\tu_off.png", "image\\Title\\Kata\\tu_on.png");
			muphic.DrawManager.Regist(kata[18].ToString(), Xplace+Xspace*9, Yplace+Yspace*3, "image\\Title\\Kata\\te_off.png", "image\\Title\\Kata\\te_on.png");
			muphic.DrawManager.Regist(kata[19].ToString(), Xplace+Xspace*9, Yplace+Yspace*4, "image\\Title\\Kata\\to_off.png", "image\\Title\\Kata\\to_on.png");
			//な行
			muphic.DrawManager.Regist(kata[20].ToString(), Xplace+Xspace*8, Yplace+Yspace*0, "image\\Title\\Kata\\na_off.png", "image\\Title\\Kata\\na_on.png");
			muphic.DrawManager.Regist(kata[21].ToString(), Xplace+Xspace*8, Yplace+Yspace*1, "image\\Title\\Kata\\ni_off.png", "image\\Title\\Kata\\ni_on.png");
			muphic.DrawManager.Regist(kata[22].ToString(), Xplace+Xspace*8, Yplace+Yspace*2, "image\\Title\\Kata\\nu_off.png", "image\\Title\\Kata\\nu_on.png");
			muphic.DrawManager.Regist(kata[23].ToString(), Xplace+Xspace*8, Yplace+Yspace*3, "image\\Title\\Kata\\ne_off.png", "image\\Title\\Kata\\ne_on.png");
			muphic.DrawManager.Regist(kata[24].ToString(), Xplace+Xspace*8, Yplace+Yspace*4, "image\\Title\\Kata\\no_off.png", "image\\Title\\Kata\\no_on.png");
			//は行
			muphic.DrawManager.Regist(kata[25].ToString(), Xplace+Xspace*7, Yplace+Yspace*0, "image\\Title\\Kata\\ha_off.png", "image\\Title\\Kata\\ha_on.png");
			muphic.DrawManager.Regist(kata[26].ToString(), Xplace+Xspace*7, Yplace+Yspace*1, "image\\Title\\Kata\\hi_off.png", "image\\Title\\Kata\\hi_on.png");
			muphic.DrawManager.Regist(kata[27].ToString(), Xplace+Xspace*7, Yplace+Yspace*2, "image\\Title\\Kata\\hu_off.png", "image\\Title\\Kata\\hu_on.png");
			muphic.DrawManager.Regist(kata[28].ToString(), Xplace+Xspace*7, Yplace+Yspace*3, "image\\Title\\Kata\\he_off.png", "image\\Title\\Kata\\he_on.png");
			muphic.DrawManager.Regist(kata[29].ToString(), Xplace+Xspace*7, Yplace+Yspace*4, "image\\Title\\Kata\\ho_off.png", "image\\Title\\Kata\\ho_on.png");
			//ま行
			muphic.DrawManager.Regist(kata[30].ToString(), Xplace+Xspace*6, Yplace+Yspace*0, "image\\Title\\Kata\\ma_off.png", "image\\Title\\Kata\\ma_on.png");
			muphic.DrawManager.Regist(kata[31].ToString(), Xplace+Xspace*6, Yplace+Yspace*1, "image\\Title\\Kata\\mi_off.png", "image\\Title\\Kata\\mi_on.png");
			muphic.DrawManager.Regist(kata[32].ToString(), Xplace+Xspace*6, Yplace+Yspace*2, "image\\Title\\Kata\\mu_off.png", "image\\Title\\Kata\\mu_on.png");
			muphic.DrawManager.Regist(kata[33].ToString(), Xplace+Xspace*6, Yplace+Yspace*3, "image\\Title\\Kata\\me_off.png", "image\\Title\\Kata\\me_on.png");
			muphic.DrawManager.Regist(kata[34].ToString(), Xplace+Xspace*6, Yplace+Yspace*4, "image\\Title\\Kata\\mo_off.png", "image\\Title\\Kata\\mo_on.png");
			//や行
			muphic.DrawManager.Regist(kata[35].ToString(), Xplace+Xspace*5, Yplace+Yspace*0, "image\\Title\\Kata\\ya_off.png", "image\\Title\\Kata\\ya_on.png");
			muphic.DrawManager.Regist(kata[36].ToString(), Xplace+Xspace*5, Yplace+Yspace*2, "image\\Title\\Kata\\yu_off.png", "image\\Title\\Kata\\yu_on.png");
			muphic.DrawManager.Regist(kata[37].ToString(), Xplace+Xspace*5, Yplace+Yspace*4, "image\\Title\\Kata\\yo_off.png", "image\\Title\\Kata\\yo_on.png");
			//ら行
			muphic.DrawManager.Regist(kata[38].ToString(), Xplace+Xspace*4, Yplace+Yspace*0, "image\\Title\\Kata\\ra_off.png", "image\\Title\\Kata\\ra_on.png");
			muphic.DrawManager.Regist(kata[39].ToString(), Xplace+Xspace*4, Yplace+Yspace*1, "image\\Title\\Kata\\ri_off.png", "image\\Title\\Kata\\ri_on.png");
			muphic.DrawManager.Regist(kata[40].ToString(), Xplace+Xspace*4, Yplace+Yspace*2, "image\\Title\\Kata\\ru_off.png", "image\\Title\\Kata\\ru_on.png");
			muphic.DrawManager.Regist(kata[41].ToString(), Xplace+Xspace*4, Yplace+Yspace*3, "image\\Title\\Kata\\re_off.png", "image\\Title\\Kata\\re_on.png");
			muphic.DrawManager.Regist(kata[42].ToString(), Xplace+Xspace*4, Yplace+Yspace*4, "image\\Title\\Kata\\ro_off.png", "image\\Title\\Kata\\ro_on.png");
			//わをん
			muphic.DrawManager.Regist(kata[43].ToString(), Xplace+Xspace*3, Yplace+Yspace*0, "image\\Title\\Kata\\wa_off.png", "image\\Title\\Kata\\wa_on.png");
			muphic.DrawManager.Regist(kata[44].ToString(), Xplace+Xspace*3, Yplace+Yspace*2, "image\\Title\\Kata\\wo_off.png", "image\\Title\\Kata\\wo_on.png");
			muphic.DrawManager.Regist(kata[45].ToString(), Xplace+Xspace*3, Yplace+Yspace*4, "image\\Title\\Kata\\n_off.png", "image\\Title\\Kata\\n_on.png");
			//ぁ行
			muphic.DrawManager.Regist(kata[46].ToString(), Xplace+Xspace*2, Yplace+Yspace*0, "image\\Title\\Kata\\xa_off.png", "image\\Title\\Kata\\xa_on.png");
			muphic.DrawManager.Regist(kata[47].ToString(), Xplace+Xspace*2, Yplace+Yspace*1, "image\\Title\\Kata\\xi_off.png", "image\\Title\\Kata\\xi_on.png");
			muphic.DrawManager.Regist(kata[48].ToString(), Xplace+Xspace*2, Yplace+Yspace*2, "image\\Title\\Kata\\xu_off.png", "image\\Title\\Kata\\xu_on.png");
			muphic.DrawManager.Regist(kata[49].ToString(), Xplace+Xspace*2, Yplace+Yspace*3, "image\\Title\\Kata\\xe_off.png", "image\\Title\\Kata\\xe_on.png");
			muphic.DrawManager.Regist(kata[50].ToString(), Xplace+Xspace*2, Yplace+Yspace*4, "image\\Title\\Kata\\xo_off.png", "image\\Title\\Kata\\xo_on.png");
			//ゃ行
			muphic.DrawManager.Regist(kata[51].ToString(), Xplace+Xspace*1, Yplace+Yspace*0, "image\\Title\\Kata\\xya_off.png", "image\\Title\\Kata\\xya_on.png");
			muphic.DrawManager.Regist(kata[52].ToString(), Xplace+Xspace*1, Yplace+Yspace*2, "image\\Title\\Kata\\xyu_off.png", "image\\Title\\Kata\\xyu_on.png");
			muphic.DrawManager.Regist(kata[53].ToString(), Xplace+Xspace*1, Yplace+Yspace*4, "image\\Title\\Kata\\xyo_off.png", "image\\Title\\Kata\\xyo_on.png");
			//っゎ゛゜ー
			muphic.DrawManager.Regist(kata[54].ToString(), Xplace+Xspace*0, Yplace+Yspace*0, "image\\Title\\Kata\\xtu_off.png", "image\\Title\\Kata\\xtu_on.png");
			muphic.DrawManager.Regist(kata[55].ToString(), Xplace+Xspace*0, Yplace+Yspace*1, "image\\Title\\Kata\\xwa_off.png", "image\\Title\\Kata\\xwa_on.png");
			muphic.DrawManager.Regist(kata[56].ToString(), Xplace+Xspace*0, Yplace+Yspace*2, "image\\Title\\Kata\\dakuten_off.png", "image\\Title\\Kata\\dakuten_on.png");
			muphic.DrawManager.Regist(kata[57].ToString(), Xplace+Xspace*0, Yplace+Yspace*3, "image\\Title\\Kata\\handakuten_off.png", "image\\Title\\Kata\\handakuten_on.png");
			muphic.DrawManager.Regist(kata[58].ToString(), Xplace+Xspace*0, Yplace+Yspace*4, "image\\Title\\Kata\\-_off.png", "image\\Title\\Kata\\-_on.png");
			

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			for(int i = 0;i < 59;i++)
				BaseList.Add(kata[i]);

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