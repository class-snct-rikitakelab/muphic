using System;

namespace muphic.ScoreScr
{
	/// <summary>
	/// ReadButton �̊T�v�̐����ł��B
	/// </summary>
	public class ReadButton : Base
	{
		public ScoreScreen parent;

		public ReadButton(ScoreScreen score)
		{
			this.parent = score;
			if(this.parent.ParentScreenMode == muphic.ScreenMode.LinkScreen) this.Visible = false;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			parent.isSelectDialog = true;

			// �t�@�C���ꗗ�̎擾
			// �V�����ۑ������t�@�C�����t�@�C���ꗗ�ɏo����悤�ɂ��邽��
			//parent.sedialog.sbs.doSearchScoreDataFiles(); �{�^���̂ق��������Ȃ��̂ŋp��
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
