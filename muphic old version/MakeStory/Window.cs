using System;
using System.Collections;
using System.Drawing;
using muphic;
using muphic.MakeStory;
using muphic.MakeStory.DesignList;
using muphic.Story;
using muphic.Common;

namespace muphic.MakeStory
{
	/// <summary>
	/// Window の概要の説明です。
	/// </summary>
	public class Window : Screen
	{
		//public int now;
		ArrayList ObjList;
		public MakeStoryScreen parent;
		public int backscreen;

		public Window(MakeStoryScreen mss)
		{
			//now = 0;
			ObjList =new ArrayList();
			parent = mss;
			backscreen = 0;
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}

		public override void Draw()
		{
			base.Draw ();
			//背景の描画
			if(parent.PictureStory.Slide[parent.NowPage].haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
			{
			}
			else
			{
				DrawManager.Draw(parent.PictureStory.Slide[parent.NowPage].haikei.ToString(),
					parent.PictureStory.Slide[parent.NowPage].haikei.X, parent.PictureStory.Slide[parent.NowPage].haikei.Y);
			}
			//配置物の描画
			for(int i = 0;i < parent.PictureStory.Slide[parent.NowPage].ObjList.Count;i++)
			{
				Obj o = (Obj)(parent.PictureStory.Slide[parent.NowPage].ObjList[i]);
				DrawManager.DrawCenter((parent.PictureStory.Slide[parent.NowPage].ObjList[i].ToString()), o.X, o.Y);
			}
		}

		/// <summary>
		/// 現在スライドが何ページまであるかを調べるメソッド
		/// </summary>
		/// <returns>入っている枚数の数(空なら0が返る)</returns>
		private int DecidePage()
		{
			for(int i=9;i>=0;i--)
			{
				Slide s = parent.PictureStory.Slide[i];
				if(s.ObjList.Count == 0 && s.AnimalList.Count == 0 &&
					s.haikei.ToString() == "BGNone")
				{
				}
				else
				{
					return i+1;
				}
			}
			return 0;
		}

		/// <summary>
		/// 物語音楽のスライドの記念形式の印刷プレビューを表示するようなもの
		/// </summary>
		public void PreviewMemorial()
		{
			int maxpage = DecidePage();								//ページが最大何枚か
			Slide slide = parent.PictureStory.Slide[0];
//			if(slide.ObjList.Count == 0)
//			{
//				return;
//			}
			//背景の描画
			DrawManager.DrawDiv(parent.wind.ToString());
			if(parent.PictureStory.Slide[parent.NowPage].haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
			{
			}
			else
			{
				DrawManager.DrawDiv(slide.haikei.ToString(), slide.haikei.X, slide.haikei.Y);
			}
			//配置物の描画
			for(int j = 0;j < slide.ObjList.Count;j++)
			{
				Obj o = (Obj)(slide.ObjList[j]);
				DrawManager.DrawDivCenter((o.ToString()), o.X, o.Y);
			}

			//wind.rect = {x=177, y=262, width=668, height=643}  right=845, bottom=643
			
		}

		/// <summary>
		/// 物語音楽のスライドを記念に印刷するようなもの
		/// </summary>
		public void PrintMemorial()
		{
			int allpage = this.DecidePage();						//ページの総数を決定する
			this.WindRect = PointManager.Get(parent.wind.ToString());
			this.WindPrintRect = new Rectangle(100, 100, 824, 568);
			for(int i=0;i<allpage;i++)
			{
				Slide slide = parent.PictureStory.Slide[i];
				if(slide.ObjList.Count == 0 && slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
					break;
				}
				PrintManager.ChangePage(i+1);

				// ページ文字列を登録
//				muphic.PrintManager.RegistString("ページ " + (i+1) + " / " + allpage, 830, 690, 16);
				
				// タイトルの登録
//				String title = parent.PictureStory.Title;							// 元から曲名をとってくる
//				if(title == null || title == "") title = "「あたらしいものがたり」";// 保存ダイアログのタイトルが無題ならば、こっちで勝手に決める
//				else title = "「" + title + "」";									// 『』をつける
//				muphic.PrintManager.RegistString("だいめい", 40, 40, 14);
//				muphic.PrintManager.RegistString(title, 65, 70, 24);

				//wind.rect = {x=177, y=262, width=668, height=381}  right=845, bottom=643
				// ページ文字列を登録
				muphic.PrintManager.RegistString("ページ " + (i+1) + " / " + allpage, 177+668-100, 262+381+45, 16);
				
				// ロゴを登録
				DrawManager.Regist("Logo", 730, 35, "image\\ScoreXGA\\logo.png");						// 印刷用ロゴ
				PrintManager.Regist("Logo", 177+668-200, 262-90);
				// タイトルの登録
				String title = parent.PictureStory.Title;							// 元から曲名をとってくる
				if(title == null || title == "") title = "「あたらしいものがたり」";// 保存ダイアログのタイトルが無題ならば、こっちで勝手に決める
				else title = "「" + title + "」";									// 『』をつける
				muphic.PrintManager.RegistString("だいめい", -20+177, -60+262, 14);
				muphic.PrintManager.RegistString(title, 5+177, -40+262, 24);

				//文章の登録
				PrintManager.RegistString(Common.CommonTools.StringCenter(slide.Sentence, 30), 117+67, 262+381+10);

				//背景の描画
				if(slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
				}
				else
				{
					PrintManager.Regist(slide.haikei.ToString(), slide.haikei.X, slide.haikei.Y);
					//PrintMemorialDiv(slide.haikei.ToString(), new Point(slide.haikei.X, slide.haikei.Y), false);
				}
				//配置物の描画
				for(int j = 0;j < slide.ObjList.Count;j++)
				{
					Obj o = (Obj)(slide.ObjList[j]);
					PrintManager.RegistCenter((o.ToString()), o.X, o.Y);
					//PrintMemorialDiv(o.ToString(), new Point(o.X, o.Y), true);
				}
			}
			Rectangle r = PointManager.Get(parent.wind.ToString());
			PrintManager.Print(new Rectangle(r.X-100, r.Y-100, r.Width+200, r.Height+200), true, true);
		}

		Rectangle WindRect;
		Rectangle WindPrintRect;

		/// <summary>
		/// 記念で印刷の場合、Windowのやつをそのまま描画すると、小さいので拡大して描画するため
		/// のメソッド
		/// </summary>
/*		private void PrintMemorialDiv(String ClassName, Point p, bool isCenter)
		{
			float divX = (float)WindPrintRect.Width / WindRect.Width;
			float divY = (float)WindPrintRect.Height / WindRect.Height;
			p.X = p.X - WindRect.X;
			p.X = (int)(p.X * divX);
			p.X = p.X + WindPrintRect.X;
			p.Y = p.Y - WindRect.Y;
			p.Y = (int)(p.Y * divY);
			p.Y = p.Y + WindPrintRect.Y;
			if(isCenter)
			{
				PrintManager.RegistCenter(ClassName, p.X, p.Y);
			}
			else
			{
				PrintManager.Regist(ClassName, p.X, p.Y);
			}
		}*/

		/// <summary>
		/// 物語音楽のスライドの紙芝居形式の印刷プレビューを表示するようなもの
		/// </summary>
		public void PreviewStory()
		{
			Slide slide = parent.PictureStory.Slide[0];
//			if(slide.ObjList.Count == 0)
//			{
//				return;
//			}
			//背景の描画
			if(parent.PictureStory.Slide[parent.NowPage].haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
			{
			}
			else
			{
				DrawManager.DrawDiv(slide.haikei.ToString(), slide.haikei.X, slide.haikei.Y);
			}
			//配置物の描画
			for(int j = 0;j < slide.ObjList.Count;j++)
			{
				Obj o = (Obj)(slide.ObjList[j]);
				DrawManager.DrawDivCenter((o.ToString()), o.X, o.Y);
			}
		}

		/// <summary>
		/// 物語音楽のスライドを紙芝居形式で印刷するようなもの
		/// </summary>
		public void PrintStory()
		{	//wind.rect = {x=177, y=262, width=668, height=381}  right=845, bottom=643
			Rectangle r = PointManager.Get(parent.wind.ToString());
			DrawManager.Regist("SlideTitle", r.X, r.Y, "image\\MakeStory\\print\\title.png");
			PrintManager.ChangePage(1);
			PrintManager.Regist("SlideTitle");
			String beforeString = parent.PictureStory.Title;
			if(beforeString == "")
			{
				beforeString = "あたらしいものがたり";
			}
			String CenterTitle = muphic.Common.CommonTools.StringCenter(beforeString, 15);
			PrintManager.RegistString(CenterTitle, 4+177, 176+262, 44);

			for(int i=0;i<10;i++)
			{
				Slide slide = parent.PictureStory.Slide[i];
				if(slide.ObjList.Count == 0 && slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
					break;
				}
				PrintManager.ChangePage(i+2);
				//背景の描画
				if(slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
				{
				}
				else
				{
					PrintManager.Regist(slide.haikei.ToString(), slide.haikei.X, slide.haikei.Y);
				}
				//配置物の描画
				for(int j = 0;j < slide.ObjList.Count;j++)
				{
					Obj o = (Obj)(slide.ObjList[j]);
					PrintManager.RegistCenter((o.ToString()), o.X, o.Y);
				}
			}
			PrintManager.Print(PointManager.Get(parent.wind.ToString()), true, true);
		}

		public override void Click(System.Drawing.Point p)
		{
			if(parent.ButtonsMode != muphic.MakeStory.ButtonsMode.None)	//右のボタン群で何かを選択していれば以下を実行する
			{
				Point place = p;
				System.Diagnostics.Debug.WriteLine(place.Y, "位置");
				System.Diagnostics.Debug.WriteLine(place.X, "位置");
				if(place.X == 0 && place.Y == 0)								//DevicePlaceが楽譜外(もしくはそれに
				{																//そうとうするもの)だと判断した
					return;
				}
				//if(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Clear)//キャンセルボタンがクリックされていたら
				if(parent.isClear)
				{
					bool b = this.Delete(place.X, place.Y);						//削除処理を実行
					System.Diagnostics.Debug.WriteLine(b, "Delete");
				}
				else if((parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Forest)
					||(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.River)
					||(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Town)
					||(parent.ButtonsMode == muphic.MakeStory.ButtonsMode.Room))
				{

				}
				else															//ほかの動物がクリックされていたら
				{
					System.Drawing.Rectangle rec = PointManager.Get(((ObjMode)parent.tsuibi.State).ToString());

					int xl = 177,yt = 262;
					System.Drawing.Rectangle wind = PointManager.Get(parent.wind.ToString());
					if((parent.tsuibi.State != 0)
						&&(place.X-rec.Width/2 > xl)&&(xl+wind.Width > place.X+rec.Width/2)
						&&(place.Y-rec.Height/2 > yt)&&(yt+wind.Height > place.Y+rec.Height/2))
					{
						bool b = this.Insert(place.X, place.Y);						//挿入処理を実行
						System.Diagnostics.Debug.WriteLine(b, "Insert");
						
						// チュートリアル実行中で、動作の待機状態だった場合
						if(b && TutorialStatus.getIsTutorial() && TutorialStatus.getNextStateStandBy())
						{
							// ステート進行
							parent.parent.tutorialparent.NextState();
						}
					}
					
				}
				
			}
		}

		/// <summary>
		/// パーツを新たに追加する
		/// </summary>
		/// <param name="place">(絶対的)位置</param>
		/// <returns>成功したかどうか</returns>
		private bool Insert(int X, int Y)
		{
			Obj newObj = new Obj(X, Y, parent.tsuibi.State);
			parent.PictureStory.Slide[parent.NowPage].ObjList.Insert(parent.PictureStory.Slide[parent.NowPage].ObjList.Count, newObj);
			return true;
		}

		/// <summary>
		/// パーツを削除する
		/// </summary>
		/// <param name="place">(絶対的)位置</param>
		/// <returns>成功したかどうか</returns>
		private bool Delete(int X, int Y)
		{
			for(int i = parent.PictureStory.Slide[parent.NowPage].ObjList.Count;i > 0;i--)
			{
				Obj o = (Obj)parent.PictureStory.Slide[parent.NowPage].ObjList[i-1];
				Rectangle rec = PointManager.Get(o.ToString());
				if((o.X-rec.Width/2 <= X & X <= o.X+rec.Width/2)&&
					(o.Y-rec.Height/2 <= Y & Y <= o.Y+rec.Height/2))
				{
					parent.PictureStory.Slide[parent.NowPage].ObjList.RemoveAt(i-1);
					return true;
				}
			}
			return false;
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
	}
}
