using System;
using System.Collections;
using System.Drawing;

namespace muphic.Link.Dialog.Listen
{
	/// <summary>
	/// ListenSelect の概要の説明です。
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
		public int lionCount; //チャタリングキャンセル

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
			
			// チュートリアル実行中はFrameCountの参照方法が変わります
			if(muphic.Common.TutorialStatus.getIsTutorial()) count = this.parent.parent.parent.tutorialparent.parent.parent.parent.FrameCount;
			else count = parent.parent.parent.parent.FrameCount;
			
			this.PlayOffset += parent.parent.Tempo;
			
			for(int i=0; i < AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);	//動物の音階と位置から座標を割り出す
				p.X -= this.PlayOffset;									//再生中なので、オフセットを引いとく
				if(p.X < 0 || a.Visible == false)						//もし画面外に出ているか、描画禁止なら
				{
					continue;											//今回のforは飛ばす
				}
				else if(p.X <= 55)										//もし家にぶつかっていたら
				{
					muphic.SoundManager.Play("Sheep" + a.code + ".wav");			//音だけ鳴らして…
					a.Visible = false;
					continue;											//次のforへ
				}
				else if(800 < p.X)										//もし、まだ楽譜まで到達していないなら
				{														//AnimalListは順番どおりに並んでいるので、これから先も
					break;												//楽譜まで到達していないことになるからfor文を終了する
				}
			}

			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalListの最後の要素を取り出す

			if(!b.Visible)
			{
				this.isPlay = false;									//動物の最後の要素が家にぶつかり終えたら
				//parent.listen.State = 0;								//再生終了
				//this.isEnd = true;
				//parent.listen.State = 0;
				
				// チュートリアル実行中で、動作の待機状態だった場合
//				if(muphic.Common.TutorialStatus.getIsTutorial() && muphic.Common.TutorialStatus.getNextStateStandBy())
//				{
//					// ステート進行
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
			return muphic.Common.ScoreTools.ScoretoPoint(place, code);			//結局相対的な位置を割り出すので
			//現在表示している座標の一番左端の値を引く
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

					// チュートリアル実行中で、動作の待機状態だった場合
					if(muphic.Common.TutorialStatus.getIsTutorial() && muphic.Common.TutorialStatus.getNextStateStandBy())
					{
						// ステート進行
						//parent.parent.tutorialparent.NextState();
						parent.parent.parent.tutorialparent.NextState();
					}
				}
			}
		}

	}
}