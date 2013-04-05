using System;
using System.Drawing;


namespace muphic
{
	public enum LinkTopScreenMode{LinkTopScreen, PlayScreen, MakeScreen};
	/// <summary>
	/// SelectDialog �̊T�v�̐����ł��B
	/// </summary>
	public class LinkTop : Screen
	{
		public TopScreen parent;
		
		public LinkBackButton back;
		public LinkMakeButton make;
		public LinkPlayButton play;

		public Screen Screen;
		private LinkTopScreenMode screenmode;

		///////////////////////////////////////////////////////////////////////
		//�v���p�e�B�̐錾
		///////////////////////////////////////////////////////////////////////
		public LinkTopScreenMode ScreenMode
		{
			get
			{
				return screenmode;
			}
			set
			{
				screenmode = value;
				switch(value)
				{
					case muphic.LinkTopScreenMode.LinkTopScreen:
						Screen = null;
						break;
					case muphic.LinkTopScreenMode.MakeScreen:
						Screen = null;									//�q�̃E�B���h�E�͕\�������A�������g��`�悷��
						break;
					case muphic.LinkTopScreenMode.PlayScreen:
						parent.Screen = new LinkScreen(parent);					//LinkMakeScreen���C���X�^���X�����A�������`�悷��
						break;
					default:
						Screen = null;									//�Ƃ肠����TopScreen�ɕ`���߂�
						break;
				}
			}
		}

		public LinkTop(TopScreen top)
		{
			this.parent = top;
			
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			//DrawManager.BeginRegist(7);

			make = new LinkMakeButton(this);
			back = new LinkBackButton(this);
			play = new LinkPlayButton(this);


			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\link\\top\\TopLink_bak.png");
            muphic.DrawManager.Regist(play.ToString(), 560, 480, "image\\link\\top\\button\\A_off.png", "image\\link\\top\\button\\A_on.png");
			muphic.DrawManager.Regist(make.ToString(), 240, 480, "image\\link\\top\\button\\Q_off.png", "image\\link\\top\\button\\Q_on.png");
			muphic.DrawManager.Regist(back.ToString(), 430, 655, "image\\link\\top\\button\\back_off.png", "image\\link\\top\\button\\back_on.png");

			//DrawManager.EndRegist();
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////

			BaseList.Add(back);
			BaseList.Add(make);
			BaseList.Add(play);

		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}

		public override void Draw()
		{
			base.Draw ();
		}

	}
}