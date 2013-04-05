using System;
using System.Drawing;
using muphic.Link;

namespace muphic
{
	public enum LinkScreenMode {LinkScreen, ScoreScreen, AnswerDialog, ListenDialog, SelectDialog, AllClearDialog};
	/// <summary>
	/// LinkScreen �̊T�v�̐����ł��B
	/// </summary>
	public class LinkScreen : Screen
	{
		public TopScreen parent;
		public Score score;
		public RightScrollButton right;
		public LeftScrollButton left;
		public House house;
		public BackButton back;
		public RestartButton restart;
		public StartStopButton startstop;
		public ScoreButton scorebutton;
		public Window window;
		public LinkButtons links;
		public ClearButton clear;
		public AllClearButton allclear;
		public Tsuibi tsuibi;
		public Titlebar titlebar;

		public muphic.Link.HouseLight light;

		public SelectButton select;
		public ListenButton listen;
		public AnswerButton answer;
		public GroupPattern group;
		public QuestionPattern quest;
		
		public SignBoard signboard;

		public muphic.Link.Dialog.Answer.AnswerCheck check;

		public LinkScreenMode linkscreenmode;
		public Screen Screen;

		private int questionNum;
		private int tempo;
		public int buttonNum = 10;

		public DataFileList dfList;

		public int Tempo
		{
			get
			{
				return tempo;
			}
			set
			{
				tempo = value;
				score.tempo = value;
				
			}
		}

		public LinkScreenMode LinkScreenMode
		{
			get
			{
				return linkscreenmode;
			}
			set
			{
				linkscreenmode = value;
				switch(linkscreenmode)
				{
					case LinkScreenMode.LinkScreen:
						this.Screen = null;
						break;
					case LinkScreenMode.ScoreScreen:
						Screen = new ScoreScreen(this);
						break;
					case LinkScreenMode.AnswerDialog:
						check = new muphic.Link.Dialog.Answer.AnswerCheck(this);
						break;
					case LinkScreenMode.ListenDialog:
						Screen = new muphic.Link.Dialog.Listen.ListenDialog(this);
						break;
					case LinkScreenMode.SelectDialog:
						Screen = new muphic.Link.Dialog.Select.SelectDialog(this);
						break;
					case LinkScreenMode.AllClearDialog:
						Screen = new muphic.Common.AllClearDialog(this);
						break;
				}
			}
		}

