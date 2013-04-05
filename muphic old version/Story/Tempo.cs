using System;

namespace muphic.Story
{
	/// <summary>
	/// Tempo の概要の説明です。
	/// </summary>
	public class Tempo : Screen
	{
		public StoryScreen parent;
		/// <summary>
		/// 現在どのテンポボタンを押されているか．範囲は1～5
		/// </summary>
		private int tempomode = 0;
		public TempoButton[] tempobutton;

		public int TempoMode
		{
			get
			{
				return tempomode;
			}
			set
			{
				parent.parent.PictureStory.Slide[parent.NowPage].tempo = value;
				tempomode = value;
				for(int i=0;i<5;i++)
				{
					tempobutton[i].State = 0;
				}
				tempobutton[value-1].State = 1;
			}
		}
		public Tempo(StoryScreen story)
		{
			parent = story;
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化、テクスチャ・座標の登録、画面への登録
			///////////////////////////////////////////////////////////////////
			tempobutton = new TempoButton[5];
			for(int i=0;i<5;i++)
				//muphic.DrawManager.Regist(tempobutton[i].ToString(), 399-(i*66), 126, 
			{
				tempobutton[i] = new TempoButton(this, i+1);
				muphic.DrawManager.Regist(tempobutton[i].ToString(), 399-(i*66),126,/*456-(i*75), 123,*/ 
					"image\\one\\button\\tempo\\off\\tempo_" + (i+1) + ".png", "image\\one\\button\\tempo\\on\\tempo_" + (i+1) + ".png");
				BaseList.Add(tempobutton[i]);
			}

			//tempobutton[2].Click(System.Drawing.Point.Empty);	//デフォルトとして真ん中のボタンをクリックした状態にしておく
		}

		public override void Click(System.Drawing.Point p)
		{												//すべての要素をクリックしていない状態に戻す
			base.Click (p);
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			for(int i=0;i<5;i++)
			{
				tempobutton[i].MouseLeave();
			}
		}
	}
}
