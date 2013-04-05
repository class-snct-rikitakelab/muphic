using System;
using muphic;
using muphic.Titlemode;

namespace muphic.Titlemode
{
	public class CharScreen : Screen
	{
		//public TitleScreen parent;
		//public AlphaScreen alphascrn;
		//public HiraScreen hirascrn;
		//public KataScreen katascrn;
		//public NumScreen numscrn;

		//public CharScreen(TitleScreen parent)
		public CharScreen()
		{
			//this.parent = parent;
			//alphascrn = new AlphaScreen(parent);
			//hirascrn = new HiraScreen(parent);
			//katascrn = new KataScreen(parent);
			//numscrn = new NumScreen(parent);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////

			//parent.InputMode = muphic.Titlemode.InputMode.Hira;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click(p);
			//if(parent.InputMode == muphic.Titlemode.InputMode.Alpha)
			//{
			//    alphascrn.Click(p);
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Hira)
			//{
			//    hirascrn.Click(p);
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Kata)
			//{
			//    katascrn.Click(p);
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Num)
			//{
			//    numscrn.Click(p);
			//}
			//else
			//{
			//    base.Click (p);
			//}

		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove(p);
			//if(parent.InputMode == muphic.Titlemode.InputMode.Alpha)
			//{
			//    alphascrn.MouseMove(p);
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Hira)
			//{
			//    hirascrn.MouseMove(p);
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Kata)
			//{
			//    katascrn.MouseMove(p);
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Num)
			//{
			//    numscrn.MouseMove(p);
			//}
			//else
			//{
			//    base.MouseMove(p);
			//}
		}

		public override void Draw()
		{
			base.Draw();
			//if(parent.InputMode == muphic.Titlemode.InputMode.Alpha)
			//{
			//    alphascrn.Draw();
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Hira)
			//{
			//    hirascrn.Draw();
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Kata)
			//{
			//    katascrn.Draw();
			//}
			//else if(parent.InputMode == muphic.Titlemode.InputMode.Num)
			//{
			//    numscrn.Draw();
			//}

		}
	}
}