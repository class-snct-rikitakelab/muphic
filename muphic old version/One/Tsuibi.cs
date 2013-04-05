using System;
using System.Drawing;

namespace muphic.One
{
	/// <summary>
	/// Tsuibi �̊T�v�̐����ł��B
	/// </summary>
	public class Tsuibi : Base
	{
		OneScreen parent;
		public Point point;							//���ɋ�؂�ꂽ���W
		public Tsuibi(OneScreen one)
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
			Rectangle rones = PointManager.Get(parent.ones.ToString());		//�E�̃{�^���Q�̗̈�
			//Rectangle r = PointManager.Get(parent.score.ToString());				//Score�̍��W���擾
//			r.Width = 1024 - r.X;													//�E�̂ق��܂Œǔ�������
//			if(r.Left <= p.X && p.X <= r.Right && r.Top <= p.Y && p.Y <= r.Bottom)
//			{
//				parent.tsuibi.Visible = true;										//�y���̒��Ȃ�\������
//			}
//			else
//			{
//				parent.tsuibi.Visible = false;										//�y���̊O�Ȃ�\�����Ȃ�
//			}
			if(Common.CommonTools.inRect(p, rscore) || Common.CommonTools.inRect(p, rones))
			{
				parent.tsuibi.Visible = true;
			}
			else
			{
				parent.tsuibi.Visible = false;
			}

			if(parent.score.isPlay)
			{
				this.Visible = false;
			}
		}
	}
}