		public LinkScreen(TopScreen ts)
		{
			this.parent = ts;
			
			//this.Point = new Point(0,0);
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			DrawManager.BeginRegist(88);
			score = new Score(this);
			right = new RightScrollButton(this);
			left = new LeftScrollButton(this);
			house = new House(this);
			back = new BackButton(this);
			restart = new RestartButton(this);
			startstop = new StartStopButton(this);
			scorebutton = new ScoreButton(this);
			window = new Window(this);
			tsuibi = new Tsuibi(this);
			links = new LinkButtons(this);
			titlebar = new Titlebar(this);

			light = new muphic.Link.HouseLight(this);

			clear = new ClearButton(this);
			allclear = new AllClearButton(this);

			select = new SelectButton(this);
			listen = new ListenButton(this);
			answer = new AnswerButton(this);


			/////���p�^�[���ǂݍ��݁@���@�_�C�A���O�I�����Ɉꊇ�Ł[
			group = new GroupPattern();
			quest = new QuestionPattern();

			signboard = new SignBoard(this);

			//�f�[�^�t�@�C�����X�g
			dfList = new DataFileList();

			questionNum = 0;

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\link\\link.png");
			muphic.DrawManager.Regist(score.ToString(), 102, 180, "image\\link\\parts\\road.png");

			muphic.DrawManager.Regist(right.ToString(), 844, 695, "image\\link\\parts\\scroll\\next.png");
			muphic.DrawManager.Regist(left.ToString(), 116, 695, "image\\link\\parts\\scroll\\prev.png");
			muphic.DrawManager.Regist(house.ToString(), 15, 137, "image\\link\\parts\\house.png");
			muphic.DrawManager.Regist(back.ToString(), 21, 25, "image\\link\\button\\back\\back_off.png", "image\\link\\button\\back\\back_on.png");
			muphic.DrawManager.Regist(restart.ToString(), 300, 12, "image\\link\\button\\restart\\restart_off.png", "image\\link\\button\\restart\\restart_on.png");
			muphic.DrawManager.Regist(startstop.ToString(), 200, 13, new String[] {"image\\link\\button\\start_stop\\start_off.png", "image\\one\\button\\start_stop\\start_on.png", "image\\link\\button\\start_stop\\stop_off.png", "image\\link\\button\\start_stop\\stop_on.png"});
			muphic.DrawManager.Regist(scorebutton.ToString(), 814, 197,/*555, 120,*/ "image\\link\\button\\gakuhu\\gakuhu_off.png", "image\\link\\button\\gakuhu\\gakuhu_on.png");
			muphic.DrawManager.Regist(window.ToString(), 749, 15, "image\\link\\parts\\link_ans_window.png");
			muphic.DrawManager.Regist(links.ToString(), 900, 198, "image\\link\\button\\animal\\buttons.png");
			muphic.DrawManager.Regist(titlebar.ToString(), 410, 40, "image\\link\\titlebar.png");

			muphic.DrawManager.Regist(clear.ToString(), 287, 194, "image\\link\\button\\score\\clear_off.png", "image\\link\\button\\score\\clear_on.png");
			muphic.DrawManager.Regist(allclear.ToString(), 159, 194, "image\\link\\button\\score\\allclear_off.png", "image\\link\\button\\score\\allclear_on.png");

			muphic.DrawManager.Regist(light.ToString(), 0, 0, "image\\link\\parts\\light.png");

			muphic.DrawManager.Regist(select.ToString(), 150, 125, "image\\link\\button\\select\\select_off.png", "image\\link\\button\\select\\select_on.png");
			muphic.DrawManager.Regist(listen.ToString(), 250, 125, "image\\link\\button\\listen\\listen_off.png", "image\\link\\button\\listen\\listen_on.png");
			muphic.DrawManager.Regist(answer.ToString(), 350, 125, "image\\link\\button\\answer\\answer_off.png", "image\\link\\button\\answer\\answer_on.png");

			//-------------------�v����---------------------
			muphic.DrawManager.Regist(tsuibi.ToString(), 0, 0, new String[12] 
					{
						"image\\link\\button\\animal\\sheep\\sheep_01.png", "image\\link\\button\\animal\\sheep\\sheep_02.png",
						"image\\link\\button\\animal\\sheep\\sheep_03.png", "image\\link\\button\\animal\\sheep\\sheep_04.png",
						"image\\link\\button\\animal\\sheep\\sheep_05.png", "image\\link\\button\\animal\\sheep\\sheep_06.png",
						"image\\link\\button\\animal\\sheep\\sheep_07.png", "image\\link\\button\\animal\\sheep\\sheep_08.png",
						"image\\link\\button\\animal\\sheep\\sheep_09.png", "image\\link\\button\\animal\\sheep\\sheep_10.png",
						"image\\link\\button\\modosu\\modosu_all.png", "image\\link\\button\\none\\none.png"
					});
			//----------------------------------------------
			
			DrawManager.EndRegist();

			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(score);
			BaseList.Add(signboard);
			BaseList.Add(right);
			BaseList.Add(left);
			//BaseList.Add(house);
			BaseList.Add(back);
			BaseList.Add(restart);
			BaseList.Add(startstop);
			BaseList.Add(scorebutton);
			BaseList.Add(window);
			BaseList.Add(links);							//tsuibi�͓o�^���Ȃ����Ƃɒ��ӁB
			BaseList.Add(titlebar);

			BaseList.Add(clear);
			BaseList.Add(allclear);

			BaseList.Add(select);
			BaseList.Add(listen);
			BaseList.Add(answer);

			//Screen = new muphic.Link.Dialog.Select.SelectDialog(this);
			this.LinkScreenMode = LinkScreenMode.SelectDialog;
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			if(this.Screen == null)
			{
				base.MouseMove (p);
				tsuibi.MouseMove(p);						//tsuibi��Base�^�����AMouseMove���K�v�Ȃ̂ŁA�Ă�
			}
			else
			{
				Screen.MouseMove(p);							//null����Ȃ��Ȃ�A������̂ق���MouseMove���Ă�
			}
		}

