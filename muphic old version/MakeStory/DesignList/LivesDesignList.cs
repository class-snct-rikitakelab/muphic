using System;
using muphic.MakeStory;
using muphic.MakeStory.DesignList;

namespace muphic.MakeStory.DesignList
{
	/// <summary>
	/// LivesDesignList の概要の説明です。
	/// </summary>
	public class LivesDesignList : DesignList
	{
		public FaceButton[] FaceB;
		public DirectionButton[] DirectB;

		public LivesDesignList(MakeStoryScreen mss, int name)
		{
			this.parent = mss;
			//ここでのnameは何のリストかを判別する為
			this.name = name;
			this.FaceB = new FaceButton[4];
			this.DirectB = new DirectionButton[4];

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			for (int i = 0; i < 4; i++)
			{
				FaceB[i] = new FaceButton(this, i+1);
				DirectB[i] = new DirectionButton(this, i+1);
			}

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			int space = 59;
			int ButtonX = 155; int ButtonY = 645;

			switch (this.name)
			{
				case 1:
					#region Boy's Picture
					muphic.DrawManager.Regist("BoyGladB", ButtonX + space * 0, ButtonY, "image\\MakeStory\\button\\human\\boy\\glad_off.png", "image\\MakeStory\\button\\human\\boy\\glad_on.png");
					muphic.DrawManager.Regist("BoyAngryB", ButtonX + space * 1, ButtonY, "image\\MakeStory\\button\\human\\boy\\angry_off.png", "image\\MakeStory\\button\\human\\boy\\angry_on.png");
					muphic.DrawManager.Regist("BoySadB", ButtonX + space * 2, ButtonY, "image\\MakeStory\\button\\human\\boy\\sad_off.png", "image\\MakeStory\\button\\human\\boy\\sad_on.png");
					muphic.DrawManager.Regist("BoyEnjoyB", ButtonX + space * 3, ButtonY, "image\\MakeStory\\button\\human\\boy\\enjoy_off.png", "image\\MakeStory\\button\\human\\boy\\enjoy_on.png");
					muphic.DrawManager.Regist("BoyFrontB", ButtonX + space * 4, ButtonY, "image\\MakeStory\\button\\human\\boy\\front_off.png", "image\\MakeStory\\button\\human\\boy\\front_on.png");
					muphic.DrawManager.Regist("BoyLeftB", ButtonX + space * 5, ButtonY, "image\\MakeStory\\button\\human\\boy\\left_off.png", "image\\MakeStory\\button\\human\\boy\\left_on.png");
					muphic.DrawManager.Regist("BoyRightB", ButtonX + space * 6, ButtonY, "image\\MakeStory\\button\\human\\boy\\right_off.png", "image\\MakeStory\\button\\human\\boy\\right_on.png");
					muphic.DrawManager.Regist("BoyBackB", ButtonX + space * 7, ButtonY, "image\\MakeStory\\button\\human\\boy\\back_off.png", "image\\MakeStory\\button\\human\\boy\\back_on.png");
					#endregion
					break;
				case 2:
					#region Girl's Picture
					muphic.DrawManager.Regist("GirlGladB", ButtonX + space * 0, ButtonY, "image\\MakeStory\\button\\human\\girl\\glad_off.png", "image\\MakeStory\\button\\human\\girl\\glad_on.png");
					muphic.DrawManager.Regist("GirlAngryB", ButtonX + space * 1, ButtonY, "image\\MakeStory\\button\\human\\girl\\angry_off.png", "image\\MakeStory\\button\\human\\girl\\angry_on.png");
					muphic.DrawManager.Regist("GirlSadB", ButtonX + space * 2, ButtonY, "image\\MakeStory\\button\\human\\girl\\sad_off.png", "image\\MakeStory\\button\\human\\girl\\sad_on.png");
					muphic.DrawManager.Regist("GirlEnjoyB", ButtonX + space * 3, ButtonY, "image\\MakeStory\\button\\human\\girl\\enjoy_off.png", "image\\MakeStory\\button\\human\\girl\\enjoy_on.png");
					muphic.DrawManager.Regist("GirlFrontB", ButtonX + space * 4, ButtonY, "image\\MakeStory\\button\\human\\girl\\front_off.png", "image\\MakeStory\\button\\human\\girl\\front_on.png");
					muphic.DrawManager.Regist("GirlLeftB", ButtonX + space * 5, ButtonY, "image\\MakeStory\\button\\human\\girl\\left_off.png", "image\\MakeStory\\button\\human\\girl\\left_on.png");
					muphic.DrawManager.Regist("GirlRightB", ButtonX + space * 6, ButtonY, "image\\MakeStory\\button\\human\\girl\\right_off.png", "image\\MakeStory\\button\\human\\girl\\right_on.png");
					muphic.DrawManager.Regist("GirlBackB", ButtonX + space * 7, ButtonY, "image\\MakeStory\\button\\human\\girl\\back_off.png", "image\\MakeStory\\button\\human\\girl\\back_on.png");
					#endregion
					break;
				case 3:
					#region Lady's Picture
					muphic.DrawManager.Regist("LadyGladB", ButtonX + space * 0, ButtonY, "image\\MakeStory\\button\\human\\lady\\glad_off.png", "image\\MakeStory\\button\\human\\lady\\glad_on.png");
					muphic.DrawManager.Regist("LadyAngryB", ButtonX + space * 1, ButtonY, "image\\MakeStory\\button\\human\\lady\\angry_off.png", "image\\MakeStory\\button\\human\\lady\\angry_on.png");
					muphic.DrawManager.Regist("LadySadB", ButtonX + space * 2, ButtonY, "image\\MakeStory\\button\\human\\lady\\sad_off.png", "image\\MakeStory\\button\\human\\lady\\sad_on.png");
					muphic.DrawManager.Regist("LadyEnjoyB", ButtonX + space * 3, ButtonY, "image\\MakeStory\\button\\human\\lady\\enjoy_off.png", "image\\MakeStory\\button\\human\\lady\\enjoy_on.png");
					muphic.DrawManager.Regist("LadyFrontB", ButtonX + space * 4, ButtonY, "image\\MakeStory\\button\\human\\lady\\front_off.png", "image\\MakeStory\\button\\human\\lady\\front_on.png");
					muphic.DrawManager.Regist("LadyLeftB", ButtonX + space * 5, ButtonY, "image\\MakeStory\\button\\human\\lady\\left_off.png", "image\\MakeStory\\button\\human\\lady\\left_on.png");
					muphic.DrawManager.Regist("LadyRightB", ButtonX + space * 6, ButtonY, "image\\MakeStory\\button\\human\\lady\\right_off.png", "image\\MakeStory\\button\\human\\lady\\right_on.png");
					muphic.DrawManager.Regist("LadyBackB", ButtonX + space * 7, ButtonY, "image\\MakeStory\\button\\human\\lady\\back_off.png", "image\\MakeStory\\button\\human\\lady\\back_on.png");
					#endregion
					break;
				case 4:
					#region Man's Picture
					muphic.DrawManager.Regist("ManGladB", ButtonX + space * 0, ButtonY, "image\\MakeStory\\button\\human\\man\\glad_off.png", "image\\MakeStory\\button\\human\\man\\glad_on.png");
					muphic.DrawManager.Regist("ManAngryB", ButtonX + space * 1, ButtonY, "image\\MakeStory\\button\\human\\man\\angry_off.png", "image\\MakeStory\\button\\human\\man\\angry_on.png");
					muphic.DrawManager.Regist("ManSadB", ButtonX + space * 2, ButtonY, "image\\MakeStory\\button\\human\\man\\sad_off.png", "image\\MakeStory\\button\\human\\man\\sad_on.png");
					muphic.DrawManager.Regist("ManEnjoyB", ButtonX + space * 3, ButtonY, "image\\MakeStory\\button\\human\\man\\enjoy_off.png", "image\\MakeStory\\button\\human\\man\\enjoy_on.png");
					muphic.DrawManager.Regist("ManFrontB", ButtonX + space * 4, ButtonY, "image\\MakeStory\\button\\human\\man\\front_off.png", "image\\MakeStory\\button\\human\\man\\front_on.png");
					muphic.DrawManager.Regist("ManLeftB", ButtonX + space * 5, ButtonY, "image\\MakeStory\\button\\human\\man\\left_off.png", "image\\MakeStory\\button\\human\\man\\left_on.png");
					muphic.DrawManager.Regist("ManRightB", ButtonX + space * 6, ButtonY, "image\\MakeStory\\button\\human\\man\\right_off.png", "image\\MakeStory\\button\\human\\man\\right_on.png");
					muphic.DrawManager.Regist("ManBackB", ButtonX + space * 7, ButtonY, "image\\MakeStory\\button\\human\\man\\back_off.png", "image\\MakeStory\\button\\human\\man\\back_on.png");
					#endregion
					break;
				//Animals
				case 6:
					#region Bear's Picture
					muphic.DrawManager.Regist("BearGladB", ButtonX + space * 0, ButtonY, "image\\MakeStory\\button\\animal\\bear\\glad_off.png", "image\\MakeStory\\button\\animal\\bear\\glad_on.png");
					muphic.DrawManager.Regist("BearAngryB", ButtonX + space * 1, ButtonY, "image\\MakeStory\\button\\animal\\bear\\angry_off.png", "image\\MakeStory\\button\\animal\\bear\\angry_on.png");
					muphic.DrawManager.Regist("BearSadB", ButtonX + space * 2, ButtonY, "image\\MakeStory\\button\\animal\\bear\\sad_off.png", "image\\MakeStory\\button\\animal\\bear\\sad_on.png");
					muphic.DrawManager.Regist("BearEnjoyB", ButtonX + space * 3, ButtonY, "image\\MakeStory\\button\\animal\\bear\\enjoy_off.png", "image\\MakeStory\\button\\animal\\bear\\enjoy_on.png");
					muphic.DrawManager.Regist("BearFrontB", ButtonX + space * 4, ButtonY, "image\\MakeStory\\button\\animal\\bear\\front_off.png", "image\\MakeStory\\button\\animal\\bear\\front_on.png");
					muphic.DrawManager.Regist("BearLeftB", ButtonX + space * 5, ButtonY, "image\\MakeStory\\button\\animal\\bear\\left_off.png", "image\\MakeStory\\button\\animal\\bear\\left_on.png");
					muphic.DrawManager.Regist("BearRightB", ButtonX + space * 6, ButtonY, "image\\MakeStory\\button\\animal\\bear\\right_off.png", "image\\MakeStory\\button\\animal\\bear\\right_on.png");
					muphic.DrawManager.Regist("BearBackB", ButtonX + space * 7, ButtonY, "image\\MakeStory\\button\\animal\\bear\\back_off.png", "image\\MakeStory\\button\\animal\\bear\\back_on.png");
					#endregion
					break;
				case 8:
					#region Bird's Picture
					muphic.DrawManager.Regist("SparrowGladB", ButtonX + space * 0, ButtonY, "image\\MakeStory\\button\\animal\\bird\\glad_off.png", "image\\MakeStory\\button\\animal\\bird\\glad_on.png");
					muphic.DrawManager.Regist("SparrowAngryB", ButtonX + space * 1, ButtonY, "image\\MakeStory\\button\\animal\\bird\\angry_off.png", "image\\MakeStory\\button\\animal\\bird\\angry_on.png");
					muphic.DrawManager.Regist("SparrowSadB", ButtonX + space * 2, ButtonY, "image\\MakeStory\\button\\animal\\bird\\sad_off.png", "image\\MakeStory\\button\\animal\\bird\\sad_on.png");
					muphic.DrawManager.Regist("SparrowEnjoyB", ButtonX + space * 3, ButtonY, "image\\MakeStory\\button\\animal\\bird\\enjoy_off.png", "image\\MakeStory\\button\\animal\\bird\\enjoy_on.png");

					muphic.DrawManager.Regist("SparrowFrontB", ButtonX + space * 4, ButtonY, "image\\MakeStory\\button\\animal\\bird\\front_off.png", "image\\MakeStory\\button\\animal\\bird\\front_on.png");
					muphic.DrawManager.Regist("SparrowLeftB", ButtonX + space * 5, ButtonY, "image\\MakeStory\\button\\animal\\bird\\left_off.png", "image\\MakeStory\\button\\animal\\bird\\left_on.png");
					muphic.DrawManager.Regist("SparrowRightB", ButtonX + space * 6, ButtonY, "image\\MakeStory\\button\\animal\\bird\\right_off.png", "image\\MakeStory\\button\\animal\\bird\\right_on.png");
					muphic.DrawManager.Regist("SparrowBackB", ButtonX + space * 7, ButtonY, "image\\MakeStory\\button\\animal\\bird\\back_off.png", "image\\MakeStory\\button\\animal\\bird\\back_on.png");
					#endregion
					break;
				case 5:
					#region Dog's Picture
					muphic.DrawManager.Regist("WolfGladB", ButtonX + space * 0, ButtonY, "image\\MakeStory\\button\\animal\\dog\\glad_off.png", "image\\MakeStory\\button\\animal\\dog\\glad_on.png");
					muphic.DrawManager.Regist("WolfAngryB", ButtonX + space * 1, ButtonY, "image\\MakeStory\\button\\animal\\dog\\angry_off.png", "image\\MakeStory\\button\\animal\\dog\\angry_on.png");
					muphic.DrawManager.Regist("WolfSadB", ButtonX + space * 2, ButtonY, "image\\MakeStory\\button\\animal\\dog\\sad_off.png", "image\\MakeStory\\button\\animal\\dog\\sad_on.png");
					muphic.DrawManager.Regist("WolfEnjoyB", ButtonX + space * 3, ButtonY, "image\\MakeStory\\button\\animal\\dog\\enjoy_off.png", "image\\MakeStory\\button\\animal\\dog\\enjoy_on.png");

					muphic.DrawManager.Regist("WolfFrontB", ButtonX + space * 4, ButtonY, "image\\MakeStory\\button\\animal\\dog\\front_off.png", "image\\MakeStory\\button\\animal\\dog\\front_on.png");
					muphic.DrawManager.Regist("WolfLeftB", ButtonX + space * 5, ButtonY, "image\\MakeStory\\button\\animal\\dog\\left_off.png", "image\\MakeStory\\button\\animal\\dog\\left_on.png");
					muphic.DrawManager.Regist("WolfRightB", ButtonX + space * 6, ButtonY, "image\\MakeStory\\button\\animal\\dog\\right_off.png", "image\\MakeStory\\button\\animal\\dog\\right_on.png");
					muphic.DrawManager.Regist("WolfBackB", ButtonX + space * 7, ButtonY, "image\\MakeStory\\button\\animal\\dog\\back_off.png", "image\\MakeStory\\button\\animal\\dog\\back_on.png");
					#endregion
					break;
				case 7:
					#region Turtle's Picture
					muphic.DrawManager.Regist("TurtleGladB", ButtonX + space * 0, ButtonY, "image\\MakeStory\\button\\animal\\turtle\\glad_off.png", "image\\MakeStory\\button\\animal\\turtle\\glad_on.png");
					muphic.DrawManager.Regist("TurtleAngryB", ButtonX + space * 1, ButtonY, "image\\MakeStory\\button\\animal\\turtle\\angry_off.png", "image\\MakeStory\\button\\animal\\turtle\\angry_on.png");
					muphic.DrawManager.Regist("TurtleSadB", ButtonX + space * 2, ButtonY, "image\\MakeStory\\button\\animal\\turtle\\sad_off.png", "image\\MakeStory\\button\\animal\\turtle\\sad_on.png");
					muphic.DrawManager.Regist("TurtleEnjoyB", ButtonX + space * 3, ButtonY, "image\\MakeStory\\button\\animal\\turtle\\enjoy_off.png", "image\\MakeStory\\button\\animal\\turtle\\enjoy_on.png");

					muphic.DrawManager.Regist("TurtleFrontB", ButtonX + space * 4, ButtonY, "image\\MakeStory\\button\\animal\\turtle\\front_off.png", "image\\MakeStory\\button\\animal\\turtle\\front_on.png");
					muphic.DrawManager.Regist("TurtleLeftB", ButtonX + space * 5, ButtonY, "image\\MakeStory\\button\\animal\\turtle\\left_off.png", "image\\MakeStory\\button\\animal\\turtle\\left_on.png");
					muphic.DrawManager.Regist("TurtleRightB", ButtonX + space * 6, ButtonY, "image\\MakeStory\\button\\animal\\turtle\\right_off.png", "image\\MakeStory\\button\\animal\\turtle\\right_on.png");
					muphic.DrawManager.Regist("TurtleBackB", ButtonX + space * 7, ButtonY, "image\\MakeStory\\button\\animal\\turtle\\back_off.png", "image\\MakeStory\\button\\animal\\turtle\\back_on.png");
					#endregion
					break;
			}
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			for (int i = 0; i < 4; i++)
			{
				BaseList.Add(FaceB[i]);
				BaseList.Add(DirectB[i]);
			}
		}

		public override void NowClick()
		{
			if ((this.FaceMode == muphic.MakeStory.DesignList.FaceMode.None)
				| (this.DirectionMode == muphic.MakeStory.DesignList.DirectionMode.None))
			{
				parent.tsuibi.State = 0;
			}
			else
			{
				if (this.DirectionMode == muphic.MakeStory.DesignList.DirectionMode.Back)
					parent.tsuibi.State = parent.buttonmode + this.directionvalue + 1;
				else
					parent.tsuibi.State = parent.buttonmode + this.directionvalue + this.facevalue;
			}
			return;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}


		public override void MouseEnter()
		{
			base.MouseEnter ();
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
		}



		public override string ToString()
		{
			return ((ButtonsMode)this.name).ToString();
		}
	}
}
