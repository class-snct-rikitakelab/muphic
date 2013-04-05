using System;
using System.Collections;
using System.Drawing;
using muphic.Story;

namespace muphic
{
	public enum StoryScreenMode {StoryScreen, ScoreScreen, AllClearDialog, VoiceRegistDialog, VoiceRegistOneMoreDialog};
	/// <summary>
	/// Story の概要の説明です。
	/// </summary>
	public class StoryScreen : Screen
	{
		public MakeStoryScreen parent;
		public Score score;
		public House house;
		public Tempo tempo;
		public BackButton back;
		public RestartButton restart;
		public StartStopButton startstop;
		public ScoreButton scorebutton;
		public Window window;
		public StoryButtons stories;
		public Tsuibi tsuibi;

		public Screen Screen;

		private StoryScreenMode storyscreenmode;
		//public String StoryTitle;
		//public ArrayList SlideList;
		public const int PageNum = 10;								//つまり、範囲としては0～9
		public int NowPage;											//現在何枚目のスライドの音楽を作曲しているか
/*
		public bool IsMakeStory
		{
			get
			{
				return isMakeStory;
			}
			set
			{
				isMakeStory = value;
				if(isMakeStory)
				{
					makestory = new MakeStoryScreen(this, this.PictureStory);
				}
				else
				{
					makestory = null;
				}
			}
		}*/

		/// <summary>
		/// ものがたり音楽画面のScreenMode
		/// 楽譜画面や物語作成画面、ダイアログなどへの切り替えに使用
		/// </summary>
		public StoryScreenMode StoryScreenMode
		{
			get
			{
				return storyscreenmode;
			}
			set
			{
				storyscreenmode = value;
				switch(value)
				{
					case StoryScreenMode.StoryScreen:
						//子のウィンドウは表示せず、自分自身を描画する
						Screen = null;
						break;
					case StoryScreenMode.ScoreScreen:
						//ScoreScreenをインスタンス化し、そちらを描画する
						Screen = new ScoreScreen(this);
						break;
					case StoryScreenMode.AllClearDialog:
						Screen = new muphic.Common.AllClearDialog(this);
						break;
					case StoryScreenMode.VoiceRegistDialog:
						Screen = new VoiceRegistDialog(this);
						break;
					case StoryScreenMode.VoiceRegistOneMoreDialog:
						Screen = new VoiceRegistOneMoreDialog(this);
						break;
					default:
						//とりあえずStoryScreenに描画を戻す
						Screen = null;
						break;
				}
			}
		}
		//public StoryScreen(TopScreen ts)
		public StoryScreen(MakeStoryScreen ts, MakeStory.Slide slide, int num)
		{
			this.parent = ts;
			this.NowPage = num;
			this.StoryScreenMode = StoryScreenMode.StoryScreen;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			DrawManager.BeginRegist(77);
			score = new Score(this);
			house = new House(this);
			tempo = new Tempo(this);
			back = new BackButton(this);
			restart = new RestartButton(this);
			startstop = new StartStopButton(this);
			scorebutton = new ScoreButton(this);
			window = new Window(this, num);
			tsuibi = new Tsuibi(this);
			stories = new StoryButtons(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\one\\background.png");
			muphic.DrawManager.Regist(score.ToString(), 102, 180, "image\\one\\parts\\road.png");
			muphic.DrawManager.Regist(house.ToString(), 15, 137, "image\\one\\parts\\house.png");
			muphic.DrawManager.Regist(tempo.ToString(), 120,120,/*147, 118,*/ "image\\one\\button\\tempo\\tempo.png");
			muphic.DrawManager.Regist(back.ToString(), 8, 5, "image\\Story\\button\\back_off.png", "image\\Story\\button\\back_on.png");
			muphic.DrawManager.Regist(restart.ToString(), 270, 12, "image\\one\\button\\restart\\restart_off.png", "image\\one\\button\\restart\\restart_on.png");
			muphic.DrawManager.Regist(startstop.ToString(), 167, 12, new String[] {"image\\one\\button\\start_stop\\start_off.png", "image\\one\\button\\start_stop\\start_on.png", "image\\one\\button\\start_stop\\stop_off.png", "image\\one\\button\\start_stop\\stop_on.png"});
			muphic.DrawManager.Regist(scorebutton.ToString(), 814, 197, "image\\one\\button\\gakuhu\\gakuhu_off.png", "image\\one\\button\\gakuhu\\gakuhu_on.png");
			muphic.DrawManager.Regist(window.ToString(), 760, 10, "image\\one\\parts\\story_window.png");
			muphic.DrawManager.Regist(stories.ToString(), 896, 190, "image\\one\\button\\animal\\buttons.png");//896,199 before 679,176
			muphic.DrawManager.Regist(tsuibi.ToString(), 0, 0, new String[9] 
					{
						"image\\MakeStory\\none.png",
						"image\\one\\button\\animal\\bird\\bird.png", "image\\one\\button\\animal\\dog\\dog.png",
						"image\\one\\button\\animal\\pig\\pig.png", "image\\one\\button\\animal\\rabbit\\rabbit.png",
						"image\\one\\button\\animal\\sheep\\sheep.png", "image\\one\\button\\animal\\cat\\cat.png",
						"image\\one\\button\\animal\\voice\\voice.png", "image\\common\\clear.png"});

			DrawManager.EndRegist();
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(house);
			BaseList.Add(score);
			BaseList.Add(tempo);
			BaseList.Add(back);
			BaseList.Add(restart);
			BaseList.Add(startstop);
			BaseList.Add(scorebutton);
			BaseList.Add(window);
			BaseList.Add(stories);						//tsuibiは登録しないことに注意。

			
			///////////////////////////////////////////////////////////////////
			//スライドにある楽譜データの取得
			///////////////////////////////////////////////////////////////////
			score.Animals.AnimalList = slide.AnimalList;
			tempo.TempoMode = slide.tempo;
		}

		public override void Click(System.Drawing.Point p)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:
					ClickStoryScreen(p);
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.Click(p);						//ScoreScreenのときも、全てあちら側の処理
					break;
				default:
					break;

			}
		}