		public override void Draw()
		{
			if (this.Screen == null)	
			{
				base.Draw ();
				if (tsuibi.Visible)
				{
					Point temp;
					if (tsuibi.State < 10)
					{
						Point temp2 = new Point();
						temp2.X = score.nowPlace;
						temp2.Y = 0;
						if (tsuibi.point.X > 360) temp2.X += 8;
						if (tsuibi.point.X > 600) temp2.X += 8;
						if (temp2.X % 8 != 0)
						{
							temp2.X = temp2.X + (8 - (temp2.X % 8));
						}
						Point temp3 = score.ScoretoPointRelative(temp2.X, temp2.Y);
						//if (!score.putFlag[temp2.X/8])
						{
							for (int i = 0; i < 8; i++)
							{
								temp = group.getPattern(tsuibi.State, i);
								if (temp.Y == -1) break;
								muphic.DrawManager.DrawCenter(tsuibi.ToString(), temp3.X+(temp.X+3)*30, 308+temp.Y*50, tsuibi.State);
							}
						}
					}
					else
					{
						Point temp2 = new Point();
						temp2.X = score.nowPlace;
						temp2.Y = 0;
						if (tsuibi.point.X > 360) temp2.X += 8;
						if (tsuibi.point.X > 600) temp2.X += 8;
						if (temp2.X % 8 != 0)
						{
							temp2.X = temp2.X + (8 - (temp2.X % 8));
						}
						Point temp3 = score.ScoretoPointRelative(temp2.X, temp2.Y);
						if (score.putFlag[temp2.X/8])
						{
							muphic.DrawManager.DrawCenter(tsuibi.ToString(), temp3.X+98, 490, tsuibi.State);
						}
						//muphic.DrawManager.DrawCenter(tsuibi.ToString(), tsuibi.point.X, tsuibi.point.Y, tsuibi.State);
					}
				}
			}
			else
			{
				base.Draw();
				Screen.Draw();									
			}
			titlebar.Draw();
		}

		public override void Click(System.Drawing.Point p)
		{
			if(this.Screen == null)								//Screen��null�Ȃ�TopScreen��Click���Ă�
			{
				base.Click (p);
			}
			else
			{
				Screen.Click(p);								//null����Ȃ��Ȃ�A������̂ق���Click���Ă�
			}
		}

		public override void DragBegin(System.Drawing.Point begin)
		{
			if(this.Screen == null)								//Screen��null�Ȃ�TopScreen��Click���Ă�
			{
				if(score.isPlay == false)
				{
					base.DragBegin(begin);
				}
			}
			else
			{
				Screen.DragBegin(begin);								//null����Ȃ��Ȃ�A������̂ق���Click���Ă�
			}
		}

		public override void DragEnd(System.Drawing.Point begin, System.Drawing.Point p)
		{
			if(this.Screen == null)								//Screen��null�Ȃ�TopScreen��Click���Ă�
			{
				if(score.isPlay == false)
				{
					base.DragEnd(begin, p);
				}
			}
			else
			{
				Screen.DragEnd(begin, p);								//null����Ȃ��Ȃ�A������̂ق���Click���Ă�
			}
		}

		public override void Drag(System.Drawing.Point begin, System.Drawing.Point p)
		{
			if(this.Screen == null)								//Screen��null�Ȃ�TopScreen��Click���Ă�
			{
				if(score.isPlay == false)
				{
					base.Drag(begin, p);
				}
			}
			else
			{
				Screen.Drag(begin, p);								//null����Ȃ��Ȃ�A������̂ق���Click���Ă�
			}
		}

		public int QuestionNum
		{
			get
			{
				return questionNum;
			}
			set
			{
				questionNum = value;
				window.qnum.State = value;
			}
		}

	}
}