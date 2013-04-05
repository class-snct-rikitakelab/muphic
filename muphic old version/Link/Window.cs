using System;
using System.Drawing;
using muphic.Link.WindowParts;

namespace muphic.Link
{
	public enum WindowMode{Title};					//現在はタイトルしか存在しない…
	/// <summary>
	/// Window の概要の説明です。
	/// </summary>
	public class Window : Screen
	{
		LinkScreen parent;
		public Title title;
		public RoadThumb road;
		public SheepMini sheep;
		public QuestionNum qnum;

		public Window(LinkScreen link)
		{
			parent = link;
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			title = new Title(this);
			road = new RoadThumb(this);
			sheep = new SheepMini(this);
			qnum = new QuestionNum(this);


			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(title.ToString(), 875, 100, "image\\link\\parts\\title.png");
			muphic.DrawManager.Regist(road.ToString(), 876, 97, "image\\link\\parts\\road_thumb.png");
			muphic.DrawManager.Regist(sheep.ToString(), 0, 0, "image\\link\\parts\\sheepmini.png");
			muphic.DrawManager.Regist(qnum.ToString(), 800, 35, new String[7]
				{
					"image\\link\\dialog\\listen\\none.png", "image\\link\\dialog\\listen\\none.png",
					"image\\link\\dialog\\listen\\mondai2.png", "image\\link\\dialog\\listen\\mondai3.png",
					"image\\link\\dialog\\listen\\mondai4.png", "image\\link\\dialog\\listen\\mondai5.png",
					"image\\link\\dialog\\listen\\mondai6.png"
				});	

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			//BaseList.Add(title);
			//道と羊は動的なので登録なし
		}
		public override void Draw()
		{
			base.Draw ();
			muphic.DrawManager.DrawCenter(qnum.ToString(), qnum.State);
			if(parent.tsuibi.State < 11)
			{
				
				Point temp;
				if (parent.tsuibi.State < 10)
				{
					muphic.DrawManager.DrawCenter(road.ToString());
					for (int i = 0; i < 8; i++)
					{
						temp = parent.group.getPattern(parent.tsuibi.State, i);
						if (temp.Y == -1) break;
						muphic.DrawManager.DrawCenter(sheep.ToString(), 880+temp.X*14, 32+temp.Y*18);
					}
				}
				
			}
			else
			{
				//muphic.DrawManager.DrawCenter(title.ToString());
			}
		}
	}
}
