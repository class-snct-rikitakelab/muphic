using System;
using muphic.Tutorial.TutorialEndDialogParts;

namespace muphic.Tutorial
{
	/// <summary>
	/// TutorialEndDialog の概要の説明です。
	/// </summary>
	public class TutorialEndDialog : Screen
	{
		public TutorialMain parent;
		
		public YesButton yesbutton;
		public NoButton nobutton;
		
		public TutorialEndDialog(TutorialMain tm)
		{
			this.parent = tm;
			
			///////////////////////////////////////////////////////////////////
			//部品のインスタンス化
			///////////////////////////////////////////////////////////////////
			yesbutton = new YesButton(this);
			nobutton = new NoButton(this);
			
			///////////////////////////////////////////////////////////////////
			//部品のテクスチャ・座標の登録
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251,  "image\\TutorialXGA\\EndDialog\\dialog_end.png");
			muphic.DrawManager.Regist(yesbutton.ToString(), 382, 434, "image\\TutorialXGA\\EndDialog\\yes_off.png",  "image\\TutorialXGA\\EndDialog\\yes_on.png");
			muphic.DrawManager.Regist(nobutton.ToString(), 531, 434, "image\\TutorialXGA\\EndDialog\\no_off.png",  "image\\TutorialXGA\\EndDialog\\no_on.png");
			
			///////////////////////////////////////////////////////////////////
			//部品の画面への登録
			///////////////////////////////////////////////////////////////////
			BaseList.Add(yesbutton);
			BaseList.Add(nobutton);
		}
		
		
		public override void Draw()
		{
			base.Draw ();
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
		}
		
		public override void MouseMove(System.Drawing.Point p)
		{
			base.MouseMove (p);
		}
	}
}
