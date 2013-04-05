using System;
using System.Collections;
using System.Drawing;
using muphic.LinkMake.ScoreParts;
using muphic.Common;

namespace muphic.LinkMake
{
	/// <summary>
	/// Score の概要の説明です。
	/// </summary>
	public class Score : Screen
	{
		//public ArrayList AnimalList;							//別にBaseListを使ってもいいが、ここはあえて別に宣言する
		public Animals Animals;
		public LinkMakeScreen parent;
		public const int MaxAnimals_perPage = 12;		//1ページあたりで1つの音階における4分音符の最大数(実験値)
		public const int MaxAnimals = 24;				//1ページあたりで1つの音階における最大数(実験値、ただしこれはKugiri=2にしか通用しないことに注意)
		public int nowPlace;							//現在表示中の位置
		int defaultPlace;								//非再生時の表示位置(再生終了時ここの位置に戻る)
		public bool isPlay;								//再生中かどうかを表すフラグ
		//動物再配置用変数
		public int beginAnimalNum;						//ドラッグ開始時に選択されていた動物の要素番号(選択されていないなら-1)
		
		public RightScrollButton right;
		public LeftScrollButton left;
		public ScrollBar bar;
		public ClearButton clear;
		public AllClearButton allclear;
		public SharpButton sharp;

		public SignBoard signboard;

