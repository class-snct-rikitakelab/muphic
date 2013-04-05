using System;
using System.Collections;
using System.Drawing;
using muphic.Common;

namespace muphic.Link
{
	/// <summary>
	/// Score の概要の説明です。
	/// </summary>
	public class Score : Screen
	{
		public ArrayList AnimalList;							//別にBaseListを使ってもいいが、ここはあえて別に宣言する
		public LinkScreen parent;
		public const int AnimalWidth = 76;				//動物の最大横幅
		public const int Kugiri = 2;					//動物の区切り。1だと4分のみ、2だと8分のみになる。
		//public const int MaxAnimals_perPage = 7;		//1ページあたりで1つの音階における4分音符の最大数(実験値)
		public int nowPlace;									//現在表示中の位置
		int defaultPlace;								//非再生時の表示位置(再生終了時ここの位置に戻る)
		int PlayOffset;									//再生中のオフセット。ピクセル単位
		public bool isPlay;								//再生中かどうかを表すフラグ

		private muphic.HouseLight houselight;


		//public ArrayList AnimalList;							//別にBaseListを使ってもいいが、ここはあえて別に宣言する
		public Animals Animals;
		public const int MaxAnimals_perPage = 12;		//1ページあたりで1つの音階における4分音符の最大数(実験値)
		public const int MaxAnimals = 24;				//1ページあたりで1つの音階における最大数(実験値、ただしこれはKugiri=2にしか通用しないことに注意)

		public bool answerCheckFlag = false;

		public int scoreLength = 0;
		public int nowScore = 0;
		public int tempo = 3;

		public bool[,] ribbon;

		public bool[] putFlag;

		public ScrollBar bar;

		public int barNum;

