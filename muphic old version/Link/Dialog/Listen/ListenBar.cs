using System;
using System.Collections;
using System.Drawing;

namespace muphic.Link.Dialog.Listen
{
	/// <summary>
	/// ListenSelect �̊T�v�̐����ł��B
	/// </summary>
	public class ListenBar : Screen
	{
		public ArrayList AnimalList;
		public ListenDialog parent;
		public bool isPlay;
		public bool isEnd;
		public int PlayOffset;
		public int MaxNum;
		public double Walk;
		public int PlayCount;
		public double lionPoint;
		public int lionCount; //�`���^�����O�L�����Z��

		public ListenBar(ListenDialog dia)
		{
			muphic.DrawManager.Regist("lion", 0, 0, "image\\link\\dialog\\listen\\lion.png");
			parent = dia;
			this.State = parent.parent.QuestionNum-1;
			AnimalList = parent.parent.quest.AnimalList;//ArrayList.Adapter(parent.parent.quest.Question[this.State]);
			isPlay = false;
			isEnd = false;
			MaxNum = 0;
			PlayCount = 0;
			for(int i=0; i < AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);
				if (MaxNum <= p.X)
				{
					MaxNum = p.X;
					//MaxNum = p.X;
				}
			}
			lionPoint = 0.0;
			lionCount = 0;
			Walk = 360.0/MaxNum;//(MaxNum)/370.0;
		}

		private void Playing()
		{
			int count;
			
			// �`���[�g���A�����s����FrameCount�̎Q�ƕ��@���ς��܂�
			if(muphic.Common.TutorialStatus.getIsTutorial()) count = this.parent.parent.parent.tutorialparent.parent.parent.parent.FrameCount;
			else count = parent.parent.parent.parent.FrameCount;
			
			this.PlayOffset += parent.parent.Tempo;
			
			for(int i=0; i < AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);	//�����̉��K�ƈʒu������W������o��
				p.X -= this.PlayOffset;									//�Đ����Ȃ̂ŁA�I�t�Z�b�g�������Ƃ�
				if(p.X < 0 || a.Visible == false)						//������ʊO�ɏo�Ă��邩�A�`��֎~�Ȃ�
				{
					continue;											//�����for�͔�΂�
				}
				else if(p.X <= 55)										//�����ƂɂԂ����Ă�����
				{
					muphic.SoundManager.Play("Sheep" + a.code + ".wav");			//�������炵�āc
					a.Visible = false;
					continue;											//����for��
				}
				else if(800 < p.X)										//�����A�܂��y���܂œ��B���Ă��Ȃ��Ȃ�
				{														//AnimalList�͏��Ԃǂ���ɕ���ł���̂ŁA���ꂩ����
					break;												//�y���܂œ��B���Ă��Ȃ����ƂɂȂ邩��for�����I������
				}
			}

			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalList�̍Ō�̗v�f�����o��

			if(!b.Visible)
			{
				this.isPlay = false;									//�����̍Ō�̗v�f���ƂɂԂ���I������
				//parent.listen.State = 0;								//�Đ��I��
				//this.isEnd = true;
				//parent.listen.State = 0;
				
				// �`���[�g���A�����s���ŁA����̑ҋ@��Ԃ������ꍇ
//				if(muphic.Common.TutorialStatus.getIsTutorial() && muphic.Common.TutorialStatus.getNextStateStandBy())
//				{
//					// �X�e�[�g�i�s
//					parent.parent.parent.tutorialparent.NextState();
//				}
				
				for (int i = 0; i < AnimalList.Count; i++)
				{
					Animal a = (Animal)AnimalList[i];
					a.Visible = true;
				}
			}
		}

		public Point ScoretoPointRelative(int place, int code)
		{
			return muphic.Common.ScoreTools.ScoretoPoint(place, code);			//���Ǒ��ΓI�Ȉʒu������o���̂�
			//���ݕ\�����Ă�����W�̈�ԍ��[�̒l������
		}

		public override void Draw()
		{
			base.Draw ();
			//int temp = 228+112+(int)Math.Floor((PlayOffset/Walk)+0.5);
			int temp = 228+112 + (int)Math.Floor(lionPoint+0.5);
			int temp2 = 0;
			if (!isEnd && parent.listen.State == 1)
			{
				lionPoint += Walk*parent.parent.Tempo;
				//temp2 = lionPoint%16 < 8 ? 2 : -2;
				temp2 = lionCount%24 < 12 ? 2 : -2;
			}

			if(this.isPlay)
			{
				
				if (temp < 583+112)
				{
					Playing();
					muphic.DrawManager.DrawCenter("lion", temp, 331+84+temp2);
					PlayCount+=parent.parent.Tempo;
				}
				else
				{
					this.isPlay = false;
					this.isEnd = true;
					parent.listen.State = 0;
					muphic.DrawManager.DrawCenter("lion", 583+112, 331+84+temp2);
				}
				lionCount++;
			}
			else
			{
				if (!isEnd && temp < 583+112)
				{
					muphic.DrawManager.DrawCenter("lion", temp, 331+84+temp2);
					lionCount++;
				}
				else
				{
					isEnd = false;
					parent.listen.State = 0;
					muphic.DrawManager.DrawCenter("lion", 583+112, 331+84+temp2);
					PlayCount = 0;
					PlayOffset = 0;

					// �`���[�g���A�����s���ŁA����̑ҋ@��Ԃ������ꍇ
					if(muphic.Common.TutorialStatus.getIsTutorial() && muphic.Common.TutorialStatus.getNextStateStandBy())
					{
						// �X�e�[�g�i�s
						//parent.parent.tutorialparent.NextState();
						parent.parent.parent.tutorialparent.NextState();
					}
				}
			}
		}

	}
}