using System;
using muphic.LinkMake.RightButtons;

namespace muphic.LinkMake
{
	public enum LinkMakeButtonsClickMode {None, Sheep, Cancel};//Cancel��LinkMakeScreen�̉��ɂ���
	/// <summary>
	/// LinkMakeButtons �̊T�v�̐����ł��B
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
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			sheep = new SheepButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			int space = 76;//679,176 after896,199
			//before 690,175
			System.Drawing.Point p = new System.Drawing.Point(904, 201);
			muphic.DrawManager.Regist(sheep.ToString(), p.X, p.Y+space*0, "image\\one\\button\\animal\\sheep\\sheep_off.png", "image\\one\\button\\animal\\sheep\\sheep_on.png");

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
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
				((Base)BaseList[i]).State = 0;							//�{���̃N���b�N�������s���O��
			}															//���ׂĂ̗v�f���N���b�N���Ă��Ȃ���Ԃɖ߂�
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
