using System;
using System.Drawing;


namespace muphic
{
	public enum LinkTopScreenMode{LinkTopScreen, PlayScreen, MakeScreen};
	/// <summary>
	/// SelectDialog の概要の説明です。
	/// </summary>
	public class LinkTop : Screen
	{
		public TopScreen parent;
		
		public LinkBackButton back;
		public LinkMakeButton make;
		public LinkPlayButton play;

		public Screen Screen;
		private LinkTopScreenMode screenmode;

		///////////////////////////////////////////////////////////////////////
		//プロパティの宣言
		///////////////////////////////////////////////////////////////////////
		public LinkTopScreenMode ScreenMode
		{
			get
			{
				return screenmode;
			}
			set
			{
				screenmode = value;
				switch(value)
				{
					case muphic.LinkTopScreenMode.LinkTopScreen:
						Screen = null;
						break;
					case muphic.LinkTopScreenMode.MakeScreen:
						Screen = null;									//子のウィンドウは表示せず、自分自身を描画する
						break;
					case muphic.LinkTopScreenMode.PlayScreen:
						parent.Screen = new LinkScreen(parent);					//LinkMakeScreenをインスタンス化し、そちらを描画する
						break;
					default:
						Screen = null;									//とりあえずTopScreenに描画を戻す
						break;
				}
			}
		}

		public LinkTop(TopScreen top)
		{
			this.parent = top;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			//DrawManager.BeginRegist(7);

			make = new LinkMakeButton(this);
			back = new LinkBackButton(this);
			play = new LinkPlayButton(this);


			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 0, 0, "image\\link\\top\\TopLink_bak.png");
            muphic.DrawManager.Regist(play.ToString(), 560, 480, "image\\link\\top\\button\\A_off.png", "image\\link\\top\\button\\A_on.png");
			muphic.DrawManager.Regist(make.ToString(), 240, 480, "image\\link\\top\\button\\Q_off.png", "image\\link\\top\\button\\Q_on.png");
			muphic.DrawManager.Regist(back.ToString(), 430, 655, "image\\link\\top\\button\\back_off.png", "image\\link\\top\\button\\back_on.png");

			//DrawManager.EndRegist();
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////

			BaseList.Add(back);
			BaseList.Add(make);
			BaseList.Add(play);

		}

		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}

		public override void Draw()
		{
			base.Draw ();
		}

	}
}