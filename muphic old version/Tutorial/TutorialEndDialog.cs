using System;
using muphic.Tutorial.TutorialEndDialogParts;

namespace muphic.Tutorial
{
	/// <summary>
	/// TutorialEndDialog �̊T�v�̐����ł��B
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
			//���i�̃C���X�^���X��
			///////////////////////////////////////////////////////////////////
			yesbutton = new YesButton(this);
			nobutton = new NoButton(this);
			
			///////////////////////////////////////////////////////////////////
			//���i�̃e�N�X�`���E���W�̓o�^
			///////////////////////////////////////////////////////////////////
			muphic.DrawManager.Regist(this.ToString(), 236, 251,  "image\\TutorialXGA\\EndDialog\\dialog_end.png");
			muphic.DrawManager.Regist(yesbutton.ToString(), 382, 434, "image\\TutorialXGA\\EndDialog\\yes_off.png",  "image\\TutorialXGA\\EndDialog\\yes_on.png");
			muphic.DrawManager.Regist(nobutton.ToString(), 531, 434, "image\\TutorialXGA\\EndDialog\\no_off.png",  "image\\TutorialXGA\\EndDialog\\no_on.png");
			
			///////////////////////////////////////////////////////////////////
			//���i�̉�ʂւ̓o�^
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
