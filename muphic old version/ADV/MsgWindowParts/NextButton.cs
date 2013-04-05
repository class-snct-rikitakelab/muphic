using System;
using muphic.Common;

namespace muphic.ADV.MsgWindowParts
{
	/// <summary>
	/// NextButton �̊T�v�̐����ł��B
	/// </summary>
	public class NextButton : Base
	{
		public MsgWindow parent;
		
		public NextButton(MsgWindow msgwindow)
		{
			this.parent = msgwindow;
		}
		
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �`���[�g���A�����s���̓N���b�N�����ŏ���ɐi�ނ�Ńp�X
			if(!TutorialStatus.getIsTutorial())
			{	
				parent.parent.NextState();
			}
			else if(TutorialStatus.getIsSPMode() == "PT02_Link30" || TutorialStatus.getIsSPMode() == "PT03_Story50" || TutorialStatus.getIsSPMode() == "PT04_One02")
			{
				// ���R���쒆�̏ꍇ�̓q���g�ŏo�Ă�n�Y�Ȃ̂ŁA���փ{�^���ŃE�B���h�E������
				this.parent.ChangeWindowCoordinate(0);
			}
		}
		
		public override void MouseEnter()
		{
			base.MouseEnter ();
			this.State = 1;
		}
		
		public override void MouseLeave()
		{
			base.MouseLeave ();
			this.State = 0;
		}
	}
}
