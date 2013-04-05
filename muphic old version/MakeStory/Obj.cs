using System;
using muphic.MakeStory;

namespace muphic.MakeStory
{
	public enum ObjMode
	{
		None,
		ManFrontGlad, ManFrontAngry, ManFrontSad, ManFrontEnjoy, ManLeftGlad, ManLeftAngry, ManLeftSad, ManLeftEnjoy,
		ManRightGlad, ManRightAngry, ManRightSad, ManRightEnjoy, ManBack,
		LadyFrontGlad, LadyFrontAngry, LadyFrontSad, LadyFrontEnjoy, LadyLeftGlad, LadyLeftAngry, LadyLeftSad, LadyLeftEnjoy,
		LadyRightGlad, LadyRightAngry, LadyRightSad, LadyRightEnjoy, LadyBack,
		GirlFrontGlad, GirlFrontAngry, GirlFrontSad, GirlFrontEnjoy, GirlLeftGlad, GirlLeftAngry, GirlLeftSad, GirlLeftEnjoy,
		GirlRightGlad, GirlRightAngry, GirlRightSad, GirlRightEnjoy, GirlBack,
		BoyFrontGlad, BoyFrontAngry, BoyFrontSad, BoyFrontEnjoy, BoyLeftGlad, BoyLeftAngry, BoyLeftSad, BoyLeftEnjoy,
		BoyRightGlad, BoyRightAngry, BoyRightSad, BoyRightEnjoy, BoyBack,

		WolfFrontGlad, WolfFrontAngry, WolfFrontSad, WolfFrontEnjoy, WolfLeftGlad, WolfLeftAngry, WolfLeftSad, WolfLeftEnjoy,
		WolfRightGlad, WolfRightAngry, WolfRightSad, WolfRightEnjoy, WolfBack,
		BearFrontGlad, BearFrontAngry, BearFrontSad, BearFrontEnjoy, BearLeftGlad, BearLeftAngry, BearLeftSad, BearLeftEnjoy,
		BearRightGlad, BearRightAngry, BearRightSad, BearRightEnjoy, BearBack,
		TurtleFrontGlad, TurtleFrontAngry, TurtleFrontSad, TurtleFrontEnjoy, TurtleLeftGlad, TurtleLeftAngry, TurtleLeftSad, TurtleLeftEnjoy,
		TurtleRightGlad, TurtleRightAngry, TurtleRightSad, TurtleRightEnjoy, TurtleBack,
		SparrowFrontGlad, SparrowFrontAngry, SparrowFrontSad, SparrowFrontEnjoy, SparrowLeftGlad, SparrowLeftAngry, SparrowLeftSad, SparrowLeftEnjoy,
		SparrowRightGlad, SparrowRightAngry, SparrowRightSad, SparrowRightEnjoy, SparrowBack,

		GoodsAmi, GoodsKago, GoodsMusi,	GoodsUsa, GoodsRappa, GoodsDenwa, GoodsBall, GoodsCar,
		FashionCapB, FashionHatR, FashionCapG, FashionHatP, FashionMugi, FashionRibbon, FashionBagR, FashionBagP,
		FoodOnigiri, FoodHumburg, FoodPudding, FoodCocoa, FoodDogfood, FoodFish, FoodDongri, FoodApple,
		FurnitureChairL, FurnitureTable, FurnitureChairR, FurnitureClock,
		FurnitureTV, FurnitureKyoudai, FurnitureHondana, FurnitureTansu
	}
	public enum BGMode
	{
		BGNone,
		ForestDay1, ForestNight1, ForestBad1, ForestDay2, ForestNight2, ForestBad2,
		RiverDay1, RiverNight1, RiverBad1, RiverDay2, RiverNight2, RiverBad2,
		TownDay1, TownNight1, TownBad1, TownDay2, TownNight2, TownBad2,
		RoomDay1, RoomNight1, RoomBad1, RoomDay2, RoomNight2, RoomBad2
	}

