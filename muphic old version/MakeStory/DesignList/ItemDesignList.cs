using System;
using muphic.MakeStory;
using muphic.MakeStory.DesignList;

namespace muphic.MakeStory.DesignList
{
	/// <summary>
	/// ITemDesignList の概要の説明です。
	/// </summary>
	public class ItemDesignList : DesignList
	{
		public ItemButton[] ItemB;

		public ItemDesignList(MakeStoryScreen mss, int name)
		{
			this.parent = mss;
			//ここでのnameは何のリストかを判別する為
			this.name = name;
			ItemB = new ItemButton[8];

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			for (int i = 0; i < 8; i++)
			{
				ItemB[i] = new ItemButton(this,i+1);
			}

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			int space = 59;
			int ButtonX = 155; int ButtonY = 645;
			switch(this.name)
			{
				case 13:
					#region Goods's Picture
					muphic.DrawManager.Regist("GoodsAB", ButtonX+space*0, ButtonY, "image\\MakeStory\\button\\item\\goods\\ami_off.png", "image\\MakeStory\\button\\item\\goods\\ami_on.png");
					muphic.DrawManager.Regist("GoodsBB", ButtonX+space*1, ButtonY, "image\\MakeStory\\button\\item\\goods\\kago_off.png", "image\\MakeStory\\button\\item\\goods\\kago_on.png");
					muphic.DrawManager.Regist("GoodsCB", ButtonX+space*2, ButtonY, "image\\MakeStory\\button\\item\\goods\\musi_off.png", "image\\MakeStory\\button\\item\\goods\\musi_on.png");
					muphic.DrawManager.Regist("GoodsDB", ButtonX+space*3, ButtonY, "image\\MakeStory\\button\\item\\goods\\usa_off.png", "image\\MakeStory\\button\\item\\goods\\usa_on.png");
					muphic.DrawManager.Regist("GoodsEB", ButtonX+space*4, ButtonY, "image\\MakeStory\\button\\item\\goods\\rappa_off.png", "image\\MakeStory\\button\\item\\goods\\rappa_on.png");
					muphic.DrawManager.Regist("GoodsFB", ButtonX+space*5, ButtonY, "image\\MakeStory\\button\\item\\goods\\denwa_off.png", "image\\MakeStory\\button\\item\\goods\\denwa_on.png");
					muphic.DrawManager.Regist("GoodsGB", ButtonX+space*6, ButtonY, "image\\MakeStory\\button\\item\\goods\\ball_off.png", "image\\MakeStory\\button\\item\\goods\\ball_on.png");
					muphic.DrawManager.Regist("GoodsHB", ButtonX+space*7, ButtonY, "image\\MakeStory\\button\\item\\goods\\car_off.png", "image\\MakeStory\\button\\item\\goods\\car_on.png");
					#endregion
					break;
				case 14:
					#region Fashion's Picture
					muphic.DrawManager.Regist("FashionAB", ButtonX+space*0, ButtonY, "image\\MakeStory\\button\\item\\fashion\\cap_b_off.png", "image\\MakeStory\\button\\item\\fashion\\cap_b_on.png");
					muphic.DrawManager.Regist("FashionBB", ButtonX+space*1, ButtonY, "image\\MakeStory\\button\\item\\fashion\\hat_r_off.png", "image\\MakeStory\\button\\item\\fashion\\hat_r_on.png");
					muphic.DrawManager.Regist("FashionCB", ButtonX+space*2, ButtonY, "image\\MakeStory\\button\\item\\fashion\\cap_g_off.png", "image\\MakeStory\\button\\item\\fashion\\cap_g_on.png");
					muphic.DrawManager.Regist("FashionDB", ButtonX+space*3, ButtonY, "image\\MakeStory\\button\\item\\fashion\\hat_p_off.png", "image\\MakeStory\\button\\item\\fashion\\hat_p_on.png");
					muphic.DrawManager.Regist("FashionEB", ButtonX+space*4, ButtonY, "image\\MakeStory\\button\\item\\fashion\\mugi_off.png", "image\\MakeStory\\button\\item\\fashion\\mugi_on.png");
					muphic.DrawManager.Regist("FashionFB", ButtonX+space*5, ButtonY, "image\\MakeStory\\button\\item\\fashion\\ribbon_off.png", "image\\MakeStory\\button\\item\\fashion\\ribbon_on.png");
					muphic.DrawManager.Regist("FashionGB", ButtonX+space*6, ButtonY, "image\\MakeStory\\button\\item\\fashion\\bag_r_off.png", "image\\MakeStory\\button\\item\\fashion\\bag_r_on.png");
					muphic.DrawManager.Regist("FashionHB", ButtonX+space*7, ButtonY, "image\\MakeStory\\button\\item\\fashion\\bag_p_off.png", "image\\MakeStory\\button\\item\\fashion\\bag_p_on.png");
					#endregion
					break;
				case 15:
					#region Food's Picture
					muphic.DrawManager.Regist("FoodAB", ButtonX+space*0, ButtonY, "image\\MakeStory\\button\\item\\food\\onigiri_off.png", "image\\MakeStory\\button\\item\\food\\onigiri_on.png");
					muphic.DrawManager.Regist("FoodBB", ButtonX+space*1, ButtonY, "image\\MakeStory\\button\\item\\food\\humburg_off.png", "image\\MakeStory\\button\\item\\food\\humburg_on.png");
					muphic.DrawManager.Regist("FoodCB", ButtonX+space*2, ButtonY, "image\\MakeStory\\button\\item\\food\\pudding_off.png", "image\\MakeStory\\button\\item\\food\\pudding_on.png");
					muphic.DrawManager.Regist("FoodDB", ButtonX+space*3, ButtonY, "image\\MakeStory\\button\\item\\food\\cocoa_off.png", "image\\MakeStory\\button\\item\\food\\cocoa_on.png");
					muphic.DrawManager.Regist("FoodEB", ButtonX+space*4, ButtonY, "image\\MakeStory\\button\\item\\food\\dogfood_off.png", "image\\MakeStory\\button\\item\\food\\dogfood_on.png");
					muphic.DrawManager.Regist("FoodFB", ButtonX+space*5, ButtonY, "image\\MakeStory\\button\\item\\food\\fish_off.png", "image\\MakeStory\\button\\item\\food\\fish_on.png");
					muphic.DrawManager.Regist("FoodGB", ButtonX+space*6, ButtonY, "image\\MakeStory\\button\\item\\food\\dongri_off.png", "image\\MakeStory\\button\\item\\food\\dongri_on.png");
					muphic.DrawManager.Regist("FoodHB", ButtonX+space*7, ButtonY, "image\\MakeStory\\button\\item\\food\\apple_off.png", "image\\MakeStory\\button\\item\\food\\apple_on.png");
					#endregion
					break;
				case 16:
					#region Furniture's Picture
					muphic.DrawManager.Regist("FurnitureAB", ButtonX+space*0, ButtonY, "image\\MakeStory\\button\\item\\furniture\\chair_l_off.png", "image\\MakeStory\\button\\item\\furniture\\chair_l_on.png");
					muphic.DrawManager.Regist("FurnitureBB", ButtonX+space*1, ButtonY, "image\\MakeStory\\button\\item\\furniture\\table_off.png", "image\\MakeStory\\button\\item\\furniture\\table_on.png");
					muphic.DrawManager.Regist("FurnitureCB", ButtonX+space*2, ButtonY, "image\\MakeStory\\button\\item\\furniture\\chair_r_off.png", "image\\MakeStory\\button\\item\\furniture\\chair_r_on.png");
					muphic.DrawManager.Regist("FurnitureDB", ButtonX+space*3, ButtonY, "image\\MakeStory\\button\\item\\furniture\\clock_off.png", "image\\MakeStory\\button\\item\\furniture\\clock_on.png");
					muphic.DrawManager.Regist("FurnitureEB", ButtonX+space*4, ButtonY, "image\\MakeStory\\button\\item\\furniture\\tv_off.png", "image\\MakeStory\\button\\item\\furniture\\tv_on.png");
					muphic.DrawManager.Regist("FurnitureFB", ButtonX+space*5, ButtonY, "image\\MakeStory\\button\\item\\furniture\\kyoudai_off.png", "image\\MakeStory\\button\\item\\furniture\\kyoudai_on.png");
					muphic.DrawManager.Regist("FurnitureGB", ButtonX+space*6, ButtonY, "image\\MakeStory\\button\\item\\furniture\\hondana_off.png", "image\\MakeStory\\button\\item\\furniture\\hondana_on.png");
					muphic.DrawManager.Regist("FurnitureHB", ButtonX+space*7, ButtonY, "image\\MakeStory\\button\\item\\furniture\\tansu_off.png", "image\\MakeStory\\button\\item\\furniture\\tansu_on.png");
					#endregion
					break;
			}
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			for (int i = 0; i < ItemB.Length; i++)
			{
				BaseList.Add(ItemB[i]);
			}
		}

		public override void MouseEnter()
		{
			base.MouseEnter ();
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
		}


		public override void NowClick()
		{
			if(this.ItemMode == muphic.MakeStory.DesignList.ItemMode.None)
			{
				parent.tsuibi.State = 0;
			}
			else
			{
				parent.tsuibi.State = parent.buttonmode +this.itemvalue;
			}
			return;
		}

		public override string ToString()
		{
			return ((ButtonsMode)this.name).ToString();
		}
	}
}