		public Score(LinkMakeScreen one)
		{
			parent = one;
			Animals = new Animals();
			defaultPlace = nowPlace = 0;
			this.isPlay = false;

			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			right = new RightScrollButton(this);
			left = new LeftScrollButton(this);
			bar = new ScrollBar(this);
			clear = new ClearButton(this);
			allclear = new AllClearButton(this);
			sharp = new SharpButton(this);

			signboard = new SignBoard(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			DrawManager.Regist("Sheep", 0, 0, "image\\one\\button\\animal\\sheep\\sheep.png");
			DrawManager.Regist("AnimalCheck_Sheep", 0, 0, "image\\one\\focus\\focus_sheep.png");
			muphic.DrawManager.Regist(right.ToString(), 844, 695, "image\\one\\parts\\scroll\\next.png");
			muphic.DrawManager.Regist(left.ToString(), 116, 695, "image\\one\\parts\\scroll\\prev.png");
			DrawManager.Regist(bar.ToString(), 145, 693, "image\\one\\parts\\scroll\\bars.png");
			muphic.DrawManager.Regist(clear.ToString(), 287, 194, "image\\one\\button\\score\\clear_off.png", "image\\one\\button\\score\\clear_on.png");
			muphic.DrawManager.Regist(allclear.ToString(), 159, 194, "image\\one\\button\\score\\allclear_off.png", "image\\one\\button\\score\\allclear_on.png");
			muphic.DrawManager.Regist(sharp.ToString(), 578, 195, "image\\one\\button\\score\\sharp_off.png", "image\\one\\button\\score\\sharp_on.png");
			muphic.DrawManager.Regist("Sign", 1050, 0, "image\\one\\parts\\signboard.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(right);
			BaseList.Add(left);
			BaseList.Add(bar);
			BaseList.Add(clear);
			BaseList.Add(allclear);
			//BaseList.Add(sharp);
			BaseList.Add(signboard);
		}

		public override void Draw()
		{
			base.Draw ();
			if(!Visible)
			{
				return;
			}

			bool isPlayFinished = Animals.Draw(this.nowPlace, parent.tempo_l.TempoMode, this.isPlay);//動物共の描画
			if(isPlayFinished)
			{
				this.PlayStop();																//もし再生終了フラグが立ったなら再生を終了する
			}
		}


		bool isRightClick = true;							//右クリック削除を実装するときはtrue
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			if(this.isPlay)								//再生中、楽譜内の編集作業は許可しない
			{
				return;
			}

			Point place = muphic.Common.ScoreTools.DecidePlace(p);			//楽譜内での位置と音階を決定する
			if(place.X == 0 && place.Y == 0)								//DebicePlaceが楽譜外(もしくはそれに
			{																//そうとうするもの)だと判断した
				return;														//ら、帰る
			}
			place.X += this.nowPlace;										//現在のオフセットを追加
			System.Diagnostics.Debug.WriteLine(place.Y, "音階");
			System.Diagnostics.Debug.WriteLine(place.X, "位置");
			if(isRightClick && Animals.Search(place.X, place.Y) != -1 && parent.parent.parent.e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				Animals.Delete(place.X, place.Y);
			}
			else
				if(parent.linkmakes.NowClick != muphic.LinkMake.LinkMakeButtonsClickMode.None)		//右のボタン群で何かを選択していれば以下を実行する
			{
				if(parent.linkmakes.NowClick == muphic.LinkMake.LinkMakeButtonsClickMode.Cancel)//キャンセルボタンがクリックされていたら
				{
					bool b = Animals.Delete(place.X, place.Y);					//削除処理を実行
					System.Diagnostics.Debug.WriteLine(b, "Delete");
				}
				else														//ほかの動物がクリックされていたら
				{
					bool b = Animals.InsertL(place.X, place.Y, parent.linkmakes.NowClick.ToString());//挿入処理を実行
					System.Diagnostics.Debug.WriteLine(b, "Insert");
					
					// チュートリアル実行中で、動作の待機状態だった場合
					if(TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
					{
						// ステート進行
						parent.parent.tutorialparent.NextState();
					}
				}
			}
			//else																//何のボタンも選択されていないときは、動物を選択状態にする
			//{
			/*int CheckedAnimalNum;
			CheckedAnimalNum = Animals.ClickAnimal(place.X, place.Y);		//動物を選択状態にする
			if(CheckedAnimalNum == -1)										//もし、何の動物も選択していなければ
			{
				this.clear.Visible = false;									//戻るボタンを表示しない
			}
			else															//何かを選択していれば
			{
				this.clear.Visible = true;									//戻るボタンを表示する

			}*/
			//}
		}
		
		public override void DragBegin(Point begin)
		{
			base.DragBegin (begin);
			if(true)//if(parent.ones.NowClick == muphic.One.OneButtonsClickMode.None)		//右のボタン群で何かを選択していないときに限る
			{
				Point place = muphic.Common.ScoreTools.DecidePlace(begin);		//楽譜内での位置と音階を決定する
				place.X += this.nowPlace;										//現在のオフセットを追加
				this.beginAnimalNum = Animals.Search(place.X, place.Y);			//現在選択されている動物の要素番号を格納しておく
				parent.tsuibi.Visible = false;
			}
			else
			{
				this.beginAnimalNum = -1;
			}
		}

		public override void DragEnd(Point begin, Point p)
		{
			base.DragEnd (begin, p);
			this.beginAnimalNum = -1;
			parent.tsuibi.Visible = true;
		}


		public override void Drag(Point begin, Point p)
		{
			base.Drag (begin, p);
			if(this.beginAnimalNum != -1)										//ドラッグ開始時に何かしらの動物を選択していたら
			{
				int OldPlace = Animals[this.beginAnimalNum].place;
				int OldCode = Animals[this.beginAnimalNum].code;
				Point now = muphic.Common.ScoreTools.DecidePlace(p);
				if(now.X == 0 && now.Y == 0)
				{
					return;														//楽譜外にドラッグしようとしてはいけません
				}
				now.X += this.nowPlace;											//現在のオフセットを追加
				int NewNum = Animals.ReplaceL(OldPlace, OldCode, now.X, now.Y, true);
				if(NewNum != -1)
				{
					this.beginAnimalNum = NewNum;								//再配置が成功であればbeginNumに入れとく
				}
			}
		}


		public void PlayFirst()
		{
			this.nowPlace = 0;													//ページを強制的に最初に戻す
			Animals.PlayOffset = 0;												//再生途中だった場合、最初に戻す
			for(int i=0;i<Animals.AnimalList.Count;i++)
			{
				((Animal)Animals[i]).Visible = true;							//再生途中だった場合を考えて
			}																	//visibleを復帰させる
			this.PlayStart();
		}

		public void PlayStart()
		{
			if(this.isPlay)
			{
				return;
			}
			if(Animals.AnimalList.Count == 0)
			{
				PlayStop();
				return;
			}
			Animals.PlayOffset = 0;												//オフセットを元に戻す
			parent.score.right.Visible = false;									//スクロールボタンは押せないようにしておく
			parent.score.left.Visible = false;
			this.isPlay = true;
		}

		public void PlayStop()
		{
			for(int i=0;i<Animals.AnimalList.Count;i++)
			{
				((Animal)Animals[i]).Visible = true;							//visibleを復帰させる
			}
			parent.score.right.Visible = true;									//スクロールボタンも復帰させる
			parent.score.left.Visible = true;
			parent.startstop.State = 0;
			this.nowPlace = this.defaultPlace;									//はじめからをやった場合のページ復帰
			this.isPlay = false;
		}

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
			if(NewPlace > 60)
			{
				NewPlace = 56;													//小節ごとに区切るから100にはならない
			}
			else if(NewPlace < 0)
			{
				NewPlace = 0;
			}
			this.defaultPlace = this.nowPlace = NewPlace;
			bar.Percent = (float)NewPlace / 56 * 100;							//スクロールの場所を新たに設定
		}

		/// <summary>
		/// 画面をスクロールするときに呼ぶメソッド
		/// こちらは、スクロールバーによって変更されたときに呼ぶ用のメソッド
		/// </summary>
		/// <param name="Percent">スクロールバーのパーセンテージ</param>
		public void ChangeScroll(float Percent)
		{
			float New = Percent / 100 * 8;										//範囲が0～96なので、まずPercentを0～12の範囲に変換する
			int NewPlace = (int)(New + 0.5) * 8;								//四捨五入のため、+0.5してからint型に直す
			ChangeScroll(NewPlace);												//改めてChangeScroll
		}
/*
		private bool inScore(Point p)
		{
			if(109 <= p.X && p.X <= 109+555)
			{
				if(181 <= p.Y && p.Y <= 181+410)
				{
					return true;
				}
			}
			return false;
		}*/
	}
}
