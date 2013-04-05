using System;
using System.Drawing;

namespace muphic.Story.WindowParts
{
	/// <summary>
	/// Thumbnail の概要の説明です。
	/// </summary>
	public class Thumbnail : Screen
	{
		public Window parent;
		int num;
//		public SlideLayout layout;

		public Thumbnail(Window window, int num)
		{
			parent = window;
			this.num = num;
/*			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			layout = new SlideLayout(this);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(layout.ToString(), 797, 38, "image\\story\\button\\null.png");

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(layout);
			
			///////////////////////////////////////////////////////////////////
			//サムネイルのセット
			///////////////////////////////////////////////////////////////////
			this.SetThumbnail(num);*/
		}

		public override void Draw()
		{
			base.Draw ();

			Rectangle src = PointManager.Get(parent.parent.parent.wind.ToString());
//			src.X -= 5;src.Y -= 5;
//			src.Width += 10;src.Height += 10;
			Rectangle dist = PointManager.Get(parent.ToString());
//			dist.X = x;
//			dist.Y = y;
			DrawManager.Change(src,dist);

			MakeStory.Slide slide = parent.parent.parent.PictureStory.Slide[this.num];
			//背景の描画
			if(slide.haikei.ToString().Equals(muphic.MakeStory.BGMode.BGNone.ToString()))
			{
			}
			else
			{
				DrawManager.DrawDiv(slide.haikei.ToString(),
					slide.haikei.X, slide.haikei.Y);
			}
			//配置物の描画
			for(int i = 0;i < slide.ObjList.Count;i++)
			{
				MakeStory.Obj o = (MakeStory.Obj)(slide.ObjList[i]);
				DrawManager.DrawDivCenter((slide.ObjList[i].ToString()), o.X, o.Y);
			}
		}



/*
		/// <summary>
		/// サムネイルを設定するときに使用するメソッド
		/// </summary>
		/// <param name="num">設定するのサムネイルのページ</param>
		public void SetThumbnail(int num)
		{
			String snum = this.easyFormat(num);								//数字を2桁でフォーマットする
			muphic.DrawManager.Regist(layout.ToString(), 797, 38, snum + ".png");//サムネイルを登録する
		}

		/// <summary>
		/// 本当に簡単なフォーマット処理。数字が1桁なら先頭に0をつけて強制的に2桁にする。3桁以上の場合は00にする。
		/// </summary>
		/// <param name="num"></param>
		private String easyFormat(int num)
		{
			String s;
			if(num < 0)
			{
				s = "00";
			}
			else if(num < 10)
			{
				s = "0" + num;
			}
			else if(num < 100)
			{
				s = num.ToString();
			}
			else
			{
				s = "00";
			}
			return s;
		}*/
	}
}
