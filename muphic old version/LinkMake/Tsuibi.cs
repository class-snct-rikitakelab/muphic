using System;
using System.Drawing;

namespace muphic.LinkMake
{
	/// <summary>
	/// Tsuibi �̊T�v�̐����ł��B
	/// </summary>
	public class Tsuibi : Base
	{
		LinkMakeScreen parent;
		public Point point;							//���ɋ�؂�ꂽ���W
		public Tsuibi(LinkMakeScreen one)
		{
			parent = one;
			this.Visible = true;
		}

		public void MouseMove(Point p)
		{
			this.point = muphic.Common.ScoreTools.DecidePoint(p);//�����}�E�X���y���̒��ł���΁A���W���A���K��ʒu�Ȃǂ�
																//��؂�B
			Rectangle rscore = Common.ScoreTools.score;						//Score�̓��̗̈���擾
			rscore.Width = 1024 - rscore.X;									//Score�ƉE�Ƃ̊Ԃ̂킸���Ȍ��Ԃ�h��
			Rectangle rones = PointManager.Get(parent.linkmakes.ToString());//�E�̃{�^���Q�̗̈�

			if(Common.CommonTools.inRect(p, rscore) || Common.CommonTools.inRect(p, rones))
			{
				parent.tsuibi.Visible = true;
			}
			else
			{
				parent.tsuibi.Visible = false;
			}

			if (parent.score.isPlay == true)
			{
				this.Visible = false;
			}
		}
	}
}
