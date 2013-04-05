using System;
using System.Drawing;
using muphic.Link.WindowParts;

namespace muphic.Link
{
	public enum WindowMode{Title};					//���݂̓^�C�g���������݂��Ȃ��c
	/// <summary>
	/// Window �̊T�v�̐����ł��B
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
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			title = new Title(this);
			road = new RoadThumb(this);
			sheep = new SheepMini(this);
			qnum = new QuestionNum(this);


			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
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
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			//BaseList.Add(title);
			//���Ɨr�͓��I�Ȃ̂œo�^�Ȃ�
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
