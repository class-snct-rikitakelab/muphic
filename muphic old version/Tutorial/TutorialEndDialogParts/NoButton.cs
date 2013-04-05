using System;

namespace muphic.Tutorial.TutorialEndDialogParts
{
	/// <summary>
	/// NoButton �̊T�v�̐����ł��B
	/// </summary>
	public class NoButton : Base
	{
		public TutorialEndDialog parent;
		
		public NoButton(TutorialEndDialog dialog)
		{
			this.parent = dialog;
		}
		
		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �_�C�A���O��\��
			parent.parent.isEndDialog = false;
			
			// �`���[�g���A���Ǘ����̃_�C�A���O����\���ɂȂ������Ƃ�TutorialStatus�ɒʒm
			muphic.Common.TutorialStatus.setDisableTutorialDialog();
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
