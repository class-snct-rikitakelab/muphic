using System;

namespace muphic.LinkMake
{
	/// <summary>
	/// Tempo の概要の説明です。
	/// </summary>
	public class Tempo : Screen
	{
		public LinkMakeScreen parent;
		public int TempoMode = 0;
		public TempoButton[] tempobutton_l;
		public Tempo(LinkMakeScreen one)
		{
			parent = one;
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化、テクスチャ・座標の登録、画面への登録
			///////////////////////////////////////////////////////////////////
			tempobutton_l = new TempoButton[5];
			for(int i=0;i<5;i++)
			{
				tempobutton_l[i] = new TempoButton(this, i+1);
				muphic.DrawManager.Regist(tempobutton_l[i].ToString(), 399-(i*66), 126,/*590-(i*70), 125,*/ 
					"image\\link\\button\\tempo\\off\\tempo_" + (i+1) + ".png", "image\\link\\button\\tempo\\on\\tempo_" + (i+1) + ".png");
				BaseList.Add(tempobutton_l[i]);
			}

			tempobutton_l[2].Click(System.Drawing.Point.Empty);	//デフォルトとして真ん中のボタンをクリックした状態にしておく
		}

		public override void Click(System.Drawing.Point p)
		{
			for(int i=0;i<5;i++)
			{
				tempobutton_l[i].State = 0;						//本来のクリック処理を行う前に
			}													//すべての要素をクリックしていない状態に戻す
			base.Click (p);
		}

		public override void MouseLeave()
		{
			base.MouseLeave ();
			for(int i=0;i<5;i++)
			{
				((Base)BaseList[i]).MouseLeave();
			}
		}


	}
}
