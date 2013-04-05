using System;
using System.Drawing;
using muphic.LinkMake;


namespace muphic
{
	public enum LinkMakeScreenMode{LinkMakeScreen, LoadDialog, SaveDialog, TitleDialog, ScoreScreen, AllClearDialog};
	/// <summary>
	/// LinkMakeScreen �̊T�v�̐����ł��B
	/// </summary>
	public class LinkMakeScreen : Screen
	{
		public TopScreen parent;
		public Score score;
		public House house;
		public Tempo tempo_l;
		public BackButton back;
		public RestartButton restart;
		public StartStopButton startstop;
		public ScoreButton scorebutton;
		public Window window;
		public LinkMakeButtons linkmakes;
		public Tsuibi tsuibi;

		public NewMakeButton newb;
		public SaveButton save;
		public LoadButton load;

		public String Title = "";
		public int filenum;

		public DataFileList dfList;

		// �ȉ��ǉ������t�B�[���h
		public Screen Screen;
		private LinkMakeScreenMode linkmakescreenmode;

		public String title { set {Title = value;} get { return Title;} }

		///////////////////////////////////////////////////////////////////////
		//�v���p�e�B�̐錾
		///////////////////////////////////////////////////////////////////////
		/// <summary>
		/// �ЂƂ�ŉ��y��ʂ�ScreenMode
		/// ��Ɋy����ʂւ̐؂�ւ��Ɏg�p
		/// </summary>
		public LinkMakeScreenMode LinkMakeScreenMode
		{
			get
			{
				return linkmakescreenmode;
			}
			set
			{
				linkmakescreenmode = value;
				switch(value)
				{
					case LinkMakeScreenMode.LinkMakeScreen:
						//�q�̃E�B���h�E�͕\�������A�������g��`�悷��
						Screen = null;
						break;
					case LinkMakeScreenMode.LoadDialog:
						//ScoreScreen���C���X�^���X�����A�������`�悷��
						Screen = new muphic.LinkMake.Dialog.Select.LinkSelectDialog(this);
						break;
					case LinkMakeScreenMode.SaveDialog:
						//�C���X�^���X�����A�������`�悷��
						Screen = new muphic.LinkMake.Dialog.Save.LinkSaveDialog(this);
						break;
					case LinkMakeScreenMode.ScoreScreen:
						//ScoreScreen���C���X�^���X�����A�������`�悷��
						Screen = new ScoreScreen(this);
						break;
					case LinkMakeScreenMode.AllClearDialog:
						Screen = new muphic.Common.AllClearDialog(this);
						break;
					default:
						//�Ƃ肠����LinkMakeScreen�ɕ`���߂�
						Screen = null;
						break;
				}
			}
		}

