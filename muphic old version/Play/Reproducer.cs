using System;
using System.Collections;
using System.Drawing;
using muphic.Common;

namespace muphic.MakeStory.Play
{
	/// <summary>
	/// BackButton �̊T�v�̐����ł��B
	/// </summary>
	public class Reproducer
	{
		PlayScreen parent;
		int NowPlayNum;
		public int PlayOffset;
		public Slide slide;

		int interval = 50;
		int intervalCount;
		int MaxSlide = 0;
		int tempo;

		public Reproducer(PlayScreen s)
		{
			parent = s;
			NowPlayNum = 0;
			slide = parent.parent.PictureStory.Slide[NowPlayNum];
			intervalCount = interval;
			tempo = slide.tempo;;
			for (int i = 0; i < muphic.StoryScreen.PageNum; i++)
			{
				if (parent.parent.PictureStory.Slide[i].AnimalList.Count > 0 || parent.parent.PictureStory.Slide[i].ObjList.Count > 0) MaxSlide = i;
			}
		}

		public void Draw()
		{
			tempo = slide.tempo;
			Obj haikei = (Obj)slide.haikei;
			DrawManager.Draw((slide.haikei.ToString()), haikei.X, haikei.Y+50);
			for (int i = 0; i < slide.ObjList.Count; i++)
			{
				Obj o = (Obj)(slide.ObjList[i]);
				DrawManager.DrawCenter((slide.ObjList[i].ToString()), o.X, o.Y+50);
			}

			DrawManager.DrawString(muphic.Common.CommonTools.StringCenter(slide.Sentence, 30), 213, 720);
		}

		public void Play()
		{
			Draw();
			Playing();
		}

		public Point ScoretoPointRelative(int place, int code)
		{
			return muphic.Common.ScoreTools.ScoretoPoint(place, code);			//���Ǒ��ΓI�Ȉʒu������o���̂�
			//���ݕ\�����Ă�����W�̈�ԍ��[�̒l������
		}

		private void Playing()
		{
			//int count = parent.parent.parent.parent.FrameCount;
			this.PlayOffset += tempo;
			if (slide.AnimalList.Count != 0)
			{
				for(int i=0; i < slide.AnimalList.Count; i++)
				{
					Animal a = (Animal)slide.AnimalList[i];

					Point p = this.ScoretoPointRelative(a.place, a.code);	//�����̉��K�ƈʒu������W������o��
					p.X -= this.PlayOffset;									//�Đ����Ȃ̂ŁA�I�t�Z�b�g�������Ƃ�
                    if (p.X < 55 && a.Visible == false)						//�����Ƃ�ʂ�߂��Ă���Ȃ�
                    {
                        continue;											//�����for�͔�΂�
                    }
					else if(p.X <= 55)										//�����ƂɂԂ����Ă�����
					{
						muphic.SoundManager.Play(a.ToString() + a.code + ".wav");			//�������炵�āc
						a.Visible = false;
						continue;											//����for��
                    }
                    else if (p.X < ScoreTools.score.Right)					//�����A���̒��ɓ������������̂Ȃ�
                    {
                        a.Visible = true;									//�\��������
                    }
                    else if (ScoreTools.score.Right < p.X)					//�����A�܂��y���܂œ��B���Ă��Ȃ��Ȃ�
					{														//AnimalList�͏��Ԃǂ���ɕ���ł���̂ŁA���ꂩ����
						break;												//�y���܂œ��B���Ă��Ȃ����ƂɂȂ邩��for�����I������
					}
				}

                Animal b = (Animal)slide.AnimalList[slide.AnimalList.Count - 1];			//AnimalList�̍Ō�̗v�f�����o��
                Point bp = this.ScoretoPointRelative(b.place, b.code);
                bp.X -= this.PlayOffset;
                if (!b.Visible && bp.X <= 55)
				{
					//this.isPlay = false;									//�����̍Ō�̗v�f���ƂɂԂ���I������
					//parent.listen.State = 0;								//�Đ��I��
					//this.isEnd = true;
					if (intervalCount == 0)
					{
						intervalCount = interval;
						for (int i = 0; i < slide.AnimalList.Count; i++)
						{
							Animal a = (Animal)slide.AnimalList[i];
							a.Visible = true;
						}
				
						if (NowPlayNum < muphic.StoryScreen.PageNum-1)
						{
							slide = parent.parent.PictureStory.Slide[++NowPlayNum];
							this.PlayOffset = 0;
						}
						else
						{
							parent.PlayFlag = false;
							slide = parent.parent.PictureStory.Slide[0];
							NowPlayNum = 0;
						}
					}
					else
					{
						intervalCount--;
					}
				}
			}
			else
			{
				if (intervalCount != 0)
				{
					intervalCount--;
				}
				else
				{
					intervalCount = interval;
					if (NowPlayNum < MaxSlide)
					{
						slide = parent.parent.PictureStory.Slide[++NowPlayNum];
						this.PlayOffset = 0;
					}
					else
					{
						parent.PlayFlag = false;
						slide = parent.parent.PictureStory.Slide[0];
						NowPlayNum = 0;
					}
				}				
			}
		}
	}
}