		private void ClickStoryScreen(Point p)
		{
			if(score.isPlay)						//----------瀬戸終了のお知らせ----------
			{
				String[] StoryPermissionList = new String[] {startstop.ToString(), restart.ToString(),
															  tempo.ToString()};
				base.Click(p, StoryPermissionList);
			}
			else
			{
				base.Click(p);
			}
		}

		public override void DragBegin(System.Drawing.Point begin)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:
					if(score.isPlay == false)
					{
						base.DragBegin(begin);
					}
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.DragBegin(begin);					//ScoreScreenのときも、全てあちら側の処理
					break;
				default:
					break;

			}
		}

		public override void Drag(System.Drawing.Point begin, System.Drawing.Point p)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:	
					if(score.isPlay == false)
					{
						base.Drag(begin, p);
					}
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.Drag(begin, p);					//ScoreScreenのときも、全てあちら側の処理
					break;
				default:
					break;

			}
		}

		public override void DragEnd(System.Drawing.Point begin, System.Drawing.Point p)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:	
					if(score.isPlay == false)
					{
						base.DragEnd(begin, p);
					}
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.DragEnd(begin, p);					//ScoreScreenのときも、全てあちら側の処理
					break;
				default:
					break;

			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:	
					base.MouseMove (p);
					tsuibi.MouseMove(p);					//tsuibiはBase型だが、MouseMoveが必要なので、呼ぶ
					break;
				case StoryScreenMode.ScoreScreen:
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					Screen.MouseMove(p);					//ScoreScreenのときも、全てあちら側の処理
					break;
				default:
					break;

			}
		}

		public override void Draw()
		{
			switch(this.StoryScreenMode)
			{
				case StoryScreenMode.StoryScreen:
					base.Draw();							//普段のデフォルト処理
					if(tsuibi.Visible)
					{
						muphic.DrawManager.DrawCenter(tsuibi.ToString(), tsuibi.point.X, tsuibi.point.Y, tsuibi.State);
						//tsuibiはBaseListに登録してないので、別に描画する必要がある
					}
					break;
				case StoryScreenMode.ScoreScreen:
					Screen.Draw();							//ScoreScreenのときも、全てあちら側の処理
					break;
				case StoryScreenMode.AllClearDialog:
				case StoryScreenMode.VoiceRegistDialog:
				case StoryScreenMode.VoiceRegistOneMoreDialog:
					base.Draw();
					Screen.Draw();
					break;
				default:
					break;

			}
		}
/*
		/// <summary>
		/// Windowのほうで、右スクロールボタンが押されたときの処理。
		/// スライドの変更やサムネイルの変更などの変更処理の一番上に存在する
		/// </summary>
		public void NextSlide()
		{
			if(muphic.StoryScreen.PageNum <= this.NowPage+1)			//+1したときにページの範囲を超える
			{
				this.ChangeSlide(muphic.StoryScreen.PageNum-1);
				return;
			}
			this.ChangeSlide(this.NowPage + 1);
		}

		/// <summary>
		/// Windowのほうで、左スクロールボタンが押されたときの処理。
		/// スライドの変更やサムネイルの変更などの変更処理の一番上に存在する
		/// </summary>
		public void PrevSlide()
		{
			if(this.NowPage-1 < 0)									//-1したときにページの範囲を超える
			{
				this.ChangeSlide(0);
				return;
			}
			this.ChangeSlide(this.NowPage - 1);
		}*/
/*
		/// <summary>
		/// スライドを初期化するときに使用するメソッド。必ず0枚目から始まる。
		/// </summary>
		public void ChangeSlide0()
		{
			this.NowPage = 0;
			Slide s = PictureStory.Slide[this.NowPage];
			tempo.TempoMode = s.tempo;
			score.Animals.AnimalList = s.AnimalList;
			window.thumbnail.ChangeThumbnail(0);
		}

		/// <summary>
		/// スライドを変更するときに使用するメソッド。ちゃっかりサムネイルも変更してくれる
		/// </summary>
		/// <param name="next">変更後のスライドのページ</param>
		private void ChangeSlide(int next)
		{
			Slide s = PictureStory.Slide[this.NowPage];
			s.tempo = tempo.TempoMode;											//現在のテンポを現在のスライドのものとして保存
			s.AnimalList = score.Animals.AnimalList;							//現在の楽譜を現在のスライドのものとして保存
			PictureStory.Slide[this.NowPage] = s;

			Slide n = PictureStory.Slide[next];
			score.Animals.AnimalList = n.AnimalList;							//新しいページの楽譜を現在の楽譜とする
			tempo.TempoMode = n.tempo;									//テンポも変更する
			window.thumbnail.ChangeThumbnail(next);						//ウィンドウのサムネルも変更する
			this.NowPage = next;
		}*/
	}
}
