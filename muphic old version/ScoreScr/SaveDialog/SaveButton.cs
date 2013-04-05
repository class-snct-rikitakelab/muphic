using System;

namespace muphic.ScoreScr.SaveDialog
{
	/// <summary>
	/// SaveButton �̊T�v�̐����ł��B
	/// </summary>
	public class SaveButton : Base
	{
		public ScoreSaveDialog parent;

		public SaveButton(ScoreSaveDialog dialog)
		{
			this.parent = dialog;
		}

		public override void Click(System.Drawing.Point p)
		{
			base.Click (p);
			
			// �薼����Ȃ牽�����Ȃ�
			if( this.parent.titlename == null ) return;

			// �t�@�C���������݃N���X���C���X�^���X�����Ă݂�
			ScoreFileWriter sfw = new ScoreFileWriter(this.parent.parent.AnimalList);
			
			// ���ۂɏ������� �������A�������݂Ɏ��s�����炻�̂܂�
			if( !sfw.WriteMSDFile(this.parent.titlename) ) return;

			// �߂�{�^�������������Ƃɂ��āA�_�C�A���O�����
			parent.back.Click(System.Drawing.Point.Empty);
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
