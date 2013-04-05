using System;
using System.Drawing;
using muphic.Link;

namespace muphic.MakeStory.Play
{
	/// <summary>
	/// PlayScreen �̊T�v�̐����ł��B
	/// </summary>
	public class PlayScreen : Screen
	{
		public MakeStoryScreen parent;
		public PlayBackButton playback;
		public PlayStartButton playstart;
		public Curtain curtain;

		public Reproducer repro;
		public PictStory PictureStory;
		public Sentence sentence;

		public bool PlayFlag = false;

		public PlayScreen(MakeStoryScreen s)
		{
			this.parent = s;
			
			//this.Point = new Point(0,0);
			///////////////////////////////////////////////////////////////////
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			playback = new PlayBackButton(this);
			playstart = new PlayStartButton(this);
			curtain = new Curtain(this);
			sentence = new Sentence(this);

			repro = new Reproducer(this);

			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\story\\play\\story_play.png");
			//muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\story\\play\\background.png");
			muphic.DrawManager.Regist(playback.ToString(), 20, 0, "image\\story\\play\\button\\back_off.png", "image\\story\\play\\button\\back_on.png");
			muphic.DrawManager.Regist(playstart.ToString(), 860, 10, "image\\story\\play\\button\\start_off.png", "image\\story\\play\\button\\start_on.png");
			muphic.DrawManager.Regist(curtain.ToString(), 0, 150, "image\\story\\play\\curtain.png");
			muphic.DrawManager.Regist(sentence.ToString(), 144, 702, "image\\story\\play\\sentence.png");
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
			///////////////////////////////////////////////////////////////////
			BaseList.Add(playback);
			BaseList.Add(playstart);
			//BaseList.Add(curtain);
			//BaseList.Add(sentence);

			/////////////////////
			//�X���C�h�o�^
			/////////////////////
			//PictureStory = parent.PictureStory;
		}


		public override void Draw()
		{
			base.Draw();
			if (PlayFlag) repro.Play();
			muphic.DrawManager.Draw(curtain.ToString());
			muphic.DrawManager.Draw(sentence.ToString());
			muphic.DrawManager.DrawString(muphic.Common.CommonTools.StringCenter(parent.PictureStory.Title, 15), 360, 65);
		}

//		public override void Click(System.Drawing.Point p)
//		{
//			if(this.Screen == null)								//Screen��null�Ȃ�TopScreen��Click���Ă�
//			{
//				base.Click (p);
//			}
//			else
//			{
//				Screen.Click(p);								//null����Ȃ��Ȃ�A������̂ق���Click���Ă�
//			}
//		}
//
//		public int QuestionNum
//		{
//			get
//			{
//				return questionNum;
//			}
//			set
//			{
//				questionNum = value;
//				window.qnum.State = value;
//			}
//		}
	}
}