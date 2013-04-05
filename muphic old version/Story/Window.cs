using System;
using muphic.Story.WindowParts;

namespace muphic.Story
{
	public enum StoryWindowMode{Title, Thumbnail};					//おそらく表示するのはサムネイルだけだけど…
	/// <summary>
	/// Window の概要の説明です。
	/// </summary>
	public class Window : Screen
	{
		public StoryScreen parent;
		public StoryWindowMode windowmode;
		public Title title;
		public Thumbnail thumbnail;
		public Window(StoryScreen story, int num)
		{
			parent = story;
			this.windowmode = StoryWindowMode.Thumbnail;
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			title = new Title(this);
			thumbnail = new Thumbnail(this, num);

			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(title.ToString(), 551, 45, "image\\one\\parts\\title.png");
			muphic.DrawManager.Regist(thumbnail.ToString(), 760, 10, "image\\one\\parts\\story_window.png");//537,12

			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			//BaseList.Add(title);					//本当はtitleもScreen型であってほしい(下の都合上)が、めどいので、そのまま
		}

		public override void Click(System.Drawing.Point p)
		{
			switch(this.windowmode)
			{
				case muphic.Story.StoryWindowMode.Title:
					title.Click(p);
					break;
				case muphic.Story.StoryWindowMode.Thumbnail:
					thumbnail.Click(p);
					break;
				default:
					base.Click (p);
					break;
			}
		}

		public override void MouseMove(System.Drawing.Point p)
		{
			switch(this.windowmode)
			{
				case muphic.Story.StoryWindowMode.Title:
					//title.MouseMove(p);
					base.MouseMove(p);
					break;
				case muphic.Story.StoryWindowMode.Thumbnail:
					thumbnail.MouseMove(p);
					break;
				default:
					base.MouseMove (p);
					break;
			}
		}

		public override void Draw()
		{
			switch(this.windowmode)
			{
				case muphic.Story.StoryWindowMode.Title:
					//title.Draw();
					base.Draw();
					break;
				case muphic.Story.StoryWindowMode.Thumbnail:
					thumbnail.Draw();
					break;
				default:
					base.Draw ();
					break;
			}
		}
	}
}
