using System;
using System.Drawing;

namespace muphic.Story.ScoreParts
{
	/// <summary>
	/// ScrollBar �̊T�v�̐����ł��B
	/// </summary>
	public class ScrollBar : Screen
	{
		Score parent;
		Point min = new Point(145, 697);							//�X�N���[���o�[�̒l���ŏ��̎��̕��i���W
		Point max = new Point(543, 697);							//�X�N���[���o�[�̒l���ő�̎��̕��i���W
		Size BarSize = new Size(306, 38);
		public float Percent = 0;									//�X�N���[���o�[����̉�%�̈�ɂ��邩������������

		bool isDrag;												//�o�[�����݃h���b�O��Ԃɂ��邩�ǂ���
		public ScrollBar(Score score)
		{
			parent = score;
			DrawManager.Regist("ScrollBar", 145, 697, "image\\one\\parts\\scroll\\bar.png");//min 145 max 543
		}

		public override void Draw()
		{
			int nowX;
			nowX = this.PercenttoPoint(Percent);
			DrawManager.Draw("ScrollBar", nowX, min.Y);
		}

		public override void DragBegin(Point begin)
		{
			base.DragBegin (begin);
			Rectangle r = new Rectangle(new Point(this.PercenttoPoint(Percent), 697), this.BarSize);	//���݂̃o�[�̈ʒu������o��
			if(this.inRect(begin, r))										//�}�E�X���o�[�̒��ɓ����Ă���Ȃ�
			{																//�h���b�O�������{��
				this.isDrag = true;
				Percent = this.PointtoPercent(begin.X - BarSize.Width / 2);		//�h���b�O���n�܂����Ƃ�
				parent.ChangeScroll(Percent);									//�܂��o�[�̈ʒu���}�E�X�̒����Ɏ����Ă���
			}
			else															//�}�E�X���o�[�̒��ɓ����Ă��Ȃ��Ȃ�
			{																//�N���b�N�������{��
				this.isDrag = false;
			}
		}


		public override void Drag(Point begin, Point p)
		{
			base.Drag (begin, p);
			if(isDrag)
			{
				Percent = this.PointtoPercent(p.X - BarSize.Width/2);			//�h���b�O����
				parent.ChangeScroll(Percent);									//�o�[�̈ʒu���}�E�X�̒����Ɏ����Ă���
			}
		}

		public override void Click(Point p)
		{
			base.Click (p);
			if(!this.isDrag)												//�N���b�N�������Ȃ�
			{
				float per = this.PointtoPercent(p.X - BarSize.Width/2);		//�}�E�X������ꏊ�̃p�[�Z���g������o��
				if(this.Percent < per)										//�}�E�X���o�[���E�ɂ���Ȃ�
				{
					parent.RightScroll();									//�E��1���ߓ�����
				}
				else														//�}�E�X���o�[��荶�ɂ���Ȃ�
				{
					parent.LeftScroll();									//����1���ߓ�����
				}
			}
		}

		/// <summary>
		/// ���݂̍��W���p�[�Z���g�\���ɕϊ����郁�\�b�h
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		private float PointtoPercent(int p)
		{
			return (float)(p - min.X) / (max.X - min.X) * 100;
		}

		/// <summary>
		/// ���݂̃p�[�Z���g�����W�ɕϊ����郁�\�b�h
		/// </summary>
		/// <param name="percent"></param>
		/// <returns></returns>
		private int PercenttoPoint(float percent)
		{
			return min.X + (int)((max.X - min.X) * percent / 100);
		}
	}
}