	/// <summary>
	/// Animal ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class Obj : Base
	{
		public String ObjName;
		public int Y, X;
		public Obj(int X, int Y)
		{
			ObjInit(X, Y, "");
		}

		public Obj(int X, int Y, int mode)
		{
			ObjInit(X, Y, State2ObjMode(mode).ToString());
		}

		public Obj(int bgmode)
		{
			ObjInit( 182, 267, State2BGMode(bgmode).ToString());
		}

		public Obj(int X, int Y, String mode)
		{
			ObjInit(X, Y, mode);
		}

		private void ObjInit(int X, int Y,  String objName)
		{
			this.X = X;
			this.Y = Y;
			this.ObjName = objName;
		}

		public override string ToString()
		{
			return this.ObjName;									//óÕãZ
		}

		ObjMode objmode;
		public ObjMode State2ObjMode(int state)
		{
			switch(state)
			{
				case 0:
					objmode = muphic.MakeStory.ObjMode.None;
					break;
				case 1:
					objmode = muphic.MakeStory.ObjMode.ManFrontGlad;
					break;
				case 2:
					objmode = muphic.MakeStory.ObjMode.ManFrontAngry;
					break;
				case 3:
					objmode = muphic.MakeStory.ObjMode.ManFrontSad;
					break;
				case 4:
					objmode = muphic.MakeStory.ObjMode.ManFrontEnjoy;
					break;
				case 5:
					objmode = muphic.MakeStory.ObjMode.ManLeftGlad;
					break;
				case 6:
					objmode = muphic.MakeStory.ObjMode.ManLeftAngry;
					break;
				case 7:
					objmode = muphic.MakeStory.ObjMode.ManLeftSad;
					break;
				case 8:
					objmode = muphic.MakeStory.ObjMode.ManLeftEnjoy;
					break;
				case 9:
					objmode = muphic.MakeStory.ObjMode.ManRightGlad;
					break;
				case 10:
					objmode = muphic.MakeStory.ObjMode.ManRightAngry;
					break;
				case 11:
					objmode = muphic.MakeStory.ObjMode.ManRightSad;
					break;
				case 12:
					objmode = muphic.MakeStory.ObjMode.ManRightEnjoy;
					break;
				case 13:
					objmode = muphic.MakeStory.ObjMode.ManBack;
					break;
				case 14:
					objmode = muphic.MakeStory.ObjMode.LadyFrontGlad;
					break;
				case 15:
					objmode = muphic.MakeStory.ObjMode.LadyFrontAngry;
					break;
				case 16:
					objmode = muphic.MakeStory.ObjMode.LadyFrontSad;
					break;
				case 17:
					objmode = muphic.MakeStory.ObjMode.LadyFrontEnjoy;
					break;
				case 18:
					objmode = muphic.MakeStory.ObjMode.LadyLeftGlad;
					break;
				case 19:
					objmode = muphic.MakeStory.ObjMode.LadyLeftAngry;
					break;
				case 20:
					objmode = muphic.MakeStory.ObjMode.LadyLeftSad;
					break;
				case 21:
					objmode = muphic.MakeStory.ObjMode.LadyLeftEnjoy;
					break;
				case 22:
					objmode = muphic.MakeStory.ObjMode.LadyRightGlad;
					break;
				case 23:
					objmode = muphic.MakeStory.ObjMode.LadyRightAngry;
					break;
				case 24:
					objmode = muphic.MakeStory.ObjMode.LadyRightSad;
					break;
				case 25:
					objmode = muphic.MakeStory.ObjMode.LadyRightEnjoy;
					break;
				case 26:
					objmode = muphic.MakeStory.ObjMode.LadyBack;
					break;
				case 27:
					objmode = muphic.MakeStory.ObjMode.GirlFrontGlad;
					break;
				case 28:
					objmode = muphic.MakeStory.ObjMode.GirlFrontAngry;
					break;
				case 29:
					objmode = muphic.MakeStory.ObjMode.GirlFrontSad;
					break;
				case 30:
					objmode = muphic.MakeStory.ObjMode.GirlFrontEnjoy;
					break;
				case 31:
					objmode = muphic.MakeStory.ObjMode.GirlLeftGlad;
					break;
				case 32:
					objmode = muphic.MakeStory.ObjMode.GirlLeftAngry;
					break;
				case 33:
					objmode = muphic.MakeStory.ObjMode.GirlLeftSad;
					break;
				case 34:
					objmode = muphic.MakeStory.ObjMode.GirlLeftEnjoy;
					break;
				case 35:
					objmode = muphic.MakeStory.ObjMode.GirlRightGlad;
					break;
				case 36:
					objmode = muphic.MakeStory.ObjMode.GirlRightAngry;
					break;
				case 37:
					objmode = muphic.MakeStory.ObjMode.GirlRightSad;
					break;
				case 38:
					objmode = muphic.MakeStory.ObjMode.GirlRightEnjoy;
					break;
				case 39:
					objmode = muphic.MakeStory.ObjMode.GirlBack;
					break;
				case 40:
					objmode = muphic.MakeStory.ObjMode.BoyFrontGlad;
					break;
				case 41:
					objmode = muphic.MakeStory.ObjMode.BoyFrontAngry;
					break;
				case 42:
					objmode = muphic.MakeStory.ObjMode.BoyFrontSad;
					break;
				case 43:
					objmode = muphic.MakeStory.ObjMode.BoyFrontEnjoy;
					break;
				case 44:
					objmode = muphic.MakeStory.ObjMode.BoyLeftGlad;
					break;
				case 45:
					objmode = muphic.MakeStory.ObjMode.BoyLeftAngry;
					break;
				case 46:
					objmode = muphic.MakeStory.ObjMode.BoyLeftSad;
					break;
				case 47:
					objmode = muphic.MakeStory.ObjMode.BoyLeftEnjoy;
					break;
				case 48:
					objmode = muphic.MakeStory.ObjMode.BoyRightGlad;
					break;
				case 49:
					objmode = muphic.MakeStory.ObjMode.BoyRightAngry;
					break;
				case 50:
					objmode = muphic.MakeStory.ObjMode.BoyRightSad;
					break;
				case 51:
					objmode = muphic.MakeStory.ObjMode.BoyRightEnjoy;
					break;
				case 52:
					objmode = muphic.MakeStory.ObjMode.BoyBack;
					break;
				case 53:
					objmode = muphic.MakeStory.ObjMode.WolfFrontGlad;
					break;
				case 54:
					objmode = muphic.MakeStory.ObjMode.WolfFrontAngry;
					break;
				case 55:
					objmode = muphic.MakeStory.ObjMode.WolfFrontSad;
					break;
				case 56:
					objmode = muphic.MakeStory.ObjMode.WolfFrontEnjoy;
					break;
				case 57:
					objmode = muphic.MakeStory.ObjMode.WolfLeftGlad;
					break;
				case 58:
					objmode = muphic.MakeStory.ObjMode.WolfLeftAngry;
					break;
				case 59:
					objmode = muphic.MakeStory.ObjMode.WolfLeftSad;
					break;
				case 60:
					objmode = muphic.MakeStory.ObjMode.WolfLeftEnjoy;
					break;
				case 61:
					objmode = muphic.MakeStory.ObjMode.WolfRightGlad;
					break;
				case 62:
					objmode = muphic.MakeStory.ObjMode.WolfRightAngry;
					break;
				case 63:
					objmode = muphic.MakeStory.ObjMode.WolfRightSad;
					break;
				case 64:
					objmode = muphic.MakeStory.ObjMode.WolfRightEnjoy;
					break;
				case 65:
					objmode = muphic.MakeStory.ObjMode.WolfBack;
					break;
				case 66:
					objmode = muphic.MakeStory.ObjMode.BearFrontGlad;
					break;
				case 67:
					objmode = muphic.MakeStory.ObjMode.BearFrontAngry;
					break;
				case 68:
					objmode = muphic.MakeStory.ObjMode.BearFrontSad;
					break;
				case 69:
					objmode = muphic.MakeStory.ObjMode.BearFrontEnjoy;
					break;
				case 70:
					objmode = muphic.MakeStory.ObjMode.BearLeftGlad;
					break;
				case 71:
					objmode = muphic.MakeStory.ObjMode.BearLeftAngry;
					break;
				case 72:
					objmode = muphic.MakeStory.ObjMode.BearLeftSad;
					break;
				case 73:
					objmode = muphic.MakeStory.ObjMode.BearLeftEnjoy;
					break;
				case 74:
					objmode = muphic.MakeStory.ObjMode.BearRightGlad;
					break;
				case 75:
					objmode = muphic.MakeStory.ObjMode.BearRightAngry;
					break;
				case 76:
					objmode = muphic.MakeStory.ObjMode.BearRightSad;
					break;
				case 77:
					objmode = muphic.MakeStory.ObjMode.BearRightEnjoy;
					break;
				case 78:
					objmode = muphic.MakeStory.ObjMode.BearBack;
					break;
				case 79:
					objmode = muphic.MakeStory.ObjMode.TurtleFrontGlad;
					break;
				case 80:
					objmode = muphic.MakeStory.ObjMode.TurtleFrontAngry;
					break;
				case 81:
					objmode = muphic.MakeStory.ObjMode.TurtleFrontSad;
					break;
				case 82:
					objmode = muphic.MakeStory.ObjMode.TurtleFrontEnjoy;
					break;
				case 83:
					objmode = muphic.MakeStory.ObjMode.TurtleLeftGlad;
					break;
				case 84:
					objmode = muphic.MakeStory.ObjMode.TurtleLeftAngry;
					break;
				case 85:
					objmode = muphic.MakeStory.ObjMode.TurtleLeftSad;
					break;
				case 86:
					objmode = muphic.MakeStory.ObjMode.TurtleLeftEnjoy;
					break;
				case 87:
					objmode = muphic.MakeStory.ObjMode.TurtleRightGlad;
					break;
				case 88:
					objmode = muphic.MakeStory.ObjMode.TurtleRightAngry;
					break;
				case 89:
					objmode = muphic.MakeStory.ObjMode.TurtleRightSad;
					break;
				case 90:
					objmode = muphic.MakeStory.ObjMode.TurtleRightEnjoy;
					break;
				case 91:
					objmode = muphic.MakeStory.ObjMode.TurtleBack;
					break;
				case 92:
					objmode = muphic.MakeStory.ObjMode.SparrowFrontGlad;
					break;
				case 93:
					objmode = muphic.MakeStory.ObjMode.SparrowFrontAngry;
					break;
				case 94:
					objmode = muphic.MakeStory.ObjMode.SparrowFrontSad;
					break;
				case 95:
					objmode = muphic.MakeStory.ObjMode.SparrowFrontEnjoy;
					break;
				case 96:
					objmode = muphic.MakeStory.ObjMode.SparrowLeftGlad;
					break;
				case 97:
					objmode = muphic.MakeStory.ObjMode.SparrowLeftAngry;
					break;
				case 98:
					objmode = muphic.MakeStory.ObjMode.SparrowLeftSad;
					break;
				case 99:
					objmode = muphic.MakeStory.ObjMode.SparrowLeftEnjoy;
					break;
				case 100:
					objmode = muphic.MakeStory.ObjMode.SparrowRightGlad;
					break;
				case 101:
					objmode = muphic.MakeStory.ObjMode.SparrowRightAngry;
					break;
				case 102:
					objmode = muphic.MakeStory.ObjMode.SparrowRightSad;
					break;
				case 103:
					objmode = muphic.MakeStory.ObjMode.SparrowRightEnjoy;
					break;
				case 104:
					objmode = muphic.MakeStory.ObjMode.SparrowBack;
					break;
				case 105:
					objmode = muphic.MakeStory.ObjMode.GoodsAmi;
					break;
				case 106:
					objmode = muphic.MakeStory.ObjMode.GoodsKago;
					break;
				case 107:
					objmode = muphic.MakeStory.ObjMode.GoodsMusi;
					break;
				case 108:
					objmode = muphic.MakeStory.ObjMode.GoodsUsa;
					break;
				case 109:
					objmode = muphic.MakeStory.ObjMode.GoodsRappa;
					break;
				case 110:
					objmode = muphic.MakeStory.ObjMode.GoodsDenwa;
					break;
				case 111:
					objmode = muphic.MakeStory.ObjMode.GoodsBall;
					break;
				case 112:
					objmode = muphic.MakeStory.ObjMode.GoodsCar;
					break;
				case 113:
					objmode = muphic.MakeStory.ObjMode.FashionCapB;
					break;
				case 114:
					objmode = muphic.MakeStory.ObjMode.FashionHatR;
					break;
				case 115:
					objmode = muphic.MakeStory.ObjMode.FashionCapG;
					break;
				case 116:
					objmode = muphic.MakeStory.ObjMode.FashionHatP;
					break;
				case 117:
					objmode = muphic.MakeStory.ObjMode.FashionMugi;
					break;
				case 118:
					objmode = muphic.MakeStory.ObjMode.FashionRibbon;
					break;
				case 119:
					objmode = muphic.MakeStory.ObjMode.FashionBagR;
					break;
				case 120:
					objmode = muphic.MakeStory.ObjMode.FashionBagP;
					break;
				case 121:
					objmode = muphic.MakeStory.ObjMode.FoodOnigiri;
					break;
				case 122:
					objmode = muphic.MakeStory.ObjMode.FoodHumburg;
					break;
				case 123:
					objmode = muphic.MakeStory.ObjMode.FoodPudding;
					break;
				case 124:
					objmode = muphic.MakeStory.ObjMode.FoodCocoa;
					break;
				case 125:
					objmode = muphic.MakeStory.ObjMode.FoodDogfood;
					break;
				case 126:
					objmode = muphic.MakeStory.ObjMode.FoodFish;
					break;
				case 127:
					objmode = muphic.MakeStory.ObjMode.FoodDongri;
					break;
				case 128:
					objmode = muphic.MakeStory.ObjMode.FoodApple;
					break;
				case 129:
					objmode = muphic.MakeStory.ObjMode.FurnitureChairL;
					break;
				case 130:
					objmode = muphic.MakeStory.ObjMode.FurnitureTable;
					break;
				case 131:
					objmode = muphic.MakeStory.ObjMode.FurnitureChairR;
					break;
				case 132:
					objmode = muphic.MakeStory.ObjMode.FurnitureClock;
					break;
				case 133:
					objmode = muphic.MakeStory.ObjMode.FurnitureTV;
					break;
				case 134:
					objmode = muphic.MakeStory.ObjMode.FurnitureKyoudai;
					break;
				case 135:
					objmode = muphic.MakeStory.ObjMode.FurnitureHondana;
					break;
				case 136:
					objmode = muphic.MakeStory.ObjMode.FurnitureTansu;
					break;
				default:
					objmode = muphic.MakeStory.ObjMode.None;
					break;
			}

			return objmode;
		}

		BGMode bgmode;
		public BGMode State2BGMode(int state)
		{
			switch(state)
			{
				case 0:
					bgmode = muphic.MakeStory.BGMode.BGNone;
					break;
				case 1:
					bgmode = muphic.MakeStory.BGMode.ForestDay1;
					break;
				case 2:
					bgmode = muphic.MakeStory.BGMode.ForestDay2;
					break;
				case 3:
					bgmode = muphic.MakeStory.BGMode.ForestNight1;
					break;
				case 4:
					bgmode = muphic.MakeStory.BGMode.ForestNight2;
					break;
				case 5:
					bgmode = muphic.MakeStory.BGMode.ForestBad1;
					break;
				case 6:
					bgmode= muphic.MakeStory.BGMode.ForestBad2;
					break;
				case 7:
					bgmode = muphic.MakeStory.BGMode.RiverDay1;
					break;
				case 8:
					bgmode = muphic.MakeStory.BGMode.RiverDay2;
					break;
				case 9:
					bgmode = muphic.MakeStory.BGMode.RiverNight1;
					break;
				case 10:
					bgmode = muphic.MakeStory.BGMode.RiverNight2;
					break;
				case 11:
					bgmode = muphic.MakeStory.BGMode.RiverBad1;
					break;
				case 12:
					bgmode = muphic.MakeStory.BGMode.RiverBad2;
					break;
				case 13:
					bgmode = muphic.MakeStory.BGMode.TownDay1;
					break;
				case 14:
					bgmode = muphic.MakeStory.BGMode.TownDay2;
					break;
				case 15:
					bgmode = muphic.MakeStory.BGMode.TownNight1;
					break;
				case 16:
					bgmode = muphic.MakeStory.BGMode.TownNight2;
					break;
				case 17:
					bgmode = muphic.MakeStory.BGMode.TownBad1;
					break;
				case 18:
					bgmode = muphic.MakeStory.BGMode.TownBad2;
					break;
				case 19:
					bgmode = muphic.MakeStory.BGMode.RoomDay1;
					break;
				case 20:
					bgmode = muphic.MakeStory.BGMode.RoomDay2;
					break;
				case 21:
					bgmode = muphic.MakeStory.BGMode.RoomNight1;
					break;
				case 22:
					bgmode = muphic.MakeStory.BGMode.RoomBad1;
					break;
				case 23:
					bgmode = muphic.MakeStory.BGMode.RoomBad2;
					break;
				default:
					bgmode = muphic.MakeStory.BGMode.BGNone;
					break;
			}
			return bgmode;
		}
	}
}
