using System;
using muphic.Link.RightButtons;

namespace muphic.Link
{
	public enum LinkButtonsClickMode {None, Sheep01, Sheep02, Sheep03, Sheep04, Sheep05, Sheep06, Sheep07, Sheep08, Sheep09, Sheep10, Cancel};
	/// <summary>
	/// LinkButtons の概要の説明です。
	/// </summary>
	public class LinkButtons : Screen
	{
		public LinkScreen parent;
		private LinkButtonsClickMode nowClick;
		private Sheep01Button sheep01;
		private Sheep02Button sheep02;
		private Sheep03Button sheep03;
		private Sheep04Button sheep04;
		private Sheep05Button sheep05;
		private Sheep06Button sheep06;
		private Sheep07Button sheep07;
		private Sheep08Button sheep08;
		private Sheep09Button sheep09;
		private Sheep10Button sheep10;

		private CancelButton cancel;

		public LinkButtonsClickMode NowClick
		{
			get
			{
				return nowClick;
			}
			set
			{
				nowClick = value;
				if(value == muphic.Link.LinkButtonsClickMode.None)
				{
					parent.tsuibi.Visible = false;
					parent.tsuibi.State = 11;
				}
				else
				{
					parent.tsuibi.Visible = false;
					switch(value)
					{
						case muphic.Link.LinkButtonsClickMode.Sheep01:
							parent.tsuibi.State = 0;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep02:
							parent.tsuibi.State = 1;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep03:
							parent.tsuibi.State = 2;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep04:
							parent.tsuibi.State = 3;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep05:
							parent.tsuibi.State = 4;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep06:
							parent.tsuibi.State = 5;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep07:
							parent.tsuibi.State = 6;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep08:
							parent.tsuibi.State = 7;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep09:
							parent.tsuibi.State = 8;
							break;
						case muphic.Link.LinkButtonsClickMode.Sheep10:
							parent.tsuibi.State = 9;
							break;
						case muphic.Link.LinkButtonsClickMode.Cancel:
							parent.tsuibi.State = 10;
							break;
						default:
							parent.tsuibi.State = 11;
							break;
					}
				}
			}
		}

		public LinkButtons(LinkScreen link)
		{
			parent = link;
	
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			sheep01 = new Sheep01Button(this);
			sheep02 = new Sheep02Button(this);
			sheep03 = new Sheep03Button(this);
			sheep04 = new Sheep04Button(this);
			sheep05 = new Sheep05Button(this);
			sheep06 = new Sheep06Button(this);
			sheep07 = new Sheep07Button(this);
			sheep08 = new Sheep08Button(this);
			sheep09 = new Sheep09Button(this);
			sheep10 = new Sheep10Button(this);
			cancel = new CancelButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			int space = 45;

			muphic.DrawManager.Regist(sheep01.ToString(), 905, 200+space*0, "image\\link\\button\\animal\\sheep\\sheep_01_off.png", "image\\link\\button\\animal\\sheep\\sheep_01_on.png");
			muphic.DrawManager.Regist(sheep02.ToString(), 905, 200+space*1, "image\\link\\button\\animal\\sheep\\sheep_02_off.png", "image\\link\\button\\animal\\sheep\\sheep_02_on.png");
			muphic.DrawManager.Regist(sheep03.ToString(), 905, 200+space*2, "image\\link\\button\\animal\\sheep\\sheep_03_off.png", "image\\link\\button\\animal\\sheep\\sheep_03_on.png");
			muphic.DrawManager.Regist(sheep04.ToString(), 905, 200+space*3, "image\\link\\button\\animal\\sheep\\sheep_04_off.png", "image\\link\\button\\animal\\sheep\\sheep_04_on.png");
			muphic.DrawManager.Regist(sheep05.ToString(), 905, 200+space*4, "image\\link\\button\\animal\\sheep\\sheep_05_off.png", "image\\link\\button\\animal\\sheep\\sheep_05_on.png");
			muphic.DrawManager.Regist(sheep06.ToString(), 905, 200+space*5, "image\\link\\button\\animal\\sheep\\sheep_06_off.png", "image\\link\\button\\animal\\sheep\\sheep_06_on.png");
			muphic.DrawManager.Regist(sheep07.ToString(), 905, 200+space*6, "image\\link\\button\\animal\\sheep\\sheep_07_off.png", "image\\link\\button\\animal\\sheep\\sheep_07_on.png");
			muphic.DrawManager.Regist(sheep08.ToString(), 905, 200+space*7, "image\\link\\button\\animal\\sheep\\sheep_08_off.png", "image\\link\\button\\animal\\sheep\\sheep_08_on.png");
			muphic.DrawManager.Regist(sheep09.ToString(), 905, 200+space*8, "image\\link\\button\\animal\\sheep\\sheep_09_off.png", "image\\link\\button\\animal\\sheep\\sheep_09_on.png");
			muphic.DrawManager.Regist(sheep10.ToString(), 905, 200+space*9, "image\\link\\button\\animal\\sheep\\sheep_10_off.png", "image\\link\\button\\animal\\sheep\\sheep_10_on.png");
			muphic.DrawManager.Regist(cancel.ToString(), 905, 200+space*10, "image\\link\\button\\modosu\\modosu_off.png", "image\\link\\button\\modosu\\modosu_on.png");


			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(sheep01);
			BaseList.Add(sheep02);
			BaseList.Add(sheep03);
			BaseList.Add(sheep04);
			BaseList.Add(sheep05);
			BaseList.Add(sheep06);
			BaseList.Add(sheep07);
			BaseList.Add(sheep08);
			BaseList.Add(sheep09);
			BaseList.Add(sheep10);
			//BaseList.Add(cancel);
		}

//		public override void Draw()
//		{
//			for (int i = 0; i < parent.buttonNum; i++)
//			{
//				String temp = "";
//				if (i == 9) temp = "10";
//				else temp = "0" + i;
//				muphic.DrawManager.Draw("muphic.Link.RightButtons.Sheep" + temp + "Button");
//			}
//		}

		public override void Click(System.Drawing.Point p)
		{
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).State = 0;							//本来のクリック処理を行う前に
			}															//すべての要素をクリックしていない状態に戻す
			parent.clear.State = 0;
			base.Click (p);
		}

		public void BaseState0()
		{
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).State = 0;							//すべての要素をクリックしていない状態に戻す
			}	
		}

		public void ButtonVisibleOn()
		{
			sheep01.Visible = true;
			sheep02.Visible = true;
			sheep03.Visible = true;
			sheep04.Visible = true;
			sheep05.Visible = true;
			sheep06.Visible = true;
			sheep07.Visible = true;
			sheep08.Visible = true;
			sheep09.Visible = true;
			sheep10.Visible = true;
		}

		public void ButtonVisibleOff(int mode)
		{
			if (mode <= 3)
			{
				sheep08.Visible = false;
				sheep09.Visible = false;
				sheep10.Visible = false;
			}

			if (mode <= 2)
			{
				sheep06.Visible = false;
				sheep07.Visible = false;
			}

			if (mode <= 1)
			{
				sheep04.Visible = false;
				sheep05.Visible = false;
			}
		}

		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).MouseLeave();
			}
		}
	}
}