		public Score(LinkScreen link)
		{
			parent = link;
			AnimalList = new ArrayList();
			bar = new ScrollBar(this);
			defaultPlace = nowPlace = 0;
			this.isPlay = false;


			DrawManager.Regist("Sheep01", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_01.png");
			DrawManager.Regist("Sheep02", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_02.png");
			DrawManager.Regist("Sheep03", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_03.png");
			DrawManager.Regist("Sheep04", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_04.png");
			DrawManager.Regist("Sheep05", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_05.png");
			DrawManager.Regist("Sheep06", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_06.png");
			DrawManager.Regist("Sheep07", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_07.png");
			DrawManager.Regist("Sheep08", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_08.png");
			DrawManager.Regist("Sheep09", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_09.png");
			DrawManager.Regist("Sheep10", 0, 0, "image\\link\\button\\animal\\sheep\\sheep_10.png");

            DrawManager.Regist("ribbon1", 0, 0, "image\\link\\parts\\ribbon\\blue.png");
			DrawManager.Regist("ribbon2", 0, 0, "image\\link\\parts\\ribbon\\pink.png");
			DrawManager.Regist("ribbon3", 0, 0, "image\\link\\parts\\ribbon\\green.png");
			DrawManager.Regist("ribbon4", 0, 0, "image\\link\\parts\\ribbon\\purple.png");

			DrawManager.Regist(bar.ToString(), 145, 693, "image\\one\\parts\\scroll\\bars.png");

			BaseList.Add(bar);

			ribbon = new bool[10, 4];
			putFlag = new bool[100];
			for (int i = 0; i < 100; i++) putFlag[i] = false;
			scoreLength = 0;
			nowScore = 0;
			barNum = 0;

			houselight = new muphic.HouseLight();

		}

		/// <summary>
		/// 現在の楽譜内での相対的な位置を割り出す
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public Point PointtoScoreRelative(Point p)
		{
			Point pp = muphic.Common.ScoreTools.PointtoScore(p);				//まず絶対的な位置を割り出す。
			pp.X += this.nowPlace;									//現在表示している座標の一番左側の値を足す
			return pp;
		}

		/// <summary>
		/// 現在の楽譜内での相対的な座標を割り出す
		/// </summary>
		/// <param name="place"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public Point ScoretoPointRelative(int place, int code)
		{
			return muphic.Common.ScoreTools.ScoretoPoint(place-this.nowPlace, code);			//結局相対的な位置を割り出すので
			//現在表示している座標の一番左端の値を引く
		}

		public override void Draw()
		{
			base.Draw ();
			houselight.Draw();
			if(!Visible)
			{
				return;
			}

			if (!this.answerCheckFlag)
			{
				if(this.isPlay)
				{
					DrawPlaying();
				}
				else
				{
					DrawNotPlaying();
				}
			}
			else
			{
				AnswerCheck();
			}
		}

		/// <summary>
		/// 再生時の描画処理
		/// </summary>
		private void DrawPlaying()
		{
			int count;
			// チュートリアル実行中はFrameCountの参照方法が変わります
			if(muphic.Common.TutorialStatus.getIsTutorial()) count = this.parent.parent.tutorialparent.parent.parent.parent.FrameCount;
			else count = parent.parent.parent.FrameCount;
			
			//int count = parent.parent.parent.FrameCount;
			//int tempo = parent.tempo.TempoMode;
			this.PlayOffset += tempo;									//テンポの分だけオフセットを足しとく
			for(int i = 0; i < AnimalList.Count; i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);	//動物の音階と位置から座標を割り出す
				p.X -= this.PlayOffset;									//再生中なので、オフセットを引いとく
				//parent.light.Draw();
				if(p.X < 55 && a.Visible == false)						//もし家を通り過ぎているなら
				{
					continue;											//今回のforは飛ばす
				}
				else if(p.X <= 55)										//もし家にぶつかっていたら
				{
					muphic.SoundManager.Play("Sheep" + a.code + ".wav");			//音だけ鳴らして…
					//parent.light.Add(a.code);
					a.Visible = false;
					houselight.Add(a.code);	
					continue;											//次のforへ
				}
				else if(p.X <= ScoreTools.score.Right)					//もし、道の中に入っているなら
				{
					a.Visible = true;									//表示させる
				}
				else if(ScoreTools.score.Right < p.X)					//もし、まだ楽譜まで到達していないなら
				{														//AnimalListは順番どおりに並んでいるので、これから先も
					break;												//楽譜まで到達していないことになるからfor文を終了する
				}

				int spY = p.X % 40 < 20 ? p.Y + 2 : p.Y - 2;

				muphic.DrawManager.DrawCenter(a.ToString(), p.X, spY);	//これらの条件を満たしていなければ、普通に描画する
				switch (a.group)
				{
					case 0:
						DrawManager.DrawCenter("ribbon1", p.X-12, spY-7);
						break;
					case 1:
						DrawManager.DrawCenter("ribbon2", p.X-12, spY-7);
						break;
					case 2:
						DrawManager.DrawCenter("ribbon3", p.X-12, spY-7);
						break;
					case 3:
						DrawManager.DrawCenter("ribbon4", p.X-12, spY-7);
						break;
					default:
						break;
				}
			}
			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalListの最後の要素を取り出す
			Point bp = this.ScoretoPointRelative(b.place, b.code);
			bp.X -= this.PlayOffset;
			if(!b.Visible && bp.X <= 55)
			{
				this.PlayStop();										//動物の最後の要素が家にぶつかり終えたら
				parent.startstop.State = 0;								//再生終了
			}
		}

		/// <summary>
		/// 非再生時の描画処理
		/// </summary>
		private void DrawNotPlaying()
		{
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				Point p = this.ScoretoPointRelative(a.place, a.code);
				//System.Diagnostics.Debug.WriteLine(a.place);
				if(ScoreTools.inScore(p))								//道の内側にいれば
				{
					a.Visible = true;									//表示させる
				}
				else													//外側にいれば
				{
					a.Visible = false;									//表示させない
				}
				if(a.Visible)											//道の内側にいるときだけ
				{														//描画させる
					DrawManager.DrawCenter(AnimalList[i].ToString(), p.X, p.Y);
				
					switch (a.group)
					{
						case 0:
							DrawManager.DrawCenter("ribbon1", p.X-12, p.Y-7);
							break;
						case 1:
							DrawManager.DrawCenter("ribbon2", p.X-12, p.Y-7);
							break;
						case 2:
							DrawManager.DrawCenter("ribbon3", p.X-12, p.Y-7);
							break;
						case 3:
							DrawManager.DrawCenter("ribbon4", p.X-12, p.Y-7);
							break;
						default:
							break;
					}
				}
			}
		}

		public override void Click(System.Drawing.Point p)
		{
			//base.Click (p);								//元のクリック処理は行わないことに注意
			if(this.isPlay || parent.LinkScreenMode == muphic.LinkScreenMode.AnswerDialog)		//再生中、楽譜内の編集作業は許可しない
			{
				return;
			}

			Point temp3 = new Point();
			temp3.X = nowPlace;
			temp3.Y = 0;
			if (p.X > 360) temp3.X += 8;
			if (p.X > 600) temp3.X += 8;
			if (temp3.X % 8 != 0)
			{
				temp3.X = temp3.X + (8 - (temp3.X % 8));
			}

			if(parent.links.NowClick != muphic.Link.LinkButtonsClickMode.None)	//右のボタン群で何かを選択していれば以下を実行する
			{
				Point place = muphic.Common.ScoreTools.DecidePlace(p);			//楽譜内での位置と音階を決定する
				place.X += this.nowPlace;
				//if (p.X > 398) place.X += 8;
				System.Diagnostics.Debug.WriteLine(place.Y, "音階");
				System.Diagnostics.Debug.WriteLine(place.X, "位置");

				if(place.X == 0 && place.Y == 0)								//DebicePlaceが楽譜外(もしくはそれに
				{																//そうとうするもの)だと判断した
					return;
				}
				

				if(parent.links.NowClick == muphic.Link.LinkButtonsClickMode.Cancel)//キャンセルボタンがクリックされていたら
				{
					int temp = temp3.X;//(place.X / 8)*8;
					for (int i = 0; i < 8; i++)
					{
						for (int j = 0; j < 3; j++) //和音対策で3回実行
						{
							bool b = this.Delete(temp+i);						//削除処理を実行
							System.Diagnostics.Debug.WriteLine(b, "Delete");
						}
					}
					putFlag[temp3.X/8] = false;
				}
				else															//ほかの動物がクリックされていたら
				{
					int rib = 0;
					bool insert = false;

					for (int i = 0; i < 4; i++)
					{
						if (!ribbon[parent.tsuibi.State, i])
						{
							rib = i;
							break;
						}
					}

					Point temp2 = new Point();
					temp2.X = this.nowPlace;
					temp2.Y = 0;
					if (p.X > 360) temp2.X += 8;
					if (p.X > 600) temp2.X += 8;
					if (temp2.X % 8 != 0)
					{
						temp2.X = temp2.X + (8 - (temp2.X % 8));
					}
					//temp2 = this.ScoretoPointRelative(temp2.X, temp2.Y);
					
					//すでにグループが配置されていたら消す(上書き機能)
					if (putFlag[temp2.X/8])
					{
						int temp = temp3.X;//(place.X / 8)*8;
						for (int i = 0; i < 8; i++)
						{
							for (int j = 0; j < 3; j++) //和音対策で3回実行
							{
								bool b = this.Delete(temp+i);						//削除処理を実行
								System.Diagnostics.Debug.WriteLine(b, "Delete");
							}
						}
						putFlag[temp3.X/8] = false;
					}
					//if (!putFlag[temp2.X/8])
					{
						for (int i = 0; i < 8; i++)
						{
							bool b;
							Point temp;
							temp = parent.group.getPattern(parent.tsuibi.State, i);
							if (temp.Y == -1) break;
							b = this.Insert(temp2.X + 3+temp.X, temp.Y+1, rib);

							//						if (parent.tsuibi.point.X < 261)
							//						{
							//							b = this.Insert(3+temp.X, temp.Y+1, rib);
							//						}
							//						else if (parent.tsuibi.point.X > 489)
							//						{
							//							b = this.Insert(9+temp.X, temp.Y+1, rib);
							//						}
							//						else
							//						{
							//							b = this.Insert(place.X+temp.X, temp.Y+1, rib);
							//						}
							System.Diagnostics.Debug.WriteLine(b, "Insert");
							if (b) insert = true;
						}
						if (insert) putFlag[temp2.X/8] = true;
						scoreLength += 8;
					}

					if (insert) ribbon[parent.tsuibi.State, rib] = true;
				}
			}
		}

		/// <summary>
		/// 動物を新たに追加する
		/// </summary>
		/// <param name="place">(絶対的)位置</param>
		/// <param name="code">音階</param>
		/// <returns>成功したかどうか</returns>
		private bool Insert(int place, int code, int group)
		{
			int i;
			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.code == code && a.place == place)							//位置と音階がかぶってるものが存在する
				{
					return false;
				}
				if(a.place > place)												//設定する位置より遠いものが存在した
				{																//AnimalListは昇順にソートしてあるから、
					break;														//遠いものが現れたということはすでにplaceを
				}																//越しているので、かぶっていないことが確定する
			}
			Animal newAnimal = new Animal(place, code, group, parent.links.NowClick);	//Animalオブジェクトをインスタンス化
			AnimalList.Insert(i, newAnimal);									//遠いようになったところに割り込む
																				//こうすることによって昇順が保たれる
			return true;
		}

		/// <summary>
		/// 動物を削除する
		/// </summary>
		/// <param name="place">(絶対的)位置</param>
		/// <param name="code">音階</param>
		/// <returns>成功したかどうか</returns>
		private bool Delete(int place)
		{
			int i;
			for(i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
				if(a.place == place)							//位置と音階がかぶってるものが存在する
				{
					AnimalList.RemoveAt(i);
					return true;
				}
				if(a.place > place)												//削除する位置より遠いものが存在した
				{																//つまり、それ以上探しても意味がないため
					break;
				}
			}
			return false;
		}

		public void PlayAnswerCheck()
		{
			this.nowPlace = 0;
			this.PlayOffset = 0;
			for(int i=0;i<AnimalList.Count;i++)
			{
				((Animal)AnimalList[i]).Visible = true;							//再生途中だった場合を考えて
			}																	//visibleを復帰させる
			if(this.isPlay)
			{
				return;
			}
			parent.right.Visible = false;
			parent.left.Visible = false;
			//this.isPlay = true;
			answerCheckFlag = true;
		}

		public void PlayFirst()
		{
			this.nowPlace = 0;													//ページを強制的に最初に戻す
			this.PlayOffset = 0;												//再生途中だった場合、最初に戻す
			for(int i=0;i<AnimalList.Count;i++)
			{
				((Animal)AnimalList[i]).Visible = true;							//再生途中だった場合を考えて
			}																	//visibleを復帰させる
			this.PlayStart();
		}

		public void PlayStart()
		{

			if(this.isPlay)
			{
				return;
			}
			if(AnimalList.Count == 0)
			{
				PlayStop();
				parent.startstop.State = 0;
				return;
			}
			parent.startstop.State = 1;
			this.PlayOffset = 0;												//オフセットを元に戻す
			parent.right.Visible = false;										//スクロールボタンは押せないようにしておく
			parent.left.Visible = false;
			this.isPlay = true;
		}



		public void PlayStop()
		{
			if(!this.isPlay)
			{
				return;
			}
			for(int i=0;i<AnimalList.Count;i++)
			{
				((Animal)AnimalList[i]).Visible = true;							//visibleを復帰させる
			}
			parent.startstop.State = 0;
			parent.right.Visible = true;										//スクロールボタンも復帰させる
			parent.left.Visible = true;
			this.isPlay = false;

			// チュートリアル実行中で、動作の待機状態だった場合
			if(TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
			{
				// ステート進行
				parent.parent.tutorialparent.NextState();
			}
		}

//		public void RightScroll()
//		{
//			if(this.nowPlace < 100)
//			{																	//1小節分ページを進める
//				this.defaultPlace = this.nowPlace = this.nowPlace + 4 * muphic.Common.ScoreTools.Kugiri;
//			}
//		}
//
//		public void LeftScroll()
//		{
//			if(this.nowPlace >= 0 + 4 * muphic.Common.ScoreTools.Kugiri)
//			{																	//1小節分ページを戻す
//				this.defaultPlace = this.nowPlace = this.nowPlace - 4 * muphic.Common.ScoreTools.Kugiri;
//			}
//		}

		public void RightScroll()
		{
			ChangeScroll(this.nowPlace + 4 * muphic.Common.ScoreTools.Kugiri);
		}

		public void LeftScroll()
		{
			ChangeScroll(this.nowPlace - 4 * muphic.Common.ScoreTools.Kugiri);
		}

		/// <summary>
		/// 画面をスクロールするときに呼ぶメソッド
		/// ここで、画面の切り替えと、スクロールの値の設定をやっている
		/// </summary>
		/// <param name="NewPlace">新しく設定する楽譜の位置</param>
		public void ChangeScroll(int NewPlace)
		{
			int num = barNum -1 -2;
			if (num >= 1)
			{
				if (NewPlace > (num * 8) + 4)
				{
					NewPlace = num * 8;													//小節ごとに区切るから100にはならない
				}
				else if(NewPlace < 0)
				{
					NewPlace = 0;
				}
				this.defaultPlace = this.nowPlace = NewPlace;
				bar.Percent = ((float)NewPlace / ((num) * 8)) * 100;							//スクロールの場所を新たに設定
			}
			else
			{
				this.defaultPlace = 0;
				bar.Percent = 0;							//スクロールの場所を新たに設定
			}
		}

		/// <summary>
		/// 画面をスクロールするときに呼ぶメソッド
		/// こちらは、スクロールバーによって変更されたときに呼ぶ用のメソッド
		/// </summary>
		/// <param name="Percent">スクロールバーのパーセンテージ</param>
		public void ChangeScroll(float Percent)
		{
			int num = barNum -1 -2;
			float New = (Percent / 100) * (num);										//範囲が0～96なので、まずPercentを0～12の範囲に変換する
			int NewPlace = (int)(New + 0.5) * 8;								//四捨五入のため、+0.5してからint型に直す
			ChangeScroll(NewPlace);												//改めてChangeScroll
		}


		public override void MouseEnter()
		{
			base.MouseEnter ();
			parent.tsuibi.Visible = true;
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			parent.tsuibi.Visible = false;
		}



		/// <summary>
		/// 再生時の描画処理
		/// </summary>
		private void AnswerCheck()
		{
			int count;
			// チュートリアル実行中はFrameCountの参照方法が変わります
			if(muphic.Common.TutorialStatus.getIsTutorial()) count = this.parent.parent.tutorialparent.parent.parent.parent.FrameCount;
			else count = parent.parent.parent.FrameCount;
			
			//int count = parent.parent.parent.FrameCount;
			//int tempo = parent.tempo.TempoMode;
			this.PlayOffset += tempo;									//テンポの分だけオフセットを足しとく
			for(int i=0;i<AnimalList.Count;i++)
			{
				Animal a = (Animal)AnimalList[i];
//				bool flag = false;
//				if (parent.quest.Question[parent.QuestionNum].Length >= i)
//				{
//					Animal q = (Animal)parent.quest.Question[parent.QuestionNum][i];
//					if (q.code == a.code && q.place == a.place)
//					{
//						flag = true;
//					}
//				}

				Point p = this.ScoretoPointRelative(a.place, a.code);	//動物の音階と位置から座標を割り出す
				p.X -= this.PlayOffset;									//再生中なので、オフセットを引いとく
				//parent.light.Draw();
				if(p.X < 0 || a.Visible == false)						//もし画面外に出ているか、描画禁止なら
				{
					continue;											//今回のforは飛ばす
				}
				else if(p.X <= 55)										//もし家にぶつかっていたら
				{
					//if (parent.check.answerFlag.Length <= i)
					//{
						//if (parent.check.answerFlag[i])
						//{
							muphic.SoundManager.Play("Sheep" + a.code + ".wav");			//音だけ鳴らして…
							//parent.light.Add(a.code);
							houselight.Add(a.code);
							a.Visible = false;
						//}
					//}
					continue;											//次のforへ
				}
				else if(800 < p.X)										//もし、まだ楽譜まで到達していないなら
				{														//AnimalListは順番どおりに並んでいるので、これから先も
					break;												//楽譜まで到達していないことになるからfor文を終了する
				}

				int spY = p.X % 40 < 20 ? p.Y + 2 : p.Y - 2;

				muphic.DrawManager.DrawCenter(a.ToString(), p.X, spY);	//これらの条件を満たしていなければ、普通に描画する
				switch (a.group)
				{
					case 0:
						DrawManager.DrawCenter("ribbon1", p.X-12, spY-7);
						break;
					case 1:
						DrawManager.DrawCenter("ribbon2", p.X-12, spY-7);
						break;
					case 2:
						DrawManager.DrawCenter("ribbon3", p.X-12, spY-7);
						break;
					case 3:
						DrawManager.DrawCenter("ribbon4", p.X-12, spY-7);
						break;
					default:
						break;
				}
			}

			Animal b = (Animal)AnimalList[AnimalList.Count-1];			//AnimalListの最後の要素を取り出す

			if(!b.Visible)
			{
				// チュートリアル実行中で、動作の待機状態だった場合
				if(TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
				{
					// ステート進行
					parent.parent.tutorialparent.NextState();
				}
				
				
				parent.Screen = parent.check.flag ? new Dialog.Answer.AnswerDialog(parent, true) : new Dialog.Answer.AnswerDialog(parent, false);
				parent.signboard.drawFlag = false;
				for(int i=0;i<AnimalList.Count;i++)
				{
					((Animal)AnimalList[i]).Visible = true;							//visibleを復帰させる
				}

				parent.right.Visible = true;										//スクロールボタンも復帰させる
				parent.left.Visible = true;
				this.answerCheckFlag = false;

				parent.startstop.State = 0;								//再生終了
			}
		}
	}
}
