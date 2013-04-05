using System;
using muphic.MakeStory;
using muphic.MakeStory.DesignList;

namespace muphic.MakeStory.DesignList
{
	/// <summary>
	/// BGDesignList の概要の説明です。
	/// </summary>
	public class BGDesignList : DesignList
	{
		public EnvButton[] EnvB;

		public BGDesignList(MakeStoryScreen mss, int name)
		{
			this.parent = mss;
			//ここでのnameは何のリストかを判別する為
			this.name = name;
			EnvB = new EnvButton[6];

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			for (int i = 0; i < 6; i++)
			{
				EnvB[i] = new EnvButton(this,i+1);
			}

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			int space = 59;
			int ButtonX = 155; int ButtonY = 645;
			switch(this.name)
			{
				case 9:
					#region Forest's Picture
					muphic.DrawManager.Regist("ForestDay1B".ToString(), ButtonX+space*0, ButtonY, "image\\MakeStory\\button\\background\\forest\\forest_1\\day1_off.png", "image\\MakeStory\\button\\background\\forest\\forest_1\\day1_on.png");
					muphic.DrawManager.Regist("ForestNight1B", ButtonX+space*1, ButtonY, "image\\MakeStory\\button\\background\\forest\\forest_1\\night1_off.png", "image\\MakeStory\\button\\background\\forest\\forest_1\\night1_on.png");
					muphic.DrawManager.Regist("ForestBad1B", ButtonX+space*2, ButtonY, "image\\MakeStory\\button\\background\\forest\\forest_1\\bw1_off.png", "image\\MakeStory\\button\\background\\forest\\forest_1\\bw1_on.png");
					muphic.DrawManager.Regist("ForestDay2B", ButtonX+space*3, ButtonY, "image\\MakeStory\\button\\background\\forest\\forest_2\\day2_off.png", "image\\MakeStory\\button\\background\\forest\\forest_2\\day2_on.png");
					muphic.DrawManager.Regist("ForestNight2B", ButtonX+space*4, ButtonY, "image\\MakeStory\\button\\background\\forest\\forest_2\\night2_off.png", "image\\MakeStory\\button\\background\\forest\\forest_2\\night2_on.png");
					muphic.DrawManager.Regist("ForestBad2B", ButtonX+space*5, ButtonY, "image\\MakeStory\\button\\background\\forest\\forest_2\\bw2_off.png", "image\\MakeStory\\button\\background\\forest\\forest_2\\bw2_on.png");
					#endregion
					break;
				case 10:
					#region River's Picture
					muphic.DrawManager.Regist("RiverDay1B", ButtonX+space*0, ButtonY, "image\\MakeStory\\button\\background\\river\\river_1\\day1_off.png", "image\\MakeStory\\button\\background\\river\\river_1\\day1_on.png");
					muphic.DrawManager.Regist("RiverNight1B", ButtonX+space*1, ButtonY, "image\\MakeStory\\button\\background\\river\\river_1\\night1_off.png", "image\\MakeStory\\button\\background\\river\\river_1\\night1_on.png");
					muphic.DrawManager.Regist("RiverBad1B", ButtonX+space*2, ButtonY, "image\\MakeStory\\button\\background\\river\\river_1\\bw1_off.png", "image\\MakeStory\\button\\background\\river\\river_1\\bw1_on.png");
					muphic.DrawManager.Regist("RiverDay2B", ButtonX+space*3, ButtonY, "image\\MakeStory\\button\\background\\river\\river_2\\day2_off.png", "image\\MakeStory\\button\\background\\river\\river_2\\day2_on.png");
					muphic.DrawManager.Regist("RiverNight2B", ButtonX+space*4, ButtonY, "image\\MakeStory\\button\\background\\river\\river_2\\night2_off.png", "image\\MakeStory\\button\\background\\river\\river_2\\night2_on.png");
					muphic.DrawManager.Regist("RiverBad2B", ButtonX+space*5, ButtonY, "image\\MakeStory\\button\\background\\river\\river_2\\bw2_off.png", "image\\MakeStory\\button\\background\\river\\river_2\\bw2_on.png");
					#endregion
					break;
				case 12:
					#region Room's Picture
					muphic.DrawManager.Regist("RoomDay1B", ButtonX+space*0, ButtonY, "image\\MakeStory\\button\\background\\room\\room_1\\day1_off.png", "image\\MakeStory\\button\\background\\room\\room_1\\day1_on.png");
					muphic.DrawManager.Regist("RoomNight1B", ButtonX+space*1, ButtonY, "image\\MakeStory\\button\\background\\room\\room_1\\night1_off.png", "image\\MakeStory\\button\\background\\room\\room_1\\night1_on.png");
					muphic.DrawManager.Regist("RoomBad1B", ButtonX+space*2, ButtonY, "image\\MakeStory\\button\\background\\room\\room_1\\bw1_off.png", "image\\MakeStory\\button\\background\\room\\room_1\\bw1_on.png");
					muphic.DrawManager.Regist("RoomDay2B", ButtonX+space*3, ButtonY, "image\\MakeStory\\button\\background\\room\\room_2\\day2_off.png", "image\\MakeStory\\button\\background\\room\\room_2\\day2_on.png");
					muphic.DrawManager.Regist("RoomNight2B", ButtonX+space*4, ButtonY, "image\\MakeStory\\button\\background\\room\\room_2\\night2_off.png", "image\\MakeStory\\button\\background\\room\\room_2\\night2_on.png");
					muphic.DrawManager.Regist("RoomBad2B", ButtonX+space*5, ButtonY, "image\\MakeStory\\button\\background\\room\\room_2\\bw2_off.png", "image\\MakeStory\\button\\background\\room\\room_2\\bw2_on.png");
					#endregion
					break;
				case 11:
					#region Town's Picture
					muphic.DrawManager.Regist("TownDay1B", ButtonX+space*0, ButtonY, "image\\MakeStory\\button\\background\\town\\town_1\\day1_off.png", "image\\MakeStory\\button\\background\\town\\town_1\\day1_on.png");
					muphic.DrawManager.Regist("TownNight1B", ButtonX+space*1, ButtonY, "image\\MakeStory\\button\\background\\town\\town_1\\night1_off.png", "image\\MakeStory\\button\\background\\town\\town_1\\night1_on.png");
					muphic.DrawManager.Regist("TownBad1B", ButtonX+space*2, ButtonY, "image\\MakeStory\\button\\background\\town\\town_1\\bw1_off.png", "image\\MakeStory\\button\\background\\town\\town_1\\bw1_on.png");
					muphic.DrawManager.Regist("TownDay2B", ButtonX+space*3, ButtonY, "image\\MakeStory\\button\\background\\town\\town_2\\day2_off.png", "image\\MakeStory\\button\\background\\town\\town_2\\day2_on.png");
					muphic.DrawManager.Regist("TownNight2B", ButtonX+space*4, ButtonY, "image\\MakeStory\\button\\background\\town\\town_2\\night2_off.png", "image\\MakeStory\\button\\background\\town\\town_2\\night2_on.png");
					muphic.DrawManager.Regist("TownBad2B", ButtonX+space*5, ButtonY, "image\\MakeStory\\button\\background\\town\\town_2\\bw2_off.png", "image\\MakeStory\\button\\background\\town\\town_2\\bw2_on.png");
					#endregion
					break;
			}
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			for (int i = 0; i < EnvB.Length; i++)
			{
				BaseList.Add(EnvB[i]);
			}
		}
		public override void MouseEnter()
		{
			System.Diagnostics.Debug.WriteLine("Enter");
			//base.MouseEnter ();
		}
		public override void MouseLeave()
		{
			System.Diagnostics.Debug.WriteLine("Leave");
			//base.MouseLeave ();
		}



		public override void NowClick()
		{
			if(this.EnvironmentMode == muphic.MakeStory.DesignList.EnvironmentMode.None)
			{
				parent.wind.backscreen = 0;
			}
			else
			{
				parent.wind.backscreen = parent.buttonmode +this.envvalue;
			}
			return;
		}

		public override string ToString()
		{
			return ((ButtonsMode)this.name).ToString();
		}
	}
}
