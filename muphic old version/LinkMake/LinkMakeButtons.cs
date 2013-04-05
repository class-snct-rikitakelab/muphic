using System;
using muphic.LinkMake.RightButtons;

namespace muphic.LinkMake
{
	public enum LinkMakeButtonsClickMode {None, Sheep, Cancel};//CancelはLinkMakeScreenの下にある
	/// <summary>
	/// LinkMakeButtons の概要の説明です。
	/// </summary>
	public class LinkMakeButtons : Screen
	{
		public LinkMakeScreen parent;
		private LinkMakeButtonsClickMode nowClick;
		private SheepButton sheep;


		public LinkMakeButtonsClickMode NowClick
		{
			get
			{
				return nowClick;
			}
			set
			{
				AllClear();
				nowClick = value;
				switch(value)
				{
					case muphic.LinkMake.LinkMakeButtonsClickMode.None:
						parent.tsuibi.State = 0;
						break;
					case muphic.LinkMake.LinkMakeButtonsClickMode.Sheep:
						parent.tsuibi.State = 1;
						break;
					case muphic.LinkMake.LinkMakeButtonsClickMode.Cancel:
						parent.tsuibi.State = 2;
						break;
					default:
						parent.tsuibi.State = 0;
						break;
				}
			}
		}

		public LinkMakeButtons(LinkMakeScreen link)
		{
			parent = link;
	
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			sheep = new SheepButton(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			int space = 76;//679,176 after896,199
			//before 690,175
			System.Drawing.Point p = new System.Drawing.Point(904, 201);
			muphic.DrawManager.Regist(sheep.ToString(), p.X, p.Y+space*0, "image\\one\\button\\animal\\sheep\\sheep_off.png", "image\\one\\button\\animal\\sheep\\sheep_on.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(sheep);
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}

		public void AllClear()
		{
			for(int i=0;i<BaseList.Count;i++)
			{
				((Base)BaseList[i]).State = 0;							//本来のクリック処理を行う前に
			}															//すべての要素をクリックしていない状態に戻す
			parent.score.clear.State = 0;
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