		public LinkMakeScreen(TopScreen link)
		{
			this.parent = link;
			this.LinkMakeScreenMode = muphic.LinkMakeScreenMode.LinkMakeScreen;
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			DrawManager.BeginRegist(55);
			score = new Score(this);
			house = new House(this);
			tempo_l = new Tempo(this);
			back = new BackButton(this);
			restart = new RestartButton(this);
			startstop = new StartStopButton(this);
			scorebutton = new ScoreButton(this);
			window = new Window(this);
			linkmakes = new LinkMakeButtons(this);
			tsuibi = new Tsuibi(this);

			newb = new NewMakeButton(this);
			save = new SaveButton(this);
			load = new LoadButton(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist("test", 0, 0, "image\\link\\linkQXGA.png");
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\link\\link2.png");
			muphic.DrawManager.Regist(score.ToString(), 102, 180, "image\\one\\parts\\road.png");
			muphic.DrawManager.Regist(house.ToString(), 15, 137, "image\\one\\parts\\house.png");
			muphic.DrawManager.Regist(tempo_l.ToString(), 120, 120, "image\\one\\button\\tempo\\tempo.png");
			muphic.DrawManager.Regist(back.ToString(), 18, 18, "image\\one\\button\\back\\back_off.png", "image\\one\\button\\back\\back_on.png");
			muphic.DrawManager.Regist(restart.ToString(), 270, 12, "image\\one\\button\\restart\\restart_off.png", "image\\one\\button\\restart\\restart_on.png");
			muphic.DrawManager.Regist(startstop.ToString(), 167, 12, new String[] {"image\\one\\button\\start_stop\\start_off.png", "image\\one\\button\\start_stop\\start_on.png", "image\\one\\button\\start_stop\\stop_off.png", "image\\one\\button\\start_stop\\stop_on.png"});
			muphic.DrawManager.Regist(scorebutton.ToString(), 814, 197, "image\\one\\button\\gakuhu\\gakuhu_off.png", "image\\one\\button\\gakuhu\\gakuhu_on.png");
			muphic.DrawManager.Regist(window.ToString(), 760, 10, "image\\link\\parts\\link_make_window.png");
			muphic.DrawManager.Regist(linkmakes.ToString(), 896, 190, "image\\one\\button\\animal\\buttons.png");//896,199 before 679,176
			muphic.DrawManager.Regist(tsuibi.ToString(), 0, 0, new String[3] 
					{
						"image\\MakeStory\\none.png",
						"image\\one\\button\\animal\\sheep\\sheep.png", "image\\common\\clear.png"});

			//muphic.DrawManager.Regist(newb.ToString(), 200, 10, "image\\link\\button\\make_off.png", "image\\link\\button\\make_on.png");
			muphic.DrawManager.Regist(load.ToString(), 529, 123, "image\\one\\button\\select\\select_off.png", "image\\one\\button\\select\\select_on.png");
			muphic.DrawManager.Regist(save.ToString(), 640, 122, "image\\one\\button\\save\\save_off.png", "image\\one\\button\\save\\save_on.png");

			DrawManager.EndRegist();
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(house);
			BaseList.Add(score);
			BaseList.Add(tempo_l);
			BaseList.Add(back);
			//BaseList.Add(newb);
			BaseList.Add(load);
			BaseList.Add(save);
			BaseList.Add(restart);
			BaseList.Add(startstop);
			BaseList.Add(scorebutton);
			BaseList.Add(window);
			BaseList.Add(linkmakes);							//tsuibi�͓o�^���Ȃ����Ƃɒ��ӁB

			//�t�@�C�����X�g�ǂݍ���
			dfList = new DataFileList();
			DataIndex di = (DataIndex)dfList.Index[dfList.Index.Count-1];
			this.filenum = di.Num+1;							//�����Ă�ꏊ�T���Ă�]�T�Ȃ�����Ƃ肠�����ő�l(��ŏC��)
		}

		public override void Click(System.Drawing.Point p)
		{
			switch(this.LinkMakeScreenMode)
			{
				case muphic.LinkMakeScreenMode.LinkMakeScreen:
					ClickLinkMakeScreen(p);
					break;
				case muphic.LinkMakeScreenMode.ScoreScreen:
					Screen.Click(p);
					break;
				default:
					Screen.Click(p);
					break;
			}
		}

		private void ClickLinkMakeScreen(Point p)
		{
			if(score.isPlay)						//----------���ˏI���̂��m�点----------
			{
				String[] LinkMakePermissionList = new String[] {startstop.ToString(), restart.ToString()};
				base.Click(p, LinkMakePermissionList);
			}
			else
			{
				base.Click(p);
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			switch(linkmakescreenmode)
			{
				case muphic.LinkMakeScreenMode.LinkMakeScreen:
					base.MouseMove (p);
					tsuibi.MouseMove(p);						//tsuibi��Base�^�����AMouseMove���K�v�Ȃ̂ŁA�Ă�
					break;
				case muphic.LinkMakeScreenMode.ScoreScreen:
					Screen.MouseMove(p);
					break;
				default:
					Screen.MouseMove(p);
					break;
			}
		}

		public override void DragBegin(System.Drawing.Point begin)
		{
			switch(this.LinkMakeScreenMode)
			{
				case muphic.LinkMakeScreenMode.LinkMakeScreen:
					if(score.isPlay == false)
					{
						base.DragBegin(begin);
					}
					break;
				case muphic.LinkMakeScreenMode.ScoreScreen:
					Screen.DragBegin(begin);
					break;
				default:
					Screen.DragBegin(begin);
					break;
			}
		}

		public override void DragEnd(Point begin, Point p)
		{
			switch(this.LinkMakeScreenMode)
			{
				case muphic.LinkMakeScreenMode.LinkMakeScreen:
					if(score.isPlay == false)
					{
						base.DragEnd(begin, p);
					}
					break;
				case muphic.LinkMakeScreenMode.ScoreScreen:
					Screen.DragEnd(begin, p);
					break;
				default:
					Screen.DragEnd(begin, p);
					break;
			}
		}

		public override void Drag(Point begin, Point p)
		{
			switch(this.LinkMakeScreenMode)
			{
				case muphic.LinkMakeScreenMode.LinkMakeScreen:
					if(score.isPlay == false)
					{
						base.Drag(begin, p);
					}
					break;
				case muphic.LinkMakeScreenMode.ScoreScreen:
					Screen.Drag(begin, p);
					break;
				default:
					Screen.Drag(begin, p);
					break;
			}
		}

		public override void Draw()
		{
			switch(linkmakescreenmode)
			{
				case muphic.LinkMakeScreenMode.LinkMakeScreen:
					base.Draw ();
					if(tsuibi.Visible)
					{
						muphic.DrawManager.DrawCenter(tsuibi.ToString(), tsuibi.point.X, tsuibi.point.Y, tsuibi.State);
						//tsuibi��BaseList�ɓo�^���ĂȂ��̂ŁA�ʂɕ`�悷��K�v������
					}
					break;
				case muphic.LinkMakeScreenMode.ScoreScreen:
					Screen.Draw();
					break;
				default:
					base.Draw();
					Screen.Draw();
					break;
			}
		}
	}
}
