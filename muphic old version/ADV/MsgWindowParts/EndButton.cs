using System;
using muphic.Common;

namespace muphic.ADV.MsgWindowParts
{
	/// <summary>
	/// BackButton �̊T�v�̐����ł��B
	/// </summary>
	public class EndButton : Base
	{
		public MsgWindow parent;
		
		public EndButton(MsgWindow msgwindow)
		{
			this.parent = msgwindow;
			
			// �`���[�g���A���łȂ���Ε\�������Ȃ�
			this.Visible = TutorialStatus.getIsTutorial()? true : false;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �_�C�A���O�\��
			((TutorialScreen)this.parent.parent.parent).tutorialmain.isEndDialog = true;
			
			// �`���[�g���A���Ǘ����̃_�C�A���O���\�����ꂽ���Ƃ�TutorialStatus�ɒm�点��
			TutorialStatus.setEnableTutorialDialog();
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
