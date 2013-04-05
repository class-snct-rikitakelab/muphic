using System;
using System.Collections;
using System.Drawing;
using muphic.Common;

namespace muphic.MakeStory.Play
{
	/// <summary>
	/// BackButton の概要の説明です。
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
			return muphic.Common.ScoreTools.ScoretoPoint(place, code);			//結局相対的な位置を割り出すので
			//現在表示している座標の一番左端の値を引く
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

					Point p = this.ScoretoPointRelative(a.place, a.code);	//動物の音階と位置から座標を割り出す
					p.X -= this.PlayOffset;									//再生中なので、オフセットを引いとく
                    if (p.X < 55 && a.Visible == false)						//もし家を通り過ぎているなら
                    {
                        continue;											//今回のforは飛ばす
                    }
					else if(p.X <= 55)										//もし家にぶつかっていたら
					{
						muphic.SoundManager.Play(a.ToString() + a.code + ".wav");			//音だけ鳴らして…
						a.Visible = false;
						continue;											//次のforへ
                    }
                    else if (p.X < ScoreTools.score.Right)					//もし、道の中に動物が入ったのなら
                    {
                        a.Visible = true;									//表示させる
                    }
                    else if (ScoreTools.score.Right < p.X)					//もし、まだ楽譜まで到達していないなら
					{														//AnimalListは順番どおりに並んでいるので、これから先も
						break;												//楽譜まで到達していないことになるからfor文を終了する
					}
				}

                Animal b = (Animal)slide.AnimalList[slide.AnimalList.Count - 1];			//AnimalListの最後の要素を取り出す
                Point bp = this.ScoretoPointRelative(b.place, b.code);
                bp.X -= this.PlayOffset;
                if (!b.Visible && bp.X <= 55)
				{
					//this.isPlay = false;									//動物の最後の要素が家にぶつかり終えたら
					//parent.listen.State = 0;								//再生終了
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