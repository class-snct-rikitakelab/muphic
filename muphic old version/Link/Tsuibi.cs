using System;
using System.Drawing;

namespace muphic.Link
{
	/// <summary>
	/// Tsuibi �̊T�v�̐����ł��B
	/// </summary>
	public class Tsuibi : Base
	{
		LinkScreen parent;
		public Point point;							//���ɋ�؂�ꂽ���W
		public Tsuibi(LinkScreen link)
		{
			parent = link;
			State = 11;								//������Ԃ͂Ȃɂ��\�������Ȃ�
		}

		public void MouseMove(Point p)
		{
			this.point = muphic.Common.ScoreTools.DecidePoint(p);//�����}�E�X���y���̒��ł���΁A���W���A���K��ʒu�Ȃǂŋ�؂�B

			if (parent.score.isPlay == true || parent.LinkScreenMode == muphic.LinkScreenMode.AnswerDialog)
			{
				this.Visible = false;
			}
		}
	}
}
